namespace AGCalculator
{
    partial class FormLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLanguage));
            this.radioButtonLanguageEnglish = new System.Windows.Forms.RadioButton();
            this.radioButtonLanguageCzech = new System.Windows.Forms.RadioButton();
            this.panelLanguages = new System.Windows.Forms.Panel();
            this.labelDescSelectLanguage = new System.Windows.Forms.Label();
            this.buttonSelectLanguage = new System.Windows.Forms.Button();
            this.panelLanguages.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonLanguageEnglish
            // 
            this.radioButtonLanguageEnglish.AutoSize = true;
            this.radioButtonLanguageEnglish.Checked = true;
            this.radioButtonLanguageEnglish.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonLanguageEnglish.Location = new System.Drawing.Point(14, 9);
            this.radioButtonLanguageEnglish.Name = "radioButtonLanguageEnglish";
            this.radioButtonLanguageEnglish.Size = new System.Drawing.Size(100, 27);
            this.radioButtonLanguageEnglish.TabIndex = 0;
            this.radioButtonLanguageEnglish.TabStop = true;
            this.radioButtonLanguageEnglish.Text = "English";
            this.radioButtonLanguageEnglish.UseVisualStyleBackColor = true;
            // 
            // radioButtonLanguageCzech
            // 
            this.radioButtonLanguageCzech.AutoSize = true;
            this.radioButtonLanguageCzech.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonLanguageCzech.Location = new System.Drawing.Point(14, 36);
            this.radioButtonLanguageCzech.Name = "radioButtonLanguageCzech";
            this.radioButtonLanguageCzech.Size = new System.Drawing.Size(88, 27);
            this.radioButtonLanguageCzech.TabIndex = 1;
            this.radioButtonLanguageCzech.Text = "Česky";
            this.radioButtonLanguageCzech.UseVisualStyleBackColor = true;
            // 
            // panelLanguages
            // 
            this.panelLanguages.Controls.Add(this.radioButtonLanguageCzech);
            this.panelLanguages.Controls.Add(this.radioButtonLanguageEnglish);
            this.panelLanguages.Location = new System.Drawing.Point(42, 79);
            this.panelLanguages.Name = "panelLanguages";
            this.panelLanguages.Size = new System.Drawing.Size(275, 74);
            this.panelLanguages.TabIndex = 3;
            // 
            // labelDescSelectLanguage
            // 
            this.labelDescSelectLanguage.AutoSize = true;
            this.labelDescSelectLanguage.Font = new System.Drawing.Font("Lucida Sans Unicode", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDescSelectLanguage.Location = new System.Drawing.Point(35, 25);
            this.labelDescSelectLanguage.Name = "labelDescSelectLanguage";
            this.labelDescSelectLanguage.Size = new System.Drawing.Size(282, 37);
            this.labelDescSelectLanguage.TabIndex = 4;
            this.labelDescSelectLanguage.Text = "Select a language:";
            // 
            // buttonSelectLanguage
            // 
            this.buttonSelectLanguage.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSelectLanguage.Location = new System.Drawing.Point(122, 187);
            this.buttonSelectLanguage.Name = "buttonSelectLanguage";
            this.buttonSelectLanguage.Size = new System.Drawing.Size(108, 55);
            this.buttonSelectLanguage.TabIndex = 2;
            this.buttonSelectLanguage.Text = "Select";
            this.buttonSelectLanguage.UseVisualStyleBackColor = true;
            this.buttonSelectLanguage.Click += new System.EventHandler(this.buttonSelectLanguage_Click);
            // 
            // FormLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 275);
            this.Controls.Add(this.buttonSelectLanguage);
            this.Controls.Add(this.labelDescSelectLanguage);
            this.Controls.Add(this.panelLanguages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormLanguage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language selection";
            this.panelLanguages.ResumeLayout(false);
            this.panelLanguages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonLanguageEnglish;
        private System.Windows.Forms.RadioButton radioButtonLanguageCzech;
        private System.Windows.Forms.Panel panelLanguages;
        private System.Windows.Forms.Label labelDescSelectLanguage;
        private System.Windows.Forms.Button buttonSelectLanguage;
    }
}