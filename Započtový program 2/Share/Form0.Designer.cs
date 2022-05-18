namespace Share
{
    partial class Form0
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form0));
            this.labelRozcestníkPopis = new System.Windows.Forms.Label();
            this.buttonForm1 = new System.Windows.Forms.Button();
            this.buttonFormÚvod2 = new System.Windows.Forms.Button();
            this.buttonForm3 = new System.Windows.Forms.Button();
            this.buttonForm4 = new System.Windows.Forms.Button();
            this.labelUvítání = new System.Windows.Forms.Label();
            this.pictureBoxRozcestí = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRozcestí)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRozcestníkPopis
            // 
            this.labelRozcestníkPopis.AutoSize = true;
            this.labelRozcestníkPopis.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRozcestníkPopis.ForeColor = System.Drawing.Color.Chocolate;
            this.labelRozcestníkPopis.Location = new System.Drawing.Point(76, 192);
            this.labelRozcestníkPopis.Name = "labelRozcestníkPopis";
            this.labelRozcestníkPopis.Size = new System.Drawing.Size(239, 21);
            this.labelRozcestníkPopis.TabIndex = 0;
            this.labelRozcestníkPopis.Text = "Vyber si, kam se chceš dostat:";
            // 
            // buttonForm1
            // 
            this.buttonForm1.Font = new System.Drawing.Font("Tempus Sans ITC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonForm1.Location = new System.Drawing.Point(32, 230);
            this.buttonForm1.Name = "buttonForm1";
            this.buttonForm1.Size = new System.Drawing.Size(161, 53);
            this.buttonForm1.TabIndex = 1;
            this.buttonForm1.Text = "Kamarád";
            this.buttonForm1.UseVisualStyleBackColor = true;
            this.buttonForm1.Click += new System.EventHandler(this.buttonForm1_Click);
            // 
            // buttonFormÚvod2
            // 
            this.buttonFormÚvod2.Font = new System.Drawing.Font("Tempus Sans ITC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFormÚvod2.Location = new System.Drawing.Point(199, 230);
            this.buttonFormÚvod2.Name = "buttonFormÚvod2";
            this.buttonFormÚvod2.Size = new System.Drawing.Size(161, 53);
            this.buttonFormÚvod2.TabIndex = 2;
            this.buttonFormÚvod2.Text = "Snyanke cat";
            this.buttonFormÚvod2.UseVisualStyleBackColor = true;
            this.buttonFormÚvod2.Click += new System.EventHandler(this.buttonFormÚvod2_Click);
            // 
            // buttonForm3
            // 
            this.buttonForm3.Enabled = false;
            this.buttonForm3.Font = new System.Drawing.Font("Tempus Sans ITC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonForm3.Location = new System.Drawing.Point(32, 289);
            this.buttonForm3.Name = "buttonForm3";
            this.buttonForm3.Size = new System.Drawing.Size(161, 53);
            this.buttonForm3.TabIndex = 3;
            this.buttonForm3.Text = "Hledání fekálií";
            this.buttonForm3.UseVisualStyleBackColor = true;
            this.buttonForm3.Click += new System.EventHandler(this.buttonForm3_Click);
            // 
            // buttonForm4
            // 
            this.buttonForm4.Font = new System.Drawing.Font("Tempus Sans ITC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonForm4.Location = new System.Drawing.Point(199, 289);
            this.buttonForm4.Name = "buttonForm4";
            this.buttonForm4.Size = new System.Drawing.Size(161, 53);
            this.buttonForm4.TabIndex = 4;
            this.buttonForm4.Text = "Neposedná tlačítka";
            this.buttonForm4.UseVisualStyleBackColor = true;
            this.buttonForm4.Click += new System.EventHandler(this.buttonForm4_Click);
            // 
            // labelUvítání
            // 
            this.labelUvítání.AutoSize = true;
            this.labelUvítání.Font = new System.Drawing.Font("Perpetua Titling MT", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUvítání.ForeColor = System.Drawing.Color.DarkRed;
            this.labelUvítání.Location = new System.Drawing.Point(56, 21);
            this.labelUvítání.Name = "labelUvítání";
            this.labelUvítání.Size = new System.Drawing.Size(278, 32);
            this.labelUvítání.TabIndex = 5;
            this.labelUvítání.Text = "Vítej na rozcestí!";
            // 
            // pictureBoxRozcestí
            // 
            this.pictureBoxRozcestí.Image = global::Share.Properties.Resources.Rozcestník;
            this.pictureBoxRozcestí.Location = new System.Drawing.Point(103, 66);
            this.pictureBoxRozcestí.Name = "pictureBoxRozcestí";
            this.pictureBoxRozcestí.Size = new System.Drawing.Size(184, 113);
            this.pictureBoxRozcestí.TabIndex = 6;
            this.pictureBoxRozcestí.TabStop = false;
            // 
            // Form0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 429);
            this.Controls.Add(this.pictureBoxRozcestí);
            this.Controls.Add(this.labelUvítání);
            this.Controls.Add(this.buttonForm4);
            this.Controls.Add(this.buttonForm3);
            this.Controls.Add(this.buttonFormÚvod2);
            this.Controls.Add(this.buttonForm1);
            this.Controls.Add(this.labelRozcestníkPopis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(700, 200);
            this.MaximizeBox = false;
            this.Name = "Form0";
            this.Text = "Rozcestník";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRozcestí)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRozcestníkPopis;
        private System.Windows.Forms.Button buttonForm1;
        private System.Windows.Forms.Button buttonFormÚvod2;
        private System.Windows.Forms.Button buttonForm3;
        private System.Windows.Forms.Button buttonForm4;
        private System.Windows.Forms.Label labelUvítání;
        private System.Windows.Forms.PictureBox pictureBoxRozcestí;
    }
}