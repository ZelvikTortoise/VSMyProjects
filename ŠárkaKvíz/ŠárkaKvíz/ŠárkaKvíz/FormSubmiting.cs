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
    public partial class FormSubmiting : Form
    {
        public FormSubmiting()
        {
            InitializeComponent();

            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormSubmiting_FormClosed);
        }

        // Proměnná cesta (UPRAVIT PODLE POTŘEBY):
        //public static string cesta = @"C:\Users\lolhe\Documents\Visual Studio 2013\Backup Files\ŠárkaKvíz\HighScore (don't move this).txt";     // Cesta u mě (Lukáše) doma na stolním PC (v pokoji).
        public static string cesta = @"C:\Users\Šárka\Desktop\Kvíz – highscore (don't move this).txt";        // Cesta u Šárky na notebooku.    !!! Pozor, soubor se jmenuje "Kvíz – highscore (don't move this.txt)"!!!
        //public static string cesta = @"C:\Users\luk19\OneDrive\Dokumenty\Visual Studio 2017\Backup Files\ŠárkaKvíz\HighScore (don't move this).txt";      // Cesta u mě (Lukáše) na notebooku.

        // Proměnná přezdívka:
        public static string přezdívka;

        // Pomocné proměnné:
        static string scores = "";

        // Closed().
        private void FormSubmiting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // buttonHotovo_Click():
        private void buttonHotovo_Click(object sender, EventArgs e)
        {
            if (textBoxPřezdívka.Text.Contains('§'))
            {
                MessageBox.Show("Přezdívka obsahuje nepovolený znak! (§) \n Zadejte prosím jinou přezdívku.", "Chyba", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            else if (textBoxPřezdívka.Text.Length < 3 || textBoxPřezdívka.Text.Length > 20)
            {
                MessageBox.Show("Zadejte přezdívku o délce minimálně 3 a maximálně 20 znaků.", "Chybí přezdívka", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                        MessageBox.Show("Cesta k souboru neexistuje! Kontaktujte vašeho programátora.", "Chyba", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        this.Show();
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(cesta))
                    {
                        scores = sr.ReadToEnd();
                    }

                    using ( StreamWriter sw = new StreamWriter(cesta))
                    {
                        sw.WriteLine(DateTime.Now + "§" + přezdívka + "§" + labelSkóre.Text);
                        sw.Write(scores);
                    }
                }

                this.Hide();
                (new FormHighScore()).Show();
            }
        }

        // Shown().
        private void FormSubmiting_Shown(object sender, EventArgs e)
        {
            textBoxPřezdívka.Text = "";
            labelSkóre.Text = FormWelcoming.skóre.ToString();
            labelSprávných.Text = FormWelcoming.správných.ToString();
            labelŠpatných.Text = FormWelcoming.špatných.ToString();
            if (FormWelcoming.správných + FormWelcoming.špatných == 0)
            {
                labelÚspěšnost.Text = "N/A";
            }
            else
            {
                labelÚspěšnost.Text = Math.Round((FormWelcoming.správných / (FormWelcoming.správných + FormWelcoming.špatných)) * 100, 2).ToString() + " %";
            }
        }
    }
}
