namespace Share
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.buttonZmáčkni = new System.Windows.Forms.Button();
            this.labelVerze = new System.Windows.Forms.Label();
            this.buttonChytni = new System.Windows.Forms.Button();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.labelZakázanéKlávesy = new System.Windows.Forms.Label();
            this.textBoxKód = new System.Windows.Forms.TextBox();
            this.labelKód = new System.Windows.Forms.Label();
            this.buttonVýhra = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonZmáčkni
            // 
            this.buttonZmáčkni.Location = new System.Drawing.Point(121, 251);
            this.buttonZmáčkni.Name = "buttonZmáčkni";
            this.buttonZmáčkni.Size = new System.Drawing.Size(98, 39);
            this.buttonZmáčkni.TabIndex = 0;
            this.buttonZmáčkni.Text = "Zmáčkni mě! :)";
            this.buttonZmáčkni.UseVisualStyleBackColor = true;
            this.buttonZmáčkni.Click += new System.EventHandler(this.buttonZmáčkni_Click);
            this.buttonZmáčkni.MouseEnter += new System.EventHandler(this.buttonZmáčkni_MouseEnter);
            // 
            // labelVerze
            // 
            this.labelVerze.AutoSize = true;
            this.labelVerze.Location = new System.Drawing.Point(21, 64);
            this.labelVerze.Name = "labelVerze";
            this.labelVerze.Size = new System.Drawing.Size(28, 13);
            this.labelVerze.TabIndex = 2;
            this.labelVerze.Text = "v1.2";
            // 
            // buttonChytni
            // 
            this.buttonChytni.Location = new System.Drawing.Point(121, 179);
            this.buttonChytni.Name = "buttonChytni";
            this.buttonChytni.Size = new System.Drawing.Size(98, 39);
            this.buttonChytni.TabIndex = 3;
            this.buttonChytni.Text = "Chytni mě! =)";
            this.buttonChytni.UseVisualStyleBackColor = true;
            this.buttonChytni.Click += new System.EventHandler(this.buttonChytni_Click);
            this.buttonChytni.MouseEnter += new System.EventHandler(this.buttonChytni_MouseEnter);
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(55, 53);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(49, 34);
            this.buttonRestart.TabIndex = 4;
            this.buttonRestart.Text = "Restart";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // labelZakázanéKlávesy
            // 
            this.labelZakázanéKlávesy.AutoSize = true;
            this.labelZakázanéKlávesy.Location = new System.Drawing.Point(9, 26);
            this.labelZakázanéKlávesy.Name = "labelZakázanéKlávesy";
            this.labelZakázanéKlávesy.Size = new System.Drawing.Size(125, 13);
            this.labelZakázanéKlávesy.TabIndex = 5;
            this.labelZakázanéKlávesy.Text = "Zakázané klávesy: Enter";
            // 
            // textBoxKód
            // 
            this.textBoxKód.Location = new System.Drawing.Point(45, 93);
            this.textBoxKód.Name = "textBoxKód";
            this.textBoxKód.Size = new System.Drawing.Size(76, 20);
            this.textBoxKód.TabIndex = 6;
            this.textBoxKód.TextChanged += new System.EventHandler(this.textBoxKód_TextChanged);
            // 
            // labelKód
            // 
            this.labelKód.AutoSize = true;
            this.labelKód.Location = new System.Drawing.Point(12, 96);
            this.labelKód.Name = "labelKód";
            this.labelKód.Size = new System.Drawing.Size(29, 13);
            this.labelKód.TabIndex = 7;
            this.labelKód.Text = "Kód:";
            // 
            // buttonVýhra
            // 
            this.buttonVýhra.Enabled = false;
            this.buttonVýhra.Location = new System.Drawing.Point(127, 89);
            this.buttonVýhra.Name = "buttonVýhra";
            this.buttonVýhra.Size = new System.Drawing.Size(80, 26);
            this.buttonVýhra.TabIndex = 8;
            this.buttonVýhra.Text = "Vyhrej";
            this.buttonVýhra.UseVisualStyleBackColor = true;
            this.buttonVýhra.Visible = false;
            this.buttonVýhra.Click += new System.EventHandler(this.buttonVýhra_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 712);
            this.Controls.Add(this.buttonChytni);
            this.Controls.Add(this.buttonVýhra);
            this.Controls.Add(this.labelKód);
            this.Controls.Add(this.textBoxKód);
            this.Controls.Add(this.labelZakázanéKlávesy);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.labelVerze);
            this.Controls.Add(this.buttonZmáčkni);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form4";
            this.Text = "Tlačítka";
            this.Shown += new System.EventHandler(this.Form4_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form4_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form4_PreviewKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonZmáčkni;
        private System.Windows.Forms.Label labelVerze;
        private System.Windows.Forms.Button buttonChytni;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Label labelZakázanéKlávesy;
        private System.Windows.Forms.TextBox textBoxKód;
        private System.Windows.Forms.Label labelKód;
        private System.Windows.Forms.Button buttonVýhra;
    }
}