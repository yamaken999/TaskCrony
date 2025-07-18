using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskCrony;

/// <summary>
/// モダンテーマの色定義とスタイル適用機能を提供する静的クラス
/// </summary>
public static class ModernTheme
{
    // カラーパレット
    public static readonly Color PrimaryColor = Color.FromArgb(0, 120, 215); // Windows Blue
    public static readonly Color SecondaryColor = Color.FromArgb(243, 243, 243); // Light Gray
    public static readonly Color AccentColor = Color.FromArgb(16, 110, 190); // Darker Blue
    public static readonly Color BackgroundColor = Color.FromArgb(255, 255, 255); // White
    public static readonly Color SurfaceColor = Color.FromArgb(248, 249, 250); // Very Light Gray
    public static readonly Color TextColor = Color.FromArgb(50, 49, 48); // Dark Gray
    public static readonly Color BorderColor = Color.FromArgb(200, 200, 200); // Medium Gray

    /// <summary>
    /// フォーム全体にモダンテーマを適用
    /// </summary>
    public static void ApplyToForm(Form form)
    {
        form.BackColor = BackgroundColor;
        form.ForeColor = TextColor;
        ApplyToControls(form);
    }

    /// <summary>
    /// コントロールにモダンスタイルを再帰的に適用
    /// </summary>
    public static void ApplyToControls(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            switch (control)
            {
                case Button button:
                    ApplyToButton(button);
                    break;
                case TextBox textBox:
                    ApplyToTextBox(textBox);
                    break;
                case GroupBox groupBox:
                    ApplyToGroupBox(groupBox);
                    break;
                case TabControl tabControl:
                    ApplyToTabControl(tabControl);
                    break;
                case ListView listView:
                    ApplyToListView(listView);
                    break;
                case Label label:
                    ApplyToLabel(label);
                    break;
                case CheckBox checkBox:
                    ApplyToCheckBox(checkBox);
                    break;
                case RadioButton radioButton:
                    ApplyToRadioButton(radioButton);
                    break;
                case ComboBox comboBox:
                    ApplyToComboBox(comboBox);
                    break;
            }

            // 子コントロールにも再帰的に適用
            if (control.HasChildren)
            {
                ApplyToControls(control);
            }
        }
    }

    /// <summary>
    /// ボタンにモダンスタイルを適用
    /// </summary>
    public static void ApplyToButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = PrimaryColor;
        button.BackColor = PrimaryColor;
        button.ForeColor = Color.White;
        button.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        button.Cursor = Cursors.Hand;

        // 既存のイベントハンドラーを削除
        button.MouseEnter -= OnButtonMouseEnter;
        button.MouseLeave -= OnButtonMouseLeave;
        
        // ホバー効果を追加
        button.MouseEnter += OnButtonMouseEnter;
        button.MouseLeave += OnButtonMouseLeave;
    }

    private static void OnButtonMouseEnter(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.BackColor = AccentColor;
            button.FlatAppearance.BorderColor = AccentColor;
        }
    }

    private static void OnButtonMouseLeave(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.BackColor = PrimaryColor;
            button.FlatAppearance.BorderColor = PrimaryColor;
        }
    }

    /// <summary>
    /// テキストボックスにモダンスタイルを適用
    /// </summary>
    public static void ApplyToTextBox(TextBox textBox)
    {
        textBox.BorderStyle = BorderStyle.FixedSingle;
        textBox.BackColor = Color.White;
        textBox.ForeColor = TextColor;
        textBox.Font = new Font("メイリオ", 9F, FontStyle.Regular);
    }

    /// <summary>
    /// グループボックスにモダンスタイルを適用
    /// </summary>
    public static void ApplyToGroupBox(GroupBox groupBox)
    {
        groupBox.BackColor = SurfaceColor;
        groupBox.ForeColor = TextColor;
        groupBox.Font = new Font("メイリオ", 9F, FontStyle.Bold);
    }

    /// <summary>
    /// タブコントロールにモダンスタイルを適用
    /// </summary>
    public static void ApplyToTabControl(TabControl tabControl)
    {
        tabControl.BackColor = BackgroundColor;
        foreach (TabPage tabPage in tabControl.TabPages)
        {
            tabPage.BackColor = BackgroundColor;
            tabPage.ForeColor = TextColor;
            tabPage.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        }
    }

    /// <summary>
    /// リストビューにモダンスタイルを適用
    /// </summary>
    public static void ApplyToListView(ListView listView)
    {
        listView.BackColor = Color.White;
        listView.ForeColor = TextColor;
        listView.BorderStyle = BorderStyle.FixedSingle;
        listView.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        listView.FullRowSelect = true;
        listView.GridLines = true;
    }

    /// <summary>
    /// ラベルにモダンスタイルを適用
    /// </summary>
    public static void ApplyToLabel(Label label)
    {
        label.ForeColor = TextColor;
        label.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        label.BackColor = Color.Transparent;
    }

    /// <summary>
    /// チェックボックスにモダンスタイルを適用
    /// </summary>
    public static void ApplyToCheckBox(CheckBox checkBox)
    {
        checkBox.ForeColor = TextColor;
        checkBox.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        checkBox.BackColor = Color.Transparent;
    }

    /// <summary>
    /// ラジオボタンにモダンスタイルを適用
    /// </summary>
    public static void ApplyToRadioButton(RadioButton radioButton)
    {
        radioButton.ForeColor = TextColor;
        radioButton.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        radioButton.BackColor = Color.Transparent;
    }

    /// <summary>
    /// コンボボックスにモダンスタイルを適用
    /// </summary>
    public static void ApplyToComboBox(ComboBox comboBox)
    {
        comboBox.BackColor = Color.White;
        comboBox.ForeColor = TextColor;
        comboBox.Font = new Font("メイリオ", 9F, FontStyle.Regular);
        comboBox.FlatStyle = FlatStyle.Flat;
    }
}
