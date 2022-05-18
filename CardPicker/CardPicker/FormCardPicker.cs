using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardPicker
{
    public partial class FormCardPicker : Form
    {
        public FormCardPicker()
        {
            InitializeComponent();

            Start();
        }

        int Drawn { get; set; } = 0;
        const string noCard = "<NO CARD>";
        Card selectedCard = null;

        private void GoToFormCardOverview(FormCardOverview form)
        {
            if (!this.Visible)
                return;

            this.Visible = false;
            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { Application.Exit(); };
            form.Visible = true;
            form.CallMeWhenAccessingThisForm();
        }

        private void Start()
        {
            pictureBoxCard.Image = null;
            labelCard.Text = noCard;
            labelDrawn.Text = "0";
            if (Program.GetAllCardCount() == 0)
            {
                labelDrawnAll.Visible = true;
                buttonDraw.Enabled = false;
            }
            else
            {
                labelDrawnAll.Visible = false;
                buttonDraw.Enabled = true;
            }

            buttonDraw.Text = "DRAW";
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to exit the application?", "End already?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            if (Drawn == Program.GetAllCardCount() - 1)
            {
                buttonDraw.Enabled = false;
                labelDrawnAll.Visible = true;
            }
            else if (Drawn == 0)
            {
                buttonDraw.Text = "DRAW NEXT";
            }

            selectedCard = Program.GetCard();       // Check + create temp Card so no null reference?
            pictureBoxCard.Image = selectedCard.Image;
            labelCard.Text = selectedCard.Text;

            Drawn++;
            labelDrawn.Text = Drawn.ToString();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Drawn = 0;
            labelDrawn.Text = Drawn.ToString();
            pictureBoxCard.Image = null;
            labelCard.Text = noCard;
            selectedCard = null;
            buttonDraw.Text = "DRAW";

            Program.Reset();

            if (Program.GetAllCardCount() != 0)
            {
                buttonDraw.Enabled = true;
                labelDrawnAll.Visible = false;
            }
            else
            {
                buttonDraw.Enabled = false;
                labelDrawnAll.Visible = true;
            }
            
        }

        private void buttonOverview_Click(object sender, EventArgs e)
        {
            GoToFormCardOverview(Program.formCardOverview);
        }
    }
}
