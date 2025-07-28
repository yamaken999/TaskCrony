namespace TaskCrony
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // フォーム設定（縦サイズをさらに拡大）
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 850);
            this.Text = "TaskCrony v1.3.2 - Windows Task Scheduler Automation Tool";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // タブコントロール
            this.tabControl = new TabControl();
            this.tabPageTaskCreation = new TabPage();
            this.tabPageTaskManagement = new TabPage();

            // タスク作成タブ
            InitializeTaskCreationTab();
            
            // タスク管理タブ
            InitializeTaskManagementTab();

            // タブコントロールの設定
            this.tabControl.SuspendLayout();
            this.tabControl.TabPages.Add(this.tabPageTaskCreation);
            this.tabControl.TabPages.Add(this.tabPageTaskManagement);
            this.tabControl.Dock = DockStyle.Fill;
            this.tabPageTaskCreation.Text = "タスク作成";
            this.tabPageTaskManagement.Text = "タスク管理";

            // フォームにコントロールを追加
            this.Controls.Add(this.tabControl);
            
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// タスク作成タブの初期化
        /// </summary>
        private void InitializeTaskCreationTab()
        {
            // 基本設定グループ
            this.groupBoxBasicSettings = new GroupBox();
            this.labelTaskName = new Label();
            this.textBoxTaskName = new TextBox();
            this.checkBoxCreateFile = new CheckBox();
            this.checkBoxCreateFolder = new CheckBox();
            this.buttonClearInputs = new Button(); // 入力クリアボタン追加

            // ファイルパス設定グループ
            this.groupBoxFilePaths = new GroupBox();
            this.labelSourcePath = new Label();
            this.textBoxSourcePath = new TextBox();
            this.buttonBrowseSource = new Button();
            this.labelDestinationPath = new Label();
            this.textBoxDestinationPath = new TextBox();
            this.buttonBrowseDestination = new Button();

            // ファイル名設定グループ
            this.groupBoxFileNameSettings = new GroupBox();
            this.labelPrefix = new Label();
            this.textBoxPrefix = new TextBox();
            this.labelSuffix = new Label();
            this.textBoxSuffix = new TextBox();
            
            // ラジオボタンに変更（プレフィックス日付位置）
            this.groupBoxPrefixOptions = new GroupBox();
            this.radioPrefixDateBefore = new RadioButton();
            this.radioPrefixDateAfter = new RadioButton();
            this.radioPrefixDateNone = new RadioButton();
            
            // ラジオボタンに変更（サフィックス日付位置）
            this.groupBoxSuffixOptions = new GroupBox();
            this.radioSuffixDateBefore = new RadioButton();
            this.radioSuffixDateAfter = new RadioButton();
            this.radioSuffixDateNone = new RadioButton();

            // フォルダ名設定グループ
            this.groupBoxFolderNameSettings = new GroupBox();
            this.labelFolderBaseName = new Label();
            this.textBoxFolderBaseName = new TextBox();
            this.labelFolderPrefix = new Label();
            this.textBoxFolderPrefix = new TextBox();
            this.labelFolderSuffix = new Label();
            this.textBoxFolderSuffix = new TextBox();
            
            // ラジオボタンに変更（フォルダプレフィックス日付位置）
            this.groupBoxFolderPrefixOptions = new GroupBox();
            this.radioFolderPrefixDateBefore = new RadioButton();
            this.radioFolderPrefixDateAfter = new RadioButton();
            this.radioFolderPrefixDateNone = new RadioButton();
            
            // ラジオボタンに変更（フォルダサフィックス日付位置）
            this.groupBoxFolderSuffixOptions = new GroupBox();
            this.radioFolderSuffixDateBefore = new RadioButton();
            this.radioFolderSuffixDateAfter = new RadioButton();
            this.radioFolderSuffixDateNone = new RadioButton();

            // 文字列置換グループ
            this.groupBoxFileReplace = new GroupBox();
            this.labelReplaceFrom = new Label();
            this.textBoxReplaceFrom = new TextBox();
            this.labelReplaceTo = new Label();
            this.textBoxReplaceTo = new TextBox();

            // 共通設定グループ
            this.groupBoxCommonSettings = new GroupBox();
            this.labelDateOffset = new Label();
            this.numericUpDownDateOffset = new NumericUpDown();
            this.labelPreview = new Label();
            this.textBoxPreview = new TextBox();

            // スケジュール設定グループ
            this.groupBoxScheduleSettings = new GroupBox();
            this.labelScheduleType = new Label();
            this.comboBoxScheduleType = new ComboBox();
            this.labelStartDateTime = new Label();
            this.dateTimePickerStart = new DateTimePicker();

            // ボタン
            this.buttonCreateTask = new Button();

            // レイアウト設定
            SetupTaskCreationLayout();
        }

        /// <summary>
        /// タスク管理タブの初期化
        /// </summary>
        private void InitializeTaskManagementTab()
        {
            // タスク一覧
            this.listViewTasks = new ListView();
            this.columnHeaderTaskName = new ColumnHeader();
            this.columnHeaderStatus = new ColumnHeader();
            this.columnHeaderNextRun = new ColumnHeader();
            this.columnHeaderLastRun = new ColumnHeader();

            // ボタン
            this.buttonRefreshTasks = new Button();
            this.buttonEditTask = new Button();
            this.buttonDeleteTask = new Button();
            this.buttonRunTask = new Button();
            this.buttonOpenTaskScheduler = new Button();

            // レイアウト設定
            SetupTaskManagementLayout();
        }

        /// <summary>
        /// タスク作成タブのレイアウト設定
        /// </summary>
        private void SetupTaskCreationLayout()
        {
            this.tabPageTaskCreation.SuspendLayout();

            // 基本設定グループ（サイズを縮小）
            this.groupBoxBasicSettings.Text = "基本設定";
            this.groupBoxBasicSettings.Location = new Point(10, 10);
            this.groupBoxBasicSettings.Size = new Size(560, 90);

            this.labelTaskName.Text = "タスク名:";
            this.labelTaskName.Location = new Point(15, 25);
            this.labelTaskName.Size = new Size(80, 20);

            this.textBoxTaskName.Location = new Point(100, 22);
            this.textBoxTaskName.Size = new Size(440, 23);

            this.checkBoxCreateFile.Text = "ファイルコピー";
            this.checkBoxCreateFile.Location = new Point(15, 55);
            this.checkBoxCreateFile.Size = new Size(160, 25);

            this.checkBoxCreateFolder.Text = "フォルダ作成";
            this.checkBoxCreateFolder.Location = new Point(190, 55);
            this.checkBoxCreateFolder.Size = new Size(160, 25);

            this.groupBoxBasicSettings.Controls.AddRange(new Control[]
            {
                this.labelTaskName, this.textBoxTaskName,
                this.checkBoxCreateFile, this.checkBoxCreateFolder
            });

            // ファイルパス設定グループ（位置調整）
            this.groupBoxFilePaths.Text = "ファイルパス設定";
            this.groupBoxFilePaths.Location = new Point(10, 110);
            this.groupBoxFilePaths.Size = new Size(1160, 80);

            this.labelSourcePath.Text = "コピー元:";
            this.labelSourcePath.Location = new Point(15, 25);
            this.labelSourcePath.Size = new Size(80, 20);

            this.textBoxSourcePath.Location = new Point(100, 22);
            this.textBoxSourcePath.Size = new Size(970, 23);

            this.buttonBrowseSource.Text = "参照...";
            this.buttonBrowseSource.Location = new Point(1080, 21);
            this.buttonBrowseSource.Size = new Size(70, 28);

            this.labelDestinationPath.Text = "作成先:";
            this.labelDestinationPath.Location = new Point(15, 50);
            this.labelDestinationPath.Size = new Size(80, 20);

            this.textBoxDestinationPath.Location = new Point(100, 47);
            this.textBoxDestinationPath.Size = new Size(970, 23);

            this.buttonBrowseDestination.Text = "参照...";
            this.buttonBrowseDestination.Location = new Point(1080, 46);
            this.buttonBrowseDestination.Size = new Size(70, 28);

            this.groupBoxFilePaths.Controls.AddRange(new Control[]
            {
                this.labelSourcePath, this.textBoxSourcePath, this.buttonBrowseSource,
                this.labelDestinationPath, this.textBoxDestinationPath, this.buttonBrowseDestination
            });

            // ファイル名設定グループの設定を呼び出し
            SetupFileNameSettingsControls();

            // フォルダ名設定グループ（ファイル名設定の下に配置）
            this.groupBoxFolderNameSettings.Text = "フォルダ名設定";
            this.groupBoxFolderNameSettings.Location = new Point(10, 460);
            this.groupBoxFolderNameSettings.Size = new Size(650, 180);

            SetupFolderNameSettingsControls();

            // 共通設定グループ（フォルダ名設定の下に移動）
            this.groupBoxCommonSettings.Text = "共通設定・プレビュー";
            this.groupBoxCommonSettings.Location = new Point(10, 650);
            this.groupBoxCommonSettings.Size = new Size(1160, 110);

            SetupCommonSettingsControls();

            // スケジュール設定グループ（位置調整）
            this.groupBoxScheduleSettings.Text = "スケジュール設定";
            this.groupBoxScheduleSettings.Location = new Point(580, 10);
            this.groupBoxScheduleSettings.Size = new Size(590, 120);

            SetupScheduleSettingsControls();

            // タスク作成ボタン（位置を下に調整）
            this.buttonCreateTask.Text = "タスク作成";
            this.buttonCreateTask.Location = new Point(480, 770);
            this.buttonCreateTask.Size = new Size(160, 40);
            this.buttonCreateTask.BackColor = Color.FromArgb(0, 120, 215);
            this.buttonCreateTask.ForeColor = Color.White;
            this.buttonCreateTask.FlatStyle = FlatStyle.Flat;

            // 入力クリアボタンをタスク作成ボタンの横に配置
            this.buttonClearInputs.Text = "入力内容をクリア";
            this.buttonClearInputs.Location = new Point(640, 770);
            this.buttonClearInputs.Size = new Size(160, 40);
            this.buttonClearInputs.BackColor = Color.FromArgb(243, 243, 243);
            this.buttonClearInputs.FlatStyle = FlatStyle.Flat;

            // タスク作成タブにコントロールを追加
            this.tabPageTaskCreation.Controls.AddRange(new Control[]
            {
                this.groupBoxBasicSettings, this.groupBoxFilePaths,
                this.groupBoxFileNameSettings, this.groupBoxFolderNameSettings,
                this.groupBoxCommonSettings, this.groupBoxScheduleSettings,
                this.buttonCreateTask, this.buttonClearInputs
            });

            this.tabPageTaskCreation.ResumeLayout(false);
        }

        /// <summary>
        /// ファイル名設定コントロールの詳細設定
        /// </summary>
        private void SetupFileNameSettingsControls()
        {
            // ファイル名設定グループの位置とサイズを正しく設定
            this.groupBoxFileNameSettings.Text = "ファイル名設定";
            this.groupBoxFileNameSettings.Location = new Point(10, 200);
            this.groupBoxFileNameSettings.Size = new Size(650, 250);

            this.labelPrefix.Text = "接頭語:";
            this.labelPrefix.Location = new Point(15, 25);
            this.labelPrefix.Size = new Size(60, 20);

            this.textBoxPrefix.Location = new Point(80, 22);
            this.textBoxPrefix.Size = new Size(120, 23);

            this.labelSuffix.Text = "接尾語:";
            this.labelSuffix.Location = new Point(220, 25);
            this.labelSuffix.Size = new Size(60, 20);

            this.textBoxSuffix.Location = new Point(285, 22);
            this.textBoxSuffix.Size = new Size(120, 23);

            // 接頭語オプション（ラジオボタンに変更）
            this.groupBoxPrefixOptions.Text = "接頭語日付位置";
            this.groupBoxPrefixOptions.Location = new Point(15, 55);
            this.groupBoxPrefixOptions.Size = new Size(600, 50);

            this.radioPrefixDateBefore.Text = "日付＋接頭語";
            this.radioPrefixDateBefore.Location = new Point(10, 20);
            this.radioPrefixDateBefore.Size = new Size(120, 23);

            this.radioPrefixDateAfter.Text = "接頭語＋日付";
            this.radioPrefixDateAfter.Location = new Point(150, 20);
            this.radioPrefixDateAfter.Size = new Size(120, 23);

            this.radioPrefixDateNone.Text = "なし";
            this.radioPrefixDateNone.Location = new Point(290, 20);
            this.radioPrefixDateNone.Size = new Size(60, 23);
            this.radioPrefixDateNone.Checked = true;

            this.groupBoxPrefixOptions.Controls.AddRange(new Control[]
            {
                this.radioPrefixDateBefore, this.radioPrefixDateAfter, this.radioPrefixDateNone
            });

            // 接尾語オプション（ラジオボタンに変更）
            this.groupBoxSuffixOptions.Text = "接尾語日付位置";
            this.groupBoxSuffixOptions.Location = new Point(15, 115);
            this.groupBoxSuffixOptions.Size = new Size(600, 50);

            this.radioSuffixDateBefore.Text = "日付＋接尾語";
            this.radioSuffixDateBefore.Location = new Point(10, 20);
            this.radioSuffixDateBefore.Size = new Size(120, 23);

            this.radioSuffixDateAfter.Text = "接尾語＋日付";
            this.radioSuffixDateAfter.Location = new Point(150, 20);
            this.radioSuffixDateAfter.Size = new Size(120, 23);

            this.radioSuffixDateNone.Text = "なし";
            this.radioSuffixDateNone.Location = new Point(290, 20);
            this.radioSuffixDateNone.Size = new Size(60, 23);
            this.radioSuffixDateNone.Checked = true;

            this.groupBoxSuffixOptions.Controls.AddRange(new Control[]
            {
                this.radioSuffixDateBefore, this.radioSuffixDateAfter, this.radioSuffixDateNone
            });

            // 文字列置換（レイアウト改善）
            this.groupBoxFileReplace.Text = "文字列置換";
            this.groupBoxFileReplace.Location = new Point(15, 175);
            this.groupBoxFileReplace.Size = new Size(600, 60);

            this.labelReplaceFrom.Text = "置換前:";
            this.labelReplaceFrom.Location = new Point(10, 23);
            this.labelReplaceFrom.Size = new Size(60, 23);

            this.textBoxReplaceFrom.Location = new Point(75, 20);
            this.textBoxReplaceFrom.Size = new Size(150, 23);

            this.labelReplaceTo.Text = "置換後:";
            this.labelReplaceTo.Location = new Point(250, 23);
            this.labelReplaceTo.Size = new Size(60, 23);

            this.textBoxReplaceTo.Location = new Point(315, 20);
            this.textBoxReplaceTo.Size = new Size(150, 23);

            this.groupBoxFileReplace.Controls.AddRange(new Control[]
            {
                this.labelReplaceFrom, this.textBoxReplaceFrom,
                this.labelReplaceTo, this.textBoxReplaceTo
            });

            this.groupBoxFileNameSettings.Controls.AddRange(new Control[]
            {
                this.labelPrefix, this.textBoxPrefix,
                this.labelSuffix, this.textBoxSuffix,
                this.groupBoxPrefixOptions, this.groupBoxSuffixOptions,
                this.groupBoxFileReplace
            });
        }

        /// <summary>
        /// フォルダ名設定コントロールの詳細設定
        /// </summary>
        private void SetupFolderNameSettingsControls()
        {
            this.labelFolderBaseName.Text = "ベース名:";
            this.labelFolderBaseName.Location = new Point(15, 25);
            this.labelFolderBaseName.Size = new Size(80, 20);

            this.textBoxFolderBaseName.Location = new Point(100, 22);
            this.textBoxFolderBaseName.Size = new Size(120, 23);
            this.textBoxFolderBaseName.Text = "Folder";

            this.labelFolderPrefix.Text = "接頭語:";
            this.labelFolderPrefix.Location = new Point(240, 25);
            this.labelFolderPrefix.Size = new Size(60, 20);

            this.textBoxFolderPrefix.Location = new Point(305, 22);
            this.textBoxFolderPrefix.Size = new Size(100, 23);

            this.labelFolderSuffix.Text = "接尾語:";
            this.labelFolderSuffix.Location = new Point(440, 25);
            this.labelFolderSuffix.Size = new Size(60, 20);

            this.textBoxFolderSuffix.Location = new Point(505, 22);
            this.textBoxFolderSuffix.Size = new Size(100, 23);

            // フォルダ接頭語オプション（ラジオボタンに変更）
            this.groupBoxFolderPrefixOptions.Text = "接頭語日付位置";
            this.groupBoxFolderPrefixOptions.Location = new Point(15, 55);
            this.groupBoxFolderPrefixOptions.Size = new Size(600, 50);

            this.radioFolderPrefixDateBefore.Text = "日付＋接頭語";
            this.radioFolderPrefixDateBefore.Location = new Point(10, 20);
            this.radioFolderPrefixDateBefore.Size = new Size(120, 23);

            this.radioFolderPrefixDateAfter.Text = "接頭語＋日付";
            this.radioFolderPrefixDateAfter.Location = new Point(150, 20);
            this.radioFolderPrefixDateAfter.Size = new Size(120, 23);

            this.radioFolderPrefixDateNone.Text = "なし";
            this.radioFolderPrefixDateNone.Location = new Point(290, 20);
            this.radioFolderPrefixDateNone.Size = new Size(60, 23);
            this.radioFolderPrefixDateNone.Checked = true;

            this.groupBoxFolderPrefixOptions.Controls.AddRange(new Control[]
            {
                this.radioFolderPrefixDateBefore, this.radioFolderPrefixDateAfter, this.radioFolderPrefixDateNone
            });

            // フォルダ接尾語オプション（ラジオボタンに変更）
            this.groupBoxFolderSuffixOptions.Text = "接尾語日付位置";
            this.groupBoxFolderSuffixOptions.Location = new Point(15, 115);
            this.groupBoxFolderSuffixOptions.Size = new Size(600, 50);

            this.radioFolderSuffixDateBefore.Text = "日付＋接尾語";
            this.radioFolderSuffixDateBefore.Location = new Point(10, 20);
            this.radioFolderSuffixDateBefore.Size = new Size(120, 23);

            this.radioFolderSuffixDateAfter.Text = "接尾語＋日付";
            this.radioFolderSuffixDateAfter.Location = new Point(150, 20);
            this.radioFolderSuffixDateAfter.Size = new Size(120, 23);

            this.radioFolderSuffixDateNone.Text = "なし";
            this.radioFolderSuffixDateNone.Location = new Point(290, 20);
            this.radioFolderSuffixDateNone.Size = new Size(60, 23);
            this.radioFolderSuffixDateNone.Checked = true;

            this.groupBoxFolderSuffixOptions.Controls.AddRange(new Control[]
            {
                this.radioFolderSuffixDateBefore, this.radioFolderSuffixDateAfter, this.radioFolderSuffixDateNone
            });

            this.groupBoxFolderNameSettings.Controls.AddRange(new Control[]
            {
                this.labelFolderBaseName, this.textBoxFolderBaseName,
                this.labelFolderPrefix, this.textBoxFolderPrefix,
                this.labelFolderSuffix, this.textBoxFolderSuffix,
                this.groupBoxFolderPrefixOptions, this.groupBoxFolderSuffixOptions
            });
        }

        /// <summary>
        /// 共通設定コントロールの詳細設定
        /// </summary>
        private void SetupCommonSettingsControls()
        {
            this.labelDateOffset.Text = "日付オフセット（日）:";
            this.labelDateOffset.Location = new Point(15, 25);
            this.labelDateOffset.Size = new Size(140, 20);

            this.numericUpDownDateOffset.Location = new Point(160, 22);
            this.numericUpDownDateOffset.Size = new Size(80, 23);
            this.numericUpDownDateOffset.Minimum = -365;
            this.numericUpDownDateOffset.Maximum = 365;
            this.numericUpDownDateOffset.Value = 0;

            this.labelPreview.Text = "プレビュー:";
            this.labelPreview.Location = new Point(15, 55);
            this.labelPreview.Size = new Size(80, 35);

            this.textBoxPreview.Location = new Point(100, 52);
            this.textBoxPreview.Size = new Size(1040, 45);
            this.textBoxPreview.Multiline = true;
            this.textBoxPreview.ReadOnly = true;
            this.textBoxPreview.BackColor = Color.FromArgb(248, 248, 248);

            this.groupBoxCommonSettings.Controls.AddRange(new Control[]
            {
                this.labelDateOffset, this.numericUpDownDateOffset,
                this.labelPreview, this.textBoxPreview
            });
        }

        /// <summary>
        /// スケジュール設定コントロールの詳細設定
        /// </summary>
        private void SetupScheduleSettingsControls()
        {
            this.labelScheduleType.Text = "スケジュール:";
            this.labelScheduleType.Location = new Point(15, 25);
            this.labelScheduleType.Size = new Size(80, 20);

            this.comboBoxScheduleType.Location = new Point(100, 22);
            this.comboBoxScheduleType.Size = new Size(120, 23);
            this.comboBoxScheduleType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxScheduleType.Items.AddRange(new string[] { "今すぐ実行", "毎日", "毎週", "毎月" });

            this.labelStartDateTime.Text = "開始日時:";
            this.labelStartDateTime.Location = new Point(235, 25);
            this.labelStartDateTime.Size = new Size(70, 23);

            this.dateTimePickerStart.Location = new Point(310, 22);
            this.dateTimePickerStart.Size = new Size(170, 25);
            this.dateTimePickerStart.Format = DateTimePickerFormat.Custom;
            this.dateTimePickerStart.CustomFormat = "yyyy/MM/dd HH:mm";

            this.groupBoxScheduleSettings.Controls.AddRange(new Control[]
            {
                this.labelScheduleType, this.comboBoxScheduleType,
                this.labelStartDateTime, this.dateTimePickerStart
            });
        }

        /// <summary>
        /// タスク管理タブのレイアウト設定
        /// </summary>
        private void SetupTaskManagementLayout()
        {
            this.tabPageTaskManagement.SuspendLayout();

            // タスク一覧ListView（拡大）
            this.listViewTasks.Location = new Point(20, 20);
            this.listViewTasks.Size = new Size(1140, 500);
            this.listViewTasks.View = View.Details;
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.GridLines = true;

            // カラムヘッダー設定（拡大）
            this.columnHeaderTaskName.Text = "タスク名";
            this.columnHeaderTaskName.Width = 300;
            this.columnHeaderStatus.Text = "ステータス";
            this.columnHeaderStatus.Width = 120;
            this.columnHeaderNextRun.Text = "次回実行";
            this.columnHeaderNextRun.Width = 200;
            this.columnHeaderLastRun.Text = "最終実行";
            this.columnHeaderLastRun.Width = 200;

            this.listViewTasks.Columns.AddRange(new ColumnHeader[]
            {
                this.columnHeaderTaskName, this.columnHeaderStatus,
                this.columnHeaderNextRun, this.columnHeaderLastRun
            });

            // ボタン配置（位置調整）
            // v0.1.0配布パッケージ作成完了 - UIボタンレイアウト最適化
            int buttonY = 540;
            int buttonSpacing = 120;
            int buttonStartX = 300;

            this.buttonRefreshTasks.Text = "更新";
            this.buttonRefreshTasks.Location = new Point(buttonStartX, buttonY);
            this.buttonRefreshTasks.Size = new Size(100, 35);

            this.buttonEditTask.Text = "編集";
            this.buttonEditTask.Location = new Point(buttonStartX + buttonSpacing, buttonY);
            this.buttonEditTask.Size = new Size(100, 35);

            this.buttonRunTask.Text = "実行";
            this.buttonRunTask.Location = new Point(buttonStartX + buttonSpacing * 2, buttonY);
            this.buttonRunTask.Size = new Size(100, 35);
            this.buttonRunTask.BackColor = Color.FromArgb(0, 120, 215);
            this.buttonRunTask.ForeColor = Color.White;
            this.buttonRunTask.FlatStyle = FlatStyle.Flat;

            this.buttonDeleteTask.Text = "削除";
            this.buttonDeleteTask.Location = new Point(buttonStartX + buttonSpacing * 3, buttonY);
            this.buttonDeleteTask.Size = new Size(100, 35);
            this.buttonDeleteTask.BackColor = Color.FromArgb(196, 43, 28);
            this.buttonDeleteTask.ForeColor = Color.White;
            this.buttonDeleteTask.FlatStyle = FlatStyle.Flat;

            this.buttonOpenTaskScheduler.Text = "タスクスケジューラ起動";
            this.buttonOpenTaskScheduler.Location = new Point(buttonStartX + buttonSpacing * 4, buttonY);
            this.buttonOpenTaskScheduler.Size = new Size(160, 35);

            // タスク管理タブにコントロールを追加
            this.tabPageTaskManagement.Controls.AddRange(new Control[]
            {
                this.listViewTasks,
                this.buttonRefreshTasks, this.buttonEditTask, this.buttonRunTask,
                this.buttonDeleteTask, this.buttonOpenTaskScheduler
            });

            this.tabPageTaskManagement.ResumeLayout(false);
        }

        #endregion

        #region コントロール宣言

        private TabControl tabControl;
        private TabPage tabPageTaskCreation;
        private TabPage tabPageTaskManagement;

        // 基本設定
        private GroupBox groupBoxBasicSettings;
        private Label labelTaskName;
        private TextBox textBoxTaskName;
        private CheckBox checkBoxCreateFile;
        private CheckBox checkBoxCreateFolder;
        private Button buttonClearInputs; // 入力クリアボタン追加

        // ファイルパス設定
        private GroupBox groupBoxFilePaths;
        private Label labelSourcePath;
        private TextBox textBoxSourcePath;
        private Button buttonBrowseSource;
        private Label labelDestinationPath;
        private TextBox textBoxDestinationPath;
        private Button buttonBrowseDestination;

        // ファイル名設定（ラジオボタンに変更）
        private GroupBox groupBoxFileNameSettings;
        private Label labelPrefix;
        private TextBox textBoxPrefix;
        private Label labelSuffix;
        private TextBox textBoxSuffix;
        private GroupBox groupBoxPrefixOptions;
        private RadioButton radioPrefixDateBefore;
        private RadioButton radioPrefixDateAfter;
        private RadioButton radioPrefixDateNone;
        private GroupBox groupBoxSuffixOptions;
        private RadioButton radioSuffixDateBefore;
        private RadioButton radioSuffixDateAfter;
        private RadioButton radioSuffixDateNone;

        // フォルダ名設定（ラジオボタンに変更）
        private GroupBox groupBoxFolderNameSettings;
        private Label labelFolderBaseName;
        private TextBox textBoxFolderBaseName;
        private Label labelFolderPrefix;
        private TextBox textBoxFolderPrefix;
        private Label labelFolderSuffix;
        private TextBox textBoxFolderSuffix;
        private GroupBox groupBoxFolderPrefixOptions;
        private RadioButton radioFolderPrefixDateBefore;
        private RadioButton radioFolderPrefixDateAfter;
        private RadioButton radioFolderPrefixDateNone;
        private GroupBox groupBoxFolderSuffixOptions;
        private RadioButton radioFolderSuffixDateBefore;
        private RadioButton radioFolderSuffixDateAfter;
        private RadioButton radioFolderSuffixDateNone;

        // 文字列置換
        private GroupBox groupBoxFileReplace;
        private Label labelReplaceFrom;
        private TextBox textBoxReplaceFrom;
        private Label labelReplaceTo;
        private TextBox textBoxReplaceTo;

        // 共通設定
        private GroupBox groupBoxCommonSettings;
        private Label labelDateOffset;
        private NumericUpDown numericUpDownDateOffset;
        private Label labelPreview;
        private TextBox textBoxPreview;

        // スケジュール設定
        private GroupBox groupBoxScheduleSettings;
        private Label labelScheduleType;
        private ComboBox comboBoxScheduleType;
        private Label labelStartDateTime;
        private DateTimePicker dateTimePickerStart;

        // ボタン
        private Button buttonCreateTask;

        // タスク管理
        private ListView listViewTasks;
        private ColumnHeader columnHeaderTaskName;
        private ColumnHeader columnHeaderStatus;
        private ColumnHeader columnHeaderNextRun;
        private ColumnHeader columnHeaderLastRun;
        private Button buttonRefreshTasks;
        private Button buttonEditTask;
        private Button buttonDeleteTask;
        private Button buttonRunTask;
        private Button buttonOpenTaskScheduler;

        #endregion
    }
}
