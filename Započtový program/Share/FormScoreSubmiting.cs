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
    public partial class FormScoreSubmiting : Form
    {
        public FormScoreSubmiting()
        {
            InitializeComponent();
        }

        // Proměnná cesta (UPRAVIT PODLE POTŘEBY):
        public const string cesta = "Snyanke - highscore (don't move this).txt";        // Cesta.

        // Veřejný nickname:
        public static string přezdívka;

        // Pomocné proměnné:
        static string scores = "";


        private void FormScoreSubmiting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonHotovo_Click(object sender, EventArgs e)
        {
            if (textBoxPřezdívka.Text.Contains('§'))
            {
                MessageBox.Show("Přezdívka obsahuje nepovolený znak! (§) \n Zadejte prosím jinou přezdívku.", "Nepovolený znak", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            else if (textBoxPřezdívka.Text.Length < 3 || textBoxPřezdívka.Text.Length > 20)
            {
                MessageBox.Show("Zadejte přezdívku o délce minimálně 3 a maximálně 20 znaků.", "Nesprávná přezdívka", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {

                přezdívka = textBoxPřezdívka.Text;

                if (!File.Exists(cesta))
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(cesta))
                        {
                            sw.WriteLine(DateTime.Now + "§" + přezdívka + "§" + labelSkóre.Text);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Cesta k souboru neexistuje! Kontaktujte vašeho programátora.", "Chyba xD", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
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
                        sw.WriteLine(DateTime.Now + "§" + přezdívka + "§" + labelSkóre.Text);
                        sw.Write(scores);
                    }
                }

                // Jde napsat kratší cestou.
                FormHighScores inst = new FormHighScores();
                this.Hide();
                inst.Closed += (s, args) => this.Close();
                inst.Show();
            }
        }

        private void FormScoreSubmiting_Shown(object sender, EventArgs e)
        {
            textBoxPřezdívka.Text = "";
            int min = Form2.info[3];
            int sec = Form2.info[4];
            labelSkóre.Text = Form2.info[0].ToString(); 
            labelJídla.Text = Form2.info[1].ToString();
            labelBonusů.Text = Form2.info[2].ToString();
            switch(Form2.info[5])
            {
                case 1:
                    labelObtížnost.Text = "Lehká";
                    break;
                case 2:
                    labelObtížnost.Text = "Střední";
                    break;
                case 3:
                    labelObtížnost.Text = "Těžká";
                    break;
                default:    // Nemělo by nastat, ale kdyby náhodou, tak se tím myslí hard.
                    labelObtížnost.Text = "Těžká";
                    break;
            }
            // Předpokládám, že déle jak 60 minut hra trvat nebude. Pokud by se tak stalo, minuty budou nabývat i dál. (Po čase 99:59 se půjde na 100:00, atd.)
            if (min < 10)
            {
                if (sec < 10)
                {
                    labelČas.Text = "0" + min.ToString() + ":0" + sec.ToString();
                }
                else
                {
                    labelČas.Text = "0" + min.ToString() + ":" + sec.ToString();
                }
            }
            else
            {
                if (sec < 10)
                {
                    labelČas.Text = min.ToString() + ":0" + sec.ToString();
                }
                else
                {
                    labelČas.Text = min.ToString() + ":" + sec.ToString();
                }
            }
            
        }
    }
}
