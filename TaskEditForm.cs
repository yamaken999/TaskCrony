using Microsoft.Win32.TaskScheduler;
using System.Text;
using System.Globalization;

namespace TaskCrony;

/// <summary>
/// タスク編集フォーム
/// </summary>
public partial class TaskEditForm : Form
{
    private readonly string _taskName;
    private readonly string _batFolderPath;
    private FolderBrowserDialog? _folderBrowserDialog;
    private Microsoft.Win32.TaskScheduler.Task? _existingTask;

    /// <summary>
    /// 編集結果
    /// </summary>
    public bool TaskUpdated { get; private set; } = false;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public TaskEditForm(string taskName, string batFolderPath)
    {
        InitializeComponent();
        _taskName = taskName;
        _batFolderPath = batFolderPath;
        
        // モダンテーマを適用
        ModernTheme.ApplyToForm(this);
        
        InitializeControls();
        LoadTaskData();
    }

    /// <summary>
    /// コントロールの初期化
    /// </summary>
    private void InitializeControls()
    {
        // フォームタイトル
        this.Text = $"タスク編集 - {_taskName}";
        
        // 間隔設定の初期化
        comboBoxInterval.Items.Clear();
        comboBoxInterval.Items.AddRange(new[] { "毎日", "毎週", "毎月" });
        comboBoxInterval.SelectedIndex = 0;
        
        // デフォルト値設定
        textBoxTaskName.Text = _taskName;
        textBoxDateOffset.Text = "0";
        
        // ラジオボタンのデフォルト設定
        checkBoxPrefixDateAfter.Checked = true;
        checkBoxSuffixDateBefore.Checked = true;
        checkBoxFolderPrefixDateAfter.Checked = true;
        checkBoxFolderSuffixDateBefore.Checked = true;
        
        // イベントハンドラーを登録
        RegisterEventHandlers();
        
        // 初期状態でのコントロールの有効/無効設定
        UpdateControlsEnabledState();
        
        // 初期プレビュー更新
        UpdatePreview();
    }

    /// <summary>
    /// イベントハンドラーの登録
    /// </summary>
    private void RegisterEventHandlers()
    {
        // テキストボックスの変更イベント
        textBoxFileName.TextChanged += (s, e) => UpdatePreview();
        textBoxPrefix.TextChanged += (s, e) => UpdatePreview();
        textBoxSuffix.TextChanged += (s, e) => UpdatePreview();
        textBoxFolderBaseName.TextChanged += (s, e) => UpdatePreview();
        textBoxFolderPrefix.TextChanged += (s, e) => UpdatePreview();
        textBoxFolderSuffix.TextChanged += (s, e) => UpdatePreview();
        textBoxDateOffset.TextChanged += (s, e) => UpdatePreview();
        
        // リプレース機能のイベントハンドラー
        if (textBoxReplaceFrom != null && textBoxReplaceTo != null)
        {
            textBoxReplaceFrom.TextChanged += (s, e) => UpdatePreview();
            textBoxReplaceTo.TextChanged += (s, e) => UpdatePreview();
        }
        
        // ラジオボタンの変更イベント
        checkBoxPrefixDateBefore.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxPrefixDateAfter.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxSuffixDateBefore.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxSuffixDateAfter.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxFolderPrefixDateBefore.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxFolderPrefixDateAfter.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxFolderSuffixDateBefore.CheckedChanged += (s, e) => UpdatePreview();
        checkBoxFolderSuffixDateAfter.CheckedChanged += (s, e) => UpdatePreview();
    }

