using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CardPicker
{
    public partial class FormCardOverview : Form
    {
        public FormCardOverview()
        {
            InitializeComponent();

            Start();
        }

        int PickedIndex { get; set; } = 0;

        public void CallMeWhenAccessingThisForm()
        {
            if (buttonNext.Enabled == false && PickedIndex != Program.GetAllCardCount() - 1)
            {
                buttonNext.Enabled = true;
            }
        }

        private void Start()
        {
            if (Program.GetAllCardCount() == 0)
            {
                pictureBoxCard.Image = null;
                labelCard.Text = "<NO CARD>";
                buttonPrevious.Enabled = false;
                buttonNext.Enabled = false;
            }
            else
            {
                Card card = Program.GetCardOverview(0);
                pictureBoxCard.Image = card.Image;
                labelCard.Text = card.Text;
                buttonPrevious.Enabled = false;
                if (Program.GetAllCardCount() == 1)
                {
                    buttonNext.Enabled = false;
                }
                else
                {
                    buttonNext.Enabled = true;

                }
            }
        }

        private void GoToFormCardPicker(FormCardPicker form)
        {
            if (!this.Visible)
                return;

            this.Visible = false;
            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { Application.Exit(); };
            form.Visible = true;
        }

        private void ShowCurrentCard()
        {
            Card card = Program.GetCardOverview(PickedIndex);

            pictureBoxCard.Image = card.Image;
            labelCard.Text = card.Text;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            PickedIndex++;

            ShowCurrentCard();

            if (PickedIndex == 1)
            {
                buttonPrevious.Enabled = true;
            }

            if (PickedIndex == Program.GetAllCardCount() - 1)
            {
                buttonNext.Enabled = false;
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            PickedIndex--;

            ShowCurrentCard();

            if (PickedIndex != Program.GetAllCardCount() - 1)
            {
                buttonNext.Enabled = true;
            }

            if (PickedIndex == 0)
            {
                buttonPrevious.Enabled = false;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            GoToFormCardPicker(Program.formCardPicker);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string picturePath = openFileDialog.FileName;

                if (!picturePath.EndsWith(".png") && !picturePath.EndsWith(".jpg"))
                {
                    MessageBox.Show("Select an image of the card called according to the text you want to use.\nNote that only .png and .jpg formats are allowed.\nOperation unsuccessful.", "Not like this", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] split = picturePath.Split('\\');

                try
                {
                    File.Copy(picturePath, string.Concat(Program.path, "\\", split[split.Length - 1]), false);
                    MessageBox.Show("The card was successfully added. Reset the cards to see the changes.");
                    Program.cardsChanged = true;
                }
                catch
                {
                    MessageBox.Show("Card with this name already exists in the database.", "No duplicates allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }            
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete all save cards?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string[] png = Directory.GetFiles(Program.path, "*.png");
                string[] jpg = Directory.GetFiles(Program.path, "*.jpg");

                foreach (string f in png)
                {
                    File.Delete(f);
                }

                foreach (string f in jpg)
                {
                    File.Delete(f);
                }

                MessageBox.Show("All cards have been deleted. Reset to see yourself.\nAdd more cards to be able to draw some.");
            }
        }
    }
}
