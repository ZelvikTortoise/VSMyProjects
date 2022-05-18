namespace ŠárkaKvíz
{
    partial class FormMixQuiz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMixQuiz));
            this.pictureBoxVěc = new System.Windows.Forms.PictureBox();
            this.labelZadání = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.labelOdpověď = new System.Windows.Forms.Label();
            this.labelNadpis = new System.Windows.Forms.Label();
            this.labelPopisSkóre = new System.Windows.Forms.Label();
            this.labelSkóre = new System.Windows.Forms.Label();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonZměna = new System.Windows.Forms.Button();
            this.labelVěc = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVěc)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxVěc
            // 
            this.pictureBoxVěc.Image = global::ŠárkaKvíz.Properties.Resources.Cigarety;
            this.pictureBoxVěc.Location = new System.Drawing.Point(204, 139);
            this.pictureBoxVěc.Name = "pictureBoxVěc";
            this.pictureBoxVěc.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxVěc.TabIndex = 0;
            this.pictureBoxVěc.TabStop = false;
            // 
            // labelZadání
            // 
            this.labelZadání.AutoSize = true;
            this.labelZadání.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZadání.Location = new System.Drawing.Point(19, 64);
            this.labelZadání.Name = "labelZadání";
            this.labelZadání.Size = new System.Drawing.Size(481, 23);
            this.labelZadání.TabIndex = 1;
            this.labelZadání.Text = "Ke které z nabízených osobností se nejvíce hodí věc na obrázku?";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Bow_new;
            this.button1.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.Indigo;
            this.button1.Location = new System.Drawing.Point(56, 340);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(0, 62, 0, 0);
            this.button1.Size = new System.Drawing.Size(100, 100);
            this.button1.TabIndex = 1;
            this.button1.Text = "Bow";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Emilie_new;
            this.button2.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.ForeColor = System.Drawing.Color.DarkRed;
            this.button2.Location = new System.Drawing.Point(204, 340);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(0, 62, 0, 0);
            this.button2.Size = new System.Drawing.Size(100, 100);
            this.button2.TabIndex = 2;
            this.button2.Text = "Emilie";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Amber_new;
            this.button3.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.ForeColor = System.Drawing.Color.DarkRed;
            this.button3.Location = new System.Drawing.Point(352, 340);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(0, 62, 0, 0);
            this.button3.Size = new System.Drawing.Size(100, 100);
            this.button3.TabIndex = 3;
            this.button3.Text = "Amber";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelOdpověď
            // 
            this.labelOdpověď.AutoSize = true;
            this.labelOdpověď.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOdpověď.Location = new System.Drawing.Point(145, 286);
            this.labelOdpověď.Name = "labelOdpověď";
            this.labelOdpověď.Size = new System.Drawing.Size(220, 38);
            this.labelOdpověď.TabIndex = 5;
            this.labelOdpověď.Text = "Vyber osobnost:";
            // 
            // labelNadpis
            // 
            this.labelNadpis.AutoSize = true;
            this.labelNadpis.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNadpis.Location = new System.Drawing.Point(158, 15);
            this.labelNadpis.Name = "labelNadpis";
            this.labelNadpis.Size = new System.Drawing.Size(183, 49);
            this.labelNadpis.TabIndex = 6;
            this.labelNadpis.Text = "Kvíz (mix)";
            // 
            // labelPopisSkóre
            // 
            this.labelPopisSkóre.AutoSize = true;
            this.labelPopisSkóre.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPopisSkóre.Location = new System.Drawing.Point(12, 493);
            this.labelPopisSkóre.Name = "labelPopisSkóre";
            this.labelPopisSkóre.Size = new System.Drawing.Size(70, 26);
            this.labelPopisSkóre.TabIndex = 7;
            this.labelPopisSkóre.Text = "Skóre:";
            // 
            // labelSkóre
            // 
            this.labelSkóre.AutoSize = true;
            this.labelSkóre.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkóre.ForeColor = System.Drawing.Color.Red;
            this.labelSkóre.Location = new System.Drawing.Point(88, 493);
            this.labelSkóre.Name = "labelSkóre";
            this.labelSkóre.Size = new System.Drawing.Size(24, 26);
            this.labelSkóre.TabIndex = 8;
            this.labelSkóre.Text = "0";
            // 
            // buttonKonec
            // 
            this.buttonKonec.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonKonec.Location = new System.Drawing.Point(417, 474);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(83, 45);
            this.buttonKonec.TabIndex = 5;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // buttonZměna
            // 
            this.buttonZměna.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonZměna.Location = new System.Drawing.Point(328, 474);
            this.buttonZměna.Name = "buttonZměna";
            this.buttonZměna.Size = new System.Drawing.Size(83, 45);
            this.buttonZměna.TabIndex = 4;
            this.buttonZměna.Text = "Zpět";
            this.buttonZměna.UseVisualStyleBackColor = true;
            this.buttonZměna.Click += new System.EventHandler(this.buttonZměna_Click);
            // 
            // labelVěc
            // 
            this.labelVěc.AutoSize = true;
            this.labelVěc.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVěc.Location = new System.Drawing.Point(204, 107);
            this.labelVěc.Name = "labelVěc";
            this.labelVěc.Size = new System.Drawing.Size(103, 29);
            this.labelVěc.TabIndex = 11;
            this.labelVěc.Text = "Obrázek:";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInfo.Location = new System.Drawing.Point(310, 175);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(112, 29);
            this.labelInfo.TabIndex = 12;
            this.labelInfo.Text = "(Cigarety)";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelResult.ForeColor = System.Drawing.Color.Green;
            this.labelResult.Location = new System.Drawing.Point(185, 481);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(125, 38);
            this.labelResult.TabIndex = 13;
            this.labelResult.Text = "Správně!";
            this.labelResult.Visible = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormMixQuiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 528);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelVěc);
            this.Controls.Add(this.buttonZměna);
            this.Controls.Add(this.buttonKonec);
            this.Controls.Add(this.labelSkóre);
            this.Controls.Add(this.labelPopisSkóre);
            this.Controls.Add(this.labelNadpis);
            this.Controls.Add(this.labelOdpověď);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelZadání);
            this.Controls.Add(this.pictureBoxVěc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMixQuiz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Šárka kvíz (mix)";
            this.Shown += new System.EventHandler(this.FormMixQuiz_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVěc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxVěc;
        private System.Windows.Forms.Label labelZadání;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelOdpověď;
        private System.Windows.Forms.Label labelNadpis;
        private System.Windows.Forms.Label labelPopisSkóre;
        private System.Windows.Forms.Label labelSkóre;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Button buttonZměna;
        private System.Windows.Forms.Label labelVěc;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Timer timer;
    }
}