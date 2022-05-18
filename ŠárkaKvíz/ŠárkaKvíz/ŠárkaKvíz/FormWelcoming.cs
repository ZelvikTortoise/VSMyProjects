using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ŠárkaKvíz
{
    public partial class FormWelcoming : Form
    {
        public FormWelcoming()
        {
            InitializeComponent();
            // FormWelcoming -> FormDividing.
            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormWelcoming_FormClosed);
        }

        // FormWelcoming (closed) -> FormDividing.
        private void FormWelcoming_FormClosed(object sender, FormClosedEventArgs e)
        {
            (new FormDividing()).Show();
        }

        // Instance třídy Random:
        public static Random random = new Random();
        
        // Pomocné proměnné:
        public static int formCode = 0;     // Welcoming – 0, Dividing – 1, Mix – 2, Osobnosti – 3, Určitá osobnost – 4

        //public static bool quizFirstTime = true;  // bugged
        public static bool returnedOnce = false;
        /* PRAVDĚPODOBNĚ NEPOUŽIJEME (SMAZAT?):
        public static bool shareFirstTime = true;
        public static bool jennyFirstTime = true;
        public static bool jaykieFirstTime = true;
        public static bool julietFirstTime = true;
        public static bool bellFirstTime = true;
        public static bool bowFirstTime = true;
        public static bool emilieFirstTime = true;
        public static bool amberFirstTime = true;
        */

        // Seznamy osobností:
        public static List<Osobnost> seznamOsobnost = new List<Osobnost>();
        public static List<Osobnost> seznamOsobnostCorrect = new List<Osobnost>();
        public static List<Osobnost> seznamOsobnostWrong = new List<Osobnost>();

        // Seznamy věcí:
        public static List<Věc> seznamVěcPůvodní = new List<Věc>();
        public static List<Věc> seznamVěcNový = new List<Věc>();
        public static List<Věc> seznamCorrectVěc = new List<Věc>();
        public static List<Věc> seznamWrongVěc = new List<Věc>();

        // Skóre:
        public static int skóre = 0;
        public static double správných = 0.00;
        public static double špatných = 0.00;
        // Odměny a tresty za správné a špatné odpovědi:
        public const int přidat = 100;
        public const int odebrat = 50;

        // Creating – Osobnosti:
        public static Osobnost share = new Osobnost();
        public static Osobnost jenny = new Osobnost();
        public static Osobnost jaykie = new Osobnost();
        public static Osobnost juliet = new Osobnost();
        public static Osobnost bell = new Osobnost();
        public static Osobnost bow = new Osobnost();
        public static Osobnost emilie = new Osobnost();
        public static Osobnost amber = new Osobnost();
        public static Osobnost claire = new Osobnost();

        // !!!Creating – Věci!!!
        public static Věc cigarety = new Věc();
        public static Věc knížky = new Věc();
        public static Věc minecraft = new Věc();
        public static Věc panda = new Věc();
        public static Věc brýleNe = new Věc();
        public static Věc hauština = new Věc();
        public static Věc kůň = new Věc();
        public static Věc lama = new Věc();
        public static Věc posilováńí = new Věc();
        public static Věc budík = new Věc();
        public static Věc lakNaNehty = new Věc();
        public static Věc šaty = new Věc();
        public static Věc čajíkOPáté = new Věc();
        public static Věc béžováBarva = new Věc();
        public static Věc snapback = new Věc();
        public static Věc síťovanéOblečení = new Věc();
        public static Věc šňupák = new Věc();
        public static Věc statek = new Věc();
        public static Věc bro = new Věc();
        public static Věc plyšáci = new Věc();
        public static Věc práce = new Věc();
        public static Věc úklid = new Věc();
        public static Věc lenost = new Věc();
        public static Věc abstinent = new Věc();
        public static Věc simíci = new Věc();
        public static Věc ochráncePřírody = new Věc();
        public static Věc meloun = new Věc();
        public static Věc bílá = new Věc();
        public static Věc pinkiePie = new Věc();
        public static Věc hudba = new Věc();
        public static Věc černáBarva = new Věc();
        public static Věc fialováBarva = new Věc();
        public static Věc růžováBarva = new Věc();
        public static Věc pennyboard = new Věc();
        public static Věc longboard = new Věc();
        public static Věc plavání = new Věc();
        public static Věc databázeMax = new Věc();
        public static Věc cimbálka = new Věc();
        public static Věc zpěvNe = new Věc();
        public static Věc khakiBarva = new Věc();
        public static Věc asociál = new Věc();
        public static Věc asexuál = new Věc();
        public static Věc homosexuál = new Věc();
        public static Věc bisexuál = new Věc();
        public static Věc smutek = new Věc();
        public static Věc levandule = new Věc();
        public static Věc kočky = new Věc();
        public static Věc bubblebum = new Věc();
        public static Věc mléko = new Věc();
        public static Věc tiger = new Věc();
        public static Věc holo = new Věc();
        public static Věc deník = new Věc();
        public static Věc věšeníZaNohu = new Věc();
        public static Věc citátPičovina = new Věc();
        public static Věc citátSobeckáMrdka = new Věc();
        public static Věc auto = new Věc();
        public static Věc buldozer = new Věc();
        // ...

        private void FormWelcoming_Shown(object sender, EventArgs e)
        {
            // Čekání (2 sekundy):
            Refresh();
            System.Threading.Thread.Sleep(2000);

            // Setting – Osobnosti:
            // Share:
            share.Name = "Share";
            share.Result = false;
            share.Image = Properties.Resources.Share_new;
            share.Color = System.Drawing.Color.DarkRed;
            // Jenny:
            jenny.Name = "Jenny";
            jenny.Result = false;
            jenny.Image = Properties.Resources.Jenny_new;
            jenny.Color = System.Drawing.Color.DarkRed;
            // Jaykie:
            jaykie.Name = "Jaykie";
            jaykie.Result = false;
            jaykie.Image = Properties.Resources.Jaykie_new;
            jaykie.Color = System.Drawing.Color.Indigo;
            // Juliet:
            juliet.Name = "Juliet";
            juliet.Result = false;
            juliet.Image = Properties.Resources.Juliet_new;
            juliet.Color = System.Drawing.Color.DarkRed;
            // Bell:
            bell.Name = "Bell";
            bell.Result = false;
            bell.Image = Properties.Resources.Bell_new;
            bell.Color = System.Drawing.Color.Indigo;
            // Bow:
            bow.Name = "Bow";
            bow.Result = false;
            bow.Image = Properties.Resources.Bow_new;
            bow.Color = System.Drawing.Color.Indigo;
            // Emilie:
            emilie.Name = "Emilie";
            emilie.Result = false;
            emilie.Image = Properties.Resources.Emilie_new;
            emilie.Color = System.Drawing.Color.DarkRed;
            // Amber:
            amber.Name = "Amber";
            amber.Result = false;
            amber.Image = Properties.Resources.Amber_new;
            amber.Color = System.Drawing.Color.DarkRed;
            // Claire:
            claire.Name = "Claire";
            claire.Result = false;
            claire.Image = Properties.Resources.Claire_new;
            claire.Color = System.Drawing.Color.Indigo;

            // Adding – Osobnosti:
            seznamOsobnost.Add(share);
            seznamOsobnost.Add(jenny);
            seznamOsobnost.Add(jaykie);
            seznamOsobnost.Add(juliet);
            seznamOsobnost.Add(bell);
            seznamOsobnost.Add(bow);
            seznamOsobnost.Add(emilie);
            seznamOsobnost.Add(amber);
            seznamOsobnost.Add(claire);

            // !!!Setting – Věci!!!
            // [0]:
            cigarety.Name = "Cigarety";
            cigarety.Result = false;
            cigarety.Image = ŠárkaKvíz.Properties.Resources.Cigarety;
            // [1]:
            knížky.Name = "Knížky";
            knížky.Result = false;
            knížky.Image = ŠárkaKvíz.Properties.Resources.Knížky;
            // [2]:
            minecraft.Name = "Minecraft";
            minecraft.Result = false;
            minecraft.Image = ŠárkaKvíz.Properties.Resources.Minecraft;
            // [3]:
            panda.Name = "Panda";
            panda.Result = false;
            panda.Image = ŠárkaKvíz.Properties.Resources.Panda;
            // [4]:
            brýleNe.Name = "Nemá brýle";
            brýleNe.Result = false;
            brýleNe.Image = ŠárkaKvíz.Properties.Resources.Brýle_ne;
            // [5]:
            hauština.Name = "Hauština";
            hauština.Result = false;
            hauština.Image = ŠárkaKvíz.Properties.Resources.Hauština;
            // [6]:
            kůň.Name = "Koně";
            kůň.Result = false;
            kůň.Image = ŠárkaKvíz.Properties.Resources.Kůň;
            // [7]:
            lama.Name = "Lama";
            lama.Result = false;
            lama.Image = ŠárkaKvíz.Properties.Resources.Lama;
            // [8]:
            posilováńí.Name = "Posilování";
            posilováńí.Result = false;
            posilováńí.Image = ŠárkaKvíz.Properties.Resources.Posilování;
            // [9]:
            budík.Name = "Brzké vstávání";
            budík.Result = false;
            budík.Image = ŠárkaKvíz.Properties.Resources.Budík;
            // [10]:
            lakNaNehty.Name = "Lak na nehty";
            lakNaNehty.Result = false;
            lakNaNehty.Image = ŠárkaKvíz.Properties.Resources.Lak_na_nehty;
            // [11]:
            šaty.Name = "Šaty";
            šaty.Result = false;
            šaty.Image = ŠárkaKvíz.Properties.Resources.Šaty;
            // [12]:
            čajíkOPáté.Name = "Čajík o páté";
            čajíkOPáté.Result = false;
            čajíkOPáté.Image = ŠárkaKvíz.Properties.Resources.Čaj_o_páté_new;
            // [13]:
            béžováBarva.Name = "Béžová barva";
            béžováBarva.Result = false;
            béžováBarva.Image = ŠárkaKvíz.Properties.Resources.Béžová_barva;
            // [14]:
            snapback.Name = "Snapback";
            snapback.Result = false;
            snapback.Image = ŠárkaKvíz.Properties.Resources.Snapback;
            // [15]:
            síťovanéOblečení.Name = "Síťované oblečení";
            síťovanéOblečení.Result = false;
            síťovanéOblečení.Image = ŠárkaKvíz.Properties.Resources.Síťované_oblečení;
            // [16]:
            šňupák.Name = "Šňupací tabák";
            šňupák.Result = false;
            šňupák.Image = ŠárkaKvíz.Properties.Resources.Šňupací_tabák;
            // [17]:
            statek.Name = "Statek";
            statek.Result = false;
            statek.Image = ŠárkaKvíz.Properties.Resources.Statek;
            // [18]:
            bro.Name = "Bro";
            bro.Result = false;
            bro.Image = ŠárkaKvíz.Properties.Resources.Bro;
            // [19]:
            plyšáci.Name = "Plyšáci";
            plyšáci.Result = false;
            plyšáci.Image = ŠárkaKvíz.Properties.Resources.Plyšáci;
            // [20]:
            práce.Name = "Práce";
            práce.Result = false;
            práce.Image = ŠárkaKvíz.Properties.Resources.Práce;
            // [21]:
            úklid.Name = "Úklid";
            úklid.Result = false;
            úklid.Image = ŠárkaKvíz.Properties.Resources.Úklid;
            // [22]:
            lenost.Name = "Lenost";
            lenost.Result = false;
            lenost.Image = ŠárkaKvíz.Properties.Resources.Lenost;
            // [23]:
            abstinent.Name = "Abstinent";
            abstinent.Result = false;
            abstinent.Image = ŠárkaKvíz.Properties.Resources.Abstinent;
            // [24]:
            simíci.Name = "The Sims";
            simíci.Result = false;
            simíci.Image = ŠárkaKvíz.Properties.Resources.Simíci;
            // [25]:
            ochráncePřírody.Name = "Chrání přírodu";
            ochráncePřírody.Result = false;
            ochráncePřírody.Image = ŠárkaKvíz.Properties.Resources.Ochránce_přírody_new;
            // [26]:
            meloun.Name = "Pan Meloun";
            meloun.Result = false;
            meloun.Image = ŠárkaKvíz.Properties.Resources.Pan_Meloun;
            // [27]:
            bílá.Name = "Bílá barva";
            bílá.Result = false;
            bílá.Image = ŠárkaKvíz.Properties.Resources.Bílá;
            // [28]:
            pinkiePie.Name = "Pinkie Pie";
            pinkiePie.Result = false;
            pinkiePie.Image = ŠárkaKvíz.Properties.Resources.Pinkie_Pie;
            // [29]:
            hudba.Name = "Hudba";
            hudba.Result = false;
            hudba.Image = ŠárkaKvíz.Properties.Resources.Hudba;
            // [30]:
            černáBarva.Name = "Černá barva";
            černáBarva.Result = false;
            černáBarva.Image = ŠárkaKvíz.Properties.Resources.Černá_barva;
            // [31]:
            fialováBarva.Name = "Fialová barva";
            fialováBarva.Result = false;
            fialováBarva.Image = ŠárkaKvíz.Properties.Resources.Fialová_barva;
            // [32]:
            růžováBarva.Name = "Růžová barva";
            růžováBarva.Result = false;
            růžováBarva.Image = ŠárkaKvíz.Properties.Resources.Růžová_barva;
            // [33]:
            pennyboard.Name = "Pennyboard";
            pennyboard.Result = false;
            pennyboard.Image = ŠárkaKvíz.Properties.Resources.Pennyboard;
            // [34]:
            longboard.Name = "Longboard";
            longboard.Result = false;
            longboard.Image = ŠárkaKvíz.Properties.Resources.Longboard;
            // [35]:
            plavání.Name = "Plavání";
            plavání.Result = false;
            plavání.Image = ŠárkaKvíz.Properties.Resources.Plavání;
            // [36]:
            databázeMax.Name = "Databáze Max";
            databázeMax.Result = false;
            databázeMax.Image = ŠárkaKvíz.Properties.Resources.Databáze_Max;
            // [37]:
            cimbálka.Name = "Cimbálka";
            cimbálka.Result = false;
            cimbálka.Image = ŠárkaKvíz.Properties.Resources.Cimbálka;
            // [38]:
            zpěvNe.Name = "Neumí zpívat";
            zpěvNe.Result = false;
            zpěvNe.Image = ŠárkaKvíz.Properties.Resources.Zpěv_ne;
            // [39]:
            khakiBarva.Name = "Khaki barva";
            khakiBarva.Result = false;
            khakiBarva.Image = ŠárkaKvíz.Properties.Resources.Khaki_barva;
            // [40]:
            asociál.Name = "Asociál";
            asociál.Result = false;
            asociál.Image = ŠárkaKvíz.Properties.Resources.Asociál;
            // [41]:
            asexuál.Name = "Asexuál";
            asexuál.Result = false;
            asexuál.Image = ŠárkaKvíz.Properties.Resources.Asexuál__vlajka_;
            // [42]:
            homosexuál.Name = "Homosexuál";
            homosexuál.Result = false;
            homosexuál.Image = ŠárkaKvíz.Properties.Resources.Homosexuál__vlajka_;
            // [43]:
            bisexuál.Name = "Bisexuál";
            bisexuál.Result = false;
            bisexuál.Image = ŠárkaKvíz.Properties.Resources.Bisexuál__vlajka_;
            // [44]:
            smutek.Name = "Častý smutek";
            smutek.Result = false;
            smutek.Image = ŠárkaKvíz.Properties.Resources.Smutek;
            // [45]:
            levandule.Name = "Levandule";
            levandule.Result = false;
            levandule.Image = ŠárkaKvíz.Properties.Resources.Levandule;
            // [46]:
            kočky.Name = "Kočky";
            kočky.Result = false;
            kočky.Image = ŠárkaKvíz.Properties.Resources.Kočky;
            // [47]:
            bubblebum.Name = "Bubble bum";
            bubblebum.Result = false;
            bubblebum.Image = ŠárkaKvíz.Properties.Resources.Bubble_bum;
            // [48]:
            mléko.Name = "Mléko";
            mléko.Result = false;
            mléko.Image = ŠárkaKvíz.Properties.Resources.Mléko;
            // [49]:
            tiger.Name = "Tiger";
            tiger.Result = false;
            tiger.Image = ŠárkaKvíz.Properties.Resources.Tiger;
            // [50]:
            holo.Name = "Holo";
            holo.Result = false;
            holo.Image = ŠárkaKvíz.Properties.Resources.Holo;
            // [51]:
            deník.Name = "Deník";
            deník.Result = false;
            deník.Image = ŠárkaKvíz.Properties.Resources.Deník;
            // [52]:
            věšeníZaNohu.Name = "Věší se za nohu";
            věšeníZaNohu.Result = false;
            věšeníZaNohu.Image = ŠárkaKvíz.Properties.Resources.Věšení_za_nohu;
            // [53]:
            citátPičovina.Name = "Pičovina";
            citátPičovina.Result = false;
            citátPičovina.Image = ŠárkaKvíz.Properties.Resources.Citát_pičovina;
            // [54]:
            citátSobeckáMrdka.Name = "Sobecká mrdka";
            citátSobeckáMrdka.Result = false;
            citátSobeckáMrdka.Image = ŠárkaKvíz.Properties.Resources.Citát_sobecká_mrdka;
            // [55]:
            auto.Name = "Auta";
            auto.Result = false;
            auto.Image = ŠárkaKvíz.Properties.Resources.Auto;
            // [56]:
            buldozer.Name = "Buldozer";
            buldozer.Result = false;
            buldozer.Image = ŠárkaKvíz.Properties.Resources.Buldozer;
            // [...]:

            // !!!Adding – Věci!!!
            // [0]:
            seznamVěcPůvodní.Add(cigarety);
            seznamVěcNový.Add(cigarety);
            // [1]:
            seznamVěcPůvodní.Add(knížky);
            seznamVěcNový.Add(knížky);
            // [2]:
            seznamVěcPůvodní.Add(minecraft);
            seznamVěcNový.Add(minecraft);
            // [3]:
            seznamVěcPůvodní.Add(panda);
            seznamVěcNový.Add(panda);
            // [4]:
            seznamVěcPůvodní.Add(brýleNe);
            seznamVěcNový.Add(brýleNe);
            // [5]:
            seznamVěcPůvodní.Add(hauština);
            seznamVěcNový.Add(hauština);
            // [6]:
            seznamVěcPůvodní.Add(kůň);
            seznamVěcNový.Add(kůň);
            // [7]:
            seznamVěcPůvodní.Add(lama);
            seznamVěcNový.Add(lama);
            // [8]:
            seznamVěcPůvodní.Add(posilováńí);
            seznamVěcNový.Add(posilováńí);
            // [9]:
            seznamVěcPůvodní.Add(budík);
            seznamVěcNový.Add(budík);
            // [10]:
            seznamVěcPůvodní.Add(lakNaNehty);
            seznamVěcNový.Add(lakNaNehty);
            // [11]:
            seznamVěcPůvodní.Add(šaty);
            seznamVěcNový.Add(šaty);
            // [12]:
            seznamVěcPůvodní.Add(čajíkOPáté);
            seznamVěcNový.Add(čajíkOPáté);
            // [13]:
            seznamVěcPůvodní.Add(béžováBarva);
            seznamVěcNový.Add(béžováBarva);
            // [14]:
            seznamVěcPůvodní.Add(snapback);
            seznamVěcNový.Add(snapback);
            // [15]:
            seznamVěcPůvodní.Add(síťovanéOblečení);
            seznamVěcNový.Add(síťovanéOblečení);
            // [16]:
            seznamVěcPůvodní.Add(šňupák);
            seznamVěcNový.Add(šňupák);
            // [17]:
            seznamVěcPůvodní.Add(statek);
            seznamVěcNový.Add(statek);
            // [18]:
            seznamVěcPůvodní.Add(bro);
            seznamVěcNový.Add(bro);
            // [19]:
            seznamVěcPůvodní.Add(plyšáci);
            seznamVěcNový.Add(plyšáci);
            // [20]:
            seznamVěcPůvodní.Add(práce);
            seznamVěcNový.Add(práce);
            // [21]:
            seznamVěcPůvodní.Add(úklid);
            seznamVěcNový.Add(úklid);
            // [22]:
            seznamVěcPůvodní.Add(lenost);
            seznamVěcNový.Add(lenost);
            // [23]:
            seznamVěcPůvodní.Add(abstinent);
            seznamVěcNový.Add(abstinent);
            // [24]:
            seznamVěcPůvodní.Add(simíci);
            seznamVěcNový.Add(simíci);
            // [25]:
            seznamVěcPůvodní.Add(ochráncePřírody);
            seznamVěcNový.Add(ochráncePřírody);
            // [26]:
            seznamVěcPůvodní.Add(meloun);
            seznamVěcNový.Add(meloun);
            //  [27]:
            seznamVěcPůvodní.Add(bílá);
            seznamVěcNový.Add(bílá);
            // [28]:
            seznamVěcPůvodní.Add(pinkiePie);
            seznamVěcNový.Add(pinkiePie);
            // [29]:
            seznamVěcPůvodní.Add(hudba);
            seznamVěcNový.Add(hudba);
            // [30]:
            seznamVěcPůvodní.Add(černáBarva);
            seznamVěcNový.Add(černáBarva);
            // [31]:
            seznamVěcPůvodní.Add(fialováBarva);
            seznamVěcNový.Add(fialováBarva);
            // [32]:
            seznamVěcPůvodní.Add(růžováBarva);
            seznamVěcNový.Add(růžováBarva);
            // [33]:
            seznamVěcPůvodní.Add(pennyboard);
            seznamVěcNový.Add(pennyboard);
            // [34]:
            seznamVěcPůvodní.Add(longboard);
            seznamVěcNový.Add(longboard);
            // [35]:
            seznamVěcPůvodní.Add(plavání);
            seznamVěcNový.Add(plavání);
            // [36]:
            seznamVěcPůvodní.Add(databázeMax);
            seznamVěcNový.Add(databázeMax);
            // [37]:
            seznamVěcPůvodní.Add(cimbálka);
            seznamVěcNový.Add(cimbálka);
            // [38]:
            seznamVěcPůvodní.Add(zpěvNe);
            seznamVěcNový.Add(zpěvNe);
            // [39]:
            seznamVěcPůvodní.Add(khakiBarva);
            seznamVěcNový.Add(khakiBarva);
            // [40]:
            seznamVěcPůvodní.Add(asociál);
            seznamVěcNový.Add(asociál);
            // [41]:
            seznamVěcPůvodní.Add(asexuál);
            seznamVěcNový.Add(asexuál);
            // [42]:
            seznamVěcPůvodní.Add(homosexuál);
            seznamVěcNový.Add(homosexuál);
            // [43]:
            seznamVěcPůvodní.Add(bisexuál);
            seznamVěcNový.Add(bisexuál);
            // [44]:
            seznamVěcPůvodní.Add(smutek);
            seznamVěcNový.Add(smutek);
            // [45]:
            seznamVěcPůvodní.Add(levandule);
            seznamVěcNový.Add(levandule);
            // [46]:
            seznamVěcPůvodní.Add(kočky);
            seznamVěcNový.Add(kočky);
            // [47]:
            seznamVěcPůvodní.Add(bubblebum);
            seznamVěcNový.Add(bubblebum);
            // [48]:
            seznamVěcPůvodní.Add(mléko);
            seznamVěcNový.Add(mléko);
            // [49]:
            seznamVěcPůvodní.Add(tiger);
            seznamVěcNový.Add(tiger);
            // [50]:
            seznamVěcPůvodní.Add(holo);
            seznamVěcNový.Add(holo);
            // [51]:
            seznamVěcPůvodní.Add(deník);
            seznamVěcNový.Add(deník);
            // [52]:
            seznamVěcPůvodní.Add(věšeníZaNohu);
            seznamVěcNový.Add(věšeníZaNohu);
            // [53]:
            seznamVěcPůvodní.Add(citátPičovina);
            seznamVěcNový.Add(citátPičovina);
            // [54]:
            seznamVěcPůvodní.Add(citátSobeckáMrdka);
            seznamVěcNový.Add(citátSobeckáMrdka);
            // [55]:
            seznamVěcPůvodní.Add(auto);
            seznamVěcNový.Add(auto);
            // [56]:
            seznamVěcPůvodní.Add(buldozer);
            seznamVěcNový.Add(buldozer);
            // ...:

            // Ukončení Welcoming Screenu.
            this.Close();
        }
    }
}
