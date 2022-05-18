namespace ŠárkaKvíz
{
    partial class FormKonec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKonec));
            this.labelOtázka = new System.Windows.Forms.Label();
            this.buttonAno = new System.Windows.Forms.Button();
            this.buttonNe = new System.Windows.Forms.Button();
            this.checkBoxZapsatSkóre = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelOtázka
            // 
            this.labelOtázka.AutoSize = true;
            this.labelOtázka.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOtázka.Location = new System.Drawing.Point(28, 29);
            this.labelOtázka.Name = "labelOtázka";
            this.labelOtázka.Size = new System.Drawing.Size(391, 38);
            this.labelOtázka.TabIndex = 0;
            this.labelOtázka.Text = "Opravdu chcete kvíz ukončit?";
            // 
            // buttonAno
            // 
            this.buttonAno.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonAno.Location = new System.Drawing.Point(225, 91);
            this.buttonAno.Name = "buttonAno";
            this.buttonAno.Size = new System.Drawing.Size(100, 67);
            this.buttonAno.TabIndex = 2;
            this.buttonAno.Text = "Ano";
            this.buttonAno.UseVisualStyleBackColor = true;
            this.buttonAno.Click += new System.EventHandler(this.buttonAno_Click);
            // 
            // buttonNe
            // 
            this.buttonNe.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonNe.Location = new System.Drawing.Point(96, 91);
            this.buttonNe.Name = "buttonNe";
            this.buttonNe.Size = new System.Drawing.Size(100, 67);
            this.buttonNe.TabIndex = 0;
            this.buttonNe.Text = "Ne";
            this.buttonNe.UseVisualStyleBackColor = true;
            this.buttonNe.Click += new System.EventHandler(this.buttonNe_Click);
            // 
            // checkBoxZapsatSkóre
            // 
            this.checkBoxZapsatSkóre.AutoSize = true;
            this.checkBoxZapsatSkóre.Checked = true;
            this.checkBoxZapsatSkóre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxZapsatSkóre.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxZapsatSkóre.Location = new System.Drawing.Point(225, 162);
            this.checkBoxZapsatSkóre.Name = "checkBoxZapsatSkóre";
            this.checkBoxZapsatSkóre.Size = new System.Drawing.Size(163, 27);
            this.checkBoxZapsatSkóre.TabIndex = 1;
            this.checkBoxZapsatSkóre.Text = "Zapsat moje skóre";
            this.checkBoxZapsatSkóre.UseVisualStyleBackColor = true;
            this.checkBoxZapsatSkóre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBoxZapsatSkóre_KeyDown);
            // 
            // FormKonec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 206);
            this.Controls.Add(this.checkBoxZapsatSkóre);
            this.Controls.Add(this.buttonNe);
            this.Controls.Add(this.buttonAno);
            this.Controls.Add(this.labelOtázka);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormKonec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Šárka kvíz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOtázka;
        private System.Windows.Forms.Button buttonAno;
        private System.Windows.Forms.Button buttonNe;
        private System.Windows.Forms.CheckBox checkBoxZapsatSkóre;
    }
}