    /// <summary>
    /// タスクデータの読み込み
    /// </summary>
    private void LoadTaskData()
    {
        try
        {
            using var taskService = new TaskService();
            _existingTask = taskService.GetTask(_taskName);
            
            if (_existingTask != null)
            {
                // タスク有効状態
                checkBoxEnableTask.Checked = _existingTask.Enabled;
                
                // トリガー情報の取得
                var trigger = _existingTask.Definition.Triggers.FirstOrDefault();
                if (trigger is DailyTrigger dailyTrigger)
                {
                    comboBoxInterval.SelectedIndex = 0; // 毎日
                    textBoxTime.Text = dailyTrigger.StartBoundary.ToString("HH:mm");
                }
                else if (trigger is WeeklyTrigger weeklyTrigger)
                {
                    comboBoxInterval.SelectedIndex = 1; // 毎週
                    textBoxTime.Text = weeklyTrigger.StartBoundary.ToString("HH:mm");
                }
                else if (trigger is MonthlyTrigger monthlyTrigger)
                {
                    comboBoxInterval.SelectedIndex = 2; // 毎月
                    textBoxTime.Text = monthlyTrigger.StartBoundary.ToString("HH:mm");
                }

                // アクション情報の取得（BATファイルから設定を解析）
                LoadSettingsFromBatFile();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクデータの読み込み中にエラーが発生しました: {ex.Message}", 
                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// BATファイルから設定を読み込み
    /// </summary>
    private void LoadSettingsFromBatFile()
    {
        try
        {
            // まず最新のBATファイルを探す（タスク名_日時の形式）
            var batFiles = Directory.GetFiles(_batFolderPath, $"{_taskName}_*.bat");
            if (batFiles.Length == 0)
            {
                // 古い形式のBATファイルも確認
                var oldBatFilePath = Path.Combine(_batFolderPath, $"{_taskName}.bat");
                if (File.Exists(oldBatFilePath))
                {
                    batFiles = new[] { oldBatFilePath };
                }
            }

            if (batFiles.Length > 0)
            {
                // 最新のファイルを選択
                var latestBatFile = batFiles.OrderByDescending(f => File.GetCreationTime(f)).First();
                var content = File.ReadAllText(latestBatFile, Encoding.UTF8);
                
                // 設定値の解析
                ParseBatFileContent(content);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"BATファイルの読み込み中にエラーが発生しました: {ex.Message}", 
                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// BATファイルの内容を解析
    /// </summary>
    private void ParseBatFileContent(string content)
    {
        var lines = content.Split('\n');
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            // copyコマンドの行からソースファイルを抽出
            if (trimmedLine.StartsWith("copy \"") && trimmedLine.Contains("\""))
            {
                var startIndex = trimmedLine.IndexOf("\"") + 1;
                var endIndex = trimmedLine.IndexOf("\"", startIndex);
                if (endIndex > startIndex)
                {
                    var sourceFile = trimmedLine.Substring(startIndex, endIndex - startIndex);
                    textBoxSourceFolder.Text = Path.GetDirectoryName(sourceFile) ?? "";
                    textBoxFileName.Text = Path.GetFileNameWithoutExtension(sourceFile);
                    checkBoxCopyFiles.Checked = true;
                }
            }
            
            // mkdirコマンドの行からコピー先フォルダを抽出
            if (trimmedLine.StartsWith("mkdir ") || trimmedLine.Contains("mkdir "))
            {
                checkBoxCreateFolder.Checked = true;
            }
            
            // call :build_filenameの行から設定を抽出
            if (trimmedLine.StartsWith("call :build_filename"))
            {
                var parts = trimmedLine.Split('"').Where(p => !string.IsNullOrWhiteSpace(p) && p != " ").ToArray();
                if (parts.Length >= 6)
                {
                    // ファイル用の設定
                    if (parts[1] != "Folder") // フォルダ名でない場合
                    {
                        if (parts.Length > 2) textBoxPrefix.Text = parts[2];
                        if (parts.Length > 3) textBoxSuffix.Text = parts[3];
                        if (parts.Length > 4) SetDatePosition(parts[4], true); // prefix
                        if (parts.Length > 5) SetDatePosition(parts[5], false); // suffix
                    }
                    else
                    {
                        // フォルダ用の設定
                        if (parts.Length > 2) textBoxFolderPrefix.Text = parts[2];
                        if (parts.Length > 3) textBoxFolderSuffix.Text = parts[3];
                        if (parts.Length > 4) SetFolderDatePosition(parts[4], true); // prefix
                        if (parts.Length > 5) SetFolderDatePosition(parts[5], false); // suffix
                    }
                }
            }
        }
        
        // 読み込み完了後にプレビューを更新
        UpdatePreview();
    }

    /// <summary>
    /// 日付位置設定を復元
    /// </summary>
    private void SetDatePosition(string position, bool isPrefix)
    {
        if (isPrefix)
        {
            checkBoxPrefixDateBefore.Checked = position == "before";
            checkBoxPrefixDateAfter.Checked = position == "after";
        }
        else
        {
            checkBoxSuffixDateBefore.Checked = position == "before";
            checkBoxSuffixDateAfter.Checked = position == "after";
        }
    }

    /// <summary>
    /// フォルダ日付位置設定を復元
    /// </summary>
    private void SetFolderDatePosition(string position, bool isPrefix)
    {
        if (isPrefix)
        {
            checkBoxFolderPrefixDateBefore.Checked = position == "before";
            checkBoxFolderPrefixDateAfter.Checked = position == "after";
        }
        else
        {
            checkBoxFolderSuffixDateBefore.Checked = position == "before";
            checkBoxFolderSuffixDateAfter.Checked = position == "after";
        }
    }

    /// <summary>
    /// コントロールの有効/無効状態を更新
    /// </summary>
    private void UpdateControlsEnabledState()
    {
        // ファイルコピー関連のコントロール
        bool fileCopyEnabled = checkBoxCopyFiles.Checked;
        textBoxFileName.Enabled = fileCopyEnabled;
        textBoxPrefix.Enabled = fileCopyEnabled;
        textBoxSuffix.Enabled = fileCopyEnabled;
        groupBoxPrefixOptions.Enabled = fileCopyEnabled;
        groupBoxSuffixOptions.Enabled = fileCopyEnabled;
        
        // フォルダ作成関連のコントロール
        bool folderCreateEnabled = checkBoxCreateFolder.Checked;
        textBoxFolderBaseName.Enabled = folderCreateEnabled;
        textBoxFolderPrefix.Enabled = folderCreateEnabled;
        textBoxFolderSuffix.Enabled = folderCreateEnabled;
        groupBoxFolderPrefixOptions.Enabled = folderCreateEnabled;
        groupBoxFolderSuffixOptions.Enabled = folderCreateEnabled;
    }

    /// <summary>
    /// プレビューを更新
    /// </summary>
    private void UpdatePreview()
    {
        var previewText = new StringBuilder();
        var sampleDate = DateTime.Now.AddDays(GetDateOffset());
        var dateStr = sampleDate.ToString("yyyyMMdd");
        
        // フォルダ名のプレビュー
        if (checkBoxCreateFolder.Checked && !string.IsNullOrWhiteSpace(textBoxFolderBaseName.Text))
        {
            var folderName = BuildFolderNameWithSettings(textBoxFolderBaseName.Text, dateStr);
            previewText.AppendLine($"フォルダ名: {folderName}");
        }
        
        // ファイル名のプレビュー
        if (checkBoxCopyFiles.Checked && !string.IsNullOrWhiteSpace(textBoxFileName.Text))
        {
            var baseFileName = textBoxFileName.Text;
            
            // リプレース機能の適用
            if (textBoxReplaceFrom != null && textBoxReplaceTo != null && 
                !string.IsNullOrEmpty(textBoxReplaceFrom.Text))
            {
                baseFileName = baseFileName.Replace(textBoxReplaceFrom.Text, textBoxReplaceTo.Text ?? "");
            }
            
            var fileName = BuildFileNameWithSettings(baseFileName, dateStr);
            previewText.AppendLine($"ファイル名: {fileName}");
        }
        
        if (previewText.Length == 0)
        {
            previewText.AppendLine("プレビューする項目がありません");
        }
        
        textBoxPreview.Text = previewText.ToString().TrimEnd();
    }

    /// <summary>
    /// 日付オフセットを取得
    /// </summary>
    private int GetDateOffset()
    {
        if (int.TryParse(textBoxDateOffset.Text, out int offset))
        {
            return offset;
        }
        return 0;
    }

    /// <summary>
    /// フォルダ名設定を適用してフォルダ名を構築
    /// </summary>
    private string BuildFolderNameWithSettings(string baseName, string dateStr)
    {
        var result = baseName;
        
        // プレフィックス処理
        if (!string.IsNullOrWhiteSpace(textBoxFolderPrefix.Text))
        {
            if (checkBoxFolderPrefixDateBefore.Checked)
            {
                result = dateStr + textBoxFolderPrefix.Text + result;
            }
            else
            {
                result = textBoxFolderPrefix.Text + dateStr + result;
            }
        }
        
        // サフィックス処理
        if (!string.IsNullOrWhiteSpace(textBoxFolderSuffix.Text))
        {
            if (checkBoxFolderSuffixDateBefore.Checked)
            {
                result = result + dateStr + textBoxFolderSuffix.Text;
            }
            else
            {
                result = result + textBoxFolderSuffix.Text + dateStr;
            }
        }
        
        return result;
    }

    /// <summary>
    /// ファイル名設定を適用してファイル名を構築
    /// </summary>
    private string BuildFileNameWithSettings(string baseName, string dateStr)
    {
        var result = baseName;
        
        // プレフィックス処理
        if (!string.IsNullOrWhiteSpace(textBoxPrefix.Text))
        {
            if (checkBoxPrefixDateBefore.Checked)
            {
                result = dateStr + textBoxPrefix.Text + result;
            }
            else
            {
                result = textBoxPrefix.Text + dateStr + result;
            }
        }
        
        // サフィックス処理
        if (!string.IsNullOrWhiteSpace(textBoxSuffix.Text))
        {
            if (checkBoxSuffixDateBefore.Checked)
            {
                result = result + dateStr + textBoxSuffix.Text;
            }
            else
            {
                result = result + textBoxSuffix.Text + dateStr;
            }
        }
        
        return result;
    }

    /// <summary>
    /// 新しいBATファイル内容を生成
    /// </summary>
    private string GenerateBatContent()
    {
        var batContent = new StringBuilder();
        
        // UTF-8 BOM付きの設定
        batContent.AppendLine("@echo off");
        batContent.AppendLine("chcp 65001 > nul");
        batContent.AppendLine();
        
        // 設定値をコメントとして保存
        batContent.AppendLine($"REM SourceFolder:{textBoxSourceFolder.Text}");
        batContent.AppendLine($"REM DestinationFolder:{textBoxDestinationFolder.Text}");
        batContent.AppendLine($"REM FileName:{textBoxFileName.Text}");
        batContent.AppendLine($"REM Prefix:{textBoxPrefix.Text}");
        batContent.AppendLine($"REM Suffix:{textBoxSuffix.Text}");
        batContent.AppendLine($"REM FolderBaseName:{textBoxFolderBaseName.Text}");
        batContent.AppendLine($"REM FolderPrefix:{textBoxFolderPrefix.Text}");
        batContent.AppendLine($"REM FolderSuffix:{textBoxFolderSuffix.Text}");
        batContent.AppendLine($"REM DateOffset:{textBoxDateOffset.Text}");
        batContent.AppendLine($"REM CopyFiles:{checkBoxCopyFiles.Checked}");
        batContent.AppendLine($"REM CreateFolder:{checkBoxCreateFolder.Checked}");
        batContent.AppendLine();
        
        // PowerShellコマンドで日付計算
        var dateOffset = GetDateOffset();
        batContent.AppendLine($"for /f %%i in ('powershell -Command \"(Get-Date).AddDays({dateOffset}).ToString('yyyyMMdd')\"') do set TARGET_DATE=%%i");
        batContent.AppendLine();
        
        // ファイル名構築関数の定義
        if (checkBoxCopyFiles.Checked && !string.IsNullOrWhiteSpace(textBoxFileName.Text))
        {
            batContent.AppendLine("REM ファイル名構築関数");
            batContent.AppendLine("call :BuildFileName");
            batContent.AppendLine();
        }
        
        // フォルダ名構築関数の定義
        if (checkBoxCreateFolder.Checked && !string.IsNullOrWhiteSpace(textBoxFolderBaseName.Text))
        {
            batContent.AppendLine("REM フォルダ名構築関数");
            batContent.AppendLine("call :BuildFolderName");
            batContent.AppendLine();
        }
        
        // フォルダ作成処理
        if (checkBoxCreateFolder.Checked && !string.IsNullOrWhiteSpace(textBoxFolderBaseName.Text))
        {
            batContent.AppendLine("REM フォルダ作成");
            batContent.AppendLine($"if not exist \"{textBoxDestinationFolder.Text}\\%FOLDER_NAME%\" (");
            batContent.AppendLine($"    mkdir \"{textBoxDestinationFolder.Text}\\%FOLDER_NAME%\"");
            batContent.AppendLine("    echo フォルダを作成しました: %FOLDER_NAME%");
            batContent.AppendLine(")");
            batContent.AppendLine();
        }
        
        // ファイルコピー処理
        if (checkBoxCopyFiles.Checked)
        {
            var destinationPath = checkBoxCreateFolder.Checked && !string.IsNullOrWhiteSpace(textBoxFolderBaseName.Text)
                ? $"{textBoxDestinationFolder.Text}\\%FOLDER_NAME%"
                : textBoxDestinationFolder.Text;
                
            batContent.AppendLine("REM ファイルコピー");
            batContent.AppendLine($"if exist \"{textBoxSourceFolder.Text}\" (");
            batContent.AppendLine($"    for %%f in (\"{textBoxSourceFolder.Text}\\*\") do (");
            batContent.AppendLine($"        copy \"%%f\" \"{destinationPath}\\%FILE_NAME%\"");
            batContent.AppendLine("        echo ファイルをコピーしました: %%~nxf -> %FILE_NAME%");
            batContent.AppendLine("    )");
            batContent.AppendLine(") else (");
            batContent.AppendLine("    echo エラー: コピー元フォルダが見つかりません");
            batContent.AppendLine(")");
            batContent.AppendLine();
        }
        
        batContent.AppendLine("goto :EOF");
        batContent.AppendLine();
        
        // ファイル名構築関数
        if (checkBoxCopyFiles.Checked && !string.IsNullOrWhiteSpace(textBoxFileName.Text))
        {
            batContent.AppendLine(":BuildFileName");
            batContent.AppendLine($"set FILE_NAME={textBoxFileName.Text}");
            
            // プレフィックス処理
            if (!string.IsNullOrWhiteSpace(textBoxPrefix.Text))
            {
                if (checkBoxPrefixDateBefore.Checked)
                {
                    batContent.AppendLine($"set FILE_NAME=%TARGET_DATE%{textBoxPrefix.Text}%FILE_NAME%");
                }
                else
                {
                    batContent.AppendLine($"set FILE_NAME={textBoxPrefix.Text}%TARGET_DATE%%FILE_NAME%");
                }
            }
            
            // サフィックス処理
            if (!string.IsNullOrWhiteSpace(textBoxSuffix.Text))
            {
                if (checkBoxSuffixDateBefore.Checked)
                {
                    batContent.AppendLine($"set FILE_NAME=%FILE_NAME%%TARGET_DATE%{textBoxSuffix.Text}");
                }
                else
                {
                    batContent.AppendLine($"set FILE_NAME=%FILE_NAME%{textBoxSuffix.Text}%TARGET_DATE%");
                }
            }
            
            batContent.AppendLine("goto :EOF");
            batContent.AppendLine();
        }
        
        // フォルダ名構築関数
        if (checkBoxCreateFolder.Checked && !string.IsNullOrWhiteSpace(textBoxFolderBaseName.Text))
        {
            batContent.AppendLine(":BuildFolderName");
            batContent.AppendLine($"set FOLDER_NAME={textBoxFolderBaseName.Text}");
            
            // プレフィックス処理
            if (!string.IsNullOrWhiteSpace(textBoxFolderPrefix.Text))
            {
                if (checkBoxFolderPrefixDateBefore.Checked)
                {
                    batContent.AppendLine($"set FOLDER_NAME=%TARGET_DATE%{textBoxFolderPrefix.Text}%FOLDER_NAME%");
                }
                else
                {
                    batContent.AppendLine($"set FOLDER_NAME={textBoxFolderPrefix.Text}%TARGET_DATE%%FOLDER_NAME%");
                }
            }
            
            // サフィックス処理
            if (!string.IsNullOrWhiteSpace(textBoxFolderSuffix.Text))
            {
                if (checkBoxFolderSuffixDateBefore.Checked)
                {
                    batContent.AppendLine($"set FOLDER_NAME=%FOLDER_NAME%%TARGET_DATE%{textBoxFolderSuffix.Text}");
                }
                else
                {
                    batContent.AppendLine($"set FOLDER_NAME=%FOLDER_NAME%{textBoxFolderSuffix.Text}%TARGET_DATE%");
                }
            }
            
            batContent.AppendLine("goto :EOF");
            batContent.AppendLine();
        }
        
        return batContent.ToString();
    }

    #region イベントハンドラー

    private void buttonSourceFolderBrowse_Click(object sender, EventArgs e)
    {
        _folderBrowserDialog ??= new FolderBrowserDialog();
        
        if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxSourceFolder.Text = _folderBrowserDialog.SelectedPath;
        }
    }

    private void buttonDestinationFolderBrowse_Click(object sender, EventArgs e)
    {
        _folderBrowserDialog ??= new FolderBrowserDialog();
        
        if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            textBoxDestinationFolder.Text = _folderBrowserDialog.SelectedPath;
        }
    }

    private void checkBoxCopyFiles_CheckedChanged(object sender, EventArgs e)
    {
        UpdateControlsEnabledState();
        UpdatePreview();
    }

    private void checkBoxCreateFolder_CheckedChanged(object sender, EventArgs e)
    {
        UpdateControlsEnabledState();
        UpdatePreview();
    }

    private void textBoxFileName_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void textBoxPrefix_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void textBoxSuffix_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void textBoxFolderBaseName_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void textBoxFolderPrefix_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void textBoxFolderSuffix_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void textBoxDateOffset_TextChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxPrefixDateBefore_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxPrefixDateAfter_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxSuffixDateBefore_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxSuffixDateAfter_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxFolderPrefixDateBefore_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxFolderPrefixDateAfter_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxFolderSuffixDateBefore_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void checkBoxFolderSuffixDateAfter_CheckedChanged(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        UpdatePreview();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        try
        {
            // 入力検証
            if (string.IsNullOrWhiteSpace(textBoxTaskName.Text))
            {
                MessageBox.Show("タスク名を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!checkBoxCopyFiles.Checked && !checkBoxCreateFolder.Checked)
            {
                MessageBox.Show("ファイルコピーまたはフォルダ作成のいずれかを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBoxCopyFiles.Checked && (string.IsNullOrWhiteSpace(textBoxSourceFolder.Text) || string.IsNullOrWhiteSpace(textBoxDestinationFolder.Text)))
            {
                MessageBox.Show("コピー元とコピー先フォルダを指定してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkBoxCreateFolder.Checked && string.IsNullOrWhiteSpace(textBoxDestinationFolder.Text))
            {
                MessageBox.Show("作成先フォルダを指定してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParseExact(textBoxTime.Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("実行時間を HH:mm 形式で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // BATファイルの更新
            UpdateBatFile();
            
            // タスクの更新
            UpdateTask();
            
            TaskUpdated = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの更新中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }

    #endregion

    /// <summary>
    /// BATファイルを更新
    /// </summary>
    private void UpdateBatFile()
    {
        var batFilePath = Path.Combine(_batFolderPath, $"{_taskName}.bat");
        var batContent = GenerateBatContent();
        
        File.WriteAllText(batFilePath, batContent, new UTF8Encoding(true));
    }

    /// <summary>
    /// タスクを更新
    /// </summary>
    private void UpdateTask()
    {
        if (_existingTask == null) return;

        using var taskService = new TaskService();
        var taskDefinition = _existingTask.Definition;
        
        // トリガーの更新
        taskDefinition.Triggers.Clear();
        
        var time = DateTime.ParseExact(textBoxTime.Text, "HH:mm", CultureInfo.InvariantCulture);
        var startTime = DateTime.Today.Add(time.TimeOfDay);
        
        if (startTime <= DateTime.Now)
        {
            startTime = startTime.AddDays(1);
        }
        
        switch (comboBoxInterval.SelectedIndex)
        {
            case 0: // 毎日
                var dailyTrigger = new DailyTrigger { StartBoundary = startTime };
                taskDefinition.Triggers.Add(dailyTrigger);
                break;
                
            case 1: // 毎週
                var weeklyTrigger = new WeeklyTrigger { StartBoundary = startTime, DaysOfWeek = DaysOfTheWeek.AllDays };
                taskDefinition.Triggers.Add(weeklyTrigger);
                break;
                
            case 2: // 毎月
                var monthlyTrigger = new MonthlyTrigger { StartBoundary = startTime };
                taskDefinition.Triggers.Add(monthlyTrigger);
                break;
        }
        
        // タスクの有効/無効設定
        taskDefinition.Settings.Enabled = checkBoxEnableTask.Checked;
        
        // タスクの更新
        taskService.RootFolder.RegisterTaskDefinition(_taskName, taskDefinition);
    }

}
