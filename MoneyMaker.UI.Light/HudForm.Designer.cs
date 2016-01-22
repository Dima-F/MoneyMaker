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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControlStats = new System.Windows.Forms.TabControl();
            this.tabPgTable = new System.Windows.Forms.TabPage();
            this.hudInfoTxtBx = new System.Windows.Forms.TextBox();
            this.tabPgStats = new System.Windows.Forms.TabPage();
            this.hudGrdVw = new System.Windows.Forms.DataGridView();
            this.tabPgGraph = new System.Windows.Forms.TabPage();
            this.profitChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.muckLabel = new System.Windows.Forms.Label();
            this.labelHeroCards = new System.Windows.Forms.Label();
            this.pictureBoxMuck2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxMuck1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxHero2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxHero1 = new System.Windows.Forms.PictureBox();
            this.tabControlStats.SuspendLayout();
            this.tabPgTable.SuspendLayout();
            this.tabPgStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hudGrdVw)).BeginInit();
            this.tabPgGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlStats
            // 
            this.tabControlStats.Controls.Add(this.tabPgTable);
            this.tabControlStats.Controls.Add(this.tabPgStats);
            this.tabControlStats.Controls.Add(this.tabPgGraph);
            this.tabControlStats.Location = new System.Drawing.Point(8, 8);
            this.tabControlStats.Name = "tabControlStats";
            this.tabControlStats.SelectedIndex = 0;
            this.tabControlStats.Size = new System.Drawing.Size(704, 206);
            this.tabControlStats.TabIndex = 2;
            // 
            // tabPgTable
            // 
            this.tabPgTable.BackColor = System.Drawing.Color.Gray;
            this.tabPgTable.Controls.Add(this.hudInfoTxtBx);
            this.tabPgTable.Location = new System.Drawing.Point(4, 22);
            this.tabPgTable.Name = "tabPgTable";
            this.tabPgTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgTable.Size = new System.Drawing.Size(696, 180);
            this.tabPgTable.TabIndex = 0;
            this.tabPgTable.Text = "Table";
            // 
            // hudInfoTxtBx
            // 
            this.hudInfoTxtBx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudInfoTxtBx.Location = new System.Drawing.Point(3, 3);
            this.hudInfoTxtBx.Multiline = true;
            this.hudInfoTxtBx.Name = "hudInfoTxtBx";
            this.hudInfoTxtBx.Size = new System.Drawing.Size(690, 174);
            this.hudInfoTxtBx.TabIndex = 1;
            // 
            // tabPgStats
            // 
            this.tabPgStats.BackColor = System.Drawing.Color.Gray;
            this.tabPgStats.Controls.Add(this.hudGrdVw);
            this.tabPgStats.Location = new System.Drawing.Point(4, 22);
            this.tabPgStats.Name = "tabPgStats";
            this.tabPgStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgStats.Size = new System.Drawing.Size(696, 180);
            this.tabPgStats.TabIndex = 1;
            this.tabPgStats.Text = "Stats";
            // 
            // hudGrdVw
            // 
            this.hudGrdVw.BackgroundColor = System.Drawing.Color.Gray;
            this.hudGrdVw.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hudGrdVw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.hudGrdVw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.hudGrdVw.DefaultCellStyle = dataGridViewCellStyle11;
            this.hudGrdVw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hudGrdVw.Location = new System.Drawing.Point(3, 3);
            this.hudGrdVw.Name = "hudGrdVw";
            this.hudGrdVw.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hudGrdVw.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.hudGrdVw.RowHeadersWidth = 30;
            this.hudGrdVw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.hudGrdVw.RowTemplate.Height = 18;
            this.hudGrdVw.Size = new System.Drawing.Size(690, 174);
            this.hudGrdVw.TabIndex = 4;
            // 
            // tabPgGraph
            // 
            this.tabPgGraph.BackColor = System.Drawing.Color.Gray;
            this.tabPgGraph.Controls.Add(this.profitChart);
            this.tabPgGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPgGraph.Name = "tabPgGraph";
            this.tabPgGraph.Size = new System.Drawing.Size(696, 180);
            this.tabPgGraph.TabIndex = 2;
            this.tabPgGraph.Text = "Graphic";
            // 
            // profitChart
            // 
            chartArea4.Name = "ChartArea1";
            this.profitChart.ChartAreas.Add(chartArea4);
            this.profitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.profitChart.Legends.Add(legend4);
            this.profitChart.Location = new System.Drawing.Point(0, 0);
            this.profitChart.Name = "profitChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.profitChart.Series.Add(series4);
            this.profitChart.Size = new System.Drawing.Size(696, 180);
            this.profitChart.TabIndex = 6;
            this.profitChart.Text = "chart1";
            // 
            // muckLabel
            // 
            this.muckLabel.AutoSize = true;
            this.muckLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.muckLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.muckLabel.Location = new System.Drawing.Point(765, 105);
            this.muckLabel.Name = "muckLabel";
            this.muckLabel.Size = new System.Drawing.Size(52, 13);
            this.muckLabel.TabIndex = 16;
            this.muckLabel.Text = "Mucking :";
            // 
            // labelHeroCards
            // 
            this.labelHeroCards.AutoSize = true;
            this.labelHeroCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeroCards.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelHeroCards.Location = new System.Drawing.Point(737, 17);
            this.labelHeroCards.Name = "labelHeroCards";
            this.labelHeroCards.Size = new System.Drawing.Size(111, 13);
            this.labelHeroCards.TabIndex = 11;
            this.labelHeroCards.Text = "Last game hero cards:";
            // 
            // pictureBoxMuck2
            // 
            this.pictureBoxMuck2.Location = new System.Drawing.Point(795, 121);
            this.pictureBoxMuck2.Name = "pictureBoxMuck2";
            this.pictureBoxMuck2.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxMuck2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMuck2.TabIndex = 15;
            this.pictureBoxMuck2.TabStop = false;
            // 
            // pictureBoxMuck1
            // 
            this.pictureBoxMuck1.Location = new System.Drawing.Point(745, 121);
            this.pictureBoxMuck1.Name = "pictureBoxMuck1";
            this.pictureBoxMuck1.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxMuck1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMuck1.TabIndex = 14;
            this.pictureBoxMuck1.TabStop = false;
            // 
            // pictureBoxHero2
            // 
            this.pictureBoxHero2.Location = new System.Drawing.Point(795, 33);
            this.pictureBoxHero2.Name = "pictureBoxHero2";
            this.pictureBoxHero2.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxHero2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHero2.TabIndex = 13;
            this.pictureBoxHero2.TabStop = false;
            // 
            // pictureBoxHero1
            // 
            this.pictureBoxHero1.Location = new System.Drawing.Point(745, 33);
            this.pictureBoxHero1.Name = "pictureBoxHero1";
            this.pictureBoxHero1.Size = new System.Drawing.Size(44, 69);
            this.pictureBoxHero1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHero1.TabIndex = 12;
            this.pictureBoxHero1.TabStop = false;
            // 
            // HudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(868, 214);
            this.Controls.Add(this.muckLabel);
            this.Controls.Add(this.labelHeroCards);
            this.Controls.Add(this.pictureBoxMuck2);
            this.Controls.Add(this.pictureBoxMuck1);
            this.Controls.Add(this.pictureBoxHero2);
            this.Controls.Add(this.pictureBoxHero1);
            this.Controls.Add(this.tabControlStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HudForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HudForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HudForm_FormClosing);
            this.Load += new System.EventHandler(this.HudForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HudForm_Paint);
            this.tabControlStats.ResumeLayout(false);
            this.tabPgTable.ResumeLayout(false);
            this.tabPgTable.PerformLayout();
            this.tabPgStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hudGrdVw)).EndInit();
            this.tabPgGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profitChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMuck1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHero1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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