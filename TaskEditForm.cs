using Microsoft.Win32.TaskScheduler;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskCrony;

/// <summary>
/// タスク編集フォーム（仕様書4.2.2準拠）
/// </summary>
public partial class TaskEditForm : Form
{
    #region フィールド

    private readonly string _taskName;
    private readonly string _batFolderPath;
    private FolderBrowserDialog? _folderBrowserDialog;
    private OpenFileDialog? _openFileDialog;
    
    /// <summary>
    /// タスクが更新されたかどうか
    /// </summary>
    public bool TaskUpdated { get; private set; } = false;

    #endregion

    #region コンストラクタ

    /// <summary>
    /// タスク編集フォームのコンストラクタ
    /// </summary>
    /// <param name="taskName">編集対象のタスク名</param>
    /// <param name="batFolderPath">BATファイルフォルダのパス</param>
    public TaskEditForm(string taskName, string batFolderPath)
    {
        _taskName = taskName;
        _batFolderPath = batFolderPath;
        
        InitializeComponent();
        
        // モダンテーマを適用
        ModernTheme.ApplyToForm(this);
        
        InitializeControls();
        LoadTaskSettings();
    }

    #endregion

    #region 初期化メソッド

    /// <summary>
    /// コントロールの初期化
    /// </summary>
    private void InitializeControls()
    {
        this.Text = $"タスク編集 - {_taskName}";
        
        // イベントハンドラーの設定
        SetupEventHandlers();
        
        // ダイアログの初期化
        InitializeDialogs();
        
        // タスク名を設定（編集不可）
        textBoxTaskName.Text = _taskName;
        textBoxTaskName.ReadOnly = true;
        textBoxTaskName.BackColor = Color.FromArgb(240, 240, 240);
        
        // 初期状態でのコントロール有効/無効状態を設定
        UpdateControlsEnabledState();
    }

    /// <summary>
    /// イベントハンドラーの設定
    /// </summary>
    private void SetupEventHandlers()
    {
        // ボタンイベント
        buttonBrowseSource.Click += ButtonBrowseSource_Click;
        buttonBrowseDestination.Click += ButtonBrowseDestination_Click;
        buttonSaveTask.Click += ButtonSaveTask_Click;
        buttonCancel.Click += ButtonCancel_Click;
        
        // チェックボックスイベント
        checkBoxCreateFile.CheckedChanged += CheckBoxCreateFile_CheckedChanged;
        checkBoxCreateFolder.CheckedChanged += CheckBoxCreateFolder_CheckedChanged;
        
        // プレビュー関連イベント
        SetupPreviewEvents();
    }

    /// <summary>
    /// プレビュー関連イベントハンドラーの設定
    /// </summary>
    private void SetupPreviewEvents()
    {
        // ファイル設定
        radioPrefixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioPrefixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioPrefixDateNone.CheckedChanged += UpdateFileNamePreview;
        radioSuffixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioSuffixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioSuffixDateNone.CheckedChanged += UpdateFileNamePreview;
        textBoxPrefix.TextChanged += UpdateFileNamePreview;
        textBoxSuffix.TextChanged += UpdateFileNamePreview;
        textBoxReplaceFrom.TextChanged += UpdateFileNamePreview;
        textBoxReplaceTo.TextChanged += UpdateFileNamePreview;
        
        // フォルダ設定
        radioFolderPrefixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioFolderPrefixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioFolderPrefixDateNone.CheckedChanged += UpdateFileNamePreview;
        radioFolderSuffixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioFolderSuffixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioFolderSuffixDateNone.CheckedChanged += UpdateFileNamePreview;
        textBoxFolderPrefix.TextChanged += UpdateFileNamePreview;
        textBoxFolderSuffix.TextChanged += UpdateFileNamePreview;
        textBoxFolderBaseName.TextChanged += UpdateFileNamePreview;
        
        // 共通設定
        numericUpDownDateOffset.ValueChanged += UpdateFileNamePreview;
        textBoxSourcePath.TextChanged += UpdateFileNamePreview;
    }

    /// <summary>
    /// ダイアログの初期化
    /// </summary>
    private void InitializeDialogs()
    {
        // ファイル選択ダイアログ
        _openFileDialog = new OpenFileDialog
        {
            Title = "コピー元ファイルを選択",
            Filter = "すべてのファイル (*.*)|*.*",
            Multiselect = true
        };
        
        // フォルダ選択ダイアログ
        _folderBrowserDialog = new FolderBrowserDialog
        {
            Description = "コピー先フォルダを選択"
        };
    }

    #endregion

    #region タスク設定読み込み

