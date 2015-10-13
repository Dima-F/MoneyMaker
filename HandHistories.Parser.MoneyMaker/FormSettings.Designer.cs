namespace HandHistories.Parser.MoneyMaker
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hhFolderButton = new System.Windows.Forms.Button();
            this.hhFolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.advancedTabPage = new System.Windows.Forms.TabPage();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxManual = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.heroTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownComit = new System.Windows.Forms.NumericUpDown();
            this.settingsTab.SuspendLayout();
            this.commonTabPage.SuspendLayout();
            this.advancedTabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComit)).BeginInit();
            this.SuspendLayout();
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.commonTabPage);
            this.settingsTab.Controls.Add(this.advancedTabPage);
            this.settingsTab.Dock = System.Windows.Forms.DockStyle.Top;
            this.settingsTab.Location = new System.Drawing.Point(0, 0);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.SelectedIndex = 0;
            this.settingsTab.Size = new System.Drawing.Size(384, 206);
            this.settingsTab.TabIndex = 0;
            // 
            // commonTabPage
            // 
            this.commonTabPage.Controls.Add(this.heroTextBox);
            this.commonTabPage.Controls.Add(this.label2);
            this.commonTabPage.Controls.Add(this.groupBox1);
            this.commonTabPage.Controls.Add(this.hhFolderButton);
            this.commonTabPage.Controls.Add(this.hhFolderTextBox);
            this.commonTabPage.Controls.Add(this.label1);
            this.commonTabPage.Location = new System.Drawing.Point(4, 22);
            this.commonTabPage.Name = "commonTabPage";
            this.commonTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.commonTabPage.Size = new System.Drawing.Size(376, 180);
            this.commonTabPage.TabIndex = 0;
            this.commonTabPage.Text = "Common";
            this.commonTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 77);
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
            // advancedTabPage
            // 
            this.advancedTabPage.Controls.Add(this.numericUpDownComit);
            this.advancedTabPage.Controls.Add(this.groupBox2);
            this.advancedTabPage.Controls.Add(this.label3);
            this.advancedTabPage.Location = new System.Drawing.Point(4, 22);
            this.advancedTabPage.Name = "advancedTabPage";
            this.advancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.advancedTabPage.Size = new System.Drawing.Size(376, 180);
            this.advancedTabPage.TabIndex = 1;
            this.advancedTabPage.Text = "Advanced";
            this.advancedTabPage.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(65, 227);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(227, 227);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hero name:";
            // 
            // heroTextBox
            // 
            this.heroTextBox.Location = new System.Drawing.Point(20, 65);
            this.heroTextBox.Name = "heroTextBox";
            this.heroTextBox.Size = new System.Drawing.Size(309, 20);
            this.heroTextBox.TabIndex = 5;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxManual);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 78);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other options";
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
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.settingsTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.settingsTab.ResumeLayout(false);
            this.commonTabPage.ResumeLayout(false);
            this.commonTabPage.PerformLayout();
            this.advancedTabPage.ResumeLayout(false);
            this.advancedTabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownComit)).EndInit();
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
    }
}