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
    public partial class Form1 : Form
    {
        // Inicializace komponentů.
        public Form1()
        {
            InitializeComponent();
        }

        static Random random = new Random(); // Instance třídy Random
        static int pressed = 0;             // Počítadlo pro switch hlavního větvení
        static int timer_option = 1;        // Určuje, které operace bude Timer provádět (stačí tak jeden Timer); defaultní možnost je 1
        static int i = 0;                   // Globální počítadlo
        static string cesta = @"C:\Users\Šárka\Desktop\Sny.txt";        // Cesta k souboru, kam se ukládají sny
        static string předchozíSny = "";
        static bool savedDream = false;
        List<string> vtipy = new List<string>();                // Seznam vtipů
        List<string> vtipnéVzpomínky = new List<string>();      // Seznam vtipných vzpomínek (zatím nefunkční)
        List<string> hezkéVzpomínky = new List<string>();       // Seznam hezkých vzpomínek (zatím nefunknčí)
        List<string> motivačníCitáty = new List<string>();      // Seznam motivačních citátů (zatím nefunkční)

        // Upraví velikosti a pozici jednoltivých komponent (skok je vždy 25: v souřadnici y) a Visibility jednoltivých radiobuttonů.
        public void DvěMožnosti()
        {
            this.Height = 231;

            groupBoxOdpovědi.Height = 66;

            labelČas.Location = new Point(3, this.Height - 55);

            buttonOdpověz.Location = new Point(this.Width / 2 - (buttonOdpověz.Width + 14) / 2, 132);
            buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);

            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
        }
        public void TřiMožnosti()
        {
            this.Height = 256;

            groupBoxOdpovědi.Height = 91;

            labelČas.Location = new Point(3, this.Height - 55);

            buttonOdpověz.Location = new Point(this.Width / 2 - (buttonOdpověz.Width + 14) / 2, 157);
            buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);

            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
        }
        public void ČtyřiMožnosti()
        {
            this.Height = 281;

            groupBoxOdpovědi.Height = 116;

            labelČas.Location = new Point(3, this.Height - 55);

            buttonOdpověz.Location = new Point(this.Width / 2 - (buttonOdpověz.Width + 14) / 2, 182);
            buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);

            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
        }
        public void PětMožností()
        {
            this.Height = 306;

            groupBoxOdpovědi.Height = 141;

            labelČas.Location = new Point(3, this.Height - 55);

            buttonOdpověz.Location = new Point(this.Width / 2 - (buttonOdpověz.Width + 14) / 2, 207);
            buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);

            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton5.Visible = true;
            radioButton6.Visible = false;
        }
        public void ŠestMožností()
        {
            this.Height = 331;

            groupBoxOdpovědi.Height = 166;

            labelČas.Location = new Point(3, this.Height - 55);

            buttonOdpověz.Location = new Point(this.Width / 2 - (buttonOdpověz.Width + 14) / 2, 232);
            buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);

            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton5.Visible = true;
            radioButton6.Visible = true;
        }

        // MessageBox.Show(...);
        public void Reakce(string zpráva)
        {
            MessageBox.Show(zpráva, "Reakce", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }       // Pokračuje v programu
        public void Rozloučení(string zpráva)
        {
            MessageBox.Show(zpráva, "Rozloučení", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }   // Vypne program

        // Změna textu jednotlivých radiobuttonů.
        public void Možnost1(string text)
        {
            radioButton1.Text = text;
        }
        public void Možnost2(string text)
        {
            radioButton2.Text = text;
        }
        public void Možnost3(string text)
        {
            radioButton3.Text = text;
        }
        public void Možnost4(string text)
        {
            radioButton4.Text = text;
        }
        public void Možnost5(string text)
        {
            radioButton5.Text = text;
        }
        public void Možnost6(string text)
        {
            radioButton6.Text = text;
        }

        // Změna textu labelu s otázkou / návrhy.
        public void Otázka(string text)
        {
            labelOtázka.Text = text;
        }

        // Zmáčknutí tlačítka Odpověz bez vybrání možnosti.
        public void Chyba()
        {
            pressed--;
            MessageBox.Show("Vyber prosím nějakou možnost z nabídky.", "Upozornění", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        }    

        // Mění vlastnost Checked radioButtonu1.
        public void RadioButton1Checked(bool value)
        {
            radioButton1.Checked = value;
        }

        // Vypíná a zapíná timerČas.
        public void TimerČasEnabled(bool value)
        {
            timerČas.Enabled = value;
        }

        // Vlastnosti času.
        public int Hour { get { return DateTime.Now.Hour; } }      // Vlastnost Hour (vrací aktuální hodinu)
        public int Minute { get { return DateTime.Now.Minute; } }  // Vlanstnost Minute (vrací aktuální minuty)


        // TimerČas (využíván k měření času každou sekundu).
        private void timerČas_Tick(object sender, EventArgs e)
        {
            if (Minute < 10)
            {
                labelČas.Text = Hour.ToString() + ":0" + Minute.ToString();
            }
            else
            {
                labelČas.Text = Hour.ToString() + ":" + Minute.ToString();
            }
        }

        // Timer (využíván na víc věcí díky proměnné "int timer_option").
        private void timer_Tick(object sender, EventArgs e)     // Interval: 1 tik za sekundu
        {
            switch (timer_option)
            {
                case 1:
                    // volné místo pro činnost Timeru timer;
                    break;
                case 2:
                    i++;
                    timer.Interval = 500;
                    labelPozdrav.Text = "Program se zavře za " + (3 - i).ToString() + "...";
                    Refresh();

                    if (i == 3)     // doba zavírání programu v sekundách v případě nepovolení jeho zapnutí
                    {
                        Refresh();
                        Application.Exit();
                    }
                    break;
            }
        }

        // Nastaví vlastnost Visible jednoltivých komponent na požadovanou hodnotu a k tomu vlastnosti Width a Location (this, groupBoxOdpovědi, labelOtázka).
        public void Reset()
        {
            this.Location = new Point(750, 250);    // Stejné jako v události Form1_Shown() – zkoušet a měnit, chceme prostředek obrazovky.
            this.Width = 250;

            groupBoxOdpovědi.Location = new Point(24, 45);
            groupBoxOdpovědi.Width = 150;

            // buttonOdpověz.Location se nastavuje automaticky v metodách DvěMožnosti(), TřiMožnosti(), apod.

            this.Visible = false;       // !!! Nutné vždy připsat this.Visible = true; (ke každému větvení ve switchi)
            labelČas.Visible = true;
            labelOtázka.Location = new Point(labelOtázka.Location.X, groupBoxOdpovědi.Location.Y - 20);
            labelOtázka.Visible = true;
            groupBoxOdpovědi.Visible = true;
            radioButton1.Visible = false;
            radioButton1.Enabled = true;
            radioButton2.Visible = false;
            radioButton2.Enabled = true;
            radioButton3.Visible = false;
            radioButton3.Enabled = true;
            radioButton4.Visible = false;
            radioButton4.Enabled = true;
            radioButton5.Visible = false;
            radioButton5.Enabled = true;
            radioButton6.Visible = false;
            radioButton6.Enabled = true;
            buttonOdpověz.Visible = true;
            buttonOdpověz.Enabled = true;
            buttonKonec.Visible = true;
            buttonKonec.Enabled = true;

            labelPozdrav.Visible = false;
            labelVtip.Visible = false;
            textBoxVtip.ReadOnly = true;
            textBoxVtip.Visible = false;

            textBoxCesta.Visible = false;
            buttonProcházet.Visible = false;
            buttonUložit.Visible = false;

            timerČas.Interval = 1000;
            timerČas.Enabled = true;
            timer.Interval = 1000;      // 1000, ale při nepovolení spuštění je defaultně 500
            timer.Enabled = false;
        }

        // Nastaví vlastnost Checked u všech radiobuttonů na False.
        public void Odznačit()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
        }


        // Tlačítko buttonKonec – po potvrzení ukončí program.
        private void buttonKonec_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Opravdu chceš aplikaci ukončit?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Tlačítko buttonKonec – Visible nebo ne.
        public void buttonKonecVisibility(bool value)
        {
            buttonKonec.Visible = value;
        }

        // Vybere soubor, do kterého se bude ukládat sen.
        private void buttonProcházet_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                cesta = saveFileDialog.FileName;

                if (!cesta.EndsWith(".txt"))
                {
                    cesta += ".txt";
                }

                textBoxCesta.Text = cesta;
            }
        }

        // Připíše sen do vybraného souboru a přidá datum a čas.
        private void buttonUložit_Click(object sender, EventArgs e)
        {
            cesta = textBoxCesta.Text;

            if (!cesta.EndsWith(".txt"))
            {
                cesta += ".txt";
            }

            if (!File.Exists(cesta))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(cesta))
                    {
                        sw.WriteLine(DateTime.Now);
                        sw.WriteLine(textBoxVtip.Text);     // (Píše se sen, ne vtip.)
                        MessageBox.Show("Sen úspěšně uložen. Cesta k nově vytvořenému souboru: \"" + cesta + "\"", "Oznámení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        savedDream = true;
                    }
                }
                catch
                {
                    MessageBox.Show("Cesta k souboru neexistuje! Zkontrolujte ji a zkuste to znovu.", "Chyba", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (radioButton1.Checked)   // "Chci ti napsat, co se mi zdálo :D": 2 možnosti (ještě X to je vše)
                    {
                        labelVtip.Text = "Tvůj sen:";
                        labelVtip.Location = new Point(22, 30);
                        labelVtip.Visible = true;
                        textBoxVtip.Location = new Point(90, 27);
                        textBoxVtip.Width = 220;
                        textBoxVtip.Height = 124;
                        textBoxVtip.Visible = true;
                        textBoxVtip.ReadOnly = false;

                        this.Width = 350;
                        this.Visible = true;
                        DvěMožnosti();
                        labelOtázka.Location = new Point(labelOtázka.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 10);
                        this.Height += 110;
                        groupBoxOdpovědi.Width = 200;
                        groupBoxOdpovědi.Location = new Point(groupBoxOdpovědi.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 30);
                        buttonOdpověz.Location = new Point(buttonOdpověz.Location.X, groupBoxOdpovědi.Location.Y + groupBoxOdpovědi.Height + 10);
                        buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);
                        labelČas.Location = new Point(3, this.Height - 55);

                        textBoxCesta.Location = new Point(7, 85);
                        textBoxCesta.Visible = true;
                        buttonProcházet.Location = new Point(7, 50);
                        buttonProcházet.Visible = true;
                        buttonUložit.Location = new Point(7, 110);
                        buttonUložit.Visible = true;

                        Otázka("Ještě nějaký sen mi chceš napsat? :3");

                        Možnost1("Jo, ještě jeden :D");
                        Možnost2("Ne, to je vše :)");

                        pressed = 12;
                        // goto case 13
                    }
                }
            }
            else
            {
                using (StreamReader sr = new StreamReader(cesta))
                {
                    předchozíSny = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(cesta))        // Text se nebude přepisovat.
                {
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine(textBoxVtip.Text);     // (Píše se sen, ne vtip.)
                    sw.WriteLine("");
                    sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("");
                    sw.Write(předchozíSny);
                    MessageBox.Show("Sen úspěšně uložen. Cesta k souboru: \"" + cesta + "\"", "Oznámení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    savedDream = true;
                }
            }
        }

        // Disabluje tlačítko Odpověz, když žádné z viditelných radiobuttonů není Enabled (je v této události pro snížení náročnosti programu).
        private void buttonOdpověz_MouseEnter(object sender, EventArgs e)
        {
            int n = 6;
            int m = 0;

            if (radioButton1.Visible)
            {
                if (!radioButton1.Enabled)
                {
                    m++;
                }
            }
            else
            {
                n--;
            }

            if (radioButton2.Visible)
            {
                if (!radioButton2.Enabled)
                {
                    m++;
                }
            }
            else
            {
                n--;
            }

            if (radioButton3.Visible)
            {
                if (!radioButton3.Enabled)
                {
                    m++;
                }
            }
            else
            {
                n--;
            }

            if (radioButton4.Visible)
            {
                if (!radioButton4.Enabled)
                {
                    m++;
                }
            }
            else
            {
                n--;
            }

            if (radioButton5.Visible)
            {
                if (!radioButton5.Enabled)
                {
                    m++;
                }
            }
            else
            {
                n--;
            }

            if (radioButton6.Visible)
            {
                if (!radioButton6.Enabled)
                {
                    m++;
                }
            }
            else
            {
                n--;
            }

            if (!(m < n))
            {
                buttonOdpověz.Enabled = false;
            }
        }

        // Přidá do seznamu vtipů všechny vtipy.
        public void PřidejVtipy()
        {
            vtipy.Add("Sanitka jede do nemocnice a náhle zastaví. Zpocený řidič říká: „Hej, sakra, chcíplo mně to.“ A zezadu zakřičí doktor: „To je jedno, mně taky...“");      // [0]
            vtipy.Add("Jaký je rozdíl mezi učitelkou a žumpou? Žádný. Buď je nasraná, anebo vyčerpaná.");      // [1]
            vtipy.Add("Rodiče jsou jako tousty... Když jsou černí, nenajíš se.");      // [2]
            vtipy.Add("V Česku máme technický stav silnic na jedničku. Problém nastává, když zařadíte dvojku...");      // [3]
            vtipy.Add("Ptá se učitel dějepisu: „Co víme o lidech, kteří žili ve starověku?“ „Že už všichni umřeli.“");     // [4]
            vtipy.Add("Hrobařovi deprese: „Kdo jinému jámu kopá...");      // [5]
            vtipy.Add("Velitel v koncentračním táboře se ptá chlapce: „Kolik ti je let?“ Kluk odpoví: „Bude mi patnáct.“ Velitel: „Hmmm, nebude.“");      // [6]
            vtipy.Add("V kanibalské porodnici: „2 kila čtyřicet, můžu to tak nechat?");      // [7]
            vtipy.Add("Přijde chlap na Paralympiádu a nikde ani noha.");      // [8]
            vtipy.Add("Co znamená, když je u firmy vyvěšena černá vlajka? No přece jedno volné místo...");      // [9]
            vtipy.Add("Jaký je rozdíl mezi běžícím a létajícím zajícem? No přece ten orel.");      // [10]
            vtipy.Add("Kapr říká kaprovi: „Hele, Pepo, věříš na život po Vánocích?“");      // [11]
            vtipy.Add("Pacient se ptá doktora při vizitě: „A pane doktore, co je to vlastně za nemoc?“ Doktor odpovíd: „Zatím nevíme, to ukáže až pitva.“");      // [12]
            vtipy.Add("Jde žížala navštívit svou sestru a ptá se jí: „Kde máš manžela?“ „Je s kámošema na rybách.“");      // [13]
            vtipy.Add("Co říká slepý, když umývá strouhadlo: „Takovou blbost jsem ještě nečetl...“");      // [14]
            vtipy.Add("„Kukačko, zakukej, kolik mi ještě zbývá let?“ „Ku.“ „Proč tak má–...?“");        // [15]
            vtipy.Add("Byli jednou dva cikáni. To byly časy...");       // [16]
            vtipy.Add("Víš co má nekrofilní pedofil nejraději? No přece vychlazenou dvanáctku.");       // [17]
            vtipy.Add("Víš, co se zeptá slušný pedofil, když přijde na návštěvu? No přece: „Můžu si skočit na malou?“");        // [18]
            vtipy.Add("Honzíček přijde domů a volá na maminku: „Maminko! V parku byl pán a spadl ze stromu!“ Maminka: „A nestalo se mu nic!?“ Honzíček: „Ne, naštěstí ho zachytilo to lano, co měl uvázané kolem krku.“");      // [19]
            vtipy.Add("„To je zvláštní... Když piju kakao, tak mě píchá v oku.“ „Tak si vytáhni tu lžičku z toho hrnku...“");       // [20]
            vtipy.Add("Víš, co dělá lenochod, když hoří les? Hoří taky.");      // [21]
            vtipy.Add("Sestra: „Doktore, ten simulant z dvojky zrovna zemřel.“ Doktor: „Tak to už ale vážně přehnal!“");        // [22]
        }

        // Přidá do seznamu vtipných vzpomínek všechny vtipné vzpomínky.
        public void PřidejVtipnéVzpomínky()
        {
            // zatím prázdné
        }

        // Přidá do seznamu hezkých vzpomínek všechny hezké vzpomínky.
        public void PřidejHezkéVzpomínky()
        {
            // zatím prázdné
        }

        // Přidá do seznamu motivačních citátů všechny motivační citáty.
        public void PřidejMotivačníCitáty()
        {
            motivačníCitáty.Add("„Volný čas tvoří nejdůležitější část našeho života.“ ~ Denis Diderot");      // [0] 
            motivačníCitáty.Add("„Není pravda, že máme málo času, pravdou ale je, že ho hodně promarníme.“ ~ Seneca");      // [1]
            motivačníCitáty.Add("„Neodpoutávej se nikdy od svých snů! Když zmizí, budeš dál existovat, ale přestaneš žít.“ ~ Mark Twain");      // [2]
            motivačníCitáty.Add("„Pamatuj, že i ta nejtěžší hodina ve tvém životě, má jen 60 minut.“ ~ Sofoklés");      // [3]
            motivačníCitáty.Add("„Jsem něžný, jsem krutý, ale jsem život. Pláčeš? I v slzách je síla. Tak jdi a žij.“ ~ John Lennon");      // [4]
            motivačníCitáty.Add("„Žijete jenom jednou. Tak by to měla být zábava.“ ~ Coco Chanel");      // [5]
            motivačníCitáty.Add("„Nejlepší způsob, jak se do něčeho pustit, je přestat o tom mluvit a začít to dělat.“ ~ Walt Disney");      // [6]
            motivačníCitáty.Add("„Život se nepíše, život se žije.“ ~ Josef Čapek");      // [7]
            motivačníCitáty.Add("„Učitel ti může otevřít dveře, ale vstoupit do nich musíš ty sám.“ ~ (Neznámý autor)");      // [8]
            motivačníCitáty.Add("„Nenechte se zastrašit dlouhými slovy. Všechny skutečně důležité věci jako život, smrt, hlad, strach, den, noc i láska mají krátké názvy.“ ~ Marcel Aymé");      // [9]
            motivačníCitáty.Add("„Až Ti bude úzko otoč se čelem ke slunci. Všechny stíny budeš mít za zády.“ ~ Jan Werich");      // [10]
            motivačníCitáty.Add("„Cokoli dá ti osud, to snášej, a zvítězíš nad ním.“ ~ Maro Publius Vergilius");      // [11]
            motivačníCitáty.Add("„Věř lidem a oni budou věřit tobě, přistupuj k nim, jako by byli velcí, a oni vyrostou.“ ~ Ralph Waldo Emerson");      // [12]
            motivačníCitáty.Add("„Udělat věc, které se bojíme, je první krok k úspěchu.“ ~ Mahátma Gándhí");      // [13]
            motivačníCitáty.Add("„Jestliže tvoje štěstí závisí na tom, co dělá někdo druhej, pak máš, myslím, problém!“ ~ Richard Bach");      // [14]
            motivačníCitáty.Add("„Nikdo se mě neptal jestli se chci narodit, tak ať mi neříká jak mám žít.“ ~ (Neznámý autor)");      // [15]
            motivačníCitáty.Add("„Budete-li se snažit porozumět celému vesmíru, nepochopíte vůbec nic. Jestliže se pokusíte porozumět sobě, pochopíte celý vesmír.“ ~ Buddha");      // [16]
            motivačníCitáty.Add("„Představivost je důležitější než vědomosti.“ ~ Albert Einstein");      // [17]
            motivačníCitáty.Add("„Pro život, ne pro školu se učíme.“ ~ Seneca");      // [18]
            motivačníCitáty.Add("„Jste-li na pochybách, říkejte pravdu.“ ~ Mark Twain");      // [19]
            motivačníCitáty.Add("„Otevřete se změnám, ale neztrácejte své vlastní hodnoty.“ ~ Dalajláma");      // [20]
            motivačníCitáty.Add("„Vše, co je v člověku krásné, je očima neviditelné.“ ~ Antoine de Saint-Exupéry");      // [21] 
            motivačníCitáty.Add("„Je jenom jedna cesta za štěstím a to přestat se trápit nad tím, co je mimo naši moc.“ ~ Epiktétos");        // [22]
            motivačníCitáty.Add("„Žij přítomností, sni o budoucnosti, uč se minulostí.“ ~ (Neznámý autor)");        // [23]
            motivačníCitáty.Add("„Čas má plné kapsy překvapení.“ ~ Jan Werich");        // [24]
            motivačníCitáty.Add("„Osud míchá karty, my hrajeme.“ ~ Arthur Schopenhauer");        // [25]
            motivačníCitáty.Add("„Dělej dobro a dobro se ti vrátí.“ ~ Buddha");        // [26]
            motivačníCitáty.Add("„Kdo vítězí nad lidmi, je mocný. Kdo vítězí nad sebou, je nejmocnější.“ ~ Lao-c'");        // [27]
            motivačníCitáty.Add("„Nechtěj být člověkem, který je úspěšný, ale člověkem, který za něco stojí.“ ~ Albert Einstein");        // [28]
            motivačníCitáty.Add("„Před námi jsou lepší věci než ty, které zůstaly za námi.“ ~ Clive Staples Lewis");        // [29]
            motivačníCitáty.Add("„Ne všichni, kteří bloudí, jsou ztraceni.“ ~ John Ronald Reuel Tolkien");        // [30]
            motivačníCitáty.Add("„Cestu buď najdu, nebo udělám.“ ~ Hannibal");        // [31]
            motivačníCitáty.Add("„Existuje tisíce způsobů, jak zabít čas, ale žádný, jak ho vzkřísit.“ ~ Albert Einstein");        // [32]
            motivačníCitáty.Add("„Kdo chce hýbat světem, ať nejprve hýbe sám sebou.“ ~ Sókratés");        // [33]
            motivačníCitáty.Add("„Nejlepším způsobem k předpovědi budoucnosti je vytvořit ji!“ ~ Alan Kay");        // [34]
            motivačníCitáty.Add("„Nemusíte být skvělí, abyste začali, ale musíte začít, abyste byli skvělí.“ ~ Les Brown");        // [35]
            motivačníCitáty.Add("„Myšlenky, které si zvolíte, jsou nástrojem, kterým malujete na plátno svého života.“ ~ Louise L. Hay");        // [36]
            motivačníCitáty.Add("„Jediné, co vám asi bude bránit jít za svými cíly je osoba, která stojí ve vašich botách, nosí vaše šaty a uvažuje vašimi negativními myšlenkami.“ ~ Les Brown");        // [37]
            motivačníCitáty.Add("„Je lepší rozsvítit, byť jen malou svíčku, než proklínat temnotu.“ ~ Konfucius");        // [38]
            motivačníCitáty.Add("„Pokaždé, když si myslím, že nemůžu, můžu ještě mnohokrát víc. Pokaždé, když myslím, že něco nejde, někdo mi ukáže, že to jde snadno. Bez vůle ani sebevětší talent není nic. Zato vůlí zmůžete všechno.“ ~ Honoré de Balzac");        // [39]
            motivačníCitáty.Add("„Svět je nádherná kniha, ale nemá cenu pro toho, kdo v ní neumí číst.“ ~ Carlo Goldoni");        // [40]
            motivačníCitáty.Add("„Je zhola zbytečné se ptát, má-li život smysl či ne. Má takový smysl, jaký mu dáme.“ ~ Seneca");        // [41]
            motivačníCitáty.Add("„Člověk se má zdokonalovat, i když je mu osmdesát. Nevážím si lidí, kteří říkají: „Jsem už takový.“ Ach, starý blázne, buď tedy jiný!“ ~ Voltaire");        // [42]
        }

        // Shown
        // Požádá o povolení se spustit, pozdraví a vyhodí úvodní obrazovku s otázkou: "Jakou jsi měla noc?" (ZDE MĚNIT INTERVAL ZAMRZNUTÍ!!!)
        private void Form1_Shown(object sender, EventArgs e)
        {

            PřidejVtipy();
            PřidejHezkéVzpomínky();
            PřidejVtipnéVzpomínky();
            PřidejMotivačníCitáty();

            this.Width = 0;
            this.Height = 0;
            this.Text = "Kamarád";
            this.Visible = false;
            this.Location = new Point(300, 300);        // Změnit, zkoušet (chceme prostředek obrazovky): Uvítací obrazovka

            DialogResult result;

            result = MessageBox.Show("Povídací program pro Share (verze 2.01) dostal příkaz se spustit. Udělíš mu povolení? =)", "Dobré ráno", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                this.Location = new Point(450, 325);        // Zkoušet, snažit se o střed obrazovky.
                this.Width = 589;
                this.Height = 214;

                labelPozdrav.Text = "Program se zavře za " + (3 - i).ToString() + "...";
                labelPozdrav.Visible = true;

                this.Visible = true;

                timer.Interval = 100;   // Původně 1000, po úpravě 500, na žádost 100.
                timer_option = 2;
                timer.Enabled = true;   // Vypne program za určitou dobu
            }
            else
            {
                this.Width = 966;
                this.Height = 214;
                this.Visible = true;

                labelPozdrav.Text = "Ahoj Share! :3 Já jsem tvůj program. :D";
                labelPozdrav.Visible = true;
                this.Refresh();

                System.Threading.Thread.Sleep(1500);    // Change to 1500 (na žádost) after testing or to <= 1000 during testing

                labelPozdrav.Text = "";
                labelPozdrav.Visible = false;

                Otázka("Jakou jsi měla noc? :3");   // 3 možnosti
                labelOtázka.Visible = true;

                groupBoxOdpovědi.Text = "";
                groupBoxOdpovědi.Location = new Point(24, 45);
                groupBoxOdpovědi.Width = 150;
                groupBoxOdpovědi.Visible = true;

                Width = 250;
                TřiMožnosti();          // Určuje Location obou tlačítek

                this.Location = new Point(700, 200);    // Změnit, zkoušet (chceme prostředek obrazovky)

                if (Minute < 10)
                {
                    labelČas.Text = Hour.ToString() + ":0" + Minute.ToString();
                }
                else
                {
                    labelČas.Text = Hour.ToString() + ":" + Minute.ToString();
                }

                labelČas.Visible = true;

                Možnost1("Dobrou :3");
                Možnost2("Špatnou :|");
                Možnost3("Holo ♥");

                buttonOdpovězVisibility(true);
                buttonKonecVisibility(true);

                Odznačit();

                Form0 rozcestník = new Form0();       // Instance třídy Form0
                rozcestník.InstanceForm1 = this;
                this.Hide();
                rozcestník.Closed += (s, args) => this.Close();
                rozcestník.Show();
                rozcestník.Location = new Point(600, 200);

            }
        }


        public void buttonOdpovězVisibility(bool value)
        {
            buttonOdpověz.Visible = value;
        }

        // Click
        // Hlavní větvení (událost po zmáčknutí tlačítka Odpověz).
        private void buttonOdpověz_Click(object sender, EventArgs e)
        {
            // Upravuje Width prvků Form1 a groupBoxOdpovědi, mění Location prvku Form1 a upravuje vlastnost Visible všech komponent.
            pressed++;

            switch (pressed)
            {
// case 1:
                case 1:         // "Jakou jsi měla noc?"
                    if (radioButton1.Checked)   // "Dobrou :3": 4 možnosti
                    {
                        Reset();
                        if (pressed == 1)
                        {
                            Reakce("To jsem moc rád. :)");
                        }
                        else
                        {
                            pressed = 1;
                        }

                        this.Width = 250;
                        this.Visible = true;
                        ČtyřiMožnosti();
                        groupBoxOdpovědi.Width = 160;

                        Otázka("Co máš dneska v plánu? =)");


                        Možnost1("Budu s Lukem :3");
                        Možnost2("Budu s Kikou :D");
                        Možnost3("Mám školu");
                        Možnost4("Dnes mám volno =)");

                        // pressed = 1; // není nutné
                        // goto case 2;
                    }
                    else if (radioButton2.Checked)   // "Špatnou :|": 6 možností        
                    {
                        Reset();
                        if (pressed == 1)
                        {
                            Reakce("Oh, to je mi líto...");
                        }

                        this.Width = 466;
                        this.Visible = true;
                        ŠestMožností();
                        groupBoxOdpovědi.Width = 260;

                        Otázka("Vyber si jednu možnost a já se ti pokusím zlepšit náladu. ^^");

                        Možnost1("Řekni mi vtip :)");
                        radioButton2.Enabled = false;   // dočasně
                        Možnost2("Řekni mi vtipnou vzpomínku :D");
                        radioButton3.Enabled = false;   // dočasně
                        Možnost3("Řekni mi hezkou vzpomínku :3");
                        radioButton4.Enabled = false;   // dočasně
                        Možnost4("Dej mi hádanku :P");
                        radioButton5.Enabled = false;   // dočasně
                        Možnost5("Řekni mi nějaké herní zážitky =)");
                        Možnost6("Zeptej se mě na můj plán na dnešek :)");

                        pressed = 3;
                        // goto case 4;
                    }
                    else if (radioButton3.Checked)  // "Holo ♥": 3 možnosti
                    {
                        Reset();
                        Reakce("Awww. :3 Holo for life! :3 ♥");

                        this.Width = 280;
                        this.Visible = true;
                        TřiMožnosti();
                        groupBoxOdpovědi.Width = 200;

                        Otázka("Jaké je tvé holo přání? :3");

                        Možnost1("Chci ti napsat, co se mi zdálo :D");
                        Možnost2("Vyhledej mi holo ♥");
                        Možnost3("Napiš mi motivační citát :)");

                        pressed = 4;
                        // goto case 5;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 2:
                case 2:             // "Co máš dneska v plánu?"
                    // timer.Enabled = false;   ZRUŠENO
                    int náhoda;

                    if (radioButton1.Checked)   // "Budu s Lukem :3": End
                    {
                        Reset();
                        náhoda = random.Next(1, 5);
                        switch (náhoda)
                        {
                            case 1:
                                Rozloučení("Ahá. :D :3 V tom případě si to užijte a pozdravuj! :D ♥");
                                break;
                            case 2:
                                Rozloučení("Pozdravuj hooo! :D A užijte si společné chvilky. :*");
                                break;

                            case 3:
                                Rozloučení("Oh, to bych vás neměl zdržovat. ':D Mějte se hezky! :3 Papa. ♥");
                                break;

                            case 4:
                                Rozloučení("Jééj. :3 Tak ať se vám vydaří společný den. :D Bye! =) ♥");
                                break;
                        }
                    }
                    else if (radioButton2.Checked)  // "Budu s Kikou :D": End
                    {
                        Reset();
                        náhoda = random.Next(1, 5);
                        switch (náhoda)
                        {
                            case 1:
                                Rozloučení("Tak to nepřežeň se šňupáken. :P Pa. :3");
                                break;
                            case 2:
                                Rozloučení("Aha. :D Pozdravuj Kiku :) a užijte si den. ^^ Papa. :*");
                                break;

                            case 3:
                                Rozloučení("Ne že se přepiješ kofoly. :D :P Čauky mňauky! =) :D A pozdravuj. :D");
                                break;

                            case 4:
                                Rozloučení("Nezabij se. ':D xD Zatím ahoj. :3 ♥");
                                break;
                        }
                    }
                    else if (radioButton3.Checked)  // "Mám školu": End
                    {
                        Reset();
                        náhoda = random.Next(1, 5);
                        switch (náhoda)
                        {
                            case 1:
                                Rozloučení("Zabij ty svině! >D ... A hlavně to tam přežij. ^^ Budu tu na tebe čekat. :* Pa. ♥");
                                break;
                            case 2:
                                Rozloučení("Good day to have a good day. =) Zvládni to tam. ^^ Zatím ahoj. :3 :*");
                                break;

                            case 3:
                                Rozloučení("Simíci čekají. :D Have fun, darling :* a užij si to tam. :D Pa. ♥");
                                break;

                            case 4:
                                Rozloučení("Pfff, zase ti retardi. :/ Snad budou v pohodě dneska. A kdyby ne, víš, za kým zajít. ^^ Budu tu pro tebe, až se vrátíš. :* Gl lásko a buď silná. :D ♥ Pá. :3");
                                break;
                        }
                    }
                    else if (radioButton4.Checked)  // "Dnes mám volno =)": 6 možností
                    {
                        Reset();
                        Reakce("Haha. Že by další den plný chillu? :D Aby ses nenudila, tady jsou nějaké návrhy činností, co bys mohla dělat. Zkus si nějakou vybrat. :D");

                        this.Width = 333;
                        this.Visible = true;
                        ŠestMožností();
                        groupBoxOdpovědi.Width = 200;

                        Otázka("Vyber si činnost, kterou bys chtěla dělat:");

                        Možnost1("Streamovat");
                        Možnost2("Hrát na počítači");
                        Možnost3("Zpívat");
                        Možnost4("Jít ven");
                        Možnost5("Někam si zajet");
                        Možnost6("Ani jedno... Prostě chillovat :D");

                        // pressed = 2; // není nutné
                        // goto case 3;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 3:
                case 3:     // "Vyber si činnost, kterou bys chtěla dělat:"
                    if (radioButton1.Checked)       // "Streamovat": 3 možnosti
                    {
                        Reset();
                        Reakce("Dobrý nápad! :D Třeba si získáš pár dalších followerů. :D :3");

                        this.Width = 360;
                        this.Visible = true;
                        TřiMožnosti();
                        groupBoxOdpovědi.Width = 294;

                        Otázka("A jaký typ streamu by to byl? :D");

                        Možnost1("Gaming stream");
                        Možnost2("Singing stream");
                        Možnost3("Nějaký custom stream, kde budu dělat random věci :D");

                        pressed = 5;
                        // goto case 6;
                    }
                    else if (radioButton2.Checked)  // "Hrát na počítači": 6 možností
                    {
                        Reset();
                        if (pressed == 3)
                        {
                            Reakce("Skvělá volba! :D Myslím, že kdyby ses zeptala mě, odpověděl bych stejně. :D :D");

                            this.Width = 260;
                            groupBoxOdpovědi.Width = 170;

                            Otázka("A jakou hru budeš hrát? :D");
                        }
                        else if (pressed == 6)      // "Streamovat"
                        {
                            Reakce("Nice, to zní jako fun! :D :3");
                            
                            this.Width = 300;
                            groupBoxOdpovědi.Width = 140;

                            Otázka("A jakou hru budeš streamovat? :D");
                        }

                        this.Visible = true;
                        ŠestMožností();

                        Možnost1("The Sims 4");
                        Možnost2("Minecraft");
                        Možnost3("League of Legends");
                        Možnost4("Plants vs. Zombies");
                        Možnost5("Overwatch");
                        Možnost6("Nějakou jinou");

                        pressed = 6;
                        // goto case 7;
                    }
                    else if (radioButton3.Checked)  // "Zpívat": 4 možnosti
                    {
                        Reset();
                        Reakce("Jéé. :3 Škoda, že to neuslyším... ':D");

                        this.Width = 310;
                        this.Visible = true;
                        ČtyřiMožnosti();
                        groupBoxOdpovědi.Width = 180;

                        Otázka("A za jakým účelem budeš zpívat? :D");

                        Možnost1("Budu to streamovat");
                        Možnost2("Budu to nahrávat");
                        Možnost3("Chci složit novou písničku");
                        Možnost4("Jen tak :D");

                        pressed = 7;
                        // goto case 8;
                    }
                    else if (radioButton4.Checked)  // "Jít ven": 4 možnosti
                    {
                        Reset();
                        Reakce("Super, to je zdravé. :D Snad nezmokneš... ':D");

                        this.Width = 200;
                        this.Visible = true;
                        ČtyřiMožnosti();
                        groupBoxOdpovědi.Width = 120;

                        Otázka("A s kým? :3");

                        Možnost1("S Kikou ♥");
                        Možnost2("S Lukem :3");
                        Možnost3("S mojí partou B)");
                        Možnost4("S někým jiným :D");

                        pressed = 8;
                        // goto case 9;
                    }
                    else if (radioButton5.Checked)  // "Někam si zajet": 4 možnosti
                    {
                        Reset();
                        Reakce("Slyšel jsem dobrodružství? :D Hurá na výlet! :D :3");

                        this.Width = 240;
                        this.Visible = true;
                        ČtyřiMožnosti();
                        groupBoxOdpovědi.Width = 145;

                        Otázka("S kým pojedeš? :D");

                        Možnost1("S Lukem :3");
                        Možnost2("S kamarády ^^");
                        Možnost3("S babičkou a dědou :)");
                        Možnost4("Sama :D");

                        pressed = 9;
                        // goto case 10;
                    }
                    else if (radioButton6.Checked)  // "Prostě chillovat": End
                    {
                        Reset();
                        Rozloučení("Oh, okay. :D Chill day means good day. :3 Tak ti přeju, ať tě babička s dědou moc neotravují a pořádné se vychilluj. :D :D :* Měj se hezky, papa. :3 ♥");
                    }
                    else  // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 4:
                case 4:     // "Vyber si jednu možnost a já se ti pokusím zlepšit náladu. ^^"
                    if (radioButton1.Checked)   // "Řekni mi vtip :)": 3 možnosti (Chci slyšet další; už je mi líp; musím jít)
                    {
                        Reset();
                        if (pressed == 4)
                        {
                            Reakce("Dobře. :D A prosím nevyčítej mi to, pokud by to bylo akward, jsem pořád „jenom“ povídací program... :P :D");
                        }

                        int indexVtipu = random.Next(0, vtipy.Count);
                        labelVtip.Text = "Vtip:";
                        labelVtip.Visible = true;
                        textBoxVtip.Text = vtipy[indexVtipu];
                        textBoxVtip.Location = new Point(48, 27);
                        textBoxVtip.Width = 263;
                        textBoxVtip.Height = 124;
                        textBoxVtip.Visible = true;
                        vtipy.RemoveAt(indexVtipu);

                        this.Width = 350;
                        this.Visible = true;
                        TřiMožnosti();
                        labelOtázka.Location = new Point(labelOtázka.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 10);
                        this.Height += 110;
                        groupBoxOdpovědi.Width = 250;
                        groupBoxOdpovědi.Location = new Point(groupBoxOdpovědi.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 30);
                        buttonOdpověz.Location = new Point(buttonOdpověz.Location.X, groupBoxOdpovědi.Location.Y + groupBoxOdpovědi.Height + 10);
                        buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);
                        labelČas.Location = new Point(3, this.Height - 55);

                        Otázka("Tvé další přání? :D :3");

                        Možnost1("Chci slyšet další! :D");
                        Možnost2("Chci zkusit něco jiného z předchozí nabídky.");
                        Možnost3("Už asi půjdu... :)");

                        pressed = 10;
                        // goto case 11;
                    }
                    else if (radioButton2.Checked)  // "Řekni mi vtipnou vzpomínku :D": ? možností
                    {
                        Reset();
                        Reakce("");

                        // NUTNÉ DODĚLAT!!! (včetně seznamu)

                        // int indexVtipu = random.Next(0, vtipy.Count);
                        // textBoxVtip.Text = vtipy[indexVtipu];
                        textBoxVtip.Location = new Point(45, 27);   // upravit
                        textBoxVtip.Width = 263;    // upravit
                        textBoxVtip.Height = 124;   // upravit
                        textBoxVtip.Visible = true;
                        labelVtip.Text = "Vtipná vzpomínka:";
                        labelVtip.Visible = true;
                        // vtipy.RemoveAt(indexVtipu);

                        this.Width = 350;
                        this.Visible = true;
                        TřiMožnosti();
                        labelOtázka.Location = new Point(labelOtázka.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 10);
                        groupBoxOdpovědi.Location = new Point(groupBoxOdpovědi.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 30);
                        this.Height += 110;
                        groupBoxOdpovědi.Width = 250;
                        buttonOdpověz.Location = new Point(buttonOdpověz.Location.X, groupBoxOdpovědi.Location.Y + groupBoxOdpovědi.Height + 10);
                        buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);
                        labelČas.Location = new Point(3, this.Height - 55);

                        Otázka("Co bys chtěla dělat dál? :D");

                        Možnost1("Řekni mi ještě nějakou. :D :D");
                        radioButton1.Enabled = false;   // dočasně
                        Možnost2("Chci zkusit něco jiného z předchozí nabídky.");
                        radioButton2.Enabled = false;   // dočasně
                        Možnost3("Už asi půjdu... :)");
                        radioButton3.Enabled = false;   // dočasně

                        pressed = 11;
                        // goto case 12;
                    }
                    else if (radioButton3.Checked)  // "Řekni mi hezkou vzpomínku :3": ? možností
                    {
                        Reset();
                        Reakce("");

                        // NUTNÉ DODĚLAT!!! (včetně seznamu)

                        this.Visible = true;
                    }
                    else if (radioButton4.Checked)  // "Dej mi hádanku :P": ? možností
                    {
                        Reset();
                        Reakce("");

                        // NUTNÉ DODĚLAT!!! (včetně seznamu)

                        this.Visible = true;
                    }
                    else if (radioButton5.Checked)  // "Řekni mi nějaké herní zážitky =)": ? možností
                    {
                        Reset();
                        Reakce("");

                        // NUTNÉ DODĚLAT!!! (včetně seznamu)

                        this.Visible = true;
                    }
                    else if (radioButton6.Checked)  // "Zeptej se mě na můj plán na dnešek :)": case 1, rB1
                    {
                        Reset();
                        Reakce("Dobře. :)");

                        radioButton6.Checked = false;
                        radioButton1.Checked = true;
                        goto case 1;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 5:
                case 5:     // "Jaké je tvé holo přání? :3"
                    if (radioButton1.Checked)   // "Chci ti napsat, co se mi zdálo :D": 2 možnosti (ještě X to je vše)
                    {
                        Reset();

                        if (pressed == 5)
                        {
                            Reakce("Óóó, jsem poctěn. :D :3 Tak sem s ním. :D ♥");
                        }

                        labelVtip.Text = "Tvůj sen:";
                        labelVtip.Location = new Point(22, 30);
                        labelVtip.Visible = true;
                        textBoxVtip.Text = "";
                        textBoxVtip.Location = new Point(90, 27);
                        textBoxVtip.Width = 220;
                        textBoxVtip.Height = 124;
                        textBoxVtip.Visible = true;
                        textBoxVtip.ReadOnly = false;

                        this.Width = 350;
                        this.Visible = true;
                        DvěMožnosti();
                        labelOtázka.Location = new Point(labelOtázka.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 10);
                        this.Height += 110;
                        groupBoxOdpovědi.Width = 200;
                        groupBoxOdpovědi.Location = new Point(groupBoxOdpovědi.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 30);
                        buttonOdpověz.Location = new Point(buttonOdpověz.Location.X, groupBoxOdpovědi.Location.Y + groupBoxOdpovědi.Height + 10);
                        buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);
                        labelČas.Location = new Point(3, this.Height - 55);

                        textBoxCesta.Location = new Point(7, 85);
                        textBoxCesta.Visible = true;
                        buttonProcházet.Location = new Point(7, 50);
                        buttonProcházet.Visible = true;
                        buttonUložit.Location = new Point(7, 110);
                        buttonUložit.Visible = true;

                        Otázka("Ještě nějaký sen mi chceš napsat? :3");

                        Možnost1("Jo, ještě jeden :D");
                        Možnost2("Ne, to je vše :)");

                        pressed = 12;
                        // goto case 13
                    }
                    else if (radioButton2.Checked)  // "Vyhledej mi holo ♥": End
                    {
                        Reset();
                        System.Diagnostics.Process.Start("https://www.google.cz/search?hl=cs&biw=1680&bih=944&tbm=isch&sa=1&ei=nqoaW8e_B8PB6ATJvoXAAQ&q=holographic&oq=Holographi&gs_l=img.3.0.35i39k1j0l9.1057.3455.0.3972.6.6.0.0.0.0.109.587.4j2.6.0....0...1c.1.64.img..0.6.584...0i10k1j0i67k1.0.MC3Dbr9enLk");
                        Rozloučení("Je libo holo, bude holo! :D ♥ Snad se ti líbí (může to chvilku trvat). ':D Já už půjdu. ^^ Měj se hezky, ahojky. :* :3");
                    }
                    else if (radioButton3.Checked)  // "Napiš mi motivační citát :)": 2 možnosti (ještě X už půjdu)
                    {
                        Reset();
                        if (pressed == 5)
                        {
                            Reakce("A jde se motivovat! :D :*");
                        }

                        int indexCitátu = random.Next(0, motivačníCitáty.Count);
                        labelVtip.Text = "Motivační citát:";
                        labelVtip.Visible = true;
                        textBoxVtip.Text = motivačníCitáty[indexCitátu];
                        textBoxVtip.Location = new Point(118, 27);
                        textBoxVtip.Width = 200;
                        textBoxVtip.Height = 124;
                        textBoxVtip.Visible = true;
                        motivačníCitáty.RemoveAt(indexCitátu);

                        this.Width = 350;
                        this.Visible = true;
                        DvěMožnosti();
                        labelOtázka.Location = new Point(labelOtázka.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 10);
                        groupBoxOdpovědi.Location = new Point(groupBoxOdpovědi.Location.X, textBoxVtip.Location.Y + textBoxVtip.Height + 30);
                        groupBoxOdpovědi.Width = 150;
                        this.Height += 110;
                        groupBoxOdpovědi.Width = 250;
                        buttonOdpověz.Location = new Point(buttonOdpověz.Location.X, groupBoxOdpovědi.Location.Y + groupBoxOdpovědi.Height + 10);
                        buttonKonec.Location = new Point(this.Width - buttonKonec.Width - 20, this.Height - buttonKonec.Height - 40);
                        labelČas.Location = new Point(3, this.Height - 55);

                        Otázka("Motivoval jsem tě dostatečně? :D");

                        Možnost1("Ještě jeden citát, prosím. =)");
                        Možnost2("Jojo, už půjdu. :D :)");

                        pressed = 11;
                        // goto case 12;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 6:
                case 6:     // "A jaký typ streamu to bude? :D"
                    if (radioButton1.Checked)   // "Gaming stream": case 3, rB2
                    {
                        // Všechno potřebné je přímo v case 3, rB2 jako větev else if (pressed == 6).

                        radioButton1.Checked = false;
                        radioButton2.Checked = true;
                        goto case 3;
                    }
                    else if (radioButton2.Checked)  // "Singing stream": case 8, rB1
                    {
                        radioButton2.Checked = false;
                        radioButton1.Checked = true;
                        goto case 8;
                    }
                    else if (radioButton3.Checked)  // "Nějaký custom stream, kde budu dělat random věci :D": End
                    {
                        Reset();
                        Rozloučení("Áha. :D Tak hlavně ať tě to baví a snad ti tam přijde hodně příjemných diváků. :D ^^ :* A třeba padne i nějaký ten sub nebo donate. :D :3 Papa. ♥ =)");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }                  
                    break;
// case 7:
                case 7:     // "A jakou hru budeš hrát? :D" && "A jakou hru budeš streamovat? :D"
                    if (radioButton1.Checked) // "The Sims 4": End
                    {
                        Reset();
                        Rozloučení("Simíci!!! :3 Ti vždycky potěší. :D Užij si je, ahojky. :3 ♥");
                    }
                    else if (radioButton2.Checked)  // "Minecraft": End
                    {
                        Reset();
                        Rozloučení("Kostičky! :D Minecraft nikdy nepřestane být sranda... :D :3ˇUžij si hru, papa. :* ♥");
                    }
                    else if (radioButton3.Checked)  // "League of Legends": End
                    {
                        Reset();
                        Rozloučení("„Máte hlad?“ „Lížu kredenc!“ :D :D Nenech se moc vytiltit, není to zdravé. :* Hezkou, klidnou hru, lásko. :3 Papa. ♥ :* =)");
                    }
                    else if (radioButton4.Checked)  // "Plants vs. Zombies": End
                    {
                        Reset();
                        Rozloučení("Staré dobré Plants vs. Zombies! :D No jo no, na staré hry by se nemělo zapomínat, dokážou většinou pobavit stejně dobře jako ty nové. :D :3 A ta nostalgie... :3 No nic, užij si hru. :D ^^ Čauky. :* ♥");
                    }
                    else if (radioButton5.Checked)   // "Overwatch": End
                    {
                        Reset();
                        Rozloučení("Ale, ale... :D Tady má už někdo Overwatch, jo? :D Mám se ti podívat do souborů, jestli mě jenom nešmelíš? :D Radši ne... :P ':D ♥ Gl ve hře a have fun. :D :3 Papa, zlato. :* ♥ :D :3");
                    }
                    else if (radioButton6.Checked)   // "Nějakou jinou": 4 možnosti
                    {
                        Reset();
                        Reakce("Áha... ':D Zajímavé. :D Třeba trefím aspoň okruh her. :P :D");

                        this.Width = 260;
                        this.Visible = true;
                        ČtyřiMožnosti();
                        groupBoxOdpovědi.Width = 205;

                        Otázka("Odkud ta hra je? :D");

                        Možnost1("Z Mateřídoušky");
                        Možnost2("Od Špidly");
                        Možnost3("Z internetu");
                        Možnost4("Naprogramoval jsi ji :D");

                        pressed = 13;
                        // goto case 14;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }                      
                    break;
// case 8:
                case 8:     // "A za jakým účelem budeš zpívat? :D"
                    if (radioButton1.Checked)   // "Budu to streamovat": 4 možnosti
                    {
                        Reset();
                        Reakce("Jo takto. :D Holofollower rohozka's singing stream incoming! :3 Už se těším! :D :3");

                        this.Width = 270;
                        this.Visible = true;
                        ČtyřiMožnosti();
                        groupBoxOdpovědi.Width = 210;

                        Otázka("A co budeš zpívat? :3 :D");

                        Možnost1("Moje oblíbené písničky :3");
                        radioButton1.Enabled = false;   // dočasně
                        Možnost2("Písničky na přání :)");
                        radioButton2.Enabled = false;   // dočasně
                        Možnost3("Co mě napadne :D");
                        radioButton3.Enabled = false;   // dočasně
                        Možnost4("Chci ti napsat názvy těch písniček :D");
                        radioButton4.Enabled = false;   // dočasně

                        pressed = 14;
                        // goto case 15;
                    }
                    else if (radioButton2.Checked)  // "Budu to nahrávat": End
                    {
                        Reset();
                        Rozloučení("Ohh. :D  To zní jako plán! :D Tak ať se ti to podaří, brouku. :* :3 A měj se hezkyyy! :D :* Pá. ♥ :3 :D");
                    }
                    else if (radioButton3.Checked)  // "Chci složit novou písničku": 
                    {
                        Reset();
                        Reakce("Wow! Snad se ti to povede. :D :3");

                        this.Width = 280;
                        this.Visible = true;
                        ŠestMožností();
                        groupBoxOdpovědi.Width = 140;

                        Otázka("A o čem ta písnička bude? :D :3");

                        Možnost1("O lásce");
                        radioButton1.Enabled = false;   // dočasně
                        Možnost2("O škole");
                        radioButton2.Enabled = false;   // dočasně
                        Možnost3("O přírodě");
                        radioButton3.Enabled = false;   // dočasně
                        Možnost4("Tak random");
                        radioButton4.Enabled = false;   // dočasně
                        Možnost5("Ještě nevím :D");
                        radioButton5.Enabled = false;   // dočasně
                        Možnost6("Napíšu ti to :D ^^");
                        radioButton6.Enabled = false;   // dočasně

                        pressed = 15;
                        // goto case 16;
                    }
                    else if (radioButton4.Checked)  // "Jen tak :D": End
                    {
                        Reset();
                        Rozloučení("Aha, oki. :D Tak chytej správné tóniny. :D :3 A kdyby náhodou sis nebyla jistá, tak se zeptej Luka, on zpěvu rozumí. ;) :D :D Have fun. ^^ :3 Papa, kotě. :* :3 ♥");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 9:
                case 9:     // "A s kým (půjdeš ven)? :3 :D"
                    if (radioButton1.Checked)   // "S Kikou ♥": End
                    {
                        Reset();
                        Rozloučení("Yay. ♥ Bohuslavická dvojka v akci! :D :3 Tak si to užijte. ^^ A nepřežeň to se šňupákem. :P :D Ahojky. :D :3");
                    }
                    else if (radioButton2.Checked)  // "S Lukem :3": End
                    {
                        Reset();
                        Rozloučení("Awww. :3 ♥ Užijte si to a nezmokněte. :D :* :3 Papa. ♥ :* :3");
                    }
                    else if (radioButton3.Checked)  // "S mojí partou B)": End
                    {
                        Reset();
                        Rozloučení("Jo vlastně, slečna je boss. :D Bav se, brouku. :D :3 Čauky. :* ♥");
                    }
                    else if (radioButton4.Checked)  // "S někým jiným :D": End
                    {
                        Reset();
                        Rozloučení("Áha. o.O :D Tak ať jdeš ven s kýmkoliv, tak se tam měj hezky. ^^ :D Ahoooj, lásko. :* :3 ♥");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 10:
                case 10:    // "S kým pojedeš? :D"
                    if (radioButton1.Checked)   // "S Lukem :3": 6 možností
                    {
                        Reset();
                        Reakce("Yay! :D :3");

                        this.Width = 200;
                        this.Visible = true;
                        ŠestMožností();
                        groupBoxOdpovědi.Width = 120;

                        Otázka("A kam? :D :3");

                        Možnost1("Na výlet");
                        Možnost2("Na párty");
                        Možnost3("Do kina");
                        Možnost4("Do divadla");
                        Možnost5("Do čajky");
                        Možnost6("Ještě nevím :D");

                        pressed = 19;
                        // goto case 20;
                    }
                    else if (radioButton2.Checked)  // "S kamarády ^^": 3 možnosti
                    {
                        Reset();
                        Reakce("Juchů! :D");

                        this.Width = 205;
                        this.Visible = true;
                        TřiMožnosti();
                        groupBoxOdpovědi.Width = 120;

                        Otázka("A co budete dělat? :D");

                        Možnost1("Pařit :D");
                        Možnost2("Hrát deskovky :3");
                        Možnost3("Ještě nevím :D");

                        pressed = 20;
                        // goto case 21;
                    }
                    else if (radioButton3.Checked)  // "S babičkou a dědou :)": 3 možnosti (do obchodu, na výlet, ještě nevím)
                    {
                        Reset();
                        Reakce("Jo tak rodinná akce, jo? :D");

                        this.Width = 200;
                        this.Visible = true;
                        TřiMožnosti();
                        groupBoxOdpovědi.Width = 130;

                        Otázka("A kam? :3");

                        Možnost1("Na výlet");
                        Možnost2("Do obchodu");
                        Možnost3("Ještě nevím :D");

                        pressed = 21;
                        // goto case 22;
                    }
                    else if (radioButton4.Checked)  // "Sama :D": 5 možností (na výlet, do Brna za kamarády, do Hájků, za taťkou, ještě nevím)
                    {
                        Reset();
                        Reakce("Já udělám si to sama, co? :D Tak dobře dojeď. :D :3");

                        this.Width = 215;
                        this.Visible = true;
                        PětMožností();
                        groupBoxOdpovědi.Width = 150;

                        Otázka("A kam vlastně? :o :D");

                        Možnost1("Někam na výlet :D");
                        Možnost2("Do Brna za kamarády");
                        Možnost3("Za taťkou :3");
                        Možnost4("Do Hájků :D");
                        Možnost5("Ještě nevím xD");

                        pressed = 22;
                        // goto case 23;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 11:
                case 11:    // "Tvé další přání? (po řeknutí vtipu) :D :3“
                    if (radioButton1.Checked)   // "Chci slyšet další! :D": case 4, rB1
                    {
                        Reset();
                        Reakce("Dobrá! Tady je. :D");
                        goto case 4;
                    }
                    else if (radioButton2.Checked) // "Chci zkusit něco jiného z předchozí nabídky.": case 1, rB2
                    {
                        Reset();
                        Reakce("Prosím, jak je libo. :)");
                        goto case 1;
                    }
                    else if (radioButton3.Checked)  // "Už asi půjdu... :)": End
                    {
                        Reset();
                        Rozloučení("Dobrá. V tom případě se měj hezky a papa. :3 ♥");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 12:
                case 12:    // "Motivoval jsem tě dostatečně? :D"
                    if (radioButton1.Checked)   // "Ještě jeden citát, prosím. =)": case 5, rB1
                    {
                        Reset();
                        Reakce("Dobře, tady je. :D");
                        radioButton1.Checked = false;
                        radioButton3.Checked = true;
                        goto case 5;
                    }
                    else if (radioButton2.Checked)  // "Jojo, už půjdu. :D :)": End
                    {
                        Reset();
                        Rozloučení("Okay. :) Snad jsem tě trochu motivoval. ':D Papa, zlati. :* ♥ =)");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;

                default:        // Pouze na testování (pokud vybraná možnost ještě nemá napsaný kód, aplikace se vypne)
                    MessageBox.Show("Promiň, ale na tuto možnost ještě neumím odpovědět. V každém případě se měj hezky, já se musím vypnout, abych se nepokazil. Papa. ♥", "Nenaprogramovaná možnost", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                    break;
// case 13:
                case 13:    // "Ještě nějaký sen mi chceš napsat? :3"
                    if (radioButton1.Checked)   // "Jo, ještě jeden :D": case 5, rB1
                    {
                        if (savedDream)
                        {
                            Reakce("Super! :D Čím víc, tím líp. :D :3");
                            savedDream = false;
                            goto case 5;
                        }
                        else
                        {
                            MessageBox.Show("Žádný sen jsi zatím neuložila. :|", "Upozornění", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            pressed--;
                        }
                    }
                    else if (radioButton2.Checked)  // "Ne, díky, to je vše :)": End
                    {
                        if (savedDream)
                        {
                            Rozloučení("Dobrá, měj se hezky. ^^ V budoucnu se těším na další tvé sny. :D :3 Papa. :* ♥");
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("Žádný sen jsi zatím neuložila. :| Jsi si jistá, že chceš program ukončit?", "Ujištění", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                Rozloučení("Dobrá, měj se hezky. ^^ V budoucnu se těším na další tvé sny. :D :3 Papa. :* ♥");
                            }
                            else
                            {
                                pressed--;
                            }
                        }                      
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 14:
                case 14:    // "Odkud ta hra je? :D"
                    if (radioButton1.Checked)   // "Z Mateřídoušky": End
                    {
                        Reset();
                        Rozloučení("Staré dobré hry z Mateřídoušky... :D :3 Dortíky, Bar,... Aww. ♥ :D :3 Have fun, zlati. ^^ :D :3 Papa. :* ♥ :3");
                    }
                    else if (radioButton2.Checked)  // "Od Špidly (půjčené od Luka)": End
                    {
                        Reset();
                        Rozloučení("Alé. :D :3 Medvěd Míša? Či Dobrý farmář? :D :3 Všechny jsou great! :D Užij si to! :D ♥ :3 Ahojky. :* :3 =)");
                    }
                    else if (radioButton3.Checked)  // "Z internetu": End
                    {
                        Reset();
                        Rozloučení("To se ještě v dnešní době dělá? o.O xD V každém případě se bav, Šáry. :D :3 A hodně štěstí. :D Čauky. :3 :D :* ♥");
                    }
                    else if (radioButton4.Checked)  // "Naprogramoval jsi ji :D": 2 možnosti
                    {
                        Reset();
                        Reakce("Ahaaa. :D :3 Tak to potěší. :* ♥");

                        this.Width = 250;   // ?? (pro 3 otestováno)
                        this.Visible = true;
                        TřiMožnosti();
                        groupBoxOdpovědi.Width = 150;   // ?? (pro 3 otestováno)

                        Otázka("Kterou hru si vybereš? :D :3");

                        Možnost1("Snyanke cat");
                        Možnost2("Hledání fekálií");
                        radioButton2.Enabled = false;   // dočasně
                        Možnost3("Neposedná tlačítka");

                        pressed = 16;
                        // goto case 17;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 15:
                case 15:    // "A co budeš zpívat? :3 :D"
                    if (radioButton1.Checked)   // "Moje oblíbené písničky :3": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton2.Checked)  //"Písničky na přání :)": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton3.Checked)  //"Co mě napadne :D": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton4.Checked)  //"Chci ti napsat názvy těch písniček :D": 2 možnosti (textBox, End)
                    {
                        Reset();
                        if (pressed == 15)
                        {
                            Reakce("");
                        }

                        this.Width = 300;   // ??
                        this.Visible = true;
                        DvěMožnosti();
                        groupBoxOdpovědi.Width = 200;   // ??

                        Otázka("Ještě nějaká? :D");

                        Možnost1("Jo, ještě jedna :D");
                        Možnost2("Nene, to je všechno :D");

                        pressed = 17;
                        // goto case 18;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 16:
                case 16:    // "A o čem ta písnička bude? :D :3"
                    if (radioButton1.Checked)   // "O lásce": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton2.Checked)  // "O škole": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton3.Checked)  // "O přírodě": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton4.Checked)  // "Tak random": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton5.Checked)  // "Ještě nevím :D": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else if (radioButton6.Checked)  // "Napíšu ti to :D ^^": End, textBox
                    {
                        Reset();
                        Reakce("Oki. :D :3 Můžeš to napsat sem do textBoxu. :D :3");

                        this.Width = 300; // ??
                        this.Visible = true;
                        // DvěMožnosti(); ZRUŠENO
                        // groupBoxOdpovědi.Width = 200; // ?? ZRUŠENO

                        // missing textBox
                        // missing label

                        pressed = 18;
                        // goto case 19;
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 17:
                case 17:    // "Kterou hru si vybereš? :D :3"
                    if (radioButton1.Checked)   // "Snyanke cat": „Form2.Show()“;
                    {
                        this.Hide();
                        timerČas.Enabled = false;
                        Reakce("Super! :D Přeji příjemnou holo hru. =) ♥ (Hra je zatím rozpracovaná a není dokončená!)");

                        Form2Úvod snakeÚvod = new Form2Úvod();      // Instance třídy Form2Úvod
                        snakeÚvod.Closed += (s, args) => this.Close();      // Nutné pro vypnutí celého programu po vypnutí Form2Úvod.
                        snakeÚvod.Show();
                        // Play, end.
                    }
                    else if (radioButton2.Checked)  // "Hledání fekálií": Form3.Show();
                    {
                        this.Hide();
                        timerČas.Enabled = false;
                        Reakce("V tom případě přeju hodně štěstí při vyhýbání se průjmu... :D :P xD");

                        
                        Form3 miny = new Form3();          // Instance třídy Form3                     
                        miny.Closed += (s, args) => this.Close();       // Nutné pro vypnutí celého programu po vypnutí Form3.
                        miny.Show();
                        // Play, end.
                    }
                    else if (radioButton3.Checked)  // "Neposedná tlačítka": Form4.Show();
                    {
                        this.Hide();
                        timerČas.Enabled = false;
                        Reakce("Haha! :D Ale opatrně, s tlačítkama si není rando zahrávat. ':D :* ♥ Have fun. :D :3 =)");

                        Form4 tlačítka = new Form4();       // Instance třídy Form4
                        tlačítka.Closed += (s, args) => this.Close();     // Nutné pro vypnutí celého programu po vypnutí Form4.
                        tlačítka.Show();
                        // Play end
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 18:
                case 18:    // "Ještě nějaká? :D" (píše názvy písniček)
                    if (radioButton1.Checked)   // "Jo, ještě jedna :D": case 15, rB4
                    {
                        Reakce("Dobře, sem s ní! :D :3");

                        radioButton1.Checked = false;
                        radioButton4.Checked = true;
                        goto case 15;
                    }
                    else if (radioButton2.Checked)  // "Nene, to je všechno :D": End
                    {
                        Reset();
                        Rozloučení("");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 19:
                case 19:    // Napsala, o čem je písnička, kterou skládá, do textBoxu: End
                    Reset();
                    Rozloučení(""); // Zahrnout to, co napasala. (Ukázat, že už to vím. :D) Pak to program zapomene.
                    break;
// case 20:
                case 20:    // "A kam? :D :3" (s Lukem)
                    if (radioButton1.Checked)   // "Na výlet": End
                    {
                        Reset();
                        Rozloučení("Aha. :D :3 V tom případě si ho užijte. :3 ♥ Papa. :* :3 =)");
                    }
                    else if (radioButton2.Checked)  // "Na párty": End
                    {
                        Reset();
                        Rozloučení("Jéj! :D Tak to nepřežeň se šňupákem a pij s mírou (šak ty víš). :* Užijte si to! :D Ahoj! :* :3 ♥");
                    }
                    else if (radioButton3.Checked)  // "Do kina": End
                    {
                        Reset();
                        Rozloučení("V tom případě si užijte film! :D :3 Měj se, zlato. :3 Ahoj. =) :* ♥");
                    }
                    else if (radioButton4.Checked)  // "Do divadla": End
                    {
                        Reset();
                        Rozloučení("Áha. :o :D Tady někdo bude mít příjemný večer. :3 Ať se vám líbí představení. ^^ :* Ahojky, zlati. ^^ ♥ :3");
                    }
                    else if (radioButton5.Checked)  // "Do čajky": End
                    {
                        Reset();
                        Rozloučení("Cože? :D Tys tam Luka dostala? o.O :D Tak opatrně s dýmkou. :D Užijte si to tam. ^^ :D Čauky. :* :3 ♥");
                    }
                    else if (radioButton6.Checked)  // "Ještě nevím :D": End
                    {
                        Reset();
                        Rozloučení("Tak to bude překvapení. :D :D V každém případě si to tam užijte. :D :3 Měj se. ♥ Papa. :D :* :3");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 21:
                case 21:    // "A co budete dělat? :D" (s kamarády)
                    if (radioButton1.Checked)   // "Pařit :D": End
                    {
                        Reset();
                        Rozloučení("Tak to nepřežeň ani s alkoholem, ani se šňupákem. :P :D Ale šak ty víš... :D Užij si to. :D :3 :* Pá. ♥ :3");
                    }
                    else if (radioButton2.Checked)  // "Hrát deskovky :3": End
                    {
                        Reset();
                        Rozloučení("Oh! To zní fajně! :D :3 Určitě to bude sranda. :D Hodně štěstí a ať vyhraješ. :D :* Have fun, brouku. ^^ Papa. :* ♥");
                    }
                    else if (radioButton3.Checked)  // "Ještě nevím :D": End
                    {
                        Reset();
                        Rozloučení("Nevadí. :D Tak hlavně ať se ti to líbí. :D ^^ Měj se hezky. :3 :* Papa. :3 ♥ :*");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 22:
                case 22:    // "A kam? :3" (s babičkou a dědou)
                    if (radioButton1.Checked)   // "Na výlet": End
                    {
                        Reset();
                        Rozloučení("To by mohla být sranda. :D Užij si to, zlato. :3 ^^ Měj se hezky :3 :* a pá! ♥ :3 :*");
                    }
                    else if (radioButton2.Checked)  // "Do obchodu": End
                    {
                        Reset();
                        Rozloučení("Jo takhle! :D Tak moc neutraťte a ať se vám podaří koupit všechno, pro co jedete. :D Papá! :* :3 ♥");
                    }
                    else if (radioButton3.Checked)  // "Ještě nevím :D": End
                    {
                        Reset();
                        Rozloučení("Třeba tě chtějí překvapit. xD V každém případě si to užij a dobře dojeďte. :* :3 Měj se, lásko. :3 Ahooj. :* ♥ :3");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
// case 23:
                case 23:    // "A kam vlastně? :o :D" (sama)
                    if (radioButton1.Checked)   // "Někam na výlet :D": End
                    {
                        Reset();
                        Rozloučení("A nebudeš se tam sama nudit? o.O :D No, v každém případě ti přeju šťastnou cestu a ať se ti tam líbí. :D :3 Ahojky. :* ♥");
                    }
                    else if (radioButton2.Checked)  // "Do Brna za kamarády": End
                    {
                        Reset();
                        Rozloučení("Jéj. :D :3 Tak si to tam užij a nezabij se. :D Papa. :3 ♥ :*");
                    }
                    else if (radioButton3.Checked)  // "Za taťkou :3": End
                    {
                        Reset();
                        Rozloučení("Yay! :3 Tak ho pozdravuj a užij si to tam! :D :3 Čauky! :D :* :3");
                    }
                    else if (radioButton4.Checked)  // "Do Hájků :D": End
                    {
                        Reset();
                        Rozloučení("Oh. :o Tak ať ti jde práce hezky od ruky ^^ a nezabij se tam. xD Měj se. :D :* :3 Pá! :* =) ♥");
                    }
                    else if (radioButton5.Checked)  // "Ještě nevím xD": End
                    {
                        Reset();
                        Rozloučení("Lol. xD I tak přeju šťastnou cestu (až se rozmyslíš :D). :* A užij si to tam. :D ^^ Papa, kotě. : :3 ♥");
                    }
                    else   // Chyba.
                    {
                        Chyba();
                    }
                    break;
            }
            Odznačit();
        }
    }
}
/*
 * TO DO LIST: 
 *              opravit něco? IDK co to je (z minulé verze)
 *              UŽ NEDĚLAT: zničit label s časem, timerČas; vyplivnout čas jenom jednou (hned na začátku dobré nálady)
 *              
 *              Uplatnit:
 *              Console.Beep(int Hz, int ms);
 *              System.Media.SystemSounds.Beep.Play();
 *              System.Media.SystemSounds.Asterisk.Play();
 *              System.Media.SystemSounds.Exclamation.Play();
 *              System.Media.SystemSounds.Question.Play();
 *              System.Media.SystemSounds.Hand.Play();
 */              
