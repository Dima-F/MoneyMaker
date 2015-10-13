namespace HandHistories.Parser.MoneyMaker
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
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinitializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGeneralStatisticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdHistory = new System.Windows.Forms.FolderBrowserDialog();
            this.bndSrdHandHistories = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxWatch = new System.Windows.Forms.TextBox();
            this.tabPgHud = new System.Windows.Forms.TabPage();
            this.gridHud = new System.Windows.Forms.DataGridView();
            this.tabPgStat = new System.Windows.Forms.TabPage();
            this.nameList = new System.Windows.Forms.ListBox();
            this.gridStat = new System.Windows.Forms.DataGridView();
            this.tabCntrl = new System.Windows.Forms.TabControl();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bndSrdHandHistories)).BeginInit();
            this.tabPgHud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHud)).BeginInit();
            this.tabPgStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStat)).BeginInit();
            this.tabCntrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.trackingToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.statisticsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(704, 24);
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
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
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
            this.reinitializeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.reinitializeToolStripMenuItem.Text = "Reinitialize";
            this.reinitializeToolStripMenuItem.Click += new System.EventHandler(this.reinitializeToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGeneralStatisticToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.statisticsToolStripMenuItem.Text = "Statistic";
            // 
            // showGeneralStatisticToolStripMenuItem
            // 
            this.showGeneralStatisticToolStripMenuItem.Name = "showGeneralStatisticToolStripMenuItem";
            this.showGeneralStatisticToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showGeneralStatisticToolStripMenuItem.Text = "Load general statistic";
            this.showGeneralStatisticToolStripMenuItem.Click += new System.EventHandler(this.loadGeneralStatisticToolStripMenuItem_Click);
            // 
            // textBoxWatch
            // 
            this.textBoxWatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxWatch.ForeColor = System.Drawing.Color.Navy;
            this.textBoxWatch.Location = new System.Drawing.Point(0, 370);
            this.textBoxWatch.Multiline = true;
            this.textBoxWatch.Name = "textBoxWatch";
            this.textBoxWatch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxWatch.Size = new System.Drawing.Size(704, 92);
            this.textBoxWatch.TabIndex = 4;
            this.textBoxWatch.Text = "Waiting for file changing...";
            // 
            // tabPgHud
            // 
            this.tabPgHud.Controls.Add(this.gridHud);
            this.tabPgHud.Location = new System.Drawing.Point(4, 22);
            this.tabPgHud.Name = "tabPgHud";
            this.tabPgHud.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgHud.Size = new System.Drawing.Size(696, 320);
            this.tabPgHud.TabIndex = 2;
            this.tabPgHud.Text = "Hud";
            this.tabPgHud.UseVisualStyleBackColor = true;
            // 
            // gridHud
            // 
            this.gridHud.AutoGenerateColumns = false;
            this.gridHud.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHud.DataSource = this.bndSrdHandHistories;
            this.gridHud.Location = new System.Drawing.Point(0, 3);
            this.gridHud.Name = "gridHud";
            this.gridHud.Size = new System.Drawing.Size(697, 315);
            this.gridHud.TabIndex = 0;
            // 
            // tabPgStat
            // 
            this.tabPgStat.Controls.Add(this.textBoxSearch);
            this.tabPgStat.Controls.Add(this.nameList);
            this.tabPgStat.Controls.Add(this.gridStat);
            this.tabPgStat.Location = new System.Drawing.Point(4, 22);
            this.tabPgStat.Name = "tabPgStat";
            this.tabPgStat.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgStat.Size = new System.Drawing.Size(696, 320);
            this.tabPgStat.TabIndex = 0;
            this.tabPgStat.Text = "Statistics";
            this.tabPgStat.UseVisualStyleBackColor = true;
            // 
            // nameList
            // 
            this.nameList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nameList.FormattingEnabled = true;
            this.nameList.Location = new System.Drawing.Point(513, 27);
            this.nameList.Name = "nameList";
            this.nameList.Size = new System.Drawing.Size(180, 290);
            this.nameList.TabIndex = 2;
            // 
            // gridStat
            // 
            this.gridStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStat.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridStat.Location = new System.Drawing.Point(3, 3);
            this.gridStat.Name = "gridStat";
            this.gridStat.Size = new System.Drawing.Size(510, 314);
            this.gridStat.TabIndex = 1;
            // 
            // tabCntrl
            // 
            this.tabCntrl.Controls.Add(this.tabPgStat);
            this.tabCntrl.Controls.Add(this.tabPgHud);
            this.tabCntrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabCntrl.Location = new System.Drawing.Point(0, 24);
            this.tabCntrl.Name = "tabCntrl";
            this.tabCntrl.SelectedIndex = 0;
            this.tabCntrl.Size = new System.Drawing.Size(704, 346);
            this.tabCntrl.TabIndex = 3;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxSearch.Location = new System.Drawing.Point(513, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 20);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.Text = "Search...";
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // MmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 462);
            this.Controls.Add(this.textBoxWatch);
            this.Controls.Add(this.tabCntrl);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MmForm";
            this.Text = "Money Maker App";
            this.Load += new System.EventHandler(this.MmForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bndSrdHandHistories)).EndInit();
            this.tabPgHud.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHud)).EndInit();
            this.tabPgStat.ResumeLayout(false);
            this.tabPgStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStat)).EndInit();
            this.tabCntrl.ResumeLayout(false);
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
        private System.Windows.Forms.BindingSource bndSrdHandHistories;
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
        private System.Windows.Forms.TabPage tabPgHud;
        private System.Windows.Forms.DataGridView gridHud;
        private System.Windows.Forms.TabPage tabPgStat;
        private System.Windows.Forms.TabControl tabCntrl;
        private System.Windows.Forms.DataGridView gridStat;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGeneralStatisticToolStripMenuItem;
        private System.Windows.Forms.ListBox nameList;
        private System.Windows.Forms.TextBox textBoxSearch;
    }
}

