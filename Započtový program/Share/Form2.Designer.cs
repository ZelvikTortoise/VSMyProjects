namespace Share
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.timerSnake = new System.Windows.Forms.Timer(this.components);
            this.buttonKonec = new System.Windows.Forms.Button();
            this.timerGameTime = new System.Windows.Forms.Timer(this.components);
            this.labelSkórePopis = new System.Windows.Forms.Label();
            this.labelSkóre = new System.Windows.Forms.Label();
            this.labelGameTimePopis = new System.Windows.Forms.Label();
            this.labelGameTime = new System.Windows.Forms.Label();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.timerDuration = new System.Windows.Forms.Timer(this.components);
            this.timerPowerSpawn = new System.Windows.Forms.Timer(this.components);
            this.timerPowerDespawn = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelPowerTakenPopis = new System.Windows.Forms.Label();
            this.timerSpeed = new System.Windows.Forms.Timer(this.components);
            this.labelDifficultyPopis = new System.Windows.Forms.Label();
            this.labelDifficulty = new System.Windows.Forms.Label();
            this.buttonDifficultyChange = new System.Windows.Forms.Button();
            this.pictureBoxPowerTaken = new System.Windows.Forms.PictureBox();
            this.panelSnake = new System.Windows.Forms.Panel();
            this.pictureBoxPower = new System.Windows.Forms.PictureBox();
            this.pictureBoxFood = new System.Windows.Forms.PictureBox();
            this.pictureBoxNyanCat = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPowerTaken)).BeginInit();
            this.panelSnake.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNyanCat)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonUp
            // 
            this.buttonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonUp.Location = new System.Drawing.Point(319, 652);
            this.buttonUp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(67, 62);
            this.buttonUp.TabIndex = 2;
            this.buttonUp.Text = "↑";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonDown.Location = new System.Drawing.Point(319, 721);
            this.buttonDown.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(67, 62);
            this.buttonDown.TabIndex = 4;
            this.buttonDown.Text = "↓";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonLeft.Location = new System.Drawing.Point(244, 721);
            this.buttonLeft.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(67, 62);
            this.buttonLeft.TabIndex = 3;
            this.buttonLeft.Text = "←";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonRight.Location = new System.Drawing.Point(393, 721);
            this.buttonRight.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(67, 62);
            this.buttonRight.TabIndex = 5;
            this.buttonRight.Text = "→";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // timerSnake
            // 
            this.timerSnake.Interval = 400;
            this.timerSnake.Tick += new System.EventHandler(this.timerSnake_Tick);
            // 
            // buttonKonec
            // 
            this.buttonKonec.Location = new System.Drawing.Point(597, 769);
            this.buttonKonec.Margin = new System.Windows.Forms.Padding(4);
            this.buttonKonec.Name = "buttonKonec";
            this.buttonKonec.Size = new System.Drawing.Size(91, 42);
            this.buttonKonec.TabIndex = 1;
            this.buttonKonec.Text = "Konec";
            this.buttonKonec.UseVisualStyleBackColor = true;
            this.buttonKonec.Click += new System.EventHandler(this.buttonKonec_Click);
            // 
            // timerGameTime
            // 
            this.timerGameTime.Interval = 1000;
            this.timerGameTime.Tick += new System.EventHandler(this.timerGameTime_Tick);
            // 
            // labelSkórePopis
            // 
            this.labelSkórePopis.AutoSize = true;
            this.labelSkórePopis.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkórePopis.Location = new System.Drawing.Point(3, 779);
            this.labelSkórePopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSkórePopis.Name = "labelSkórePopis";
            this.labelSkórePopis.Size = new System.Drawing.Size(109, 38);
            this.labelSkórePopis.TabIndex = 6;
            this.labelSkórePopis.Text = "Skóre: ";
            // 
            // labelSkóre
            // 
            this.labelSkóre.AutoSize = true;
            this.labelSkóre.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkóre.Location = new System.Drawing.Point(97, 779);
            this.labelSkóre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSkóre.Name = "labelSkóre";
            this.labelSkóre.Size = new System.Drawing.Size(33, 38);
            this.labelSkóre.TabIndex = 7;
            this.labelSkóre.Text = "0";
            // 
            // labelGameTimePopis
            // 
            this.labelGameTimePopis.AutoSize = true;
            this.labelGameTimePopis.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameTimePopis.Location = new System.Drawing.Point(4, 652);
            this.labelGameTimePopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGameTimePopis.Name = "labelGameTimePopis";
            this.labelGameTimePopis.Size = new System.Drawing.Size(130, 28);
            this.labelGameTimePopis.TabIndex = 8;
            this.labelGameTimePopis.Text = "Čas ve hře:";
            // 
            // labelGameTime
            // 
            this.labelGameTime.AutoSize = true;
            this.labelGameTime.Font = new System.Drawing.Font("Papyrus", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameTime.Location = new System.Drawing.Point(137, 647);
            this.labelGameTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGameTime.Name = "labelGameTime";
            this.labelGameTime.Size = new System.Drawing.Size(84, 42);
            this.labelGameTime.TabIndex = 9;
            this.labelGameTime.Text = "00:00";
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 250;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // timerDuration
            // 
            this.timerDuration.Interval = 1000;
            this.timerDuration.Tick += new System.EventHandler(this.timerDuration_Tick);
            // 
            // timerPowerSpawn
            // 
            this.timerPowerSpawn.Interval = 1000;
            this.timerPowerSpawn.Tick += new System.EventHandler(this.timerPowerSpawn_Tick);
            // 
            // timerPowerDespawn
            // 
            this.timerPowerDespawn.Interval = 1000;
            this.timerPowerDespawn.Tick += new System.EventHandler(this.timerPowerDespawn_Tick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(495, 729);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(188, 37);
            this.progressBar.TabIndex = 10;
            this.progressBar.Value = 100;
            this.progressBar.Visible = false;
            // 
            // labelPowerTakenPopis
            // 
            this.labelPowerTakenPopis.AutoSize = true;
            this.labelPowerTakenPopis.Font = new System.Drawing.Font("Century Schoolbook", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPowerTakenPopis.Location = new System.Drawing.Point(508, 700);
            this.labelPowerTakenPopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPowerTakenPopis.Name = "labelPowerTakenPopis";
            this.labelPowerTakenPopis.Size = new System.Drawing.Size(150, 23);
            this.labelPowerTakenPopis.TabIndex = 12;
            this.labelPowerTakenPopis.Text = "Otevřené kraje";
            this.labelPowerTakenPopis.Visible = false;
            // 
            // timerSpeed
            // 
            this.timerSpeed.Enabled = true;
            this.timerSpeed.Interval = 1000;
            this.timerSpeed.Tick += new System.EventHandler(this.timerSpeed_Tick);
            // 
            // labelDifficultyPopis
            // 
            this.labelDifficultyPopis.AutoSize = true;
            this.labelDifficultyPopis.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDifficultyPopis.Location = new System.Drawing.Point(11, 705);
            this.labelDifficultyPopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDifficultyPopis.Name = "labelDifficultyPopis";
            this.labelDifficultyPopis.Size = new System.Drawing.Size(128, 28);
            this.labelDifficultyPopis.TabIndex = 13;
            this.labelDifficultyPopis.Text = "Obtížnost: ";
            // 
            // labelDifficulty
            // 
            this.labelDifficulty.AutoSize = true;
            this.labelDifficulty.Font = new System.Drawing.Font("Poor Richard", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDifficulty.Location = new System.Drawing.Point(140, 705);
            this.labelDifficulty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDifficulty.Name = "labelDifficulty";
            this.labelDifficulty.Size = new System.Drawing.Size(88, 31);
            this.labelDifficulty.TabIndex = 14;
            this.labelDifficulty.Text = "Střední";
            // 
            // buttonDifficultyChange
            // 
            this.buttonDifficultyChange.Location = new System.Drawing.Point(60, 735);
            this.buttonDifficultyChange.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDifficultyChange.Name = "buttonDifficultyChange";
            this.buttonDifficultyChange.Size = new System.Drawing.Size(136, 38);
            this.buttonDifficultyChange.TabIndex = 15;
            this.buttonDifficultyChange.Text = "Změnit obtížnost";
            this.buttonDifficultyChange.UseVisualStyleBackColor = true;
            this.buttonDifficultyChange.Click += new System.EventHandler(this.buttonDifficultyChange_Click);
            // 
            // pictureBoxPowerTaken
            // 
            this.pictureBoxPowerTaken.Image = global::Share.Properties.Resources.Zlatá_hvězda;
            this.pictureBoxPowerTaken.Location = new System.Drawing.Point(561, 647);
            this.pictureBoxPowerTaken.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxPowerTaken.Name = "pictureBoxPowerTaken";
            this.pictureBoxPowerTaken.Size = new System.Drawing.Size(53, 49);
            this.pictureBoxPowerTaken.TabIndex = 11;
            this.pictureBoxPowerTaken.TabStop = false;
            this.pictureBoxPowerTaken.Visible = false;
            // 
            // panelSnake
            // 
            this.panelSnake.BackColor = System.Drawing.Color.White;
            this.panelSnake.BackgroundImage = global::Share.Properties.Resources.Background;
            this.panelSnake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSnake.Controls.Add(this.pictureBoxPower);
            this.panelSnake.Controls.Add(this.pictureBoxFood);
            this.panelSnake.Controls.Add(this.pictureBoxNyanCat);
            this.panelSnake.Location = new System.Drawing.Point(16, 15);
            this.panelSnake.Margin = new System.Windows.Forms.Padding(4);
            this.panelSnake.Name = "panelSnake";
            this.panelSnake.Size = new System.Drawing.Size(666, 615);
            this.panelSnake.TabIndex = 0;
            // 
            // pictureBoxPower
            // 
            this.pictureBoxPower.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPower.Image = global::Share.Properties.Resources.Stříbrná_hvězda;
            this.pictureBoxPower.Location = new System.Drawing.Point(43, 406);
            this.pictureBoxPower.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxPower.Name = "pictureBoxPower";
            this.pictureBoxPower.Size = new System.Drawing.Size(53, 49);
            this.pictureBoxPower.TabIndex = 2;
            this.pictureBoxPower.TabStop = false;
            this.pictureBoxPower.Visible = false;
            // 
            // pictureBoxFood
            // 
            this.pictureBoxFood.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxFood.Image = global::Share.Properties.Resources.Cookie;
            this.pictureBoxFood.Location = new System.Drawing.Point(532, 353);
            this.pictureBoxFood.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxFood.Name = "pictureBoxFood";
            this.pictureBoxFood.Size = new System.Drawing.Size(53, 49);
            this.pictureBoxFood.TabIndex = 1;
            this.pictureBoxFood.TabStop = false;
            this.pictureBoxFood.Visible = false;
            // 
            // pictureBoxNyanCat
            // 
            this.pictureBoxNyanCat.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxNyanCat.Image = global::Share.Properties.Resources.nyan_cat_doprava;
            this.pictureBoxNyanCat.Location = new System.Drawing.Point(297, 274);
            this.pictureBoxNyanCat.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxNyanCat.Name = "pictureBoxNyanCat";
            this.pictureBoxNyanCat.Size = new System.Drawing.Size(67, 62);
            this.pictureBoxNyanCat.TabIndex = 0;
            this.pictureBoxNyanCat.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 823);
            this.Controls.Add(this.buttonDifficultyChange);
            this.Controls.Add(this.labelDifficulty);
            this.Controls.Add(this.labelDifficultyPopis);
            this.Controls.Add(this.labelPowerTakenPopis);
            this.Controls.Add(this.pictureBoxPowerTaken);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelGameTime);
            this.Controls.Add(this.labelGameTimePopis);
            this.Controls.Add(this.labelSkóre);
            this.Controls.Add(this.labelSkórePopis);
            this.Controls.Add(this.buttonKonec);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonDown);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.panelSnake);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snyanke cat";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form2_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPowerTaken)).EndInit();
            this.panelSnake.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNyanCat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSnake;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Timer timerSnake;
        private System.Windows.Forms.Button buttonKonec;
        private System.Windows.Forms.PictureBox pictureBoxNyanCat;
        private System.Windows.Forms.Timer timerGameTime;
        private System.Windows.Forms.PictureBox pictureBoxFood;
        private System.Windows.Forms.PictureBox pictureBoxPower;
        private System.Windows.Forms.Label labelSkórePopis;
        private System.Windows.Forms.Label labelSkóre;
        private System.Windows.Forms.Label labelGameTimePopis;
        private System.Windows.Forms.Label labelGameTime;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Timer timerDuration;
        private System.Windows.Forms.Timer timerPowerSpawn;
        private System.Windows.Forms.Timer timerPowerDespawn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureBoxPowerTaken;
        private System.Windows.Forms.Label labelPowerTakenPopis;
        private System.Windows.Forms.Timer timerSpeed;
        private System.Windows.Forms.Label labelDifficultyPopis;
        private System.Windows.Forms.Label labelDifficulty;
        private System.Windows.Forms.Button buttonDifficultyChange;
    }
}