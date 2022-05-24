using System;

namespace Zvěřinec
{
    abstract class Zvíře
    {
        public readonly string Jméno = ""; // Nemá jméno / nevíme.
        public int Věk { get; protected set; } = 0;   // Narodil se.
        public int Hmotnost { get; protected set; } = 0; // Nevím.

        protected static Random random = new Random();

        public abstract void UdělejZvuk();

        public virtual void PřibralČiShodil(int změnaHmotnosti)
        {
            int nováHmotnost = this.Hmotnost + změnaHmotnosti;

            if (nováHmotnost <= 0)
                return;     // Nelze.

            this.Hmotnost = nováHmotnost;
        }

        public virtual void Zestárl(int oKolik)
        {
            if (oKolik <= 0)
                return;     // Nelze omládnout.

            this.Věk += oKolik;
        }

        public void Skoč()
        {
            Console.WriteLine("Hop.");
        }

        public Zvíře()
        {
            // Aby mohl existovat bezparametrický konstruktor u potomků.
        }

        protected Zvíře(string jméno)
        {
            this.Jméno = jméno; // Řešímě problém s readonly.
        }
    }

    class Pes : Zvíře
    {
        public bool Štěká { get; }        

        public override void UdělejZvuk()
        {
            Console.WriteLine("Haf.");            
        }

        public override void Zestárl(int oKolik)
        {
            base.Zestárl(7 * oKolik);   // Psi stárnou sedmkrát rychleji než lidi.
        }       
        
        public Pes()
        {
            this.Štěká = Zvíře.random.Next(0, 2) == 1;
        }

        public Pes(string jméno) : base(jméno)
        {
            this.Štěká = Zvíře.random.Next(0, 2) == 1;  // Direktiva this volá base, ale this(jméno) nelze zde použít.
        }

        public Pes(string jméno, int věk, int hmotnost) : this (jméno)
        {
            // Nelze záporné hodnoty!
            this.Věk = věk > 0 ? věk: 0;
            this.Hmotnost = hmotnost > 0 ? hmotnost : 0;
        }
    }

    class Želva : Zvíře
    {
        public override void UdělejZvuk()
        {
            Console.WriteLine("*Želví zvuky.*");
        }

        new public void Skoč()  // Překrytí! Záleží na typu!
        {
            Console.WriteLine("... (želvy neumí skákat)");
        }

        public Želva()
        {
            // Pouze povolujeme takovýto konstruktor.
        }

        public Želva(string jméno) : base(jméno)
        {
            // Volá se base(jméno) a nic víc neděláme.
        }

        public Želva(string jméno, int věk, int hmotnost) : this(jméno)
        {
            // Nelze záporné hodnoty!
            this.Věk = věk > 0 ? věk : 0;
            this.Hmotnost = hmotnost > 0 ? hmotnost : 0;

            // Poznámka: Kód musíme kopírovat... Lepší je, když je přímo v base class Zvíře.
        }
    }
        

    class Program
    {
        public static void Main(string[] args)
        {
            #region Polymorfismus
            /**/
            Zvíře[] zvěřinec = {
                new Pes("Alík", 13, -28),
                new Pes("Brok", 25, 17),
                new Pes("Cypísek"),
                new Pes(),
                new Želva("Želvička", -50, 45),
                new Želva("Krasavička", 20, 62),
                new Želva("Roztomilka"),
                new Želva()
                };

            foreach (Zvíře zvíře in zvěřinec)
            {
                Console.WriteLine("Toto zvíře je {0}.", zvíře.GetType());
                Console.WriteLine(zvíře.Jméno == "" ? "Bohužel neznáme jeho jméno." : "Jmenuje se {0}.", zvíře.Jméno);
                Console.WriteLine(zvíře.Věk == 0 ? "Bohužel neznáme jeho věk." : "Má {0} let.", zvíře.Věk);
                Console.WriteLine(zvíře.Hmotnost == 0 ? "Bohužel neznáme jeho hmotnost." : "Má hmotnost {0} kg.", zvíře.Hmotnost);
                Console.WriteLine("Takto skáče, když je {0}:", nameof(Zvíře));
                zvíře.Skoč();                
                if (zvíře is Pes)   // Jinak taky zvíře.GetType() == typeof(Pes).
                {
                    Console.WriteLine("Takto skáče, když je {0}:", nameof(Pes));
                    ((Pes)zvíře).Skoč();    // Explicitní konverze. (Víme už, že je typu Pes.)
                }
                else if (zvíře is Želva)   // Jinak taky zvíře.GetType() == typeof(Želva).
                {
                    Console.WriteLine("Takto skáče, když je {0}:", nameof(Želva));
                    ((Želva)zvíře).Skoč();    // Explicitní konverze. (Víme už, že je typu Želva.)
                }
                Console.WriteLine("A nakonec pro nás udělá zvuk:");
                zvíře.UdělejZvuk();
                
                Console.WriteLine();
            }
            Console.WriteLine("To byla všechna naše zvířátka.");
            Console.WriteLine("Děkujeme za návštěvu. Přijďte zas! :-)");
            /**/
            #endregion  // On
            #region Překrytí
            /*/
            Želva želva1 = new Želva("Želva 1");
            Zvíře želva2 = new Želva("Želva 2");

            // Překrytí:
            Console.WriteLine("Želvy skáčou:");
            Console.WriteLine("{0} skáče:", želva1.Jméno);
            želva1.Skoč();
            Console.WriteLine("Její typ: {0}", želva1.GetType());
            Console.WriteLine("Deklarovaný typ její proměnné: {0}", typeof(Želva));            
            Console.WriteLine();
            Console.WriteLine("{0} skáče:", želva2.Jméno);
            želva2.Skoč();
            Console.WriteLine("Její typ: {0}", želva2.GetType());
            Console.WriteLine("Deklarovaný typ její proměnné: {0}", typeof(Zvíře));
            Console.WriteLine();
            /*/
            #endregion       // Off

            Console.WriteLine();
            Console.Write("Zmáčknutím libovolné klávesy ukončíte program... ");
            Console.ReadKey();
        }
    }
}
