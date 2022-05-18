using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;



namespace CSHra
{

    abstract class Prvek
    {
        public Mapa mapa;
        public int x;
        public int y;
        public virtual void UdelejCoUmis()
        {
        }
    }

    abstract class PohyblivyPrvek : Prvek
    {
        public int Smer;
        public override abstract void UdelejCoUmis();
    }
    
    enum StisknutaSipka { zadna, doleva, nahoru, doprava, dolu };

    enum StisknutaBomba { zadna, poloz, odpal };

    public enum Stav { nezacala, bezi, vyhra, prohra };

    

    class Hrdina : PohyblivyPrvek
    {
        
        public int bomb;
        public int dostrel;
        public int rychlost;
        public int zivotu;
        public bool posouva;
        public bool hazi;
        public bool odpaluje;
        public StisknutaSipka stisknutaSipka;
        public StisknutaBomba stisknutaBomba;

        public Hrdina(Mapa mapa, int kdex, int kdey, char charSmer, int bomb, int dostrel, int rychlost, int zivotu, bool posouva, bool hazi, bool odpaluje, StisknutaSipka stisknutaSipka, StisknutaBomba stisknutaBomba)
        {
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;

            Smer = "<^>v".IndexOf(charSmer);

            this.bomb = bomb;
            this.dostrel = dostrel;
            this.rychlost = rychlost;
            this.zivotu = zivotu;

            this.posouva = posouva;
            this.hazi = hazi;
            this.odpaluje = odpaluje;
        }

        public override void UdelejCoUmis()
        {
            ObsluzBomby();

            UdelejKrok();
        }

