using System;
using System.IO;
using System.Collections.Generic;

namespace Povidani
{
    class Program
    {
        // Deklarace:
        static List<string> seznamOdpovedi = new List<string>();

        const string strShare = "Share.txt";
        const string strJenny = "Jenny.txt";
        const string strJaykie = "Jaykie.txt";
        const string strJuliet = "Juliet.txt";
        const string strBell = "Bell.txt";
        const string strBow = "Bow.txt";
        const string strEm = "Em.txt";
        const string strAmber = "Amber.txt";
        const string strClaire = "Claire.txt";
        const string strJacob = "Jacob.txt";
        const string strFenix = "Fenix.txt";
        const string strEvelynn = "Evelynn.txt";

        const string odpovedi = "Odpovedi.txt";
        const string otazky = "Otazky.txt";

        static string jmeno;
        static string cesta;
        static string psw;
        static string inpPsw;
        static bool pswCreate = false;
        static bool pswChange;
        static string puvodniText;
        static string test;

        static string answ;
        static string quest;

        static string radek;
        static char znak;
        static bool exit = true;

        // Konec deklarace.


        // Úvod (zjistí cestu ke správnému souboru), pozná uživatele.
        public static void UzivatelskeJmeno()
        {
            Console.Clear();
            Console.WriteLine("Ahoj! :)");
            Console.WriteLine("Jak se jmenuješ? =)");
            Console.WriteLine("(Začni velkým písmenem.)");
            Console.Write("Jméno: ");
            jmeno = Console.ReadLine();

            switch (jmeno)
            {
                case "Share":
                case "Šárka":
                case "Holostonožka":
                case "Holostonozka":
                    cesta = strShare;
                    break;
                case "Jenny":
                case "Jenn":
                    cesta = strJenny;
                    break;
                case "Jaykie":
                case "Zmrzlina":
                    cesta = strJaykie;
                    break;
                case "Juliet":
                case "Julča":
                case "Julca":
                case "Pandík":
                case "Pandik":
                case "Panda":
                    cesta = strJuliet;
                    break;
                case "Bell":
                case "AnaBell":
                case "Anabell":
                case "Ana Bell":
                    cesta = strBell;
                    break;
                case "Bow":
                    cesta = strBow;
                    break;
                case "Em":
                case "Emilie":
                    cesta = strEm;
                    break;
                case "Amber":
                    cesta = strAmber;
                    break;
                case "Claire":
                case "Pinkie":
                case "Pinkie Pie":
                case "Party":
                    cesta = strClaire;
                    break;
                case "Jacob":
                case "Ušák":
                    cesta = strJacob;
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Jméno neexistuje!");
                    Console.WriteLine("Zkuste to prosím znovu.");
                    PAKtC();
                    UzivatelskeJmeno();
                    break;
            }
        }

        // Press any key to continue.
        public static void PAKtC()
        {
            Console.WriteLine();
            Console.Write("Pro pokračování zmáčkněte libovolnou klávesu... ");
            Console.ReadKey();
        }

