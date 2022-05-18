namespace CSHra
{
    partial class Form1
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
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Oneplayerpanel = new System.Windows.Forms.Panel();
            this.Oneplayerbutton = new System.Windows.Forms.Button();
            this.Twoplayersbutton = new System.Windows.Forms.Button();
            this.Twoplayerspanel = new System.Windows.Forms.Panel();
            this.Settings = new System.Windows.Forms.Button();
            this.Settingspanel = new System.Windows.Forms.Panel();
            this.Soundbutton = new System.Windows.Forms.Button();
            this.Timeritem = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.Timerblesku = new System.Windows.Forms.HScrollBar();
            this.label4 = new System.Windows.Forms.Label();
            this.Timerbomb = new System.Windows.Forms.HScrollBar();
            this.label3 = new System.Windows.Forms.Label();
            this.Timerhracu = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Timerhry = new System.Windows.Forms.HScrollBar();
            this.timerVybuchBomb1 = new System.Windows.Forms.Timer(this.components);
            this.timerPohybHráč1 = new System.Windows.Forms.Timer(this.components);
            this.timerPohybHráč2 = new System.Windows.Forms.Timer(this.components);
            this.timerVybuchBomb2 = new System.Windows.Forms.Timer(this.components);
            this.timerPrisery = new System.Windows.Forms.Timer(this.components);
            this.Settingspanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Oneplayerpanel
            // 
            this.Oneplayerpanel.Location = new System.Drawing.Point(42, 199);
            this.Oneplayerpanel.Name = "Oneplayerpanel";
            this.Oneplayerpanel.Size = new System.Drawing.Size(741, 368);
            this.Oneplayerpanel.TabIndex = 1;
            this.Oneplayerpanel.Visible = false;
            // 
            // Oneplayerbutton
            // 
            this.Oneplayerbutton.Location = new System.Drawing.Point(93, 69);
            this.Oneplayerbutton.Name = "Oneplayerbutton";
            this.Oneplayerbutton.Size = new System.Drawing.Size(229, 80);
            this.Oneplayerbutton.TabIndex = 2;
            this.Oneplayerbutton.Text = "1 hráč";
            this.Oneplayerbutton.UseVisualStyleBackColor = true;
            this.Oneplayerbutton.Click += new System.EventHandler(this.Oneplayer_Click);
            // 
            // Twoplayersbutton
            // 
            this.Twoplayersbutton.Location = new System.Drawing.Point(480, 69);
            this.Twoplayersbutton.Name = "Twoplayersbutton";
            this.Twoplayersbutton.Size = new System.Drawing.Size(229, 80);
            this.Twoplayersbutton.TabIndex = 3;
            this.Twoplayersbutton.Text = "2 hráči";
            this.Twoplayersbutton.UseVisualStyleBackColor = true;
            this.Twoplayersbutton.Click += new System.EventHandler(this.Twoplayersbutton_Click);
            // 
            // Twoplayerspanel
            // 
            this.Twoplayerspanel.Location = new System.Drawing.Point(42, 199);
            this.Twoplayerspanel.Name = "Twoplayerspanel";
            this.Twoplayerspanel.Size = new System.Drawing.Size(741, 368);
            this.Twoplayerspanel.TabIndex = 2;
            this.Twoplayerspanel.Visible = false;
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(902, 69);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(215, 80);
            this.Settings.TabIndex = 4;
            this.Settings.Text = "Nastavení";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Settingspanel
            // 
            this.Settingspanel.Controls.Add(this.Soundbutton);
            this.Settingspanel.Controls.Add(this.Timeritem);
            this.Settingspanel.Controls.Add(this.label5);
            this.Settingspanel.Controls.Add(this.Timerblesku);
            this.Settingspanel.Controls.Add(this.label4);
            this.Settingspanel.Controls.Add(this.Timerbomb);
            this.Settingspanel.Controls.Add(this.label3);
            this.Settingspanel.Controls.Add(this.Timerhracu);
            this.Settingspanel.Controls.Add(this.label2);
            this.Settingspanel.Controls.Add(this.label1);
            this.Settingspanel.Controls.Add(this.Timerhry);
            this.Settingspanel.Location = new System.Drawing.Point(875, 198);
            this.Settingspanel.Name = "Settingspanel";
            this.Settingspanel.Size = new System.Drawing.Size(308, 369);
            this.Settingspanel.TabIndex = 5;
            this.Settingspanel.Visible = false;
            this.Settingspanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Settingspanel_Paint);
            // 
            // Soundbutton
            // 
            this.Soundbutton.Image = global::CSHra.Properties.Resources.speaker;
            this.Soundbutton.Location = new System.Drawing.Point(186, 260);
            this.Soundbutton.Name = "Soundbutton";
            this.Soundbutton.Size = new System.Drawing.Size(105, 95);
            this.Soundbutton.TabIndex = 10;
            this.Soundbutton.UseVisualStyleBackColor = true;
            this.Soundbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Timeritem
            // 
            this.Timeritem.LargeChange = 1;
            this.Timeritem.Location = new System.Drawing.Point(17, 212);
            this.Timeritem.Maximum = 30;
            this.Timeritem.Minimum = 5;
            this.Timeritem.Name = "Timeritem";
            this.Timeritem.Size = new System.Drawing.Size(246, 24);
            this.Timeritem.TabIndex = 9;
            this.Timeritem.Value = 15;
            this.Timeritem.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Timeritem_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Timer itemů";
            // 
            // Timerblesku
            // 
            this.Timerblesku.LargeChange = 1;
            this.Timerblesku.Location = new System.Drawing.Point(17, 161);
            this.Timerblesku.Maximum = 6;
            this.Timerblesku.Minimum = 2;
            this.Timerblesku.Name = "Timerblesku";
            this.Timerblesku.Size = new System.Drawing.Size(246, 24);
            this.Timerblesku.TabIndex = 7;
            this.Timerblesku.Value = 3;
            this.Timerblesku.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Timerblesku_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Timer blesků";
            // 
            // Timerbomb
            // 
            this.Timerbomb.LargeChange = 1;
            this.Timerbomb.Location = new System.Drawing.Point(17, 120);
            this.Timerbomb.Maximum = 8;
            this.Timerbomb.Minimum = 3;
            this.Timerbomb.Name = "Timerbomb";
            this.Timerbomb.Size = new System.Drawing.Size(246, 24);
            this.Timerbomb.TabIndex = 5;
            this.Timerbomb.Value = 5;
            this.Timerbomb.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Timerbomb_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Timer bomb";
            // 
            // Timerhracu
            // 
            this.Timerhracu.LargeChange = 1;
            this.Timerhracu.Location = new System.Drawing.Point(17, 79);
            this.Timerhracu.Maximum = 10;
            this.Timerhracu.Minimum = 1;
            this.Timerhracu.Name = "Timerhracu";
            this.Timerhracu.Size = new System.Drawing.Size(246, 24);
            this.Timerhracu.TabIndex = 3;
            this.Timerhracu.Value = 6;
            this.Timerhracu.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Timerhracu_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rychlost hráčů";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rychlost hry";
            // 
            // Timerhry
            // 
            this.Timerhry.LargeChange = 1;
            this.Timerhry.Location = new System.Drawing.Point(17, 38);
            this.Timerhry.Maximum = 10;
            this.Timerhry.Minimum = 1;
            this.Timerhry.Name = "Timerhry";
            this.Timerhry.Size = new System.Drawing.Size(246, 24);
            this.Timerhry.TabIndex = 0;
            this.Timerhry.Value = 3;
            this.Timerhry.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Timerhry_Scroll);
            // 
            // timerVybuchBomb1
            // 
            this.timerVybuchBomb1.Tick += new System.EventHandler(this.timerVybuchBomb1_Tick);
            // 
            // timerPohybHráč1
            // 
            this.timerPohybHráč1.Tick += new System.EventHandler(this.timerPohybHráč1_Tick);
            // 
            // timerPohybHráč2
            // 
            this.timerPohybHráč2.Tick += new System.EventHandler(this.timerPohybHráč2_Tick);
            // 
            // timerVybuchBomb2
            // 
            this.timerVybuchBomb2.Tick += new System.EventHandler(this.timerVybuchBomb2_Tick);
            // 
            // timerPrisery
            // 
            this.timerPrisery.Tick += new System.EventHandler(this.timerPrisery_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 953);
            this.Controls.Add(this.Settingspanel);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.Twoplayerspanel);
            this.Controls.Add(this.Twoplayersbutton);
            this.Controls.Add(this.Oneplayerbutton);
            this.Controls.Add(this.Oneplayerpanel);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Ahoj, mám lepší název díky vlastnosti Form1.Text";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Settingspanel.ResumeLayout(false);
            this.Settingspanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Panel Oneplayerpanel;
        private System.Windows.Forms.Button Oneplayerbutton;
        private System.Windows.Forms.Button Twoplayersbutton;
        private System.Windows.Forms.Panel Twoplayerspanel;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Panel Settingspanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar Timerhracu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar Timerhry;
        private System.Windows.Forms.HScrollBar Timerbomb;
        private System.Windows.Forms.HScrollBar Timerblesku;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HScrollBar Timeritem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Soundbutton;
        private System.Windows.Forms.Timer timerVybuchBomb1;
        private System.Windows.Forms.Timer timerPohybHráč1;
        private System.Windows.Forms.Timer timerPohybHráč2;
        private System.Windows.Forms.Timer timerVybuchBomb2;
        private System.Windows.Forms.Timer timerPrisery;
    }
}

