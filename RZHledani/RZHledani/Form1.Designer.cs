namespace RZHledani
{
    partial class FormBase
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBase));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.labelNumberOfFIlesLabel = new System.Windows.Forms.Label();
            this.labelNumberOfFiles = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelNumberOfFoundLabel = new System.Windows.Forms.Label();
            this.labelNumberOfFound = new System.Windows.Forms.Label();
            this.buttonShowFound = new System.Windows.Forms.Button();
            this.labelPathOutLabel = new System.Windows.Forms.Label();
            this.textBoxPathOut = new System.Windows.Forms.TextBox();
            this.radioButtonGenerateFileYes = new System.Windows.Forms.RadioButton();
            this.radioButtonGenerateFileNo = new System.Windows.Forms.RadioButton();
            this.groupBoxGenerateFile = new System.Windows.Forms.GroupBox();
            this.buttonPathOutFile = new System.Windows.Forms.Button();
            this.buttonRemoveAllFiles = new System.Windows.Forms.Button();
            this.checkBoxGenerateFileNew = new System.Windows.Forms.CheckBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxGenerateFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonAddFile.Location = new System.Drawing.Point(155, 42);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(138, 62);
            this.buttonAddFile.TabIndex = 0;
            this.buttonAddFile.Text = "Přidat";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // labelNumberOfFIlesLabel
            // 
            this.labelNumberOfFIlesLabel.AutoSize = true;
            this.labelNumberOfFIlesLabel.Location = new System.Drawing.Point(80, 124);
            this.labelNumberOfFIlesLabel.Name = "labelNumberOfFIlesLabel";
            this.labelNumberOfFIlesLabel.Size = new System.Drawing.Size(143, 13);
            this.labelNumberOfFIlesLabel.TabIndex = 1;
            this.labelNumberOfFIlesLabel.Text = "Počet souborů k porovnání: ";
            // 
            // labelNumberOfFiles
            // 
            this.labelNumberOfFiles.AutoSize = true;
            this.labelNumberOfFiles.Location = new System.Drawing.Point(224, 124);
            this.labelNumberOfFiles.Name = "labelNumberOfFiles";
            this.labelNumberOfFiles.Size = new System.Drawing.Size(13, 13);
            this.labelNumberOfFiles.TabIndex = 2;
            this.labelNumberOfFiles.Text = "0";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Enabled = false;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSearch.Location = new System.Drawing.Point(155, 162);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(138, 62);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "HLEDEJ";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelNumberOfFoundLabel
            // 
            this.labelNumberOfFoundLabel.AutoSize = true;
            this.labelNumberOfFoundLabel.Location = new System.Drawing.Point(80, 248);
            this.labelNumberOfFoundLabel.Name = "labelNumberOfFoundLabel";
            this.labelNumberOfFoundLabel.Size = new System.Drawing.Size(213, 13);
            this.labelNumberOfFoundLabel.TabIndex = 4;
            this.labelNumberOfFoundLabel.Text = "Počet nalezených RZ ve všech souborech:";
            this.labelNumberOfFoundLabel.Visible = false;
            // 
            // labelNumberOfFound
            // 
            this.labelNumberOfFound.AutoSize = true;
            this.labelNumberOfFound.Location = new System.Drawing.Point(299, 248);
            this.labelNumberOfFound.Name = "labelNumberOfFound";
            this.labelNumberOfFound.Size = new System.Drawing.Size(13, 13);
            this.labelNumberOfFound.TabIndex = 5;
            this.labelNumberOfFound.Text = "0";
            this.labelNumberOfFound.Visible = false;
            // 
            // buttonShowFound
            // 
            this.buttonShowFound.Enabled = false;
            this.buttonShowFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonShowFound.Location = new System.Drawing.Point(141, 278);
            this.buttonShowFound.Name = "buttonShowFound";
            this.buttonShowFound.Size = new System.Drawing.Size(181, 35);
            this.buttonShowFound.TabIndex = 6;
            this.buttonShowFound.Text = "Ukaž nalezené RZ";
            this.buttonShowFound.UseVisualStyleBackColor = true;
            this.buttonShowFound.Click += new System.EventHandler(this.buttonShowFound_Click);
            // 
            // labelPathOutLabel
            // 
            this.labelPathOutLabel.AutoSize = true;
            this.labelPathOutLabel.Location = new System.Drawing.Point(80, 433);
            this.labelPathOutLabel.Name = "labelPathOutLabel";
            this.labelPathOutLabel.Size = new System.Drawing.Size(81, 13);
            this.labelPathOutLabel.TabIndex = 7;
            this.labelPathOutLabel.Text = "Výstupní cesta:";
            // 
            // textBoxPathOut
            // 
            this.textBoxPathOut.BackColor = System.Drawing.Color.White;
            this.textBoxPathOut.Location = new System.Drawing.Point(83, 449);
            this.textBoxPathOut.Name = "textBoxPathOut";
            this.textBoxPathOut.Size = new System.Drawing.Size(291, 20);
            this.textBoxPathOut.TabIndex = 8;
            this.textBoxPathOut.TextChanged += new System.EventHandler(this.textBoxPathOut_TextChanged);
            // 
            // radioButtonGenerateFileYes
            // 
            this.radioButtonGenerateFileYes.AutoSize = true;
            this.radioButtonGenerateFileYes.Checked = true;
            this.radioButtonGenerateFileYes.Location = new System.Drawing.Point(6, 23);
            this.radioButtonGenerateFileYes.Name = "radioButtonGenerateFileYes";
            this.radioButtonGenerateFileYes.Size = new System.Drawing.Size(44, 17);
            this.radioButtonGenerateFileYes.TabIndex = 9;
            this.radioButtonGenerateFileYes.TabStop = true;
            this.radioButtonGenerateFileYes.Text = "Ano";
            this.radioButtonGenerateFileYes.UseVisualStyleBackColor = true;
            // 
            // radioButtonGenerateFileNo
            // 
            this.radioButtonGenerateFileNo.AutoSize = true;
            this.radioButtonGenerateFileNo.Location = new System.Drawing.Point(6, 53);
            this.radioButtonGenerateFileNo.Name = "radioButtonGenerateFileNo";
            this.radioButtonGenerateFileNo.Size = new System.Drawing.Size(39, 17);
            this.radioButtonGenerateFileNo.TabIndex = 10;
            this.radioButtonGenerateFileNo.Text = "Ne";
            this.radioButtonGenerateFileNo.UseVisualStyleBackColor = true;
            this.radioButtonGenerateFileNo.CheckedChanged += new System.EventHandler(this.radioButtonGenerateFileNo_CheckedChanged);
            // 
            // groupBoxGenerateFile
            // 
            this.groupBoxGenerateFile.Controls.Add(this.radioButtonGenerateFileNo);
            this.groupBoxGenerateFile.Controls.Add(this.radioButtonGenerateFileYes);
            this.groupBoxGenerateFile.Location = new System.Drawing.Point(83, 336);
            this.groupBoxGenerateFile.Name = "groupBoxGenerateFile";
            this.groupBoxGenerateFile.Size = new System.Drawing.Size(193, 83);
            this.groupBoxGenerateFile.TabIndex = 11;
            this.groupBoxGenerateFile.TabStop = false;
            this.groupBoxGenerateFile.Text = "Generovat výstupní soubor?";
            // 
            // buttonPathOutFile
            // 
            this.buttonPathOutFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPathOutFile.Location = new System.Drawing.Point(164, 475);
            this.buttonPathOutFile.Name = "buttonPathOutFile";
            this.buttonPathOutFile.Size = new System.Drawing.Size(114, 35);
            this.buttonPathOutFile.TabIndex = 12;
            this.buttonPathOutFile.Text = "Vybrat složku...";
            this.buttonPathOutFile.UseVisualStyleBackColor = true;
            this.buttonPathOutFile.Click += new System.EventHandler(this.buttonPathOutFile_Click);
            // 
            // buttonRemoveAllFiles
            // 
            this.buttonRemoveAllFiles.Enabled = false;
            this.buttonRemoveAllFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonRemoveAllFiles.Location = new System.Drawing.Point(282, 110);
            this.buttonRemoveAllFiles.Name = "buttonRemoveAllFiles";
            this.buttonRemoveAllFiles.Size = new System.Drawing.Size(100, 36);
            this.buttonRemoveAllFiles.TabIndex = 13;
            this.buttonRemoveAllFiles.Text = "Odebrat vše";
            this.buttonRemoveAllFiles.UseVisualStyleBackColor = true;
            this.buttonRemoveAllFiles.Click += new System.EventHandler(this.buttonRemoveAllFiles_Click);
            // 
            // checkBoxGenerateFileNew
            // 
            this.checkBoxGenerateFileNew.AutoSize = true;
            this.checkBoxGenerateFileNew.Checked = true;
            this.checkBoxGenerateFileNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGenerateFileNew.Location = new System.Drawing.Point(282, 369);
            this.checkBoxGenerateFileNew.Name = "checkBoxGenerateFileNew";
            this.checkBoxGenerateFileNew.Size = new System.Drawing.Size(88, 17);
            this.checkBoxGenerateFileNew.TabIndex = 14;
            this.checkBoxGenerateFileNew.Text = "Vytvořit nový";
            this.checkBoxGenerateFileNew.UseVisualStyleBackColor = true;
            this.checkBoxGenerateFileNew.CheckedChanged += new System.EventHandler(this.checkBoxGenerateFileNew_CheckedChanged);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExit.Location = new System.Drawing.Point(417, 449);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(88, 61);
            this.buttonExit.TabIndex = 15;
            this.buttonExit.Text = "Ukončit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 541);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.checkBoxGenerateFileNew);
            this.Controls.Add(this.buttonRemoveAllFiles);
            this.Controls.Add(this.buttonPathOutFile);
            this.Controls.Add(this.groupBoxGenerateFile);
            this.Controls.Add(this.textBoxPathOut);
            this.Controls.Add(this.labelPathOutLabel);
            this.Controls.Add(this.buttonShowFound);
            this.Controls.Add(this.labelNumberOfFound);
            this.Controls.Add(this.labelNumberOfFoundLabel);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelNumberOfFiles);
            this.Controls.Add(this.labelNumberOfFIlesLabel);
            this.Controls.Add(this.buttonAddFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hledání RZ obsažené ve všech vybraných souborech";
            this.groupBoxGenerateFile.ResumeLayout(false);
            this.groupBoxGenerateFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.Label labelNumberOfFIlesLabel;
        private System.Windows.Forms.Label labelNumberOfFiles;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelNumberOfFoundLabel;
        private System.Windows.Forms.Label labelNumberOfFound;
        private System.Windows.Forms.Button buttonShowFound;
        private System.Windows.Forms.Label labelPathOutLabel;
        private System.Windows.Forms.TextBox textBoxPathOut;
        private System.Windows.Forms.RadioButton radioButtonGenerateFileYes;
        private System.Windows.Forms.RadioButton radioButtonGenerateFileNo;
        private System.Windows.Forms.GroupBox groupBoxGenerateFile;
        private System.Windows.Forms.Button buttonPathOutFile;
        private System.Windows.Forms.Button buttonRemoveAllFiles;
        private System.Windows.Forms.CheckBox checkBoxGenerateFileNew;
        private System.Windows.Forms.Button buttonExit;
    }
}