        // Uživatel si zadá nové heslo o 3-20 znacích.  // Nastavuje pswCreate = true. Heslo uloží do proměnné psw.
        public static void NoveHeslo()
        {
            bool success = false;
            int pokusy;

            while (!success)
            {
                Console.Clear();
                Console.WriteLine("Uživatel: " + jmeno);
                Console.WriteLine("Zadejte vaše heslo, které budete používat při každém spuštění programu. (3-20 znaků)");
                Console.Write("Vaše heslo: ");
                psw = Console.ReadLine();
                Console.WriteLine();

                if (psw.Length >= 3 && psw.Length <= 20)
                {
                    pokusy = 3;
                    Console.WriteLine("Úspěch!");
                    Console.WriteLine("Nyní pro kontrolu zadejte heslo ještě jednou.");
                    PAKtC();

                    while (pokusy > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Uživatel: " + jmeno);
                        Console.Write("Heslo znovu: ");
                        inpPsw = Console.ReadLine();
                        Console.WriteLine();

                        if (inpPsw == psw)
                        {
                            if (exit)   // Při vytváření prního ano, při změně ne.
                            {
                                Console.WriteLine("Heslo úspěšně uloženo.");
                                pswCreate = true;
                                PAKtC();
                            }
                            pokusy = 0;     // Konec cyklů, úspěšně zadáno.
                            success = true;
                        }
                        else
                        {
                            pokusy--;
                            Console.WriteLine("Neúspěch. Vstup: " + inpPsw);
                            Console.WriteLine("Zbývající pokusy: " + pokusy);
                            Console.WriteLine("Zkuste to znovu.");
                            PAKtC();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Neplatná délka hesla!");
                    Console.WriteLine("Heslo musí být dlouhé alespoň 3 znaky a nejvýše 20 znaků! ");
                    PAKtC();
                }
            }
        }

        // Ověří heslo.
        public static void Overeni()
        {
            if (!File.Exists(cesta))
            {
                using (StreamWriter sw = new StreamWriter(cesta))   // Vytvoří se soubor.
                {
                }
            }

            using (StreamReader sr = new StreamReader(cesta))
            {
                int pokusy = 3;
                psw = sr.ReadLine();

                if (psw != null)
                {
                    bool leave = false;
                    while (!leave)
                    {
                        Console.Clear();
                        Console.WriteLine("Uživatel: " + jmeno);
                        Console.Write("Heslo: ");
                        inpPsw = Console.ReadLine();

                        if (psw == inpPsw)
                        {
                            Console.WriteLine("Úspěch. :)");
                            leave = true;
                            pswChange = true;
                            PAKtC();
                        }
                        else
                        {
                            pokusy--;
                            Console.WriteLine("Špatné heslo!");
                            if (pokusy > 0)
                            {
                                Console.WriteLine("Zkuste to znovu.");
                                Console.WriteLine("Zbývající pokusy: " + pokusy);
                                PAKtC();
                            }
                            else
                            {
                                Console.WriteLine("Žádné zbývající pokusy.");
                                Console.Write("Je nám líto. ");
                                if (exit)
                                {
                                    Console.WriteLine("Aplikace se vypne.");
                                    PAKtC();
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    Console.WriteLine();
                                    leave = true;
                                    //PAKtC();  // Nežádoucí
                                }
                            }
                        }
                    }                   
                }
                else
                {
                    NoveHeslo();
                }
            }
        }

        // Zapamatuje si heslo, pokud uživatel využívá program poprvé.
        public static void VytvorHeslo()
        {
            using (StreamWriter sw = new StreamWriter(cesta))
            {
                sw.WriteLine(psw);
            }
            pswCreate = false;
        }

        // Zjistí, jestli chce uživatel změnit své heslo.
        public static void ChceteZmenitHeslo()
        {
            Console.Clear();
            Console.WriteLine("Chcete změnit heslo? (Ano - a, Ne - n)");
            Console.Write("Vaše odpověď: ");
            try
            {
                znak = char.Parse(Console.ReadLine());

                switch (znak)
                {
                    case 'a':
                    case 'A':
                        ZmenaHesla();
                        break;
                    case 'n':
                    case 'N':
                        Console.WriteLine("Vaše heslo nebude změněno.");
                        PAKtC();
                        break;
                    default:
                        Console.WriteLine("Neplatný znak.");
                        Console.WriteLine("Zkuste to znovu.");
                        PAKtC();
                        ChceteZmenitHeslo();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Neplatný vstup. Zadejte buď 'a' pro změnu hesla, nebo 'n', pokud heslo měnit nechcete.");
                PAKtC();
                ChceteZmenitHeslo();
            }
        }

        // Změní heslo.
        public static void ZmenaHesla()
        {
            Console.WriteLine("Pro potvrzení musíte zadat vaše stávající heslo.");
            PAKtC();
            exit = false;   // Nevytváříme první heslo.
            pswChange = false;  // Reset, až při ověření zjistíme, zda heslo změnit.
            Overeni();

            if (pswChange)
            {
                NoveHeslo();
                // Fyzická změna hesla.
                using (StreamReader sr = new StreamReader(cesta))
                {
                    sr.ReadLine();
                    puvodniText = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(cesta))
                {
                    sw.WriteLine(psw);
                    sw.Write(puvodniText);
                }
                Console.WriteLine("Heslo úspěšně uloženo.                         ");   // Mezery jsou tam kvůli bugu (velká rychlost).
                PAKtC();
            }
            else
            {
                Console.WriteLine("Heslo nebylo změněno.");
                PAKtC();
            }           
        }

        // Informace o 'HELP', pozdrav.
        public static void Uvod()
        {
            Console.Clear();
            Console.WriteLine("Zdravím tě.");
            Console.WriteLine("Já jsem tvůj povídací program. :)");
            PAKtC();
            Console.WriteLine("Pokud chceš znát příkazy, napiš kdykoliv 'HELP'.");
            Console.WriteLine("Já ti pak poradím, co dělat. =)");
            PAKtC();
        }

        // Program se zeptal na otázku a dostane odpověď. Pokud ji nezná, naučí se ji.
        public static void Odpovezeno(string dotaz)
        {
            Console.Clear();
            Console.WriteLine(dotaz);
            answ = Console.ReadLine();

            using (StreamReader sr = new StreamReader(odpovedi))
            {
                while ((radek = sr.ReadLine()) != null)
                {
                    seznamOdpovedi.Add(radek);
                }
                test = null;  // Vynulování.
                foreach (string odp in seznamOdpovedi)
                {
                    if (radek == odp)
                    {
                        test = radek;
                        break;
                    }
                }
                if (test == null)
                {
                    MamZapamatovatOdpoved(dotaz, answ);
                }
                else
                {
                    // Podle char mood (-/0/+) náhodně vybere odpovídající reakci.
                }
            }
        }

        // Program se zeptá, jestli si má odpověď "odp" zapamatovat, anebo obsahuje překlepy.
        public static void MamZapamatovatOdpoved(string dotaz, string odp)
        {
            Console.WriteLine("Tuto odpověď jsem ještě neslyšel.");
            Console.WriteLine("Mám si ji zapamatovat? (Ano - a, Ne - n)");
            Console.WriteLine("Poznámka: \"Ne\" volte v případě, že chcete odpovědět jinak.");
            Console.Write("Odpvověď: ");
            try
            {
                znak = char.Parse(Console.ReadLine());

                switch (znak)
                {
                    case 'a':
                    case 'A':
                        znak = Znamenko();
                        Zapamatovat(string.Concat(odp, "§", znak, "§"), odpovedi);    // Odpovědi ukládáme ve tvaru: "Blablablablabla§znamenko§"
                        Console.WriteLine();
                        Console.WriteLine("Děkuji, naučil jsem se novou odpověď. :)");
                        Console.WriteLine("Naučeno: " + odp);
                        Console.WriteLine("Hodnocení: " + znak);
                        PAKtC();
                        break;
                    case 'n':
                    case 'N':
                        Console.WriteLine("Aha, tady máš možnost odpovědět znovu.");
                        PAKtC();
                        Odpovezeno(dotaz);
                        break;
                    default:
                        Console.WriteLine("Neplatný znak.");
                        Console.WriteLine("Zkuste to znovu.");
                        PAKtC();
                        MamZapamatovatOdpoved(dotaz, odp);
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Neplatný vstup, nerozumím.");
                Console.WriteLine("Zkuste to znovu.");
                PAKtC();
                MamZapamatovatOdpoved(dotaz, odp);
            }
        }

        // Program si zapamatuje to, co si má zapamatovat.
        public static void Zapamatovat(string co, string kam)
        {
            using (StreamWriter sw = new StreamWriter(kam, append: true))  // Budeme přidávat!
            {
                sw.WriteLine(co);   // Každý údaj bude na jednom řádku.
            }           
        }
        
        // Zjistí, jakého rázu odpověď je (*/+/0/-/x) a vrátí takový znak.
        public static char Znamenko()
        {
            char ch = '\0'; // Znak #0.
            Console.Clear();
            Console.WriteLine("Jak moc pozitivní odpověď je?");
            Console.WriteLine("*: hodně pozitivní, +: pozitivní, 0: neutrální, -: negativní, x: hodně negativní");
            Console.Write("Odpověď: ");
            try
            {
                ch = char.Parse(Console.ReadLine());

                switch (ch)
                {
                    case '*':
                        ch = '*';
                        // Hodně pozitivní.
                        PAKtC();
                        break;
                    case '+':
                        ch = '+';
                        // Pozitivní.
                        break;
                    case '0':
                    case 'O':
                    case 'o':
                        ch = '0';
                        // Neutrální.
                        break;
                    case '-':
                    case '–':
                        ch = '-';
                        // Negativní.
                        break;
                    case '×':
                    case 'x':
                    case 'X':
                        ch = 'x';
                        // Hodně negativní.
                        break;
                    default:
                        Console.WriteLine("Neplatný znak.");
                        Console.WriteLine("Zkuste to znovu.");
                        PAKtC();
                        Znamenko();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Neplatný vstup, nerozumím.");
                Console.WriteLine("Zkuste to znovu.");
                PAKtC();
                Znamenko();
            }
            return ch;
        }

        // Metoda Main().
        static void Main(string[] args)
        {
            // Přihlášení.
            UzivatelskeJmeno();
            Overeni();
            if (pswCreate)
            {
                VytvorHeslo();  // Poprvé.
            }

            // Povídání.
            Uvod();

            // Načíst počet možných otázek pomocí počítadla a while NOT eof(otazky)  do... (pouze jednou - na začátku programu)
            // Pak vytvořit seznam těchto indexů řádku v souboru - 1.
            // Vytvořit metodu na losování otázky. Po vylosování se zeptá, zajistí reakci na odpověď a pak otazku ze seznamu.

            // Otázka, odpověď, reakce...
            Console.Clear();
            Console.WriteLine("Jak se máš?");
            Odpovezeno("Jak se máš?");

            // PŘIDAT FENIX.TXT, EVELYNN.TXT, pak kejsy (case)!

            Console.ReadKey();
        }
    }
}
