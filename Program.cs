using System.Text;
using System.Reflection;

namespace TaskCrony;

/// <summary>
/// TaskCrony v1.3.0 メインプログラム
/// </summary>
static class Program
{
    /// <summary>
    /// アプリケーションのメインエントリポイント
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            // 最初にログファイルにアプリケーション開始を記録
            LogApplicationStart();
            LogError("Debug: Main関数開始", new Exception("Debug情報"));
            
            // libsフォルダからのDLL読み込みを設定
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            LogError("Debug: AssemblyResolve設定完了", new Exception("Debug情報"));
            
            // 未処理例外のキャッチ
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.ThreadException += OnThreadException;
            LogError("Debug: 例外ハンドラ設定完了", new Exception("Debug情報"));
            
            // Shift_JISエンコーディングサポートを有効化（仕様書4.3.1準拠）
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LogError("Debug: エンコーディング設定完了", new Exception("Debug情報"));
            
            // アプリケーション設定の初期化
            ApplicationConfiguration.Initialize();
            LogError("Debug: ApplicationConfiguration.Initialize完了", new Exception("Debug情報"));
            
            // メインフォームをApplication.Run内で作成・実行
            LogError("Debug: Application.Run開始前", new Exception("Debug情報"));
            try
            {
                Application.Run(new MainForm());
                LogError("Debug: Application.Run終了", new Exception("Debug情報"));
            }
            catch (Exception ex)
            {
                LogError("Application.Run中にエラー", ex);
                MessageBox.Show($"Application.Runエラー:\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            // エラーをファイルとダイアログ両方に記録
            LogError("Main()でキャッチされた例外", ex);
            
            // 起動時エラーをユーザーに表示
            MessageBox.Show($"アプリケーションの起動中にエラーが発生しました:\n\n{ex.Message}\n\nスタックトレース:\n{ex.StackTrace}", 
                "TaskCrony 起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    /// <summary>
    /// アセンブリ解決イベントハンドラ - libsフォルダからDLLを読み込み
    /// </summary>
    private static Assembly? OnAssemblyResolve(object? sender, ResolveEventArgs args)
    {
        try
        {
            var assemblyName = new AssemblyName(args.Name);
            var dllName = assemblyName.Name + ".dll";
            
            // 実行ファイルのディレクトリを取得
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // まず実行ファイルと同じディレクトリから読み込みを試行
            var assemblyPath = Path.Combine(exeDir, dllName);
            if (File.Exists(assemblyPath))
            {
                return Assembly.LoadFrom(assemblyPath);
            }
            
            // libsフォルダから読み込みを試行
            var libsPath = Path.Combine(exeDir, "libs", dllName);
            if (File.Exists(libsPath))
            {
                return Assembly.LoadFrom(libsPath);
            }
        }
        catch (Exception ex)
        {
            // DLL読み込みエラーもファイルに記録
            LogError($"アセンブリ解決エラー: {args.Name}", ex);
        }
        
        return null;
    }
    
    /// <summary>
    /// アプリケーション開始をログに記録
    /// </summary>
    private static void LogApplicationStart()
    {
        try
        {
            var logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }
            
            var logFile = Path.Combine(logsDir, $"TaskCrony_{DateTime.Now:yyyyMMdd}.log");
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [INFO] TaskCrony v1.3.0 アプリケーション開始 (詳細デバッグモード){Environment.NewLine}";
            
            File.AppendAllText(logFile, logEntry);
        }
        catch
        {
            // ログ出力エラーは無視（アプリケーション開始を阻害しない）
        }
    }
    
    /// <summary>
    /// エラーをログファイルに記録
    /// </summary>
    private static void LogError(string message, Exception ex)
    {
        try
        {
            var logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }
            
            var logFile = Path.Combine(logsDir, $"TaskCrony_{DateTime.Now:yyyyMMdd}.log");
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [ERROR] {message}{Environment.NewLine}" +
                          $"エラー詳細: {ex.Message}{Environment.NewLine}" +
                          $"スタックトレース: {ex.StackTrace}{Environment.NewLine}" +
                          $"内部例外: {ex.InnerException?.ToString() ?? "なし"}{Environment.NewLine}";
            
            File.AppendAllText(logFile, logEntry);
        }
        catch
        {
            // ログ出力エラーは無視
        }
    }
    
    /// <summary>
    /// 未処理例外ハンドラ
    /// </summary>
    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            LogError("未処理例外が発生", ex);
            
            MessageBox.Show($"予期しないエラーが発生しました:\n\n{ex.Message}\n\nアプリケーションを終了します。", 
                "TaskCrony 致命的エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    /// <summary>
    /// スレッド例外ハンドラ
    /// </summary>
    private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
        LogError("スレッド例外が発生", e.Exception);
        
        MessageBox.Show($"スレッドエラーが発生しました:\n\n{e.Exception.Message}", 
            "TaskCrony スレッドエラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
