using System;
using System.Windows.Forms;

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
            hhFolderTextBox.Text = Properties.Settings.Default.HandHistoryFolder;
            fileTrackingTxtBx.Text = Properties.Settings.Default.FileTrackingFolder;
            heroTextBox.Text = Properties.Settings.Default.Hero;
            numericUpDownComit.Value = Properties.Settings.Default.CommitCount;

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
            Properties.Settings.Default.HandHistoryFolder = hhFolderTextBox.Text;
            Properties.Settings.Default.FileTrackingFolder = fileTrackingTxtBx.Text;
            Properties.Settings.Default.Hero = heroTextBox.Text;
            Properties.Settings.Default.CommitCount = numericUpDownComit.Value;
            Properties.Settings.Default.Save();
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
