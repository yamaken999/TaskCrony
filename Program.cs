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
            // libsフォルダからのDLL読み込みを設定
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            
            // Shift_JISエンコーディングサポートを有効化（仕様書4.3.1準拠）
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            // アプリケーション設定の初期化
            ApplicationConfiguration.Initialize();
            
            // メインフォームを実行
            Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
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
            // デバッグ用：アセンブリ読み込みエラーをログに記録
            System.Diagnostics.Debug.WriteLine($"Assembly resolve error: {ex.Message}");
        }
        
        return null;
    }
}
