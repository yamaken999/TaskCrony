namespace TaskCrony;

/// <summary>
/// モダンテーマクラス（Windows 11風のスタイル適用）
/// </summary>
public static class ModernTheme
{
    #region カラー定義

    public static readonly Color BackgroundColor = Color.White;
    public static readonly Color PrimaryColor = Color.FromArgb(0, 120, 215);
    public static readonly Color SecondaryColor = Color.FromArgb(243, 243, 243);
    public static readonly Color TextColor = Color.FromArgb(32, 32, 32);
    public static readonly Color BorderColor = Color.FromArgb(200, 200, 200);
    public static readonly Color AccentColor = Color.FromArgb(0, 95, 184);

    #endregion

    #region テーマ適用メソッド

    /// <summary>
    /// フォームにモダンテーマを適用
    /// </summary>
    /// <param name="form">適用対象のフォーム</param>
    public static void ApplyToForm(Form form)
    {
        form.BackColor = BackgroundColor;
        form.ForeColor = TextColor;
        
        // フォーム内の全コントロールにテーマを適用
        ApplyToControls(form.Controls);
    }

    /// <summary>
    /// コントロールコレクションにテーマを適用
    /// </summary>
    /// <param name="controls">コントロールコレクション</param>
    private static void ApplyToControls(Control.ControlCollection controls)
    {
        foreach (Control control in controls)
        {
            ApplyToControl(control);
            
            // 子コントロールがある場合は再帰的に適用
            if (control.HasChildren)
            {
                ApplyToControls(control.Controls);
            }
        }
    }

    /// <summary>
    /// 個別コントロールにテーマを適用
    /// </summary>
    /// <param name="control">適用対象のコントロール</param>
    private static void ApplyToControl(Control control)
    {
        switch (control)
        {
            case Button button:
                ApplyButtonTheme(button);
                break;
            case TextBox textBox:
                ApplyTextBoxTheme(textBox);
                break;
            case ComboBox comboBox:
                ApplyComboBoxTheme(comboBox);
                break;
            case CheckBox checkBox:
                ApplyCheckBoxTheme(checkBox);
                break;
            case GroupBox groupBox:
                ApplyGroupBoxTheme(groupBox);
                break;
            case ListView listView:
                ApplyListViewTheme(listView);
                break;
            case TabControl tabControl:
                ApplyTabControlTheme(tabControl);
                break;
            case NumericUpDown numericUpDown:
                ApplyNumericUpDownTheme(numericUpDown);
                break;
            case DateTimePicker dateTimePicker:
                ApplyDateTimePickerTheme(dateTimePicker);
                break;
            default:
                // 基本的なテーマ適用
                control.BackColor = BackgroundColor;
                control.ForeColor = TextColor;
                break;
        }
    }

    #endregion

    #region 個別コントロールテーマ

    /// <summary>
    /// ボタンテーマを適用
    /// </summary>
    private static void ApplyButtonTheme(Button button)
    {
        if (button.BackColor == Color.FromArgb(0, 120, 215) || 
            button.BackColor == Color.FromArgb(196, 43, 28))
        {
            // 既にカスタムカラーが設定されている場合はそのまま
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            return;
        }

        button.BackColor = SecondaryColor;
        button.ForeColor = TextColor;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderColor = BorderColor;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
        button.FlatAppearance.MouseDownBackColor = Color.FromArgb(204, 204, 204);
    }

    /// <summary>
    /// テキストボックステーマを適用
    /// </summary>
    private static void ApplyTextBoxTheme(TextBox textBox)
    {
        textBox.BackColor = BackgroundColor;
        textBox.ForeColor = TextColor;
        textBox.BorderStyle = BorderStyle.FixedSingle;
    }

    /// <summary>
    /// コンボボックステーマを適用
    /// </summary>
    private static void ApplyComboBoxTheme(ComboBox comboBox)
    {
        comboBox.BackColor = BackgroundColor;
        comboBox.ForeColor = TextColor;
        comboBox.FlatStyle = FlatStyle.Flat;
    }

    /// <summary>
    /// チェックボックステーマを適用
    /// </summary>
    private static void ApplyCheckBoxTheme(CheckBox checkBox)
    {
        checkBox.BackColor = BackgroundColor;
        checkBox.ForeColor = TextColor;
        checkBox.FlatStyle = FlatStyle.Flat;
    }

    /// <summary>
    /// グループボックステーマを適用
    /// </summary>
    private static void ApplyGroupBoxTheme(GroupBox groupBox)
    {
        groupBox.BackColor = BackgroundColor;
        groupBox.ForeColor = TextColor;
        groupBox.FlatStyle = FlatStyle.Flat;
    }

    /// <summary>
    /// リストビューテーマを適用
    /// </summary>
    private static void ApplyListViewTheme(ListView listView)
    {
        listView.BackColor = BackgroundColor;
        listView.ForeColor = TextColor;
        listView.BorderStyle = BorderStyle.FixedSingle;
    }

    /// <summary>
    /// タブコントロールテーマを適用
    /// </summary>
    private static void ApplyTabControlTheme(TabControl tabControl)
    {
        tabControl.BackColor = BackgroundColor;
        tabControl.ForeColor = TextColor;
        
        foreach (TabPage tabPage in tabControl.TabPages)
        {
            tabPage.BackColor = BackgroundColor;
            tabPage.ForeColor = TextColor;
        }
    }

    /// <summary>
    /// NumericUpDownテーマを適用
    /// </summary>
    private static void ApplyNumericUpDownTheme(NumericUpDown numericUpDown)
    {
        numericUpDown.BackColor = BackgroundColor;
        numericUpDown.ForeColor = TextColor;
        numericUpDown.BorderStyle = BorderStyle.FixedSingle;
    }

    /// <summary>
    /// DateTimePickerテーマを適用
    /// </summary>
    private static void ApplyDateTimePickerTheme(DateTimePicker dateTimePicker)
    {
        dateTimePicker.BackColor = BackgroundColor;
        dateTimePicker.ForeColor = TextColor;
    }

    #endregion
}
