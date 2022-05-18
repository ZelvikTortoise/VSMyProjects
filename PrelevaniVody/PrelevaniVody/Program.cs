using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrelevaniVody
{
    class Ctecka  // upravená třída Ctecka od pana doktora Holana.
    {
        private static char? lastChar = null;

        public static int ReadNum()
        {
            int z;
            bool negative = false;

            if (lastChar.HasValue)
                z = (char)lastChar;
            else
                z = Console.Read();

            while ((z < '0') || (z > '9'))
            {
                if (z == '-')
                    negative = true;    // Another z should break the cycle.

                z = Console.Read();
            }

            int x = 0;
            while ((z >= '0') && (z <= '9'))
            {
                x = 10 * x + z - '0';
                z = Console.Read();
            }

            if (z != -1)
                lastChar = (char)z;
            else
                lastChar = null;

            if (negative)
                return -x;
            else
                return x;
        }
    }

    struct DosazitelneMnozstvi
    {
        public int Vody;
        public int Kroku;
        
        public bool StejneVody(int kolik)
        {
            return this.Vody == kolik;
        }

        private static void Vymen(ref DosazitelneMnozstvi a, ref DosazitelneMnozstvi b)
        {
            DosazitelneMnozstvi pom = a;
            a = b;
            b = pom;            
        }

        public static void SeradSeznamVzestupnePodleObjemu(List<DosazitelneMnozstvi> seznam)
        {
            if (seznam == null)
                return;


            // BubbleSort:
            bool done = false;
            while (!done)
            {
                done = true;    // Pokud se nic nezmeni, neni potreba dal bublat.
                for (int j = 0; j < seznam.Count - 1; j++)
                {
                    if (seznam[j].Vody > seznam[j + 1].Vody)
                    {
                        DosazitelneMnozstvi pom = seznam[j];
                        seznam[j] = seznam[j + 1];
                        seznam[j + 1] = pom;
                        done = false;   // Jeste se neco zmenilo.
                    }
                }
            }           
        }

        public DosazitelneMnozstvi(int vody, int kroku)
        {
            this.Vody = vody;
            this.Kroku = kroku;
        }
    }

    class Stav
    {
        public Nadoba Prvni { get; }
        public Nadoba Druha { get; }
        public Nadoba Treti { get; }
        public int Faze { get; }        // Nejmensi pocet preliti, abychom se dostali z pocatecniho stavu do soucasneho.

        /// <summary>
        /// Zkontroluje seznam "moznosti" a pokud v nem neni zaznam nektere z objemu, ktery se v tomto stavu nachazi
        /// v nejake z nadob, zaznam tam prida a vrati true. Pokud nic neprida, vrati false.
        /// </summary>
        /// <param name="moznosti">Seznam jiz nalezenych objemu vody v nejake nadobe (spolu s nemensim poctem kroku)</param>
        /// <returns>Byl seznam "moznosti" rozsiren?</returns>
        public void ZkontrolujADopln(List<DosazitelneMnozstvi> moznosti)
        {
            List<Nadoba> listNadob = new List<Nadoba> { this.Prvni, this.Druha, this.Treti };

            bool nalezeno = false;
            foreach (Nadoba nad in listNadob)
            {
                foreach (DosazitelneMnozstvi zjisteno in moznosti)
                {
                    if (zjisteno.StejneVody(nad.Vody))
                    {
                        nalezeno = true;
                        break;
                    }                        
                }
                if (!nalezeno)
                    moznosti.Add(new DosazitelneMnozstvi(nad.Vody, this.Faze));

                else
                    nalezeno = false;
            }
        }

        public static bool Ekvivalentni(Stav a, Stav b)
        {
            List<Nadoba> listA = new List<Nadoba> { a.Prvni, a.Druha, a.Treti };
            List<Nadoba> listB = new List<Nadoba> { b.Prvni, b.Druha, b.Treti };

            bool ekv = false;
            foreach(Nadoba nadA in listA)
            {
                ekv = false;
                foreach(Nadoba nadB in listB)
                {
                    if (Nadoba.Ekvivalentni(nadA, nadB))
                    {
                        ekv = true;
                        listB.Remove(nadB);
                        break;
                    }
                }
                if (!ekv)
                    break;
            }

            return ekv;
        }

        private Stav VytvorStavPrelitim(Nadoba odkud, Nadoba kam, Nadoba stejna)
        {
            Nadoba a = odkud.VytvorKopii();
            Nadoba b = kam.VytvorKopii();
            Nadoba c = stejna.VytvorKopii();

            Nadoba.PrelejVodu(a, b);

            return new Stav(a, b, c, this.Faze + 1);    // Generujeme stavy prelevanim -> faze se zvysuje.
        }

        /// <summary>
        /// Vraci pole sesti stavu, ktere muzeme dostat na jedno preliti z jedne nadoby do druhe.
        /// </summary>
        /// <returns>Stav[6]</returns>
        public Stav[] GenerujNoveStavy()
        {
            Stav[] noveStavy = new Stav[6];

            // Prelevame vzdycky z jine do jine nadoby v kazdem stavu stavoveho prostoru:
            noveStavy[0] = VytvorStavPrelitim(this.Prvni, this.Druha, this.Treti);  // z 1. do 2.
            noveStavy[1] = VytvorStavPrelitim(this.Druha, this.Prvni, this.Treti);  // z 2. do 1.
            noveStavy[2] = VytvorStavPrelitim(this.Prvni, this.Treti, this.Druha);  // z 1. do 3.
            noveStavy[3] = VytvorStavPrelitim(this.Treti, this.Prvni, this.Druha);  // z 3. do 1.
            noveStavy[4] = VytvorStavPrelitim(this.Druha, this.Treti, this.Prvni);  // z 2. do 3.
            noveStavy[5] = VytvorStavPrelitim(this.Treti, this.Druha, this.Prvni);  // z 3. do 2.

            return noveStavy;
        }

        public Stav(Nadoba a, Nadoba b, Nadoba c, int faze)
        {
            this.Prvni = a;
            this.Druha = b;
            this.Treti = c;
            this.Faze = faze;
        }
    }

    class Nadoba
    {
        public int Kapacita { get; }
        public int Vody { get; private set; }

        public static bool Ekvivalentni(Nadoba a, Nadoba b)
        {
            return a.Kapacita == b.Kapacita && a.Vody == b.Vody;
        }

        public static void PrelejVodu(Nadoba odkud, Nadoba kam)
        {
            int misto = kam.Kapacita - kam.Vody;
            if (misto >= odkud.Vody)    // Prelejeme vsechno
            {
                kam.Vody += odkud.Vody;
                odkud.Vody = 0;
            }
            else   // Naplnili jsme nadobu a zbylo nam
            {
                odkud.Vody -= misto;
                kam.Vody = kam.Kapacita;
            }
        }

        public Nadoba VytvorKopii()
        {
            return new Nadoba(this.Kapacita, this.Vody);
        }

        public Nadoba(int kapacita, int vody)
        {
            this.Kapacita = kapacita;
            this.Vody = vody;
        }
    }

    class Program
    {
        static readonly Queue<Stav> NeprohledaneStavy = new Queue<Stav>();
        static readonly List<Stav> ProhledaneStavy = new List<Stav>();
        static readonly List<DosazitelneMnozstvi> moznosti = new List<DosazitelneMnozstvi>();

        static void Main(string[] args)
        {
            const char oddelovac = ':';
            const char mezera = ' ';
            int kapA = Ctecka.ReadNum();
            int kapB = Ctecka.ReadNum();
            int kapC = Ctecka.ReadNum();
            int vodyA = Ctecka.ReadNum();
            int vodyB = Ctecka.ReadNum();
            int vodyC = Ctecka.ReadNum();

            NeprohledaneStavy.Enqueue(new Stav(new Nadoba(kapA, vodyA), new Nadoba(kapB, vodyB), new Nadoba(kapC, vodyC), 0));

            while (NeprohledaneStavy.Count != 0)
            {
                Stav stav = NeprohledaneStavy.Dequeue();
                ProhledaneStavy.Add(stav);
                stav.ZkontrolujADopln(moznosti);

                Stav[] noveStavy = stav.GenerujNoveStavy();
                for (int i = 0; i < noveStavy.Length; i++)  // noveStavy.Length == 6
                {
                    noveStavy[i].ZkontrolujADopln(moznosti);

                    bool pridat = true;
                    foreach (Stav s in ProhledaneStavy)
                    {
                        if (Stav.Ekvivalentni(s, noveStavy[i]))
                        {
                            pridat = false;
                            break;
                        }                            
                    }
                    if (pridat)
                        NeprohledaneStavy.Enqueue(noveStavy[i]);
                }
            }

            DosazitelneMnozstvi.SeradSeznamVzestupnePodleObjemu(moznosti);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < moznosti.Count; i++)
            {
                sb.Append(moznosti[i].Vody);
                sb.Append(oddelovac);
                sb.Append(moznosti[i].Kroku);
                sb.Append(mezera);
            }
            sb.Remove(sb.Length - 1, 1);    // Odstranime posledni mezeru.

            Console.WriteLine(sb.ToString());
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
