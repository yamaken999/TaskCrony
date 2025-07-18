using System.Text;

namespace TaskCrony;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Shift_JISエンコーディングサポートを有効化
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }    
}