    /// <summary>
    /// 既存タスクの設定を読み込み（仕様書4.2.2準拠）
    /// </summary>
    private void LoadTaskSettings()
    {
        try
        {
            // Windowsタスクスケジューラーからタスク情報を取得
            using var taskService = new TaskService();
            var task = taskService.GetTask(_taskName);
            
            if (task == null)
            {
                MessageBox.Show($"タスク '{_taskName}' が見つかりません。", "エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // スケジュール設定を読み込み
            LoadScheduleSettings(task);
            
            // BATファイルの内容を解析して設定を復元
            LoadBatFileSettings(task);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスク設定の読み込みに失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// スケジュール設定を読み込み
    /// </summary>
    private void LoadScheduleSettings(Microsoft.Win32.TaskScheduler.Task task)
    {
        if (task.Definition.Triggers.Count > 0)
        {
            var trigger = task.Definition.Triggers[0];
            dateTimePickerStart.Value = trigger.StartBoundary;
            
            // トリガータイプに基づいてスケジュール種別を設定
            switch (trigger)
            {
                case TimeTrigger:
                    comboBoxScheduleType.SelectedIndex = 0; // 今すぐ実行
                    break;
                case DailyTrigger:
                    comboBoxScheduleType.SelectedIndex = 1; // 毎日
                    break;
                case WeeklyTrigger:
                    comboBoxScheduleType.SelectedIndex = 2; // 毎週
                    break;
                case MonthlyTrigger:
                    comboBoxScheduleType.SelectedIndex = 3; // 毎月
                    break;
                default:
                    comboBoxScheduleType.SelectedIndex = 0;
                    break;
            }
        }
    }

    /// <summary>
    /// BATファイル内容を解析して設定を復元（仕様書4.2.2の複雑なファイル名パターン対応）
    /// </summary>
    private void LoadBatFileSettings(Microsoft.Win32.TaskScheduler.Task task)
    {
        try
        {
            // タスクのアクションからBATファイルパスを取得
            if (task.Definition.Actions.Count > 0 && task.Definition.Actions[0] is ExecAction execAction)
            {
                var batFilePath = ExtractBatFilePathFromCommand(execAction.Arguments);
                if (!string.IsNullOrEmpty(batFilePath) && File.Exists(batFilePath))
                {
                    var batContent = File.ReadAllText(batFilePath, Encoding.UTF8);
                    ParseBatFileContent(batContent);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"BATファイルの解析に失敗しました:\n{ex.Message}", "警告", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// コマンドからBATファイルパスを抽出
    /// </summary>
    private string ExtractBatFilePathFromCommand(string arguments)
    {
        // "/c "BATファイルパス"" の形式から BATファイルパス を抽出
        var match = Regex.Match(arguments, @"/c\s+""([^""]+)""");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    /// <summary>
    /// BATファイルの内容を解析して設定を復元
    /// </summary>
    private void ParseBatFileContent(string batContent)
    {
        var lines = batContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        // 日付オフセットの解析
        ParseDateOffset(lines);
        
        // フォルダ作成設定の解析
        ParseFolderSettings(lines);
        
        // ファイルコピー設定の解析
        ParseFileSettings(lines);
        
        // 初回プレビュー更新
        UpdateFileNamePreview(null, EventArgs.Empty);
    }

    /// <summary>
    /// 日付オフセットを解析
    /// </summary>
    private void ParseDateOffset(string[] lines)
    {
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"AddDays\((-?\d+)\)");
            if (match.Success)
            {
                if (int.TryParse(match.Groups[1].Value, out var offset))
                {
                    numericUpDownDateOffset.Value = offset;
                }
                break;
            }
        }
    }

    /// <summary>
    /// フォルダ作成設定を解析
    /// </summary>
    private void ParseFolderSettings(string[] lines)
    {
        bool hasFolderCreation = lines.Any(line => line.Contains("mkdir") || line.Contains("フォルダ作成"));
        checkBoxCreateFolder.Checked = hasFolderCreation;

        if (hasFolderCreation)
        {
            // フォルダ名構築の呼び出しを解析
            ParseFolderNameBuilding(lines);
            
            // コピー先パスを解析
            ParseDestinationPath(lines);
        }
    }

    /// <summary>
    /// フォルダ名構築設定を解析
    /// </summary>
    private void ParseFolderNameBuilding(string[] lines)
    {
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"call\s+:build_filename\s+""([^""]*)""\s+""([^""]*)""\s+""([^""]*)""\s+""([^""]*)""\s+""([^""]*)""");
            if (match.Success)
            {
                textBoxFolderBaseName.Text = match.Groups[1].Value;
                textBoxFolderPrefix.Text = match.Groups[2].Value;
                textBoxFolderSuffix.Text = match.Groups[3].Value;
                
                var prefixDatePos = match.Groups[4].Value;
                var suffixDatePos = match.Groups[5].Value;
                
                SetDatePosition(prefixDatePos, 
                    radioFolderPrefixDateBefore, radioFolderPrefixDateAfter, radioFolderPrefixDateNone);
                SetDatePosition(suffixDatePos, 
                    radioFolderSuffixDateBefore, radioFolderSuffixDateAfter, radioFolderSuffixDateNone);
                break;
            }
        }
    }

    /// <summary>
    /// ファイル設定を解析
    /// </summary>
    private void ParseFileSettings(string[] lines)
    {
        bool hasFileCopy = lines.Any(line => line.Contains("copy") && !line.Contains("echo"));
        checkBoxCreateFile.Checked = hasFileCopy;

        if (hasFileCopy)
        {
            // ファイル名構築の呼び出しを解析
            ParseFileNameBuilding(lines);
            
            // コピー元ファイルを解析
            ParseSourceFiles(lines);
        }
    }

    /// <summary>
    /// ファイル名構築設定を解析
    /// </summary>
    private void ParseFileNameBuilding(string[] lines)
    {
        // フォルダ作成の後にあるファイル名構築を探す
        bool foundFileNameBuilding = false;
        
        foreach (var line in lines)
        {
            if (line.Contains("ファイルコピー") || (foundFileNameBuilding && line.Contains("call :build_filename")))
            {
                var match = Regex.Match(line, @"call\s+:build_filename\s+""([^""]*)""\s+""([^""]*)""\s+""([^""]*)""\s+""([^""]*)""\s+""([^""]*)""");
                if (match.Success)
                {
                    // ベースファイル名は実際のファイルから取得するので、ここでは接頭語・接尾語のみ設定
                    textBoxPrefix.Text = match.Groups[2].Value;
                    textBoxSuffix.Text = match.Groups[3].Value;
                    
                    var prefixDatePos = match.Groups[4].Value;
                    var suffixDatePos = match.Groups[5].Value;
                    
                    SetDatePosition(prefixDatePos, 
                        radioPrefixDateBefore, radioPrefixDateAfter, radioPrefixDateNone);
                    SetDatePosition(suffixDatePos, 
                        radioSuffixDateBefore, radioSuffixDateAfter, radioSuffixDateNone);
                    break;
                }
            }
            
            if (line.Contains("mkdir"))
            {
                foundFileNameBuilding = true;
            }
        }
    }

    /// <summary>
    /// コピー元ファイルを解析
    /// </summary>
    private void ParseSourceFiles(string[] lines)
    {
        var sourceFiles = new List<string>();
        
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"copy\s+""([^""]+)""\s+%");
            if (match.Success)
            {
                sourceFiles.Add(match.Groups[1].Value);
            }
        }

        if (sourceFiles.Count > 0)
        {
            textBoxSourcePath.Text = string.Join("; ", sourceFiles);
        }
    }

    /// <summary>
    /// コピー先パスを解析
    /// </summary>
    private void ParseDestinationPath(string[] lines)
    {
        foreach (var line in lines)
        {
            var match = Regex.Match(line, @"set\s+FOLDER_PATH=""([^""\\]+)\\");
            if (match.Success)
            {
                textBoxDestinationPath.Text = match.Groups[1].Value;
                break;
            }
        }
    }

    /// <summary>
    /// 日付位置を設定
    /// </summary>
    private void SetDatePosition(string position, RadioButton beforeRadioButton, RadioButton afterRadioButton, RadioButton noneRadioButton)
    {
        beforeRadioButton.Checked = position == "before";
        afterRadioButton.Checked = position == "after";
        noneRadioButton.Checked = position == "none" || string.IsNullOrEmpty(position);
    }

    #endregion

    #region イベントハンドラー

    /// <summary>
    /// ファイル作成チェックボックスの状態変更イベント
    /// </summary>
    private void CheckBoxCreateFile_CheckedChanged(object? sender, EventArgs e)
    {
        UpdateControlsEnabledState();
        UpdateFileNamePreview(sender, e);
    }

    /// <summary>
    /// フォルダ作成チェックボックスの状態変更イベント
    /// </summary>
    private void CheckBoxCreateFolder_CheckedChanged(object? sender, EventArgs e)
    {
        UpdateControlsEnabledState();
        UpdateFileNamePreview(sender, e);
    }

    /// <summary>
    /// コピー元ファイル参照ボタンクリックイベント
    /// </summary>
    private void ButtonBrowseSource_Click(object? sender, EventArgs e)
    {
        if (_openFileDialog?.ShowDialog() == DialogResult.OK)
        {
            textBoxSourcePath.Text = string.Join("; ", _openFileDialog.FileNames);
        }
    }

    /// <summary>
    /// コピー先フォルダ参照ボタンクリックイベント
    /// </summary>
    private void ButtonBrowseDestination_Click(object? sender, EventArgs e)
    {
        if (_folderBrowserDialog?.ShowDialog() == DialogResult.OK)
        {
            textBoxDestinationPath.Text = _folderBrowserDialog.SelectedPath;
        }
    }

    /// <summary>
    /// 保存ボタンクリックイベント
    /// </summary>
    private async void ButtonSaveTask_Click(object? sender, EventArgs e)
    {
        try
        {
            if (ValidateInput())
            {
                await SaveTaskChangesAsync();
                TaskUpdated = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの保存に失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// キャンセルボタンクリックイベント
    /// </summary>
    private void ButtonCancel_Click(object? sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }

    #endregion

    #region ユーティリティメソッド

    /// <summary>
    /// 入力値のバリデーション
    /// </summary>
    private bool ValidateInput()
    {
        // 作成オプションの検証
        if (!checkBoxCreateFolder.Checked && !checkBoxCreateFile.Checked)
        {
            MessageBox.Show("フォルダ作成またはファイルコピーのいずれかを選択してください。", "入力エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // コピー先フォルダの検証
        if (checkBoxCreateFolder.Checked || checkBoxCreateFile.Checked)
        {
            if (string.IsNullOrWhiteSpace(textBoxDestinationPath.Text) || 
                !Directory.Exists(textBoxDestinationPath.Text))
            {
                MessageBox.Show("有効なコピー先フォルダを指定してください。", "入力エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        // コピー元ファイルの検証
        if (checkBoxCreateFile.Checked)
        {
            if (string.IsNullOrWhiteSpace(textBoxSourcePath.Text))
            {
                MessageBox.Show("コピー元ファイルを指定してください。", "入力エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var sourceFiles = textBoxSourcePath.Text.Split(';')
                .Select(f => f.Trim())
                .Where(f => !string.IsNullOrEmpty(f))
                .ToArray();

            foreach (var file in sourceFiles)
            {
                if (!File.Exists(file))
                {
                    MessageBox.Show($"ファイルが存在しません: {file}", "入力エラー", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// コントロールの有効/無効状態を更新
    /// </summary>
    private void UpdateControlsEnabledState()
    {
        bool fileEnabled = checkBoxCreateFile.Checked;
        bool folderEnabled = checkBoxCreateFolder.Checked;
        bool commonEnabled = fileEnabled || folderEnabled;

        // ファイル関連コントロールの制御
        textBoxSourcePath.Enabled = fileEnabled;
        buttonBrowseSource.Enabled = fileEnabled;
        labelSourcePath.Enabled = fileEnabled;
        
        // ファイル名設定コントロール
        textBoxPrefix.Enabled = fileEnabled;
        labelPrefix.Enabled = fileEnabled;
        textBoxSuffix.Enabled = fileEnabled;
        labelSuffix.Enabled = fileEnabled;
        groupBoxPrefixOptions.Enabled = fileEnabled;
        groupBoxSuffixOptions.Enabled = fileEnabled;
        groupBoxFileReplace.Enabled = fileEnabled;
        
        // フォルダ名設定コントロール
        textBoxFolderBaseName.Enabled = folderEnabled;
        labelFolderBaseName.Enabled = folderEnabled;
        textBoxFolderPrefix.Enabled = folderEnabled;
        labelFolderPrefix.Enabled = folderEnabled;
        textBoxFolderSuffix.Enabled = folderEnabled;
        labelFolderSuffix.Enabled = folderEnabled;
        groupBoxFolderPrefixOptions.Enabled = folderEnabled;
        groupBoxFolderSuffixOptions.Enabled = folderEnabled;
        
        // 共通設定
        textBoxDestinationPath.Enabled = commonEnabled;
        buttonBrowseDestination.Enabled = commonEnabled;
        labelDestinationPath.Enabled = commonEnabled;
        numericUpDownDateOffset.Enabled = commonEnabled;
        labelDateOffset.Enabled = commonEnabled;
        textBoxPreview.Enabled = commonEnabled;
        labelPreview.Enabled = commonEnabled;
    }

    /// <summary>
    /// ファイル名・フォルダ名を構築
    /// </summary>
    private string BuildFileNameWithSettings(string baseName, string dateString, string prefix, string suffix, 
        bool prefixDateBefore, bool prefixDateAfter, bool prefixDateNone, 
        bool suffixDateBefore, bool suffixDateAfter, bool suffixDateNone)
    {
        var result = baseName;

        // 接頭語の処理
        if (!string.IsNullOrWhiteSpace(prefix))
        {
            if (prefixDateBefore)
            {
                result = dateString + prefix + result;
            }
            else if (prefixDateAfter)
            {
                result = prefix + dateString + result;
            }
            else
            {
                result = prefix + result;
            }
        }
        else
        {
            if (prefixDateBefore || prefixDateAfter)
            {
                result = dateString + result;
            }
        }

        // 接尾語の処理
        if (!string.IsNullOrWhiteSpace(suffix))
        {
            if (suffixDateBefore)
            {
                result = result + dateString + suffix;
            }
            else if (suffixDateAfter)
            {
                result = result + suffix + dateString;
            }
            else
            {
                result = result + suffix;
            }
        }
        else
        {
            if (suffixDateBefore || suffixDateAfter)
            {
                result = result + dateString;
            }
        }

        return result;
    }

    /// <summary>
    /// リアルタイムプレビューの更新
    /// </summary>
    private void UpdateFileNamePreview(object? sender, EventArgs e)
    {
        try
        {
            var previewText = new StringBuilder();
            DateTime targetDate = DateTime.Now.AddDays((double)numericUpDownDateOffset.Value);
            string dateString = targetDate.ToString("yyyyMMdd");

            // フォルダ名プレビュー
            if (checkBoxCreateFolder.Checked)
            {
                var folderBaseName = string.IsNullOrEmpty(textBoxFolderBaseName.Text) ? "Folder" : textBoxFolderBaseName.Text;
                var folderName = BuildFileNameWithSettings(
                    folderBaseName, 
                    dateString, 
                    textBoxFolderPrefix.Text, 
                    textBoxFolderSuffix.Text,
                    radioFolderPrefixDateBefore.Checked,
                    radioFolderPrefixDateAfter.Checked,
                    radioFolderPrefixDateNone.Checked,
                    radioFolderSuffixDateBefore.Checked,
                    radioFolderSuffixDateAfter.Checked,
                    radioFolderSuffixDateNone.Checked);
                previewText.AppendLine($"フォルダ名: {folderName}");
            }

            // ファイル名プレビュー
            if (checkBoxCreateFile.Checked)
            {
                var sourceFiles = textBoxSourcePath.Text.Split(';')
                    .Select(f => f.Trim())
                    .Where(f => !string.IsNullOrEmpty(f))
                    .ToArray();

                if (sourceFiles.Length == 0)
                {
                    sourceFiles = new[] { "example.txt" };
                }

                foreach (var sourceFile in sourceFiles.Take(3))
                {
                    string fileName = Path.GetFileNameWithoutExtension(sourceFile);
                    string extension = Path.GetExtension(sourceFile);
                    
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "ファイル名";
                        extension = ".txt";
                    }

                    // 文字列置換処理
                    if (!string.IsNullOrEmpty(textBoxReplaceFrom.Text))
                    {
                        fileName = fileName.Replace(textBoxReplaceFrom.Text, textBoxReplaceTo.Text ?? "");
                    }

                    var processedFileName = BuildFileNameWithSettings(
                        fileName, 
                        dateString, 
                        textBoxPrefix.Text, 
                        textBoxSuffix.Text,
                        radioPrefixDateBefore.Checked,
                        radioPrefixDateAfter.Checked,
                        radioPrefixDateNone.Checked,
                        radioSuffixDateBefore.Checked,
                        radioSuffixDateAfter.Checked,
                        radioSuffixDateNone.Checked) + extension;
                    
                    if (checkBoxCreateFolder.Checked)
                    {
                        var folderBaseName = string.IsNullOrEmpty(textBoxFolderBaseName.Text) ? "Folder" : textBoxFolderBaseName.Text;
                        var folderName = BuildFileNameWithSettings(
                            folderBaseName, 
                            dateString, 
                            textBoxFolderPrefix.Text, 
                            textBoxFolderSuffix.Text,
                            radioFolderPrefixDateBefore.Checked,
                            radioFolderPrefixDateAfter.Checked,
                            radioFolderPrefixDateNone.Checked,
                            radioFolderSuffixDateBefore.Checked,
                            radioFolderSuffixDateAfter.Checked,
                            radioFolderSuffixDateNone.Checked);
                        previewText.AppendLine($"ファイル名: {folderName}\\{processedFileName}");
                    }
                    else
                    {
                        previewText.AppendLine($"ファイル名: {processedFileName}");
                    }
                }

                if (sourceFiles.Length > 3)
                {
                    previewText.AppendLine($"... 他 {sourceFiles.Length - 3} ファイル");
                }
            }

            if (previewText.Length == 0)
            {
                previewText.AppendLine("作成対象が選択されていません");
            }

            textBoxPreview.Text = previewText.ToString().TrimEnd();
        }
        catch (Exception ex)
        {
            textBoxPreview.Text = $"プレビューエラー: {ex.Message}";
        }
    }

    /// <summary>
    /// 日付位置の設定を取得
    /// </summary>
    private string GetDatePosition(bool dateBefore, bool dateAfter, bool dateNone)
    {
        if (dateBefore) return "before";
        if (dateAfter) return "after";
        return "none";
    }

    #endregion

    #region タスク保存

    /// <summary>
    /// タスクの変更を保存
    /// </summary>
    private async System.Threading.Tasks.Task SaveTaskChangesAsync()
    {
        // 古いBATファイルをクリーンアップ
        CleanupOldBatFiles(_taskName);
        
        // 新しいBATファイルを生成
        var batFileName = $"{_taskName}_{DateTime.Now:yyyyMMddHHmmss}.bat";
        var batFilePath = Path.Combine(_batFolderPath, batFileName);

        // BATファイルの内容を生成
        var batContent = GenerateBatContent();

        // BATファイルを作成
        var encoding = new UTF8Encoding(true);
        await File.WriteAllTextAsync(batFilePath, batContent, encoding);

        // タスクスケジューラーの設定を更新
        UpdateScheduledTask(_taskName, batFilePath);

        MessageBox.Show($"タスク '{_taskName}' の設定が正常に更新されました。", "成功", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// BATファイルの内容を生成（メインフォームと同じロジック）
    /// </summary>
    private string GenerateBatContent()
    {
        var batContent = new StringBuilder();
        
        // BATファイルヘッダー
        batContent.AppendLine("@echo off");
        batContent.AppendLine("chcp 65001 > nul");
        batContent.AppendLine("setlocal");
        batContent.AppendLine("");
        batContent.AppendLine("echo TaskCrony v1.1.0 自動実行開始: %date% %time%");
        batContent.AppendLine("");

        // 日付オフセットを適用した日付文字列を動的に生成
        var dateOffset = (int)numericUpDownDateOffset.Value;
        batContent.AppendLine("rem 日付オフセットの計算");
        if (dateOffset != 0)
        {
            batContent.AppendLine($"powershell -command \"$date = (Get-Date).AddDays({dateOffset}); $date.ToString('yyyyMMdd')\" > temp_date.txt");
        }
        else
        {
            batContent.AppendLine("powershell -command \"(Get-Date).ToString('yyyyMMdd')\" > temp_date.txt");
        }
        batContent.AppendLine("set /p DATE_STRING=<temp_date.txt");
        batContent.AppendLine("del temp_date.txt");
        batContent.AppendLine("");

        // ファイル名構築関数（メインフォームと同じ）
        GenerateBatFileNameFunction(batContent);

        // メイン処理
        batContent.AppendLine(":main");

        // フォルダ作成処理
        if (checkBoxCreateFolder.Checked)
        {
            GenerateFolderCreationBat(batContent);
        }

        // ファイルコピー処理
        if (checkBoxCreateFile.Checked)
        {
            GenerateFileCopyBat(batContent);
        }

        batContent.AppendLine("echo TaskCrony v1.1.0 自動実行完了: %date% %time%");
        batContent.AppendLine("endlocal");

        return batContent.ToString();
    }

    /// <summary>
    /// BATファイル内のファイル名構築関数を生成
    /// </summary>
    private void GenerateBatFileNameFunction(StringBuilder batContent)
    {
        batContent.AppendLine("rem ファイル名構築関数");
        batContent.AppendLine("goto :main");
        batContent.AppendLine("");
        batContent.AppendLine(":build_filename");
        batContent.AppendLine("set BASE_NAME=%1");
        batContent.AppendLine("set PREFIX=%2");
        batContent.AppendLine("set SUFFIX=%3");
        batContent.AppendLine("set PREFIX_DATE_POS=%4");
        batContent.AppendLine("set SUFFIX_DATE_POS=%5");
        batContent.AppendLine("");
        batContent.AppendLine("set RESULT=%BASE_NAME%");
        batContent.AppendLine("");
        
        // 接頭語処理
        batContent.AppendLine("if not \"%PREFIX%\"==\"\" (");
        batContent.AppendLine("    if \"%PREFIX_DATE_POS%\"==\"before\" (");
        batContent.AppendLine("        set RESULT=%DATE_STRING%%PREFIX%%RESULT%");
        batContent.AppendLine("    ) else if \"%PREFIX_DATE_POS%\"==\"after\" (");
        batContent.AppendLine("        set RESULT=%PREFIX%%DATE_STRING%%RESULT%");
        batContent.AppendLine("    ) else (");
        batContent.AppendLine("        set RESULT=%PREFIX%%RESULT%");
        batContent.AppendLine("    )");
        batContent.AppendLine(") else (");
        batContent.AppendLine("    if \"%PREFIX_DATE_POS%\"==\"before\" set RESULT=%DATE_STRING%%RESULT%");
        batContent.AppendLine("    if \"%PREFIX_DATE_POS%\"==\"after\" set RESULT=%DATE_STRING%%RESULT%");
        batContent.AppendLine(")");
        batContent.AppendLine("");
        
        // 接尾語処理
        batContent.AppendLine("if not \"%SUFFIX%\"==\"\" (");
        batContent.AppendLine("    if \"%SUFFIX_DATE_POS%\"==\"before\" (");
        batContent.AppendLine("        set RESULT=%RESULT%%DATE_STRING%%SUFFIX%");
        batContent.AppendLine("    ) else if \"%SUFFIX_DATE_POS%\"==\"after\" (");
        batContent.AppendLine("        set RESULT=%RESULT%%SUFFIX%%DATE_STRING%");
        batContent.AppendLine("    ) else (");
        batContent.AppendLine("        set RESULT=%RESULT%%SUFFIX%");
        batContent.AppendLine("    )");
        batContent.AppendLine(") else (");
        batContent.AppendLine("    if \"%SUFFIX_DATE_POS%\"==\"before\" set RESULT=%RESULT%%DATE_STRING%");
        batContent.AppendLine("    if \"%SUFFIX_DATE_POS%\"==\"after\" set RESULT=%RESULT%%DATE_STRING%");
        batContent.AppendLine(")");
        batContent.AppendLine("");
        batContent.AppendLine("goto :eof");
        batContent.AppendLine("");
    }

    /// <summary>
    /// フォルダ作成のBATコードを生成
    /// </summary>
    private void GenerateFolderCreationBat(StringBuilder batContent)
    {
        var folderBaseName = string.IsNullOrEmpty(textBoxFolderBaseName.Text) ? "Folder" : textBoxFolderBaseName.Text;
        var folderPrefix = textBoxFolderPrefix.Text;
        var folderSuffix = textBoxFolderSuffix.Text;
        var folderPrefixDatePos = GetDatePosition(
            radioFolderPrefixDateBefore.Checked, 
            radioFolderPrefixDateAfter.Checked, 
            radioFolderPrefixDateNone.Checked);
        var folderSuffixDatePos = GetDatePosition(
            radioFolderSuffixDateBefore.Checked, 
            radioFolderSuffixDateAfter.Checked, 
            radioFolderSuffixDateNone.Checked);
        
        batContent.AppendLine($"call :build_filename \"{folderBaseName}\" \"{folderPrefix}\" \"{folderSuffix}\" \"{folderPrefixDatePos}\" \"{folderSuffixDatePos}\"");
        batContent.AppendLine("set FOLDER_NAME=%RESULT%");
        batContent.AppendLine($"set FOLDER_PATH=\"{textBoxDestinationPath.Text}\\%FOLDER_NAME%\"");
        batContent.AppendLine("");
        batContent.AppendLine("echo フォルダ作成: %FOLDER_PATH%");
        batContent.AppendLine("if not exist %FOLDER_PATH% mkdir %FOLDER_PATH%");
        batContent.AppendLine("");
    }

    /// <summary>
    /// ファイルコピーのBATコードを生成
    /// </summary>
    private void GenerateFileCopyBat(StringBuilder batContent)
    {
        var sourceFiles = textBoxSourcePath.Text.Split(';')
            .Select(f => f.Trim())
            .Where(f => !string.IsNullOrEmpty(f))
            .ToArray();

        var filePrefix = textBoxPrefix.Text;
        var fileSuffix = textBoxSuffix.Text;
        var filePrefixDatePos = GetDatePosition(
            radioPrefixDateBefore.Checked, 
            radioPrefixDateAfter.Checked, 
            radioPrefixDateNone.Checked);
        var fileSuffixDatePos = GetDatePosition(
            radioSuffixDateBefore.Checked, 
            radioSuffixDateAfter.Checked, 
            radioSuffixDateNone.Checked);

        foreach (var sourceFile in sourceFiles)
        {
            var fileExtension = Path.GetExtension(sourceFile);
            var baseFileName = Path.GetFileNameWithoutExtension(sourceFile);
            
            // 文字列置換処理
            if (!string.IsNullOrEmpty(textBoxReplaceFrom.Text))
            {
                baseFileName = baseFileName.Replace(textBoxReplaceFrom.Text, textBoxReplaceTo.Text ?? "");
            }

            batContent.AppendLine($"call :build_filename \"{baseFileName}\" \"{filePrefix}\" \"{fileSuffix}\" \"{filePrefixDatePos}\" \"{fileSuffixDatePos}\"");
            batContent.AppendLine($"set FILE_NAME=%RESULT%{fileExtension}");
            
            if (checkBoxCreateFolder.Checked)
            {
                batContent.AppendLine("set FILE_PATH=%FOLDER_PATH%\\%FILE_NAME%");
                batContent.AppendLine("echo ファイルコピー（フォルダ内）: %FILE_PATH%");
            }
            else
            {
                batContent.AppendLine($"set FILE_PATH=\"{textBoxDestinationPath.Text}\\%FILE_NAME%\"");
                batContent.AppendLine("echo ファイルコピー: %FILE_PATH%");
            }
            
            batContent.AppendLine($"copy \"{sourceFile}\" %FILE_PATH%");
            batContent.AppendLine("");
        }
    }

    /// <summary>
    /// スケジュールされたタスクを更新
    /// </summary>
    private void UpdateScheduledTask(string taskName, string batFilePath)
    {
        using var taskService = new TaskService();

        // 既存のタスクを削除
        var existingTask = taskService.GetTask(taskName);
        if (existingTask != null)
        {
            taskService.RootFolder.DeleteTask(taskName);
        }

        // 新しいタスクを作成
        var taskDefinition = taskService.NewTask();
        taskDefinition.RegistrationInfo.Description = $"TaskCrony v1.1.0 で作成されたタスク: {taskName}";
        taskDefinition.Principal.LogonType = TaskLogonType.InteractiveToken;

        // トリガーの設定
        CreateTaskTrigger(taskDefinition);

        // アクションの設定
        var execAction = new ExecAction("cmd.exe", $"/c \"{batFilePath}\"", null);
        taskDefinition.Actions.Add(execAction);

        // タスクを登録
        taskService.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
    }

    /// <summary>
    /// タスクトリガーを作成
    /// </summary>
    private void CreateTaskTrigger(TaskDefinition taskDefinition)
    {
        var selectedScheduleType = comboBoxScheduleType.SelectedIndex;
        switch (selectedScheduleType)
        {
            case 0: // 今すぐ実行
                var timeTrigger = new TimeTrigger(dateTimePickerStart.Value);
                taskDefinition.Triggers.Add(timeTrigger);
                break;
            case 1: // 毎日
                var dailyTrigger = new DailyTrigger
                {
                    StartBoundary = dateTimePickerStart.Value,
                    DaysInterval = 1
                };
                taskDefinition.Triggers.Add(dailyTrigger);
                break;
            case 2: // 毎週
                var weeklyTrigger = new WeeklyTrigger
                {
                    StartBoundary = dateTimePickerStart.Value,
                    WeeksInterval = 1,
                    DaysOfWeek = DaysOfTheWeek.AllDays
                };
                taskDefinition.Triggers.Add(weeklyTrigger);
                break;
            case 3: // 毎月
                var monthlyTrigger = new MonthlyTrigger
                {
                    StartBoundary = dateTimePickerStart.Value,
                    MonthsOfYear = MonthsOfTheYear.AllMonths,
                    DaysOfMonth = new int[] { dateTimePickerStart.Value.Day }
                };
                taskDefinition.Triggers.Add(monthlyTrigger);
                break;
        }
    }

    /// <summary>
    /// 同じタスク名の古いBATファイルを削除
    /// </summary>
    private void CleanupOldBatFiles(string taskName)
    {
        try
        {
            var batFiles = Directory.GetFiles(_batFolderPath, "*.bat");
            foreach (var batFile in batFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(batFile);
                if (fileName.StartsWith(taskName + "_") && fileName != taskName)
                {
                    File.Delete(batFile);
                }
            }
        }
        catch (Exception ex)
        {
            // エラーは無視（ファイル削除は必須ではない）
            System.Diagnostics.Debug.WriteLine($"BATファイルクリーンアップエラー: {ex.Message}");
        }
    }

    #endregion

    #region リソース管理

    /// <summary>
    /// リソースの解放
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
            _folderBrowserDialog?.Dispose();
            _openFileDialog?.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion
}
