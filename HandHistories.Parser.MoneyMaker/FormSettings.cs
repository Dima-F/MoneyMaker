using System;
using System.Windows.Forms;
using MoneyMaker.BLL.Configuration;

namespace HandHistories.Parser.MoneyMaker
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }
        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            hhFolderTextBox.Text = SettingsConfig.GetConfig("HandHistoryFolder");
            fileTrackingTxtBx.Text = SettingsConfig.GetConfig("FileTrackingFolder");
            heroTextBox.Text = SettingsConfig.GetConfig("Hero");
            numericUpDownComit.Value = Int32.Parse(SettingsConfig.GetConfig("CommitCount"));

        }
        private void hhFolderButton_Click(object sender, EventArgs e)
        {
            var result=folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                hhFolderTextBox.Text = folderDialog.SelectedPath;
            }
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            SettingsConfig.SetConfig("HandHistoryFolder", hhFolderTextBox.Text);
            SettingsConfig.SetConfig("FileTrackingFolder", fileTrackingTxtBx.Text);
            SettingsConfig.SetConfig("Hero", heroTextBox.Text);
            SettingsConfig.SetConfig("CommitCount", numericUpDownComit.Value.ToString());
            Close();
        }

        private void fileTrackingBtn_Click(object sender, EventArgs e)
        {
            var result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileTrackingTxtBx.Text = folderDialog.SelectedPath;
            }
        }
    }
}
