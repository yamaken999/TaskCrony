using System.Net.Http;
using System.Text.Json;

namespace TaskCrony;

/// <summary>
/// TaskCrony バージョンチェッククラス v1.3.2
/// GitHub Actions ビルドテスト用コメント更新 - 2025/07/28
/// </summary>
public static class VersionChecker
{
    private const string GITHUB_API_URL = "https://api.github.com/repos/yamaken999/TaskCrony/releases/latest";
    private const string CURRENT_VERSION = "1.3.2";
    
    /// <summary>
    /// バージョン情報
    /// </summary>
    public class VersionInfo
    {
        public string Version { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public string ReleaseNotes { get; set; } = "";
        public DateTime PublishedAt { get; set; }
        public bool IsNewer { get; set; }
    }

    /// <summary>
    /// 現在のバージョンを取得
    /// </summary>
    public static string GetCurrentVersion()
    {
        return CURRENT_VERSION;
    }

    /// <summary>
    /// 最新バージョンを非同期でチェック
    /// </summary>
    public static async Task<VersionInfo?> CheckLatestVersionAsync()
    {
        try
        {
            Logger.Info("最新バージョンチェック開始");
            
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", $"TaskCrony/{CURRENT_VERSION}");
            client.Timeout = TimeSpan.FromSeconds(10);

            var response = await client.GetStringAsync(GITHUB_API_URL);
            var jsonDoc = JsonDocument.Parse(response);
            var root = jsonDoc.RootElement;

            var latestVersion = root.GetProperty("tag_name").GetString()?.TrimStart('v') ?? "";
            var downloadUrl = "";
            var releaseNotes = root.GetProperty("body").GetString() ?? "";
            var publishedAt = DateTime.Parse(root.GetProperty("published_at").GetString() ?? DateTime.Now.ToString());

            // アセットからZIPファイルのダウンロードURLを取得
            if (root.TryGetProperty("assets", out var assets))
            {
                foreach (var asset in assets.EnumerateArray())
                {
                    var assetName = asset.GetProperty("name").GetString() ?? "";
                    if (assetName.Contains("Release.zip"))
                    {
                        downloadUrl = asset.GetProperty("browser_download_url").GetString() ?? "";
                        break;
                    }
                }
            }

            var isNewer = IsVersionNewer(latestVersion, CURRENT_VERSION);
            
            var versionInfo = new VersionInfo
            {
                Version = latestVersion,
                DownloadUrl = downloadUrl,
                ReleaseNotes = releaseNotes,
                PublishedAt = publishedAt,
                IsNewer = isNewer
            };

            Logger.Info($"バージョンチェック完了 - 現在: {CURRENT_VERSION}, 最新: {latestVersion}, 更新可能: {isNewer}");
            return versionInfo;
        }
        catch (HttpRequestException ex)
        {
            Logger.Warning($"バージョンチェック失敗 (ネットワークエラー): {ex.Message}");
            return null;
        }
        catch (TaskCanceledException ex)
        {
            Logger.Warning($"バージョンチェック失敗 (タイムアウト): {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Logger.Error("バージョンチェック中に予期しないエラーが発生", ex);
            return null;
        }
    }

    /// <summary>
    /// バージョン番号を比較（セマンティックバージョニング対応）
    /// </summary>
    private static bool IsVersionNewer(string latestVersion, string currentVersion)
    {
        try
        {
            var latest = ParseVersion(latestVersion);
            var current = ParseVersion(currentVersion);

            // メジャーバージョン比較
            if (latest.Major > current.Major) return true;
            if (latest.Major < current.Major) return false;

            // マイナーバージョン比較
            if (latest.Minor > current.Minor) return true;
            if (latest.Minor < current.Minor) return false;

            // パッチバージョン比較
            return latest.Patch > current.Patch;
        }
        catch (Exception ex)
        {
            Logger.Warning($"バージョン比較エラー: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// バージョン文字列を解析
    /// </summary>
    private static (int Major, int Minor, int Patch) ParseVersion(string version)
    {
        var parts = version.Split('.');
        var major = parts.Length > 0 && int.TryParse(parts[0], out var maj) ? maj : 0;
        var minor = parts.Length > 1 && int.TryParse(parts[1], out var min) ? min : 0;
        var patch = parts.Length > 2 && int.TryParse(parts[2], out var pat) ? pat : 0;
        
        return (major, minor, patch);
    }

    /// <summary>
    /// 更新ダイアログを表示
    /// </summary>
    public static DialogResult ShowUpdateDialog(VersionInfo versionInfo, Form? parentForm = null)
    {
        var message = $"新しいバージョンが利用可能です！\n\n" +
                     $"現在のバージョン: {CURRENT_VERSION}\n" +
                     $"最新バージョン: {versionInfo.Version}\n" +
                     $"リリース日: {versionInfo.PublishedAt:yyyy年MM月dd日}\n\n" +
                     $"今すぐダウンロードページを開きますか？";

        var result = MessageBox.Show(message, "TaskCrony 更新通知", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Information);

        if (result == DialogResult.Yes && !string.IsNullOrEmpty(versionInfo.DownloadUrl))
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = versionInfo.DownloadUrl,
                    UseShellExecute = true
                });
                Logger.Info($"ダウンロードページを開きました: {versionInfo.DownloadUrl}");
            }
            catch (Exception ex)
            {
                Logger.Error("ダウンロードページを開けませんでした", ex);
                MessageBox.Show($"ダウンロードページを開けませんでした:\n{ex.Message}", "エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        return result;
    }

    /// <summary>
    /// バックグラウンドで定期的にバージョンチェックを実行
    /// </summary>
    public static async Task StartPeriodicVersionCheckAsync(Action<VersionInfo> onUpdateAvailable)
    {
        await Task.Run(async () =>
        {
            try
            {
                // アプリ起動から5秒後にチェック実行
                await Task.Delay(5000);
                
                var versionInfo = await CheckLatestVersionAsync();
                if (versionInfo?.IsNewer == true)
                {
                    // UIスレッドで更新通知を実行
                    await Task.Run(() => onUpdateAvailable(versionInfo));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("定期バージョンチェック中にエラーが発生", ex);
            }
        });
    }
}
