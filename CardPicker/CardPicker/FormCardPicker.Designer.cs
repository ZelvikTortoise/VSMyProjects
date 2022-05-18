namespace CardPicker
{
    partial class FormCardPicker
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
            this.pictureBoxCard = new System.Windows.Forms.PictureBox();
            this.labelCard = new System.Windows.Forms.Label();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonOverview = new System.Windows.Forms.Button();
            this.labelDrawnDesc = new System.Windows.Forms.Label();
            this.labelDrawn = new System.Windows.Forms.Label();
            this.labelDrawnAll = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCard)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCard
            // 
            this.pictureBoxCard.Location = new System.Drawing.Point(212, 33);
            this.pictureBoxCard.Name = "pictureBoxCard";
            this.pictureBoxCard.Size = new System.Drawing.Size(180, 274);
            this.pictureBoxCard.TabIndex = 0;
            this.pictureBoxCard.TabStop = false;
            // 
            // labelCard
            // 
            this.labelCard.AutoSize = true;
            this.labelCard.Location = new System.Drawing.Point(212, 325);
            this.labelCard.MinimumSize = new System.Drawing.Size(180, 0);
            this.labelCard.Name = "labelCard";
            this.labelCard.Size = new System.Drawing.Size(180, 17);
            this.labelCard.TabIndex = 1;
            this.labelCard.Text = "<NO CARD>";
            this.labelCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(239, 384);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(127, 44);
            this.buttonDraw.TabIndex = 2;
            this.buttonDraw.Text = "DRAW";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(34, 384);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(127, 44);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "RESET";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Location = new System.Drawing.Point(444, 384);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(127, 44);
            this.buttonEnd.TabIndex = 4;
            this.buttonEnd.Text = "END";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonOverview
            // 
            this.buttonOverview.Location = new System.Drawing.Point(444, 110);
            this.buttonOverview.Name = "buttonOverview";
            this.buttonOverview.Size = new System.Drawing.Size(127, 120);
            this.buttonOverview.TabIndex = 5;
            this.buttonOverview.Text = "Card overview";
            this.buttonOverview.UseVisualStyleBackColor = true;
            this.buttonOverview.Click += new System.EventHandler(this.buttonOverview_Click);
            // 
            // labelDrawnDesc
            // 
            this.labelDrawnDesc.AutoSize = true;
            this.labelDrawnDesc.Location = new System.Drawing.Point(51, 110);
            this.labelDrawnDesc.Name = "labelDrawnDesc";
            this.labelDrawnDesc.Size = new System.Drawing.Size(91, 17);
            this.labelDrawnDesc.TabIndex = 6;
            this.labelDrawnDesc.Text = "Cards drawn:";
            this.labelDrawnDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDrawn
            // 
            this.labelDrawn.AutoSize = true;
            this.labelDrawn.Location = new System.Drawing.Point(84, 162);
            this.labelDrawn.MinimumSize = new System.Drawing.Size(24, 0);
            this.labelDrawn.Name = "labelDrawn";
            this.labelDrawn.Size = new System.Drawing.Size(24, 17);
            this.labelDrawn.TabIndex = 7;
            this.labelDrawn.Text = "0";
            this.labelDrawn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDrawnAll
            // 
            this.labelDrawnAll.AutoSize = true;
            this.labelDrawnAll.Location = new System.Drawing.Point(51, 222);
            this.labelDrawnAll.MinimumSize = new System.Drawing.Size(24, 0);
            this.labelDrawnAll.Name = "labelDrawnAll";
            this.labelDrawnAll.Size = new System.Drawing.Size(97, 85);
            this.labelDrawnAll.TabIndex = 8;
            this.labelDrawnAll.Text = "All cards have\r\nbeen drawn.\r\n\r\nPress RESET\r\nto draw more.";
            this.labelDrawnAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelDrawnAll.Visible = false;
            // 
            // FormCardPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 471);
            this.Controls.Add(this.labelDrawnAll);
            this.Controls.Add(this.labelDrawn);
            this.Controls.Add(this.labelDrawnDesc);
            this.Controls.Add(this.buttonOverview);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonDraw);
            this.Controls.Add(this.labelCard);
            this.Controls.Add(this.pictureBoxCard);
            this.Name = "FormCardPicker";
            this.Text = "Card picker";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCard;
        private System.Windows.Forms.Label labelCard;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonOverview;
        private System.Windows.Forms.Label labelDrawnDesc;
        private System.Windows.Forms.Label labelDrawn;
        private System.Windows.Forms.Label labelDrawnAll;
    }
}

