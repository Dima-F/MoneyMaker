﻿namespace HandHistories.Parser.MoneyMaker
{
    partial class MmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MmForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinitializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdHistory = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxWatch = new System.Windows.Forms.TextBox();
            this.tabPgGeneral = new System.Windows.Forms.TabPage();
            this.gridSummary = new System.Windows.Forms.DataGridView();
            this.nameList = new System.Windows.Forms.ListBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.txBoxPlayerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txBoxSite = new System.Windows.Forms.TextBox();
            this.groupBoxSummary = new System.Windows.Forms.GroupBox();
            this.groupBoxStatistics = new System.Windows.Forms.GroupBox();
            this.gridStatistics = new System.Windows.Forms.DataGridView();
            this.mainMenuStrip.SuspendLayout();
            this.tabPgGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).BeginInit();
            this.mainTab.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.groupBoxSummary.SuspendLayout();
            this.groupBoxStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatistics)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.trackingToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(853, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // trackingToolStripMenuItem
            // 
            this.trackingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem});
            this.trackingToolStripMenuItem.Name = "trackingToolStripMenuItem";
            this.trackingToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.trackingToolStripMenuItem.Text = "Tracking";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reinitializeToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // reinitializeToolStripMenuItem
            // 
            this.reinitializeToolStripMenuItem.Name = "reinitializeToolStripMenuItem";
            this.reinitializeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reinitializeToolStripMenuItem.Text = "Reinitialize";
            this.reinitializeToolStripMenuItem.Click += new System.EventHandler(this.reinitializeToolStripMenuItem_Click);
            // 
            // textBoxWatch
            // 
            this.textBoxWatch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxWatch.ForeColor = System.Drawing.Color.Navy;
            this.textBoxWatch.Location = new System.Drawing.Point(0, 403);
            this.textBoxWatch.Multiline = true;
            this.textBoxWatch.Name = "textBoxWatch";
            this.textBoxWatch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxWatch.Size = new System.Drawing.Size(853, 59);
            this.textBoxWatch.TabIndex = 4;
            this.textBoxWatch.Text = "Waiting for file changing...";
            // 
            // tabPgGeneral
            // 
            this.tabPgGeneral.Controls.Add(this.groupBoxStatistics);
            this.tabPgGeneral.Controls.Add(this.groupBoxSummary);
            this.tabPgGeneral.Controls.Add(this.groupBoxDetails);
            this.tabPgGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPgGeneral.Name = "tabPgGeneral";
            this.tabPgGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgGeneral.Size = new System.Drawing.Size(659, 344);
            this.tabPgGeneral.TabIndex = 0;
            this.tabPgGeneral.Text = "General";
            this.tabPgGeneral.UseVisualStyleBackColor = true;
            // 
            // gridSummary
            // 
            this.gridSummary.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSummary.Location = new System.Drawing.Point(6, 19);
            this.gridSummary.Name = "gridSummary";
            this.gridSummary.Size = new System.Drawing.Size(632, 57);
            this.gridSummary.TabIndex = 1;
            // 
            // nameList
            // 
            this.nameList.FormattingEnabled = true;
            this.nameList.Location = new System.Drawing.Point(669, 68);
            this.nameList.Name = "nameList";
            this.nameList.Size = new System.Drawing.Size(184, 329);
            this.nameList.TabIndex = 2;
            this.nameList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nameList_MouseDoubleClick);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(669, 42);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(184, 20);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.Text = "Search...";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.tabPgGeneral);
            this.mainTab.Location = new System.Drawing.Point(0, 27);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(667, 370);
            this.mainTab.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.txBoxSite);
            this.groupBoxDetails.Controls.Add(this.label2);
            this.groupBoxDetails.Controls.Add(this.txBoxPlayerName);
            this.groupBoxDetails.Controls.Add(this.label1);
            this.groupBoxDetails.Location = new System.Drawing.Point(7, 7);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(646, 85);
            this.groupBoxDetails.TabIndex = 4;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Player Details";
            // 
            // txBoxPlayerName
            // 
            this.txBoxPlayerName.Location = new System.Drawing.Point(60, 22);
            this.txBoxPlayerName.Name = "txBoxPlayerName";
            this.txBoxPlayerName.ReadOnly = true;
            this.txBoxPlayerName.Size = new System.Drawing.Size(142, 20);
            this.txBoxPlayerName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Site:";
            // 
            // txBoxSite
            // 
            this.txBoxSite.Location = new System.Drawing.Point(242, 22);
            this.txBoxSite.Name = "txBoxSite";
            this.txBoxSite.ReadOnly = true;
            this.txBoxSite.Size = new System.Drawing.Size(142, 20);
            this.txBoxSite.TabIndex = 5;
            this.txBoxSite.Text = "Poker888";
            // 
            // groupBoxSummary
            // 
            this.groupBoxSummary.Controls.Add(this.gridSummary);
            this.groupBoxSummary.Location = new System.Drawing.Point(9, 98);
            this.groupBoxSummary.Name = "groupBoxSummary";
            this.groupBoxSummary.Size = new System.Drawing.Size(644, 100);
            this.groupBoxSummary.TabIndex = 5;
            this.groupBoxSummary.TabStop = false;
            this.groupBoxSummary.Text = "Player Summary";
            // 
            // groupBoxStatistics
            // 
            this.groupBoxStatistics.Controls.Add(this.gridStatistics);
            this.groupBoxStatistics.Location = new System.Drawing.Point(9, 204);
            this.groupBoxStatistics.Name = "groupBoxStatistics";
            this.groupBoxStatistics.Size = new System.Drawing.Size(644, 100);
            this.groupBoxStatistics.TabIndex = 6;
            this.groupBoxStatistics.TabStop = false;
            this.groupBoxStatistics.Text = "Player Statistics";
            // 
            // gridStatistics
            // 
            this.gridStatistics.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStatistics.Location = new System.Drawing.Point(6, 22);
            this.gridStatistics.Name = "gridStatistics";
            this.gridStatistics.Size = new System.Drawing.Size(632, 57);
            this.gridStatistics.TabIndex = 2;
            // 
            // MmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 462);
            this.Controls.Add(this.nameList);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.textBoxWatch);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MmForm";
            this.Text = "Money Maker App";
            this.Load += new System.EventHandler(this.MmForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tabPgGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).EndInit();
            this.mainTab.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            this.groupBoxSummary.ResumeLayout(false);
            this.groupBoxStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStatistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdHistory;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn comumnityCardsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heroDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn runItTwiceDataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPlayersActiveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfHandUtcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn handIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealerButtonPositionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cancelledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPlayersSeatedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullHandHistoryTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPotDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rakeDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reinitializeToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxWatch;
        private System.Windows.Forms.TabPage tabPgGeneral;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ListBox nameList;
        private System.Windows.Forms.DataGridView gridSummary;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.GroupBox groupBoxSummary;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.TextBox txBoxSite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txBoxPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxStatistics;
        private System.Windows.Forms.DataGridView gridStatistics;
    }
}

