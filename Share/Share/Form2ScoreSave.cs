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

namespace Share
{
    public partial class Form2ScoreSave : Form
    {
        public Form2ScoreSave()
        {
            InitializeComponent();
        }

        // Proměnné (nejsou potřeba vlastnosti):
        string name = "Share";
        string cesta = @"C:\Users\Šárka\Desktop\Snyanke cat scores.txt";
        bool saved = false;
        String scores;

        // Vlastnost score:
        public string Score
        {
            get;
            set;
        }

        // Vlasnost Dif (ukládá Form2.Difficulty):
        public string Dif
        {
            get;
            set;
        }

        // Save button.
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Form2 game = new Form2();
            // Points are already saved as Score.
            cesta = textBoxPath.Text;
            name = textBoxName.Text;

            if (!cesta.EndsWith(".txt"))
            {
                cesta += ".txt";
            }

            if (!File.Exists(cesta))
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(cesta))
                    {
                        sw.WriteLine(DateTime.Now);
                        sw.WriteLine(name + ": " + Score + " (Difficulty: " + Dif + ")");
                        MessageBox.Show("Skóre úspěšně uloženo! :) Hodně štěstí příště. ^^", "Oznámení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        saved = true;
                    }
                }
                catch
                {
                    MessageBox.Show("Cesta k souboru neexistuje! Zkontrolujte ji a zkuste to znovu.", "Chyba", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    this.Show();
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(cesta))
                {
                    scores = sr.ReadToEnd();
                }

                using (StreamWriter sw = new StreamWriter(cesta))
                {
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine(name + ": " + Score + " (Difficulty: " + Dif + ")");
                    sw.WriteLine("");
                    sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.Write(scores);
                    MessageBox.Show("Skóre úspěšně uloženo! :) Hodně štěstí příště. ^^", "Oznámení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    saved = true;
                }
            }
        }

        // Exit button.
        private void buttonEnd_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                MessageBox.Show("Děkuji, že jsi hrála tuto hru. ^^ Měj se hezky, ahoj. :3", "Rozloučení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else
            {
                DialogResult result = MessageBox.Show("Skóre nebylo uloženo. Pokud teď vypnete program, váš výsledek ztratíte. Chcete ukončit program?", "Upozornění", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Děkuji, že jsi hrála tuto hru. ^^ Měj se hezky, ahoj. :3", "Rozloučení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
        }

        // Browse button.
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                cesta = saveFileDialog.FileName;

                if (!cesta.EndsWith(".txt"))
                {
                    cesta += ".txt";
                }

                textBoxPath.Text = cesta;
            }
            // saveFileDialog.OverwritePrompt = false;      // Nezeptá se ohledně přepisování po vybrání již existujícího souboru.
        }

        // Shown().
        private void Form2ScoreSave_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(650, 200);        // Zkoušet, snažit se o prostředek obrazovky.
            textBoxScore.Text = Score;
        }
    }
}
