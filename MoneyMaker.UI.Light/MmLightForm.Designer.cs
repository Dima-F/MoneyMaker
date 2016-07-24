namespace MoneyMaker.UI.Light
{
    partial class MmLightForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MmLightForm));
            this.pictureBoxAbout = new System.Windows.Forms.PictureBox();
            this.pictureBoxTrack = new System.Windows.Forms.PictureBox();
            this.pictureBoxSettings = new System.Windows.Forms.PictureBox();
            this.pictureBoxSkin = new System.Windows.Forms.PictureBox();
            this.tabControlGeneral = new System.Windows.Forms.TabControl();
            this.tabPageAllStats = new System.Windows.Forms.TabPage();
            this.cbAllStats = new System.Windows.Forms.CheckBox();
            this.btnStatSyncron = new System.Windows.Forms.Button();
            this.comboBoxPlayers = new System.Windows.Forms.ComboBox();
            this.checkBoxLive = new System.Windows.Forms.CheckBox();
            this.datGrViewAllStats = new System.Windows.Forms.DataGridView();
            this.btnAllStat = new System.Windows.Forms.Button();
            this.tabPageSimpleParsing = new System.Windows.Forms.TabPage();
            this.lblGamesCount = new System.Windows.Forms.Label();
            this.lblSimpPars = new System.Windows.Forms.Label();
            this.progBarSimpleParsing = new System.Windows.Forms.ProgressBar();
            this.textBoxSimpleParsing = new System.Windows.Forms.TextBox();
            this.btnSimplePars = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin)).BeginInit();
            this.tabControlGeneral.SuspendLayout();
            this.tabPageAllStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datGrViewAllStats)).BeginInit();
            this.tabPageSimpleParsing.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxAbout
            // 
            this.pictureBoxAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxAbout.Image = global::MoneyMaker.UI.Light.Properties.Resources.blue_info;
            this.pictureBoxAbout.Location = new System.Drawing.Point(124, 0);
            this.pictureBoxAbout.Name = "pictureBoxAbout";
            this.pictureBoxAbout.Size = new System.Drawing.Size(56, 56);
            this.pictureBoxAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAbout.TabIndex = 13;
            this.pictureBoxAbout.TabStop = false;
            this.pictureBoxAbout.Click += new System.EventHandler(this.pictureBoxAbout_Click);
            // 
            // pictureBoxTrack
            // 
            this.pictureBoxTrack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxTrack.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTrack.Name = "pictureBoxTrack";
            this.pictureBoxTrack.Size = new System.Drawing.Size(56, 56);
            this.pictureBoxTrack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTrack.TabIndex = 11;
            this.pictureBoxTrack.TabStop = false;
            this.pictureBoxTrack.Click += new System.EventHandler(this.pictureBoxTrack_Click);
            // 
            // pictureBoxSettings
            // 
            this.pictureBoxSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSettings.Image = global::MoneyMaker.UI.Light.Properties.Resources.green_settings;
            this.pictureBoxSettings.Location = new System.Drawing.Point(62, 0);
            this.pictureBoxSettings.Name = "pictureBoxSettings";
            this.pictureBoxSettings.Size = new System.Drawing.Size(56, 56);
            this.pictureBoxSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSettings.TabIndex = 12;
            this.pictureBoxSettings.TabStop = false;
            this.pictureBoxSettings.Click += new System.EventHandler(this.pictureBoxSettings_Click);
            // 
            // pictureBoxSkin
            // 
            this.pictureBoxSkin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSkin.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSkin.Name = "pictureBoxSkin";
            this.pictureBoxSkin.Size = new System.Drawing.Size(721, 391);
            this.pictureBoxSkin.TabIndex = 1;
            this.pictureBoxSkin.TabStop = false;
            // 
            // tabControlGeneral
            // 
            this.tabControlGeneral.Controls.Add(this.tabPageAllStats);
            this.tabControlGeneral.Controls.Add(this.tabPageSimpleParsing);
            this.tabControlGeneral.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControlGeneral.Location = new System.Drawing.Point(0, 62);
            this.tabControlGeneral.Name = "tabControlGeneral";
            this.tabControlGeneral.SelectedIndex = 0;
            this.tabControlGeneral.Size = new System.Drawing.Size(721, 329);
            this.tabControlGeneral.TabIndex = 14;
            // 
            // tabPageAllStats
            // 
            this.tabPageAllStats.Controls.Add(this.cbAllStats);
            this.tabPageAllStats.Controls.Add(this.btnStatSyncron);
            this.tabPageAllStats.Controls.Add(this.comboBoxPlayers);
            this.tabPageAllStats.Controls.Add(this.checkBoxLive);
            this.tabPageAllStats.Controls.Add(this.datGrViewAllStats);
            this.tabPageAllStats.Controls.Add(this.btnAllStat);
            this.tabPageAllStats.Location = new System.Drawing.Point(4, 22);
            this.tabPageAllStats.Name = "tabPageAllStats";
            this.tabPageAllStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAllStats.Size = new System.Drawing.Size(713, 303);
            this.tabPageAllStats.TabIndex = 0;
            this.tabPageAllStats.Text = "All stats";
            this.tabPageAllStats.UseVisualStyleBackColor = true;
            // 
            // cbAllStats
            // 
            this.cbAllStats.AutoSize = true;
            this.cbAllStats.Checked = true;
            this.cbAllStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllStats.Location = new System.Drawing.Point(329, 10);
            this.cbAllStats.Name = "cbAllStats";
            this.cbAllStats.Size = new System.Drawing.Size(62, 17);
            this.cbAllStats.TabIndex = 5;
            this.cbAllStats.Text = "All stats";
            this.cbAllStats.UseVisualStyleBackColor = true;
            // 
            // btnStatSyncron
            // 
            this.btnStatSyncron.Location = new System.Drawing.Point(120, 6);
            this.btnStatSyncron.Name = "btnStatSyncron";
            this.btnStatSyncron.Size = new System.Drawing.Size(108, 23);
            this.btnStatSyncron.TabIndex = 4;
            this.btnStatSyncron.Text = "Synchronously";
            this.btnStatSyncron.UseVisualStyleBackColor = true;
            this.btnStatSyncron.Click += new System.EventHandler(this.btnStatSyncron_Click);
            // 
            // comboBoxPlayers
            // 
            this.comboBoxPlayers.FormattingEnabled = true;
            this.comboBoxPlayers.Location = new System.Drawing.Point(397, 8);
            this.comboBoxPlayers.Name = "comboBoxPlayers";
            this.comboBoxPlayers.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPlayers.TabIndex = 3;
            // 
            // checkBoxLive
            // 
            this.checkBoxLive.AutoSize = true;
            this.checkBoxLive.Location = new System.Drawing.Point(241, 10);
            this.checkBoxLive.Name = "checkBoxLive";
            this.checkBoxLive.Size = new System.Drawing.Size(82, 17);
            this.checkBoxLive.TabIndex = 2;
            this.checkBoxLive.Text = "Live players";
            this.checkBoxLive.UseVisualStyleBackColor = true;
            // 
            // datGrViewAllStats
            // 
            this.datGrViewAllStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datGrViewAllStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.datGrViewAllStats.Location = new System.Drawing.Point(3, 35);
            this.datGrViewAllStats.Name = "datGrViewAllStats";
            this.datGrViewAllStats.Size = new System.Drawing.Size(707, 265);
            this.datGrViewAllStats.TabIndex = 1;
            // 
            // btnAllStat
            // 
            this.btnAllStat.Location = new System.Drawing.Point(6, 6);
            this.btnAllStat.Name = "btnAllStat";
            this.btnAllStat.Size = new System.Drawing.Size(108, 23);
            this.btnAllStat.TabIndex = 0;
            this.btnAllStat.Text = "Asynchronously";
            this.btnAllStat.UseVisualStyleBackColor = true;
            this.btnAllStat.Click += new System.EventHandler(this.btnAllStat_Click);
            // 
            // tabPageSimpleParsing
            // 
            this.tabPageSimpleParsing.Controls.Add(this.lblGamesCount);
            this.tabPageSimpleParsing.Controls.Add(this.lblSimpPars);
            this.tabPageSimpleParsing.Controls.Add(this.progBarSimpleParsing);
            this.tabPageSimpleParsing.Controls.Add(this.textBoxSimpleParsing);
            this.tabPageSimpleParsing.Controls.Add(this.btnSimplePars);
            this.tabPageSimpleParsing.Location = new System.Drawing.Point(4, 22);
            this.tabPageSimpleParsing.Name = "tabPageSimpleParsing";
            this.tabPageSimpleParsing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSimpleParsing.Size = new System.Drawing.Size(713, 303);
            this.tabPageSimpleParsing.TabIndex = 1;
            this.tabPageSimpleParsing.Text = "Simple parsing";
            this.tabPageSimpleParsing.UseVisualStyleBackColor = true;
            // 
            // lblGamesCount
            // 
            this.lblGamesCount.AutoSize = true;
            this.lblGamesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGamesCount.Location = new System.Drawing.Point(136, 11);
            this.lblGamesCount.Name = "lblGamesCount";
            this.lblGamesCount.Size = new System.Drawing.Size(16, 17);
            this.lblGamesCount.TabIndex = 5;
            this.lblGamesCount.Text = "0";
            // 
            // lblSimpPars
            // 
            this.lblSimpPars.AutoSize = true;
            this.lblSimpPars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSimpPars.Location = new System.Drawing.Point(53, 13);
            this.lblSimpPars.Name = "lblSimpPars";
            this.lblSimpPars.Size = new System.Drawing.Size(77, 13);
            this.lblSimpPars.TabIndex = 4;
            this.lblSimpPars.Text = "Parsed games:";
            // 
            // progBarSimpleParsing
            // 
            this.progBarSimpleParsing.Location = new System.Drawing.Point(610, 6);
            this.progBarSimpleParsing.Name = "progBarSimpleParsing";
            this.progBarSimpleParsing.Size = new System.Drawing.Size(100, 23);
            this.progBarSimpleParsing.TabIndex = 3;
            // 
            // textBoxSimpleParsing
            // 
            this.textBoxSimpleParsing.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxSimpleParsing.Location = new System.Drawing.Point(3, 35);
            this.textBoxSimpleParsing.Multiline = true;
            this.textBoxSimpleParsing.Name = "textBoxSimpleParsing";
            this.textBoxSimpleParsing.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSimpleParsing.Size = new System.Drawing.Size(707, 265);
            this.textBoxSimpleParsing.TabIndex = 2;
            // 
            // btnSimplePars
            // 
            this.btnSimplePars.Location = new System.Drawing.Point(3, 6);
            this.btnSimplePars.Name = "btnSimplePars";
            this.btnSimplePars.Size = new System.Drawing.Size(44, 23);
            this.btnSimplePars.TabIndex = 1;
            this.btnSimplePars.Text = "Try";
            this.btnSimplePars.UseVisualStyleBackColor = true;
            this.btnSimplePars.Click += new System.EventHandler(this.btnSimplePars_Click);
            // 
            // MmLightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(721, 391);
            this.Controls.Add(this.tabControlGeneral);
            this.Controls.Add(this.pictureBoxTrack);
            this.Controls.Add(this.pictureBoxAbout);
            this.Controls.Add(this.pictureBoxSettings);
            this.Controls.Add(this.pictureBoxSkin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MmLightForm";
            this.Text = "Money maker light";
            this.Load += new System.EventHandler(this.MmLightForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSkin)).EndInit();
            this.tabControlGeneral.ResumeLayout(false);
            this.tabPageAllStats.ResumeLayout(false);
            this.tabPageAllStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datGrViewAllStats)).EndInit();
            this.tabPageSimpleParsing.ResumeLayout(false);
            this.tabPageSimpleParsing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxTrack;
        private System.Windows.Forms.PictureBox pictureBoxSettings;
        private System.Windows.Forms.PictureBox pictureBoxAbout;
        private System.Windows.Forms.PictureBox pictureBoxSkin;
        private System.Windows.Forms.TabControl tabControlGeneral;
        private System.Windows.Forms.TabPage tabPageAllStats;
        private System.Windows.Forms.TabPage tabPageSimpleParsing;
        private System.Windows.Forms.Button btnAllStat;
        private System.Windows.Forms.Button btnSimplePars;
        private System.Windows.Forms.TextBox textBoxSimpleParsing;
        private System.Windows.Forms.ProgressBar progBarSimpleParsing;
        private System.Windows.Forms.Label lblSimpPars;
        private System.Windows.Forms.DataGridView datGrViewAllStats;
        private System.Windows.Forms.CheckBox checkBoxLive;
        private System.Windows.Forms.ComboBox comboBoxPlayers;
        private System.Windows.Forms.Label lblGamesCount;
        private System.Windows.Forms.Button btnStatSyncron;
        private System.Windows.Forms.CheckBox cbAllStats;
    }
}

