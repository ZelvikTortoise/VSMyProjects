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
    public partial class FormOsobnosti : Form
    {
        public FormOsobnosti()
        {
            InitializeComponent();

            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormOsobnosti_FormClosed);
        }

        public static int code = 0;     // 0-8      // Indexy v seznamu seznamOsobností.
        public static bool ritualDone = false;      // Nové využití Pointless buttonu. Musí začínat na false.
        public static bool ritualDoneAdd = false;   // Nové využití Pointless buttonu. Musí začínat na false.

        // Closed.
        private void FormOsobnosti_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Metody jednotlivých osobností:
        // [0]:
        public void ShareClick()
        {
            // Settings – Share:
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = true;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = true;
            FormWelcoming.posilováńí.Result = true;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = true;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = true;
            FormWelcoming.statek.Result = true;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = false;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = true;
            FormWelcoming.abstinent.Result = false;
            FormWelcoming.simíci.Result = true;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = true;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = true;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = true;
            FormWelcoming.longboard.Result = true;
            FormWelcoming.plavání.Result = true;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = true;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = true;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = true;
            FormWelcoming.kočky.Result = true;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = true;
            FormWelcoming.holo.Result = true;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = true;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 0;   // Share's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [1]:
        public void JennyClick()
        {
            // Settings – Jenny:
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = true;
            FormWelcoming.minecraft.Result = false;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = false;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = true;
            FormWelcoming.čajíkOPáté.Result = true;
            FormWelcoming.béžováBarva.Result = true;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = false;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = true;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = true;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = false;
            FormWelcoming.simíci.Result = true;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = false;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = false;
            FormWelcoming.longboard.Result = false;
            FormWelcoming.plavání.Result = false;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = false;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = true;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 1;   // Jenny's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [2]:
        public void JaykieClick()
        {
            // Settings – Jaykie:
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = true;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = true;
            FormWelcoming.kůň.Result = true;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = true;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = true;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = true;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = true;
            FormWelcoming.plyšáci.Result = false;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = false;
            FormWelcoming.simíci.Result = true;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = true;
            FormWelcoming.hudba.Result = true;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = true;
            FormWelcoming.longboard.Result = true;
            FormWelcoming.plavání.Result = false;
            FormWelcoming.databázeMax.Result = true;
            FormWelcoming.cimbálka.Result = true;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = false;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = true;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = true;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 2;   // Jaykie's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [3]:
        public void JulietClick()
        {
            // Settings – Juliet:
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = true;
            FormWelcoming.panda.Result = true;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = false;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = false;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = true;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = true;
            FormWelcoming.simíci.Result = true;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = true;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = true;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = true;
            FormWelcoming.longboard.Result = false;
            FormWelcoming.plavání.Result = true;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = false;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = true;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = true;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = true;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 3;   // Juliet's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [4]:
        public void BellClick()
        {
            // Settings – Bell:
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = false;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = true;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = false;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = true;
            FormWelcoming.čajíkOPáté.Result = true;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = true;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = false;
            FormWelcoming.práce.Result = true;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = false;
            FormWelcoming.simíci.Result = false;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = false;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = false;
            FormWelcoming.longboard.Result = false;
            FormWelcoming.plavání.Result = false;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = true;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = true;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = true;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = false;
            FormWelcoming.bubblebum.Result = true;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = true;

            // Switch:
            code = 4;   // Bell's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [5]:
        public void BowClick()
        {
            // Settings – Bow:
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = false;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = true;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = false;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = true;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = false;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = false;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = false;
            FormWelcoming.simíci.Result = false;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = true;
            FormWelcoming.černáBarva.Result = true;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = false;
            FormWelcoming.longboard.Result = false;
            FormWelcoming.plavání.Result = false;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = false;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = true;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = true;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = true;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = true;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = true;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 5;   // Bow's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [6]:
        public void EmilieClick()
        {
            FormWelcoming.cigarety.Result = false;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = false;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = true;
            FormWelcoming.budík.Result = true;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = false;
            FormWelcoming.statek.Result = true;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = false;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = true;
            FormWelcoming.simíci.Result = false;
            FormWelcoming.ochráncePřírody.Result = true;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = false;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = false;
            FormWelcoming.longboard.Result = true;
            FormWelcoming.plavání.Result = true;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = false;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = true;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = true;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 6;   // Emilie's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [7]:
        public void AmberClick()
        {
            // Settings – Amber:
            FormWelcoming.cigarety.Result = true;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = false;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = false;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = false;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = true;
            FormWelcoming.šňupák.Result = true;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = false;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = false;
            FormWelcoming.simíci.Result = false;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = false;
            FormWelcoming.pinkiePie.Result = false;
            FormWelcoming.hudba.Result = false;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = true;
            FormWelcoming.pennyboard.Result = true;
            FormWelcoming.longboard.Result = false;
            FormWelcoming.plavání.Result = true;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = false;
            FormWelcoming.zpěvNe.Result = true;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = false;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 7;   // Amber's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }

        // [8]:
        public void ClaireClick()
        {
            // Settings – Claire:
            FormWelcoming.cigarety.Result = false; ;
            FormWelcoming.knížky.Result = false;
            FormWelcoming.minecraft.Result = true;
            FormWelcoming.panda.Result = false;
            FormWelcoming.brýleNe.Result = false;
            FormWelcoming.hauština.Result = false;
            FormWelcoming.kůň.Result = false;
            FormWelcoming.lama.Result = false;
            FormWelcoming.posilováńí.Result = true;
            FormWelcoming.budík.Result = false;
            FormWelcoming.lakNaNehty.Result = true;
            FormWelcoming.šaty.Result = false;
            FormWelcoming.čajíkOPáté.Result = false;
            FormWelcoming.béžováBarva.Result = false;
            FormWelcoming.snapback.Result = false;
            FormWelcoming.síťovanéOblečení.Result = false;
            FormWelcoming.šňupák.Result = false;
            FormWelcoming.statek.Result = false;
            FormWelcoming.bro.Result = false;
            FormWelcoming.plyšáci.Result = true;
            FormWelcoming.práce.Result = false;
            FormWelcoming.úklid.Result = false;
            FormWelcoming.lenost.Result = false;
            FormWelcoming.abstinent.Result = true;
            FormWelcoming.simíci.Result = true;
            FormWelcoming.ochráncePřírody.Result = false;
            FormWelcoming.meloun.Result = false;
            FormWelcoming.bílá.Result = true;
            FormWelcoming.pinkiePie.Result = true;
            FormWelcoming.hudba.Result = true;
            FormWelcoming.černáBarva.Result = false;
            FormWelcoming.fialováBarva.Result = false;
            FormWelcoming.růžováBarva.Result = false;
            FormWelcoming.pennyboard.Result = true;
            FormWelcoming.longboard.Result = true;
            FormWelcoming.plavání.Result = false;
            FormWelcoming.databázeMax.Result = false;
            FormWelcoming.cimbálka.Result = false;
            FormWelcoming.zpěvNe.Result = false;
            FormWelcoming.khakiBarva.Result = false;
            FormWelcoming.asociál.Result = false;
            FormWelcoming.asexuál.Result = false;
            FormWelcoming.homosexuál.Result = false;
            FormWelcoming.bisexuál.Result = false;
            FormWelcoming.smutek.Result = false;
            FormWelcoming.levandule.Result = false;
            FormWelcoming.kočky.Result = true;
            FormWelcoming.bubblebum.Result = false;
            FormWelcoming.mléko.Result = false;
            FormWelcoming.tiger.Result = false;
            FormWelcoming.holo.Result = false;
            FormWelcoming.deník.Result = false;
            FormWelcoming.věšeníZaNohu.Result = false;
            FormWelcoming.citátPičovina.Result = false;
            FormWelcoming.citátSobeckáMrdka.Result = false;
            FormWelcoming.auto.Result = false;
            FormWelcoming.buldozer.Result = false;

            // Switch:
            code = 8;   // Claire's code
            this.Hide();
            (new FormUrčitáOsobnost()).Show();
        }    
        // Konec metod jednotlivých osobností.

        // Jednotlivé osobnosti:
        // Share:
        private void buttonShare_Click(object sender, EventArgs e)
        {
            ShareClick();
        }

        // Jenny:
        private void buttonJenny_Click(object sender, EventArgs e)
        {
            JennyClick();
        }

        // Jaykie:
        private void buttonJaykie_Click(object sender, EventArgs e)
        {
            JaykieClick();
        }      

        // Juliet:
        private void buttonJuliet_Click(object sender, EventArgs e)
        {
            JulietClick();
        }

        // Bell:
        private void buttonBell_Click(object sender, EventArgs e)
        {
            BellClick();
        }

        // Bow:
        private void buttonBow_Click(object sender, EventArgs e)
        {
            BowClick();
        }

        // Emilie:
        private void buttonEmilie_Click(object sender, EventArgs e)
        {
            EmilieClick();
        }

        // Amber:
        private void buttonAmber_Click(object sender, EventArgs e)
        {
            AmberClick();
        }

        // Claire:
        private void buttonClaire_Click(object sender, EventArgs e)
        {
            ClaireClick();
        }

        // Konec jednotlivých osobností.




        private void buttonZpět_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormDividing()).Show();
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            FormWelcoming.formCode = 3;
            this.Hide();
            (new FormKonec()).Show();
        }

        private void buttonNáhodně_Click(object sender, EventArgs e)
        {
            code = FormWelcoming.random.Next(0, 9);     // 0-8. 

            switch(code)
            {
                // Share:
                case 0:
                    ShareClick();
                    break;
                // Jenny:
                case 1:
                    JennyClick();
                    break;
                // Jaykie:
                case 2:
                    JaykieClick();
                    break;
                // Juliet:
                case 3:
                    JulietClick();
                    break;
                // Bell:
                case 4:
                    BellClick();
                    break;
                // Bow:
                case 5:
                    BowClick();
                    break;
                // Emilie:
                case 6:
                    EmilieClick();
                    break;
                // Amber:
                case 7:
                    AmberClick();
                    break;
                // Claire:
                case 8:
                    ClaireClick();
                    break;
            }
        }
    }
}
