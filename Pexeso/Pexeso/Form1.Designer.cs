namespace Pexeso
{
    partial class Form1
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
            this.lUvod = new System.Windows.Forms.Label();
            this.lPocetTahu = new System.Windows.Forms.Label();
            this.lVysledek = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.bNovaHra = new System.Windows.Forms.Button();
            this.bKonec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lUvod
            // 
            this.lUvod.AutoSize = true;
            this.lUvod.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lUvod.ForeColor = System.Drawing.Color.Red;
            this.lUvod.Location = new System.Drawing.Point(79, 72);
            this.lUvod.Name = "lUvod";
            this.lUvod.Size = new System.Drawing.Size(659, 69);
            this.lUvod.TabIndex = 0;
            this.lUvod.Text = "Vitejte ve hre PEXESO!";
            // 
            // lPocetTahu
            // 
            this.lPocetTahu.AutoSize = true;
            this.lPocetTahu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lPocetTahu.Location = new System.Drawing.Point(291, 406);
            this.lPocetTahu.Name = "lPocetTahu";
            this.lPocetTahu.Size = new System.Drawing.Size(223, 25);
            this.lPocetTahu.TabIndex = 1;
            this.lPocetTahu.Text = "Dosavadni pocet tahu: 0";
            // 
            // lVysledek
            // 
            this.lVysledek.AutoSize = true;
            this.lVysledek.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lVysledek.ForeColor = System.Drawing.Color.Red;
            this.lVysledek.Location = new System.Drawing.Point(36, 72);
            this.lVysledek.Name = "lVysledek";
            this.lVysledek.Size = new System.Drawing.Size(718, 132);
            this.lVysledek.TabIndex = 2;
            this.lVysledek.Text = "                Gratulujeme k vyhre!\r\nCelkem potrebnych tahu k dokonceni hry:\r\n  " +
    "                            xxx";
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.Color.Yellow;
            this.bStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bStart.ForeColor = System.Drawing.Color.Red;
            this.bStart.Location = new System.Drawing.Point(227, 227);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(323, 122);
            this.bStart.TabIndex = 3;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bNovaHra
            // 
            this.bNovaHra.BackColor = System.Drawing.Color.Yellow;
            this.bNovaHra.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bNovaHra.ForeColor = System.Drawing.Color.Red;
            this.bNovaHra.Location = new System.Drawing.Point(227, 227);
            this.bNovaHra.Name = "bNovaHra";
            this.bNovaHra.Size = new System.Drawing.Size(323, 122);
            this.bNovaHra.TabIndex = 4;
            this.bNovaHra.Text = "Nova hra";
            this.bNovaHra.UseVisualStyleBackColor = false;
            this.bNovaHra.Click += new System.EventHandler(this.bNovaHra_Click);
            // 
            // bKonec
            // 
            this.bKonec.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bKonec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bKonec.Location = new System.Drawing.Point(692, 399);
            this.bKonec.Name = "bKonec";
            this.bKonec.Size = new System.Drawing.Size(96, 39);
            this.bKonec.TabIndex = 5;
            this.bKonec.Text = "Konec";
            this.bKonec.UseVisualStyleBackColor = false;
            this.bKonec.Click += new System.EventHandler(this.bKonec_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lVysledek);
            this.Controls.Add(this.bKonec);
            this.Controls.Add(this.bNovaHra);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.lPocetTahu);
            this.Controls.Add(this.lUvod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pexeso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lUvod;
        private System.Windows.Forms.Label lPocetTahu;
        private System.Windows.Forms.Label lVysledek;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bNovaHra;
        private System.Windows.Forms.Button bKonec;
    }
}

