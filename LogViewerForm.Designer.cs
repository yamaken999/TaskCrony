namespace TaskCrony
{
    partial class LogViewerForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox textBoxLogs;
        private Button buttonRefresh;
        private Button buttonClear;
        private Button buttonOpenLogFolder;
        private Button buttonClose;
        private ComboBox comboBoxLogLevel;
        private Label labelLogLevel;
        private CheckBox checkBoxAutoScroll;

        private void InitializeComponent()
        {
            this.textBoxLogs = new TextBox();
            this.buttonRefresh = new Button();
            this.buttonClear = new Button();
            this.buttonOpenLogFolder = new Button();
            this.buttonClose = new Button();
            this.comboBoxLogLevel = new ComboBox();
            this.labelLogLevel = new Label();
            this.checkBoxAutoScroll = new CheckBox();
            this.SuspendLayout();
            
            // Form
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.checkBoxAutoScroll);
            this.Controls.Add(this.labelLogLevel);
            this.Controls.Add(this.comboBoxLogLevel);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpenLogFolder);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.textBoxLogs);
            this.MinimumSize = new Size(600, 400);
            this.Name = "LogViewerForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "TaskCrony ログビューアー";
            
            // textBoxLogs
            this.textBoxLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxLogs.BackColor = Color.Black;
            this.textBoxLogs.ForeColor = Color.Lime;
            this.textBoxLogs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.textBoxLogs.Location = new Point(12, 45);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = ScrollBars.Both;
            this.textBoxLogs.Size = new Size(776, 510);
            this.textBoxLogs.TabIndex = 0;
            this.textBoxLogs.WordWrap = false;
            
            // buttonRefresh
            this.buttonRefresh.Location = new Point(12, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new Size(75, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "更新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            
            // buttonClear
            this.buttonClear.Location = new Point(93, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new Size(75, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "クリア";
            this.buttonClear.UseVisualStyleBackColor = true;
            
            // buttonOpenLogFolder
            this.buttonOpenLogFolder.Location = new Point(174, 12);
            this.buttonOpenLogFolder.Name = "buttonOpenLogFolder";
            this.buttonOpenLogFolder.Size = new Size(100, 23);
            this.buttonOpenLogFolder.TabIndex = 3;
            this.buttonOpenLogFolder.Text = "ログフォルダ";
            this.buttonOpenLogFolder.UseVisualStyleBackColor = true;
            
            // labelLogLevel
            this.labelLogLevel.AutoSize = true;
            this.labelLogLevel.Location = new Point(300, 16);
            this.labelLogLevel.Name = "labelLogLevel";
            this.labelLogLevel.Size = new Size(72, 15);
            this.labelLogLevel.TabIndex = 4;
            this.labelLogLevel.Text = "ログレベル:";
            
            // comboBoxLogLevel
            this.comboBoxLogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxLogLevel.FormattingEnabled = true;
            this.comboBoxLogLevel.Items.AddRange(new object[] {
                "すべて",
                "INFO",
                "WARN", 
                "ERROR",
                "TASK",
                "DEBUG"
            });
            this.comboBoxLogLevel.Location = new Point(378, 12);
            this.comboBoxLogLevel.Name = "comboBoxLogLevel";
            this.comboBoxLogLevel.Size = new Size(80, 23);
            this.comboBoxLogLevel.TabIndex = 5;
            
            // checkBoxAutoScroll
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Checked = true;
            this.checkBoxAutoScroll.CheckState = CheckState.Checked;
            this.checkBoxAutoScroll.Location = new Point(480, 15);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new Size(84, 19);
            this.checkBoxAutoScroll.TabIndex = 6;
            this.checkBoxAutoScroll.Text = "自動スクロール";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            
            // buttonClose
            this.buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.buttonClose.Location = new Point(713, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
