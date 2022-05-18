namespace Snake
{
    partial class FormSnake
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
            this.panelPlayground = new System.Windows.Forms.Panel();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.timerSnake = new System.Windows.Forms.Timer(this.components);
            this.labelTime = new System.Windows.Forms.Label();
            this.labelScoreDesc = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelPlayground
            // 
            this.panelPlayground.BackColor = System.Drawing.Color.Black;
            this.panelPlayground.Location = new System.Drawing.Point(12, 12);
            this.panelPlayground.Name = "panelPlayground";
            this.panelPlayground.Size = new System.Drawing.Size(600, 400);
            this.panelPlayground.TabIndex = 0;
            this.panelPlayground.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPlayground_Paint);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(292, 426);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(50, 50);
            this.buttonUp.TabIndex = 1;
            this.buttonUp.Text = "↑";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Location = new System.Drawing.Point(236, 482);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(50, 50);
            this.buttonLeft.TabIndex = 2;
            this.buttonLeft.Text = "←";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(292, 482);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(50, 50);
            this.buttonDown.TabIndex = 3;
            this.buttonDown.Text = "↓";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(348, 482);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(50, 50);
            this.buttonRight.TabIndex = 4;
            this.buttonRight.Text = "→";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // timerSnake
            // 
            this.timerSnake.Interval = 1000;
            this.timerSnake.Tick += new System.EventHandler(this.timerSnake_Tick);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Papyrus", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(527, 503);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(90, 44);
            this.labelTime.TabIndex = 5;
            this.labelTime.Text = "00:00";
            // 
            // labelScoreDesc
            // 
            this.labelScoreDesc.AutoSize = true;
            this.labelScoreDesc.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScoreDesc.Location = new System.Drawing.Point(6, 507);
            this.labelScoreDesc.Name = "labelScoreDesc";
            this.labelScoreDesc.Size = new System.Drawing.Size(88, 33);
            this.labelScoreDesc.TabIndex = 6;
            this.labelScoreDesc.Text = "Score:";
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Comic Sans MS", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScore.Location = new System.Drawing.Point(100, 507);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(105, 33);
            this.labelScore.TabIndex = 7;
            this.labelScore.Text = "000000";
            // 
            // FormSnake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 546);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelScoreDesc);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.buttonDown);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.panelPlayground);
            this.Name = "FormSnake";
            this.Text = "Snake";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelPlayground;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Timer timerSnake;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelScoreDesc;
        private System.Windows.Forms.Label labelScore;
    }
}

