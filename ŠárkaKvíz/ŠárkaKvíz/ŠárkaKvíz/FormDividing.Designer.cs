namespace ŠárkaKvíz
{
    partial class FormDividing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDividing));
            this.buttonMix = new System.Windows.Forms.Button();
            this.buttonOsobnosti = new System.Windows.Forms.Button();
            this.labelNázev = new System.Windows.Forms.Label();
            this.labelZadání = new System.Windows.Forms.Label();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.buttonPointless = new System.Windows.Forms.Button();
            this.timerBarvy = new System.Windows.Forms.Timer(this.components);
            this.timerOdpočet = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonMix
            // 
            this.buttonMix.BackColor = System.Drawing.Color.Bisque;
            this.buttonMix.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMix.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonMix.ForeColor = System.Drawing.Color.SaddleBrown;
            this.buttonMix.Location = new System.Drawing.Point(273, 147);
            this.buttonMix.Name = "buttonMix";
            this.buttonMix.Size = new System.Drawing.Size(119, 79);
            this.buttonMix.TabIndex = 1;
            this.buttonMix.Text = "Mix";
            this.buttonMix.UseVisualStyleBackColor = false;
            this.buttonMix.Click += new System.EventHandler(this.buttonMix_Click);
            // 
            // buttonOsobnosti
            // 
            this.buttonOsobnosti.BackColor = System.Drawing.Color.Bisque;
            this.buttonOsobnosti.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOsobnosti.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonOsobnosti.ForeColor = System.Drawing.Color.SaddleBrown;
            this.buttonOsobnosti.Location = new System.Drawing.Point(148, 147);
            this.buttonOsobnosti.Name = "buttonOsobnosti";
            this.buttonOsobnosti.Size = new System.Drawing.Size(119, 79);
            this.buttonOsobnosti.TabIndex = 0;
            this.buttonOsobnosti.Text = "Osobnosti";
            this.buttonOsobnosti.UseVisualStyleBackColor = false;
            this.buttonOsobnosti.Click += new System.EventHandler(this.buttonOsobnosti_Click);
            // 
            // labelNázev
            // 
            this.labelNázev.AutoSize = true;
            this.labelNázev.BackColor = System.Drawing.Color.Transparent;
            this.labelNázev.Font = new System.Drawing.Font("Comic Sans MS", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNázev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelNázev.Location = new System.Drawing.Point(77, 8);
            this.labelNázev.Name = "labelNázev";
            this.labelNázev.Size = new System.Drawing.Size(392, 90);
            this.labelNázev.TabIndex = 2;
            this.labelNázev.Text = "Hlavní menu";
            // 
            // labelZadání
            // 
            this.labelZadání.AutoSize = true;
            this.labelZadání.BackColor = System.Drawing.Color.Transparent;
            this.labelZadání.Font = new System.Drawing.Font("Comic Sans MS", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelZadání.ForeColor = System.Drawing.Color.Maroon;
            this.labelZadání.Location = new System.Drawing.Point(166, 99);
            this.labelZadání.Name = "labelZadání";
            this.labelZadání.Size = new System.Drawing.Size(209, 34);
            this.labelZadání.TabIndex = 3;
            this.labelZadání.Text = "Zvol si typ kvízu:";
            // 
            // buttonKonec
            // 
            this.buttonKonec.BackColor = System.Drawing.Color.Bisque;
            this.buttonKonec.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonKonec.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonKonec.ForeColor = System.Drawing.Color.SaddleBrown;
            this.buttonKonec.Location = new System.Drawing.Point(393, 275);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(123, 60);
            this.buttonKonec.TabIndex = 2;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = false;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // buttonPointless
            // 
            this.buttonPointless.BackColor = System.Drawing.Color.Bisque;
            this.buttonPointless.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPointless.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPointless.ForeColor = System.Drawing.Color.SaddleBrown;
            this.buttonPointless.Location = new System.Drawing.Point(12, 275);
            this.buttonPointless.Name = "buttonPointless";
            this.buttonPointless.Size = new System.Drawing.Size(123, 60);
            this.buttonPointless.TabIndex = 3;
            this.buttonPointless.Text = "Pointless.";
            this.buttonPointless.UseVisualStyleBackColor = false;
            this.buttonPointless.Click += new System.EventHandler(this.buttonPointless_Click);
            // 
            // timerBarvy
            // 
            this.timerBarvy.Interval = 250;
            this.timerBarvy.Tick += new System.EventHandler(this.timerBarvy_Tick);
            // 
            // timerOdpočet
            // 
            this.timerOdpočet.Interval = 1000;
            this.timerOdpočet.Tick += new System.EventHandler(this.timerOdpočet_Tick);
            // 
            // FormDividing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ŠárkaKvíz.Properties.Resources.Background__dividing_form__new_2;
            this.ClientSize = new System.Drawing.Size(528, 347);
            this.Controls.Add(this.buttonPointless);
            this.Controls.Add(this.buttonKonec);
            this.Controls.Add(this.labelZadání);
            this.Controls.Add(this.labelNázev);
            this.Controls.Add(this.buttonOsobnosti);
            this.Controls.Add(this.buttonMix);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormDividing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Šárka kvíz";
            this.Shown += new System.EventHandler(this.FormDividing_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMix;
        private System.Windows.Forms.Button buttonOsobnosti;
        private System.Windows.Forms.Label labelNázev;
        private System.Windows.Forms.Label labelZadání;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Button buttonPointless;
        private System.Windows.Forms.Timer timerBarvy;
        private System.Windows.Forms.Timer timerOdpočet;
    }
}