using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Riskuj
{
    public partial class FormEnd : Form
    {
        public FormEnd()
        {
            InitializeComponent();
        }

        internal void CallMeWhenAccessingThisForm()
        {
            LoadNamesWithColors();
            LoadScoresWithColors();
            PrintCongratulations();
        }
        private void LoadNamesWithColors()
        {
            Label[] labels = new Label[4] { labelName1, labelName2, labelName3, labelName4 };
            for (int i = 0; i < 4; i++)
            {
                labels[i].Text = Program.formGame.finalOrder[i];
                labels[i].ForeColor = Program.formGame.finalColors[i];
            }
        }
        private void LoadScoresWithColors()
        {
            Label[] labels = new Label[4] { labelScore1, labelScore2, labelScore3, labelScore4 };
            for (int i = 0; i < 4; i++)
            {
                labels[i].Text = Program.formGame.finalScores[i];
                labels[i].ForeColor = Program.formGame.finalColors[i];
            }
        }
        private void PrintCongratulations()
        {
            MessageBox.Show("Soutěž vyhrál tým " + Program.formGame.finalOrder[0].Remove(0, 3) + "!\nGratulujeme vítězům a čest poraženým. :)", "Gratulace", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEndOfApp_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Opravdu chcete program ukončit?", "Potvrzení", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
                Application.Exit();
        }
    }
}
