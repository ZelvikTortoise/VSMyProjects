namespace ŠárkaKvíz
{
    partial class FormUrčitáOsobnost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUrčitáOsobnost));
            this.buttonZpět = new System.Windows.Forms.Button();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.labelSkóre = new System.Windows.Forms.Label();
            this.labelPopisSkóre = new System.Windows.Forms.Label();
            this.labelNadpis = new System.Windows.Forms.Label();
            this.buttonVěc3 = new System.Windows.Forms.Button();
            this.buttonVěc2 = new System.Windows.Forms.Button();
            this.buttonVěc1 = new System.Windows.Forms.Button();
            this.labelZadání = new System.Windows.Forms.Label();
            this.buttonOsobnost = new System.Windows.Forms.Button();
            this.labelOsobnost = new System.Windows.Forms.Label();
            this.labelOdpověď = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonZpět
            // 
            this.buttonZpět.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonZpět.Location = new System.Drawing.Point(328, 474);
            this.buttonZpět.Name = "buttonZpět";
            this.buttonZpět.Size = new System.Drawing.Size(83, 45);
            this.buttonZpět.TabIndex = 3;
            this.buttonZpět.Text = "Zpět";
            this.buttonZpět.UseVisualStyleBackColor = true;
            this.buttonZpět.Click += new System.EventHandler(this.buttonZpět_Click);
            // 
            // buttonKonec
            // 
            this.buttonKonec.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonKonec.Location = new System.Drawing.Point(417, 474);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(83, 45);
            this.buttonKonec.TabIndex = 4;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // labelSkóre
            // 
            this.labelSkóre.AutoSize = true;
            this.labelSkóre.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkóre.ForeColor = System.Drawing.Color.Red;
            this.labelSkóre.Location = new System.Drawing.Point(88, 493);
            this.labelSkóre.Name = "labelSkóre";
            this.labelSkóre.Size = new System.Drawing.Size(24, 26);
            this.labelSkóre.TabIndex = 19;
            this.labelSkóre.Text = "0";
            // 
            // labelPopisSkóre
            // 
            this.labelPopisSkóre.AutoSize = true;
            this.labelPopisSkóre.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPopisSkóre.Location = new System.Drawing.Point(12, 493);
            this.labelPopisSkóre.Name = "labelPopisSkóre";
            this.labelPopisSkóre.Size = new System.Drawing.Size(70, 26);
            this.labelPopisSkóre.TabIndex = 18;
            this.labelPopisSkóre.Text = "Skóre:";
            // 
            // labelNadpis
            // 
            this.labelNadpis.AutoSize = true;
            this.labelNadpis.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNadpis.Location = new System.Drawing.Point(158, 15);
            this.labelNadpis.Name = "labelNadpis";
            this.labelNadpis.Size = new System.Drawing.Size(222, 49);
            this.labelNadpis.TabIndex = 17;
            this.labelNadpis.Text = "Kvíz (Share)";
            // 
            // buttonVěc3
            // 
            this.buttonVěc3.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Minecraft;
            this.buttonVěc3.Location = new System.Drawing.Point(352, 340);
            this.buttonVěc3.Name = "buttonVěc3";
            this.buttonVěc3.Size = new System.Drawing.Size(100, 100);
            this.buttonVěc3.TabIndex = 2;
            this.buttonVěc3.UseVisualStyleBackColor = true;
            this.buttonVěc3.Click += new System.EventHandler(this.buttonVěc3_Click);
            // 
            // buttonVěc2
            // 
            this.buttonVěc2.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Knížky;
            this.buttonVěc2.Location = new System.Drawing.Point(204, 340);
            this.buttonVěc2.Name = "buttonVěc2";
            this.buttonVěc2.Size = new System.Drawing.Size(100, 100);
            this.buttonVěc2.TabIndex = 1;
            this.buttonVěc2.UseVisualStyleBackColor = true;
            this.buttonVěc2.Click += new System.EventHandler(this.buttonVěc2_Click);
            // 
            // buttonVěc1
            // 
            this.buttonVěc1.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Cigarety;
            this.buttonVěc1.Location = new System.Drawing.Point(56, 340);
            this.buttonVěc1.Name = "buttonVěc1";
            this.buttonVěc1.Size = new System.Drawing.Size(100, 100);
            this.buttonVěc1.TabIndex = 0;
            this.buttonVěc1.UseVisualStyleBackColor = true;
            this.buttonVěc1.Click += new System.EventHandler(this.buttonVěc1_Click);
            // 
            // labelZadání
            // 
            this.labelZadání.AutoSize = true;
            this.labelZadání.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZadání.Location = new System.Drawing.Point(49, 64);
            this.labelZadání.Name = "labelZadání";
            this.labelZadání.Size = new System.Drawing.Size(426, 23);
            this.labelZadání.TabIndex = 12;
            this.labelZadání.Text = "Který obrázek se nejlépe hodí k vámi vybrané osobnosti?";
            // 
            // buttonOsobnost
            // 
            this.buttonOsobnost.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Share_new;
            this.buttonOsobnost.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonOsobnost.ForeColor = System.Drawing.Color.Indigo;
            this.buttonOsobnost.Location = new System.Drawing.Point(204, 139);
            this.buttonOsobnost.Name = "buttonOsobnost";
            this.buttonOsobnost.Padding = new System.Windows.Forms.Padding(0, 62, 0, 0);
            this.buttonOsobnost.Size = new System.Drawing.Size(100, 100);
            this.buttonOsobnost.TabIndex = 22;
            this.buttonOsobnost.TabStop = false;
            this.buttonOsobnost.Text = "Share";
            this.buttonOsobnost.UseVisualStyleBackColor = true;
            this.buttonOsobnost.Click += new System.EventHandler(this.buttonOsobnost_Click_1);
            // 
            // labelOsobnost
            // 
            this.labelOsobnost.AutoSize = true;
            this.labelOsobnost.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOsobnost.Location = new System.Drawing.Point(174, 110);
            this.labelOsobnost.Name = "labelOsobnost";
            this.labelOsobnost.Size = new System.Drawing.Size(173, 26);
            this.labelOsobnost.TabIndex = 23;
            this.labelOsobnost.Text = "Vybraná osobnost:";
            // 
            // labelOdpověď
            // 
            this.labelOdpověď.AutoSize = true;
            this.labelOdpověď.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOdpověď.Location = new System.Drawing.Point(154, 286);
            this.labelOdpověď.Name = "labelOdpověď";
            this.labelOdpověď.Size = new System.Drawing.Size(211, 38);
            this.labelOdpověď.TabIndex = 24;
            this.labelOdpověď.Text = "Vyber obrázek:";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResult.ForeColor = System.Drawing.Color.Green;
            this.labelResult.Location = new System.Drawing.Point(192, 481);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(125, 38);
            this.labelResult.TabIndex = 25;
            this.labelResult.Text = "Správně!";
            this.labelResult.Visible = false;
            // 
            // FormUrčitáOsobnost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 528);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelOdpověď);
            this.Controls.Add(this.labelOsobnost);
            this.Controls.Add(this.buttonOsobnost);
            this.Controls.Add(this.buttonZpět);
            this.Controls.Add(this.buttonKonec);
            this.Controls.Add(this.labelSkóre);
            this.Controls.Add(this.labelPopisSkóre);
            this.Controls.Add(this.labelNadpis);
            this.Controls.Add(this.buttonVěc3);
            this.Controls.Add(this.buttonVěc2);
            this.Controls.Add(this.buttonVěc1);
            this.Controls.Add(this.labelZadání);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormUrčitáOsobnost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Šárka kvíz (Share)";
            this.Shown += new System.EventHandler(this.FormUrčitáOsobnost_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonZpět;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Label labelSkóre;
        private System.Windows.Forms.Label labelPopisSkóre;
        private System.Windows.Forms.Label labelNadpis;
        private System.Windows.Forms.Button buttonVěc3;
        private System.Windows.Forms.Button buttonVěc2;
        private System.Windows.Forms.Button buttonVěc1;
        private System.Windows.Forms.Label labelZadání;
        private System.Windows.Forms.Button buttonOsobnost;
        private System.Windows.Forms.Label labelOsobnost;
        private System.Windows.Forms.Label labelOdpověď;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelResult;

    }
}