using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HandHistories.Parser.MoneyMaker.Configuration;

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
            Close();
        }

    }
}
