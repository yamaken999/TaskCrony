namespace TaskCrony;

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
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPageCreate = new System.Windows.Forms.TabPage();
        this.groupBoxOptions = new System.Windows.Forms.GroupBox();
        this.checkBoxCreateFile = new System.Windows.Forms.CheckBox();
        this.checkBoxCreateFolder = new System.Windows.Forms.CheckBox();
        this.groupBoxFiles = new System.Windows.Forms.GroupBox();
        this.labelDestinationPath = new System.Windows.Forms.Label();
        this.labelSourcePath = new System.Windows.Forms.Label();
        this.textBoxDestinationPath = new System.Windows.Forms.TextBox();
        this.textBoxSourcePath = new System.Windows.Forms.TextBox();
        this.buttonBrowseDestination = new System.Windows.Forms.Button();
        this.buttonBrowseSource = new System.Windows.Forms.Button();
        this.groupBoxNaming = new System.Windows.Forms.GroupBox();
        this.groupBoxPrefixOptions = new System.Windows.Forms.GroupBox();
        this.checkBoxPrefixDateBefore = new System.Windows.Forms.RadioButton();
        this.checkBoxPrefixDateAfter = new System.Windows.Forms.RadioButton();
        this.checkBoxPrefixDateNone = new System.Windows.Forms.RadioButton();
        this.groupBoxSuffixOptions = new System.Windows.Forms.GroupBox();
        this.checkBoxSuffixDateBefore = new System.Windows.Forms.RadioButton();
        this.checkBoxSuffixDateAfter = new System.Windows.Forms.RadioButton();
        this.checkBoxSuffixDateNone = new System.Windows.Forms.RadioButton();
        this.groupBoxFileReplace = new System.Windows.Forms.GroupBox();
        this.labelReplaceFrom = new System.Windows.Forms.Label();
        this.textBoxReplaceFrom = new System.Windows.Forms.TextBox();
        this.labelReplaceTo = new System.Windows.Forms.Label();
        this.textBoxReplaceTo = new System.Windows.Forms.TextBox();
        this.labelPreview = new System.Windows.Forms.Label();
        this.textBoxPreview = new System.Windows.Forms.TextBox();
        this.labelDateOffset = new System.Windows.Forms.Label();
        this.numericUpDownDateOffset = new System.Windows.Forms.NumericUpDown();
        this.labelSuffix = new System.Windows.Forms.Label();
        this.labelPrefix = new System.Windows.Forms.Label();
        this.textBoxSuffix = new System.Windows.Forms.TextBox();
        this.textBoxPrefix = new System.Windows.Forms.TextBox();
        this.textBoxFolderBaseName = new System.Windows.Forms.TextBox();
        this.labelFolderBaseName = new System.Windows.Forms.Label();
        this.textBoxFolderPrefix = new System.Windows.Forms.TextBox();
        this.labelFolderPrefix = new System.Windows.Forms.Label();
        this.textBoxFolderSuffix = new System.Windows.Forms.TextBox();
        this.labelFolderSuffix = new System.Windows.Forms.Label();
        this.groupBoxFolderPrefixOptions = new System.Windows.Forms.GroupBox();
        this.checkBoxFolderPrefixDateBefore = new System.Windows.Forms.RadioButton();
        this.checkBoxFolderPrefixDateAfter = new System.Windows.Forms.RadioButton();
        this.checkBoxFolderPrefixDateNone = new System.Windows.Forms.RadioButton();
        this.groupBoxFolderSuffixOptions = new System.Windows.Forms.GroupBox();
        this.checkBoxFolderSuffixDateBefore = new System.Windows.Forms.RadioButton();
        this.checkBoxFolderSuffixDateAfter = new System.Windows.Forms.RadioButton();
        this.checkBoxFolderSuffixDateNone = new System.Windows.Forms.RadioButton();
        this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
        this.labelTaskName = new System.Windows.Forms.Label();
        this.textBoxTaskName = new System.Windows.Forms.TextBox();
        this.labelStartTime = new System.Windows.Forms.Label();
        this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
        this.labelScheduleType = new System.Windows.Forms.Label();
        this.comboBoxScheduleType = new System.Windows.Forms.ComboBox();
        this.buttonCreateTask = new System.Windows.Forms.Button();
        this.tabPageManage = new System.Windows.Forms.TabPage();
        this.listViewTasks = new System.Windows.Forms.ListView();
        this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
        this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
        this.columnHeaderNextRun = new System.Windows.Forms.ColumnHeader();
        this.columnHeaderLastRun = new System.Windows.Forms.ColumnHeader();
        this.buttonRefreshTasks = new System.Windows.Forms.Button();
        this.buttonDeleteTask = new System.Windows.Forms.Button();
        this.buttonEditTask = new System.Windows.Forms.Button();
        this.buttonOpenTaskScheduler = new System.Windows.Forms.Button();
        this.tabControl1.SuspendLayout();
        this.tabPageCreate.SuspendLayout();
        this.groupBoxOptions.SuspendLayout();
        this.groupBoxFiles.SuspendLayout();
        this.groupBoxNaming.SuspendLayout();
        this.groupBoxPrefixOptions.SuspendLayout();
        this.groupBoxSuffixOptions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDateOffset)).BeginInit();
        this.groupBoxSchedule.SuspendLayout();
        this.tabPageManage.SuspendLayout();
        this.SuspendLayout();
        // 
        // tabControl1
        // 
        this.tabControl1.Controls.Add(this.tabPageCreate);
        this.tabControl1.Controls.Add(this.tabPageManage);
        this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl1.Location = new System.Drawing.Point(0, 0);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(800, 600);
        this.tabControl1.TabIndex = 0;
        // 
        // tabPageCreate
        // 
        this.tabPageCreate.Controls.Add(this.buttonCreateTask);
        this.tabPageCreate.Controls.Add(this.groupBoxSchedule);
        this.tabPageCreate.Controls.Add(this.groupBoxNaming);
        this.tabPageCreate.Controls.Add(this.groupBoxFiles);
        this.tabPageCreate.Controls.Add(this.groupBoxOptions);
        this.tabPageCreate.Location = new System.Drawing.Point(4, 24);
        this.tabPageCreate.Name = "tabPageCreate";
        this.tabPageCreate.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageCreate.Size = new System.Drawing.Size(792, 572);
        this.tabPageCreate.TabIndex = 0;
        this.tabPageCreate.Text = "タスク作成";
        this.tabPageCreate.UseVisualStyleBackColor = true;
        // 
        // groupBoxOptions
        // 
        this.groupBoxOptions.Controls.Add(this.checkBoxCreateFile);
        this.groupBoxOptions.Controls.Add(this.checkBoxCreateFolder);
        this.groupBoxOptions.Location = new System.Drawing.Point(6, 6);
        this.groupBoxOptions.Name = "groupBoxOptions";
        this.groupBoxOptions.Size = new System.Drawing.Size(780, 60);
        this.groupBoxOptions.TabIndex = 0;
        this.groupBoxOptions.TabStop = false;
        this.groupBoxOptions.Text = "実行オプション";
        // 
        // checkBoxCreateFile
        // 
        this.checkBoxCreateFile.AutoSize = true;
        this.checkBoxCreateFile.Checked = true;
        this.checkBoxCreateFile.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBoxCreateFile.Location = new System.Drawing.Point(6, 22);
        this.checkBoxCreateFile.Name = "checkBoxCreateFile";
        this.checkBoxCreateFile.Size = new System.Drawing.Size(99, 19);
        this.checkBoxCreateFile.TabIndex = 0;
        this.checkBoxCreateFile.Text = "ファイルを作成";
        this.checkBoxCreateFile.UseVisualStyleBackColor = true;
        // 
        // checkBoxCreateFolder
        // 
        this.checkBoxCreateFolder.AutoSize = true;
        this.checkBoxCreateFolder.Location = new System.Drawing.Point(111, 22);
        this.checkBoxCreateFolder.Name = "checkBoxCreateFolder";
        this.checkBoxCreateFolder.Size = new System.Drawing.Size(103, 19);
        this.checkBoxCreateFolder.TabIndex = 1;
        this.checkBoxCreateFolder.Text = "フォルダを作成";
        this.checkBoxCreateFolder.UseVisualStyleBackColor = true;
        // 
        // groupBoxFiles
        // 
        this.groupBoxFiles.Controls.Add(this.buttonBrowseSource);
        this.groupBoxFiles.Controls.Add(this.buttonBrowseDestination);
        this.groupBoxFiles.Controls.Add(this.textBoxSourcePath);
        this.groupBoxFiles.Controls.Add(this.textBoxDestinationPath);
        this.groupBoxFiles.Controls.Add(this.labelSourcePath);
        this.groupBoxFiles.Controls.Add(this.labelDestinationPath);
        this.groupBoxFiles.Location = new System.Drawing.Point(6, 72);
        this.groupBoxFiles.Name = "groupBoxFiles";
        this.groupBoxFiles.Size = new System.Drawing.Size(780, 100);
        this.groupBoxFiles.TabIndex = 1;
        this.groupBoxFiles.TabStop = false;
        this.groupBoxFiles.Text = "ファイル/フォルダ設定";
        // 
        // labelSourcePath
        // 
        this.labelSourcePath.AutoSize = true;
        this.labelSourcePath.Location = new System.Drawing.Point(6, 25);
        this.labelSourcePath.Name = "labelSourcePath";
        this.labelSourcePath.Size = new System.Drawing.Size(43, 15);
        this.labelSourcePath.TabIndex = 0;
        this.labelSourcePath.Text = "コピー元:";
        // 
        // labelDestinationPath
        // 
        this.labelDestinationPath.AutoSize = true;
        this.labelDestinationPath.Location = new System.Drawing.Point(6, 60);
        this.labelDestinationPath.Name = "labelDestinationPath";
        this.labelDestinationPath.Size = new System.Drawing.Size(43, 15);
        this.labelDestinationPath.TabIndex = 3;
        this.labelDestinationPath.Text = "コピー先:";
        // 
        // textBoxSourcePath
        // 
        this.textBoxSourcePath.Location = new System.Drawing.Point(70, 22);
        this.textBoxSourcePath.Name = "textBoxSourcePath";
        this.textBoxSourcePath.Size = new System.Drawing.Size(620, 23);
        this.textBoxSourcePath.TabIndex = 1;
        // 
        // textBoxDestinationPath
        // 
        this.textBoxDestinationPath.Location = new System.Drawing.Point(70, 57);
        this.textBoxDestinationPath.Name = "textBoxDestinationPath";
        this.textBoxDestinationPath.Size = new System.Drawing.Size(620, 23);
        this.textBoxDestinationPath.TabIndex = 4;
        // 
        // buttonBrowseSource
        // 
        this.buttonBrowseSource.Location = new System.Drawing.Point(696, 21);
        this.buttonBrowseSource.Name = "buttonBrowseSource";
        this.buttonBrowseSource.Size = new System.Drawing.Size(75, 23);
        this.buttonBrowseSource.TabIndex = 2;
        this.buttonBrowseSource.Text = "参照";
        this.buttonBrowseSource.UseVisualStyleBackColor = true;
        // 
        // buttonBrowseDestination
        // 
        this.buttonBrowseDestination.Location = new System.Drawing.Point(696, 56);
        this.buttonBrowseDestination.Name = "buttonBrowseDestination";
        this.buttonBrowseDestination.Size = new System.Drawing.Size(75, 23);
        this.buttonBrowseDestination.TabIndex = 5;
        this.buttonBrowseDestination.Text = "参照";
        this.buttonBrowseDestination.UseVisualStyleBackColor = true;
        // 
        // groupBoxNaming
        // 
        this.groupBoxNaming.Controls.Add(this.textBoxPrefix);
        this.groupBoxNaming.Controls.Add(this.textBoxSuffix);
        this.groupBoxNaming.Controls.Add(this.labelPrefix);
        this.groupBoxNaming.Controls.Add(this.labelSuffix);
        this.groupBoxNaming.Controls.Add(this.numericUpDownDateOffset);
        this.groupBoxNaming.Controls.Add(this.labelDateOffset);
        this.groupBoxNaming.Controls.Add(this.textBoxPreview);
        this.groupBoxNaming.Controls.Add(this.labelPreview);
        this.groupBoxNaming.Controls.Add(this.groupBoxSuffixOptions);
        this.groupBoxNaming.Controls.Add(this.groupBoxPrefixOptions);
        this.groupBoxNaming.Controls.Add(this.groupBoxFileReplace);
        this.groupBoxNaming.Controls.Add(this.textBoxFolderBaseName);
        this.groupBoxNaming.Controls.Add(this.labelFolderBaseName);
        this.groupBoxNaming.Controls.Add(this.textBoxFolderPrefix);
        this.groupBoxNaming.Controls.Add(this.labelFolderPrefix);
        this.groupBoxNaming.Controls.Add(this.textBoxFolderSuffix);
        this.groupBoxNaming.Controls.Add(this.labelFolderSuffix);
        this.groupBoxNaming.Controls.Add(this.groupBoxFolderPrefixOptions);
        this.groupBoxNaming.Controls.Add(this.groupBoxFolderSuffixOptions);
        this.groupBoxNaming.Location = new System.Drawing.Point(6, 178);
        this.groupBoxNaming.Name = "groupBoxNaming";
        this.groupBoxNaming.Size = new System.Drawing.Size(780, 300);
        this.groupBoxNaming.TabIndex = 2;
        this.groupBoxNaming.TabStop = false;
        this.groupBoxNaming.Text = "ファイル・フォルダ名設定";
        // 
        // labelPrefix
        // 
        this.labelPrefix.AutoSize = true;
        this.labelPrefix.Location = new System.Drawing.Point(6, 25);
        this.labelPrefix.Name = "labelPrefix";
        this.labelPrefix.Size = new System.Drawing.Size(140, 20);
        this.labelPrefix.TabIndex = 0;
        this.labelPrefix.Text = "ファイルプレフィックス:";
        // 
        // textBoxPrefix
        // 
        this.textBoxPrefix.Location = new System.Drawing.Point(150, 22);
        this.textBoxPrefix.Name = "textBoxPrefix";
        this.textBoxPrefix.Size = new System.Drawing.Size(150, 23);
        this.textBoxPrefix.TabIndex = 1;
        // 
        // groupBoxPrefixOptions
        // 
        this.groupBoxPrefixOptions.Controls.Add(this.checkBoxPrefixDateNone);
        this.groupBoxPrefixOptions.Controls.Add(this.checkBoxPrefixDateAfter);
        this.groupBoxPrefixOptions.Controls.Add(this.checkBoxPrefixDateBefore);
        this.groupBoxPrefixOptions.Location = new System.Drawing.Point(290, 15);
        this.groupBoxPrefixOptions.Name = "groupBoxPrefixOptions";
        this.groupBoxPrefixOptions.Size = new System.Drawing.Size(170, 40);
        this.groupBoxPrefixOptions.TabIndex = 2;
        this.groupBoxPrefixOptions.TabStop = false;
        this.groupBoxPrefixOptions.Text = "日付位置";
        // 
        // checkBoxPrefixDateBefore
        // 
        this.checkBoxPrefixDateBefore.AutoSize = true;
        this.checkBoxPrefixDateBefore.Location = new System.Drawing.Point(6, 18);
        this.checkBoxPrefixDateBefore.Name = "checkBoxPrefixDateBefore";
        this.checkBoxPrefixDateBefore.Size = new System.Drawing.Size(37, 19);
        this.checkBoxPrefixDateBefore.TabIndex = 0;
        this.checkBoxPrefixDateBefore.Text = "前";
        this.checkBoxPrefixDateBefore.UseVisualStyleBackColor = true;
        // 
        // checkBoxPrefixDateAfter
        // 
        this.checkBoxPrefixDateAfter.AutoSize = true;
        this.checkBoxPrefixDateAfter.Location = new System.Drawing.Point(60, 18);
        this.checkBoxPrefixDateAfter.Name = "checkBoxPrefixDateAfter";
        this.checkBoxPrefixDateAfter.Size = new System.Drawing.Size(37, 19);
        this.checkBoxPrefixDateAfter.TabIndex = 1;
        this.checkBoxPrefixDateAfter.Text = "後";
        this.checkBoxPrefixDateAfter.UseVisualStyleBackColor = true;
        // 
        // checkBoxPrefixDateNone
        // 
        this.checkBoxPrefixDateNone.AutoSize = true;
        this.checkBoxPrefixDateNone.Checked = true;
        this.checkBoxPrefixDateNone.Location = new System.Drawing.Point(114, 18);
        this.checkBoxPrefixDateNone.Name = "checkBoxPrefixDateNone";
        this.checkBoxPrefixDateNone.Size = new System.Drawing.Size(50, 19);
        this.checkBoxPrefixDateNone.TabIndex = 2;
        this.checkBoxPrefixDateNone.TabStop = true;
        this.checkBoxPrefixDateNone.Text = "なし";
        this.checkBoxPrefixDateNone.UseVisualStyleBackColor = true;
        // 
        // labelSuffix
        // 
        this.labelSuffix.AutoSize = true;
        this.labelSuffix.Location = new System.Drawing.Point(6, 70);
        this.labelSuffix.Name = "labelSuffix";
        this.labelSuffix.Size = new System.Drawing.Size(140, 20);
        this.labelSuffix.TabIndex = 3;
        this.labelSuffix.Text = "ファイルサフィックス:";
        // 
        // textBoxSuffix
        // 
        this.textBoxSuffix.Location = new System.Drawing.Point(150, 67);
        this.textBoxSuffix.Name = "textBoxSuffix";
        this.textBoxSuffix.Size = new System.Drawing.Size(150, 23);
        this.textBoxSuffix.TabIndex = 4;
        // 
        // groupBoxSuffixOptions
        // 
        this.groupBoxSuffixOptions.Controls.Add(this.checkBoxSuffixDateNone);
        this.groupBoxSuffixOptions.Controls.Add(this.checkBoxSuffixDateAfter);
        this.groupBoxSuffixOptions.Controls.Add(this.checkBoxSuffixDateBefore);
        this.groupBoxSuffixOptions.Location = new System.Drawing.Point(290, 60);
        this.groupBoxSuffixOptions.Name = "groupBoxSuffixOptions";
        this.groupBoxSuffixOptions.Size = new System.Drawing.Size(170, 40);
        this.groupBoxSuffixOptions.TabIndex = 5;
        this.groupBoxSuffixOptions.TabStop = false;
        this.groupBoxSuffixOptions.Text = "日付位置";
        // 
        // checkBoxSuffixDateBefore
        // 
        this.checkBoxSuffixDateBefore.AutoSize = true;
        this.checkBoxSuffixDateBefore.Location = new System.Drawing.Point(6, 18);
        this.checkBoxSuffixDateBefore.Name = "checkBoxSuffixDateBefore";
        this.checkBoxSuffixDateBefore.Size = new System.Drawing.Size(37, 19);
        this.checkBoxSuffixDateBefore.TabIndex = 0;
        this.checkBoxSuffixDateBefore.Text = "前";
        this.checkBoxSuffixDateBefore.UseVisualStyleBackColor = true;
        // 
        // checkBoxSuffixDateAfter
        // 
        this.checkBoxSuffixDateAfter.AutoSize = true;
        this.checkBoxSuffixDateAfter.Location = new System.Drawing.Point(60, 18);
        this.checkBoxSuffixDateAfter.Name = "checkBoxSuffixDateAfter";
        this.checkBoxSuffixDateAfter.Size = new System.Drawing.Size(37, 19);
        this.checkBoxSuffixDateAfter.TabIndex = 1;
        this.checkBoxSuffixDateAfter.Text = "後";
        this.checkBoxSuffixDateAfter.UseVisualStyleBackColor = true;
        // 
        // checkBoxSuffixDateNone
        // 
        this.checkBoxSuffixDateNone.AutoSize = true;
        this.checkBoxSuffixDateNone.Checked = true;
        this.checkBoxSuffixDateNone.Location = new System.Drawing.Point(114, 18);
        this.checkBoxSuffixDateNone.Name = "checkBoxSuffixDateNone";
        this.checkBoxSuffixDateNone.Size = new System.Drawing.Size(50, 19);
        this.checkBoxSuffixDateNone.TabIndex = 2;
        this.checkBoxSuffixDateNone.TabStop = true;
        this.checkBoxSuffixDateNone.Text = "なし";
        this.checkBoxSuffixDateNone.UseVisualStyleBackColor = true;
        // 
        // groupBoxFileReplace
        // 
        this.groupBoxFileReplace.Controls.Add(this.labelReplaceFrom);
        this.groupBoxFileReplace.Controls.Add(this.textBoxReplaceFrom);
        this.groupBoxFileReplace.Controls.Add(this.labelReplaceTo);
        this.groupBoxFileReplace.Controls.Add(this.textBoxReplaceTo);
        this.groupBoxFileReplace.Location = new System.Drawing.Point(480, 15);
        this.groupBoxFileReplace.Name = "groupBoxFileReplace";
        this.groupBoxFileReplace.Size = new System.Drawing.Size(280, 85);
        this.groupBoxFileReplace.TabIndex = 10;
        this.groupBoxFileReplace.TabStop = false;
        this.groupBoxFileReplace.Text = "ファイル名置換";
        // 
        // labelReplaceFrom
        // 
        this.labelReplaceFrom.AutoSize = true;
        this.labelReplaceFrom.Location = new System.Drawing.Point(6, 25);
        this.labelReplaceFrom.Name = "labelReplaceFrom";
        this.labelReplaceFrom.Size = new System.Drawing.Size(43, 15);
        this.labelReplaceFrom.TabIndex = 0;
        this.labelReplaceFrom.Text = "置換元:";
        // 
        // textBoxReplaceFrom
        // 
        this.textBoxReplaceFrom.Location = new System.Drawing.Point(55, 22);
        this.textBoxReplaceFrom.Name = "textBoxReplaceFrom";
        this.textBoxReplaceFrom.Size = new System.Drawing.Size(210, 23);
        this.textBoxReplaceFrom.TabIndex = 1;
        // 
        // labelReplaceTo
        // 
        this.labelReplaceTo.AutoSize = true;
        this.labelReplaceTo.Location = new System.Drawing.Point(6, 54);
        this.labelReplaceTo.Name = "labelReplaceTo";
        this.labelReplaceTo.Size = new System.Drawing.Size(43, 15);
        this.labelReplaceTo.TabIndex = 2;
        this.labelReplaceTo.Text = "置換先:";
        // 
        // textBoxReplaceTo
        // 
        this.textBoxReplaceTo.Location = new System.Drawing.Point(55, 51);
        this.textBoxReplaceTo.Name = "textBoxReplaceTo";
        this.textBoxReplaceTo.Size = new System.Drawing.Size(210, 23);
        this.textBoxReplaceTo.TabIndex = 3;
        // 
        // labelDateOffset
        // 
        this.labelDateOffset.AutoSize = true;
        this.labelDateOffset.Location = new System.Drawing.Point(6, 115);
        this.labelDateOffset.Name = "labelDateOffset";
        this.labelDateOffset.Size = new System.Drawing.Size(67, 15);
        this.labelDateOffset.TabIndex = 6;
        this.labelDateOffset.Text = "日付オフセット:";
        // 
        // numericUpDownDateOffset
        // 
        this.numericUpDownDateOffset.Location = new System.Drawing.Point(80, 112);
        this.numericUpDownDateOffset.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
        this.numericUpDownDateOffset.Minimum = new decimal(new int[] {
            365,
            0,
            0,
            -2147483648});
        this.numericUpDownDateOffset.Name = "numericUpDownDateOffset";
        this.numericUpDownDateOffset.Size = new System.Drawing.Size(120, 23);
        this.numericUpDownDateOffset.TabIndex = 7;
        // 
        // labelFolderBaseName
        // 
        this.labelFolderBaseName.AutoSize = true;
        this.labelFolderBaseName.Location = new System.Drawing.Point(250, 115);
        this.labelFolderBaseName.Name = "labelFolderBaseName";
        this.labelFolderBaseName.Size = new System.Drawing.Size(95, 20);
        this.labelFolderBaseName.TabIndex = 10;
        this.labelFolderBaseName.Text = "フォルダ名ベース:";
        // 
        // textBoxFolderBaseName
        // 
        this.textBoxFolderBaseName.Location = new System.Drawing.Point(340, 112);
        this.textBoxFolderBaseName.Name = "textBoxFolderBaseName";
        this.textBoxFolderBaseName.Size = new System.Drawing.Size(200, 23);
        this.textBoxFolderBaseName.TabIndex = 11;
        // 
        // labelFolderPrefix
        // 
        this.labelFolderPrefix.AutoSize = true;
        this.labelFolderPrefix.Location = new System.Drawing.Point(6, 160);
        this.labelFolderPrefix.Name = "labelFolderPrefix";
        this.labelFolderPrefix.Size = new System.Drawing.Size(89, 15);
        this.labelFolderPrefix.TabIndex = 12;
        this.labelFolderPrefix.Text = "フォルダプレフィックス:";
        // 
        // textBoxFolderPrefix
        // 
        this.textBoxFolderPrefix.Location = new System.Drawing.Point(110, 157);
        this.textBoxFolderPrefix.Name = "textBoxFolderPrefix";
        this.textBoxFolderPrefix.Size = new System.Drawing.Size(170, 23);
        this.textBoxFolderPrefix.TabIndex = 13;
        // 
        // groupBoxFolderPrefixOptions
        // 
        this.groupBoxFolderPrefixOptions.Controls.Add(this.checkBoxFolderPrefixDateNone);
        this.groupBoxFolderPrefixOptions.Controls.Add(this.checkBoxFolderPrefixDateAfter);
        this.groupBoxFolderPrefixOptions.Controls.Add(this.checkBoxFolderPrefixDateBefore);
        this.groupBoxFolderPrefixOptions.Location = new System.Drawing.Point(300, 150);
        this.groupBoxFolderPrefixOptions.Name = "groupBoxFolderPrefixOptions";
        this.groupBoxFolderPrefixOptions.Size = new System.Drawing.Size(250, 40);
        this.groupBoxFolderPrefixOptions.TabIndex = 14;
        this.groupBoxFolderPrefixOptions.TabStop = false;
        this.groupBoxFolderPrefixOptions.Text = "フォルダ日付位置";
        // 
        // checkBoxFolderPrefixDateBefore
        // 
        this.checkBoxFolderPrefixDateBefore.AutoSize = true;
        this.checkBoxFolderPrefixDateBefore.Location = new System.Drawing.Point(6, 18);
        this.checkBoxFolderPrefixDateBefore.Name = "checkBoxFolderPrefixDateBefore";
        this.checkBoxFolderPrefixDateBefore.Size = new System.Drawing.Size(37, 19);
        this.checkBoxFolderPrefixDateBefore.TabIndex = 0;
        this.checkBoxFolderPrefixDateBefore.Text = "前";
        this.checkBoxFolderPrefixDateBefore.UseVisualStyleBackColor = true;
        // 
        // checkBoxFolderPrefixDateAfter
        // 
        this.checkBoxFolderPrefixDateAfter.AutoSize = true;
        this.checkBoxFolderPrefixDateAfter.Location = new System.Drawing.Point(60, 18);
        this.checkBoxFolderPrefixDateAfter.Name = "checkBoxFolderPrefixDateAfter";
        this.checkBoxFolderPrefixDateAfter.Size = new System.Drawing.Size(37, 19);
        this.checkBoxFolderPrefixDateAfter.TabIndex = 1;
        this.checkBoxFolderPrefixDateAfter.Text = "後";
        this.checkBoxFolderPrefixDateAfter.UseVisualStyleBackColor = true;
        // 
        // checkBoxFolderPrefixDateNone
        // 
        this.checkBoxFolderPrefixDateNone.AutoSize = true;
        this.checkBoxFolderPrefixDateNone.Checked = true;
        this.checkBoxFolderPrefixDateNone.Location = new System.Drawing.Point(114, 18);
        this.checkBoxFolderPrefixDateNone.Name = "checkBoxFolderPrefixDateNone";
        this.checkBoxFolderPrefixDateNone.Size = new System.Drawing.Size(50, 19);
        this.checkBoxFolderPrefixDateNone.TabIndex = 2;
        this.checkBoxFolderPrefixDateNone.TabStop = true;
        this.checkBoxFolderPrefixDateNone.Text = "なし";
        this.checkBoxFolderPrefixDateNone.UseVisualStyleBackColor = true;
        // 
        // labelFolderSuffix
        // 
        this.labelFolderSuffix.AutoSize = true;
        this.labelFolderSuffix.Location = new System.Drawing.Point(6, 205);
        this.labelFolderSuffix.Name = "labelFolderSuffix";
        this.labelFolderSuffix.Size = new System.Drawing.Size(84, 15);
        this.labelFolderSuffix.TabIndex = 15;
        this.labelFolderSuffix.Text = "フォルダサフィックス:";
        // 
        // textBoxFolderSuffix
        // 
        this.textBoxFolderSuffix.Location = new System.Drawing.Point(110, 202);
        this.textBoxFolderSuffix.Name = "textBoxFolderSuffix";
        this.textBoxFolderSuffix.Size = new System.Drawing.Size(170, 23);
        this.textBoxFolderSuffix.TabIndex = 16;
        // 
        // groupBoxFolderSuffixOptions
        // 
        this.groupBoxFolderSuffixOptions.Controls.Add(this.checkBoxFolderSuffixDateNone);
        this.groupBoxFolderSuffixOptions.Controls.Add(this.checkBoxFolderSuffixDateAfter);
        this.groupBoxFolderSuffixOptions.Controls.Add(this.checkBoxFolderSuffixDateBefore);
        this.groupBoxFolderSuffixOptions.Location = new System.Drawing.Point(300, 195);
        this.groupBoxFolderSuffixOptions.Name = "groupBoxFolderSuffixOptions";
        this.groupBoxFolderSuffixOptions.Size = new System.Drawing.Size(250, 40);
        this.groupBoxFolderSuffixOptions.TabIndex = 17;
        this.groupBoxFolderSuffixOptions.TabStop = false;
        this.groupBoxFolderSuffixOptions.Text = "フォルダ日付位置";
        // 
        // checkBoxFolderSuffixDateBefore
        // 
        this.checkBoxFolderSuffixDateBefore.AutoSize = true;
        this.checkBoxFolderSuffixDateBefore.Location = new System.Drawing.Point(6, 18);
        this.checkBoxFolderSuffixDateBefore.Name = "checkBoxFolderSuffixDateBefore";
        this.checkBoxFolderSuffixDateBefore.Size = new System.Drawing.Size(37, 19);
        this.checkBoxFolderSuffixDateBefore.TabIndex = 0;
        this.checkBoxFolderSuffixDateBefore.Text = "前";
        this.checkBoxFolderSuffixDateBefore.UseVisualStyleBackColor = true;
        // 
        // checkBoxFolderSuffixDateAfter
        // 
        this.checkBoxFolderSuffixDateAfter.AutoSize = true;
        this.checkBoxFolderSuffixDateAfter.Location = new System.Drawing.Point(60, 18);
        this.checkBoxFolderSuffixDateAfter.Name = "checkBoxFolderSuffixDateAfter";
        this.checkBoxFolderSuffixDateAfter.Size = new System.Drawing.Size(37, 19);
        this.checkBoxFolderSuffixDateAfter.TabIndex = 1;
        this.checkBoxFolderSuffixDateAfter.Text = "後";
        this.checkBoxFolderSuffixDateAfter.UseVisualStyleBackColor = true;
        // 
        // checkBoxFolderSuffixDateNone
        // 
        this.checkBoxFolderSuffixDateNone.AutoSize = true;
        this.checkBoxFolderSuffixDateNone.Checked = true;
        this.checkBoxFolderSuffixDateNone.Location = new System.Drawing.Point(114, 18);
        this.checkBoxFolderSuffixDateNone.Name = "checkBoxFolderSuffixDateNone";
        this.checkBoxFolderSuffixDateNone.Size = new System.Drawing.Size(50, 19);
        this.checkBoxFolderSuffixDateNone.TabIndex = 2;
        this.checkBoxFolderSuffixDateNone.TabStop = true;
        this.checkBoxFolderSuffixDateNone.Text = "なし";
        this.checkBoxFolderSuffixDateNone.UseVisualStyleBackColor = true;
        // 
        // labelPreview
        // 
        this.labelPreview.AutoSize = true;
        this.labelPreview.Location = new System.Drawing.Point(6, 250);
        this.labelPreview.Name = "labelPreview";
        this.labelPreview.Size = new System.Drawing.Size(44, 15);
        this.labelPreview.TabIndex = 8;
        this.labelPreview.Text = "プレビュー:";
        // 
        // textBoxPreview
        // 
        this.textBoxPreview.BackColor = System.Drawing.SystemColors.Control;
        this.textBoxPreview.Location = new System.Drawing.Point(80, 247);
        this.textBoxPreview.Multiline = true;
        this.textBoxPreview.Name = "textBoxPreview";
        this.textBoxPreview.ReadOnly = true;
        this.textBoxPreview.Size = new System.Drawing.Size(690, 40);
        this.textBoxPreview.TabIndex = 9;
        // 
        // groupBoxSchedule
        // 
        this.groupBoxSchedule.Controls.Add(this.comboBoxScheduleType);
        this.groupBoxSchedule.Controls.Add(this.labelScheduleType);
        this.groupBoxSchedule.Controls.Add(this.dateTimePickerStart);
        this.groupBoxSchedule.Controls.Add(this.labelStartTime);
        this.groupBoxSchedule.Controls.Add(this.textBoxTaskName);
        this.groupBoxSchedule.Controls.Add(this.labelTaskName);
        this.groupBoxSchedule.Location = new System.Drawing.Point(6, 484);
        this.groupBoxSchedule.Name = "groupBoxSchedule";
        this.groupBoxSchedule.Size = new System.Drawing.Size(780, 100);
        this.groupBoxSchedule.TabIndex = 3;
        this.groupBoxSchedule.TabStop = false;
        this.groupBoxSchedule.Text = "スケジュール設定";
        // 
        // labelTaskName
        // 
        this.labelTaskName.AutoSize = true;
        this.labelTaskName.Location = new System.Drawing.Point(6, 25);
        this.labelTaskName.Name = "labelTaskName";
        this.labelTaskName.Size = new System.Drawing.Size(44, 15);
        this.labelTaskName.TabIndex = 0;
        this.labelTaskName.Text = "タスク名:";
        // 
        // textBoxTaskName
        // 
        this.textBoxTaskName.Location = new System.Drawing.Point(80, 22);
        this.textBoxTaskName.Name = "textBoxTaskName";
        this.textBoxTaskName.Size = new System.Drawing.Size(300, 23);
        this.textBoxTaskName.TabIndex = 1;
        // 
        // labelStartTime
        // 
        this.labelStartTime.AutoSize = true;
        this.labelStartTime.Location = new System.Drawing.Point(6, 60);
        this.labelStartTime.Name = "labelStartTime";
        this.labelStartTime.Size = new System.Drawing.Size(55, 15);
        this.labelStartTime.TabIndex = 2;
        this.labelStartTime.Text = "開始時刻:";
        // 
        // dateTimePickerStart
        // 
        this.dateTimePickerStart.CustomFormat = "yyyy/MM/dd HH:mm";
        this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dateTimePickerStart.Location = new System.Drawing.Point(80, 57);
        this.dateTimePickerStart.Name = "dateTimePickerStart";
        this.dateTimePickerStart.Size = new System.Drawing.Size(150, 23);
        this.dateTimePickerStart.TabIndex = 3;
        // 
        // labelScheduleType
        // 
        this.labelScheduleType.AutoSize = true;
        this.labelScheduleType.Location = new System.Drawing.Point(250, 60);
        this.labelScheduleType.Name = "labelScheduleType";
        this.labelScheduleType.Size = new System.Drawing.Size(67, 15);
        this.labelScheduleType.TabIndex = 4;
        this.labelScheduleType.Text = "実行タイプ:";
        // 
        // comboBoxScheduleType
        // 
        this.comboBoxScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxScheduleType.FormattingEnabled = true;
        this.comboBoxScheduleType.Items.AddRange(new object[] {
            "一回のみ",
            "毎日",
            "毎週",
            "毎月"});
        this.comboBoxScheduleType.Location = new System.Drawing.Point(323, 57);
        this.comboBoxScheduleType.Name = "comboBoxScheduleType";
        this.comboBoxScheduleType.Size = new System.Drawing.Size(121, 23);
        this.comboBoxScheduleType.TabIndex = 5;
        // 
        // buttonCreateTask
        // 
        this.buttonCreateTask.Location = new System.Drawing.Point(711, 530);
        this.buttonCreateTask.Name = "buttonCreateTask";
        this.buttonCreateTask.Size = new System.Drawing.Size(75, 30);
        this.buttonCreateTask.TabIndex = 4;
        this.buttonCreateTask.Text = "作成";
        this.buttonCreateTask.UseVisualStyleBackColor = true;
        // 
        // tabPageManage
        // 
        this.tabPageManage.Controls.Add(this.buttonOpenTaskScheduler);
        this.tabPageManage.Controls.Add(this.buttonEditTask);
        this.tabPageManage.Controls.Add(this.buttonDeleteTask);
        this.tabPageManage.Controls.Add(this.buttonRefreshTasks);
        this.tabPageManage.Controls.Add(this.listViewTasks);
        this.tabPageManage.Location = new System.Drawing.Point(4, 24);
        this.tabPageManage.Name = "tabPageManage";
        this.tabPageManage.Padding = new System.Windows.Forms.Padding(3);
        this.tabPageManage.Size = new System.Drawing.Size(792, 572);
        this.tabPageManage.TabIndex = 1;
        this.tabPageManage.Text = "タスク管理";
        this.tabPageManage.UseVisualStyleBackColor = true;
        // 
        // listViewTasks
        // 
        this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderStatus,
            this.columnHeaderNextRun,
            this.columnHeaderLastRun});
        this.listViewTasks.FullRowSelect = true;
        this.listViewTasks.GridLines = true;
        this.listViewTasks.Location = new System.Drawing.Point(6, 6);
        this.listViewTasks.Name = "listViewTasks";
        this.listViewTasks.Size = new System.Drawing.Size(780, 500);
        this.listViewTasks.TabIndex = 0;
        this.listViewTasks.UseCompatibleStateImageBehavior = false;
        this.listViewTasks.View = System.Windows.Forms.View.Details;
        // 
        // columnHeaderName
        // 
        this.columnHeaderName.Text = "タスク名";
        this.columnHeaderName.Width = 200;
        // 
        // columnHeaderStatus
        // 
        this.columnHeaderStatus.Text = "状態";
        this.columnHeaderStatus.Width = 100;
        // 
        // columnHeaderNextRun
        // 
        this.columnHeaderNextRun.Text = "次回実行";
        this.columnHeaderNextRun.Width = 150;
        // 
        // columnHeaderLastRun
        // 
        this.columnHeaderLastRun.Text = "前回実行";
        this.columnHeaderLastRun.Width = 150;
        // 
        // buttonRefreshTasks
        // 
        this.buttonRefreshTasks.Location = new System.Drawing.Point(6, 515);
        this.buttonRefreshTasks.Name = "buttonRefreshTasks";
        this.buttonRefreshTasks.Size = new System.Drawing.Size(75, 30);
        this.buttonRefreshTasks.TabIndex = 1;
        this.buttonRefreshTasks.Text = "更新";
        this.buttonRefreshTasks.UseVisualStyleBackColor = true;
        // 
        // buttonDeleteTask
        // 
        this.buttonDeleteTask.Location = new System.Drawing.Point(87, 515);
        this.buttonDeleteTask.Name = "buttonDeleteTask";
        this.buttonDeleteTask.Size = new System.Drawing.Size(75, 30);
        this.buttonDeleteTask.TabIndex = 2;
        this.buttonDeleteTask.Text = "削除";
        this.buttonDeleteTask.UseVisualStyleBackColor = true;
        // 
        // buttonEditTask
        // 
        this.buttonEditTask.Location = new System.Drawing.Point(168, 515);
        this.buttonEditTask.Name = "buttonEditTask";
        this.buttonEditTask.Size = new System.Drawing.Size(75, 30);
        this.buttonEditTask.TabIndex = 3;
        this.buttonEditTask.Text = "編集";
        this.buttonEditTask.UseVisualStyleBackColor = true;
        // 
        // buttonOpenTaskScheduler
        // 
        this.buttonOpenTaskScheduler.Location = new System.Drawing.Point(249, 515);
        this.buttonOpenTaskScheduler.Name = "buttonOpenTaskScheduler";
        this.buttonOpenTaskScheduler.Size = new System.Drawing.Size(140, 30);
        this.buttonOpenTaskScheduler.TabIndex = 4;
        this.buttonOpenTaskScheduler.Text = "タスクスケジューラを開く";
        this.buttonOpenTaskScheduler.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 700);
        this.Controls.Add(this.tabControl1);
        this.Name = "MainForm";
        this.Text = "TaskCrony - タスク自動化ツール";
        this.tabControl1.ResumeLayout(false);
        this.tabPageCreate.ResumeLayout(false);
        this.groupBoxOptions.ResumeLayout(false);
        this.groupBoxOptions.PerformLayout();
        this.groupBoxFiles.ResumeLayout(false);
        this.groupBoxFiles.PerformLayout();
        this.groupBoxNaming.ResumeLayout(false);
        this.groupBoxNaming.PerformLayout();
        this.groupBoxPrefixOptions.ResumeLayout(false);
        this.groupBoxPrefixOptions.PerformLayout();
        this.groupBoxSuffixOptions.ResumeLayout(false);
        this.groupBoxSuffixOptions.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDateOffset)).EndInit();
        this.groupBoxSchedule.ResumeLayout(false);
        this.groupBoxSchedule.PerformLayout();
        this.tabPageManage.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPageCreate;
    private System.Windows.Forms.TabPage tabPageManage;
    private System.Windows.Forms.GroupBox groupBoxOptions;
    private System.Windows.Forms.CheckBox checkBoxCreateFile;
    private System.Windows.Forms.CheckBox checkBoxCreateFolder;
    private System.Windows.Forms.GroupBox groupBoxFiles;
    private System.Windows.Forms.Button buttonBrowseSource;
    private System.Windows.Forms.Button buttonBrowseDestination;
    private System.Windows.Forms.TextBox textBoxSourcePath;
    private System.Windows.Forms.TextBox textBoxDestinationPath;
    private System.Windows.Forms.Label labelSourcePath;
    private System.Windows.Forms.Label labelDestinationPath;
    private System.Windows.Forms.GroupBox groupBoxNaming;
    private System.Windows.Forms.TextBox textBoxPrefix;
    private System.Windows.Forms.TextBox textBoxSuffix;
    private System.Windows.Forms.Label labelPrefix;
    private System.Windows.Forms.Label labelSuffix;
    private System.Windows.Forms.NumericUpDown numericUpDownDateOffset;
    private System.Windows.Forms.Label labelDateOffset;
    private System.Windows.Forms.TextBox textBoxPreview;
    private System.Windows.Forms.Label labelPreview;
    private System.Windows.Forms.GroupBox groupBoxPrefixOptions;
    private System.Windows.Forms.RadioButton checkBoxPrefixDateAfter;
    private System.Windows.Forms.RadioButton checkBoxPrefixDateBefore;
    private System.Windows.Forms.RadioButton checkBoxPrefixDateNone;
    private System.Windows.Forms.GroupBox groupBoxSuffixOptions;
    private System.Windows.Forms.RadioButton checkBoxSuffixDateAfter;
    private System.Windows.Forms.RadioButton checkBoxSuffixDateBefore;
    private System.Windows.Forms.RadioButton checkBoxSuffixDateNone;
    private System.Windows.Forms.GroupBox groupBoxSchedule;
    private System.Windows.Forms.ComboBox comboBoxScheduleType;
    private System.Windows.Forms.Label labelScheduleType;
    private System.Windows.Forms.DateTimePicker dateTimePickerStart;
    private System.Windows.Forms.Label labelStartTime;
    private System.Windows.Forms.TextBox textBoxTaskName;
    private System.Windows.Forms.Label labelTaskName;
    private System.Windows.Forms.Button buttonCreateTask;
    private System.Windows.Forms.ListView listViewTasks;
    private System.Windows.Forms.ColumnHeader columnHeaderName;
    private System.Windows.Forms.ColumnHeader columnHeaderStatus;
    private System.Windows.Forms.ColumnHeader columnHeaderLastRun;
    private System.Windows.Forms.ColumnHeader columnHeaderNextRun;
    private System.Windows.Forms.Button buttonRefreshTasks;
    private System.Windows.Forms.Button buttonDeleteTask;
    private System.Windows.Forms.Button buttonEditTask;
    private System.Windows.Forms.Button buttonOpenTaskScheduler;
    private System.Windows.Forms.TextBox textBoxFolderBaseName;
    private System.Windows.Forms.Label labelFolderBaseName;
    private System.Windows.Forms.TextBox textBoxFolderPrefix;
    private System.Windows.Forms.Label labelFolderPrefix;
    private System.Windows.Forms.TextBox textBoxFolderSuffix;
    private System.Windows.Forms.Label labelFolderSuffix;
    private System.Windows.Forms.GroupBox groupBoxFolderPrefixOptions;
    private System.Windows.Forms.RadioButton checkBoxFolderPrefixDateBefore;
    private System.Windows.Forms.RadioButton checkBoxFolderPrefixDateAfter;
    private System.Windows.Forms.RadioButton checkBoxFolderPrefixDateNone;
    private System.Windows.Forms.GroupBox groupBoxFolderSuffixOptions;
    private System.Windows.Forms.RadioButton checkBoxFolderSuffixDateBefore;
    private System.Windows.Forms.RadioButton checkBoxFolderSuffixDateAfter;
    private System.Windows.Forms.RadioButton checkBoxFolderSuffixDateNone;
    private System.Windows.Forms.GroupBox groupBoxFileReplace;
    private System.Windows.Forms.Label labelReplaceFrom;
    private System.Windows.Forms.TextBox textBoxReplaceFrom;
    private System.Windows.Forms.Label labelReplaceTo;
    private System.Windows.Forms.TextBox textBoxReplaceTo;
}
