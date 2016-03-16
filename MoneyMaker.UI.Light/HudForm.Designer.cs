namespace MoneyMaker.UI.Light
{
    partial class HudForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControlStats = new System.Windows.Forms.TabControl();
            this.tabPgTable = new System.Windows.Forms.TabPage();
            this.pictureBoxMuck2 = new System.Windows.Forms.PictureBox();
            this.muckLabel = new System.Windows.Forms.Label();
            this.pictureBoxMuck1 = new System.Windows.Forms.PictureBox();
            this.hudInfoTxtBx = new System.Windows.Forms.TextBox();
            this.labelHeroCards = new System.Windows.Forms.Label();
            this.pictureBoxHero1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxHero2 = new System.Windows.Forms.PictureBox();
            this.tabPgStats = new System.Windows.Forms.TabPage();
            this.hudGrdVw = new System.Windows.Forms.DataGridView();
            this.tabPgGraph = new System.Windows.Forms.TabPage();
            this.profitChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControlStats.SuspendLayout();
            this.tabPgTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero2)).BeginInit();
            this.tabPgStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hudGrdVw)).BeginInit();
            this.tabPgGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlStats
            // 
            this.tabControlStats.Controls.Add(this.tabPgTable);
            this.tabControlStats.Controls.Add(this.tabPgStats);
            this.tabControlStats.Controls.Add(this.tabPgGraph);
            this.tabControlStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlStats.Location = new System.Drawing.Point(0, 0);
            this.tabControlStats.Name = "tabControlStats";
            this.tabControlStats.SelectedIndex = 0;
            this.tabControlStats.Size = new System.Drawing.Size(508, 214);
            this.tabControlStats.TabIndex = 2;
            // 
            // tabPgTable
            // 
            this.tabPgTable.BackColor = System.Drawing.Color.Gray;
            this.tabPgTable.Controls.Add(this.pictureBoxMuck2);
            this.tabPgTable.Controls.Add(this.muckLabel);
            this.tabPgTable.Controls.Add(this.pictureBoxMuck1);
            this.tabPgTable.Controls.Add(this.hudInfoTxtBx);
            this.tabPgTable.Controls.Add(this.labelHeroCards);
            this.tabPgTable.Controls.Add(this.pictureBoxHero1);
            this.tabPgTable.Controls.Add(this.pictureBoxHero2);
            this.tabPgTable.Location = new System.Drawing.Point(4, 22);
            this.tabPgTable.Name = "tabPgTable";
            this.tabPgTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgTable.Size = new System.Drawing.Size(500, 188);
            this.tabPgTable.TabIndex = 0;
            this.tabPgTable.Text = "Table";
            // 
            // pictureBoxMuck2
            // 
            this.pictureBoxMuck2.Location = new System.Drawing.Point(411, 106);
            this.pictureBoxMuck2.Name = "pictureBoxMuck2";
            this.pictureBoxMuck2.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxMuck2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMuck2.TabIndex = 15;
            this.pictureBoxMuck2.TabStop = false;
            // 
            // muckLabel
            // 
            this.muckLabel.AutoSize = true;
            this.muckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.muckLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.muckLabel.Location = new System.Drawing.Point(377, 91);
            this.muckLabel.Name = "muckLabel";
            this.muckLabel.Size = new System.Drawing.Size(52, 13);
            this.muckLabel.TabIndex = 16;
            this.muckLabel.Text = "Mucking :";
            // 
            // pictureBoxMuck1
            // 
            this.pictureBoxMuck1.Location = new System.Drawing.Point(345, 105);
            this.pictureBoxMuck1.Name = "pictureBoxMuck1";
            this.pictureBoxMuck1.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxMuck1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMuck1.TabIndex = 14;
            this.pictureBoxMuck1.TabStop = false;
            // 
            // hudInfoTxtBx
            // 
            this.hudInfoTxtBx.Dock = System.Windows.Forms.DockStyle.Left;
            this.hudInfoTxtBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hudInfoTxtBx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.hudInfoTxtBx.Location = new System.Drawing.Point(3, 3);
            this.hudInfoTxtBx.Multiline = true;
            this.hudInfoTxtBx.Name = "hudInfoTxtBx";
            this.hudInfoTxtBx.Size = new System.Drawing.Size(299, 182);
            this.hudInfoTxtBx.TabIndex = 1;
            // 
            // labelHeroCards
            // 
            this.labelHeroCards.AutoSize = true;
            this.labelHeroCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeroCards.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelHeroCards.Location = new System.Drawing.Point(342, 3);
            this.labelHeroCards.Name = "labelHeroCards";
            this.labelHeroCards.Size = new System.Drawing.Size(111, 13);
            this.labelHeroCards.TabIndex = 11;
            this.labelHeroCards.Text = "Last game hero cards:";
            // 
            // pictureBoxHero1
            // 
            this.pictureBoxHero1.Location = new System.Drawing.Point(345, 19);
            this.pictureBoxHero1.Name = "pictureBoxHero1";
            this.pictureBoxHero1.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxHero1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHero1.TabIndex = 12;
            this.pictureBoxHero1.TabStop = false;
            // 
            // pictureBoxHero2
            // 
            this.pictureBoxHero2.Location = new System.Drawing.Point(411, 19);
            this.pictureBoxHero2.Name = "pictureBoxHero2";
            this.pictureBoxHero2.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxHero2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHero2.TabIndex = 13;
            this.pictureBoxHero2.TabStop = false;
            // 
            // tabPgStats
            // 
            this.tabPgStats.BackColor = System.Drawing.Color.Gray;
            this.tabPgStats.Controls.Add(this.hudGrdVw);
            this.tabPgStats.Location = new System.Drawing.Point(4, 22);
            this.tabPgStats.Name = "tabPgStats";
            this.tabPgStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgStats.Size = new System.Drawing.Size(500, 188);
            this.tabPgStats.TabIndex = 1;
            this.tabPgStats.Text = "Stats";
            // 
            // hudGrdVw
            // 
            this.hudGrdVw.BackgroundColor = System.Drawing.Color.Gray;
            this.hudGrdVw.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hudGrdVw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.hudGrdVw.DefaultCellStyle = dataGridViewCellStyle2;
            this.hudGrdVw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudGrdVw.Location = new System.Drawing.Point(3, 3);
            this.hudGrdVw.Name = "hudGrdVw";
            this.hudGrdVw.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hudGrdVw.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.hudGrdVw.RowHeadersWidth = 20;
            this.hudGrdVw.RowTemplate.Height = 18;
            this.hudGrdVw.Size = new System.Drawing.Size(494, 182);
            this.hudGrdVw.TabIndex = 4;
            // 
            // tabPgGraph
            // 
            this.tabPgGraph.BackColor = System.Drawing.Color.Gray;
            this.tabPgGraph.Controls.Add(this.profitChart);
            this.tabPgGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPgGraph.Name = "tabPgGraph";
            this.tabPgGraph.Size = new System.Drawing.Size(500, 188);
            this.tabPgGraph.TabIndex = 2;
            this.tabPgGraph.Text = "Graphic";
            // 
            // profitChart
            // 
            chartArea1.Name = "ChartArea1";
            this.profitChart.ChartAreas.Add(chartArea1);
            this.profitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.profitChart.Legends.Add(legend1);
            this.profitChart.Location = new System.Drawing.Point(0, 0);
            this.profitChart.Name = "profitChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.profitChart.Series.Add(series1);
            this.profitChart.Size = new System.Drawing.Size(500, 188);
            this.profitChart.TabIndex = 6;
            this.profitChart.Text = "chart1";
            // 
            // HudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(508, 214);
            this.Controls.Add(this.tabControlStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HudForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HudForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HudForm_FormClosing);
            this.Load += new System.EventHandler(this.HudForm_Load);
            this.tabControlStats.ResumeLayout(false);
            this.tabPgTable.ResumeLayout(false);
            this.tabPgTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero2)).EndInit();
            this.tabPgStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hudGrdVw)).EndInit();
            this.tabPgGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlStats;
        private System.Windows.Forms.TabPage tabPgTable;
        private System.Windows.Forms.TextBox hudInfoTxtBx;
        private System.Windows.Forms.TabPage tabPgStats;
        private System.Windows.Forms.DataGridView hudGrdVw;
        private System.Windows.Forms.TabPage tabPgGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart profitChart;
        private System.Windows.Forms.Label muckLabel;
        private System.Windows.Forms.Label labelHeroCards;
        private System.Windows.Forms.PictureBox pictureBoxMuck2;
        private System.Windows.Forms.PictureBox pictureBoxMuck1;
        private System.Windows.Forms.PictureBox pictureBoxHero2;
        private System.Windows.Forms.PictureBox pictureBoxHero1;
    }
}