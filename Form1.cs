using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.Text;

namespace TaskCrony;

/// <summary>
/// TaskCrony メインフォーム
/// </summary>
public partial class MainForm : Form
{
    private readonly string _batFolderPath;
    private FolderBrowserDialog? _folderBrowserDialog;
    private OpenFileDialog? _openFileDialog;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
        
        // BATフォルダのパスを設定（実行ファイルと同じディレクトリにBATフォルダを作成）
        _batFolderPath = Path.Combine(Application.StartupPath, "BAT");
        
        // BATフォルダが存在しない場合は作成
        if (!Directory.Exists(_batFolderPath))
        {
            Directory.CreateDirectory(_batFolderPath);
        }

        // モダンテーマを適用
        ModernTheme.ApplyToForm(this);
        
        InitializeControls();
        LoadExistingTasks();
    }

    /// <summary>
    /// コントロールの初期化
    /// </summary>
    private void InitializeControls()
    {
        // デフォルト値の設定
        comboBoxScheduleType.SelectedIndex = 0;
        dateTimePickerStart.Value = DateTime.Now.AddMinutes(5);
        
        // イベントハンドラーの設定
        buttonBrowseSource.Click += ButtonBrowseSource_Click;
        buttonBrowseDestination.Click += ButtonBrowseDestination_Click;
        buttonCreateTask.Click += ButtonCreateTask_Click;
        buttonRefreshTasks.Click += ButtonRefreshTasks_Click;
        buttonDeleteTask.Click += ButtonDeleteTask_Click;
        buttonEditTask.Click += ButtonEditTask_Click;
        buttonOpenTaskScheduler.Click += ButtonOpenTaskScheduler_Click;
        
        checkBoxCreateFile.CheckedChanged += CheckBoxCreateFile_CheckedChanged;
        checkBoxCreateFolder.CheckedChanged += CheckBoxCreateFolder_CheckedChanged;
        
        // 新しいファイル名プレビュー関連のイベントハンドラー
        checkBoxPrefixDateBefore.CheckedChanged += UpdateFileNamePreview;
        checkBoxPrefixDateAfter.CheckedChanged += UpdateFileNamePreview;
        checkBoxPrefixDateNone.CheckedChanged += UpdateFileNamePreview;
        checkBoxSuffixDateBefore.CheckedChanged += UpdateFileNamePreview;
        checkBoxSuffixDateAfter.CheckedChanged += UpdateFileNamePreview;
        checkBoxSuffixDateNone.CheckedChanged += UpdateFileNamePreview;
        textBoxPrefix.TextChanged += UpdateFileNamePreview;
        textBoxSuffix.TextChanged += UpdateFileNamePreview;
        numericUpDownDateOffset.ValueChanged += UpdateFileNamePreview;
        textBoxSourcePath.TextChanged += UpdateFileNamePreview;
        textBoxFolderBaseName.TextChanged += UpdateFileNamePreview;
        
        // フォルダ設定用のイベントハンドラー
        checkBoxFolderPrefixDateBefore.CheckedChanged += UpdateFileNamePreview;
        checkBoxFolderPrefixDateAfter.CheckedChanged += UpdateFileNamePreview;
        checkBoxFolderPrefixDateNone.CheckedChanged += UpdateFileNamePreview;
        checkBoxFolderSuffixDateBefore.CheckedChanged += UpdateFileNamePreview;
        checkBoxFolderSuffixDateAfter.CheckedChanged += UpdateFileNamePreview;
        checkBoxFolderSuffixDateNone.CheckedChanged += UpdateFileNamePreview;
        textBoxFolderPrefix.TextChanged += UpdateFileNamePreview;
        textBoxFolderSuffix.TextChanged += UpdateFileNamePreview;
        
        // ファイル名置換用のイベントハンドラー
        textBoxReplaceFrom.TextChanged += UpdateFileNamePreview;
        textBoxReplaceTo.TextChanged += UpdateFileNamePreview;
        
        // 初期値設定
        textBoxFolderBaseName.Text = "Folder";
        
        // ファイルダイアログの初期化
        _openFileDialog = new OpenFileDialog
        {
            Title = "コピー元ファイルを選択",
            Filter = "すべてのファイル (*.*)|*.*"
        };
        
        _folderBrowserDialog = new FolderBrowserDialog
        {
            Description = "コピー先フォルダを選択"
        };
        
        // 初回プレビュー更新
        UpdateFileNamePreview(null, EventArgs.Empty);
        
        // 初期状態でのコントロール有効/無効状態を設定
        UpdateControlsEnabledState();
    }

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
    /// コントロールの有効/無効状態を更新
    /// </summary>
    private void UpdateControlsEnabledState()
    {
        bool fileEnabled = checkBoxCreateFile.Checked;
        bool folderEnabled = checkBoxCreateFolder.Checked;

        // ファイル関連のコントロール（groupBoxFiles全体ではなく個別制御）
        // groupBoxFiles.Enabled = fileEnabled; // これを削除して個別制御
        
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
        
        // 共通設定（日付オフセット）は、どちらかが有効なら有効
        bool commonEnabled = fileEnabled || folderEnabled;
        numericUpDownDateOffset.Enabled = commonEnabled;
        labelDateOffset.Enabled = commonEnabled;
        textBoxPreview.Enabled = commonEnabled;
        labelPreview.Enabled = commonEnabled;
        
        // コピー元・コピー先フォルダの参照ボタン（個別制御）
        buttonBrowseSource.Enabled = fileEnabled; // ファイルコピー時のみ有効
        buttonBrowseDestination.Enabled = commonEnabled; // フォルダ作成またはファイルコピー時に有効
        
        // コピー元・コピー先のテキストボックス（個別制御）
        textBoxSourcePath.Enabled = fileEnabled; // ファイルコピー時のみ有効
        textBoxDestinationPath.Enabled = commonEnabled; // フォルダ作成またはファイルコピー時に有効
        
        // ラベルの制御
        labelSourcePath.Enabled = fileEnabled;
        labelDestinationPath.Enabled = commonEnabled;
        
        // groupBoxFiles自体の有効/無効は設定しない（常に有効のまま）
    }

    /// <summary>
    /// コピー元ファイル参照ボタンクリックイベント
    /// </summary>
    private void ButtonBrowseSource_Click(object? sender, EventArgs e)
    {
        if (_openFileDialog?.ShowDialog() == DialogResult.OK)
        {
            textBoxSourcePath.Text = _openFileDialog.FileName;
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
    private void ButtonEditTask_Click(object? sender, EventArgs e)
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
    /// バリデーション実行
    /// </summary>
    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(textBoxTaskName.Text))
        {
            MessageBox.Show("タスク名を入力してください。", "入力エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!checkBoxCreateFolder.Checked && !checkBoxCreateFile.Checked)
        {
            MessageBox.Show("フォルダ作成またはファイルコピーのいずれかを選択してください。", "入力エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // フォルダ作成またはファイルコピーが選択されている場合、コピー先フォルダは必須
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

        if (checkBoxCreateFile.Checked)
        {
            if (string.IsNullOrWhiteSpace(textBoxSourcePath.Text) || 
                !File.Exists(textBoxSourcePath.Text))
            {
                MessageBox.Show("有効なコピー元ファイルを指定してください。", "入力エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        return true;
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
    /// タスクを非同期で作成
    /// </summary>
    private async System.Threading.Tasks.Task CreateTaskAsync()
    {
        if (!ValidateInput())
            return;

        var taskName = textBoxTaskName.Text.Trim();
        
        // 古いBATファイルをクリーンアップ
        CleanupOldBatFiles(taskName);
        
        var batFileName = $"{taskName}_{DateTime.Now:yyyyMMddHHmmss}.bat";
        var batFilePath = Path.Combine(_batFolderPath, batFileName);

        // 同じタスク名の古いBATファイルを削除
        CleanupOldBatFiles(taskName);

        // BATファイルの内容を生成
        var batContent = GenerateBatContent();

        // BATファイルを作成 (UTF-8 with BOM for Windows compatibility)
        var encoding = new UTF8Encoding(true); // BOM付きUTF-8
        await File.WriteAllTextAsync(batFilePath, batContent, encoding);

        // タスクスケジューラーに登録
        CreateScheduledTask(taskName, batFilePath);

        MessageBox.Show($"タスク '{taskName}' が正常に作成されました。\nBATファイル: {batFilePath}", 
            "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // タスク一覧を更新
        LoadExistingTasks();
    }

    /// <summary>
    /// BATファイルの内容を生成
    /// </summary>
    private string GenerateBatContent()
    {
        var batContent = new System.Text.StringBuilder();
        
        batContent.AppendLine("@echo off");
        batContent.AppendLine("chcp 65001 > nul"); // UTF-8コードページに設定
        batContent.AppendLine("setlocal");
        batContent.AppendLine("");
        batContent.AppendLine("echo TaskCrony 自動実行開始: %date% %time%");
        batContent.AppendLine("");

        // 日付オフセットを適用した日付文字列を動的に生成
        var dateOffset = (int)numericUpDownDateOffset.Value;
        batContent.AppendLine("rem 日付オフセットの計算");
        if (dateOffset != 0)
        {
            batContent.AppendLine($"powershell -command \"$date = (Get-Date).AddDays({dateOffset}); $date.ToString('yyyyMMdd')\" > temp_date.txt");
            batContent.AppendLine("set /p DATE_STRING=<temp_date.txt");
            batContent.AppendLine("del temp_date.txt");
        }
        else
        {
            batContent.AppendLine("powershell -command \"(Get-Date).ToString('yyyyMMdd')\" > temp_date.txt");
            batContent.AppendLine("set /p DATE_STRING=<temp_date.txt");
            batContent.AppendLine("del temp_date.txt");
        }
        batContent.AppendLine("");

        // ファイル名構築関数をBATに追加
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
        batContent.AppendLine(":main");

        if (checkBoxCreateFolder.Checked)
        {
            var folderBaseName = string.IsNullOrEmpty(textBoxFolderBaseName.Text) ? "Folder" : textBoxFolderBaseName.Text;
            
            // フォルダ用のプレフィックス・サフィックス設定を取得
            var folderPrefix = textBoxFolderPrefix.Text;
            var folderSuffix = textBoxFolderSuffix.Text;
            var folderPrefixDatePos = GetDatePosition(checkBoxFolderPrefixDateBefore.Checked, checkBoxFolderPrefixDateAfter.Checked, checkBoxFolderPrefixDateNone.Checked);
            var folderSuffixDatePos = GetDatePosition(checkBoxFolderSuffixDateBefore.Checked, checkBoxFolderSuffixDateAfter.Checked, checkBoxFolderSuffixDateNone.Checked);
            
            batContent.AppendLine($"call :build_filename \"{folderBaseName}\" \"{folderPrefix}\" \"{folderSuffix}\" \"{folderPrefixDatePos}\" \"{folderSuffixDatePos}\"");
            batContent.AppendLine($"set FOLDER_NAME=%RESULT%");
            batContent.AppendLine($"set FOLDER_PATH=\"{textBoxDestinationPath.Text}\\%FOLDER_NAME%\"");
            batContent.AppendLine("");
            batContent.AppendLine("echo フォルダ作成: %FOLDER_PATH%");
            batContent.AppendLine("if not exist %FOLDER_PATH% mkdir %FOLDER_PATH%");
            batContent.AppendLine("");

            // 両方にチェックが入っている場合、フォルダ内にファイルをコピー
            if (checkBoxCreateFile.Checked)
            {
                var sourceFile = textBoxSourcePath.Text;
                var fileExtension = Path.GetExtension(sourceFile);
                var baseFileName = Path.GetFileNameWithoutExtension(sourceFile);
                
                // ファイル用のプレフィックス・サフィックス設定を取得
                var filePrefix = textBoxPrefix.Text;
                var fileSuffix = textBoxSuffix.Text;
                var filePrefixDatePos = GetDatePosition(checkBoxPrefixDateBefore.Checked, checkBoxPrefixDateAfter.Checked, checkBoxPrefixDateNone.Checked);
                var fileSuffixDatePos = GetDatePosition(checkBoxSuffixDateBefore.Checked, checkBoxSuffixDateAfter.Checked, checkBoxSuffixDateNone.Checked);

                batContent.AppendLine($"call :build_filename \"{baseFileName}\" \"{filePrefix}\" \"{fileSuffix}\" \"{filePrefixDatePos}\" \"{fileSuffixDatePos}\"");
                batContent.AppendLine($"set FILE_NAME=%RESULT%{fileExtension}");
                batContent.AppendLine($"set FILE_PATH=%FOLDER_PATH%\\%FILE_NAME%");
                batContent.AppendLine("");
                batContent.AppendLine("echo ファイルコピー（フォルダ内）: %FILE_PATH%");
                batContent.AppendLine($"copy \"{sourceFile}\" %FILE_PATH%");
                batContent.AppendLine("");
            }
        }
        else if (checkBoxCreateFile.Checked)
        {
            var sourceFile = textBoxSourcePath.Text;
            var fileExtension = Path.GetExtension(sourceFile);
            var baseFileName = Path.GetFileNameWithoutExtension(sourceFile);
            
            // ファイル用のプレフィックス・サフィックス設定を取得
            var filePrefix = textBoxPrefix.Text;
            var fileSuffix = textBoxSuffix.Text;
            var filePrefixDatePos = GetDatePosition(checkBoxPrefixDateBefore.Checked, checkBoxPrefixDateAfter.Checked, checkBoxPrefixDateNone.Checked);
            var fileSuffixDatePos = GetDatePosition(checkBoxSuffixDateBefore.Checked, checkBoxSuffixDateAfter.Checked, checkBoxSuffixDateNone.Checked);

            batContent.AppendLine($"call :build_filename \"{baseFileName}\" \"{filePrefix}\" \"{fileSuffix}\" \"{filePrefixDatePos}\" \"{fileSuffixDatePos}\"");
            batContent.AppendLine($"set FILE_NAME=%RESULT%{fileExtension}");
            batContent.AppendLine($"set FILE_PATH=\"{textBoxDestinationPath.Text}\\%FILE_NAME%\"");
            batContent.AppendLine("");
            batContent.AppendLine("echo ファイルコピー: %FILE_PATH%");
            batContent.AppendLine($"copy \"{sourceFile}\" %FILE_PATH%");
            batContent.AppendLine("");
        }

        batContent.AppendLine("echo TaskCrony 自動実行完了: %date% %time%");
        batContent.AppendLine("endlocal");

        return batContent.ToString();
    }

    /// <summary>
    /// ファイル名を構築
    /// </summary>
    private string BuildFileName(string baseName, string dateString)
    {
        var result = baseName;
        string prefix = textBoxPrefix.Text;
        string suffix = textBoxSuffix.Text;

        // 接頭語の追加
        if (!string.IsNullOrWhiteSpace(prefix))
        {
            if (checkBoxPrefixDateBefore.Checked)
            {
                result = dateString + prefix + result;
            }
            else if (checkBoxPrefixDateAfter.Checked)
            {
                result = prefix + dateString + result;
            }
            else
            {
                result = prefix + result;
            }
        }

        // 接尾語の追加
        if (!string.IsNullOrWhiteSpace(suffix))
        {
            if (checkBoxSuffixDateBefore.Checked)
            {
                result = result + dateString + suffix;
            }
            else if (checkBoxSuffixDateAfter.Checked)
            {
                result = result + suffix + dateString;
            }
            else
            {
                result = result + suffix;
            }
        }

        return result;
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

    /// <summary>
    /// スケジュールされたタスクを作成
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
        taskDefinition.RegistrationInfo.Description = $"TaskCrony で作成されたタスク: {taskName}";
        taskDefinition.Principal.LogonType = TaskLogonType.InteractiveToken;

        // トリガーの設定
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

        // アクションの設定
        var execAction = new ExecAction("cmd.exe", $"/c \"{batFilePath}\"", null);
        taskDefinition.Actions.Add(execAction);

        // タスクを登録
        taskService.RootFolder.RegisterTaskDefinition(taskName, taskDefinition);
    }

    /// <summary>
    /// 既存のタスクを読み込み
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
    /// タスクを削除
    /// </summary>
    private void DeleteTask(string taskName)
    {
        try
        {
            using var taskService = new TaskService();
            taskService.RootFolder.DeleteTask(taskName);
            
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
    /// ファイル名を構築（フォルダとファイル用の個別バージョン）
    /// </summary>
    private string BuildFileNameWithSettings(string baseName, string dateString, string prefix, string suffix, bool prefixDateBefore, bool prefixDateAfter, bool prefixDateNone, bool suffixDateBefore, bool suffixDateAfter, bool suffixDateNone)
    {
        var result = baseName;

        // 接頭語の追加
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
            // 接頭語が空でも日付チェックボックスが選択されている場合（「なし」以外）
            if (prefixDateBefore || prefixDateAfter)
            {
                result = dateString + result;
            }
        }

        // 接尾語の追加
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
            // 接尾語が空でも日付チェックボックスが選択されている場合（「なし」以外）
            if (suffixDateBefore || suffixDateAfter)
            {
                result = result + dateString;
            }
        }

        return result;
    }

    /// <summary>
    /// ファイル名・フォルダ名プレビューを更新
    /// </summary>
    private void UpdateFileNamePreview(object? sender, EventArgs e)
    {
        try
        {
            var previewText = new System.Text.StringBuilder();
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
                    checkBoxFolderPrefixDateBefore.Checked,
                    checkBoxFolderPrefixDateAfter.Checked,
                    checkBoxFolderPrefixDateNone.Checked,
                    checkBoxFolderSuffixDateBefore.Checked,
                    checkBoxFolderSuffixDateAfter.Checked,
                    checkBoxFolderSuffixDateNone.Checked);
                previewText.AppendLine($"フォルダ名: {folderName}");
            }

            // ファイル名プレビュー
            if (checkBoxCreateFile.Checked)
            {
                string sourceFileName = Path.GetFileNameWithoutExtension(textBoxSourcePath.Text);
                string sourceExtension = Path.GetExtension(textBoxSourcePath.Text);
                
                if (string.IsNullOrEmpty(sourceFileName))
                {
                    sourceFileName = "ファイル名";
                    sourceExtension = ".txt";
                }

                // ファイル名置換処理
                if (!string.IsNullOrEmpty(textBoxReplaceFrom.Text))
                {
                    sourceFileName = sourceFileName.Replace(textBoxReplaceFrom.Text, textBoxReplaceTo.Text ?? "");
                }

                var fileName = BuildFileNameWithSettings(
                    sourceFileName, 
                    dateString, 
                    textBoxPrefix.Text, 
                    textBoxSuffix.Text,
                    checkBoxPrefixDateBefore.Checked,
                    checkBoxPrefixDateAfter.Checked,
                    checkBoxPrefixDateNone.Checked,
                    checkBoxSuffixDateBefore.Checked,
                    checkBoxSuffixDateAfter.Checked,
                    checkBoxSuffixDateNone.Checked) + sourceExtension;
                
                if (checkBoxCreateFolder.Checked)
                {
                    var folderBaseName = string.IsNullOrEmpty(textBoxFolderBaseName.Text) ? "Folder" : textBoxFolderBaseName.Text;
                    var folderName = BuildFileNameWithSettings(
                        folderBaseName, 
                        dateString, 
                        textBoxFolderPrefix.Text, 
                        textBoxFolderSuffix.Text,
                        checkBoxFolderPrefixDateBefore.Checked,
                        checkBoxFolderPrefixDateAfter.Checked,
                        checkBoxFolderPrefixDateNone.Checked,
                        checkBoxFolderSuffixDateBefore.Checked,
                        checkBoxFolderSuffixDateAfter.Checked,
                        checkBoxFolderSuffixDateNone.Checked);
                    previewText.AppendLine($"ファイル名: {folderName}\\{fileName}");
                }
                else
                {
                    previewText.AppendLine($"ファイル名: {fileName}");
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
            textBoxPreview.Text = $"エラー: {ex.Message}";
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
}
