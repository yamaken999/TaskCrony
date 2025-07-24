using System.Diagnostics;

namespace TaskCrony;

/// <summary>
/// ログビューアーフォーム
/// </summary>
public partial class LogViewerForm : Form
{
    private System.Windows.Forms.Timer? _refreshTimer;

    public LogViewerForm()
    {
        InitializeComponent();
        InitializeEvents();
        InitializeTimer();
        ModernTheme.ApplyToForm(this);
        LoadLogs();
        
        // デフォルト選択
        comboBoxLogLevel.SelectedIndex = 0; // "すべて"
    }

    /// <summary>
    /// イベントハンドラーの初期化
    /// </summary>
    private void InitializeEvents()
    {
        buttonRefresh.Click += ButtonRefresh_Click;
        buttonClear.Click += ButtonClear_Click;
        buttonOpenLogFolder.Click += ButtonOpenLogFolder_Click;
        buttonClose.Click += ButtonClose_Click;
        comboBoxLogLevel.SelectedIndexChanged += ComboBoxLogLevel_SelectedIndexChanged;
        
        // フォーム閉じるイベント
        this.FormClosing += LogViewerForm_FormClosing;
    }

    /// <summary>
    /// 自動更新タイマーの初期化
    /// </summary>
    private void InitializeTimer()
    {
        _refreshTimer = new System.Windows.Forms.Timer();
        _refreshTimer.Interval = 2000; // 2秒間隔
        _refreshTimer.Tick += RefreshTimer_Tick;
        _refreshTimer.Start();
    }

    /// <summary>
    /// 更新ボタンクリックイベント
    /// </summary>
    private void ButtonRefresh_Click(object? sender, EventArgs e)
    {
        LoadLogs();
    }

    /// <summary>
    /// クリアボタンクリックイベント
    /// </summary>
    private void ButtonClear_Click(object? sender, EventArgs e)
    {
        textBoxLogs.Clear();
    }

    /// <summary>
    /// ログフォルダ開くボタンクリックイベント
    /// </summary>
    private void ButtonOpenLogFolder_Click(object? sender, EventArgs e)
    {
        try
        {
            var logDirectory = Logger.GetLogDirectory();
            Process.Start(new ProcessStartInfo
            {
                FileName = logDirectory,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"ログフォルダを開けませんでした:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// 閉じるボタンクリックイベント
    /// </summary>
    private void ButtonClose_Click(object? sender, EventArgs e)
    {
        this.Close();
    }

    /// <summary>
    /// ログレベル選択変更イベント
    /// </summary>
    private void ComboBoxLogLevel_SelectedIndexChanged(object? sender, EventArgs e)
    {
        LoadLogs();
    }

    /// <summary>
    /// 自動更新タイマーイベント
    /// </summary>
    private void RefreshTimer_Tick(object? sender, EventArgs e)
    {
        if (this.Visible && !this.IsDisposed)
        {
            LoadLogs();
        }
    }

    /// <summary>
    /// フォーム閉じるイベント
    /// </summary>
    private void LogViewerForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        _refreshTimer?.Stop();
        _refreshTimer?.Dispose();
    }

    /// <summary>
    /// ログを読み込んで表示
    /// </summary>
    private void LoadLogs()
    {
        try
        {
            var logEntries = Logger.GetRecentLogEntries(500); // 最新500行
            var selectedLevel = comboBoxLogLevel.SelectedItem?.ToString() ?? "すべて";
            
            // ログレベルでフィルタリング
            if (selectedLevel != "すべて")
            {
                logEntries = logEntries.Where(entry => entry.Contains($"[{selectedLevel}]")).ToArray();
            }
            
            var currentText = textBoxLogs.Text;
            var newText = string.Join(Environment.NewLine, logEntries);
            
            // テキストが変更された場合のみ更新
            if (currentText != newText)
            {
                var shouldScroll = checkBoxAutoScroll.Checked && 
                    (textBoxLogs.SelectionStart >= textBoxLogs.Text.Length - 10);
                
                textBoxLogs.Text = newText;
                
                // 自動スクロールが有効な場合は最下部へ移動
                if (shouldScroll)
                {
                    textBoxLogs.SelectionStart = textBoxLogs.Text.Length;
                    textBoxLogs.ScrollToCaret();
                }
                
                // ログエントリーに応じた色付け
                ApplyLogColoring();
            }
        }
        catch (Exception ex)
        {
            textBoxLogs.Text = $"ログ読み込みエラー: {ex.Message}";
        }
    }

    /// <summary>
    /// ログレベルに応じた色付けを適用
    /// </summary>
    private void ApplyLogColoring()
    {
        // RichTextBoxではないため、基本的な色付けのみ
        // ERRORとWARNが含まれる場合の背景色を変更
        var text = textBoxLogs.Text;
        if (text.Contains("[ERROR]"))
        {
            textBoxLogs.BackColor = Color.FromArgb(40, 20, 20); // 暗い赤
        }
        else if (text.Contains("[WARN]"))
        {
            textBoxLogs.BackColor = Color.FromArgb(40, 40, 20); // 暗い黄
        }
        else
        {
            textBoxLogs.BackColor = Color.Black; // 通常の黒
        }
    }

    /// <summary>
    /// リソースの解放
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
