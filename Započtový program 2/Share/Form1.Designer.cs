namespace Share
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelPozdrav = new System.Windows.Forms.Label();
            this.labelOtázka = new System.Windows.Forms.Label();
            this.groupBoxOdpovědi = new System.Windows.Forms.GroupBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.buttonOdpověz = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.textBoxVtip = new System.Windows.Forms.TextBox();
            this.labelVtip = new System.Windows.Forms.Label();
            this.buttonKonec = new System.Windows.Forms.Button();
            this.labelČas = new System.Windows.Forms.Label();
            this.timerČas = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonProcházet = new System.Windows.Forms.Button();
            this.buttonUložit = new System.Windows.Forms.Button();
            this.textBoxCesta = new System.Windows.Forms.TextBox();
            this.groupBoxOdpovědi.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPozdrav
            // 
            this.labelPozdrav.AutoSize = true;
            this.labelPozdrav.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPozdrav.Location = new System.Drawing.Point(18, 51);
            this.labelPozdrav.Name = "labelPozdrav";
            this.labelPozdrav.Size = new System.Drawing.Size(0, 67);
            this.labelPozdrav.TabIndex = 0;
            this.labelPozdrav.Visible = false;
            // 
            // labelOtázka
            // 
            this.labelOtázka.AutoSize = true;
            this.labelOtázka.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOtázka.Location = new System.Drawing.Point(12, 25);
            this.labelOtázka.Name = "labelOtázka";
            this.labelOtázka.Size = new System.Drawing.Size(103, 20);
            this.labelOtázka.TabIndex = 1;
            this.labelOtázka.Text = "Krátký text :D";
            this.labelOtázka.Visible = false;
            // 
            // groupBoxOdpovědi
            // 
            this.groupBoxOdpovědi.Controls.Add(this.radioButton6);
            this.groupBoxOdpovědi.Controls.Add(this.radioButton5);
            this.groupBoxOdpovědi.Controls.Add(this.radioButton4);
            this.groupBoxOdpovědi.Controls.Add(this.radioButton3);
            this.groupBoxOdpovědi.Controls.Add(this.radioButton2);
            this.groupBoxOdpovědi.Controls.Add(this.radioButton1);
            this.groupBoxOdpovědi.Location = new System.Drawing.Point(24, 51);
            this.groupBoxOdpovědi.Name = "groupBoxOdpovědi";
            this.groupBoxOdpovědi.Size = new System.Drawing.Size(122, 157);
            this.groupBoxOdpovědi.TabIndex = 6;
            this.groupBoxOdpovědi.TabStop = false;
            this.groupBoxOdpovědi.Visible = false;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(6, 139);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(14, 13);
            this.radioButton6.TabIndex = 11;
            this.radioButton6.TabStop = true;
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Visible = false;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 116);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(14, 13);
            this.radioButton5.TabIndex = 10;
            this.radioButton5.TabStop = true;
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Visible = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 93);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(14, 13);
            this.radioButton4.TabIndex = 9;
            this.radioButton4.TabStop = true;
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Visible = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 70);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(14, 13);
            this.radioButton3.TabIndex = 8;
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 47);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            // 
            // buttonOdpověz
            // 
            this.buttonOdpověz.Location = new System.Drawing.Point(50, 223);
            this.buttonOdpověz.Name = "buttonOdpověz";
            this.buttonOdpověz.Size = new System.Drawing.Size(76, 23);
            this.buttonOdpověz.TabIndex = 7;
            this.buttonOdpověz.Text = "Odpověz";
            this.buttonOdpověz.UseVisualStyleBackColor = true;
            this.buttonOdpověz.Visible = false;
            this.buttonOdpověz.Click += new System.EventHandler(this.buttonOdpověz_Click);
            this.buttonOdpověz.MouseEnter += new System.EventHandler(this.buttonOdpověz_MouseEnter);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // textBoxVtip
            // 
            this.textBoxVtip.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxVtip.Location = new System.Drawing.Point(162, 27);
            this.textBoxVtip.Multiline = true;
            this.textBoxVtip.Name = "textBoxVtip";
            this.textBoxVtip.ReadOnly = true;
            this.textBoxVtip.Size = new System.Drawing.Size(158, 128);
            this.textBoxVtip.TabIndex = 8;
            this.textBoxVtip.Visible = false;
            // 
            // labelVtip
            // 
            this.labelVtip.AutoSize = true;
            this.labelVtip.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVtip.Location = new System.Drawing.Point(12, 30);
            this.labelVtip.Name = "labelVtip";
            this.labelVtip.Size = new System.Drawing.Size(36, 18);
            this.labelVtip.TabIndex = 9;
            this.labelVtip.Text = "Vtip:";
            this.labelVtip.Visible = false;
            // 
            // buttonKonec
            // 
            this.buttonKonec.Location = new System.Drawing.Point(252, 223);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(62, 25);
            this.buttonKonec.TabIndex = 10;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Visible = false;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // labelČas
            // 
            this.labelČas.AutoSize = true;
            this.labelČas.Location = new System.Drawing.Point(3, 243);
            this.labelČas.Name = "labelČas";
            this.labelČas.Size = new System.Drawing.Size(34, 13);
            this.labelČas.TabIndex = 11;
            this.labelČas.Text = "00:00";
            this.labelČas.Visible = false;
            // 
            // timerČas
            // 
            this.timerČas.Enabled = true;
            this.timerČas.Tick += new System.EventHandler(this.timerČas_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.OverwritePrompt = false;
            // 
            // buttonProcházet
            // 
            this.buttonProcházet.Location = new System.Drawing.Point(162, 150);
            this.buttonProcházet.Name = "buttonProcházet";
            this.buttonProcházet.Size = new System.Drawing.Size(79, 30);
            this.buttonProcházet.TabIndex = 12;
            this.buttonProcházet.Text = "Procházet...";
            this.buttonProcházet.UseVisualStyleBackColor = true;
            this.buttonProcházet.Visible = false;
            this.buttonProcházet.Click += new System.EventHandler(this.buttonProcházet_Click);
            // 
            // buttonUložit
            // 
            this.buttonUložit.Location = new System.Drawing.Point(162, 212);
            this.buttonUložit.Name = "buttonUložit";
            this.buttonUložit.Size = new System.Drawing.Size(79, 30);
            this.buttonUložit.TabIndex = 13;
            this.buttonUložit.Text = "Uložit";
            this.buttonUložit.UseVisualStyleBackColor = true;
            this.buttonUložit.Visible = false;
            this.buttonUložit.Click += new System.EventHandler(this.buttonUložit_Click);
            // 
            // textBoxCesta
            // 
            this.textBoxCesta.Location = new System.Drawing.Point(162, 186);
            this.textBoxCesta.Name = "textBoxCesta";
            this.textBoxCesta.Size = new System.Drawing.Size(79, 20);
            this.textBoxCesta.TabIndex = 14;
            this.textBoxCesta.Text = "C:\\Users\\Šárka\\Desktop\\Sny.txt";
            this.textBoxCesta.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 260);
            this.Controls.Add(this.textBoxCesta);
            this.Controls.Add(this.labelČas);
            this.Controls.Add(this.buttonUložit);
            this.Controls.Add(this.buttonKonec);
            this.Controls.Add(this.buttonProcházet);
            this.Controls.Add(this.labelVtip);
            this.Controls.Add(this.textBoxVtip);
            this.Controls.Add(this.buttonOdpověz);
            this.Controls.Add(this.groupBoxOdpovědi);
            this.Controls.Add(this.labelOtázka);
            this.Controls.Add(this.labelPozdrav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(700, 200);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Kamarád";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBoxOdpovědi.ResumeLayout(false);
            this.groupBoxOdpovědi.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPozdrav;
        private System.Windows.Forms.Label labelOtázka;
        private System.Windows.Forms.GroupBox groupBoxOdpovědi;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button buttonOdpověz;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.TextBox textBoxVtip;
        private System.Windows.Forms.Label labelVtip;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.Label labelČas;
        private System.Windows.Forms.Timer timerČas;
        private System.Windows.Forms.TextBox textBoxCesta;
        private System.Windows.Forms.Button buttonUložit;
        private System.Windows.Forms.Button buttonProcházet;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

