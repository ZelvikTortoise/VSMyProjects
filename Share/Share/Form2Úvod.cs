using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share
{
    public partial class Form2Úvod : Form
    {
        public Form2Úvod()
        {
            InitializeComponent();
        }

        Form2 snake = new Form2();  //   instance třídy Form2
        int k = 0;

        public void GoToSnake()
        {
            this.Hide();
            if (k == 0)
            {
                k++;
                snake.Closed += (s, args) => this.Close();      // Nutné pro vypnutí celého programu po vypnutí Form2.
                snake.Show();
            }
            else
            {
                snake.Show();
                MessageBox.Show("Vítejte ve hře Snyanke cat! Přejeme příjemnou zábavu. =)", "Snyanke cat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                snake.Restart();
            }
        }

        // Lehká
        private void button1_Click(object sender, EventArgs e)
        {
            snake.Difficulty = "easy";
            GoToSnake();
        }

        // Střední
        private void button2_Click(object sender, EventArgs e)
        {
            snake.Difficulty = "medium";
            GoToSnake();
        }

        // Těžká
        private void button3_Click(object sender, EventArgs e)
        {
            snake.Difficulty = "hard";
            GoToSnake();
        }

        // Shown()
        private void Form2Úvod_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(550, 175);        // Sjednotit s this.Location pro Form2!
        }
    }
}
