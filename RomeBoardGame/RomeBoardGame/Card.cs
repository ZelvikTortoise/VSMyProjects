using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RomeBoardGame
{
    internal abstract class Card
    {
        public byte Cost { get; private set; }
        public byte Defense { get; private set; }
        public bool Building { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Tooltip { get; private set; } = string.Empty;
        public byte Count { get; private set; }
        public virtual void ActivateAbility() { }     
        public Card(byte cost, byte defense, bool building, string name, string tooltip, byte count)
        {
            Cost = cost;
            Defense = defense;
            Building = building;
            Name = name;
            Tooltip = tooltip;
            Count = count;
            Rome.AddToDrawDeck(this);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(" (");
            sb.Append(Count);
            sb.Append("×)\nTyp: ");
            sb.Append(Building ? "budova" : "osoba");
            sb.Append("\nCena: ");
            sb.Append(Cost);
            sb.Append("\nObrana: ");
            sb.Append(Defense);
            sb.Append('\n');
            sb.Append(Tooltip);

            return sb.ToString();
        }
    }

    internal class Aesculapinum : Card
    {
        public Aesculapinum() : base(5, 2, true, "Chrám léčení", "Hráč si může vzít z odkládacího balíčku jednu libovolnou kartu osoby do ruky.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityAesculapinum();
        }
    }
    internal class Architectus : Card
    {
        public Architectus() : base(3, 4, false, "Stavitel", "Umožňuje bezplatné vynášení libovolného počtu karet budov. Je možné překrýt libovolný počet karet i stavitele samotného.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityArchitectus();
        }
    }
    internal class Basilica : Card
    {
        public Basilica() : base(6, 5, true, "Bazilika", "Pokud je aktivována budova Fórum (musí ležet hned vedle baziliky), dostane hráč 2 vítězné body navíc. Bazilika se sama zvlášť neaktivuje.", 2) { }
    }
    internal class Centurio : Card
    {
        public Centurio() : base(9, 5, false, "Centurion", "Útočí na kartu soupeře ležící na stejném slotu. K hodnotě bojové kostky je možné přičíst hodnotu ještě nepoužité akční kostky (ta se poté považuje za použitou). Hráč o tom rozhodne až po hodu bojovou kostkou.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityCenturio();
        }
    }
    internal class Consiliarius : Card
    {
        public Consiliarius() : base(4, 4, false, "Poradce", "Hráč vezme své karty osob a znovu je umístí k libovolným slotům. Je možné překrýt karty budov.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityConsiliarius();            
        }
    }
    internal class Consul : Card
    {
        public Consul() : base(3, 3, false, "Konzul", "Hráč může počet bodů na akční kostce, která ještě nebyla použita, zvýšit nebo snížit o jedna.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityConsul();
        }
    }
    internal class Essedum : Card
    {
        public Essedum() : base(6, 3, false, "Bojový vůz", "Obranná hodnota vyložených karet soupeře se pro tento tah sníží o 2. Tento efekt se sčítá při vícero použitích ve stejném kole.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityEssedum();
        }
    }
    internal class Forum : Card
    {
        public Forum() : base(5, 5, true, "Fórum", "Vyžaduje dvě akční kostky – jednu pro aktivaci, druhou hráč zvolí, kolik vítězných bodů dostanu ze zásob.", 6) { }
        public override void ActivateAbility()
        {
            Rome.AbilityForum();
        }
    }
    internal class Gladiator : Card
    {
        public Gladiator() : base(6, 5, false, "Gladiátor", "Zvol jednu soupeřovu vyloženou kartu osoby. Ten si ji musí vzít zpět do ruky.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityGladiator();
        }
    }
    internal class Haruspex : Card
    {
        public Haruspex() : base(4, 3, false, "Věštec", "Hráč si může s dobíracího  balíčku vybrat jakoukoliv jednu kartu a vzít si ji do ruky. Poté se dobírací balíček zamíchá.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityHaruspex();
        }
    }
    internal class Legat : Card
    {
        public Legat() : base(5, 2, false, "Legát", "Hráč obdrží za každý soupeřův prázdný slot jeden vítězný bod ze zásob.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityLegat();
        }
    }
    internal class Legionarius : Card
    {
        public Legionarius() : base(4, 5, false, "Legionář", "Útočí na kartu soupeře ležící na stejném slotu.", 3) { }
        public override void ActivateAbility()
        {
            Rome.AbilityLegionarius();
        }
    }
    internal class Machina : Card
    {
        public Machina() : base(4, 4, true, "Jeřáb", "Hráč vezme své karty budov a znovu je umístí k libovolným slotům. Je možné překrýt karty osob.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityMachina();
        }
    }
    internal class Mercator : Card
    {
        public Mercator() : base(7, 2, false, "Obchodník", "Hráč si může od soupeře kupovat vítězné body (1 bod za 2 sestercie), a to tak dlouho, dokud má peníze. Zaplacené peníze dostane soupeř.", 1) { }
        public override void ActivateAbility()
        {
            Rome.AbilityMercator();
        }
    }
    internal class Mercatus : Card
    {
        public Mercatus() : base(6, 3, true, "Tržnice", "Za každé fórum, které má soupeř aktuálně vyložené, od něho dostaneš 1 vítězný bod.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityMercatus();
        }
    }
    internal class Nero : Card
    {
        public Nero() : base(8, 9, false, "Nero", "Zničí jakoukoliv jednu vyloženou kartu stavby soupeře (dle volby hráče na tahu). Odhodí se jak karta se zvolenou stavbou, tak karta Nera.", 1) { }
        public override void ActivateAbility()
        {
            Rome.AbilityNero();
        }
    }
    internal class Onager : Card
    {
        public Onager() : base(5, 4, true, "Katapult", "Útočí na libovolnou soupeřovu vyloženou budovu (dle volby hráče na tahu).", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityOnager();
        }
    }
    internal class Praetorianus : Card
    {
        public Praetorianus() : base(4, 4, false, "Pretorián", "Zablokuje soupeři libovolný slot po dobu jeho příštího tahu (dle volby hráče na tahu).", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityPraetorianus();
        }
    }
    internal class Scaenicus : Card
    {
        public Scaenicus() : base(8, 3, false, "Herec", "Sám nepřináší žádnou akci, ale může zkopírovat akci jakékoliv vlastní vyložené karty osoby. Při každé aktivaci může kopírovat jinou kartu.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityScaenicus();
        }
    }
    internal class Senator : Card
    {
        public Senator() : base(3, 3, false, "Senátor", "Umožňuje bezplatné vynášení libovolného počtu karet osob. Je možné překrýt libovolný počet karet i senátora samotného.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilitySenator();
        }
    }
    internal class Sicarius : Card
    {
        public Sicarius() : base(9, 2, false, "Nájemný vrah", "Odstraní jakoukoliv jednu soupeřovu vyloženou kartu osoby (dle volby hráče na tahu). Odhodí se jak karta se zvolenou osobou, tak karta nájemného vraha.", 1) { }
        public override void ActivateAbility()
        {
            Rome.AbilitySicarius();
        }
    }
    internal class Templum : Card
    {
        public Templum() : base(2, 2, true, "Chrám", "Pokud je aktivována budova Fórum (musí ležet hned vedle chrámu), může hráč použít třetí akční kostku (nesmí ještě být použita) pro zisk doplňujících vítězných bodů k bodům získaných fórem (vše ze zásob). Chrám se sám zvlášť neaktivuje.", 2) { }        
    }
    internal class TribunusPlebis : Card
    {
        public TribunusPlebis() : base(5, 5, false, "Tribun lidu", "Hráč dostane od soupeře 1 vítězný bod.", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityTribunusPlebis();
        }
    }
    internal class Turris : Card
    {
        public Turris() : base(6, 6, true, "Věž", "Po celou dobu, kdy je tato karta vyložena (nemusí být aktivována), je obranná hodnota všech ostatních vyložených karet daného hráče o 1 vyšší.", 2) { }
    }
    internal class Velites : Card
    {
        public Velites() : base(5, 3, false, "Oštěpař", "Útočí na libovolnou soupeřovu vyloženou kartu osoby (dle volby hráče na tahu).", 2) { }
        public override void ActivateAbility()
        {
            Rome.AbilityVelites();
        }
    }
}
