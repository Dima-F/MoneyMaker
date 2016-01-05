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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxSettings = new System.Windows.Forms.PictureBox();
            this.pictureBoxTrack = new System.Windows.Forms.PictureBox();
            this.muckLabel = new System.Windows.Forms.Label();
            this.labelHeroCards = new System.Windows.Forms.Label();
            this.pictureBoxMuck2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxMuck1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxHero2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxHero1 = new System.Windows.Forms.PictureBox();
            this.tabControlStats = new System.Windows.Forms.TabControl();
            this.tabPgGraph = new System.Windows.Forms.TabPage();
            this.profitChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPgTable = new System.Windows.Forms.TabPage();
            this.hudInfoTxtBx = new System.Windows.Forms.TextBox();
            this.tabPgStats = new System.Windows.Forms.TabPage();
            this.hudGrdVw = new System.Windows.Forms.DataGridView();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero1)).BeginInit();
            this.tabControlStats.SuspendLayout();
            this.tabPgGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).BeginInit();
            this.tabPgTable.SuspendLayout();
            this.tabPgStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hudGrdVw)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.pictureBox1);
            this.menuPanel.Controls.Add(this.pictureBoxSettings);
            this.menuPanel.Controls.Add(this.pictureBoxTrack);
            this.menuPanel.Controls.Add(this.muckLabel);
            this.menuPanel.Controls.Add(this.labelHeroCards);
            this.menuPanel.Controls.Add(this.pictureBoxMuck2);
            this.menuPanel.Controls.Add(this.pictureBoxMuck1);
            this.menuPanel.Controls.Add(this.pictureBoxHero2);
            this.menuPanel.Controls.Add(this.pictureBoxHero1);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(180, 196);
            this.menuPanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::MoneyMaker.UI.Light.Properties.Resources.blue_info;
            this.pictureBox1.Location = new System.Drawing.Point(8, 122);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxSettings
            // 
            this.pictureBoxSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSettings.Image = global::MoneyMaker.UI.Light.Properties.Resources.green_settings;
            this.pictureBoxSettings.Location = new System.Drawing.Point(8, 66);
            this.pictureBoxSettings.Name = "pictureBoxSettings";
            this.pictureBoxSettings.Size = new System.Drawing.Size(56, 56);
            this.pictureBoxSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSettings.TabIndex = 12;
            this.pictureBoxSettings.TabStop = false;
            this.pictureBoxSettings.Click += new System.EventHandler(this.pictureBoxSettings_Click);
            // 
            // pictureBoxTrack
            // 
            this.pictureBoxTrack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxTrack.Location = new System.Drawing.Point(8, 9);
            this.pictureBoxTrack.Name = "pictureBoxTrack";
            this.pictureBoxTrack.Size = new System.Drawing.Size(56, 56);
            this.pictureBoxTrack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxTrack.TabIndex = 11;
            this.pictureBoxTrack.TabStop = false;
            this.pictureBoxTrack.Click += new System.EventHandler(this.pictureBoxTrack_Click);
            // 
            // muckLabel
            // 
            this.muckLabel.AutoSize = true;
            this.muckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.muckLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.muckLabel.Location = new System.Drawing.Point(93, 97);
            this.muckLabel.Name = "muckLabel";
            this.muckLabel.Size = new System.Drawing.Size(52, 13);
            this.muckLabel.TabIndex = 10;
            this.muckLabel.Text = "Mucking :";
            // 
            // labelHeroCards
            // 
            this.labelHeroCards.AutoSize = true;
            this.labelHeroCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeroCards.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelHeroCards.Location = new System.Drawing.Point(65, 9);
            this.labelHeroCards.Name = "labelHeroCards";
            this.labelHeroCards.Size = new System.Drawing.Size(111, 13);
            this.labelHeroCards.TabIndex = 2;
            this.labelHeroCards.Text = "Last game hero cards:";
            // 
            // pictureBoxMuck2
            // 
            this.pictureBoxMuck2.Location = new System.Drawing.Point(123, 113);
            this.pictureBoxMuck2.Name = "pictureBoxMuck2";
            this.pictureBoxMuck2.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxMuck2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMuck2.TabIndex = 9;
            this.pictureBoxMuck2.TabStop = false;
            // 
            // pictureBoxMuck1
            // 
            this.pictureBoxMuck1.Location = new System.Drawing.Point(73, 113);
            this.pictureBoxMuck1.Name = "pictureBoxMuck1";
            this.pictureBoxMuck1.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxMuck1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMuck1.TabIndex = 8;
            this.pictureBoxMuck1.TabStop = false;
            // 
            // pictureBoxHero2
            // 
            this.pictureBoxHero2.Location = new System.Drawing.Point(123, 25);
            this.pictureBoxHero2.Name = "pictureBoxHero2";
            this.pictureBoxHero2.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxHero2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHero2.TabIndex = 7;
            this.pictureBoxHero2.TabStop = false;
            // 
            // pictureBoxHero1
            // 
            this.pictureBoxHero1.Location = new System.Drawing.Point(73, 25);
            this.pictureBoxHero1.Name = "pictureBoxHero1";
            this.pictureBoxHero1.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxHero1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHero1.TabIndex = 6;
            this.pictureBoxHero1.TabStop = false;
            // 
            // tabControlStats
            // 
            this.tabControlStats.Controls.Add(this.tabPgTable);
            this.tabControlStats.Controls.Add(this.tabPgStats);
            this.tabControlStats.Controls.Add(this.tabPgGraph);
            this.tabControlStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStats.Location = new System.Drawing.Point(180, 0);
            this.tabControlStats.Name = "tabControlStats";
            this.tabControlStats.SelectedIndex = 0;
            this.tabControlStats.Size = new System.Drawing.Size(704, 196);
            this.tabControlStats.TabIndex = 1;
            // 
            // tabPgGraph
            // 
            this.tabPgGraph.BackColor = System.Drawing.Color.Gray;
            this.tabPgGraph.Controls.Add(this.profitChart);
            this.tabPgGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPgGraph.Name = "tabPgGraph";
            this.tabPgGraph.Size = new System.Drawing.Size(696, 170);
            this.tabPgGraph.TabIndex = 2;
            this.tabPgGraph.Text = "Graphic";
            // 
            // profitChart
            // 
            chartArea2.Name = "ChartArea1";
            this.profitChart.ChartAreas.Add(chartArea2);
            this.profitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.profitChart.Legends.Add(legend2);
            this.profitChart.Location = new System.Drawing.Point(0, 0);
            this.profitChart.Name = "profitChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.profitChart.Series.Add(series2);
            this.profitChart.Size = new System.Drawing.Size(696, 170);
            this.profitChart.TabIndex = 6;
            this.profitChart.Text = "chart1";
            // 
            // tabPgTable
            // 
            this.tabPgTable.BackColor = System.Drawing.Color.Gray;
            this.tabPgTable.Controls.Add(this.hudInfoTxtBx);
            this.tabPgTable.Location = new System.Drawing.Point(4, 22);
            this.tabPgTable.Name = "tabPgTable";
            this.tabPgTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgTable.Size = new System.Drawing.Size(696, 170);
            this.tabPgTable.TabIndex = 0;
            this.tabPgTable.Text = "Table";
            // 
            // hudInfoTxtBx
            // 
            this.hudInfoTxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudInfoTxtBx.Location = new System.Drawing.Point(3, 3);
            this.hudInfoTxtBx.Multiline = true;
            this.hudInfoTxtBx.Name = "hudInfoTxtBx";
            this.hudInfoTxtBx.Size = new System.Drawing.Size(690, 164);
            this.hudInfoTxtBx.TabIndex = 1;
            // 
            // tabPgStats
            // 
            this.tabPgStats.BackColor = System.Drawing.Color.Gray;
            this.tabPgStats.Controls.Add(this.hudGrdVw);
            this.tabPgStats.Location = new System.Drawing.Point(4, 22);
            this.tabPgStats.Name = "tabPgStats";
            this.tabPgStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgStats.Size = new System.Drawing.Size(696, 170);
            this.tabPgStats.TabIndex = 1;
            this.tabPgStats.Text = "Stats";
            // 
            // hudGrdVw
            // 
            this.hudGrdVw.BackgroundColor = System.Drawing.Color.Gray;
            this.hudGrdVw.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hudGrdVw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.hudGrdVw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.hudGrdVw.DefaultCellStyle = dataGridViewCellStyle5;
            this.hudGrdVw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudGrdVw.Location = new System.Drawing.Point(3, 3);
            this.hudGrdVw.Name = "hudGrdVw";
            this.hudGrdVw.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hudGrdVw.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.hudGrdVw.RowHeadersWidth = 30;
            this.hudGrdVw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.hudGrdVw.RowTemplate.Height = 18;
            this.hudGrdVw.Size = new System.Drawing.Size(690, 164);
            this.hudGrdVw.TabIndex = 4;
            // 
            // MmLightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(884, 196);
            this.Controls.Add(this.tabControlStats);
            this.Controls.Add(this.menuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MmLightForm";
            this.Text = "Money maker light";
            this.Load += new System.EventHandler(this.MmLightForm_Load);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero1)).EndInit();
            this.tabControlStats.ResumeLayout(false);
            this.tabPgGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).EndInit();
            this.tabPgTable.ResumeLayout(false);
            this.tabPgTable.PerformLayout();
            this.tabPgStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hudGrdVw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.TabControl tabControlStats;
        private System.Windows.Forms.TabPage tabPgTable;
        private System.Windows.Forms.TabPage tabPgStats;
        private System.Windows.Forms.TabPage tabPgGraph;
        private System.Windows.Forms.PictureBox pictureBoxMuck2;
        private System.Windows.Forms.PictureBox pictureBoxMuck1;
        private System.Windows.Forms.PictureBox pictureBoxHero2;
        private System.Windows.Forms.PictureBox pictureBoxHero1;
        private System.Windows.Forms.TextBox hudInfoTxtBx;
        private System.Windows.Forms.DataGridView hudGrdVw;
        private System.Windows.Forms.Label muckLabel;
        private System.Windows.Forms.Label labelHeroCards;
        private System.Windows.Forms.PictureBox pictureBoxTrack;
        private System.Windows.Forms.PictureBox pictureBoxSettings;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart profitChart;
    }
}

