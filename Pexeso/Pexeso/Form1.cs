using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            NastavStav(Stav.Start);
        }

        private Random random = new Random();   // At nerandomujeme "porad stejne".

        enum Stav { Start, Hra, Jedna, Dve, Konec }
        private Stav stav = Stav.Start;
        private List<Button> otoceneDilky = new List<Button>();
        private int zbyvaParu = paruStart;
        private int pocetTahu = 0;

        private const string rubDilkuText = "PEXESO";
        private const int zpozdeni = 1000;  // v ms (po otoceni dvou dilku)
        private const string pocetTahuText = "Dosavadni pocet tahu: ";
        private const int paruStart = 18;      

        private void NastavStav(Stav novy)
        {
            // sw + <TAB><TAB>, napsat novy, <ENTER><ENTER> ... vytvori switch se vsemi case bloky diky tomu, ze novy je vyctoveho typu
            switch (novy)
            {
                case Stav.Start:
                    bStart.Visible = true;                    
                    bNovaHra.Visible = false;
                    bKonec.Visible = true;
                    lUvod.Visible = true;
                    lPocetTahu.Visible = false;
                    lVysledek.Visible = false;
                    break;
                case Stav.Hra:
                    if (this.stav == Stav.Start || this.stav == Stav.Konec)
                    {
                        VygenerujKarticky();    // Zacina nova hra.
                        pocetTahu = 0;
                        zbyvaParu = paruStart;
                        lPocetTahu.Text = pocetTahuText + pocetTahu.ToString();
                    }                        
                    bStart.Visible = false;
                    bNovaHra.Visible = false;
                    bKonec.Visible = true;
                    lUvod.Visible = false;
                    lPocetTahu.Visible = true;
                    lVysledek.Visible = false;
                    break;
                case Stav.Jedna:
                case Stav.Dve:
                    bStart.Visible = false;
                    bNovaHra.Visible = false;
                    bKonec.Visible = true;
                    lUvod.Visible = false;
                    lPocetTahu.Visible = true;
                    lVysledek.Visible = false;
                    break;
                case Stav.Konec:
                    bStart.Visible = false;
                    bNovaHra.Visible = true;
                    bKonec.Visible = true;
                    lUvod.Visible = false;
                    lPocetTahu.Visible = false;
                    lVysledek.Visible = true;

                    lVysledek.Text = "                  Gratulujeme k vyhre!\nCelkem potrebnych tahu k dokonceni hry:\n                              " + pocetTahu.ToString();
                    break;
                default:
                    throw new Exception("Unhandled value of enum " + nameof(Stav) + " in the " + nameof(NastavStav) + "(...) method.");
            }

            this.stav = novy;   // Nastavujeme novy stav.
        }

        private void VygenerujKarticky()
        {
            List<int> seznamHodnot = new List<int>();
            for (int i = 1; i <= paruStart; i++)
            {
                // Kazdou hodnotu chceme dvakrat.
                seznamHodnot.Add(i);
                seznamHodnot.Add(i);
            }

            const int N = 6;    // N × N obdelnik

            // Triky na rozmisteni:
            int sx = ClientRectangle.Width / N;
            int sy = (ClientRectangle.Height - 50) / N; // Odecitame, abychom meli misto pod dilky

            int x, y;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    x = i * sx;
                    y = j * sy;

                    Button b = new Button();
                    b.Parent = this;    // Prida se do this.Controls, diky tomu bude fungovat
                    // Okno:
                    b.Left = x;
                    b.Top = y;
                    b.Width = sx - 2;
                    b.Height = sy - 2;
                    b.Font = new Font(b.Font.FontFamily, 14, FontStyle.Regular);
                    b.Text = rubDilkuText;
                    b.Tag = ZiskejNahodnouHodnotu(seznamHodnot);
                    b.Click += Klik;
                }
            }
        }

        /// <summary>
        /// Dostane seznam hodnot, randomne pomoci random atributu Form1 index a vrati hodnotu v seznamu na danem indexu.
        /// Pote ze seznamu danou hodnotu (na danem indexu) odstani.
        /// </summary>
        /// <param name="seznam">Seznam hodnot, ze kterych se nahodne vybira hodnota</param>
        /// <returns></returns>
        private int ZiskejNahodnouHodnotu(List<int> seznam)
        {
            int index = random.Next(0, seznam.Count - 1);
            int hodnota = seznam[index];
            seznam.RemoveAt(index);

            return hodnota;
        }

        private void Klik(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            foreach (Button otocene in otoceneDilky)
            {
                if (otocene == b)   // Nechceme dělat nic
                    return;
            }

            if (otoceneDilky.Count == 0)
                NastavStav(Stav.Jedna);
            else if (otoceneDilky.Count == 1)
                NastavStav(Stav.Dve);
            
            b.Text = b.Tag.ToString();  // Muzeme vyuzit Tagu a "otocit" tak dilek
            otoceneDilky.Add(b);
            Refresh();  // Jinak se nezobrazi cislo na druhem dilku

            if (otoceneDilky.Count == 2)
            {
                System.Threading.Thread.Sleep(zpozdeni);
                pocetTahu++;
                lPocetTahu.Text = pocetTahuText + pocetTahu.ToString();
                
                if (!NalezenPar(otoceneDilky[0], otoceneDilky[1]))
                {
                    otoceneDilky[0].Text = rubDilkuText;
                    otoceneDilky[1].Text = rubDilkuText;
                }
                otoceneDilky.Clear();   // Seznam otocenych dilku je na konci hry vzdy prazdny.

                if (zbyvaParu == 0) // Uz jsme nasli vsechno.
                    NastavStav(Stav.Konec);
                else   // Porad je co hledat.
                    NastavStav(Stav.Hra);
            }
        }

        /// <summary>
        /// Pokud dilky jsou par (podle Textu, nikoliv Tagu), odstrani je a zmensi hodnotu paru potrebnych k nalezeni a vrati true.
        /// Jinak pouze vrati false.
        /// </summary>
        /// <param name="b1">Jeden dilek</param>
        /// <param name="b2">Druhy dilek</param>
        /// <returns>Je to par?</returns>
        private bool NalezenPar(Button b1, Button b2)
        {
            if (b1.Text == b2.Text)
            {
                //b1.Visible = false;
                b1.Parent = null;
                //b2.Visible = false;
                b2.Parent = null;
                this.zbyvaParu--;

                return true;
            }
            else
                return false;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            NastavStav(Stav.Hra);
        }

        private void bKonec_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Opravdu chcete hru ukoncit?", "Konec?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                Close();    // Vypne formular. // Lze take Application.Exit().
        }

        private void bNovaHra_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Opravdu chcete zacit novou hru?", "Nova hra?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
                NastavStav(Stav.Hra);
        }
    }
}
