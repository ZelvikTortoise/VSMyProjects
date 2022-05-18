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

        static Form2 snake = new Form2();  //  statická instance třídy Form2
        static bool k = false;  // statická proměnná k, která rozlišuje prvnotní spuštění od dalších

        public void GoToSnake()
        {
            this.Hide();
            if (!k)
            {
                k = true;
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
    }
}
