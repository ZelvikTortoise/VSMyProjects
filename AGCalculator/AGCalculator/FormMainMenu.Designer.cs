namespace AGCalculator
{
    partial class FormMainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainMenu));
            this.labelDescMainMenu = new System.Windows.Forms.Label();
            this.buttonGoToDisplay2D = new System.Windows.Forms.Button();
            this.labelDesc2D = new System.Windows.Forms.Label();
            this.buttonGoToAdd2D = new System.Windows.Forms.Button();
            this.buttonGoToAdd3D = new System.Windows.Forms.Button();
            this.labelDesc3D = new System.Windows.Forms.Label();
            this.buttonGoToDisplay3D = new System.Windows.Forms.Button();
            this.buttonGoToChangeLanguage = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonGoToRemove2D = new System.Windows.Forms.Button();
            this.buttonGoToRemove3D = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDescMainMenu
            // 
            this.labelDescMainMenu.AutoSize = true;
            this.labelDescMainMenu.Font = new System.Drawing.Font("Lucida Sans Unicode", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDescMainMenu.Location = new System.Drawing.Point(127, 29);
            this.labelDescMainMenu.Margin = new System.Windows.Forms.Padding(5, 20, 5, 20);
            this.labelDescMainMenu.Name = "labelDescMainMenu";
            this.labelDescMainMenu.Size = new System.Drawing.Size(255, 53);
            this.labelDescMainMenu.TabIndex = 0;
            this.labelDescMainMenu.Text = "Main menu";
            this.labelDescMainMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGoToDisplay2D
            // 
            this.buttonGoToDisplay2D.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToDisplay2D.Location = new System.Drawing.Point(183, 134);
            this.buttonGoToDisplay2D.Name = "buttonGoToDisplay2D";
            this.buttonGoToDisplay2D.Size = new System.Drawing.Size(146, 64);
            this.buttonGoToDisplay2D.TabIndex = 1;
            this.buttonGoToDisplay2D.Text = "Display";
            this.buttonGoToDisplay2D.UseVisualStyleBackColor = true;
            this.buttonGoToDisplay2D.Click += new System.EventHandler(this.buttonGoToDisplay2D_Click);
            // 
            // labelDesc2D
            // 
            this.labelDesc2D.AutoSize = true;
            this.labelDesc2D.Font = new System.Drawing.Font("Lucida Sans Unicode", 13.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDesc2D.Location = new System.Drawing.Point(12, 103);
            this.labelDesc2D.Name = "labelDesc2D";
            this.labelDesc2D.Size = new System.Drawing.Size(310, 28);
            this.labelDesc2D.TabIndex = 2;
            this.labelDesc2D.Text = "Objects in a plain (in 2D):";
            // 
            // buttonGoToAdd2D
            // 
            this.buttonGoToAdd2D.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToAdd2D.Location = new System.Drawing.Point(33, 134);
            this.buttonGoToAdd2D.Name = "buttonGoToAdd2D";
            this.buttonGoToAdd2D.Size = new System.Drawing.Size(146, 64);
            this.buttonGoToAdd2D.TabIndex = 0;
            this.buttonGoToAdd2D.Text = "Add";
            this.buttonGoToAdd2D.UseVisualStyleBackColor = true;
            this.buttonGoToAdd2D.Click += new System.EventHandler(this.buttonGoToAdd2D_Click);
            // 
            // buttonGoToAdd3D
            // 
            this.buttonGoToAdd3D.Enabled = false;
            this.buttonGoToAdd3D.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToAdd3D.Location = new System.Drawing.Point(33, 248);
            this.buttonGoToAdd3D.Name = "buttonGoToAdd3D";
            this.buttonGoToAdd3D.Size = new System.Drawing.Size(146, 64);
            this.buttonGoToAdd3D.TabIndex = 3;
            this.buttonGoToAdd3D.Text = "Add";
            this.buttonGoToAdd3D.UseVisualStyleBackColor = true;
            this.buttonGoToAdd3D.Click += new System.EventHandler(this.buttonGoToAdd3D_Click);
            // 
            // labelDesc3D
            // 
            this.labelDesc3D.AutoSize = true;
            this.labelDesc3D.Font = new System.Drawing.Font("Lucida Sans Unicode", 13.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDesc3D.Location = new System.Drawing.Point(12, 217);
            this.labelDesc3D.Name = "labelDesc3D";
            this.labelDesc3D.Size = new System.Drawing.Size(318, 28);
            this.labelDesc3D.TabIndex = 5;
            this.labelDesc3D.Text = "Objects in a space (in 3D):";
            // 
            // buttonGoToDisplay3D
            // 
            this.buttonGoToDisplay3D.Enabled = false;
            this.buttonGoToDisplay3D.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToDisplay3D.Location = new System.Drawing.Point(183, 248);
            this.buttonGoToDisplay3D.Name = "buttonGoToDisplay3D";
            this.buttonGoToDisplay3D.Size = new System.Drawing.Size(146, 64);
            this.buttonGoToDisplay3D.TabIndex = 4;
            this.buttonGoToDisplay3D.Text = "Display";
            this.buttonGoToDisplay3D.UseVisualStyleBackColor = true;
            this.buttonGoToDisplay3D.Click += new System.EventHandler(this.buttonGoToDisplay3D_Click);
            // 
            // buttonGoToChangeLanguage
            // 
            this.buttonGoToChangeLanguage.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToChangeLanguage.Location = new System.Drawing.Point(34, 335);
            this.buttonGoToChangeLanguage.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.buttonGoToChangeLanguage.Name = "buttonGoToChangeLanguage";
            this.buttonGoToChangeLanguage.Size = new System.Drawing.Size(447, 64);
            this.buttonGoToChangeLanguage.TabIndex = 6;
            this.buttonGoToChangeLanguage.Text = "Change language";
            this.buttonGoToChangeLanguage.UseVisualStyleBackColor = true;
            this.buttonGoToChangeLanguage.Click += new System.EventHandler(this.buttonGoToChangeLanguage_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExit.Location = new System.Drawing.Point(34, 407);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(447, 64);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonGoToRemove2D
            // 
            this.buttonGoToRemove2D.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToRemove2D.Location = new System.Drawing.Point(335, 134);
            this.buttonGoToRemove2D.Name = "buttonGoToRemove2D";
            this.buttonGoToRemove2D.Size = new System.Drawing.Size(146, 64);
            this.buttonGoToRemove2D.TabIndex = 2;
            this.buttonGoToRemove2D.Text = "Remove";
            this.buttonGoToRemove2D.UseVisualStyleBackColor = true;
            this.buttonGoToRemove2D.Click += new System.EventHandler(this.buttonGoToRemove2D_Click);
            // 
            // buttonGoToRemove3D
            // 
            this.buttonGoToRemove3D.Enabled = false;
            this.buttonGoToRemove3D.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonGoToRemove3D.Location = new System.Drawing.Point(335, 248);
            this.buttonGoToRemove3D.Name = "buttonGoToRemove3D";
            this.buttonGoToRemove3D.Size = new System.Drawing.Size(146, 64);
            this.buttonGoToRemove3D.TabIndex = 5;
            this.buttonGoToRemove3D.Text = "Remove";
            this.buttonGoToRemove3D.UseVisualStyleBackColor = true;
            this.buttonGoToRemove3D.Click += new System.EventHandler(this.buttonGoToRemove3D_Click);
            // 
            // FormMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 500);
            this.Controls.Add(this.buttonGoToRemove3D);
            this.Controls.Add(this.buttonGoToRemove2D);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonGoToChangeLanguage);
            this.Controls.Add(this.buttonGoToAdd3D);
            this.Controls.Add(this.labelDesc3D);
            this.Controls.Add(this.buttonGoToDisplay3D);
            this.Controls.Add(this.buttonGoToAdd2D);
            this.Controls.Add(this.labelDesc2D);
            this.Controls.Add(this.buttonGoToDisplay2D);
            this.Controls.Add(this.labelDescMainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main menu";
            this.Load += new System.EventHandler(this.FormMainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDescMainMenu;
        private System.Windows.Forms.Button buttonGoToDisplay2D;
        private System.Windows.Forms.Label labelDesc2D;
        private System.Windows.Forms.Button buttonGoToAdd2D;
        private System.Windows.Forms.Button buttonGoToAdd3D;
        private System.Windows.Forms.Label labelDesc3D;
        private System.Windows.Forms.Button buttonGoToDisplay3D;
        private System.Windows.Forms.Button buttonGoToChangeLanguage;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonGoToRemove2D;
        private System.Windows.Forms.Button buttonGoToRemove3D;
    }
}