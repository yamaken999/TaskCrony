namespace TaskCrony
{
    partial class TaskEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTaskName = new System.Windows.Forms.TextBox();
            this.labelTaskName = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.textBoxSourceFolder = new System.Windows.Forms.TextBox();
            this.labelSourceFolder = new System.Windows.Forms.Label();
            this.buttonSourceFolderBrowse = new System.Windows.Forms.Button();
            this.textBoxDestinationFolder = new System.Windows.Forms.TextBox();
            this.labelDestinationFolder = new System.Windows.Forms.Label();
            this.buttonDestinationFolderBrowse = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.labelFileName = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.labelPrefix = new System.Windows.Forms.Label();
            this.textBoxSuffix = new System.Windows.Forms.TextBox();
            this.labelSuffix = new System.Windows.Forms.Label();
            this.checkBoxEnableTask = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyFiles = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateFolder = new System.Windows.Forms.CheckBox();
            this.comboBoxInterval = new System.Windows.Forms.ComboBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.textBoxDateOffset = new System.Windows.Forms.TextBox();
            this.labelDateOffset = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxPreview = new System.Windows.Forms.TextBox();
            this.labelPreview = new System.Windows.Forms.Label();
            this.textBoxFolderBaseName = new System.Windows.Forms.TextBox();
            this.labelFolderBaseName = new System.Windows.Forms.Label();
            this.textBoxFolderPrefix = new System.Windows.Forms.TextBox();
            this.labelFolderPrefix = new System.Windows.Forms.Label();
            this.textBoxFolderSuffix = new System.Windows.Forms.TextBox();
            this.labelFolderSuffix = new System.Windows.Forms.Label();
            this.groupBoxPrefixOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxPrefixDateBefore = new System.Windows.Forms.RadioButton();
            this.checkBoxPrefixDateAfter = new System.Windows.Forms.RadioButton();
            this.groupBoxSuffixOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxSuffixDateBefore = new System.Windows.Forms.RadioButton();
            this.checkBoxSuffixDateAfter = new System.Windows.Forms.RadioButton();
            this.groupBoxFolderPrefixOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxFolderPrefixDateBefore = new System.Windows.Forms.RadioButton();
            this.checkBoxFolderPrefixDateAfter = new System.Windows.Forms.RadioButton();
            this.groupBoxFolderSuffixOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxFolderSuffixDateBefore = new System.Windows.Forms.RadioButton();
            this.checkBoxFolderSuffixDateAfter = new System.Windows.Forms.RadioButton();
            this.groupBoxFileReplace = new System.Windows.Forms.GroupBox();
            this.labelReplaceFrom = new System.Windows.Forms.Label();
            this.textBoxReplaceFrom = new System.Windows.Forms.TextBox();
            this.labelReplaceTo = new System.Windows.Forms.Label();
            this.textBoxReplaceTo = new System.Windows.Forms.TextBox();
            this.groupBoxPrefixOptions.SuspendLayout();
            this.groupBoxSuffixOptions.SuspendLayout();
            this.groupBoxFolderPrefixOptions.SuspendLayout();
            this.groupBoxFolderSuffixOptions.SuspendLayout();
            this.groupBoxFileReplace.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxTaskName
            // 
            this.textBoxTaskName.Location = new System.Drawing.Point(150, 15);
            this.textBoxTaskName.Name = "textBoxTaskName";
            this.textBoxTaskName.Size = new System.Drawing.Size(300, 27);
            this.textBoxTaskName.TabIndex = 1;
            // 
            // labelTaskName
            // 
            this.labelTaskName.AutoSize = true;
            this.labelTaskName.Location = new System.Drawing.Point(15, 18);
            this.labelTaskName.Name = "labelTaskName";
            this.labelTaskName.Size = new System.Drawing.Size(67, 20);
            this.labelTaskName.TabIndex = 0;
            this.labelTaskName.Text = "タスク名:";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(150, 55);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(150, 27);
            this.textBoxTime.TabIndex = 3;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(15, 58);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(67, 20);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "実行時間:";
            // 
            // textBoxSourceFolder
            // 
            this.textBoxSourceFolder.Location = new System.Drawing.Point(150, 95);
            this.textBoxSourceFolder.Name = "textBoxSourceFolder";
            this.textBoxSourceFolder.Size = new System.Drawing.Size(450, 27);
            this.textBoxSourceFolder.TabIndex = 5;
            // 
            // labelSourceFolder
            // 
            this.labelSourceFolder.AutoSize = true;
            this.labelSourceFolder.Location = new System.Drawing.Point(15, 98);
            this.labelSourceFolder.Name = "labelSourceFolder";
            this.labelSourceFolder.Size = new System.Drawing.Size(91, 20);
            this.labelSourceFolder.TabIndex = 4;
            this.labelSourceFolder.Text = "コピー元フォルダ:";
            // 
            // buttonSourceFolderBrowse
            // 
            this.buttonSourceFolderBrowse.Location = new System.Drawing.Point(610, 95);
            this.buttonSourceFolderBrowse.Name = "buttonSourceFolderBrowse";
            this.buttonSourceFolderBrowse.Size = new System.Drawing.Size(75, 27);
            this.buttonSourceFolderBrowse.TabIndex = 6;
            this.buttonSourceFolderBrowse.Text = "参照...";
            this.buttonSourceFolderBrowse.UseVisualStyleBackColor = true;
            this.buttonSourceFolderBrowse.Click += new System.EventHandler(this.buttonSourceFolderBrowse_Click);
            // 
            // textBoxDestinationFolder
            // 
            this.textBoxDestinationFolder.Location = new System.Drawing.Point(150, 135);
            this.textBoxDestinationFolder.Name = "textBoxDestinationFolder";
            this.textBoxDestinationFolder.Size = new System.Drawing.Size(450, 27);
            this.textBoxDestinationFolder.TabIndex = 8;
            // 
            // labelDestinationFolder
            // 
            this.labelDestinationFolder.AutoSize = true;
            this.labelDestinationFolder.Location = new System.Drawing.Point(15, 138);
            this.labelDestinationFolder.Name = "labelDestinationFolder";
            this.labelDestinationFolder.Size = new System.Drawing.Size(91, 20);
            this.labelDestinationFolder.TabIndex = 7;
            this.labelDestinationFolder.Text = "コピー先フォルダ:";
            // 
            // buttonDestinationFolderBrowse
            // 
            this.buttonDestinationFolderBrowse.Location = new System.Drawing.Point(610, 135);
            this.buttonDestinationFolderBrowse.Name = "buttonDestinationFolderBrowse";
            this.buttonDestinationFolderBrowse.Size = new System.Drawing.Size(75, 27);
            this.buttonDestinationFolderBrowse.TabIndex = 9;
            this.buttonDestinationFolderBrowse.Text = "参照...";
            this.buttonDestinationFolderBrowse.UseVisualStyleBackColor = true;
            this.buttonDestinationFolderBrowse.Click += new System.EventHandler(this.buttonDestinationFolderBrowse_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(150, 175);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(300, 27);
            this.textBoxFileName.TabIndex = 11;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.textBoxFileName_TextChanged);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(15, 178);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(103, 20);
            this.labelFileName.TabIndex = 10;
            this.labelFileName.Text = "ファイル名ベース:";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(150, 215);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(200, 27);
            this.textBoxPrefix.TabIndex = 13;
            this.textBoxPrefix.TextChanged += new System.EventHandler(this.textBoxPrefix_TextChanged);
            // 
            // labelPrefix
            // 
            this.labelPrefix.AutoSize = true;
            this.labelPrefix.Location = new System.Drawing.Point(15, 218);
            this.labelPrefix.Name = "labelPrefix";
            this.labelPrefix.Size = new System.Drawing.Size(127, 20);
            this.labelPrefix.TabIndex = 12;
            this.labelPrefix.Text = "ファイル名プレフィックス:";
            // 
            // textBoxSuffix
            // 
            this.textBoxSuffix.Location = new System.Drawing.Point(150, 255);
            this.textBoxSuffix.Name = "textBoxSuffix";
            this.textBoxSuffix.Size = new System.Drawing.Size(200, 27);
            this.textBoxSuffix.TabIndex = 15;
            this.textBoxSuffix.TextChanged += new System.EventHandler(this.textBoxSuffix_TextChanged);
            // 
            // labelSuffix
            // 
            this.labelSuffix.AutoSize = true;
            this.labelSuffix.Location = new System.Drawing.Point(15, 258);
            this.labelSuffix.Name = "labelSuffix";
            this.labelSuffix.Size = new System.Drawing.Size(123, 20);
            this.labelSuffix.TabIndex = 14;
            this.labelSuffix.Text = "ファイル名サフィックス:";
            // 
            // textBoxFolderBaseName
            // 
            this.textBoxFolderBaseName.Location = new System.Drawing.Point(150, 335);
            this.textBoxFolderBaseName.Name = "textBoxFolderBaseName";
            this.textBoxFolderBaseName.Size = new System.Drawing.Size(300, 27);
            this.textBoxFolderBaseName.TabIndex = 21;
            this.textBoxFolderBaseName.TextChanged += new System.EventHandler(this.textBoxFolderBaseName_TextChanged);
            // 
            // labelFolderBaseName
            // 
            this.labelFolderBaseName.AutoSize = true;
            this.labelFolderBaseName.Location = new System.Drawing.Point(15, 338);
            this.labelFolderBaseName.Name = "labelFolderBaseName";
            this.labelFolderBaseName.Size = new System.Drawing.Size(107, 20);
            this.labelFolderBaseName.TabIndex = 20;
            this.labelFolderBaseName.Text = "フォルダ名ベース:";
            // 
            // textBoxFolderPrefix
            // 
            this.textBoxFolderPrefix.Location = new System.Drawing.Point(150, 375);
            this.textBoxFolderPrefix.Name = "textBoxFolderPrefix";
            this.textBoxFolderPrefix.Size = new System.Drawing.Size(200, 27);
            this.textBoxFolderPrefix.TabIndex = 23;
            this.textBoxFolderPrefix.TextChanged += new System.EventHandler(this.textBoxFolderPrefix_TextChanged);
            // 
            // labelFolderPrefix
            // 
            this.labelFolderPrefix.AutoSize = true;
            this.labelFolderPrefix.Location = new System.Drawing.Point(15, 378);
            this.labelFolderPrefix.Name = "labelFolderPrefix";
            this.labelFolderPrefix.Size = new System.Drawing.Size(131, 20);
            this.labelFolderPrefix.TabIndex = 22;
            this.labelFolderPrefix.Text = "フォルダ名プレフィックス:";
            // 
            // textBoxFolderSuffix
            // 
            this.textBoxFolderSuffix.Location = new System.Drawing.Point(150, 415);
            this.textBoxFolderSuffix.Name = "textBoxFolderSuffix";
            this.textBoxFolderSuffix.Size = new System.Drawing.Size(200, 27);
            this.textBoxFolderSuffix.TabIndex = 25;
            this.textBoxFolderSuffix.TextChanged += new System.EventHandler(this.textBoxFolderSuffix_TextChanged);
            // 
            // labelFolderSuffix
            // 
            this.labelFolderSuffix.AutoSize = true;
            this.labelFolderSuffix.Location = new System.Drawing.Point(15, 418);
            this.labelFolderSuffix.Name = "labelFolderSuffix";
            this.labelFolderSuffix.Size = new System.Drawing.Size(127, 20);
            this.labelFolderSuffix.TabIndex = 24;
            this.labelFolderSuffix.Text = "フォルダ名サフィックス:";
            // 
            // groupBoxPrefixOptions
            // 
            this.groupBoxPrefixOptions.Controls.Add(this.checkBoxPrefixDateBefore);
            this.groupBoxPrefixOptions.Controls.Add(this.checkBoxPrefixDateAfter);
            this.groupBoxPrefixOptions.Location = new System.Drawing.Point(370, 215);
            this.groupBoxPrefixOptions.Name = "groupBoxPrefixOptions";
            this.groupBoxPrefixOptions.Size = new System.Drawing.Size(200, 30);
            this.groupBoxPrefixOptions.TabIndex = 16;
            this.groupBoxPrefixOptions.TabStop = false;
            // 
            // checkBoxPrefixDateBefore
            // 
            this.checkBoxPrefixDateBefore.AutoSize = true;
            this.checkBoxPrefixDateBefore.Location = new System.Drawing.Point(10, 8);
            this.checkBoxPrefixDateBefore.Name = "checkBoxPrefixDateBefore";
            this.checkBoxPrefixDateBefore.Size = new System.Drawing.Size(65, 24);
            this.checkBoxPrefixDateBefore.TabIndex = 0;
            this.checkBoxPrefixDateBefore.Text = "前置";
            this.checkBoxPrefixDateBefore.UseVisualStyleBackColor = true;
            this.checkBoxPrefixDateBefore.CheckedChanged += new System.EventHandler(this.checkBoxPrefixDateBefore_CheckedChanged);
            // 
            // checkBoxPrefixDateAfter
            // 
            this.checkBoxPrefixDateAfter.AutoSize = true;
            this.checkBoxPrefixDateAfter.Checked = true;
            this.checkBoxPrefixDateAfter.Location = new System.Drawing.Point(85, 8);
            this.checkBoxPrefixDateAfter.Name = "checkBoxPrefixDateAfter";
            this.checkBoxPrefixDateAfter.Size = new System.Drawing.Size(65, 24);
            this.checkBoxPrefixDateAfter.TabIndex = 1;
            this.checkBoxPrefixDateAfter.TabStop = true;
            this.checkBoxPrefixDateAfter.Text = "後置";
            this.checkBoxPrefixDateAfter.UseVisualStyleBackColor = true;
            this.checkBoxPrefixDateAfter.CheckedChanged += new System.EventHandler(this.checkBoxPrefixDateAfter_CheckedChanged);
            // 
            // groupBoxSuffixOptions
            // 
            this.groupBoxSuffixOptions.Controls.Add(this.checkBoxSuffixDateBefore);
            this.groupBoxSuffixOptions.Controls.Add(this.checkBoxSuffixDateAfter);
            this.groupBoxSuffixOptions.Location = new System.Drawing.Point(370, 255);
            this.groupBoxSuffixOptions.Name = "groupBoxSuffixOptions";
            this.groupBoxSuffixOptions.Size = new System.Drawing.Size(200, 30);
            this.groupBoxSuffixOptions.TabIndex = 17;
            this.groupBoxSuffixOptions.TabStop = false;
            // 
            // checkBoxSuffixDateBefore
            // 
            this.checkBoxSuffixDateBefore.AutoSize = true;
            this.checkBoxSuffixDateBefore.Checked = true;
            this.checkBoxSuffixDateBefore.Location = new System.Drawing.Point(10, 8);
            this.checkBoxSuffixDateBefore.Name = "checkBoxSuffixDateBefore";
            this.checkBoxSuffixDateBefore.Size = new System.Drawing.Size(65, 24);
            this.checkBoxSuffixDateBefore.TabIndex = 0;
            this.checkBoxSuffixDateBefore.TabStop = true;
            this.checkBoxSuffixDateBefore.Text = "前置";
            this.checkBoxSuffixDateBefore.UseVisualStyleBackColor = true;
            this.checkBoxSuffixDateBefore.CheckedChanged += new System.EventHandler(this.checkBoxSuffixDateBefore_CheckedChanged);
            // 
            // checkBoxSuffixDateAfter
            // 
            this.checkBoxSuffixDateAfter.AutoSize = true;
            this.checkBoxSuffixDateAfter.Location = new System.Drawing.Point(85, 8);
            this.checkBoxSuffixDateAfter.Name = "checkBoxSuffixDateAfter";
            this.checkBoxSuffixDateAfter.Size = new System.Drawing.Size(65, 24);
            this.checkBoxSuffixDateAfter.TabIndex = 1;
            this.checkBoxSuffixDateAfter.Text = "後置";
            this.checkBoxSuffixDateAfter.UseVisualStyleBackColor = true;
            this.checkBoxSuffixDateAfter.CheckedChanged += new System.EventHandler(this.checkBoxSuffixDateAfter_CheckedChanged);
            // 
            // groupBoxFolderPrefixOptions
            // 
            this.groupBoxFolderPrefixOptions.Controls.Add(this.checkBoxFolderPrefixDateBefore);
            this.groupBoxFolderPrefixOptions.Controls.Add(this.checkBoxFolderPrefixDateAfter);
            this.groupBoxFolderPrefixOptions.Location = new System.Drawing.Point(370, 375);
            this.groupBoxFolderPrefixOptions.Name = "groupBoxFolderPrefixOptions";
            this.groupBoxFolderPrefixOptions.Size = new System.Drawing.Size(200, 30);
            this.groupBoxFolderPrefixOptions.TabIndex = 26;
            this.groupBoxFolderPrefixOptions.TabStop = false;
            // 
            // checkBoxFolderPrefixDateBefore
            // 
            this.checkBoxFolderPrefixDateBefore.AutoSize = true;
            this.checkBoxFolderPrefixDateBefore.Location = new System.Drawing.Point(10, 8);
            this.checkBoxFolderPrefixDateBefore.Name = "checkBoxFolderPrefixDateBefore";
            this.checkBoxFolderPrefixDateBefore.Size = new System.Drawing.Size(65, 24);
            this.checkBoxFolderPrefixDateBefore.TabIndex = 0;
            this.checkBoxFolderPrefixDateBefore.Text = "前置";
            this.checkBoxFolderPrefixDateBefore.UseVisualStyleBackColor = true;
            this.checkBoxFolderPrefixDateBefore.CheckedChanged += new System.EventHandler(this.checkBoxFolderPrefixDateBefore_CheckedChanged);
            // 
            // checkBoxFolderPrefixDateAfter
            // 
            this.checkBoxFolderPrefixDateAfter.AutoSize = true;
            this.checkBoxFolderPrefixDateAfter.Checked = true;
            this.checkBoxFolderPrefixDateAfter.Location = new System.Drawing.Point(85, 8);
            this.checkBoxFolderPrefixDateAfter.Name = "checkBoxFolderPrefixDateAfter";
            this.checkBoxFolderPrefixDateAfter.Size = new System.Drawing.Size(65, 24);
            this.checkBoxFolderPrefixDateAfter.TabIndex = 1;
            this.checkBoxFolderPrefixDateAfter.TabStop = true;
            this.checkBoxFolderPrefixDateAfter.Text = "後置";
            this.checkBoxFolderPrefixDateAfter.UseVisualStyleBackColor = true;
            this.checkBoxFolderPrefixDateAfter.CheckedChanged += new System.EventHandler(this.checkBoxFolderPrefixDateAfter_CheckedChanged);
            // 
            // groupBoxFolderSuffixOptions
            // 
            this.groupBoxFolderSuffixOptions.Controls.Add(this.checkBoxFolderSuffixDateBefore);
            this.groupBoxFolderSuffixOptions.Controls.Add(this.checkBoxFolderSuffixDateAfter);
            this.groupBoxFolderSuffixOptions.Location = new System.Drawing.Point(370, 415);
            this.groupBoxFolderSuffixOptions.Name = "groupBoxFolderSuffixOptions";
            this.groupBoxFolderSuffixOptions.Size = new System.Drawing.Size(200, 30);
            this.groupBoxFolderSuffixOptions.TabIndex = 27;
            this.groupBoxFolderSuffixOptions.TabStop = false;
            // 
            // checkBoxFolderSuffixDateBefore
            // 
            this.checkBoxFolderSuffixDateBefore.AutoSize = true;
            this.checkBoxFolderSuffixDateBefore.Checked = true;
            this.checkBoxFolderSuffixDateBefore.Location = new System.Drawing.Point(10, 8);
            this.checkBoxFolderSuffixDateBefore.Name = "checkBoxFolderSuffixDateBefore";
            this.checkBoxFolderSuffixDateBefore.Size = new System.Drawing.Size(65, 24);
            this.checkBoxFolderSuffixDateBefore.TabIndex = 0;
            this.checkBoxFolderSuffixDateBefore.TabStop = true;
            this.checkBoxFolderSuffixDateBefore.Text = "前置";
            this.checkBoxFolderSuffixDateBefore.UseVisualStyleBackColor = true;
            this.checkBoxFolderSuffixDateBefore.CheckedChanged += new System.EventHandler(this.checkBoxFolderSuffixDateBefore_CheckedChanged);
            // 
            // checkBoxFolderSuffixDateAfter
            // 
            this.checkBoxFolderSuffixDateAfter.AutoSize = true;
            this.checkBoxFolderSuffixDateAfter.Location = new System.Drawing.Point(85, 8);
            this.checkBoxFolderSuffixDateAfter.Name = "checkBoxFolderSuffixDateAfter";
            this.checkBoxFolderSuffixDateAfter.Size = new System.Drawing.Size(65, 24);
            this.checkBoxFolderSuffixDateAfter.TabIndex = 1;
            this.checkBoxFolderSuffixDateAfter.Text = "後置";
            this.checkBoxFolderSuffixDateAfter.UseVisualStyleBackColor = true;
            this.checkBoxFolderSuffixDateAfter.CheckedChanged += new System.EventHandler(this.checkBoxFolderSuffixDateAfter_CheckedChanged);
            // 
            // checkBoxEnableTask
            // 
            this.checkBoxEnableTask.AutoSize = true;
            this.checkBoxEnableTask.Location = new System.Drawing.Point(15, 295);
            this.checkBoxEnableTask.Name = "checkBoxEnableTask";
            this.checkBoxEnableTask.Size = new System.Drawing.Size(117, 24);
            this.checkBoxEnableTask.TabIndex = 18;
            this.checkBoxEnableTask.Text = "タスクを有効化";
            this.checkBoxEnableTask.UseVisualStyleBackColor = true;
            // 
            // checkBoxCopyFiles
            // 
            this.checkBoxCopyFiles.AutoSize = true;
            this.checkBoxCopyFiles.Location = new System.Drawing.Point(150, 295);
            this.checkBoxCopyFiles.Name = "checkBoxCopyFiles";
            this.checkBoxCopyFiles.Size = new System.Drawing.Size(134, 24);
            this.checkBoxCopyFiles.TabIndex = 19;
            this.checkBoxCopyFiles.Text = "ファイルをコピー";
            this.checkBoxCopyFiles.UseVisualStyleBackColor = true;
            this.checkBoxCopyFiles.CheckedChanged += new System.EventHandler(this.checkBoxCopyFiles_CheckedChanged);
            // 
            // checkBoxCreateFolder
            // 
            this.checkBoxCreateFolder.AutoSize = true;
            this.checkBoxCreateFolder.Location = new System.Drawing.Point(300, 295);
            this.checkBoxCreateFolder.Name = "checkBoxCreateFolder";
            this.checkBoxCreateFolder.Size = new System.Drawing.Size(118, 24);
            this.checkBoxCreateFolder.TabIndex = 20;
            this.checkBoxCreateFolder.Text = "フォルダを作成";
            this.checkBoxCreateFolder.UseVisualStyleBackColor = true;
            this.checkBoxCreateFolder.CheckedChanged += new System.EventHandler(this.checkBoxCreateFolder_CheckedChanged);
            // 
            // comboBoxInterval
            // 
            this.comboBoxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterval.FormattingEnabled = true;
            this.comboBoxInterval.Location = new System.Drawing.Point(380, 55);
            this.comboBoxInterval.Name = "comboBoxInterval";
            this.comboBoxInterval.Size = new System.Drawing.Size(150, 28);
            this.comboBoxInterval.TabIndex = 5;
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(320, 58);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(54, 20);
            this.labelInterval.TabIndex = 4;
            this.labelInterval.Text = "間隔:";
            // 
            // textBoxDateOffset
            // 
            this.textBoxDateOffset.Location = new System.Drawing.Point(630, 55);
            this.textBoxDateOffset.Name = "textBoxDateOffset";
            this.textBoxDateOffset.Size = new System.Drawing.Size(100, 27);
            this.textBoxDateOffset.TabIndex = 7;
            this.textBoxDateOffset.Text = "0";
            this.textBoxDateOffset.TextChanged += new System.EventHandler(this.textBoxDateOffset_TextChanged);
            // 
            // labelDateOffset
            // 
            this.labelDateOffset.AutoSize = true;
            this.labelDateOffset.Location = new System.Drawing.Point(550, 58);
            this.labelDateOffset.Name = "labelDateOffset";
            this.labelDateOffset.Size = new System.Drawing.Size(74, 20);
            this.labelDateOffset.TabIndex = 6;
            this.labelDateOffset.Text = "日付オフセット:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 28;
            this.button1.Text = "プレビュー更新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(600, 620);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 30);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(685, 620);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 32;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxPreview
            // 
            this.textBoxPreview.Location = new System.Drawing.Point(150, 550);
            this.textBoxPreview.Multiline = true;
            this.textBoxPreview.Name = "textBoxPreview";
            this.textBoxPreview.ReadOnly = true;
            this.textBoxPreview.Size = new System.Drawing.Size(610, 50);
            this.textBoxPreview.TabIndex = 30;
            // 
            // labelPreview
            // 
            this.labelPreview.AutoSize = true;
            this.labelPreview.Location = new System.Drawing.Point(15, 553);
            this.labelPreview.Name = "labelPreview";
            this.labelPreview.Size = new System.Drawing.Size(71, 20);
            this.labelPreview.TabIndex = 29;
            this.labelPreview.Text = "プレビュー:";
            // 
            // TaskEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.textBoxPreview);
            this.Controls.Add(this.labelPreview);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxFolderSuffixOptions);
            this.Controls.Add(this.groupBoxFolderPrefixOptions);
            this.Controls.Add(this.textBoxFolderSuffix);
            this.Controls.Add(this.labelFolderSuffix);
            this.Controls.Add(this.textBoxFolderPrefix);
            this.Controls.Add(this.labelFolderPrefix);
            this.Controls.Add(this.textBoxFolderBaseName);
            this.Controls.Add(this.labelFolderBaseName);
            this.Controls.Add(this.checkBoxCreateFolder);
            this.Controls.Add(this.checkBoxCopyFiles);
            this.Controls.Add(this.checkBoxEnableTask);
            this.Controls.Add(this.groupBoxSuffixOptions);
            this.Controls.Add(this.groupBoxPrefixOptions);
            this.Controls.Add(this.textBoxSuffix);
            this.Controls.Add(this.labelSuffix);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.labelPrefix);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.buttonDestinationFolderBrowse);
            this.Controls.Add(this.textBoxDestinationFolder);
            this.Controls.Add(this.labelDestinationFolder);
            this.Controls.Add(this.buttonSourceFolderBrowse);
            this.Controls.Add(this.textBoxSourceFolder);
            this.Controls.Add(this.labelSourceFolder);
            this.Controls.Add(this.textBoxDateOffset);
            this.Controls.Add(this.labelDateOffset);
            this.Controls.Add(this.comboBoxInterval);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.textBoxTime);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.textBoxTaskName);
            this.Controls.Add(this.labelTaskName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "タスク編集";
            this.groupBoxPrefixOptions.ResumeLayout(false);
            this.groupBoxPrefixOptions.PerformLayout();
            this.groupBoxSuffixOptions.ResumeLayout(false);
            this.groupBoxSuffixOptions.PerformLayout();
            this.groupBoxFolderPrefixOptions.ResumeLayout(false);
            this.groupBoxFolderPrefixOptions.PerformLayout();
            this.groupBoxFolderSuffixOptions.ResumeLayout(false);
            this.groupBoxFolderSuffixOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTaskName;
        private System.Windows.Forms.Label labelTaskName;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox textBoxSourceFolder;
        private System.Windows.Forms.Label labelSourceFolder;
        private System.Windows.Forms.Button buttonSourceFolderBrowse;
        private System.Windows.Forms.TextBox textBoxDestinationFolder;
        private System.Windows.Forms.Label labelDestinationFolder;
        private System.Windows.Forms.Button buttonDestinationFolderBrowse;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.TextBox textBoxSuffix;
        private System.Windows.Forms.Label labelSuffix;
        private System.Windows.Forms.CheckBox checkBoxEnableTask;
        private System.Windows.Forms.CheckBox checkBoxCopyFiles;
        private System.Windows.Forms.CheckBox checkBoxCreateFolder;
        private System.Windows.Forms.ComboBox comboBoxInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.TextBox textBoxDateOffset;
        private System.Windows.Forms.Label labelDateOffset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxPreview;
        private System.Windows.Forms.Label labelPreview;
        private System.Windows.Forms.TextBox textBoxFolderBaseName;
        private System.Windows.Forms.Label labelFolderBaseName;
        private System.Windows.Forms.TextBox textBoxFolderPrefix;
        private System.Windows.Forms.Label labelFolderPrefix;
        private System.Windows.Forms.TextBox textBoxFolderSuffix;
        private System.Windows.Forms.Label labelFolderSuffix;
        private System.Windows.Forms.GroupBox groupBoxPrefixOptions;
        private System.Windows.Forms.RadioButton checkBoxPrefixDateBefore;
        private System.Windows.Forms.RadioButton checkBoxPrefixDateAfter;
        private System.Windows.Forms.GroupBox groupBoxSuffixOptions;
        private System.Windows.Forms.RadioButton checkBoxSuffixDateBefore;
        private System.Windows.Forms.RadioButton checkBoxSuffixDateAfter;
        private System.Windows.Forms.GroupBox groupBoxFolderPrefixOptions;
        private System.Windows.Forms.RadioButton checkBoxFolderPrefixDateBefore;
        private System.Windows.Forms.RadioButton checkBoxFolderPrefixDateAfter;
        private System.Windows.Forms.GroupBox groupBoxFolderSuffixOptions;
        private System.Windows.Forms.RadioButton checkBoxFolderSuffixDateBefore;
        private System.Windows.Forms.RadioButton checkBoxFolderSuffixDateAfter;
        private System.Windows.Forms.GroupBox groupBoxFileReplace;
        private System.Windows.Forms.Label labelReplaceFrom;
        private System.Windows.Forms.TextBox textBoxReplaceFrom;
        private System.Windows.Forms.Label labelReplaceTo;
        private System.Windows.Forms.TextBox textBoxReplaceTo;
    }
}
