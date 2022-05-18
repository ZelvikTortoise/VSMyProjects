using System;
using System.Collections.Generic;
using System.Text;

namespace Obchod
{
    public enum TypUdalosti
    {
        Start,
        Trpelivost,
        Obslouzen
    }

    public class Udalost
    {
        public int kdy;
        public Proces kdo;
        public TypUdalosti co;
        public Udalost(int kdy, Proces kdo, TypUdalosti co)
        {
            this.kdy = kdy;
            this.kdo = kdo;
            this.co = co;
        }
    }
    public class Kalendar
    {
        private List<Udalost> seznam;
        public Kalendar()
        {
            seznam = new List<Udalost>();
        }
        public void Pridej(int kdy, Proces kdo, TypUdalosti co)
        {
            // pro hledani chyby:
            foreach (Udalost ud in seznam)
                if (ud.kdo == kdo)
                    Console.WriteLine("");

            seznam.Add(new Udalost(kdy, kdo, co));
        }
        public void Odeber(Proces kdo, TypUdalosti co)
        {
            foreach (Udalost ud in seznam)
            {
                if ((ud.kdo == kdo) && (ud.co == co))
                {
                    seznam.Remove(ud);
                    return; // odebiram jen jeden vyskyt!
                }
            }
        }
        public Udalost Prvni()
        {
            Udalost prvni = null;
            foreach (Udalost ud in seznam)
                if ((prvni == null) || (ud.kdy < prvni.kdy))
                    prvni = ud;
            seznam.Remove(prvni);
            return prvni;
        }
        public Udalost Vyber()
        {
            return Prvni();
        }
    }

    public abstract class Proces
    {
        public static char[] mezery = { ' ' };
        public int patro;
        public string ID;
        public abstract void Zpracuj(Udalost ud);
        public void log(string zprava)
        {
            //if (ID == "Dana")
            //if (ID == "elefant")
            //if (this is Zakaznik)
            Console.WriteLine("{0}/{3} {1}: {2}",
                model.Cas, ID, zprava, patro);
        }
        protected Model model;
    }

    public class Oddeleni : Proces
    {
        private int rychlost;
        private List<Zakaznik> fronta;
        private bool obsluhuje;

