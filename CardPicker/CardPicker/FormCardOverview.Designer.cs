namespace CardPicker
{
    partial class FormCardOverview
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
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelCard = new System.Windows.Forms.Label();
            this.pictureBoxCard = new System.Windows.Forms.PictureBox();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonRemoveAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCard)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(426, 125);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(94, 71);
            this.buttonNext.TabIndex = 0;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(185, 380);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(185, 50);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.Text = "Return to picking";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // labelCard
            // 
            this.labelCard.AutoSize = true;
            this.labelCard.Location = new System.Drawing.Point(190, 330);
            this.labelCard.MinimumSize = new System.Drawing.Size(180, 0);
            this.labelCard.Name = "labelCard";
            this.labelCard.Size = new System.Drawing.Size(180, 17);
            this.labelCard.TabIndex = 3;
            this.labelCard.Text = "<NO CARD>";
            this.labelCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxCard
            // 
            this.pictureBoxCard.Location = new System.Drawing.Point(190, 23);
            this.pictureBoxCard.Name = "pictureBoxCard";
            this.pictureBoxCard.Size = new System.Drawing.Size(180, 274);
            this.pictureBoxCard.TabIndex = 2;
            this.pictureBoxCard.TabStop = false;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(37, 125);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(94, 71);
            this.buttonPrevious.TabIndex = 4;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(37, 359);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(94, 71);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "Add new card";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // buttonRemoveAll
            // 
            this.buttonRemoveAll.Location = new System.Drawing.Point(426, 359);
            this.buttonRemoveAll.Name = "buttonRemoveAll";
            this.buttonRemoveAll.Size = new System.Drawing.Size(94, 71);
            this.buttonRemoveAll.TabIndex = 6;
            this.buttonRemoveAll.Text = "Remove all cards";
            this.buttonRemoveAll.UseVisualStyleBackColor = true;
            this.buttonRemoveAll.Visible = false;
            this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
            // 
            // FormCardOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 460);
            this.Controls.Add(this.buttonRemoveAll);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.labelCard);
            this.Controls.Add(this.pictureBoxCard);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonNext);
            this.Name = "FormCardOverview";
            this.Text = "Card overview";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelCard;
        private System.Windows.Forms.PictureBox pictureBoxCard;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonRemoveAll;
    }
}