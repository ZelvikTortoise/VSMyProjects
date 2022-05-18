namespace Riskuj
{
    partial class FormStart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.openFileDialogLoading = new System.Windows.Forms.OpenFileDialog();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.labelFilePathDesc = new System.Windows.Forms.Label();
            this.buttonFormatInfo = new System.Windows.Forms.Button();
            this.textBoxTeam1 = new System.Windows.Forms.TextBox();
            this.textBoxTeam2 = new System.Windows.Forms.TextBox();
            this.textBoxTeam3 = new System.Windows.Forms.TextBox();
            this.textBoxTeam4 = new System.Windows.Forms.TextBox();
            this.labelTeamNamesDesc = new System.Windows.Forms.Label();
            this.labelAdjectiveDesc = new System.Windows.Forms.Label();
            this.textBoxAdjective = new System.Windows.Forms.TextBox();
            this.toolTipMaxTime = new System.Windows.Forms.ToolTip(this.components);
            this.labelMaxTimeDesc = new System.Windows.Forms.Label();
            this.numericUpDownMaxTime = new System.Windows.Forms.NumericUpDown();
            this.toolTipAdjective = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxTime)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(293, 47);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(124, 52);
            this.buttonBrowse.TabIndex = 0;
            this.buttonBrowse.Text = "Procházet...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // openFileDialogLoading
            // 
            this.openFileDialogLoading.DefaultExt = "txt";
            this.openFileDialogLoading.Filter = "txt files (*.txt)|*.txt";
            this.openFileDialogLoading.FilterIndex = 2;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(39, 291);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(378, 65);
            this.buttonLoad.TabIndex = 8;
            this.buttonLoad.Text = "Načti otázky a začni hru";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(39, 77);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(233, 22);
            this.textBoxFilePath.TabIndex = 2;
            this.textBoxFilePath.TabStop = false;
            this.textBoxFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelFilePathDesc
            // 
            this.labelFilePathDesc.AutoSize = true;
            this.labelFilePathDesc.Location = new System.Drawing.Point(36, 47);
            this.labelFilePathDesc.Name = "labelFilePathDesc";
            this.labelFilePathDesc.Size = new System.Drawing.Size(236, 17);
            this.labelFilePathDesc.TabIndex = 3;
            this.labelFilePathDesc.Text = "Zadejte cestu k souboru s otázkami:";
            // 
            // buttonFormatInfo
            // 
            this.buttonFormatInfo.Location = new System.Drawing.Point(293, 105);
            this.buttonFormatInfo.Name = "buttonFormatInfo";
            this.buttonFormatInfo.Size = new System.Drawing.Size(124, 52);
            this.buttonFormatInfo.TabIndex = 1;
            this.buttonFormatInfo.Text = "Ukaž vzorový soubor";
            this.buttonFormatInfo.UseVisualStyleBackColor = true;
            this.buttonFormatInfo.Click += new System.EventHandler(this.buttonFormatInfo_Click);
            // 
            // textBoxTeam1
            // 
            this.textBoxTeam1.BackColor = System.Drawing.Color.Red;
            this.textBoxTeam1.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTeam1.Location = new System.Drawing.Point(39, 139);
            this.textBoxTeam1.MaxLength = 15;
            this.textBoxTeam1.Name = "textBoxTeam1";
            this.textBoxTeam1.Size = new System.Drawing.Size(233, 31);
            this.textBoxTeam1.TabIndex = 2;
            this.textBoxTeam1.Text = "Tým 1";
            this.textBoxTeam1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTeam2
            // 
            this.textBoxTeam2.BackColor = System.Drawing.Color.Yellow;
            this.textBoxTeam2.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTeam2.Location = new System.Drawing.Point(39, 176);
            this.textBoxTeam2.MaxLength = 15;
            this.textBoxTeam2.Name = "textBoxTeam2";
            this.textBoxTeam2.Size = new System.Drawing.Size(233, 31);
            this.textBoxTeam2.TabIndex = 3;
            this.textBoxTeam2.Text = "Tým 2";
            this.textBoxTeam2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTeam3
            // 
            this.textBoxTeam3.BackColor = System.Drawing.Color.Blue;
            this.textBoxTeam3.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTeam3.Location = new System.Drawing.Point(39, 213);
            this.textBoxTeam3.MaxLength = 15;
            this.textBoxTeam3.Name = "textBoxTeam3";
            this.textBoxTeam3.Size = new System.Drawing.Size(233, 31);
            this.textBoxTeam3.TabIndex = 4;
            this.textBoxTeam3.Text = "Tým 3";
            this.textBoxTeam3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxTeam4
            // 
            this.textBoxTeam4.BackColor = System.Drawing.Color.Green;
            this.textBoxTeam4.Font = new System.Drawing.Font("Comic Sans MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTeam4.Location = new System.Drawing.Point(39, 250);
            this.textBoxTeam4.MaxLength = 15;
            this.textBoxTeam4.Name = "textBoxTeam4";
            this.textBoxTeam4.Size = new System.Drawing.Size(233, 31);
            this.textBoxTeam4.TabIndex = 5;
            this.textBoxTeam4.Text = "Tým 4";
            this.textBoxTeam4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTeamNamesDesc
            // 
            this.labelTeamNamesDesc.AutoSize = true;
            this.labelTeamNamesDesc.Location = new System.Drawing.Point(36, 119);
            this.labelTeamNamesDesc.Name = "labelTeamNamesDesc";
            this.labelTeamNamesDesc.Size = new System.Drawing.Size(136, 17);
            this.labelTeamNamesDesc.TabIndex = 9;
            this.labelTeamNamesDesc.Text = "Zadejte jména týmů:";
            // 
            // labelAdjectiveDesc
            // 
            this.labelAdjectiveDesc.AutoSize = true;
            this.labelAdjectiveDesc.Location = new System.Drawing.Point(290, 236);
            this.labelAdjectiveDesc.Name = "labelAdjectiveDesc";
            this.labelAdjectiveDesc.Size = new System.Drawing.Size(87, 17);
            this.labelAdjectiveDesc.TabIndex = 10;
            this.labelAdjectiveDesc.Text = "Jaký Riskuj?";
            this.toolTipAdjective.SetToolTip(this.labelAdjectiveDesc, resources.GetString("labelAdjectiveDesc.ToolTip"));
            // 
            // textBoxAdjective
            // 
            this.textBoxAdjective.Location = new System.Drawing.Point(293, 256);
            this.textBoxAdjective.MaxLength = 15;
            this.textBoxAdjective.Name = "textBoxAdjective";
            this.textBoxAdjective.Size = new System.Drawing.Size(124, 22);
            this.textBoxAdjective.TabIndex = 7;
            this.textBoxAdjective.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipAdjective.SetToolTip(this.textBoxAdjective, resources.GetString("textBoxAdjective.ToolTip"));
            // 
            // toolTipMaxTime
            // 
            this.toolTipMaxTime.AutoPopDelay = 30000;
            this.toolTipMaxTime.InitialDelay = 500;
            this.toolTipMaxTime.ReshowDelay = 100;
            this.toolTipMaxTime.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipMaxTime.ToolTipTitle = "Vysvětlivka";
            // 
            // labelMaxTimeDesc
            // 
            this.labelMaxTimeDesc.AutoSize = true;
            this.labelMaxTimeDesc.Location = new System.Drawing.Point(290, 176);
            this.labelMaxTimeDesc.Name = "labelMaxTimeDesc";
            this.labelMaxTimeDesc.Size = new System.Drawing.Size(141, 17);
            this.labelMaxTimeDesc.TabIndex = 11;
            this.labelMaxTimeDesc.Text = "Čas na jednu otázku:";
            this.toolTipMaxTime.SetToolTip(this.labelMaxTimeDesc, resources.GetString("labelMaxTimeDesc.ToolTip"));
            // 
            // numericUpDownMaxTime
            // 
            this.numericUpDownMaxTime.Location = new System.Drawing.Point(293, 196);
            this.numericUpDownMaxTime.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownMaxTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownMaxTime.Name = "numericUpDownMaxTime";
            this.numericUpDownMaxTime.Size = new System.Drawing.Size(124, 22);
            this.numericUpDownMaxTime.TabIndex = 6;
            this.numericUpDownMaxTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipMaxTime.SetToolTip(this.numericUpDownMaxTime, resources.GetString("numericUpDownMaxTime.ToolTip"));
            this.numericUpDownMaxTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // toolTipAdjective
            // 
            this.toolTipAdjective.AutoPopDelay = 30000;
            this.toolTipAdjective.InitialDelay = 500;
            this.toolTipAdjective.ReshowDelay = 100;
            this.toolTipAdjective.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipAdjective.ToolTipTitle = "Vysvětlivka";
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 368);
            this.Controls.Add(this.numericUpDownMaxTime);
            this.Controls.Add(this.labelMaxTimeDesc);
            this.Controls.Add(this.textBoxAdjective);
            this.Controls.Add(this.labelAdjectiveDesc);
            this.Controls.Add(this.labelTeamNamesDesc);
            this.Controls.Add(this.textBoxTeam4);
            this.Controls.Add(this.textBoxTeam3);
            this.Controls.Add(this.textBoxTeam2);
            this.Controls.Add(this.textBoxTeam1);
            this.Controls.Add(this.buttonFormatInfo);
            this.Controls.Add(this.labelFilePathDesc);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nastavování otázek";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialogLoading;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Label labelFilePathDesc;
        private System.Windows.Forms.Button buttonFormatInfo;
        private System.Windows.Forms.TextBox textBoxTeam1;
        private System.Windows.Forms.TextBox textBoxTeam2;
        private System.Windows.Forms.TextBox textBoxTeam3;
        private System.Windows.Forms.TextBox textBoxTeam4;
        private System.Windows.Forms.Label labelTeamNamesDesc;
        private System.Windows.Forms.Label labelAdjectiveDesc;
        private System.Windows.Forms.TextBox textBoxAdjective;
        private System.Windows.Forms.ToolTip toolTipMaxTime;
        private System.Windows.Forms.Label labelMaxTimeDesc;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxTime;
        private System.Windows.Forms.ToolTip toolTipAdjective;
    }
}