        private void ObsluzBomby()
        {
            switch (this.stisknutaBomba)
            {
                case StisknutaBomba.zadna:
                    break;
                case StisknutaBomba.poloz:
                    if (this.bomb > 0 && Mapa.plan[x, y] != 'k' && Mapa.plan[x, y] != 's')
                    {
                        mapa.VytvorAVykresliBombu(mapa, x, y, this);
                    }
                    break;

                case StisknutaBomba.odpal:
                    if (this.odpaluje)
                    {
                        for (int i = 0; i < Mapa.Bomby.Count; i++)
                        {
                            Bomba p = Mapa.Bomby.ElementAt(i);

                            if (p.autor == this) { p.UdelejCoUmis(); }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void UdelejKrok()
        {
            int nove_x = x;
            int nove_y = y;

            switch (this.stisknutaSipka)
            {
                case StisknutaSipka.zadna:
                    break;
                case StisknutaSipka.doleva:
                    nove_x--;
                    break;
                case StisknutaSipka.nahoru:
                    nove_y--;
                    break;
                case StisknutaSipka.doprava:
                    nove_x++;
                    break;
                case StisknutaSipka.dolu:
                    nove_y++;
                    break;
                default:
                    break;
            }

           
            if (mapa.JeVolnoNeboBomba(nove_x, nove_y))
            {
                if (this.posouva && mapa.JeBomba(nove_x, nove_y))
                { PosliBombu(x, y, nove_x, nove_y); }
                mapa.Presun(x, y, nove_x, nove_y); 
            }

            if (mapa.JeItem(nove_x, nove_y))
            {
                SeberItem(nove_x, nove_y);
                mapa.Presun(x, y, nove_x, nove_y); 
            }

            if (mapa.JeOtevrenyVychod(nove_x, nove_y))
            {
                Sounds.soundWin.Play();
                mapa.stav = Stav.vyhra;
            }
        }

        private void PosliBombu(int x, int y, int nove_x, int nove_y)
        {
            if (mapa.JeVolno(2 * nove_x - x, 2 * nove_y - y))
            {
                mapa.Presun(nove_x, nove_y, 2 * nove_x - x, 2 * nove_y - y);
            }
        }
       
        private void SeberItem(int x, int y)
        {
            Sounds.soundItem.Play();
            switch (Mapa.plan[x, y])
            {
                case '0':
                    this.bomb++;
                    break;
                case '1':
                    this.dostrel++;
                    break;
                case '2':
                    this.rychlost++;
                    break;
                case '3':
                    this.odpaluje = true;
                    break;
                case '4':
                    this.posouva = true;
                    break;
                case '5':
                    this.zivotu++;
                    break;
                default:
                    break;
            }
            mapa.ZrusItem(x, y);
        }
    }

    class Prisera : PohyblivyPrvek
    {

        
        public Prisera(Mapa mapa, int kdex, int kdey, char charSmer)
        {
            this.mapa = mapa;
            this.x = kdex;
            this.y = kdey;

            Smer = "<^>v".IndexOf(charSmer);
        }
        
        public override void UdelejCoUmis()
        {
            // ###########################################################
            // ...tady neco schazi... 
            switch (Smer)
            {
                case 0:
                    PohniPriserouOJedenKrok(this.x, this.y, 4);
                    break;
                case 1:
                    PohniPriserouOJedenKrok(this.x, this.y, 8);
                    break;
                case 2:
                    PohniPriserouOJedenKrok(this.x, this.y, 6);
                    break;
                case 3:
                    PohniPriserouOJedenKrok(this.x, this.y, 2);
                    break;
            }



            // ###########################################################
        }

        public static char[,] bludiste = new char[Mapa.plan.GetLength(0), Mapa.plan.GetLength(1)];

        public void PohniPriserouOJedenKrok(int X, int Y, int O)
        {
            for (int i = 0; i < Mapa.plan.GetLength(0); i++)
            {
                for (int j = 0; j < Mapa.plan.GetLength(1); j++)
                {
                    switch (Mapa.plan[i, j])
                    {
                        case ' ':
                        case 'k':
                        case 'K':
                        case 's':
                        case 'S':

                            bludiste[i, j] = '.';
                            break;
                        default:
                            bludiste[i, j] = 'X';
                            break;

                    }
                }
            }

            switch (O)
            {
                //příšera otočená doprava
                case 6:
                    // před příšerou volno a po pravé straně zeď -> posun vpřed
                    if (bludiste[X + 1, Y] == '.' && bludiste[X, Y + 1] == 'X')
                    {
                        bludiste[X, Y] = '.'; bludiste[X + 1, Y] = '>'; if (this.mapa.JeHrdina(this.x + 1, this.y))
                        {
                            mapa.stav = Stav.prohra;
                        }
                        this.mapa.Presun(this.x, this.y, this.x + 1, this.y);
                    }
                    // před příšerou volno a po pravé straně volno -> pokračování rovně pokud přijde stěna, jinak otočení dolu
                    if (bludiste[X + 1, Y] == '.' && bludiste[X, Y + 1] == '.')
                    {
                        if (bludiste[X + 1, Y + 1] == 'X')
                        {
                            bludiste[X, Y] = '.'; bludiste[X + 1, Y] = '>'; if (this.mapa.JeHrdina(this.x + 1, this.y))
                            {
                                mapa.stav = Stav.prohra;
                            }
                            this.mapa.Presun(this.x, this.y, this.x + 1, this.y); ;
                        }
                        else
                        {
                            bludiste[X, Y] = 'v'; this.Smer = 3;
                        }
                    }
                    // před příšerou zeď a po pravé straně zeď -> otočení nahoru
                    if (bludiste[X + 1, Y] == 'X' && bludiste[X, Y + 1] == 'X') { bludiste[X, Y] = '^'; this.Smer = 1; }
                    // před příšerou zeď a po pravé straně volno -> otočení dolu
                    if (bludiste[X + 1, Y] == 'X' && bludiste[X, Y + 1] == '.') { bludiste[X, Y] = 'v'; this.Smer = 3; }
                    break;

                //příšera otočená nahoru
                case 8:
                    // před příšerou volno a po pravé straně zeď -> posun vpřed
                    if (bludiste[X, Y - 1] == '.' && bludiste[X + 1, Y] == 'X')
                    {
                        bludiste[X, Y] = '.'; bludiste[X, Y - 1] = '^'; if (this.mapa.JeHrdina(this.x, this.y - 1))
                        {
                            mapa.stav = Stav.prohra;
                        }
                        this.mapa.Presun(this.x, this.y, this.x, this.y - 1);
                    }
                    // před příšerou volno a po pravé straně volno -> pokračování rovně pokud přijde stěna, jinak otočení doprava
                    if (bludiste[X, Y - 1] == '.' && bludiste[X + 1, Y] == '.')
                    {
                        if (bludiste[X + 1, Y - 1] == 'X')
                        {
                            bludiste[X, Y] = '.'; bludiste[X, Y - 1] = '^'; if (this.mapa.JeHrdina(this.x, this.y - 1))
                            {
                                mapa.stav = Stav.prohra;
                            }
                            this.mapa.Presun(this.x, this.y, this.x, this.y - 1);
                        }
                        else
                        {
                            bludiste[X, Y] = '>'; this.Smer = 2;
                        }
                    }
                    // před příšerou zeď a po pravé straně zeď -> otočení doleva
                    if (bludiste[X, Y - 1] == 'X' && bludiste[X + 1, Y] == 'X') { bludiste[X, Y] = '<'; this.Smer = 0; }
                    // před příšerou zeď a po pravé straně volno -> otočení doprava
                    if (bludiste[X, Y - 1] == 'X' && bludiste[X + 1, Y] == '.') { bludiste[X, Y] = '>'; this.Smer = 2; }
                    break;

                //příšera otočená doleva
                case 4:
                    // před příšerou volno a po pravé straně zeď -> posun vpřed
                    if (bludiste[X - 1, Y] == '.' && bludiste[X, Y - 1] == 'X')
                    {
                        bludiste[X, Y] = '.'; bludiste[X - 1, Y] = '<'; if (this.mapa.JeHrdina(this.x - 1, this.y))
                        {
                            mapa.stav = Stav.prohra;
                        }
                        this.mapa.Presun(this.x, this.y, this.x - 1, this.y);
                    }
                    // před příšerou volno a po pravé straně volno -> pokračování rovně pokud přijde stěna, jinak otočení nahoru
                    if (bludiste[X - 1, Y] == '.' && bludiste[X, Y - 1] == '.')
                    {
                        if (bludiste[X - 1, Y - 1] == 'X')
                        {
                            bludiste[X, Y] = '.'; bludiste[X - 1, Y] = '<'; if (this.mapa.JeHrdina(this.x - 1, this.y))
                            {
                                mapa.stav = Stav.prohra;
                            }
                            this.mapa.Presun(this.x, this.y, this.x - 1, this.y);
                        }
                        else
                        {
                            bludiste[X, Y] = '^'; this.Smer = 1;
                        }
                    }
                    // před příšerou zeď a po pravé straně zeď -> otočení dolu
                    if (bludiste[X - 1, Y] == 'X' && bludiste[X, Y - 1] == 'X') { bludiste[X, Y] = 'v'; this.Smer = 3; }
                    // před příšerou zeď a po pravé straně volno -> otočení nahoru
                    if (bludiste[X - 1, Y] == 'X' && bludiste[X, Y - 1] == '.') { bludiste[X, Y] = '^'; this.Smer = 1; }
                    break;

                //příšera otočená dolu
                case 2:
                    // před příšerou volno a po pravé straně zeď -> posun vpřed
                    if (bludiste[X, Y + 1] == '.' && bludiste[X - 1, Y] == 'X')
                    {
                        bludiste[X, Y] = '.'; bludiste[X, Y + 1] = 'v'; if (this.mapa.JeHrdina(this.x, this.y + 1))
                        {
                            mapa.stav = Stav.prohra;
                        }
                        this.mapa.Presun(this.x, this.y, this.x, this.y + 1);
                    }
                    // před příšerou volno a po pravé straně volno -> pokračování rovně pokud přijde stěna, jinak otočení doleva
                    if (bludiste[X, Y + 1] == '.' && bludiste[X - 1, Y] == '.')
                    {
                        if (bludiste[X - 1, Y + 1] == 'X')
                        {
                            bludiste[X, Y] = '.'; bludiste[X, Y + 1] = 'v'; if (this.mapa.JeHrdina(this.x, this.y + 1))
                            {
                                mapa.stav = Stav.prohra;
                            }
                            this.mapa.Presun(this.x, this.y, this.x, this.y + 1);
                        }
                        else
                        {
                            bludiste[X, Y] = '<'; this.Smer = 0;
                        }
                    }
                    // před příšerou zeď a po pravé straně zeď -> otočení doprava
                    if (bludiste[X, Y + 1] == 'X' && bludiste[X - 1, Y] == 'X') { bludiste[X, Y] = '>'; this.Smer = 2; }
                    // před příšerou zeď a po pravé straně volno -> otočení doleva
                    if (bludiste[X, Y + 1] == 'X' && bludiste[X - 1, Y] == '.') { bludiste[X, Y] = '<'; this.Smer = 0; }
                    break;
            }
        }

    }

    class Barel : Prvek
    {
        readonly int skryva;

        public Barel(Mapa mapa, int x, int y, int skryva)
        {
            this.x = x;
            this.y = y;
            this.mapa = mapa;
            this.skryva = skryva; //boty jsou 2
        }
        
        public void UdelejCoUmis(char typ, Bomba autor)
        {
            if (skryva >= 0) { mapa.ZrusBarel(x, y); mapa.VytvorAVykresliItem(mapa, x, y, skryva); }
            else { mapa.ZrusBarel(x, y); mapa.VytvorAVykresliBlesk(mapa, x, y, autor, typ); }
        }
        
    }

    class Bomba : Prvek
    {
        
        public Hrdina autor;
        //dostrel
        public int odpocet;
        public Bomba(Mapa mapa, int x, int y, Hrdina hrdina, int odpocet)
        {
            this.mapa = mapa;
            this.x = x;
            this.y = y;
            this.autor = hrdina;
            this.odpocet = odpocet;
        }
        
        public override void UdelejCoUmis()
        {

            Sounds.soundBomb.Play();

            mapa.VytvorAVykresliBlesk(mapa, x, y, this, 'g');

            for (int i = y + 1; i <= y + autor.dostrel; i++)
            {
                if (mapa.JeVolno(x, i) || mapa.JeHrdinaBezBomby(x, i) || mapa.JeHrdinaSBombou(x, i))
                {
                    if (i != y + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 'n');
                    if (i == y + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 'b');
                }
                if (mapa.JeBomba(x, i))
                {
                    for (int j = 0; j < Mapa.Bomby.Count; j++)
                    {
                        if ((Mapa.Bomby[j].x == x) && (Mapa.Bomby[j].y == i))
                        {
                            Mapa.Bomby[j].UdelejCoUmis();
                            break;
                        }
                    }
                }
                if (mapa.JeBarel(x, i))
                {
                    for (int j = 0; j < Mapa.Barely.Count; j++)
                    {
                        if ((Mapa.Barely[j].x == x) && (Mapa.Barely[j].y == i))
                        {
                            Mapa.Barely[j].UdelejCoUmis('b', this);
                            
                            break;
                        }
                    }
                    i = y + autor.dostrel + 1;
                    break;
                }
                if (mapa.JeZed(x, i))
                {
                    i = y + autor.dostrel + 1;
                    break;
                }
                if (mapa.JeItem(x, i))
                {
                    mapa.ZrusItem(x, i);
                    if (i != y + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 'n');
                    if (i == y + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 'b');
                    break;
                }
                if (mapa.JePrisera(x, i))
                {
                    mapa.ZrusPriseru(x, i);
                    break;
                }
            }

            for (int i = x + 1; i <= x + autor.dostrel; i++)
            {
                if (mapa.JeVolno(i, y) || mapa.JeHrdinaBezBomby(i, y) || mapa.JeHrdinaSBombou(i, y))
                {
                    if (i != x + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'z');
                    if (i == x + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'h');
                }
                if (mapa.JeBomba(i, y))
                {
                    for (int j = 0; j < Mapa.Bomby.Count; j++)
                    {
                        if ((Mapa.Bomby[j].x == i) && (Mapa.Bomby[j].y == y))
                        {
                            Mapa.Bomby[j].UdelejCoUmis();
                            break;
                        }
                    }
                }
                if (mapa.JeBarel(i, y))
                {
                    for (int j = 0; j < Mapa.Barely.Count; j++)
                    {
                        if ((Mapa.Barely[j].x == i) && (Mapa.Barely[j].y == y))
                        {
                            Mapa.Barely[j].UdelejCoUmis('h', this);
                            break;
                        }
                    }
                    i = x + autor.dostrel + 1;
                    break;
                }
                if (mapa.JeZed(i, y))
                {
                    i = x + autor.dostrel + 1;
                    break;
                }
                if (mapa.JeItem(i, y))
                {
                    mapa.ZrusItem(i, y);
                    if (i != x + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'z');
                    if (i == x + autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'h');
                    break;
                }
                if (mapa.JePrisera(i, y))
                {
                    mapa.ZrusPriseru(i, y);
                    break;
                }
            }

            for (int i = y - 1; i >= y - autor.dostrel; i--)
            {
                if (mapa.JeVolno(x, i) || mapa.JeHrdinaBezBomby(x, i) || mapa.JeHrdinaSBombou(x, i))
                {
                    if (i != y - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 'n');
                    if (i == y - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 't');
                }
                if (mapa.JeBomba(x, i))
                {
                    for (int j = 0; j < Mapa.Bomby.Count; j++)
                    {
                        if ((Mapa.Bomby[j].x == x) && (Mapa.Bomby[j].y == i))
                        {
                            Mapa.Bomby[j].UdelejCoUmis();
                            break;
                        }
                    }
                }
                if (mapa.JeBarel(x, i))
                {
                    for (int j = 0; j < Mapa.Barely.Count; j++)
                    {
                        if ((Mapa.Barely[j].x == x) && (Mapa.Barely[j].y == i))
                        {
                            Mapa.Barely[j].UdelejCoUmis('t', this);
                            break;
                        }
                    }
                    i = y - autor.dostrel - 1;
                    break;
                }
                if (mapa.JeZed(x, i))
                {
                    i = y - autor.dostrel - 1;
                    break;
                }
                if (mapa.JeItem(x, i))
                {
                    mapa.ZrusItem(x, i);
                    if (i != y - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 'n');
                    if (i == y - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, x, i, this, 't');
                    break;
                }
                if (mapa.JePrisera(x, i))
                {
                    mapa.ZrusPriseru(x, i);
                    break;
                }
            }

            for (int i = x - 1; i >= x - autor.dostrel; i--)
            {
                if (mapa.JeVolno(i, y) || mapa.JeHrdinaBezBomby(i, y) || mapa.JeHrdinaSBombou(i, y))
                {
                    if (i != x - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'z');
                    if (i == x - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'f');
                }
                if (mapa.JeBomba(i, y))
                {
                    for (int j = 0; j < Mapa.Bomby.Count; j++)
                    {
                        if ((Mapa.Bomby[j].x == i) && (Mapa.Bomby[j].y == y))
                        {
                            Mapa.Bomby[j].UdelejCoUmis();
                            break;
                        }
                    }
                }
                if (mapa.JeBarel(i, y))
                {
                    for (int j = 0; j < Mapa.Barely.Count; j++)
                    {
                        if ((Mapa.Barely[j].x == i) && (Mapa.Barely[j].y == y))
                        {
                            Mapa.Barely[j].UdelejCoUmis('f', this);
                            break;
                        }
                    }
                    i = x - autor.dostrel - 1;
                    break;
                }
                if (mapa.JeZed(i, y))
                {
                    i = x - autor.dostrel - 1;
                    break;
                }
                if (mapa.JeItem(i, y))
                {
                    mapa.ZrusItem(i, y);
                    if (i != x - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'z');
                    if (i == x - autor.dostrel) mapa.VytvorAVykresliBlesk(mapa, i, y, this, 'f');
                    break;
                }
                if (mapa.JePrisera(i, y))
                {
                    mapa.ZrusPriseru(i, y);
                    break;
                }
            }

            autor.bomb++; 
        }
    }

    class Item : Prvek
    {
        public int odpocet;
        public int typ;
        public Item(Mapa mapa, int x, int y, int odpocet, int typ)
        {
            this.mapa = mapa;
            this.x = x;
            this.y = y;
            this.odpocet = odpocet;
            this.typ = typ;
        }

        public override void UdelejCoUmis()
        {
            mapa.ZrusItem(x, y);
        }
    }

    class Blesk : Prvek
    {
        public int odpocet;
        public char typ;
        public Bomba autor;
        public Hrdina prekryva;
        public Blesk(Mapa mapa, int x, int y, Bomba autor, int odpocet, char typ, Hrdina prekryva)
        {
            this.mapa = mapa;
            this.x = x;
            this.y = y;
            this.odpocet = odpocet;
            this.typ = typ;
            this.autor = autor;
            this.prekryva = prekryva;
        }

        public override void UdelejCoUmis()
        {
            mapa.ZrusBlesk(x, y);
        }
    }

    class Sounds
    {
        public static SoundPlayer soundItem = new SoundPlayer("sounds/item.wav");

        public static SoundPlayer soundBomb = new SoundPlayer("sounds/bomb.wav");

        public static SoundPlayer soundWin = new SoundPlayer("sounds/win.wav");

        public static SoundPlayer soundMonsterDeath = new SoundPlayer("sounds/monsterdeath.wav");

        public static SoundPlayer soundPlayerBombDeath = new SoundPlayer("sounds/playerbombdeath.wav");
    }

    class Mapa
    {
        readonly int PocetTypuItemu = 6;

        


        public static int PocetPriser = 0;

        public int vychodX;
        public int vychodY;

        public static Random padnezbarelu = new Random(); 
        public static char[,] plan;
        
        public int sirka;
        public int vyska;
        public int ZbyvaCasu;
        public readonly int zacatekItemu = 10; //index, kde začínají itemy

        public Stav stav = Stav.nezacala;
        
        public static Bitmap[] ikonky;
        public static readonly int sx = 60; // velikost kosticky ikonek
        
        public Hrdina hrdina1;
        public Hrdina hrdina2;

        public static List<Prisera> Prisery;

        public static List<Bomba> Bomby;

        public static List<Item> Itemy;

        public static List<Blesk> Blesky;

        public static List<Barel> Barely;

        public static List<Hrdina> Hrdinove;


        public Mapa(string cestaMapa, string cestaIkonky)
        {
            NactiIkonky(cestaIkonky);
            NactiMapu(cestaMapa);
            stav = Stav.bezi;
        }

        public void Presun(int zX, int zY, int naX, int naY)
        {
            char c = plan[zX, zY];
            char d = plan[naX, naY];
            plan[zX, zY] = ' ';
            plan[naX, naY] = c;

            // podivat se, jestli tam nestal hrdina:
            if (c == 'K' || c == 'k')
            {
                hrdina1.x = naX;
                hrdina1.y = naY;

                switch(c)
                {
                    case 'k':
                        plan[zX, zY] = 'U';
                        break;

                    case 'K':
                        plan[zX, zY] = ' ';
                        break;
                }
                switch(d)
                {
                    case 'U':
                        plan[naX, naY] = 'k';
                        break;

                    case ' ':
                        plan[naX, naY] = 'K';
                        break;
                }

                return; // kdyz na [zY,zX] stoji hrdina, tak tam nic jineho nestoji
            }

            if (c == 'S' || c == 's')
            {
                hrdina2.x = naX;
                hrdina2.y = naY;

                switch (c)
                {
                    case 's':
                        plan[zX, zY] = 'Q';
                        break;

                    case 'S':
                        plan[zX, zY] = ' ';
                        break;
                }
                switch (d)
                {
                    case 'Q':
                        plan[naX, naY] = 's';
                        break;

                    case ' ':
                        plan[naX, naY] = 'S';
                        break;
                }

                return; // kdyz na [zY,zX] stoji hrdina, tak tam nic jineho nestoji
            }
            
            // najit pripadny pohyblivyPrvek a zmenit mu polohu :
            foreach (Prisera po in Prisery)
            {
                if ((po.x == zX) && (po.y == zY))
                {
                    po.x = naX;
                    po.y = naY;
                    break; // jakmile tam stoji jeden, tak uz tam nikdo jiny nestoji
                }
            }

            foreach (Bomba po in Bomby)
            {
                if ((po.x == zX) && (po.y == zY))
                {
                    po.x = naX;
                    po.y = naY;
                    break; // jakmile tam stoji jeden, tak uz tam nikdo jiny nestoji
                }
            }
        }


        public void VytvorAVykresliBlesk(Mapa mapa, int x, int y, Bomba autor, char typ)
        {
            foreach(Hrdina h in Hrdinove)
            {
                if ((h.x == x) && (h.y == y))
                {
                    h.zivotu--;
                    Sounds.soundPlayerBombDeath.Play();
                    if (h.zivotu == 0) { mapa.stav = Stav.prohra; plan[x, y] = typ; }
                    else {
                        Blesk blesk = new Blesk(mapa, x, y, autor, Form1.TimerBlesku, typ, h);
                        Blesky.Add(blesk);
                        mapa.ZrusBombu(x, y);
                        plan[x, y] = typ;
                    }
                }
                else
                {
                    Blesk blesk = new Blesk(mapa, x, y, autor, Form1.TimerBlesku, typ, null);
                    Blesky.Add(blesk);
                    mapa.ZrusBombu(x, y);
                    plan[x, y] = typ;
                }
            }
        }

        public void VytvorAVykresliBombu(Mapa mapa, int x, int y, Hrdina autor) //int odpocet pro různou délku odpočtů mezi hrdiny
        {
            Bomba bomba = new Bomba(mapa, x, y, autor, Form1.TimerBomb);
           
                if (autor == hrdina1) { plan[x, y] = 'k'; }
                if (autor == hrdina2) { plan[x, y] = 's'; }

            Bomby.Add(bomba);
            
            autor.bomb--;
        }

        public void VytvorAVykresliItem(Mapa mapa, int x, int y, int typ)
        {
            Item item = new Item(mapa, x, y, Form1.TimerItemu, typ);
            Itemy.Add(item);
            plan[x, y] = typ.ToString().First();
        }

        public void VytvorAVykresliBarel(Mapa mapa, int x, int y)
        {
            Barel barel = new Barel(mapa, x, y, padnezbarelu.Next(-PocetTypuItemu, PocetTypuItemu));
            Barely.Add(barel);
            plan[x, y] = '+';
        }



        public void ZrusItem(int zX, int zY)
        {
            for (int i = 0; i < Itemy.Count; i++)
            {
                if ((Itemy[i].x == zX) && (Itemy[i].y == zY))
                {
                    Itemy.RemoveAt(i); // 1. vyhodit ze seznamu pohyblivych prvku...
                    plan[zX, zY] = ' ';                    // 2. ...a z planu!
                    break;
                }
            }
        }

        public void ZrusPriseru(int zX, int zY)
        {
            // najit pohyblivyPrvek a vyhodit ho ze seznamu :
            for (int i = 0; i < Prisery.Count; i++)
            {
                if ((Prisery[i].x == zX) && (Prisery[i].y == zY))
                {
                    Sounds.soundMonsterDeath.Play();
                    Prisery.RemoveAt(i); // 1. vyhodit ze seznamu pohyblivych prvku...
                    PocetPriser--;
                    plan[zX, zY] = ' ';                    // 2. ...a z planu!
                    break;
                }
                
            }
        }

        public void ZrusBombu(int zX, int zY)
        {
            // najit pohyblivyPrvek a vyhodit ho ze seznamu :
            for (int i = 0; i < Bomby.Count; i++)
            {
                if ((Bomby[i].x == zX) && (Bomby[i].y == zY))
                {
                    Bomby.RemoveAt(i); // 1. vyhodit ze seznamu pohyblivych prvku...
                    plan[zX, zY] = ' ';                    // 2. ...a z planu!
                    break;
                }
            }
        }

        public void ZrusBlesk(int zX, int zY)
        {
            // najit pohyblivyPrvek a vyhodit ho ze seznamu :
            for (int i = 0; i < Blesky.Count; i++)
            {
                if ((Blesky[i].x == zX) && (Blesky[i].y == zY))
                {
                    Blesky.RemoveAt(i);
                    plan[zX, zY] = ' ';
                    plan[hrdina1.x, hrdina1.y] = 'K'; //vyřešilo problém s mizením hrdinů
                    if (Form1.pocetHracu == 2) { plan[hrdina2.x, hrdina2.y] = 'S'; }
                    break;
                }
            }
        }

        public void ZrusBarel(int zX, int zY)
        {
            for (int i = 0; i < Barely.Count; i++)
            {
                if ((Barely[i].x == zX) && (Barely[i].y == zY))
                {
                    Barely.RemoveAt(i); // 1. vyhodit ze seznamu pohyblivych prvku...
                    plan[zX, zY] = ' ';                    // 2. ...a z planu!
                    break;
                }
            }
        }


        // Zjisti, co kde je
        public bool JeZed(int x, int y)
        {
            return plan[x, y] == 'X';
        }

        public bool JeBomba(int x, int y)
        {
            return plan[x, y] == 'U' || plan[x, y] == 'Q';
        }

        public bool JeHrdina(int x, int y)
        {
            return (JeHrdinaSBombou(x, y) || JeHrdinaBezBomby(x, y));
        }

        public bool JeHrdinaSBombou(int x, int y)
        {
            return plan[x, y] == 'k' || plan[x, y] == 's';
        }

        public bool JeBarel(int x, int y)
        {
            return plan[x, y] == '+';
        }

        public bool JeHrdinaBezBomby(int x, int y)
        {
            return plan[x, y] == 'K' || plan[x, y] == 'S';
        }

        public bool JeVolno(int x, int y)
        {
            return (plan[x, y] == ' ');
        }

        public bool JeVolnoNeboBomba(int x, int y)
        {
            return (plan[x, y] == ' ') || (plan[x, y] == 'U' || plan[x, y] == 'Q');
        }

        public bool JePrisera(int x, int y)
        {
            return (plan[x, y] == '>') || (plan[x, y] == '<' || plan[x, y] == 'v' || plan[x, y] == '^');
        }

        public bool JeItem(int x, int y)
        {
            bool item = false;
            for (int i = 0; i < PocetTypuItemu; i++)
            {
               if(plan[x, y] == i.ToString().First()) { item = true; }
            }
            return item;
        }

        public bool JeOtevrenyVychod(int x, int y)
        {
            return plan[x, y] == 'e';
        }

        public void OtevriVychod()
        {
            plan[vychodX, vychodY] = 'e';
        }
        
        /*
        public bool JeVolnoZNa(int zX, int zY, int naX, int naY)
        {
            
                bool volno = true;
                if (zX == naX)
                {
                    for (int i = Math.Min(zX, naY); i <= Math.Max(zY, naY); i++)
                    {
                        if (JeVolnoNeboBomba(zX, i)) { volno = false; }
                    }
                }
                if (zY == naY)
                {
                    for (int i = Math.Min(zX, naX); i <= Math.Max(zX, naX); i++)
                    {
                        if (JeVolnoNeboBomba(i, zY)) { volno = false; }
                    }
                }
                return volno;
            
        }
        */

        public void NactiMapu(string cesta)
        {
            Prisery = new List<Prisera>();

            Bomby = new List<Bomba>();

            Itemy = new List<Item>();

            Blesky = new List<Blesk>();

            Barely = new List<Barel>();

            Hrdinove = new List<Hrdina>();

            System.IO.StreamReader sr = new System.IO.StreamReader(cesta);
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());
            ZbyvaCasu = int.Parse(sr.ReadLine());
            plan = new char[sirka, vyska];
            

            for (int y = 0; y < vyska; y++)
            {
                string radek = sr.ReadLine();
                for (int x = 0; x < sirka; x++)
                {
                    char znak = radek[x];
                    plan[x, y] = znak;
                    
                    switch (znak)
                    {
                        case 'K':
                            this.hrdina1 = new Hrdina(this, x, y, 'v', 1, 1, 1, 3, true, false, false, StisknutaSipka.zadna, StisknutaBomba.zadna);
                            Hrdinove.Add(hrdina1);
                            break;
                        
                        case 'S':
                            this.hrdina2 = new Hrdina(this, x, y, 'v', 1, 1, 1, 1, false, false, false, StisknutaSipka.zadna, StisknutaBomba.zadna);
                            Hrdinove.Add(hrdina2);
                            break;
                        
                        case '<':
                        case '^':
                        case '>':
                        case 'v':
                            Prisera prisera = new Prisera(this, x, y, znak);
                            Prisery.Add(prisera);
                            PocetPriser++;
                            break;

                        case '+':
                            VytvorAVykresliBarel(this, x, y);
                            break;
                        case 'E':
                            plan[x, y] = 'X';
                            vychodX = x;
                            vychodY = y;
                            break;

                        default:
                            break;

                    }
                }
            }
            sr.Close();
        }

        public void NactiIkonky(string cesta)
        {
            
            int pocet = Directory.GetFiles(cesta, "*", SearchOption.TopDirectoryOnly).Length; 
            ikonky = new Bitmap[pocet];
            for (int i = 0; i < pocet; i++)
            {
                string lokalniCesta = cesta + "\\" + i + ".png";
                ikonky[i] = new Bitmap(lokalniCesta);
            }
            // this.sx = ikonky[0].Height; //velikost čtverečku podle první
        }

        public void VykresliSe(Graphics g, int sirkaVyrezuPixely, int vyskaVyrezuPixely)
        {
            
            int sirkaVyrezu = sirkaVyrezuPixely / sx;
            int vyskaVyrezu = vyskaVyrezuPixely / sx;

            if (sirkaVyrezu > sirka)
                sirkaVyrezu = sirka;

            if (vyskaVyrezu > vyska)
                vyskaVyrezu = vyska;

            // urcit LHR vyrezu:
            
            int dx = hrdina1.x - sirkaVyrezu / 2;
            if (dx < 0)
                dx = 0;
            if (dx + sirkaVyrezu - 1 >= this.sirka)
                dx = this.sirka - sirkaVyrezu;

            int dy = hrdina1.y - vyskaVyrezu / 2;
            if (dy < 0)
                dy = 0;
            if (dy + vyskaVyrezu - 1 >= this.vyska)
                dy = this.vyska - vyskaVyrezu;
            
            
            for (int x = 0; x < sirkaVyrezu; x++)
            {
                for (int y = 0; y < vyskaVyrezu; y++)
                {
                    
                    int mx = dx + x; // index do mapy
                    int my = dy + y; // index do mapy

                    char c = plan[mx, my];
                    int indexObrazku = " +XUKkgznhftb012345Ee<^>vQSs".IndexOf(c); // 0.. // <^>vXDEe

                    g.DrawImage(ikonky[indexObrazku], x * sx, y * sx);
                    
                }
            }
        }

        public void UdelejSeVsemiPrvkyKromeHracuCoUmi()
        {

            if (PocetPriser == 0) { OtevriVychod(); }

            foreach (Prisera p in Prisery)
            {
                p.UdelejCoUmis();
            }

            for (int i = 0; i < Bomby.Count; i++)
            {
                Bomba p = Bomby.ElementAt(i);


                if (p.autor == hrdina1 && plan[p.x, p.y] != 'k') { plan[p.x, p.y] = 'U'; } //toto vyřešilo problém s nezobrazováním bomb po položení
                if (p.autor == hrdina2 && plan[p.x, p.y] != 's') { plan[p.x, p.y] = 'Q'; }

                p.odpocet = p.odpocet - Form1.RychlostHry;

                if (p.odpocet < Form1.RychlostHry) { p.UdelejCoUmis(); if (Bomby.Count == 0) { break; } }
            }

            for (int i = 0; i < Blesky.Count; i++)
            {
                Blesk p = Blesky.ElementAt(i);

                p.odpocet = p.odpocet - Form1.RychlostHry;

                if (p.odpocet < Form1.RychlostHry) { p.UdelejCoUmis(); if (Blesky.Count == 0) { break; } }
            }

            for (int i = 0; i < Itemy.Count; i++)
            {
                Item p = Itemy.ElementAt(i);

                p.odpocet = p.odpocet - Form1.RychlostHry;

                if (p.odpocet < Form1.RychlostHry) { p.UdelejCoUmis() ; if (Itemy.Count == 0) { break; } }
            }           
        }

        public void UdelejSModrymHrdinouCoUmi(StisknutaSipka stisknutaSipka, StisknutaBomba stisknutaBomba)
        {
            hrdina1.stisknutaSipka = stisknutaSipka;
            hrdina1.stisknutaBomba = stisknutaBomba;
            hrdina1.UdelejCoUmis();
        }

        public void UdelejSCervenymHrdinouCoUmi(StisknutaSipka stisknutaSipka, StisknutaBomba stisknutaBomba)
        {
            hrdina2.stisknutaSipka = stisknutaSipka;
            hrdina2.stisknutaBomba = stisknutaBomba;
            hrdina2.UdelejCoUmis();
        }
    }
}
