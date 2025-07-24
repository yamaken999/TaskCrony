using System.Text;

namespace TaskCrony;

/// <summary>
/// TaskCrony ログ管理クラス v1.2.0
/// </summary>
public static class Logger
{
    private static readonly string _logDirectory;
    private static readonly string _logFilePath;
    private static readonly object _lockObject = new();

    static Logger()
    {
        _logDirectory = Path.Combine(Application.StartupPath, "Logs");
        _logFilePath = Path.Combine(_logDirectory, $"TaskCrony_{DateTime.Now:yyyyMMdd}.log");
        
        // ログディレクトリが存在しない場合は作成
        if (!Directory.Exists(_logDirectory))
        {
            Directory.CreateDirectory(_logDirectory);
        }
    }

    /// <summary>
    /// 情報レベルのログを出力
    /// </summary>
    public static void Info(string message)
    {
        WriteLog("INFO", message);
    }

    /// <summary>
    /// 警告レベルのログを出力
    /// </summary>
    public static void Warning(string message)
    {
        WriteLog("WARN", message);
    }

    /// <summary>
    /// エラーレベルのログを出力
    /// </summary>
    public static void Error(string message, Exception? exception = null)
    {
        var logMessage = exception != null 
            ? $"{message}\n例外詳細: {exception}" 
            : message;
        WriteLog("ERROR", logMessage);
    }

    /// <summary>
    /// デバッグレベルのログを出力
    /// </summary>
    public static void Debug(string message)
    {
#if DEBUG
        WriteLog("DEBUG", message);
#endif
    }

    /// <summary>
    /// タスク実行ログを出力
    /// </summary>
    public static void TaskExecution(string taskName, string action, bool success, string? details = null)
    {
        var status = success ? "成功" : "失敗";
        var message = $"タスク: {taskName} | アクション: {action} | 結果: {status}";
        if (!string.IsNullOrEmpty(details))
        {
            message += $" | 詳細: {details}";
        }
        WriteLog("TASK", message);
    }

    /// <summary>
    /// ログファイルにメッセージを書き込み
    /// </summary>
    private static void WriteLog(string level, string message)
    {
        try
        {
            lock (_lockObject)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var logEntry = $"[{timestamp}] [{level}] {message}";
                
                // ファイルに追記（UTF-8エンコーディング）
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                
                // デバッグ出力にも送信
                System.Diagnostics.Debug.WriteLine(logEntry);
            }
        }
        catch (Exception ex)
        {
            // ログ書き込みエラーはデバッグ出力のみ
            System.Diagnostics.Debug.WriteLine($"ログ書き込みエラー: {ex.Message}");
        }
    }

    /// <summary>
    /// 古いログファイルをクリーンアップ（7日以上古いファイルを削除）
    /// </summary>
    public static void CleanupOldLogs()
    {
        try
        {
            if (!Directory.Exists(_logDirectory)) return;

            var cutoffDate = DateTime.Now.AddDays(-7);
            var logFiles = Directory.GetFiles(_logDirectory, "TaskCrony_*.log");

            foreach (var logFile in logFiles)
            {
                var fileInfo = new FileInfo(logFile);
                if (fileInfo.CreationTime < cutoffDate)
                {
                    File.Delete(logFile);
                    Debug($"古いログファイルを削除: {logFile}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug($"ログクリーンアップエラー: {ex.Message}");
        }
    }

    /// <summary>
    /// 今日のログファイルパスを取得
    /// </summary>
    public static string GetTodayLogFilePath()
    {
        return _logFilePath;
    }

    /// <summary>
    /// ログディレクトリパスを取得
    /// </summary>
    public static string GetLogDirectory()
    {
        return _logDirectory;
    }

    /// <summary>
    /// 最新のログエントリを指定行数分取得
    /// </summary>
    public static string[] GetRecentLogEntries(int maxLines = 100)
    {
        try
        {
            if (!File.Exists(_logFilePath)) return Array.Empty<string>();

            var lines = File.ReadAllLines(_logFilePath, Encoding.UTF8);
            return lines.TakeLast(maxLines).ToArray();
        }
        catch (Exception ex)
        {
            Debug($"ログ読み込みエラー: {ex.Message}");
            return Array.Empty<string>();
        }
    }
}
