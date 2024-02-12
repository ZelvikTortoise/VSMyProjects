namespace Otázky
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
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblQuestionsRemaining = new System.Windows.Forms.Label();
            this.lblChapterTitle = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnGenerateQuestion = new System.Windows.Forms.Button();
            this.btnChangeChapter = new System.Windows.Forms.Button();
            this.btnSelectStudent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFile.Location = new System.Drawing.Point(32, 23);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(175, 67);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Vybrat soubor";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(223, 23);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedFile.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblQuestionsRemaining
            // 
            this.lblQuestionsRemaining.AutoSize = true;
            this.lblQuestionsRemaining.Font = new System.Drawing.Font("Arial Black", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionsRemaining.Location = new System.Drawing.Point(550, 120);
            this.lblQuestionsRemaining.Name = "lblQuestionsRemaining";
            this.lblQuestionsRemaining.Size = new System.Drawing.Size(164, 45);
            this.lblQuestionsRemaining.TabIndex = 2;
            this.lblQuestionsRemaining.Text = "Otázek: ";
            this.lblQuestionsRemaining.Visible = false;
            // 
            // lblChapterTitle
            // 
            this.lblChapterTitle.AutoSize = true;
            this.lblChapterTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblChapterTitle.Font = new System.Drawing.Font("Arial Black", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChapterTitle.Location = new System.Drawing.Point(25, 120);
            this.lblChapterTitle.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblChapterTitle.Name = "lblChapterTitle";
            this.lblChapterTitle.Size = new System.Drawing.Size(164, 45);
            this.lblChapterTitle.TabIndex = 3;
            this.lblChapterTitle.Text = "Otázek: ";
            this.lblChapterTitle.Visible = false;
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(30, 200);
            this.lblQuestion.MaximumSize = new System.Drawing.Size(800, 0);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(0, 26);
            this.lblQuestion.TabIndex = 4;
            this.lblQuestion.Visible = false;
            // 
            // btnGenerateQuestion
            // 
            this.btnGenerateQuestion.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateQuestion.Location = new System.Drawing.Point(250, 80);
            this.btnGenerateQuestion.Name = "btnGenerateQuestion";
            this.btnGenerateQuestion.Size = new System.Drawing.Size(280, 100);
            this.btnGenerateQuestion.TabIndex = 5;
            this.btnGenerateQuestion.Text = "Generovat otázku";
            this.btnGenerateQuestion.UseVisualStyleBackColor = true;
            this.btnGenerateQuestion.Visible = false;
            this.btnGenerateQuestion.Click += new System.EventHandler(this.btnGenerateQuestion_Click);
            // 
            // btnChangeChapter
            // 
            this.btnChangeChapter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeChapter.Location = new System.Drawing.Point(600, 23);
            this.btnChangeChapter.Name = "btnChangeChapter";
            this.btnChangeChapter.Size = new System.Drawing.Size(180, 67);
            this.btnChangeChapter.TabIndex = 6;
            this.btnChangeChapter.Text = "Změnit kapitolu";
            this.btnChangeChapter.UseVisualStyleBackColor = true;
            this.btnChangeChapter.Visible = false;
            this.btnChangeChapter.Click += new System.EventHandler(this.btnChangeChapter_Click);
            // 
            // btnSelectStudent
            // 
            this.btnSelectStudent.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSelectStudent.Location = new System.Drawing.Point(312, 198);
            this.btnSelectStudent.Name = "btnSelectStudent";
            this.btnSelectStudent.Size = new System.Drawing.Size(162, 51);
            this.btnSelectStudent.TabIndex = 7;
            this.btnSelectStudent.Text = "Vyvolat studenta";
            this.btnSelectStudent.UseVisualStyleBackColor = true;
            this.btnSelectStudent.Visible = false;
            this.btnSelectStudent.Click += new System.EventHandler(this.btnSelectStudent_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 261);
            this.Controls.Add(this.btnSelectStudent);
            this.Controls.Add(this.btnChangeChapter);
            this.Controls.Add(this.btnGenerateQuestion);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblChapterTitle);
            this.Controls.Add(this.lblQuestionsRemaining);
            this.Controls.Add(this.lblSelectedFile);
            this.Controls.Add(this.btnSelectFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Losování otázek";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblQuestionsRemaining;
        private System.Windows.Forms.Label lblChapterTitle;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button btnGenerateQuestion;
        private System.Windows.Forms.Button btnChangeChapter;
        private System.Windows.Forms.Button btnSelectStudent;
    }
}

