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
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reinitializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.fbdHistory = new System.Windows.Forms.FolderBrowserDialog();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tabCntrl = new System.Windows.Forms.TabControl();
            this.tabPgLoad = new System.Windows.Forms.TabPage();
            this.tabPgWatcher = new System.Windows.Forms.TabPage();
            this.tabPgHud = new System.Windows.Forms.TabPage();
            this.dataGrdVwHud = new System.Windows.Forms.DataGridView();
            this.bndSrdHandHistories = new System.Windows.Forms.BindingSource(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.tabCntrl.SuspendLayout();
            this.tabPgLoad.SuspendLayout();
            this.tabPgWatcher.SuspendLayout();
            this.tabPgHud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrdVwHud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndSrdHandHistories)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.trackingToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(705, 24);
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
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.historyToolStripMenuItem.Text = "History";
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
            // textBoxInfo
            // 
            this.textBoxInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxInfo.Location = new System.Drawing.Point(3, 3);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(493, 380);
            this.textBoxInfo.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(3, 3);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(133, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Waiting for file changing....";
            // 
            // tabCntrl
            // 
            this.tabCntrl.Controls.Add(this.tabPgLoad);
            this.tabCntrl.Controls.Add(this.tabPgWatcher);
            this.tabCntrl.Controls.Add(this.tabPgHud);
            this.tabCntrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCntrl.Location = new System.Drawing.Point(0, 24);
            this.tabCntrl.Name = "tabCntrl";
            this.tabCntrl.SelectedIndex = 0;
            this.tabCntrl.Size = new System.Drawing.Size(705, 412);
            this.tabCntrl.TabIndex = 3;
            // 
            // tabPgLoad
            // 
            this.tabPgLoad.Controls.Add(this.textBoxInfo);
            this.tabPgLoad.Location = new System.Drawing.Point(4, 22);
            this.tabPgLoad.Name = "tabPgLoad";
            this.tabPgLoad.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgLoad.Size = new System.Drawing.Size(697, 386);
            this.tabPgLoad.TabIndex = 0;
            this.tabPgLoad.Text = "Load";
            this.tabPgLoad.UseVisualStyleBackColor = true;
            // 
            // tabPgWatcher
            // 
            this.tabPgWatcher.Controls.Add(this.lblInfo);
            this.tabPgWatcher.Location = new System.Drawing.Point(4, 22);
            this.tabPgWatcher.Name = "tabPgWatcher";
            this.tabPgWatcher.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgWatcher.Size = new System.Drawing.Size(697, 386);
            this.tabPgWatcher.TabIndex = 1;
            this.tabPgWatcher.Text = "Watch";
            this.tabPgWatcher.UseVisualStyleBackColor = true;
            // 
            // tabPgHud
            // 
            this.tabPgHud.Controls.Add(this.dataGrdVwHud);
            this.tabPgHud.Location = new System.Drawing.Point(4, 22);
            this.tabPgHud.Name = "tabPgHud";
            this.tabPgHud.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgHud.Size = new System.Drawing.Size(697, 386);
            this.tabPgHud.TabIndex = 2;
            this.tabPgHud.Text = "Hud";
            this.tabPgHud.UseVisualStyleBackColor = true;
            // 
            // dataGrdVwHud
            // 
            this.dataGrdVwHud.AutoGenerateColumns = false;
            this.dataGrdVwHud.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrdVwHud.DataSource = this.bndSrdHandHistories;
            this.dataGrdVwHud.Location = new System.Drawing.Point(0, 3);
            this.dataGrdVwHud.Name = "dataGrdVwHud";
            this.dataGrdVwHud.Size = new System.Drawing.Size(697, 315);
            this.dataGrdVwHud.TabIndex = 0;
            // 
            // MmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 436);
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
            this.tabCntrl.ResumeLayout(false);
            this.tabPgLoad.ResumeLayout(false);
            this.tabPgLoad.PerformLayout();
            this.tabPgWatcher.ResumeLayout(false);
            this.tabPgWatcher.PerformLayout();
            this.tabPgHud.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrdVwHud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndSrdHandHistories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.FolderBrowserDialog fbdHistory;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ToolStripMenuItem trackingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.TabControl tabCntrl;
        private System.Windows.Forms.TabPage tabPgLoad;
        private System.Windows.Forms.TabPage tabPgWatcher;
        private System.Windows.Forms.TabPage tabPgHud;
        private System.Windows.Forms.DataGridView dataGrdVwHud;
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
    }
}

