namespace MoneyMaker.UI.Light
{
    partial class FormSettings
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
            this.settingsTab = new System.Windows.Forms.TabControl();
            this.commonTabPage = new System.Windows.Forms.TabPage();
            this.fileTrackingBtn = new System.Windows.Forms.Button();
            this.fileTrackingTxtBx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.heroTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hhFolderButton = new System.Windows.Forms.Button();
            this.hhFolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageStat = new System.Windows.Forms.TabPage();
            this.cbAtsCo = new System.Windows.Forms.CheckBox();
            this.cbAfRiver = new System.Windows.Forms.CheckBox();
            this.cbAfTurn = new System.Windows.Forms.CheckBox();
            this.cbAfFlop = new System.Windows.Forms.CheckBox();
            this.cbPfrLp = new System.Windows.Forms.CheckBox();
            this.cbPfrMp = new System.Windows.Forms.CheckBox();
            this.cbPfrEp = new System.Windows.Forms.CheckBox();
            this.cbVpipLp = new System.Windows.Forms.CheckBox();
            this.cbVpipMp = new System.Windows.Forms.CheckBox();
            this.cbVpipEp = new System.Windows.Forms.CheckBox();
            this.cbStatBB = new System.Windows.Forms.CheckBox();
            this.cbStat3B = new System.Windows.Forms.CheckBox();
            this.cbStatAf = new System.Windows.Forms.CheckBox();
            this.cbStatAts = new System.Windows.Forms.CheckBox();
            this.cbStatVpip = new System.Windows.Forms.CheckBox();
            this.cbStatPfr = new System.Windows.Forms.CheckBox();
            this.cbStatWin = new System.Windows.Forms.CheckBox();
            this.cbStatProfit = new System.Windows.Forms.CheckBox();
            this.cbStatHands = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.advancedTabPage = new System.Windows.Forms.TabPage();
            this.numericUpDownComit = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxManual = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbAtsB = new System.Windows.Forms.CheckBox();
            this.cbAtsSb = new System.Windows.Forms.CheckBox();
            this.settingsTab.SuspendLayout();
            this.commonTabPage.SuspendLayout();
            this.tabPageStat.SuspendLayout();
            this.advancedTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComit)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.commonTabPage);
            this.settingsTab.Controls.Add(this.tabPageStat);
            this.settingsTab.Controls.Add(this.advancedTabPage);
            this.settingsTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsTab.Location = new System.Drawing.Point(0, 0);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.SelectedIndex = 0;
            this.settingsTab.Size = new System.Drawing.Size(382, 314);
            this.settingsTab.TabIndex = 0;
            // 
            // commonTabPage
            // 
            this.commonTabPage.Controls.Add(this.fileTrackingBtn);
            this.commonTabPage.Controls.Add(this.fileTrackingTxtBx);
            this.commonTabPage.Controls.Add(this.label4);
            this.commonTabPage.Controls.Add(this.heroTextBox);
            this.commonTabPage.Controls.Add(this.label2);
            this.commonTabPage.Controls.Add(this.groupBox1);
            this.commonTabPage.Controls.Add(this.hhFolderButton);
            this.commonTabPage.Controls.Add(this.hhFolderTextBox);
            this.commonTabPage.Controls.Add(this.label1);
            this.commonTabPage.Location = new System.Drawing.Point(4, 22);
            this.commonTabPage.Name = "commonTabPage";
            this.commonTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.commonTabPage.Size = new System.Drawing.Size(374, 288);
            this.commonTabPage.TabIndex = 0;
            this.commonTabPage.Text = "Common";
            this.commonTabPage.UseVisualStyleBackColor = true;
            // 
            // fileTrackingBtn
            // 
            this.fileTrackingBtn.Location = new System.Drawing.Point(335, 65);
            this.fileTrackingBtn.Name = "fileTrackingBtn";
            this.fileTrackingBtn.Size = new System.Drawing.Size(31, 23);
            this.fileTrackingBtn.TabIndex = 8;
            this.fileTrackingBtn.Text = "...";
            this.fileTrackingBtn.UseVisualStyleBackColor = true;
            this.fileTrackingBtn.Click += new System.EventHandler(this.fileTrackingBtn_Click);
            // 
            // fileTrackingTxtBx
            // 
            this.fileTrackingTxtBx.Location = new System.Drawing.Point(20, 65);
            this.fileTrackingTxtBx.Name = "fileTrackingTxtBx";
            this.fileTrackingTxtBx.Size = new System.Drawing.Size(309, 20);
            this.fileTrackingTxtBx.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "File tracking folder:";
            // 
            // heroTextBox
            // 
            this.heroTextBox.Location = new System.Drawing.Point(20, 104);
            this.heroTextBox.Name = "heroTextBox";
            this.heroTextBox.Size = new System.Drawing.Size(309, 20);
            this.heroTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hero name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 155);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other options";
            // 
            // hhFolderButton
            // 
            this.hhFolderButton.Location = new System.Drawing.Point(335, 24);
            this.hhFolderButton.Name = "hhFolderButton";
            this.hhFolderButton.Size = new System.Drawing.Size(31, 23);
            this.hhFolderButton.TabIndex = 2;
            this.hhFolderButton.Text = "...";
            this.hhFolderButton.UseVisualStyleBackColor = true;
            this.hhFolderButton.Click += new System.EventHandler(this.hhFolderButton_Click);
            // 
            // hhFolderTextBox
            // 
            this.hhFolderTextBox.Location = new System.Drawing.Point(20, 26);
            this.hhFolderTextBox.Name = "hhFolderTextBox";
            this.hhFolderTextBox.Size = new System.Drawing.Size(309, 20);
            this.hhFolderTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hand histories folder:";
            // 
            // tabPageStat
            // 
            this.tabPageStat.Controls.Add(this.cbAtsSb);
            this.tabPageStat.Controls.Add(this.cbAtsB);
            this.tabPageStat.Controls.Add(this.cbAtsCo);
            this.tabPageStat.Controls.Add(this.cbAfRiver);
            this.tabPageStat.Controls.Add(this.cbAfTurn);
            this.tabPageStat.Controls.Add(this.cbAfFlop);
            this.tabPageStat.Controls.Add(this.cbPfrLp);
            this.tabPageStat.Controls.Add(this.cbPfrMp);
            this.tabPageStat.Controls.Add(this.cbPfrEp);
            this.tabPageStat.Controls.Add(this.cbVpipLp);
            this.tabPageStat.Controls.Add(this.cbVpipMp);
            this.tabPageStat.Controls.Add(this.cbVpipEp);
            this.tabPageStat.Controls.Add(this.cbStatBB);
            this.tabPageStat.Controls.Add(this.cbStat3B);
            this.tabPageStat.Controls.Add(this.cbStatAf);
            this.tabPageStat.Controls.Add(this.cbStatAts);
            this.tabPageStat.Controls.Add(this.cbStatVpip);
            this.tabPageStat.Controls.Add(this.cbStatPfr);
            this.tabPageStat.Controls.Add(this.cbStatWin);
            this.tabPageStat.Controls.Add(this.cbStatProfit);
            this.tabPageStat.Controls.Add(this.cbStatHands);
            this.tabPageStat.Controls.Add(this.label5);
            this.tabPageStat.Location = new System.Drawing.Point(4, 22);
            this.tabPageStat.Name = "tabPageStat";
            this.tabPageStat.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStat.Size = new System.Drawing.Size(374, 288);
            this.tabPageStat.TabIndex = 2;
            this.tabPageStat.Text = "Stats";
            this.tabPageStat.UseVisualStyleBackColor = true;
            // 
            // cbAtsCo
            // 
            this.cbAtsCo.AutoSize = true;
            this.cbAtsCo.Location = new System.Drawing.Point(158, 39);
            this.cbAtsCo.Name = "cbAtsCo";
            this.cbAtsCo.Size = new System.Drawing.Size(65, 17);
            this.cbAtsCo.TabIndex = 20;
            this.cbAtsCo.Text = "CO ATS";
            this.cbAtsCo.UseVisualStyleBackColor = true;
            // 
            // cbAfRiver
            // 
            this.cbAfRiver.AutoSize = true;
            this.cbAfRiver.Location = new System.Drawing.Point(74, 223);
            this.cbAfRiver.Name = "cbAfRiver";
            this.cbAfRiver.Size = new System.Drawing.Size(67, 17);
            this.cbAfRiver.TabIndex = 19;
            this.cbAfRiver.Text = "AF River";
            this.cbAfRiver.UseVisualStyleBackColor = true;
            // 
            // cbAfTurn
            // 
            this.cbAfTurn.AutoSize = true;
            this.cbAfTurn.Location = new System.Drawing.Point(74, 200);
            this.cbAfTurn.Name = "cbAfTurn";
            this.cbAfTurn.Size = new System.Drawing.Size(64, 17);
            this.cbAfTurn.TabIndex = 18;
            this.cbAfTurn.Text = "AF Turn";
            this.cbAfTurn.UseVisualStyleBackColor = true;
            // 
            // cbAfFlop
            // 
            this.cbAfFlop.AutoSize = true;
            this.cbAfFlop.Location = new System.Drawing.Point(74, 177);
            this.cbAfFlop.Name = "cbAfFlop";
            this.cbAfFlop.Size = new System.Drawing.Size(62, 17);
            this.cbAfFlop.TabIndex = 17;
            this.cbAfFlop.Text = "AF Flop";
            this.cbAfFlop.UseVisualStyleBackColor = true;
            // 
            // cbPfrLp
            // 
            this.cbPfrLp.AutoSize = true;
            this.cbPfrLp.Location = new System.Drawing.Point(74, 154);
            this.cbPfrLp.Name = "cbPfrLp";
            this.cbPfrLp.Size = new System.Drawing.Size(63, 17);
            this.cbPfrLp.TabIndex = 16;
            this.cbPfrLp.Text = "LP PFR";
            this.cbPfrLp.UseVisualStyleBackColor = true;
            // 
            // cbPfrMp
            // 
            this.cbPfrMp.AutoSize = true;
            this.cbPfrMp.Location = new System.Drawing.Point(74, 131);
            this.cbPfrMp.Name = "cbPfrMp";
            this.cbPfrMp.Size = new System.Drawing.Size(66, 17);
            this.cbPfrMp.TabIndex = 15;
            this.cbPfrMp.Text = "MP PFR";
            this.cbPfrMp.UseVisualStyleBackColor = true;
            // 
            // cbPfrEp
            // 
            this.cbPfrEp.AutoSize = true;
            this.cbPfrEp.Location = new System.Drawing.Point(74, 108);
            this.cbPfrEp.Name = "cbPfrEp";
            this.cbPfrEp.Size = new System.Drawing.Size(64, 17);
            this.cbPfrEp.TabIndex = 14;
            this.cbPfrEp.Text = "EP PFR";
            this.cbPfrEp.UseVisualStyleBackColor = true;
            // 
            // cbVpipLp
            // 
            this.cbVpipLp.AutoSize = true;
            this.cbVpipLp.Location = new System.Drawing.Point(74, 85);
            this.cbVpipLp.Name = "cbVpipLp";
            this.cbVpipLp.Size = new System.Drawing.Size(66, 17);
            this.cbVpipLp.TabIndex = 13;
            this.cbVpipLp.Text = "LP VPIP";
            this.cbVpipLp.UseVisualStyleBackColor = true;
            // 
            // cbVpipMp
            // 
            this.cbVpipMp.AutoSize = true;
            this.cbVpipMp.Location = new System.Drawing.Point(74, 62);
            this.cbVpipMp.Name = "cbVpipMp";
            this.cbVpipMp.Size = new System.Drawing.Size(69, 17);
            this.cbVpipMp.TabIndex = 12;
            this.cbVpipMp.Text = "MP VPIP";
            this.cbVpipMp.UseVisualStyleBackColor = true;
            // 
            // cbVpipEp
            // 
            this.cbVpipEp.AutoSize = true;
            this.cbVpipEp.Location = new System.Drawing.Point(74, 39);
            this.cbVpipEp.Name = "cbVpipEp";
            this.cbVpipEp.Size = new System.Drawing.Size(67, 17);
            this.cbVpipEp.TabIndex = 11;
            this.cbVpipEp.Text = "EP VPIP";
            this.cbVpipEp.UseVisualStyleBackColor = true;
            // 
            // cbStatBB
            // 
            this.cbStatBB.AutoSize = true;
            this.cbStatBB.Location = new System.Drawing.Point(8, 223);
            this.cbStatBB.Name = "cbStatBB";
            this.cbStatBB.Size = new System.Drawing.Size(40, 17);
            this.cbStatBB.TabIndex = 10;
            this.cbStatBB.Text = "BB";
            this.cbStatBB.UseVisualStyleBackColor = true;
            // 
            // cbStat3B
            // 
            this.cbStat3B.AutoSize = true;
            this.cbStat3B.Location = new System.Drawing.Point(8, 200);
            this.cbStat3B.Name = "cbStat3B";
            this.cbStat3B.Size = new System.Drawing.Size(39, 17);
            this.cbStat3B.TabIndex = 9;
            this.cbStat3B.Text = "3B";
            this.cbStat3B.UseVisualStyleBackColor = true;
            // 
            // cbStatAf
            // 
            this.cbStatAf.AutoSize = true;
            this.cbStatAf.Location = new System.Drawing.Point(8, 177);
            this.cbStatAf.Name = "cbStatAf";
            this.cbStatAf.Size = new System.Drawing.Size(39, 17);
            this.cbStatAf.TabIndex = 8;
            this.cbStatAf.Text = "AF";
            this.cbStatAf.UseVisualStyleBackColor = true;
            // 
            // cbStatAts
            // 
            this.cbStatAts.AutoSize = true;
            this.cbStatAts.Location = new System.Drawing.Point(8, 154);
            this.cbStatAts.Name = "cbStatAts";
            this.cbStatAts.Size = new System.Drawing.Size(47, 17);
            this.cbStatAts.TabIndex = 7;
            this.cbStatAts.Text = "ATS";
            this.cbStatAts.UseVisualStyleBackColor = true;
            // 
            // cbStatVpip
            // 
            this.cbStatVpip.AutoSize = true;
            this.cbStatVpip.Location = new System.Drawing.Point(8, 108);
            this.cbStatVpip.Name = "cbStatVpip";
            this.cbStatVpip.Size = new System.Drawing.Size(50, 17);
            this.cbStatVpip.TabIndex = 6;
            this.cbStatVpip.Text = "VPIP";
            this.cbStatVpip.UseVisualStyleBackColor = true;
            // 
            // cbStatPfr
            // 
            this.cbStatPfr.AutoSize = true;
            this.cbStatPfr.Location = new System.Drawing.Point(8, 131);
            this.cbStatPfr.Name = "cbStatPfr";
            this.cbStatPfr.Size = new System.Drawing.Size(47, 17);
            this.cbStatPfr.TabIndex = 5;
            this.cbStatPfr.Text = "PFR";
            this.cbStatPfr.UseVisualStyleBackColor = true;
            // 
            // cbStatWin
            // 
            this.cbStatWin.AutoSize = true;
            this.cbStatWin.Location = new System.Drawing.Point(8, 85);
            this.cbStatWin.Name = "cbStatWin";
            this.cbStatWin.Size = new System.Drawing.Size(56, 17);
            this.cbStatWin.TabIndex = 4;
            this.cbStatWin.Text = "Win %";
            this.cbStatWin.UseVisualStyleBackColor = true;
            // 
            // cbStatProfit
            // 
            this.cbStatProfit.AutoSize = true;
            this.cbStatProfit.Location = new System.Drawing.Point(8, 62);
            this.cbStatProfit.Name = "cbStatProfit";
            this.cbStatProfit.Size = new System.Drawing.Size(50, 17);
            this.cbStatProfit.TabIndex = 3;
            this.cbStatProfit.Text = "Profit";
            this.cbStatProfit.UseVisualStyleBackColor = true;
            // 
            // cbStatHands
            // 
            this.cbStatHands.AutoSize = true;
            this.cbStatHands.Location = new System.Drawing.Point(8, 39);
            this.cbStatHands.Name = "cbStatHands";
            this.cbStatHands.Size = new System.Drawing.Size(57, 17);
            this.cbStatHands.TabIndex = 2;
            this.cbStatHands.Text = "Hands";
            this.cbStatHands.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(103, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Stats display settings:";
            // 
            // advancedTabPage
            // 
            this.advancedTabPage.Controls.Add(this.numericUpDownComit);
            this.advancedTabPage.Controls.Add(this.groupBox2);
            this.advancedTabPage.Controls.Add(this.label3);
            this.advancedTabPage.Location = new System.Drawing.Point(4, 22);
            this.advancedTabPage.Name = "advancedTabPage";
            this.advancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.advancedTabPage.Size = new System.Drawing.Size(374, 288);
            this.advancedTabPage.TabIndex = 1;
            this.advancedTabPage.Text = "Advanced";
            this.advancedTabPage.UseVisualStyleBackColor = true;
            // 
            // numericUpDownComit
            // 
            this.numericUpDownComit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownComit.Location = new System.Drawing.Point(92, 13);
            this.numericUpDownComit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownComit.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownComit.Name = "numericUpDownComit";
            this.numericUpDownComit.Size = new System.Drawing.Size(59, 20);
            this.numericUpDownComit.TabIndex = 4;
            this.numericUpDownComit.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxManual);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 161);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other options";
            // 
            // checkBoxManual
            // 
            this.checkBoxManual.AutoSize = true;
            this.checkBoxManual.Location = new System.Drawing.Point(6, 19);
            this.checkBoxManual.Name = "checkBoxManual";
            this.checkBoxManual.Size = new System.Drawing.Size(129, 17);
            this.checkBoxManual.TabIndex = 1;
            this.checkBoxManual.Text = "Use manual initializing";
            this.checkBoxManual.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Commit count :";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(60, 340);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(218, 340);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // cbAtsB
            // 
            this.cbAtsB.AutoSize = true;
            this.cbAtsB.Location = new System.Drawing.Point(158, 62);
            this.cbAtsB.Name = "cbAtsB";
            this.cbAtsB.Size = new System.Drawing.Size(57, 17);
            this.cbAtsB.TabIndex = 21;
            this.cbAtsB.Text = "B ATS";
            this.cbAtsB.UseVisualStyleBackColor = true;
            // 
            // cbAtsSb
            // 
            this.cbAtsSb.AutoSize = true;
            this.cbAtsSb.Location = new System.Drawing.Point(158, 85);
            this.cbAtsSb.Name = "cbAtsSb";
            this.cbAtsSb.Size = new System.Drawing.Size(64, 17);
            this.cbAtsSb.TabIndex = 22;
            this.cbAtsSb.Text = "SB ATS";
            this.cbAtsSb.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(382, 375);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.settingsTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.settingsTab.ResumeLayout(false);
            this.commonTabPage.ResumeLayout(false);
            this.commonTabPage.PerformLayout();
            this.tabPageStat.ResumeLayout(false);
            this.tabPageStat.PerformLayout();
            this.advancedTabPage.ResumeLayout(false);
            this.advancedTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTab;
        private System.Windows.Forms.TabPage commonTabPage;
        private System.Windows.Forms.TabPage advancedTabPage;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button hhFolderButton;
        private System.Windows.Forms.TextBox hhFolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox heroTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxManual;
        private System.Windows.Forms.NumericUpDown numericUpDownComit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fileTrackingTxtBx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button fileTrackingBtn;
        private System.Windows.Forms.TabPage tabPageStat;
        private System.Windows.Forms.CheckBox cbStat3B;
        private System.Windows.Forms.CheckBox cbStatAf;
        private System.Windows.Forms.CheckBox cbStatAts;
        private System.Windows.Forms.CheckBox cbStatVpip;
        private System.Windows.Forms.CheckBox cbStatPfr;
        private System.Windows.Forms.CheckBox cbStatWin;
        private System.Windows.Forms.CheckBox cbStatProfit;
        private System.Windows.Forms.CheckBox cbStatHands;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbStatBB;
        private System.Windows.Forms.CheckBox cbPfrLp;
        private System.Windows.Forms.CheckBox cbPfrMp;
        private System.Windows.Forms.CheckBox cbPfrEp;
        private System.Windows.Forms.CheckBox cbVpipLp;
        private System.Windows.Forms.CheckBox cbVpipMp;
        private System.Windows.Forms.CheckBox cbVpipEp;
        private System.Windows.Forms.CheckBox cbAfRiver;
        private System.Windows.Forms.CheckBox cbAfTurn;
        private System.Windows.Forms.CheckBox cbAfFlop;
        private System.Windows.Forms.CheckBox cbAtsCo;
        private System.Windows.Forms.CheckBox cbAtsSb;
        private System.Windows.Forms.CheckBox cbAtsB;
    }
}