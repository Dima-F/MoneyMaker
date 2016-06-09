using System;
using System.Windows.Forms;

namespace MoneyMaker.UI.Light
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
            numericUpDownLiveCount.Value = Properties.Settings.Default.LiveGamesCount;

            cbStatHands.Checked = Properties.Settings.Default.Stat_Hands;
            cbStatProfit.Checked = Properties.Settings.Default.Stat_Profit;
            cbStatWin.Checked = Properties.Settings.Default.Stat_Win;
            cbStatVpip.Checked = Properties.Settings.Default.Stat_VPIP;
            cbStatPfr.Checked = Properties.Settings.Default.Stat_PFR;
            cbStatAts.Checked = Properties.Settings.Default.Stat_ATS;
            cbStatAf.Checked = Properties.Settings.Default.Stat_AF;
            cbStat3B.Checked = Properties.Settings.Default.Stat_3B;
            cbStatBB.Checked = Properties.Settings.Default.Stat_BB;

            cbPfrEp.Checked = Properties.Settings.Default.Stat_EP_PFR;
            cbPfrMp.Checked = Properties.Settings.Default.Stat_MP_PFR;
            cbPfrLp.Checked = Properties.Settings.Default.Stat_LP_PFR;
            cbVpipEp.Checked = Properties.Settings.Default.Stat_EP_VPIP;
            cbVpipMp.Checked = Properties.Settings.Default.Stat_MP_VPIP;
            cbVpipLp.Checked = Properties.Settings.Default.Stat_LP_VPIP;

            cbAfFlop.Checked = Properties.Settings.Default.Stat_AF_Flop;
            cbAfTurn.Checked = Properties.Settings.Default.Stat_AF_Turn;
            cbAfRiver.Checked = Properties.Settings.Default.Stat_AF_River;

            cbAtsCo.Checked = Properties.Settings.Default.Stat_ATS_CO;
            cbAtsB.Checked = Properties.Settings.Default.Stat_ATS_B;
            cbAtsSb.Checked = Properties.Settings.Default.Stat_ATS_SB;
            cbFoldSbToStl.Checked = Properties.Settings.Default.Stat_Fold_SB_ToSteal;
            cbFoldBbToStl.Checked = Properties.Settings.Default.Stat_Fold_BB_ToSteal;
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
            Properties.Settings.Default.LiveGamesCount = (int)numericUpDownLiveCount.Value;

            Properties.Settings.Default.Stat_Hands=cbStatHands.Checked;
            Properties.Settings.Default.Stat_Profit = cbStatProfit.Checked;
            Properties.Settings.Default.Stat_Win = cbStatWin.Checked;
            Properties.Settings.Default.Stat_VPIP = cbStatVpip.Checked;
            Properties.Settings.Default.Stat_PFR = cbStatPfr.Checked;
            Properties.Settings.Default.Stat_ATS = cbStatAts.Checked;
            Properties.Settings.Default.Stat_AF = cbStatAf.Checked;
            Properties.Settings.Default.Stat_3B = cbStat3B.Checked;
            Properties.Settings.Default.Stat_BB = cbStatBB.Checked;

            Properties.Settings.Default.Stat_EP_PFR =  cbPfrEp.Checked;
            Properties.Settings.Default.Stat_MP_PFR = cbPfrMp.Checked;
            Properties.Settings.Default.Stat_LP_PFR = cbPfrLp.Checked;
            Properties.Settings.Default.Stat_EP_VPIP = cbVpipEp.Checked;
            Properties.Settings.Default.Stat_MP_VPIP = cbVpipMp.Checked;
            Properties.Settings.Default.Stat_LP_VPIP = cbVpipLp.Checked;

            Properties.Settings.Default.Stat_AF_Flop = cbAfFlop.Checked;
            Properties.Settings.Default.Stat_AF_Turn = cbAfTurn.Checked;
            Properties.Settings.Default.Stat_AF_River = cbAfRiver.Checked;

            Properties.Settings.Default.Stat_ATS_CO = cbAtsCo.Checked;
            Properties.Settings.Default.Stat_ATS_B = cbAtsB.Checked;
            Properties.Settings.Default.Stat_ATS_SB = cbAtsSb.Checked;
            Properties.Settings.Default.Stat_Fold_SB_ToSteal = cbFoldSbToStl.Checked;
            Properties.Settings.Default.Stat_Fold_BB_ToSteal = cbFoldBbToStl.Checked;


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
