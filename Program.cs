using System.Text;
using System.Reflection;
using System.Runtime.Loader;

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
        // libsフォルダからのDLL読み込みを設定
        AssemblyLoadContext.Default.Resolving += OnResolving;
        
        // Shift_JISエンコーディングサポートを有効化（仕様書4.3.1準拠）
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        // アプリケーション設定の初期化
        ApplicationConfiguration.Initialize();
        
        // メインフォームを実行
        Application.Run(new MainForm());
    }
    
    /// <summary>
    /// アセンブリ解決イベントハンドラ - libsフォルダからDLLを読み込み
    /// </summary>
    private static Assembly? OnResolving(AssemblyLoadContext context, AssemblyName assemblyName)
    {
        try
        {
            // まず標準の場所から読み込みを試行
            var exeDir = AppDomain.CurrentDomain.BaseDirectory;
            var assemblyPath = Path.Combine(exeDir, assemblyName.Name + ".dll");
            
            if (File.Exists(assemblyPath))
            {
                return context.LoadFromAssemblyPath(assemblyPath);
            }
            
            // libsフォルダから読み込みを試行
            var libsPath = Path.Combine(exeDir, "libs", assemblyName.Name + ".dll");
            if (File.Exists(libsPath))
            {
                return context.LoadFromAssemblyPath(libsPath);
            }
        }
        catch
        {
            // 読み込み失敗時は標準の解決メカニズムに委譲
        }
        
        return null;
    }
}
