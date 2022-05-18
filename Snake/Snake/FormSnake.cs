using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Snake
{
    public partial class FormSnake : Form
    {       

        public FormSnake()
        {
            InitializeComponent();
            timerSnake.Enabled = false; // Zapnout časovač!
            labelTime.Text = "00:00";
            labelScore.Text = "000000";


        }

        // Deklarační prostor:
        int score = 0;
        int gameTimeSec = 0;
        int gameTimeMin = 0;

        private void panelPlayground_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            System.Drawing.Pen pero = new System.Drawing.Pen(Color.White);  // Kreslí bílou barvou.
            g.DrawRectangle(pero, 50, 50, 2, 2);
        }

        private void timerSnake_Tick(object sender, EventArgs e)
        {
            gameTimeSec++;
            if (gameTimeSec >= 60)
            {
                gameTimeSec = 0;
                gameTimeMin++;
            }

            if (gameTimeMin < 10)
            {
                if (gameTimeSec < 10)
                {
                    labelTime.Text = "0" + gameTimeMin.ToString() + ":0" + gameTimeSec.ToString();
                }
                else
                {
                    labelTime.Text = "0" + gameTimeMin.ToString() + ":" + gameTimeSec.ToString();
                }
            }
            else
            {
                if (gameTimeSec < 10)
                {
                    labelTime.Text = gameTimeMin.ToString() + ":0" + gameTimeSec.ToString();
                }
                else
                {
                    labelTime.Text = gameTimeMin.ToString() + ":" + gameTimeSec.ToString();
                }
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {

        }

        private void buttonDown_Click(object sender, EventArgs e)
        {

        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {

        }

        private void buttonRight_Click(object sender, EventArgs e)
        {

        }
    }
}
