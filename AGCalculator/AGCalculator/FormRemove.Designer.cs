namespace AGCalculator
{
    partial class FormRemove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRemove));
            this.buttonAddFromRemove = new System.Windows.Forms.Button();
            this.buttonMenuFromRemove = new System.Windows.Forms.Button();
            this.buttonRemoveObject = new System.Windows.Forms.Button();
            this.labelDescRemoveHeadline = new System.Windows.Forms.Label();
            this.comboBoxRemoveObject = new System.Windows.Forms.ComboBox();
            this.labelDimension = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAddFromRemove
            // 
            this.buttonAddFromRemove.Font = new System.Drawing.Font("Lucida Bright", 9F);
            this.buttonAddFromRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddFromRemove.Location = new System.Drawing.Point(419, 27);
            this.buttonAddFromRemove.Name = "buttonAddFromRemove";
            this.buttonAddFromRemove.Size = new System.Drawing.Size(106, 65);
            this.buttonAddFromRemove.TabIndex = 2;
            this.buttonAddFromRemove.Text = "Add objects (in 2D)";
            this.buttonAddFromRemove.UseVisualStyleBackColor = true;
            this.buttonAddFromRemove.Click += new System.EventHandler(this.buttonAddFromRemove_Click);
            // 
            // buttonMenuFromRemove
            // 
            this.buttonMenuFromRemove.Font = new System.Drawing.Font("Lucida Bright", 10.2F);
            this.buttonMenuFromRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonMenuFromRemove.Location = new System.Drawing.Point(419, 107);
            this.buttonMenuFromRemove.Name = "buttonMenuFromRemove";
            this.buttonMenuFromRemove.Size = new System.Drawing.Size(106, 65);
            this.buttonMenuFromRemove.TabIndex = 3;
            this.buttonMenuFromRemove.Text = "Menu";
            this.buttonMenuFromRemove.UseVisualStyleBackColor = true;
            this.buttonMenuFromRemove.Click += new System.EventHandler(this.buttonMenuFromRemove_Click);
            // 
            // buttonRemoveObject
            // 
            this.buttonRemoveObject.Font = new System.Drawing.Font("Lucida Bright", 12F);
            this.buttonRemoveObject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRemoveObject.Location = new System.Drawing.Point(90, 107);
            this.buttonRemoveObject.Name = "buttonRemoveObject";
            this.buttonRemoveObject.Size = new System.Drawing.Size(179, 77);
            this.buttonRemoveObject.TabIndex = 1;
            this.buttonRemoveObject.Text = "Remove";
            this.buttonRemoveObject.UseVisualStyleBackColor = true;
            this.buttonRemoveObject.Click += new System.EventHandler(this.buttonRemoveObject_Click);
            // 
            // labelDescRemoveHeadline
            // 
            this.labelDescRemoveHeadline.AutoSize = true;
            this.labelDescRemoveHeadline.Font = new System.Drawing.Font("Lucida Bright", 10.8F);
            this.labelDescRemoveHeadline.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDescRemoveHeadline.Location = new System.Drawing.Point(12, 27);
            this.labelDescRemoveHeadline.Name = "labelDescRemoveHeadline";
            this.labelDescRemoveHeadline.Size = new System.Drawing.Size(299, 22);
            this.labelDescRemoveHeadline.TabIndex = 17;
            this.labelDescRemoveHeadline.Text = "Select an object to be removed:";
            // 
            // comboBoxRemoveObject
            // 
            this.comboBoxRemoveObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRemoveObject.Font = new System.Drawing.Font("Lucida Bright", 12F);
            this.comboBoxRemoveObject.FormattingEnabled = true;
            this.comboBoxRemoveObject.Location = new System.Drawing.Point(50, 61);
            this.comboBoxRemoveObject.Name = "comboBoxRemoveObject";
            this.comboBoxRemoveObject.Size = new System.Drawing.Size(254, 31);
            this.comboBoxRemoveObject.TabIndex = 0;
            this.comboBoxRemoveObject.SelectedIndexChanged += new System.EventHandler(this.comboBoxRemoveObject_SelectedIndexChanged);
            // 
            // labelDimension
            // 
            this.labelDimension.AutoSize = true;
            this.labelDimension.Font = new System.Drawing.Font("Lucida Bright", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDimension.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDimension.Location = new System.Drawing.Point(26, 136);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(37, 22);
            this.labelDimension.TabIndex = 21;
            this.labelDimension.Text = "2D";
            // 
            // FormRemove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 211);
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.buttonAddFromRemove);
            this.Controls.Add(this.buttonMenuFromRemove);
            this.Controls.Add(this.buttonRemoveObject);
            this.Controls.Add(this.labelDescRemoveHeadline);
            this.Controls.Add(this.comboBoxRemoveObject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormRemove";
            this.Text = "Removing objects from memory (in 2D)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddFromRemove;
        private System.Windows.Forms.Button buttonMenuFromRemove;
        private System.Windows.Forms.Button buttonRemoveObject;
        private System.Windows.Forms.Label labelDescRemoveHeadline;
        private System.Windows.Forms.ComboBox comboBoxRemoveObject;
        private System.Windows.Forms.Label labelDimension;
    }
}