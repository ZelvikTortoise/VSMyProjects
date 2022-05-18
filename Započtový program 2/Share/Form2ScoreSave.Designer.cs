namespace Share
{
    partial class Form2ScoreSave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2ScoreSave));
            this.labelPopis = new System.Windows.Forms.Label();
            this.labelNadpis = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.labelScorePopis = new System.Windows.Forms.Label();
            this.textBoxScore = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelNamePopis = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelPopis
            // 
            this.labelPopis.AutoSize = true;
            this.labelPopis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPopis.Location = new System.Drawing.Point(36, 223);
            this.labelPopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPopis.Name = "labelPopis";
            this.labelPopis.Size = new System.Drawing.Size(188, 25);
            this.labelPopis.TabIndex = 9;
            this.labelPopis.Text = "Zaznamenejte si ho:";
            // 
            // labelNadpis
            // 
            this.labelNadpis.AutoSize = true;
            this.labelNadpis.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNadpis.Location = new System.Drawing.Point(44, 23);
            this.labelNadpis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNadpis.Name = "labelNadpis";
            this.labelNadpis.Size = new System.Drawing.Size(366, 42);
            this.labelNadpis.TabIndex = 6;
            this.labelNadpis.Text = "SCORE SUBMITING";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxPath.Location = new System.Drawing.Point(41, 251);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(388, 29);
            this.textBoxPath.TabIndex = 3;
            this.textBoxPath.Text = "Snyanke cat scores.txt";
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSave.Location = new System.Drawing.Point(168, 299);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(104, 50);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Uložit";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Location = new System.Drawing.Point(347, 306);
            this.buttonEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(100, 43);
            this.buttonEnd.TabIndex = 5;
            this.buttonEnd.Text = "Konec";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // labelScorePopis
            // 
            this.labelScorePopis.AutoSize = true;
            this.labelScorePopis.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScorePopis.Location = new System.Drawing.Point(36, 161);
            this.labelScorePopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelScorePopis.Name = "labelScorePopis";
            this.labelScorePopis.Size = new System.Drawing.Size(139, 29);
            this.labelScorePopis.TabIndex = 8;
            this.labelScorePopis.Text = "Vaše skóre:";
            // 
            // textBoxScore
            // 
            this.textBoxScore.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxScore.ForeColor = System.Drawing.Color.Red;
            this.textBoxScore.Location = new System.Drawing.Point(189, 156);
            this.textBoxScore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxScore.Name = "textBoxScore";
            this.textBoxScore.ReadOnly = true;
            this.textBoxScore.Size = new System.Drawing.Size(240, 41);
            this.textBoxScore.TabIndex = 10;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.OverwritePrompt = false;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(321, 223);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(109, 28);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "Procházet...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelNamePopis
            // 
            this.labelNamePopis.AutoSize = true;
            this.labelNamePopis.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNamePopis.Location = new System.Drawing.Point(36, 105);
            this.labelNamePopis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNamePopis.Name = "labelNamePopis";
            this.labelNamePopis.Size = new System.Drawing.Size(146, 29);
            this.labelNamePopis.TabIndex = 7;
            this.labelNamePopis.Text = "Vaše jméno:";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxName.Location = new System.Drawing.Point(189, 105);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxName.MaxLength = 15;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(240, 34);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.Text = "Hráč1";
            // 
            // Form2ScoreSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 364);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelNamePopis);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxScore);
            this.Controls.Add(this.labelScorePopis);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.labelNadpis);
            this.Controls.Add(this.labelPopis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form2ScoreSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snyanke cat";
            this.Shown += new System.EventHandler(this.Form2ScoreSave_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPopis;
        private System.Windows.Forms.Label labelNadpis;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Label labelScorePopis;
        private System.Windows.Forms.TextBox textBoxScore;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelNamePopis;
        private System.Windows.Forms.TextBox textBoxName;
    }
}