using System.Text;

namespace TaskCrony;

/// <summary>
/// TaskCrony v1.1.0 メインプログラム
/// </summary>
static class Program
{
    /// <summary>
    /// アプリケーションのメインエントリポイント
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Shift_JISエンコーディングサポートを有効化（仕様書4.3.1準拠）
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        // アプリケーション設定の初期化
        ApplicationConfiguration.Initialize();
        
        // メインフォームを実行
        Application.Run(new MainForm());
    }
}
