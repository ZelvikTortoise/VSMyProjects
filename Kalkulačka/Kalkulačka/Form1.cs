using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalkulačka
{
    public partial class FormCalculator : Form
    {
        public FormCalculator()
        {
            InitializeComponent();
        }

        //Deklarace proměnných
        static double x;
        static double y;
        static char operation;
        static bool overwrite;


        private void buttonNu0_Click(object sender, EventArgs e)
        {
            if (!overwrite)
            {
                textBoxDisplay.Text += "0";
            }
            else
            {
                textBoxDisplay.Text = "0";
            }


        }

        private void buttonNu1_Click(object sender, EventArgs e)
        {
            if (!overwrite)
            {
                textBoxDisplay.Text += "1";
            }
            else
            {
                textBoxDisplay.Text = "1";
            }
        }

        private void buttonNu2_Click(object sender, EventArgs e)
        {
            if (!overwrite)
            {
                textBoxDisplay.Text += "2";
            }
            else
            {
                textBoxDisplay.Text = "2";
            }
        }

            //Rovná se
            private void buttonRovnaSe_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonNu3_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "3";
                }
                else
                {
                    textBoxDisplay.Text = "3";
                }
            }

        private void buttonNu4_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "4";
                }
                else
                {
                    textBoxDisplay.Text = "4";
                }
            }

        private void buttonNu5_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "5";
                }
                else
                {
                    textBoxDisplay.Text = "5";
                }
            }

        private void buttonNu6_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "6";
                }
                else
                {
                    textBoxDisplay.Text = "6";
                }
            }

        private void buttonNu7_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "7";
                }
                else
                {
                    textBoxDisplay.Text = "7";
                }
            }

        private void buttonNu8_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "8";
                }
                else
                {
                    textBoxDisplay.Text = "8";
                }
            }

        private void buttonNu9_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += "9";
                }
                else
                {
                    textBoxDisplay.Text = "9";
                }
            }

        private void buttonDesetinnáCarka_Click(object sender, EventArgs e)
        {
                if (!overwrite)
                {
                    textBoxDisplay.Text += ",";
                }
                else
                {
                    textBoxDisplay.Text = ",";
                }
            }

        private void buttonSmazat_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = "";
        }

        //Plus
        private void buttonPlus_Click(object sender, EventArgs e)
        {
            buttonPlus.Enabled = false;
            buttonMinus.Enabled = false;
            buttonKrat.Enabled = false;
            buttonDeleno.Enabled = false;
            buttonOdmocnina.Enabled = false;
            buttonMocnina.Enabled = false;

            operation = '+';
            overwrite = true;
            x = double.Parse(textBoxDisplay.Text);


        }
    }
}
