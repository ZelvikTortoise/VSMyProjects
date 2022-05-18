namespace Share
{
    partial class Form2Úvod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2Úvod));
            this.labelUvítání = new System.Windows.Forms.Label();
            this.labelVýběrObtížnostiPopis = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelUvítání
            // 
            this.labelUvítání.AutoSize = true;
            this.labelUvítání.BackColor = System.Drawing.Color.Transparent;
            this.labelUvítání.Font = new System.Drawing.Font("Magneto", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUvítání.ForeColor = System.Drawing.Color.Red;
            this.labelUvítání.Location = new System.Drawing.Point(33, 59);
            this.labelUvítání.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUvítání.Name = "labelUvítání";
            this.labelUvítání.Size = new System.Drawing.Size(529, 48);
            this.labelUvítání.TabIndex = 0;
            this.labelUvítání.Text = "Vítejte ve Snyanke cat!";
            // 
            // labelVýběrObtížnostiPopis
            // 
            this.labelVýběrObtížnostiPopis.AutoSize = true;
            this.labelVýběrObtížnostiPopis.BackColor = System.Drawing.Color.Transparent;
            this.labelVýběrObtížnostiPopis.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVýběrObtížnostiPopis.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelVýběrObtížnostiPopis.Location = new System.Drawing.Point(137, 259);
            this.labelVýběrObtížnostiPopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVýběrObtížnostiPopis.Name = "labelVýběrObtížnostiPopis";
            this.labelVýběrObtížnostiPopis.Size = new System.Drawing.Size(378, 39);
            this.labelVýběrObtížnostiPopis.TabIndex = 1;
            this.labelVýběrObtížnostiPopis.Text = "Vyberte si obtížnost hry:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.ForestGreen;
            this.button1.Location = new System.Drawing.Point(43, 382);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 79);
            this.button1.TabIndex = 2;
            this.button1.Text = "Snadná";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.ForeColor = System.Drawing.Color.ForestGreen;
            this.button2.Location = new System.Drawing.Point(260, 382);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 79);
            this.button2.TabIndex = 3;
            this.button2.Text = "Střední";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.ForeColor = System.Drawing.Color.ForestGreen;
            this.button3.Location = new System.Drawing.Point(477, 382);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 79);
            this.button3.TabIndex = 4;
            this.button3.Text = "Těžká";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(89, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pomozte hladové Nyan cat posbírat všechny dobrůtky.";
            // 
            // Form2Úvod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Share.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(645, 569);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelVýběrObtížnostiPopis);
            this.Controls.Add(this.labelUvítání);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form2Úvod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snyanke cat (výběr obtížnosti)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUvítání;
        private System.Windows.Forms.Label labelVýběrObtížnostiPopis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
    }
}