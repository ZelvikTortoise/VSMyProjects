using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace CSHra
{
    public partial class Form1 : Form
    {

        int AktualniLevel;

        public static int PocetLeveluProJednoho = Directory.GetFiles("plansforone", "*", SearchOption.TopDirectoryOnly).Length;

        public static int PocetLeveluProDva = Directory.GetFiles("plansfortwo", "*", SearchOption.TopDirectoryOnly).Length;

        public Button[,] levely = new Button[Math.Max(PocetLeveluProJednoho, PocetLeveluProDva)+1, 2];

        public Form1()
        {
            InitializeComponent();
            RychlostHry = (Timerhry.Maximum - Timerhry.Value + 1) * 100;
            RychlostHracu = (Timerhracu.Maximum - Timerhracu.Value + 1) * 100;
            TimerBomb = Timerbomb.Value * 1000;
            TimerItemu = Timeritem.Value * 1000;
            TimerBlesku = Timerblesku.Value * 1000;
        }
    
        Mapa mapa;
        public static Graphics g;

        public static int RychlostHry;

        public static int RychlostHracu;

        public static int pocetHracu;

        public static int TimerBomb;

        public static int TimerBlesku;

        public static int TimerItemu;

        public Timer TimerBlue = new Timer();

        public Timer TimerRed = new Timer();

        private void levelforone_Click(object sender, EventArgs e)
        {
            pocetHracu = 1;
            g = CreateGraphics();
            mapa = new Mapa("plansforone/plan"+((Button)sender).Tag.ToString()+".txt", "assets");
            this.Text = "Zbývá " + mapa.ZbyvaCasu / 1000 + " vteřin";
            Timer1.Interval = RychlostHry;
            Timer1.Enabled = true;
            TimerBlue.Tick += new EventHandler(TimerBlue_Tick);
            TimerBlue.Interval = RychlostHracu;
            TimerBlue.Enabled = true;
            SkryjVsechnyOvladaciPrvky();
        }

        private void levelfortwo_Click(object sender, EventArgs e)
        {
            pocetHracu = 2;
            g = CreateGraphics();
            mapa = new Mapa("plansfortwo/plan" + ((Button)sender).Tag.ToString() + ".txt", "assets");
            this.Text = "Zbývá " + mapa.ZbyvaCasu / 1000 + " vteřin";
            Timer1.Interval = RychlostHry;
            Timer1.Enabled = true;
            TimerBlue.Tick += new EventHandler(TimerBlue_Tick);
            TimerBlue.Interval = RychlostHracu;
            TimerBlue.Enabled = true;
            TimerRed.Tick += new EventHandler(TimerRed_Tick);
            TimerRed.Interval = RychlostHracu;
            TimerRed.Enabled = true;
            SkryjVsechnyOvladaciPrvky();
        }

        private void SkryjLevelyProJednohoHrace()
        {
            for (int i = 1; i <= PocetLeveluProJednoho; i++)
            {
                levely[i, 0].Visible = false;
            }
        }

        private void SkryjLevelyProDvaHrace()
        {
            for (int i = 1; i <= PocetLeveluProDva; i++)
            {
                levely[i, 1].Visible = false;
            }
        }

        private void SkryjVsechnyOvladaciPrvky()
        {
            Oneplayerpanel.Visible = false;
            Twoplayerspanel.Visible = false;
            Oneplayerbutton.Visible = false;
            Twoplayersbutton.Visible = false;
            Settings.Visible = false;
            Settingspanel.Visible = false;
            if (pocetHracu == 1)
            {
                SkryjLevelyProJednohoHrace();
            }
            if (pocetHracu == 2)
            {
                SkryjLevelyProDvaHrace();
            }
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            Timerhry.Visible = false;
            Timerhracu.Visible = false;
            Timerblesku.Visible = false;
            Timerbomb.Visible = false;
        }


        StisknutaSipka stisknutaSipka1 = StisknutaSipka.zadna;
        StisknutaBomba stisknutaBomba1 = StisknutaBomba.zadna;
        StisknutaSipka stisknutaSipka2 = StisknutaSipka.zadna;
        StisknutaBomba stisknutaBomba2 = StisknutaBomba.zadna;

        // HACK na odchyceni stisku sipek
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // No control key pressed.
            stisknutaSipka1 = StisknutaSipka.zadna;
            stisknutaBomba1 = StisknutaBomba.zadna;
            stisknutaSipka2 = StisknutaSipka.zadna;
            stisknutaBomba2 = StisknutaBomba.zadna;
            // Tento kód rozdělit do událostí timerů... 



            // Místo toho, co je pode mnou, udělej obrovský switch s default casem:

            switch (keyData)
            {
                case Keys.Up:
                    // bla bla
                    break;
                case Keys.Down:
                    // bla bla
                    break;
                // ...
                // ...
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;

            if (keyData == Keys.Up | keyData == Keys.Down | keyData == Keys.Left | keyData == Keys.Right | keyData == Keys.L | keyData == Keys.K |
                keyData == Keys.W | keyData == Keys.S | keyData == Keys.A | keyData == Keys.D | keyData == Keys.F | keyData == Keys.G)
            {              
                if (keyData == Keys.Up | keyData == Keys.Down | keyData == Keys.Left | keyData == Keys.Right)
                {
                    switch (keyData)
                    {
                        case Keys.Up:
                            stisknutaSipka1 = StisknutaSipka.nahoru;
                            Mapa.ikonky[4] = new Bitmap("orientationassets/K1.png");
                            break;
                        case Keys.Down:
                            stisknutaSipka1 = StisknutaSipka.dolu;
                            Mapa.ikonky[4] = new Bitmap("orientationassets/K3.png");
                            break;
                        case Keys.Left:
                            stisknutaSipka1 = StisknutaSipka.doleva;
                            Mapa.ikonky[4] = new Bitmap("orientationassets/K0.png");
                            break;
                        case Keys.Right:
                            stisknutaSipka1 = StisknutaSipka.doprava;
                            Mapa.ikonky[4] = new Bitmap("orientationassets/K2.png");
                            break;
                    }
                }

                if (keyData == Keys.L | keyData == Keys.K)
                {
                    switch (keyData)
                    {
                        case Keys.L:
                            stisknutaBomba1 = StisknutaBomba.poloz;
                            break;
                        case Keys.K:
                            stisknutaBomba1 = StisknutaBomba.odpal;
                            break;
                    }
                }

                if (keyData == Keys.W | keyData == Keys.S | keyData == Keys.A | keyData == Keys.D)
                {
                    switch (keyData)
                    {
                        case Keys.W:
                            stisknutaSipka2 = StisknutaSipka.nahoru;
                            Mapa.ikonky[26] = new Bitmap("orientationassets/S1.png");
                            break;
                        case Keys.S:
                            stisknutaSipka2 = StisknutaSipka.dolu;
                            Mapa.ikonky[26] = new Bitmap("orientationassets/S3.png");
                            break;
                        case Keys.A:
                            stisknutaSipka2 = StisknutaSipka.doleva;
                            Mapa.ikonky[26] = new Bitmap("orientationassets/S0.png");
                            break;
                        case Keys.D:
                            stisknutaSipka2 = StisknutaSipka.doprava;
                            Mapa.ikonky[26] = new Bitmap("orientationassets/S2.png");
                            break;
                    }
                }

                if (keyData == Keys.F | keyData == Keys.G)
                {
                    switch (keyData)
                    {
                        case Keys.F:
                            stisknutaBomba2 = StisknutaBomba.poloz;
                            break;
                        case Keys.G:
                            stisknutaBomba2 = StisknutaBomba.odpal;
                            break;
                    }
                }

                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            stisknutaSipka1 = StisknutaSipka.zadna;
            stisknutaBomba1 = StisknutaBomba.zadna;
            stisknutaSipka2 = StisknutaSipka.zadna;
            stisknutaBomba2 = StisknutaBomba.zadna;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            mapa.ZbyvaCasu = mapa.ZbyvaCasu - Timer1.Interval;
            if (mapa.ZbyvaCasu == 0) { mapa.stav = Stav.prohra; }
            switch (mapa.stav)
            {
                case Stav.bezi:
                    mapa.UdelejSeVsemiPrvkyKromeHracuCoUmi();
                    mapa.VykresliSe(g, ClientSize.Width, ClientSize.Height);
                    this.Text = "Rychlost hry: " + RychlostHry + "Rychlost prvniho: " + TimerBlue.Interval + "Rychlost druheho: " + TimerRed.Interval + " Zbývající čas: " + mapa.ZbyvaCasu / 1000 + ", modrý má " + mapa.hrdina1.zivotu + " životů";
                    if (pocetHracu == 2) { this.Text = this.Text + " a červený má " + mapa.hrdina2.zivotu + " životů"; }
                    break;
                case Stav.vyhra:
                    Timer1.Enabled = false;
                    MessageBox.Show("Vyhra!");
                    InitializeComponent();
                    break;
                case Stav.prohra:
                    Timer1.Enabled = false;
                    MessageBox.Show("Prohra!");
                    InitializeComponent();
                    break;
                default:
                    break;
            }
        }

        private void TimerBlue_Tick(object sender, EventArgs e)
        {
            if (RychlostHracu - 100 * mapa.hrdina1.rychlost >= 100) { TimerBlue.Interval = RychlostHracu - 100 * mapa.hrdina1.rychlost; }
            switch (mapa.stav) 
            {
                case Stav.bezi:
                    mapa.UdelejSModrymHrdinouCoUmi(stisknutaSipka1, stisknutaBomba1);
                    mapa.VykresliSe(g, ClientSize.Width, ClientSize.Height);
                    break;
                case Stav.vyhra:
                    TimerBlue.Enabled = false;
                    break;
                case Stav.prohra:
                    TimerBlue.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void TimerRed_Tick(object sender, EventArgs e)
        {
            if (RychlostHracu - 100 * mapa.hrdina2.rychlost >= 100) { TimerRed.Interval = RychlostHracu - 100 * mapa.hrdina2.rychlost; }
            switch (mapa.stav)
            {
                case Stav.bezi:
                    mapa.UdelejSCervenymHrdinouCoUmi(stisknutaSipka2, stisknutaBomba2);
                    mapa.VykresliSe(g, ClientSize.Width, ClientSize.Height);
                    break;
                case Stav.vyhra:
                    TimerBlue.Enabled = false;
                    break;
                case Stav.prohra:
                    TimerBlue.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void Oneplayer_Click(object sender, EventArgs e)
        {
            if (Twoplayersbutton.Visible == true)
            {
                Twoplayersbutton.Visible = false;
                NacteteSeLevelyProJednohoHrace(Oneplayerpanel.Width, Oneplayerpanel.Height, Oneplayerpanel.Top, Oneplayerpanel.Left);
            }
            else
            {
                Twoplayersbutton.Visible = true;
                SkryjLevelyProJednohoHrace();
            }
        }

        private void Twoplayersbutton_Click(object sender, EventArgs e)
        {
            if (Oneplayerbutton.Visible == true)
            {
                Oneplayerbutton.Visible = false;
                NacteteSeLevelyProDvaHrace(Twoplayerspanel.Width, Twoplayerspanel.Height, Twoplayerspanel.Top, Twoplayerspanel.Left);
            }
            else
            {
                Oneplayerbutton.Visible = true;
                SkryjLevelyProDvaHrace();
            }
        }

        public void NacteteSeLevelyProJednohoHrace(int sirkaPixely, int vyskaPixely, int horniHranice, int levaHranice)
        {
            pocetHracu = 1;
            int vyskay = vyskaPixely / PocetLeveluProJednoho;
            for (int i = 1; i <= PocetLeveluProJednoho; i++)
            {
                levely[i, 0] = new Button();
                levely[i, 0].Location = new Point(levaHranice, horniHranice + (i-1) * vyskay);
                levely[i, 0].Size = new System.Drawing.Size(sirkaPixely, vyskay);
                levely[i, 0].Visible = true;
                levely[i, 0].Tag = i;
                levely[i, 0].Text = "Level " + (i).ToString();
                levely[i, 0].Click += new EventHandler(levelforone_Click);
                levely[i, 0].BringToFront();
                this.Controls.Add(levely[i, 0]);
            }
            
        }

        public void NacteteSeLevelyProDvaHrace(int sirkaPixely, int vyskaPixely, int horniHranice, int levaHranice)
        {
            pocetHracu = 2;
            int vyskay = vyskaPixely / PocetLeveluProDva;
            for (int i = 1; i <= PocetLeveluProDva; i++)
            {
                levely[i, 1] = new Button();
                levely[i, 1].Location = new Point(levaHranice, horniHranice + (i - 1) * vyskay);
                levely[i, 1].Size = new System.Drawing.Size(sirkaPixely, vyskay);
                levely[i, 1].Visible = true;
                levely[i, 1].Tag = i;
                levely[i, 1].Text = "Level " + (i).ToString();
                levely[i, 1].Click += new EventHandler(levelfortwo_Click);
                levely[i, 1].BringToFront();
                this.Controls.Add(levely[i, 1]);
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if (Settingspanel.Visible == false)
            { 
                Settingspanel.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                Timerhry.Visible = true;
                Timerhracu.Visible = true;
                Timerblesku.Visible = true;
                Timerbomb.Visible = true;
            }
            else
            {
                Settingspanel.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                Timerhry.Visible = false;
                Timerhracu.Visible = false;
                Timerblesku.Visible = false;
                Timerbomb.Visible = false;
            }
        }

        private void Timerhry_Scroll(object sender, ScrollEventArgs e)
        {
            RychlostHry = (Timerhry.Maximum - Timerhry.Value + 1) * 100;
        }

        private void Timerhracu_Scroll(object sender, ScrollEventArgs e)
        {
            RychlostHracu = (Timerhracu.Maximum - Timerhracu.Value + 1) * 100;
        }

        private void Timerbomb_Scroll(object sender, ScrollEventArgs e)
        {
            TimerBomb = Timerbomb.Value * 1000;
        }

        private void Timerblesku_Scroll(object sender, ScrollEventArgs e)
        {
            TimerBlesku = Timerblesku.Value * 1000;
        }

        private void Timeritem_Scroll(object sender, ScrollEventArgs e)
        {
            TimerItemu = Timeritem.Value * 1000;
        }

        private void Settingspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Sounds.soundBomb.SoundLocation != "sounds/0.wav")
            {
                Soundbutton.Image = CSHra.Properties.Resources.audio;
                Sounds.soundBomb = new SoundPlayer("sounds/0.wav");
                Sounds.soundItem = new SoundPlayer("sounds/0.wav");
                Sounds.soundWin = new SoundPlayer("sounds/0.wav");
                Sounds.soundMonsterDeath = new SoundPlayer("sounds/0.wav");
                Sounds.soundPlayerBombDeath = new SoundPlayer("sounds/0.wav");
            }
            else
            {
                Soundbutton.Image = CSHra.Properties.Resources.speaker;
                Sounds.soundItem = new SoundPlayer("sounds/item.wav");
                Sounds.soundBomb = new SoundPlayer("sounds/bomb.wav");
                Sounds.soundWin = new SoundPlayer("sounds/win.wav");
                Sounds.soundMonsterDeath = new SoundPlayer("sounds/monsterdeath.wav");
                Sounds.soundPlayerBombDeath = new SoundPlayer("sounds/playerbombdeath.wav");
            }
        }

        private void timerPohybHráč1_Tick(object sender, EventArgs e)
        {
            // Pohni se tam, kam máš.
            // Pak dej směr pohybu na žádný.            
            switch (stisknutaSipka1)
            {
                case StisknutaSipka.nahoru:
                    // pohyb nahoru
                    break;
                case StisknutaSipka.dolu:
                    // pohyb dolů
                    break;
                case StisknutaSipka.doprava:
                    // pohyb doprava
                    break;
                case StisknutaSipka.doleva:
                    // pohyb doleva
                    break;
            }

            stisknutaSipka1 = StisknutaSipka.zadna;

            // Pokud jsi ručně vybuchl bomby, vybuchni je.
            // Polož bombu, pokud máš.
            // Pak nastav další nastavení bomby na žádné.
            switch (stisknutaBomba1)
            {
                case StisknutaBomba.odpal:
                    // odpal všechny bomby
                    break;
                case StisknutaBomba.poloz:
                    // polož bombu
                    break;
            }

            stisknutaBomba1 = StisknutaBomba.zadna;
        }

        private void timerVybuchBomb1_Tick(object sender, EventArgs e)
        {
            // Zmenši výbuch položených bomb.
        }

        private void timerPohybHráč2_Tick(object sender, EventArgs e)
        {
            // podobně...
        }

        private void timerVybuchBomb2_Tick(object sender, EventArgs e)
        {
            // podobně...
        }

        private void timerPrisery_Tick(object sender, EventArgs e)
        {
            // Pohyb příšer
        }
    }
}