        public Oddeleni(Model model, string popis)
        {
            this.model = model;
            string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);
            this.ID = popisy[0];
            this.patro = int.Parse(popisy[1]);
            if (this.patro > model.MaxPatro)
                model.MaxPatro = this.patro;
            this.rychlost = int.Parse(popisy[2]);
            obsluhuje = false;
            fronta = new List<Zakaznik>();
            model.VsechnaOddeleni.Add(this);
        }
        public void ZaradDoFronty(Zakaznik zak)
        {
            fronta.Add(zak);
            log("do fronty " + zak.ID);

            if (!obsluhuje)
            {
                obsluhuje = true;
                model.Naplanuj(model.Cas, this, TypUdalosti.Start);
            }
            // else (nic)
        }
        public void VyradZFronty(Zakaznik koho)
        {
            fronta.Remove(koho);
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    if (fronta.Count == 0)
                        obsluhuje = false; // a dal neni naplanovana a probudi se tim, ze se nekdo zaradi do fronty
                    else
                    {
                        Zakaznik zak = fronta[0];
                        fronta.RemoveAt(0);
                        model.Odplanuj(zak, TypUdalosti.Trpelivost);
                        model.Naplanuj(model.Cas + rychlost, zak, TypUdalosti.Obslouzen);
                        model.Naplanuj(model.Cas + rychlost, this, TypUdalosti.Start);
                    }
                    break;
            }
        }
        public int VratDelkuFronty()
        {
            return fronta.Count;
        }
    }
    public enum SmeryJizdy
    {
        Nahoru,
        Dolu,
        Stoji
    }
    public class Vytah : Proces
    {
        private int kapacita;
        private int dobaNastupu;
        private int dobaVystupu;
        private int dobaPatro2Patro;
        static int[] ismery = { +1, -1, 0 }; // prevod (int) SmeryJizdy na smer

        private class Pasazer
        {
            public Proces kdo;
            public int kamJede;
            public Pasazer(Proces kdo, int kamJede)
            {
                this.kdo = kdo;
                this.kamJede = kamJede;
            }
        }

        private List<Pasazer>[,] cekatele; // [patro,smer]
        private List<Pasazer> naklad;   // pasazeri ve vytahu
        private SmeryJizdy smer;
        private int kdyJsemMenilSmer;

        public void PridejDoFronty(int odkud, int kam, Proces kdo)
        {
            Pasazer pas = new Pasazer(kdo, kam);
            if (kam > odkud)
                cekatele[odkud, (int)SmeryJizdy.Nahoru].Add(pas);
            else
                cekatele[odkud, (int)SmeryJizdy.Dolu].Add(pas);

            // pripadne rozjet stojici vytah:
            if (smer == SmeryJizdy.Stoji)
            {
                model.Odplanuj(model.vytah, TypUdalosti.Start); // kdyby nahodou uz byl naplanovany
                model.Naplanuj(model.Cas, this, TypUdalosti.Start);
            }
        }
        public bool CekaNekdoVPatrechVeSmeruJizdy()
        {
            int ismer = ismery[(int)smer];
            for (int pat = patro + ismer; (pat > 0) && (pat <= model.MaxPatro); pat += ismer)
                if ((cekatele[pat, (int)SmeryJizdy.Nahoru].Count > 0) || (cekatele[pat, (int)SmeryJizdy.Dolu].Count > 0))
                {
                    if (cekatele[pat, (int)SmeryJizdy.Nahoru].Count > 0)
                        log("Nahoru ceka " + cekatele[pat, (int)SmeryJizdy.Nahoru][0].kdo.ID
                            + " v patre " + pat + "/" + cekatele[pat, (int)SmeryJizdy.Nahoru][0].kdo.patro);
                    if (cekatele[pat, (int)SmeryJizdy.Dolu].Count > 0)
                        log("Dolu ceka " + cekatele[pat, (int)SmeryJizdy.Dolu][0].kdo.ID
                            + " v patre " + pat + "/" + cekatele[pat, (int)SmeryJizdy.Dolu][0].kdo.patro);

                    //log(" x "+cekatele[pat, (int)SmeryJizdy.Nahoru].Count+" x "+cekatele[pat, (int)SmeryJizdy.Dolu].Count);
                    return true;
                }
            return false;
        }

        public Vytah(Model model, string popis)
        {
            this.model = model;
            string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);
            this.ID = popisy[0];
            this.kapacita = int.Parse(popisy[1]);
            this.dobaNastupu = int.Parse(popisy[2]);
            this.dobaVystupu = int.Parse(popisy[3]);
            this.dobaPatro2Patro = int.Parse(popisy[4]);
            this.patro = 0;
            this.smer = SmeryJizdy.Stoji;
            this.kdyJsemMenilSmer = -1;

            cekatele = new List<Pasazer>[model.MaxPatro + 1, 2];
            for (int i = 0; i < model.MaxPatro + 1; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    cekatele[i, j] = new List<Pasazer>();
                }

            }
            naklad = new List<Pasazer>();
        }
        public int VratFrontuVPatreVDanemSmeru(int patro, int smer)
        {
            return this.cekatele[patro, smer].Count;
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:

                    // HACK pro cerstve probuzeny vytah:
                    if (smer == SmeryJizdy.Stoji)
                        // stoji, tedy nikoho neveze a nekdo ho prave probudil => nastavim jakykoliv smer a najde ho:
                        smer = SmeryJizdy.Nahoru;

                    // chce nekdo vystoupit?
                    foreach (Pasazer pas in naklad)
                        if (pas.kamJede == patro)
                        // bude vystupovat:
                        {
                            naklad.Remove(pas);

                            pas.kdo.patro = patro;
                            model.Naplanuj(model.Cas + dobaVystupu, pas.kdo, TypUdalosti.Start);
                            log("vystupuje " + pas.kdo.ID);

                            model.Naplanuj(model.Cas + dobaVystupu, this, TypUdalosti.Start);

                            return; // to je pro tuhle chvili vsechno
                        }

                    // muze a chce nekdo nastoupit?
                    if (naklad.Count == kapacita)
                    // i kdyby chtel nekdo nastupovat, nemuze; veze lidi => pokracuje:
                    {
                        // popojet:
                        int ismer = ismery[(int)smer];
                        patro = patro + ismer;

                        string spas = "";
                        foreach (Pasazer pas in naklad)
                            spas += " " + pas.kdo.ID;
                        log("odjizdim");
                        model.Naplanuj(model.Cas + dobaPatro2Patro, this, TypUdalosti.Start);
                        return; // to je pro tuhle chvili vsechno
                    }
                    else
                    // neni uplne plny
                    {
                        // chce nastoupit nekdo VE SMERU jizdy?
                        if (cekatele[patro, (int)smer].Count > 0)
                        {
                            log("nastupuje " + cekatele[patro, (int)smer][0].kdo.ID);
                            naklad.Add(cekatele[patro, (int)smer][0]);
                            cekatele[patro, (int)smer].RemoveAt(0);
                            model.Naplanuj(model.Cas + dobaNastupu, this, TypUdalosti.Start);

                            return; // to je pro tuhle chvili vsechno
                        }

                        // ve smeru jizdy nikdo nenastupuje:
                        if (naklad.Count > 0)
                        // nikdo nenastupuje, vezu pasazery => pokracuju v jizde:
                        {
                            // popojet:
                            int ismer = ismery[(int)smer];
                            patro = patro + ismer;

                            string spas = "";
                            foreach (Pasazer pas in naklad)
                                spas += " " + pas.kdo.ID;
                            //log("nekoho vezu");
                            log("odjizdim: " + spas);

                            model.Naplanuj(model.Cas + dobaPatro2Patro, this, TypUdalosti.Start);
                            return; // to je pro tuhle chvili vsechno
                        }

                        // vytah je prazdny, pokud v dalsich patrech ve smeru jizdy uz nikdo neceka, muze zmenit smer nebo se zastavit:
                        if (CekaNekdoVPatrechVeSmeruJizdy() == true)
                        // pokracuje v jizde:
                        {
                            // popojet:
                            int ismer = ismery[(int)smer];
                            patro = patro + ismer;

                            //log("nekdo ceka");
                            log("odjizdim");
                            model.Naplanuj(model.Cas + dobaPatro2Patro, this, TypUdalosti.Start);
                            return; // to je pro tuhle chvili vsechno
                        }

                        // ve smeru jizdy uz nikdo neceka => zmenit smer nebo zastavit:
                        if (smer == SmeryJizdy.Nahoru)
                            smer = SmeryJizdy.Dolu;
                        else
                            smer = SmeryJizdy.Nahoru;

                        log("zmena smeru");

                        //chce nekdo nastoupit prave tady?
                        if (kdyJsemMenilSmer != model.Cas)
                        {
                            kdyJsemMenilSmer = model.Cas;
                            // podivat se, jestli nekdo nechce nastoupit opacnym smerem:
                            model.Naplanuj(model.Cas, this, TypUdalosti.Start);
                            return;
                        }

                        // uz jsem jednou smer menil a zase nikdo nenastoupil a nechce => zastavit
                        log("zastavuje");
                        smer = SmeryJizdy.Stoji;
                        return; // to je pro tuhle chvili vsechno
                    }
            }
        }
    }

    public enum SpecialniSchopnostZakaznika { Zadna, S1, S2 }

    public class Zakaznik : Proces
    {
        private int trpelivost;
        private int prichod;
        private List<string> Nakupy;
        public readonly SpecialniSchopnostZakaznika schopnost;
        public Zakaznik(Model model, string popis)
        {
            this.model = model;
            string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);
            this.ID = popisy[0];
            this.prichod = int.Parse(popisy[1]);
            this.trpelivost = int.Parse(popisy[2]);
            this.schopnost = SpecialniSchopnostZakaznika.Zadna;
            Nakupy = new List<string>();
            for (int i = 3; i < popisy.Length; i++)
            {
                Nakupy.Add(popisy[i]);
            }
            this.patro = 0;
            Console.WriteLine("Init Zakaznik: {0}", ID);
            model.Naplanuj(prichod, this, TypUdalosti.Start);
        }
        public Zakaznik(Model model, string popis, SpecialniSchopnostZakaznika schopnost)
        {
            this.model = model;
            string[] popisy = popis.Split(Proces.mezery, StringSplitOptions.RemoveEmptyEntries);
            this.ID = popisy[0];
            this.prichod = int.Parse(popisy[1]);
            this.trpelivost = int.Parse(popisy[2]);
            this.schopnost = schopnost;
            Nakupy = new List<string>();
            for (int i = 3; i < popisy.Length; i++)
            {
                Nakupy.Add(popisy[i]);
            }
            this.patro = 0;
            Console.WriteLine("Init Zakaznik: {0}", ID);
            model.Naplanuj(prichod, this, TypUdalosti.Start);
        }
        public override void Zpracuj(Udalost ud)
        {
            switch (ud.co)
            {
                case TypUdalosti.Start:
                    if (Nakupy.Count == 0)
                    // ma nakoupeno
                    {
                        if (patro == 0)
                        {
                            log("-------------- odchází");  // konci
                            model.PridejCasStravenyVOD(model.Cas - prichod, schopnost);   // zaznamename si jeho cas v obchodnim dome
                        }                            
                        else
                            model.vytah.PridejDoFronty(patro, 0, this);
                    }
                    else
                    {
                        Oddeleni odd;
                        if (this.schopnost == SpecialniSchopnostZakaznika.S1)   // superschopnost S1
                        {
                            odd = null; // kvuli prekladaci
                            bool found = false;
                            for (int i = 0; i < this.Nakupy.Count; i++)
                            {
                                odd = OddeleniPodleJmena(this.Nakupy[i]);
                                if (odd.patro == this.patro)
                                {
                                    found = true;   // nasli jsme obchod v tomto patre -> jdeme tam
                                    // Vybrany nakup ma byt prvni na listku:
                                    string pom = this.Nakupy[i];
                                    this.Nakupy[i] = this.Nakupy[0];
                                    this.Nakupy[0] = pom;
                                    break;
                                }                                    
                            }
                            if (!found)
                                odd = OddeleniPodleJmena(Nakupy[0]);
                        }
                        else if (this.schopnost == SpecialniSchopnostZakaznika.S2)
                        {
                            int fronty, min = int.MaxValue;
                            int iter, smer;
                            Oddeleni oddeleni;
                            if (this.Nakupy.Count > 1)
                            {
                                odd = null; // kvuli prekladaci
                                for (int i = 0; i < this.Nakupy.Count; i++)
                                {
                                    oddeleni = OddeleniPodleJmena(this.Nakupy[i]);  // zkoumane oddeleni
                                    fronty = oddeleni.VratDelkuFronty();   // lidi ve fronte u daneho oddeleni

                                    // vytah:
                                    if (oddeleni.patro > patro)
                                    {
                                        // potrebujeme jet nahoru
                                        iter = 1;
                                        smer = (int)SmeryJizdy.Nahoru;
                                    }
                                    else
                                    {
                                        // potrebujeme dolu
                                        iter = -1;
                                        smer = (int)SmeryJizdy.Dolu;
                                    }
                                    // Pozn.: v pripade, ze nepotrebujeme jet vytahem, ve for cyklu neprobehne ani jedna iterace

                                    for (int p = this.patro; p < oddeleni.patro; p += iter) // Pozn.: nezajimaji nas lidi u vytahu v cilovem patre
                                    {
                                        fronty += model.vytah.VratFrontuVPatreVDanemSmeru(p, smer); // lidi cekajici na vytah v mezipatrech a nasem patre
                                    }

                                    if (fronty == 0)
                                    {
                                        odd = oddeleni; // lepsi to nebude (S2 neumi preferovat oddeleni v soucasnem patre)
                                        break;
                                    }
                                    else if (fronty <= min) // (rovnost: kdyby fronty == int.MaxValue, odd by zustalo jako null -> vyjimka)
                                    {
                                        min = fronty;
                                        odd = oddeleni; // mame nove zatim nejlepsi oddeleni
                                    }
                                }
                                // vybrane oddeleni musi byt na prvnim miste seznamu
                                int index = this.Nakupy.IndexOf(odd.ID);
                                string pom = this.Nakupy[index];
                                this.Nakupy[index] = this.Nakupy[0];
                                this.Nakupy[0] = pom;
                            }                                
                            else
                                odd = OddeleniPodleJmena(this.Nakupy[0]);
                        }
                        else
                        {
                            odd = OddeleniPodleJmena(this.Nakupy[0]);
                        }
                        
                        int pat = odd.patro;
                        if (pat == this.patro) // to oddeleni je v patre, kde prave jsem
                        {
                            if (Nakupy.Count > 1)
                                model.Naplanuj(model.Cas + trpelivost, this, TypUdalosti.Trpelivost);
                            odd.ZaradDoFronty(this);
                        }
                        else
                            model.vytah.PridejDoFronty(patro, pat, this);
                    }
                    break;
                case TypUdalosti.Obslouzen:
                    log("Nakoupeno: " + Nakupy[0]);
                    Nakupy.RemoveAt(0);
                    // ...a budu hledat dalsi nakup -->> Start
                    model.Naplanuj(model.Cas, this, TypUdalosti.Start);
                    break;
                case TypUdalosti.Trpelivost:
                    log("!!! Trpelivost: " + Nakupy[0]);
                    // vyradit z fronty:
                    {
                        Oddeleni odd = OddeleniPodleJmena(Nakupy[0]);
                        odd.VyradZFronty(this);
                    }

                    // prehodit tenhle nakup na konec:
                    string nesplneny = Nakupy[0];
                    Nakupy.RemoveAt(0);
                    Nakupy.Add(nesplneny);

                    // ...a budu hledat dalsi nakup -->> Start
                    model.Naplanuj(model.Cas, this, TypUdalosti.Start);
                    break;
            }
        }

        private Oddeleni OddeleniPodleJmena(string kamChci)
        {
            foreach (Oddeleni odd in model.VsechnaOddeleni)
                if (odd.ID == kamChci)
                    return odd;
            return null;
        }
    }

    public class Model
    {
        public int Cas;
        public Vytah vytah;
        public List<Oddeleni> VsechnaOddeleni = new List<Oddeleni>();
        public int MaxPatro;
        private Random randomizer;
        private Kalendar kalendar;
        private List<int> dobyStraveneVODNormalni = new List<int>();
        private List<int> dobyStraveneVODS1 = new List<int>();
        private List<int> dobyStraveneVODS2 = new List<int>();
        public void Naplanuj(int kdy, Proces kdo, TypUdalosti co)
        {
            kalendar.Pridej(kdy, kdo, co);
        }
        public void Odplanuj(Proces kdo, TypUdalosti co)
        {
            kalendar.Odeber(kdo, co);
        }
        public void PridejCasStravenyVOD(int cas, SpecialniSchopnostZakaznika schopnost)
        {
            switch (schopnost)
            {
                case SpecialniSchopnostZakaznika.Zadna:
                    dobyStraveneVODNormalni.Add(cas);
                    break;
                case SpecialniSchopnostZakaznika.S1:
                    dobyStraveneVODS1.Add(cas);
                    break;
                case SpecialniSchopnostZakaznika.S2:
                    dobyStraveneVODS2.Add(cas);
                    break;
            }
        }
        public List<int> VratSeznamDob()    // stara verze
        {
            return this.dobyStraveneVODNormalni;
        }
        public List<int> VratSeznamDobNormalnich()
        {
            return this.dobyStraveneVODNormalni;
        }
        public List<int> VratSeznamDobS1()
        {
            return this.dobyStraveneVODS1;
        }
        public List<int> VratSeznamDobS2()
        {
            return this.dobyStraveneVODS2;
        }
        private void NahodneVytvorZakaznikySeSchopnostmiMod3(int zakazniku)
        {
            Console.WriteLine("Init model: {0} zakazniku --------------------------------", zakazniku);
            const string idBase = "zakaznik";
            const string s1 = "_S1", s2 = "_S2";
            const char separator = ' ';
            int nakupu, cisloObchodu;
            SpecialniSchopnostZakaznika schopnost;

            for (int i = 1; i <= zakazniku; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(idBase);
                sb.Append(i);
                switch (i % 3)
                {
                    case 1: // normalni zakaznici
                        schopnost = SpecialniSchopnostZakaznika.Zadna;
                        break;
                    case 2: // S1
                        schopnost = SpecialniSchopnostZakaznika.S1;
                        sb.Append(s1);
                        break;
                    case 0: // S2
                        schopnost = SpecialniSchopnostZakaznika.S2;
                        sb.Append(s2);
                        break;
                    default:
                        schopnost = SpecialniSchopnostZakaznika.Zadna;  // kvuli prekladaci
                        break;
                }

                sb.Append(separator);
                sb.Append(this.randomizer.Next(0, 601));   // prichod 0–600
                sb.Append(separator);
                sb.Append(this.randomizer.Next(1, 181));   // trpelivost 1–180 

                nakupu = this.randomizer.Next(1, 21);  // nakupu 1–20
                for (int j = 1; j <= nakupu; j++)
                {
                    sb.Append(separator);
                    cisloObchodu = this.randomizer.Next(0, this.VsechnaOddeleni.Count);
                    sb.Append(this.VsechnaOddeleni[cisloObchodu].ID);
                }

                // vznik zakaznika:
                new Zakaznik(this, sb.ToString(), schopnost);  // model, ID prichod trpelivost nakupy, schopnost
                // Pozn.: zapojeni do modelu probiha v konstruktoru
            }
        }

        private void NahodneVytvorZakazniky(int zakazniku)
        {
            Console.WriteLine("Init model: {0} zakazniku --------------------------------", zakazniku);
            const string idBase = "zakaznik";
            const char separator = ' ';
            int nakupu, cisloObchodu;

            for (int i = 1; i <= zakazniku; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(idBase);
                sb.Append(i);
                sb.Append(separator);
                sb.Append(this.randomizer.Next(0, 601));   // prichod 0–600
                sb.Append(separator);
                sb.Append(this.randomizer.Next(1, 181));   // trpelivost 1–180 

                nakupu = this.randomizer.Next(1, 21);  // nakupu 1–20
                for (int j = 1; j <= nakupu; j++)
                {
                    sb.Append(separator);
                    cisloObchodu = this.randomizer.Next(0, this.VsechnaOddeleni.Count);
                    sb.Append(this.VsechnaOddeleni[cisloObchodu].ID);
                }

                // vznik zakaznika:
                new Zakaznik(this, sb.ToString());    // model, ID prichod trpelivost nakupy
                // Pozn.: vychozi schopnost je SpecialniSchopnosti.Zadna
                // Pozn.: zapojeni do modelu probiha v konstruktoru
            }
        }
        private void VytvorProcesy(bool vytvorZakazniky)
        {
            System.IO.StreamReader soubor
                = new
          System.IO.StreamReader("obchod_data.txt", Encoding.GetEncoding(1250));
            while (!soubor.EndOfStream)
            {
                string s = soubor.ReadLine();
                if (s != "")
                {
                    switch (s[0])
                    {
                        case 'O':
                            new Oddeleni(this, s.Substring(1));
                            break;
                        case 'Z':
                            if (vytvorZakazniky)    // chceme cist ze souboru?
                                new Zakaznik(this, s.Substring(1));
                            break;
                        case 'V':
                            vytah = new Vytah(this, s.Substring(1));
                            break;
                    }
                }
            }
            soubor.Close();
        }
        public int Vypocet(int zakazniku, bool schopnosti)
        {
            Cas = 0;
            kalendar = new Kalendar();
            VytvorProcesy(false);
            if (schopnosti)
                NahodneVytvorZakaznikySeSchopnostmiMod3(zakazniku);
            else
                NahodneVytvorZakazniky(zakazniku);

            Udalost ud;

            while ((ud = kalendar.Vyber()) != null)
            {
                Cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
            }
            return Cas;
        }
        // Stara verze:
        public int Vypocet(int zakazniku)
        {
            Cas = 0;
            kalendar = new Kalendar();
            VytvorProcesy(false);
            NahodneVytvorZakazniky(zakazniku);

            Udalost ud;

            while ((ud = kalendar.Vyber()) != null)
            {
                Cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
            }
            return Cas;
        }
        public int Vypocet()
        {
            Cas = 0;
            kalendar = new Kalendar();
            VytvorProcesy(true);

            Udalost ud;

            while ((ud = kalendar.Vyber()) != null)
            {
                Cas = ud.kdy;
                ud.kdo.Zpracuj(ud);
            }
            return Cas;
        }

        public Model()
        {
            this.randomizer = new Random();
        }

        public Model(Random randomizer)
        {
            this.randomizer = randomizer;
        }
    }

    class Statistiky
    {
        // absolutne ne
        private List<List<int>[]> DobyPobytuVOD = new List<List<int>[]>();  // stara verze, katastrofa na pamet
        public const int failNum = int.MinValue;
        public const string fail = "N/A";
        
        // nova verze:
        public int VratPrumer(List<int> seznam)
        {
            if (seznam.Count <= 0)
                return failNum;
            else
            {
                int soucet = 0;
                foreach (int doba in seznam)
                {
                    soucet += doba;
                }

                return soucet / seznam.Count;
            }            
        }

        public int VratPrumerBezMinAMax(int[] pole)
        {
            for (int k = 0; k < pole.Length; k++)
                if (pole[k] == failNum)
                    return failNum;

            if (pole.Length <= 0)
                return int.MinValue;
            else if (pole.Length == 1)
                return pole[0];
            else if (pole.Length == 2)
                return (pole[0] + pole[1]) / 2;
            else   // nas chteny algoritmus
            {
                int min = NajdiMinimum(pole);
                int max = NajdiMaximum(pole);

                // chceme seznam, ktery ma delku o 2 mensi nez puvodni pole a je "chudsi" o min a max puvodniho pole
                List<int> novySeznam = new List<int>();
                for (int i = 0; i < pole.Length - 2; i++)
                    novySeznam.Add(pole[i]);  // posledni index bude pole.Length - 3
                novySeznam[0] += pole[pole.Length - 2] + pole[pole.Length - 1] - min - max;
                // Pozn.: nezalezi na tom, kterou / ktere hodnoty v poli zmenime, ale potrebujeme, aby soucet hodnot byl soucet hodnot
                //        puvodniho pole minus min a minus max

                return VratPrumer(novySeznam);
            }
        }

        public bool VypisStatistiky(List<int> prumery, int start, int konec, int iterKrok)
        {
            int zakazniku = start;
            for (int i = 0; i < prumery.Count; i++)
            {
                if (prumery[i] != failNum)
                    Console.WriteLine("Zakazniku: {0} --- PDKZSVOD: {1}", zakazniku, prumery[i]);
                else
                    Console.WriteLine("Zakazniku: {0} --- PDKZSVOD: {1}", zakazniku, fail);
                zakazniku += iterKrok;
            }

            if (zakazniku - iterKrok == konec)
                return true;
            else
                return false;   // neco se pokazilo nebo byly spatne zadany argumenty
        }
        // konec nove verze

        private int NajdiMinimum(int[] pole)
        {
            int minimum = int.MaxValue;

            for (int i = 0; i < pole.Length; i++)
                if (pole[i] < minimum)
                    minimum = pole[i];

            return minimum;
        }
        private int NajdiMaximum(int[] pole)
        {
            int maximum = int.MinValue;

            for (int i = 0; i < pole.Length; i++)
                if (pole[i] > maximum)
                    maximum = pole[i];

            return maximum;
        }

        // stare
        public void PridejDoSeznamuDob(List<int>[] statistikyZaJedenVypocetDobVOD)
        {
            if (statistikyZaJedenVypocetDobVOD == null)
                throw new ArgumentException("Neprijimame vypocty s nulovym poctem zakazniku!");

            this.DobyPobytuVOD.Add(statistikyZaJedenVypocetDobVOD);
        }
        // fuj! (pouzil jsem na skolni cast ukolu, programoval jsem to ve 2 rano, taky to tak vypada...)
        public void SpocitejAVypisPDKZSVODy()
        {
            int soucet, docasnyPrumer = 0; // prekladac potrebuje inic
            List<int>[] poleDob;
            int[] prumery;
            int prumer;

            for (int i = 0; i < DobyPobytuVOD.Count; i++)   // bereme i-te pole seznamu dob
            {
                prumery = new int[10];
                poleDob = DobyPobytuVOD[i];
                for (int j = 0; j < poleDob.Length; j++)    // bereme j-ty seznam dob z naseho pole
                {
                    soucet = 0;
                    for (int k = 0; k < poleDob[j].Count; k++)
                        soucet += poleDob[j][k];    // k-ta doba stravena v OD v nasem seznamu dob v nasem poli
                    docasnyPrumer = soucet / poleDob[j].Count; // j-ty seznam v poli
                    prumery[j] = docasnyPrumer;
                }

                soucet = 0;
                for (int j = 0; j < prumery.Length; j++)
                    soucet += prumery[j];
                soucet -= NajdiMinimum(prumery);    // vyrazujeme minimum
                soucet -= NajdiMaximum(prumery);    // vyrazujeme maximum

                prumer = soucet / 8;    // pocitame prumer jenom z 8 hodnot
                Console.WriteLine("Celkem vsech zakazniku: {0} --- PDKZSVOD: {1}", DobyPobytuVOD[i][0].Count, prumer);   // pocet zakazniku je stejny ve vsech seznamech i-teho pole
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Statistiky statistiky = new Statistiky();
            Model model;
            int casKonce;

            const int startPocetZakazniku = 1;
            const int konecPocetZakazniku = 501;
            const int iteracniKrok = 10;
            Random random = new Random(12345);  // seed 12345 je povinny ve specifikaci zadani

            int[] polePrumeruNorm;
            int[] polePrumeruS1;
            int[] polePrumeruS2;
            char koncovka;
            List<int> PDKZSVODyNorm = new List<int>();  // PDKZSVODy pro normalni zakazniky
            List<int> PDKZSVODyS1 = new List<int>();  // PDKZSVODy pro S1 zakazniky
            List<int> PDKZSVODyS2 = new List<int>();  // PDKZSVODy pro S2 zakazniky
            // Pozn.: Kazda polozka ve vsech seznamech odpovida nejakemu celkovemu poctu zakazniku v mereni
            for (int i = startPocetZakazniku; i <= konecPocetZakazniku; i += iteracniKrok)
            {
                polePrumeruNorm = new int[10];
                polePrumeruS1 = new int[10];
                polePrumeruS2 = new int[10];
                for (int j = 1; j <= 10; j++)
                {
                    // neosetruju zaporny pocet zakazniku
                    if (i == 1)
                        koncovka = 'a';
                    else if (i >= 2 && i <= 4)
                        koncovka = 'y';
                    else
                        koncovka = 'u';
                    Console.WriteLine("Simulace cislo {0} pro {1} zakaznik{2}", j, i, koncovka);
                    model = new Model(random);
                    casKonce = model.Vypocet(i, true);  // chceme i zakazniku a specialni schopnosti
                    polePrumeruNorm[j - 1] = statistiky.VratPrumer(model.VratSeznamDobNormalnich());
                    polePrumeruS1[j - 1] = statistiky.VratPrumer(model.VratSeznamDobS1());
                    polePrumeruS2[j - 1] = statistiky.VratPrumer(model.VratSeznamDobS2());
                    Console.WriteLine("{0} KONEC --------------------------------", casKonce);
                    Console.WriteLine();
                }
                PDKZSVODyNorm.Add(statistiky.VratPrumerBezMinAMax(polePrumeruNorm));
                PDKZSVODyS1.Add(statistiky.VratPrumerBezMinAMax(polePrumeruS1));
                PDKZSVODyS2.Add(statistiky.VratPrumerBezMinAMax(polePrumeruS2));
            }
            Console.WriteLine("-------------------------------- KONEC VYPOCTU --------------------------------");
            Console.WriteLine();

            Console.WriteLine("STATISTIKY:");
            Console.WriteLine("Statistiky normalnich zakazniku:");
            statistiky.VypisStatistiky(PDKZSVODyNorm, startPocetZakazniku, konecPocetZakazniku, iteracniKrok);
            Console.WriteLine("KONEC --------------------------------");
            Console.WriteLine("Statistiky S1 zakazniku:");
            statistiky.VypisStatistiky(PDKZSVODyS1, startPocetZakazniku, konecPocetZakazniku, iteracniKrok);
            Console.WriteLine("KONEC --------------------------------");
            Console.WriteLine("Statistiky S2 zakazniku:");
            statistiky.VypisStatistiky(PDKZSVODyS2, startPocetZakazniku, konecPocetZakazniku, iteracniKrok);
            Console.WriteLine("KONEC --------------------------------");
            Console.WriteLine("-------------------------------- KONEC STATISTIK --------------------------------");

            Console.WriteLine();
            Console.Write("Zmáčknete ENTER k ukončení programu... ");
            Console.ReadLine();
        }
    }
}