namespace ŠárkaKvíz
{
    partial class FormWelcoming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWelcoming));
            this.labelZpráva = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelZpráva
            // 
            this.labelZpráva.AutoSize = true;
            this.labelZpráva.BackColor = System.Drawing.Color.Transparent;
            this.labelZpráva.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelZpráva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelZpráva.Location = new System.Drawing.Point(3, 22);
            this.labelZpráva.Name = "labelZpráva";
            this.labelZpráva.Size = new System.Drawing.Size(770, 90);
            this.labelZpráva.TabIndex = 0;
            this.labelZpráva.Text = "Vítejte v Šárka kvízu! ☺";
            // 
            // FormWelcoming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Welcome;
            this.ClientSize = new System.Drawing.Size(768, 131);
            this.Controls.Add(this.labelZpráva);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWelcoming";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Šárka kvíz";
            this.Shown += new System.EventHandler(this.FormWelcoming_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelZpráva;
    }
}

