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

namespace ŠárkaKvíz
{
    public partial class FormHighScore : Form
    {
        public FormHighScore()
        {
            InitializeComponent();

            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormHighScore_FormClosed);
        }

        // SoundPlayery:
        System.Media.SoundPlayer fanfáry = new System.Media.SoundPlayer(ŠárkaKvíz.Properties.Resources.Fanfáry_new);
        System.Media.SoundPlayer potlesk = new System.Media.SoundPlayer(ŠárkaKvíz.Properties.Resources.Potlesk);
        System.Media.SoundPlayer prohra = new System.Media.SoundPlayer(ŠárkaKvíz.Properties.Resources.Lost);

        // Pomocné proměnné (seznamy):
        static string seznamString;        // Text textové souboru s highscore.
        static string[] seznamPomocnéPole = new string[3];     // Datum, přezdívka, skóre.
        static string[] seznamPole;        // Pole řádků textového souboru s highscore.
        static string[,] seznamMatice;     // Matice, kde jsou informace roztřízeny ve sloupcích a řádcích.
        static int indexNejvětší;
        // Další seznam (na třídění):
        static List<int> seznamTříděníPůvodní = new List<int>();
        static List<int> seznamTříděníNový = new List<int>();
        static List<int> seznamRealSkóre = new List<int>();

        // Pomocné počítadlo:
        static int počítadlo = 0;
        static int podmínkovéPočítadlo = 1;

        // Closed().
        private void FormHighScore_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Shown().
        private void FormHighScore_Shown(object sender, EventArgs e)
        {
            try
            {
                // Základní úpravy:
                labelVyPřezdívka.Text = FormSubmiting.přezdívka;
                labelVySkóre.Text = FormWelcoming.skóre.ToString();

                // Vytvoření matice s informacemi:           
                using (StreamReader sr = new StreamReader(FormSubmiting.cesta))
                {
                    seznamString = sr.ReadToEnd();
                }

                seznamPole = seznamString.Split('\n');
                seznamMatice = new string[seznamPole.Length - 1, 3];        // Poslední řádek by byl "", proto Lenght - 1.

                foreach (string řádek in seznamPole)
                {
                    if (řádek != "")
                    {
                        seznamPomocnéPole = řádek.Split('§');

                        foreach (string info in seznamPomocnéPole)
                        {
                            seznamMatice[Array.IndexOf(seznamPole, řádek), Array.IndexOf(seznamPomocnéPole, info)] = info;
                        }
                    }
                }

                // Vypsání samotných Highscores:

                while (počítadlo <= 9 && podmínkovéPočítadlo > 0)
                {
                    seznamTříděníPůvodní.Clear();
                    seznamTříděníNový.Clear();

                    for (int i = 0; i < seznamPole.Length - 1; i++)
                    {
                        seznamTříděníPůvodní.Add(int.Parse(seznamMatice[i, 2]));
                        seznamTříděníNový.Add(int.Parse(seznamMatice[i, 2]));
                        if (int.Parse(seznamMatice[i, 2]) > int.MinValue)
                        {
                            seznamRealSkóre.Add(int.Parse(seznamMatice[i, 2]));
                        }
                    }

                    seznamTříděníNový.Sort();      // Seřadí seznam čísel od nejmenšího po největší. (Viz Test1.cs)
                    seznamTříděníNový.Reverse();        // Bude seřazený od největšího po nejmenší.

                    // Zajistí, aby se cyklus opakovat maximálně tolikrát, kolik je opravdových submitnutých skóre.
                    if (počítadlo == 0)
                    {
                        podmínkovéPočítadlo = seznamRealSkóre.Count;
                    }

                    podmínkovéPočítadlo--;

                    // Zajistí, že se cyklus bude opakovat maximálně 10krát.
                    počítadlo++;

                    indexNejvětší = seznamTříděníPůvodní.IndexOf(seznamTříděníNový[0]);

                    switch (počítadlo)
                    {
                        case 1:
                            label1Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label1Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 2:
                            label2Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label2Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 3:
                            label3Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label3Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 4:
                            label4Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label4Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 5:
                            label5Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label5Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 6:
                            label6Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label6Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 7:
                            label7Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label7Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 8:
                            label8Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label8Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 9:
                            label9Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label9Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                        case 10:
                            label10Přezdívka.Text = seznamMatice[indexNejvětší, 1];
                            label10Skóre.Text = seznamMatice[indexNejvětší, 2];
                            break;
                    }
                    seznamMatice[indexNejvětší, 2] = int.MinValue.ToString();                   
                }

                if (int.Parse(labelVySkóre.Text) >= int.Parse(label1Skóre.Text))
                {
                    fanfáry.Play();
                }
                else if (int.Parse(labelVySkóre.Text) >= int.Parse(label10Skóre.Text))
                {
                    potlesk.Play();
                }
                else
                {
                    prohra.Play();
                }

                // Pokračovat:
                // 1) Zjistit jeho první index (druhý je 2).
                // 2) Najít nejvyšší seřazený prvek v matici.
                // 3) Přidat celý řádek s tímto indexem do nové matice (je třeba ještě vytvořit – stejná velikost jako seznamMatice).
                // 4) Nahradit všechny prvky tohoto řádku ve staré matici stringem int.MinValue.ToString().
                // 5) Opakovat 10×.
                // 6) Pak prvky nové matice v pořadí od [0, 1] a [0, 2] do [9, 1] a [9, 2] vložit do vlastnosti Text daných labelů.
            }
            catch
            {
                MessageBox.Show("Cesta neexistuje. Kontaktujte vašeho programátora.", "Chyba", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonHrátZnovu_Click(object sender, EventArgs e)
        {
            fanfáry.Stop();
            potlesk.Stop();
            prohra.Stop();
            this.Hide();
            FormWelcoming.skóre = 0;
            FormWelcoming.správných = 0.00;
            FormWelcoming.špatných = 0.00;
            FormWelcoming.returnedOnce = false;
            počítadlo = 0;
            podmínkovéPočítadlo = 1;
            // FormWelcoming.quizFirstTime = true;  // bugged
            (new FormDividing()).Show();
        }
    }
}
