using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaťkovaOslavaÚkoly
{
    public partial class FormTasks : Form
    {
        public FormTasks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows a MessageBox with a text explaining the task hidden under this button and then disables the button.
        /// </summary>
        /// <param name="button">Reference to the button as an object.</param>
        /// <param name="number">Ordinal number of the task counting from 1.</param>
        private void TaskButtonClicked(object button, int number)
        {           
            MessageBox.Show(messages[number - 1], title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ((Button)button).Enabled = false;
        }

        //
        static string title = "Úkol";
        static string message1 = "Načepuj a přines oslavencovi pivo.";
        static string message2 = "Sólo s oslavencem na téma Labutí jezero.";
        static string message3 = "Společně s oslavencem zazpívej pomocí zvířecích citoslovcí píseň Skákal pes.";
        static string message4 = "Předveď holubičku.";
        string[] messages = new string[4] { message1, message2, message3, message4 };
        //


        private void buttonTask1_Click(object sender, EventArgs e)
        {
            TaskButtonClicked(sender, 1);
        }

        private void buttonTask2_Click(object sender, EventArgs e)
        {
            TaskButtonClicked(sender, 2);
        }

        private void buttonTask3_Click(object sender, EventArgs e)
        {
            TaskButtonClicked(sender, 3);
        }

        private void buttonTask4_Click(object sender, EventArgs e)
        {
            TaskButtonClicked(sender, 4);
        }
    }
}
