using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.Text;

namespace TaskCrony;

/// <summary>
/// TaskCrony メインフォーム v1.1.0
/// </summary>
public partial class MainForm : Form
{
    #region フィールド

    private readonly string _batFolderPath;
    private FolderBrowserDialog? _folderBrowserDialog;
    private OpenFileDialog? _openFileDialog;

    #endregion

    #region コンストラクタ

    /// <summary>
    /// メインフォームのコンストラクタ
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
        
        // BATフォルダのパスを設定（実行ファイルと同じディレクトリにBATフォルダを作成）
        _batFolderPath = Path.Combine(Application.StartupPath, "BAT");
        
        // BATフォルダが存在しない場合は自動作成
        if (!Directory.Exists(_batFolderPath))
        {
            Directory.CreateDirectory(_batFolderPath);
        }

        // モダンテーマを適用
        ModernTheme.ApplyToForm(this);
        
        InitializeControls();
        LoadExistingTasks();
    }

    #endregion

    #region 初期化メソッド

    /// <summary>
    /// コントロールの初期化
    /// </summary>
    private void InitializeControls()
    {
        // デフォルト値の設定
        comboBoxScheduleType.SelectedIndex = 0;
        dateTimePickerStart.Value = DateTime.Now.AddMinutes(5);
        
        // イベントハンドラーの設定
        SetupEventHandlers();
        
        // ダイアログの初期化
        InitializeDialogs();
        
        // 初期プレビュー更新
        UpdateFileNamePreview(null, EventArgs.Empty);
        
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
        buttonCreateTask.Click += ButtonCreateTask_Click;
        buttonClearInputs.Click += ButtonClearInputs_Click; // 入力クリアボタン追加
        buttonRefreshTasks.Click += ButtonRefreshTasks_Click;
        buttonDeleteTask.Click += ButtonDeleteTask_Click;
        buttonEditTask.Click += ButtonEditTask_Click;
        buttonRunTask.Click += ButtonRunTask_Click;
        buttonOpenTaskScheduler.Click += ButtonOpenTaskScheduler_Click;
        
        // チェックボックスイベント
        checkBoxCreateFile.CheckedChanged += CheckBoxCreateFile_CheckedChanged;
        checkBoxCreateFolder.CheckedChanged += CheckBoxCreateFolder_CheckedChanged;
        
        // ファイル名プレビュー関連イベント
        SetupPreviewEvents();
    }

    /// <summary>
    /// プレビュー関連イベントハンドラーの設定
    /// </summary>
    private void SetupPreviewEvents()
    {
        // ファイル設定（ラジオボタンに変更）
        radioPrefixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioPrefixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioPrefixDateNone.CheckedChanged += UpdateFileNamePreview;
        radioSuffixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioSuffixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioSuffixDateNone.CheckedChanged += UpdateFileNamePreview;
        
        // フォルダ設定（ラジオボタンに変更）
        radioFolderPrefixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioFolderPrefixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioFolderPrefixDateNone.CheckedChanged += UpdateFileNamePreview;
        radioFolderSuffixDateBefore.CheckedChanged += UpdateFileNamePreview;
        radioFolderSuffixDateAfter.CheckedChanged += UpdateFileNamePreview;
        radioFolderSuffixDateNone.CheckedChanged += UpdateFileNamePreview;
        textBoxPrefix.TextChanged += UpdateFileNamePreview;
        textBoxSuffix.TextChanged += UpdateFileNamePreview;
        textBoxReplaceFrom.TextChanged += UpdateFileNamePreview;
        textBoxReplaceTo.TextChanged += UpdateFileNamePreview;
        
        // フォルダ設定
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
            Multiselect = true // 複数ファイル選択可能（仕様書4.1.2）
        };
        
        // フォルダ選択ダイアログ
        _folderBrowserDialog = new FolderBrowserDialog
        {
            Description = "コピー先フォルダを選択"
        };
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
            // 複数ファイル選択時の処理（仕様書4.1.2）
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
    /// 入力内容クリアボタンクリックイベント
    /// </summary>
    private void ButtonClearInputs_Click(object? sender, EventArgs e)
    {
        // 基本設定クリア
        textBoxTaskName.Clear();
        checkBoxCreateFile.Checked = false;
        checkBoxCreateFolder.Checked = false;

        // ファイルパス設定クリア
        textBoxSourcePath.Clear();
        textBoxDestinationPath.Clear();

        // ファイル名設定クリア
        textBoxPrefix.Clear();
        textBoxSuffix.Clear();
        radioPrefixDateNone.Checked = true;
        radioSuffixDateNone.Checked = true;

        // フォルダ名設定クリア
        textBoxFolderBaseName.Text = "Folder";
        textBoxFolderPrefix.Clear();
        textBoxFolderSuffix.Clear();
        radioFolderPrefixDateNone.Checked = true;
        radioFolderSuffixDateNone.Checked = true;

        // 文字列置換クリア
        textBoxReplaceFrom.Clear();
        textBoxReplaceTo.Clear();

        // 共通設定リセット
        numericUpDownDateOffset.Value = 0;

        // スケジュール設定リセット
        comboBoxScheduleType.SelectedIndex = 0;
        dateTimePickerStart.Value = DateTime.Now.AddMinutes(5);

        // プレビュー更新
        UpdateFileNamePreview(sender, e);
        
        MessageBox.Show("入力内容をクリアしました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// タスク作成ボタンクリックイベント
    /// </summary>
    private async void ButtonCreateTask_Click(object? sender, EventArgs e)
    {
        try
        {
            await CreateTaskAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの作成に失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// タスク更新ボタンクリックイベント
    /// </summary>
    private void ButtonRefreshTasks_Click(object? sender, EventArgs e)
    {
        LoadExistingTasks();
    }

    /// <summary>
    /// タスク削除ボタンクリックイベント
    /// </summary>
    private void ButtonDeleteTask_Click(object? sender, EventArgs e)
    {
        if (listViewTasks.SelectedItems.Count == 0)
        {
            MessageBox.Show("削除するタスクを選択してください。", "警告", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedItem = listViewTasks.SelectedItems[0];
        var taskName = selectedItem.Text;

        var result = MessageBox.Show($"タスク '{taskName}' を削除しますか？", "確認", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            DeleteTask(taskName);
        }
    }

    /// <summary>
    /// タスク編集ボタンクリックイベント
    /// </summary>
    private async void ButtonEditTask_Click(object? sender, EventArgs e)
    {
        if (listViewTasks.SelectedItems.Count == 0)
        {
            MessageBox.Show("編集するタスクを選択してください。", "警告", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedItem = listViewTasks.SelectedItems[0];
        var taskName = selectedItem.Text;
        
        try
        {
            // 設定ファイルから読み込み試行
            bool settingsLoaded = await LoadTaskSettings(taskName);
            
            if (!settingsLoaded)
            {
                MessageBox.Show("タスクの設定ファイルが見つかりません。BATファイルから設定を復元します。", 
                    "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            // 編集フォームを表示
            using var editForm = new TaskEditForm(taskName, _batFolderPath);
            var result = editForm.ShowDialog(this);
            
            if (result == DialogResult.OK && editForm.TaskUpdated)
            {
                // タスク一覧を更新
                LoadExistingTasks();
                MessageBox.Show("タスクが正常に更新されました。", "成功", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの編集中にエラーが発生しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// タスク即座実行ボタンクリックイベント（仕様書4.2.4）
    /// </summary>
    private async void ButtonRunTask_Click(object? sender, EventArgs e)
    {
        if (listViewTasks.SelectedItems.Count == 0)
        {
            MessageBox.Show("実行するタスクを選択してください。", "警告", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedItem = listViewTasks.SelectedItems[0];
        var taskName = selectedItem.Text;

        try
        {
            await RunTaskImmediatelyAsync(taskName);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの実行に失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// タスクスケジューラ起動ボタンクリックイベント
    /// </summary>
    private void ButtonOpenTaskScheduler_Click(object? sender, EventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "taskschd.msc",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクスケジューラの起動に失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

    #region バリデーション・ユーティリティメソッド

    /// <summary>
    /// 入力値のバリデーション
    /// </summary>
    private bool ValidateInput()
    {
        // タスク名の検証
        if (string.IsNullOrWhiteSpace(textBoxTaskName.Text))
        {
            MessageBox.Show("タスク名を入力してください。", "入力エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

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

            // 複数ファイルの場合の検証
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
    /// 日付文字列を生成
    /// </summary>
    private string GenerateDateString()
    {
        var targetDate = DateTime.Now.AddDays((double)numericUpDownDateOffset.Value);
        return targetDate.ToString("yyyyMMdd");
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

    #region ファイル名・フォルダ名構築

    /// <summary>
    /// ファイル名・フォルダ名を構築（仕様書4.1.3準拠）
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
                result = dateString + prefix + "_" + result;
            }
            else if (prefixDateAfter)
            {
                result = prefix + dateString + "_" + result;
            }
            else
            {
                result = prefix + "_" + result;
            }
        }
        else
        {
            // 接頭語が空でも日付位置が指定されている場合は日付のみを追加
            if (prefixDateBefore || prefixDateAfter)
            {
                result = dateString + "_" + result;
            }
        }

        // 接尾語の処理
        if (!string.IsNullOrWhiteSpace(suffix))
        {
            if (suffixDateBefore)
            {
                result = result + "_" + dateString + suffix;
            }
            else if (suffixDateAfter)
            {
                result = result + "_" + suffix + dateString;
            }
            else
            {
                result = result + "_" + suffix;
            }
        }
        else
        {
            // 接尾語が空でも日付位置が指定されている場合は日付のみを追加
            if (suffixDateBefore || suffixDateAfter)
            {
                result = result + "_" + dateString;
            }
        }

        return result;
    }

    /// <summary>
    /// リアルタイムプレビューの更新（仕様書4.1.4）
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

                foreach (var sourceFile in sourceFiles.Take(3)) // 最大3つまでプレビュー表示
                {
                    string fileName = Path.GetFileNameWithoutExtension(sourceFile);
                    string extension = Path.GetExtension(sourceFile);
                    
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "ファイル名";
                        extension = ".txt";
                    }

                    // 文字列置換処理（仕様書4.1.3）
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

    #endregion

    #region タスク作成・管理

    /// <summary>
    /// タスクを非同期で作成
    /// </summary>
    private async System.Threading.Tasks.Task CreateTaskAsync()
    {
        if (!ValidateInput())
            return;

        var taskName = textBoxTaskName.Text.Trim();
        
        // 古いBATファイルをクリーンアップ
        CleanupOldBatFiles(taskName);
        
        // BATファイル名の生成（仕様書4.3.3）
        var batFileName = $"{taskName}_{DateTime.Now:yyyyMMddHHmmss}.bat";
        var batFilePath = Path.Combine(_batFolderPath, batFileName);

        // BATファイルの内容を生成
        var batContent = GenerateBatContent();

        // BATファイルを作成 (UTF-8 with BOM for Windows compatibility)
        var encoding = new UTF8Encoding(true);
        await File.WriteAllTextAsync(batFilePath, batContent, encoding);

        // 設定ファイルを保存（編集機能用）
        await SaveTaskSettings(taskName);

        // タスクスケジューラーに登録
        CreateScheduledTask(taskName, batFilePath);

        MessageBox.Show($"タスク '{taskName}' が正常に作成されました。\nBATファイル: {batFilePath}", 
            "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // タスク一覧を更新
        LoadExistingTasks();
    }

    /// <summary>
    /// BATファイルの内容を生成（仕様書4.3.1準拠）
    /// </summary>
    private string GenerateBatContent()
    {
        var batContent = new StringBuilder();
        
        // BATファイルヘッダー（仕様書6.3）
        batContent.AppendLine("@echo off");
        batContent.AppendLine("chcp 65001 > nul"); // UTF-8コードページに設定
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

        // 設定値をBATファイルに記録（編集機能用）
        batContent.AppendLine("rem === TaskCrony 設定情報 ===");
        batContent.AppendLine($"rem DESTINATION={textBoxDestinationPath.Text}");
        batContent.AppendLine($"rem REPLACE_FROM={textBoxReplaceFrom.Text}");
        batContent.AppendLine($"rem REPLACE_TO={textBoxReplaceTo.Text}");
        batContent.AppendLine("rem === 設定情報終了 ===");
        batContent.AppendLine("");

        // ファイル名構築関数
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
    /// ファイルコピーのBATコードを生成（複数ファイル対応）
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
    /// スケジュールされたタスクを作成（仕様書4.1.5）
    /// </summary>
    private void CreateScheduledTask(string taskName, string batFilePath)
    {
        using var taskService = new TaskService();

        // 既存のタスクがある場合は削除
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
    /// タスクを即座に実行（仕様書4.2.4）
    /// </summary>
    private async System.Threading.Tasks.Task RunTaskImmediatelyAsync(string taskName)
    {
        using var taskService = new TaskService();
        var task = taskService.GetTask(taskName);
        
        if (task == null)
        {
            throw new InvalidOperationException($"タスク '{taskName}' が見つかりません。");
        }

        MessageBox.Show($"タスク '{taskName}' を実行します。", "実行確認", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        // タスクを実行
        task.Run();

        // 実行完了まで待機
        await System.Threading.Tasks.Task.Run(() =>
        {
            System.Threading.Thread.Sleep(1000); // 1秒待機
            
            // タスクの状態を確認（Refreshの代わりにTaskServiceから再取得）
            var refreshedTask = taskService.GetTask(taskName);
            while (refreshedTask != null && refreshedTask.State == TaskState.Running)
            {
                System.Threading.Thread.Sleep(500);
                refreshedTask = taskService.GetTask(taskName);
            }
        });

        // 実行結果を表示
        var lastResult = task.LastTaskResult;
        if (lastResult == 0)
        {
            MessageBox.Show($"タスク '{taskName}' が正常に実行されました。", "実行完了", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show($"タスク '{taskName}' の実行中にエラーが発生しました。\nエラーコード: {lastResult}", 
                "実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // タスク一覧を更新
        LoadExistingTasks();
    }

    /// <summary>
    /// 既存のタスクを読み込み（仕様書4.2.1）
    /// </summary>
    private void LoadExistingTasks()
    {
        listViewTasks.Items.Clear();

        try
        {
            using var taskService = new TaskService();
            var tasks = taskService.RootFolder.GetTasks();

            foreach (var task in tasks)
            {
                // TaskCrony で作成されたタスクかチェック
                if (task.Definition.RegistrationInfo.Description?.Contains("TaskCrony") == true)
                {
                    var item = new ListViewItem(task.Name);
                    item.SubItems.Add(task.Enabled ? "有効" : "無効");
                    item.SubItems.Add(task.NextRunTime == DateTime.MinValue ? "なし" : task.NextRunTime.ToString("yyyy/MM/dd HH:mm"));
                    item.SubItems.Add(task.LastRunTime == DateTime.MinValue ? "なし" : task.LastRunTime.ToString("yyyy/MM/dd HH:mm"));
                    
                    listViewTasks.Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの読み込みに失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// タスクを削除（仕様書4.2.3）
    /// </summary>
    private void DeleteTask(string taskName)
    {
        try
        {
            using var taskService = new TaskService();
            taskService.RootFolder.DeleteTask(taskName);
            
            // 関連するBATファイルを削除
            CleanupOldBatFiles(taskName);
            
            // 関連するJSONファイルを削除
            CleanupTaskJsonFiles(taskName);
            
            MessageBox.Show($"タスク '{taskName}' を削除しました。", "成功", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            LoadExistingTasks();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"タスクの削除に失敗しました:\n{ex.Message}", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // ファイル名が "タスク名_日時" の形式かチェック
                if (fileName.StartsWith(taskName + "_") && fileName != taskName)
                {
                    File.Delete(batFile);
                    Debug.WriteLine($"古いBATファイルを削除: {batFile}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"BATファイルクリーンアップエラー: {ex.Message}");
            // エラーは無視（ファイル削除は必須ではない）
        }
    }

    /// <summary>
    /// タスクに関連するJSONファイルを削除
    /// </summary>
    private void CleanupTaskJsonFiles(string taskName)
    {
        try
        {
            var jsonFilePath = Path.Combine(_batFolderPath, $"{taskName}_settings.json");
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
                Debug.WriteLine($"JSONファイルを削除: {jsonFilePath}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"JSONファイルクリーンアップエラー: {ex.Message}");
            // エラーは無視（ファイル削除は必須ではない）
        }
    }

    #endregion

    #region 設定保存・読み込み機能

    /// <summary>
    /// タスク設定をJSONファイルに保存
    /// </summary>
    private async System.Threading.Tasks.Task SaveTaskSettings(string taskName)
    {
        try
        {
            var settings = new
            {
                TaskName = taskName,
                CreateFile = checkBoxCreateFile.Checked,
                CreateFolder = checkBoxCreateFolder.Checked,
                SourcePath = textBoxSourcePath.Text,
                DestinationPath = textBoxDestinationPath.Text,
                Prefix = textBoxPrefix.Text,
                Suffix = textBoxSuffix.Text,
                PrefixDatePosition = GetRadioButtonValue(radioPrefixDateBefore, radioPrefixDateAfter, radioPrefixDateNone),
                SuffixDatePosition = GetRadioButtonValue(radioSuffixDateBefore, radioSuffixDateAfter, radioSuffixDateNone),
                FolderBaseName = textBoxFolderBaseName.Text,
                FolderPrefix = textBoxFolderPrefix.Text,
                FolderSuffix = textBoxFolderSuffix.Text,
                FolderPrefixDatePosition = GetRadioButtonValue(radioFolderPrefixDateBefore, radioFolderPrefixDateAfter, radioFolderPrefixDateNone),
                FolderSuffixDatePosition = GetRadioButtonValue(radioFolderSuffixDateBefore, radioFolderSuffixDateAfter, radioFolderSuffixDateNone),
                ReplaceFrom = textBoxReplaceFrom.Text,
                ReplaceTo = textBoxReplaceTo.Text,
                DateOffset = (int)numericUpDownDateOffset.Value,
                ScheduleType = comboBoxScheduleType.SelectedIndex,
                StartDateTime = dateTimePickerStart.Value,
                CreatedAt = DateTime.Now
            };

            var settingsJson = System.Text.Json.JsonSerializer.Serialize(settings, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var settingsPath = Path.Combine(_batFolderPath, $"{taskName}_settings.json");
            await File.WriteAllTextAsync(settingsPath, settingsJson, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"設定保存エラー: {ex.Message}");
        }
    }

    /// <summary>
    /// タスク設定をJSONファイルから読み込み
    /// </summary>
    private async System.Threading.Tasks.Task<bool> LoadTaskSettings(string taskName)
    {
        try
        {
            var settingsPath = Path.Combine(_batFolderPath, $"{taskName}_settings.json");
            if (!File.Exists(settingsPath))
            {
                return false;
            }

            var settingsJson = await File.ReadAllTextAsync(settingsPath, Encoding.UTF8);
            var settings = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(settingsJson);

            if (settings != null)
            {
                // 基本設定
                if (settings.TryGetValue("TaskName", out var taskNameValue))
                    textBoxTaskName.Text = taskNameValue.ToString() ?? "";
                
                if (settings.TryGetValue("CreateFile", out var createFileValue) && bool.TryParse(createFileValue.ToString(), out var createFile))
                    checkBoxCreateFile.Checked = createFile;

                if (settings.TryGetValue("CreateFolder", out var createFolderValue) && bool.TryParse(createFolderValue.ToString(), out var createFolder))
                    checkBoxCreateFolder.Checked = createFolder;

                // パス設定
                if (settings.TryGetValue("SourcePath", out var sourcePathValue))
                    textBoxSourcePath.Text = sourcePathValue.ToString() ?? "";

                if (settings.TryGetValue("DestinationPath", out var destPathValue))
                    textBoxDestinationPath.Text = destPathValue.ToString() ?? "";

                // ファイル名設定
                if (settings.TryGetValue("Prefix", out var prefixValue))
                    textBoxPrefix.Text = prefixValue.ToString() ?? "";

                if (settings.TryGetValue("Suffix", out var suffixValue))
                    textBoxSuffix.Text = suffixValue.ToString() ?? "";

                // ラジオボタン設定
                if (settings.TryGetValue("PrefixDatePosition", out var prefixDatePos))
                    SetRadioButtonValue(prefixDatePos.ToString() ?? "", radioPrefixDateBefore, radioPrefixDateAfter, radioPrefixDateNone);

                if (settings.TryGetValue("SuffixDatePosition", out var suffixDatePos))
                    SetRadioButtonValue(suffixDatePos.ToString() ?? "", radioSuffixDateBefore, radioSuffixDateAfter, radioSuffixDateNone);

                // フォルダ設定
                if (settings.TryGetValue("FolderBaseName", out var folderBaseValue))
                    textBoxFolderBaseName.Text = folderBaseValue.ToString() ?? "Folder";

                if (settings.TryGetValue("FolderPrefix", out var folderPrefixValue))
                    textBoxFolderPrefix.Text = folderPrefixValue.ToString() ?? "";

                if (settings.TryGetValue("FolderSuffix", out var folderSuffixValue))
                    textBoxFolderSuffix.Text = folderSuffixValue.ToString() ?? "";

                if (settings.TryGetValue("FolderPrefixDatePosition", out var folderPrefixDatePos))
                    SetRadioButtonValue(folderPrefixDatePos.ToString() ?? "", radioFolderPrefixDateBefore, radioFolderPrefixDateAfter, radioFolderPrefixDateNone);

                if (settings.TryGetValue("FolderSuffixDatePosition", out var folderSuffixDatePos))
                    SetRadioButtonValue(folderSuffixDatePos.ToString() ?? "", radioFolderSuffixDateBefore, radioFolderSuffixDateAfter, radioFolderSuffixDateNone);

                // 文字列置換
                if (settings.TryGetValue("ReplaceFrom", out var replaceFromValue))
                    textBoxReplaceFrom.Text = replaceFromValue.ToString() ?? "";

                if (settings.TryGetValue("ReplaceTo", out var replaceToValue))
                    textBoxReplaceTo.Text = replaceToValue.ToString() ?? "";

                // 共通設定
                if (settings.TryGetValue("DateOffset", out var dateOffsetValue) && int.TryParse(dateOffsetValue.ToString(), out var dateOffset))
                    numericUpDownDateOffset.Value = Math.Max(-365, Math.Min(365, dateOffset));

                // スケジュール設定
                if (settings.TryGetValue("ScheduleType", out var scheduleTypeValue) && int.TryParse(scheduleTypeValue.ToString(), out var scheduleType))
                    comboBoxScheduleType.SelectedIndex = Math.Max(0, Math.Min(comboBoxScheduleType.Items.Count - 1, scheduleType));

                if (settings.TryGetValue("StartDateTime", out var startDateTimeValue) && DateTime.TryParse(startDateTimeValue.ToString(), out var startDateTime))
                    dateTimePickerStart.Value = startDateTime;

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"設定読み込みエラー: {ex.Message}");
        }

        return false;
    }

    /// <summary>
    /// ラジオボタンの値を取得
    /// </summary>
    private string GetRadioButtonValue(RadioButton before, RadioButton after, RadioButton none)
    {
        if (before.Checked) return "Before";
        if (after.Checked) return "After";
        return "None";
    }

    /// <summary>
    /// ラジオボタンの値を設定
    /// </summary>
    private void SetRadioButtonValue(string value, RadioButton before, RadioButton after, RadioButton none)
    {
        before.Checked = value == "Before";
        after.Checked = value == "After";
        none.Checked = value == "None" || (!before.Checked && !after.Checked);
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
