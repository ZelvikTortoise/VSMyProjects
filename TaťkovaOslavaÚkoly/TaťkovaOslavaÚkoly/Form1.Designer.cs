namespace TaťkovaOslavaÚkoly
{
    partial class FormTasks
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
            this.buttonTask1 = new System.Windows.Forms.Button();
            this.buttonTask2 = new System.Windows.Forms.Button();
            this.buttonTask3 = new System.Windows.Forms.Button();
            this.buttonTask4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTask1
            // 
            this.buttonTask1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTask1.Location = new System.Drawing.Point(53, 38);
            this.buttonTask1.Name = "buttonTask1";
            this.buttonTask1.Size = new System.Drawing.Size(77, 70);
            this.buttonTask1.TabIndex = 0;
            this.buttonTask1.Text = "1";
            this.buttonTask1.UseVisualStyleBackColor = true;
            this.buttonTask1.Click += new System.EventHandler(this.buttonTask1_Click);
            // 
            // buttonTask2
            // 
            this.buttonTask2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTask2.Location = new System.Drawing.Point(136, 38);
            this.buttonTask2.Name = "buttonTask2";
            this.buttonTask2.Size = new System.Drawing.Size(77, 70);
            this.buttonTask2.TabIndex = 1;
            this.buttonTask2.Text = "2";
            this.buttonTask2.UseVisualStyleBackColor = true;
            this.buttonTask2.Click += new System.EventHandler(this.buttonTask2_Click);
            // 
            // buttonTask3
            // 
            this.buttonTask3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTask3.Location = new System.Drawing.Point(53, 114);
            this.buttonTask3.Name = "buttonTask3";
            this.buttonTask3.Size = new System.Drawing.Size(77, 70);
            this.buttonTask3.TabIndex = 2;
            this.buttonTask3.Text = "3";
            this.buttonTask3.UseVisualStyleBackColor = true;
            this.buttonTask3.Click += new System.EventHandler(this.buttonTask3_Click);
            // 
            // buttonTask4
            // 
            this.buttonTask4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTask4.Location = new System.Drawing.Point(136, 114);
            this.buttonTask4.Name = "buttonTask4";
            this.buttonTask4.Size = new System.Drawing.Size(77, 70);
            this.buttonTask4.TabIndex = 3;
            this.buttonTask4.Text = "4";
            this.buttonTask4.UseVisualStyleBackColor = true;
            this.buttonTask4.Click += new System.EventHandler(this.buttonTask4_Click);
            // 
            // FormTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 225);
            this.Controls.Add(this.buttonTask4);
            this.Controls.Add(this.buttonTask3);
            this.Controls.Add(this.buttonTask2);
            this.Controls.Add(this.buttonTask1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormTasks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Úkoly";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTask1;
        private System.Windows.Forms.Button buttonTask2;
        private System.Windows.Forms.Button buttonTask3;
        private System.Windows.Forms.Button buttonTask4;
    }
}

