using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RomeBoardGame
{
    internal class Rome
    {
        // Methods for initialization:
        static void InitializeHandsAndSlots()
        {
            hands[0] = new List<Card>();
            hands[1] = new List<Card>();
            slots[0] = new Card[6];
            slots[1] = new Card[6];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    slots[i][j] = emptySlotValue;
                }
            }
        }
        static void InitializeCards()
        {
            drawDeck = new List<Card>();    // Emptying draw deck.
            discardPile = new List<Card>(); // Emptying discard pile.
            var typeOfCard = typeof(Card);
            var cardTypes = typeOfCard.Assembly.GetTypes().Where(t => t.IsSubclassOf(typeOfCard));
            cardTypes = cardTypes.OrderBy(type => type.Name);
            foreach (var cardType in cardTypes)
            {
                var count = cardType.GetField("Count", BindingFlags.Public | BindingFlags.Static)!.GetValue(null) ?? 0;
                for (byte i = 1; i <= (byte)count; i++)
                {
                    AddToDrawDeck((Card)Activator.CreateInstance(cardType)!);   // Filling draw deck.
                }
            }
            listOfAllCardTypes = cardTypes.Select(t => (Card)Activator.CreateInstance(t)!).ToList();    // Creating list of all types.
            listOfAllCardTypes.OrderBy(t => t.Name);
        }
        static void InitializePlayerStats()
        {
            playerStats[0] = new int[2];
            playerStats[0][0] = 0;
            playerStats[0][1] = 10;
            playerStats[1] = new int[2];
            playerStats[1][0] = 0;
            playerStats[1][1] = 10;
        }
        // Variables and constants:
        private static Random random = new Random();
        public static byte ActivePlayer;   // 0 or 1
        public static string? namePlayer1, namePlayer2;
        static List<Card> drawDeck = new List<Card>();
        static List<Card> discardPile = new List<Card>();
        static List<Card> listOfAllCardTypes = new List<Card>();
        static List<Card>[] hands = new List<Card>[2];
        static Card?[][] slots = new Card[2][];
        static List<byte> blockedSlotsCurrent = new List<byte>();
        static List<byte> blockedSlotsOfOpponentNextTurn = new List<byte>();
        static int[][] playerStats = new int[2][];    // for each player: [money, victory points - can be negative at the end of the game]
        const byte totalVictoryPoints = 36;
        const Card? emptySlotValue = null;  // do not change (because of ?? operators)
        const byte longestCardName = 12;
        const byte numberOfInitialCards = 4;
        const string hiddenCardName = "[Skryto]";
        static Type notCardType = typeof(string);  // anything different from Card or its subclasses
        static List<byte> actionDice = new List<byte>();
        public static byte lastActivatedSlot = 6; // 0, 1, 2, 3, 4, 5
        private static bool freeBuldings = false;
        private static bool freeCharacters = false;
        private static bool hiddenCardsInSlots = false;
        private static byte actionNumber = 0;
        private static byte opponentsDefenseLoweredBy = 0;
        private enum GameStates { GameStarting, Preparation, PayingVictoryPoints, RollingActionDice, TakingActions, GameEnded };
        private static GameStates gameState;

        // Constructors:
        static Rome()
        {
            InitializeCards();
            InitializeHandsAndSlots();
            InitializePlayerStats();            
        }
        public Rome()
        {
            namePlayer1 = "hráč 1";
            namePlayer2 = "hráč 2";
        }
        public Rome(string name1, string name2)
        {
            namePlayer1 = name1;
            namePlayer2 = name2;
        }

        // Game cycle:
        public static void GameStart()
        {
            gameState = GameStates.GameStarting;
            while (gameState != GameStates.GameEnded)
            {
                switch (gameState)
                {
                    case GameStates.GameStarting:
                        PrintGameStart();
                        SetPlayerNamesAndCreateGame();
                        ActivePlayer = 0;
                        ResetDecks();
                        Console.WriteLine("Hra je připravena. Hodně štěstí a příjemnou zábavu.");
                        gameState = GameStates.Preparation;
                        break;
                    case GameStates.Preparation:
                        TogglePreparationPhase(true);
                        List<Card>[] cardsToGive = new List<Card>[2];
                        for (byte k = 0; k < 2; k++)
                        {
                            ExchangeHiddenCards(cardsToGive);
                        }
                        for (byte k = 0; k < 2; k++)
                        {
                            RecieveCardsAndPlaceFirstCards(cardsToGive);
                        }
                        TogglePreparationPhase(false);
                        gameState = GameStates.PayingVictoryPoints;
                        break;
                    case GameStates.PayingVictoryPoints:
                        LoseVictoryPointsToSupply(GetNumberOfEmptySlots(true));
                        if (gameState != GameStates.GameEnded)
                            gameState = GameStates.RollingActionDice;
                        break;
                    case GameStates.RollingActionDice:
                        StartTurn();
                        gameState = GameStates.TakingActions;
                        break;
                    case GameStates.TakingActions:
                        Console.WriteLine();
                        PrintPossibleActions();
                        TakeAction(PromptForNumber(1, actionNumber));
                        if (gameState != GameStates.GameEnded)
                            gameState = GameStates.PayingVictoryPoints;
                        break;
                    case GameStates.GameEnded:
                        Console.Write("Zmáčknutím libovolné klávesy ukončíte aplikaci... ");
                        Console.ReadKey();
                        break;
                    default:
                        throw new NotImplementedException("V herním cyklu byl dosažen neimplementovaný stav. Kontaktujte programátora.");
                }
            }
        }

        // Game running methods:
        public static void StartTurn()
        {
            Console.WriteLine("Na tahu je {0}.", ActivePlayer == 0 ? namePlayer1 : namePlayer2);
            Console.Write("Zmáčkni libovolnou klávesu pro hod akčními kostkami... ");
            Console.ReadKey();
            RollActionDice();
            PrintPlayerStats(); // Prints the amount of money and victory points of both players, then prints the state of the slots on both sides.
        }
        public static void EndTurn()
        {
            freeBuldings = false;
            freeCharacters = false;
            opponentsDefenseLoweredBy = 0;
            blockedSlotsCurrent = blockedSlotsOfOpponentNextTurn;
            blockedSlotsOfOpponentNextTurn = new List<byte>();
            ToggleActivePlayer();
            StartTurn();
        }

        private static void PrintGameStart()
        {
            Console.WriteLine("ŘÍM");
            Console.WriteLine("Vítejte ve hře Řím.");
            Console.WriteLine("Jedná se o hru pro 2 hráče.");
        }

        private static void SetPlayerNamesAndCreateGame()
        {
            Rome game;
            string player1Name, player2Name;
            Console.Write("Chcete si nastavit jména pro každého z hráčů? (a = ano, n = ne)");
            if ((Console.ReadLine() ?? string.Empty).ToLower() == "a")
            {
                Console.Write("Zadejte jméno hráče 1: ");
                player1Name = Console.ReadLine()!;
                Console.Write("Zadejte jméno hráče 2: ");
                player2Name = Console.ReadLine()!;
                game = new Rome(player1Name, player2Name);
            }
            else
            {
                Console.WriteLine("Hra použije výchozí jména pro hráče.");
                game = new Rome();
            }
        }

        private static void ExchangeHiddenCards(List<Card>[] cardsToGive)
        {
            Console.Write("Po zmáčknutí libovolné klávesy si vezmeš do ruky čtyři karty z balíčku... ");
            Console.ReadKey();
            DrawCards(numberOfInitialCards);
            cardsToGive[ActivePlayer] = new List<Card>();
            Console.WriteLine("Zvol si dvě karty, které dáš soupeřovi.");
            for (int i = 1; i <= 2; i++)
            {
                PickOneFromPile(hands[ActivePlayer], 1);    // Adds card to the end of the hand.
                cardsToGive[ActivePlayer].Add(hands[ActivePlayer][hands[ActivePlayer].Count - 1]);  // Marks the card as a card to give away.
                hands[ActivePlayer].RemoveAt(hands[ActivePlayer].Count - 1);    // Removes the card from hand.
            }
            Console.WriteLine("Karty k výměně byly úspěšně vybrány.");
            ToggleActivePlayer();
        }

        private static void RecieveCardsAndPlaceFirstCards(List<Card>[] cardsToGive)
        {
            hands[ActivePlayer].Add(cardsToGive[ActivePlayer ^ 1][0]);
            hands[ActivePlayer].Add(cardsToGive[ActivePlayer ^ 1][1]);
            Console.WriteLine("Nyní po jedné umísti všechny své karty na své sloty.");
            for (byte i = 1; i <= numberOfInitialCards; i++)
            {
                PlayCardFromHand();
            }
            Console.WriteLine("Všechny karty byly úspěšně umístěny.");
            ToggleActivePlayer();
        }

        private static void ReadCardTooltip(Card card)
        {
            if (card == null)
                throw new ArgumentException("V nápovědě byla zvolena neznámá karta. Kontaktujte programátora.", "Nápověda: neexistující karta");
            Console.WriteLine(card!.ToString());
            Console.Write("K pokračování zmáčkni libovolnou klávesu... ");
            Console.ReadKey();
        }

        // Not used.
        private static void ReadCardTypeTooltip(Type? cardType)
        {
            if (cardType == null)
                throw new ArgumentException("V nápovědě byl zvolen neznámý typ karty. Kontaktujte programátora.");
            if (!typeof(Card).IsAssignableFrom(cardType))
                throw new ArgumentException(String.Format("V nápovědě byl zvolen typ {0}, který však není kartou typu {1}. Kontaktujte programátora.", nameof(cardType), nameof(Card)), "Nápověda: zadán typ, který není kartou");
            ReadCardTooltip((Card)Activator.CreateInstance(cardType!)!);
        }

        public static void LookUpCardTooltip()
        {
            Console.WriteLine();
            Console.WriteLine("NÁPOVĚDA – zvol si kartu, která má být vysvětlena:");
            PrintPile(listOfAllCardTypes);
            ReadCardTooltip(listOfAllCardTypes[PromptForNumber(1, (byte)listOfAllCardTypes.Count) - 1]);
        }

        // Other actions (info, etc.):
        public static void PrintSlots()
        {
            StringBuilder sb = new StringBuilder();
            Card?[] activePlayerSlots = slots[ActivePlayer];
            Card?[] opponentPlayerSlots = slots[ActivePlayer ^ 1];
            int charactersLeft;
            bool activePlayerCardInSlot;

            if (hiddenCardsInSlots)
            {
                for (int i = 0; i < 6; i++)
                {
                    // Appending left cardName aligned to right filled with spaces from left.
                    charactersLeft = longestCardName;
                    activePlayerCardInSlot = false;
                    if (activePlayerSlots[i] != emptySlotValue)
                    {
                        charactersLeft -= hiddenCardName.Length;
                        activePlayerCardInSlot = true;
                    }
                    for (int j = charactersLeft; j > 0; j--)
                        sb.Append(' ');
                    if (activePlayerCardInSlot)
                        sb.Append(hiddenCardName);

                    sb.Append($" #{i + 1}# "); // space #slotNumber# space

                    // Appending right cardName aligned to left filled with spaces from right.
                    charactersLeft = longestCardName;
                    if (opponentPlayerSlots[i] != emptySlotValue)
                    {
                        charactersLeft -= hiddenCardName.Length;
                        sb.Append(hiddenCardName);
                    }
                    for (int j = charactersLeft; j > 0; j--)
                        sb.Append(' ');
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    // Appending left cardName aligned to right filled with spaces from left.
                    charactersLeft = longestCardName;
                    activePlayerCardInSlot = false;
                    if (activePlayerSlots[i] != emptySlotValue)
                    {
                        charactersLeft -= activePlayerSlots[i]!.Name.Length;
                        activePlayerCardInSlot = true;
                    }
                    for (int j = charactersLeft; j > 0; j--)
                        sb.Append(' ');
                    if (activePlayerCardInSlot)
                        sb.Append(activePlayerSlots[i]!.Name);

                    sb.Append($" #{i + 1}# "); // space #slotNumber# space

                    // Appending right cardName aligned to left filled with spaces from right.
                    charactersLeft = longestCardName;
                    if (opponentPlayerSlots[i] != emptySlotValue)
                    {
                        charactersLeft -= opponentPlayerSlots[i]!.Name.Length;
                        sb.Append(opponentPlayerSlots[i]!.Name);
                    }
                    for (int j = charactersLeft; j > 0; j--)
                        sb.Append(' ');
                }

                // Outputting the slot stats:
                Console.WriteLine(sb.ToString());
                sb.Clear();
            }
        }
        public static void PrintOverviewStats()
        {
            PrintStatsVictoryMoney();
            Console.WriteLine();
            PrintStatsVictoryPoints();
        }
        public static void PrintPlayerStats()
        {
            PrintStatsPlayer1();
            Console.WriteLine();
            PrintStatsPlayer2();
            Console.WriteLine();
            PrintSlots();
        }
        public static void PrintStatsPlayer1()
        {
            Console.WriteLine(namePlayer1);
            Console.WriteLine("      Peníze: {0}", playerStats[0][0]);
            Console.WriteLine("Vítězné body: {0}", playerStats[0][1]);
        }
        public static void PrintStatsPlayer2()
        {
            Console.WriteLine(namePlayer2);
            Console.WriteLine("      Peníze: {0}", playerStats[1][0]);
            Console.WriteLine("Vítězné body: {0}", playerStats[1][1]);
        }
        public static void PrintStatsVictoryMoney()
        {
            Console.WriteLine("PENÍZE");
            Console.WriteLine("{0}: {1}", namePlayer1, playerStats[0][0]);
            Console.WriteLine("{0}: {1}", namePlayer2, playerStats[1][0]);
        }
        public static void PrintStatsVictoryPoints()
        {
            Console.WriteLine("VÍTĚZNÉ BODY");
            Console.WriteLine("{0}: {1}", namePlayer1, playerStats[0][1]);
            Console.WriteLine("{0}: {1}", namePlayer2, playerStats[1][1]);
        }
        public static void PrintDiscardPile()
        {
            PrintPile(discardPile, 1);
        }
        public static void AddToDiscardPile(Card card)
        {
            AddToPile(card, discardPile);
        }
        public static void ShuffleDrawDeck()
        {
            int i, j;
            Card temp;
            for (i = drawDeck.Count - 1; i >= 0; i--)
            {
                j = random.Next(i + 1);
                temp = drawDeck[i];
                drawDeck[i] = drawDeck[j];
                drawDeck[j] = temp;
            }
        }
        public static byte GetNumberOfEmptySlots(bool activePlayer)
        {
            byte emptySlots = 0;
            for (byte i = 0; i < 6; i++)
            {
                if (slots[activePlayer ? ActivePlayer : ActivePlayer ^ 1][i] == emptySlotValue)
                    emptySlots++;
            }

            return emptySlots;
        }



        // Game actions:
        private static void PrintPossibleActions()
        {
            actionNumber = 0;
            Console.WriteLine("VOLBA AKCE");
            Console.WriteLine("{0} = aktivace karty ve slotu za akční kostku", ++actionNumber);
            Console.WriteLine("{0} = výběr peněz za akční kostku", ++actionNumber);
            Console.WriteLine("{0} = dobrání karty za akční kostku", ++actionNumber);
            Console.WriteLine("{0} = vyložení karty do slotů za peníze", ++actionNumber);
            Console.WriteLine("{0} = nápověda: co dělá daná karta", ++actionNumber);
            if (actionDice.Count == 0)
                Console.WriteLine("{0} = konec kola", ++actionNumber);
        }
        private static void TakeAction(byte actionIndex)
        {
            switch (actionIndex)
            {
                case 1:
                    ActivateAbilityWithActionDie();
                    break;
                case 2:
                    TakeMoneyWithActionDie();
                    break;
                case 3:
                    DrawCardsWithActionDie();
                    break;
                case 4:
                    PlayCardFromHand();
                    break;
                case 5:
                    LookUpCardTooltip();
                    break;
                case 6:
                    EndTurn();
                    break;
                default:
                    throw new ArgumentException("V tahu hráče byla zvolena neznámá akce. Kontaktujte programátora.", "Zvolena neexistující akce");
            }
        }
        private static void ActivateAbilityWithActionDie()
        {
            byte temp = lastActivatedSlot;
            lastActivatedSlot = (byte)(ChooseActionDie() - 1);  // lastActivatedSlot is 0–5, ChooseActionDie() removes the die from play
            if (slots[ActivePlayer][lastActivatedSlot] != emptySlotValue && !blockedSlotsCurrent.Contains(lastActivatedSlot))
                slots[ActivePlayer][lastActivatedSlot]!.ActivateAbility();
            else
            {
                Console.WriteLine("Na vybraném slotu není žádná tvoje karta nebo je vybraný slot v tomto kole zablokován.");
                RefundActionDie();
                lastActivatedSlot = temp;   // Change back only after refunding the die, not before!
            }
        }
        private static void TakeMoneyWithActionDie()
        {
            byte addedMoney = ChooseActionDie();
            playerStats[ActivePlayer][0] += addedMoney;
            Console.WriteLine("Tvé zásoby sestercií se zvýšily o {0}", addedMoney);
        }
        private static void DrawCards(byte number)
        {
            byte numberOfCards = number;
            List<Card> drawnCards = new List<Card>();
            Console.WriteLine();
            for (int i = 1; i <= numberOfCards; i++)
            {
                if (drawDeck.Count == 0)
                {
                    MakeNewDrawDeck();
                    Console.WriteLine("Karty v dobíracím balíčku byly vyčerpány. Odkládací balíček byl zamíchán a tvoří nyní nový dobírací balíček.");
                }
                drawnCards.Add(drawDeck[0]);
                drawDeck.RemoveAt(0);
            }
            drawnCards.Sort();
            PickOneFromPile(drawnCards, 1);     // Removes the picked card from the pile.
            foreach (Card card in drawnCards)
            {
                discardPile.Add(card);
            }
            Console.WriteLine("Karta byla úspěšně vybrána. Počet karet odhozených na odkládací balíček: {0}", drawnCards.Count);
            drawnCards.Clear(); // probably unnecessary (declaration space is ending)
        }
        private static void DrawCardsWithActionDie()
        {
            byte numberOfCards = ChooseActionDie();
            DrawCards(numberOfCards);
        }
        private static bool CanBePaidFor(Card card)
        {
            return (card.Building && freeBuldings) || (!card.Building && freeCharacters) || (card.Cost <= playerStats[ActivePlayer][0]);
        }
        private static void PlayCardFromHand()
        {
            List<Card> cardsPossibleToPlay = new List<Card>();
            for (byte i = 0; i < hands[ActivePlayer].Count; i++)
            {
                if (CanBePaidFor(hands[ActivePlayer][i]))
                    cardsPossibleToPlay.Add(hands[ActivePlayer][i]);
            }
            if (cardsPossibleToPlay.Count <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Nemáš peníze na vyložení žádné karty ze své ruky.");
            }
            else
            {
                Console.WriteLine();
                cardsPossibleToPlay.OrderBy(card => card.Name);
                PickOneFromPile(cardsPossibleToPlay, 1);    // Adds the picked card to hand (as the last card).
                byte chosenSlot = ChooseSlot();
                if (slots[ActivePlayer][chosenSlot] != emptySlotValue)
                {
                    DiscardCardInPlay(true, chosenSlot);
                }
                slots[ActivePlayer][chosenSlot] = hands[ActivePlayer][hands[ActivePlayer].Count - 1]; // Placing the picked card in its chosen slot. 
                hands[ActivePlayer].RemoveAt(hands[ActivePlayer].Count - 1);    // Removing the last card in hand (it never should have been there).
                hands[ActivePlayer].Remove(slots[ActivePlayer][chosenSlot]!);   // Removing the picked card from actual hand.
                PayForCard(slots[ActivePlayer][chosenSlot]!);  // Paying the cost of the card.
            }
        }

        // Private or internal methods:
        private static byte PromptForNumber(byte min, byte max)
        {
            string? answer;
            byte number;
            bool first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zkus to znovu.");
                    Console.WriteLine();
                }
                else
                    first = false;
                Console.Write($"Zvol celé číslo od {min} do {max}: ");
                answer = Console.ReadLine();
            }
            while (!byte.TryParse(answer, out number));

            return number;
        }
        private static void TogglePreparationPhase(bool starting)
        {
            hiddenCardsInSlots = starting;
            freeBuldings = starting;
            freeCharacters = starting;
        }
        private static void ActionFinished()
        {
            Console.WriteLine("Využití schopnosti dokončeno.");
        }
        private static void PayForCard(Card card)
        {
            if ((card.Building && !freeBuldings) || (!card.Building && !freeCharacters))
                playerStats[ActivePlayer][0] -= card.Cost;
            // else: card is free thanks to Architectus or Senator            
        }
        private static void AddToPile(Card card, List<Card> pile)
        {
            pile.Add(card);
        }
        internal static void AddToDrawDeck(Card card)
        {
            AddToPile(card, drawDeck);
        }
        private static void PrintPile(List<Card> pile)
        {
            PrintPile(pile, 1);
        }
        /// <summary>
        /// Zobrazí v konzoli karty v balíčku. Parametr option rozhodne, zda všechny: 1, pouze osoby: 2, nebo pouze budovy :3.
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="option">1 ... all, 2 ... only characters, 3 ... only buildings</param>
        private static void PrintPile(List<Card> pile, byte option)
        {
            Console.WriteLine("Karty::");
            StringBuilder sb = new StringBuilder();
            byte id = 1;
            bool doubleDigits = pile.Count >= 10;
            List<Card> tempPile;
            if (option == 1)
            {
                tempPile = pile;
            }
            else
            {
                tempPile = new List<Card>();
                if (option == 2)
                {
                    foreach (Card card in pile)
                    {
                        if (!card.Building) tempPile.Add(card);
                    }
                }
                else if (option == 3)
                {
                    foreach (Card card in pile)
                    {
                        if (card.Building) tempPile.Add(card);
                    }
                }
            }
            foreach (Card card in tempPile)
            {
                sb.Clear();
                sb.Append('[');
                if (doubleDigits && id < 10) sb.Append(' ');
                sb.Append(id++);
                sb.Append("] ");
                sb.Append(card.Name);
                Console.WriteLine(sb.ToString());
            }
        }
        private static void ToggleActivePlayer()
        {
            Console.Write("Zmáčknutím libovolné klávesy ukončíš svůj tah... ");
            Console.ReadKey();
            Console.Clear();
            ActivePlayer ^= 1;  // Flipping the last bit: 0 -> 1, 1 -> 0.
        }
        private static void ResetDecks()
        {
            for (int i = discardPile.Count; i > 0; i--)
            {
                AddToDrawDeck(discardPile[i - 1]);
                discardPile.RemoveAt(i - 1);
            }
            for (int k = 0; k < 2; k++)
            {
                for (int i = hands[k].Count; i > 0; i--)
                {
                    AddToDrawDeck(hands[k][i - 1]);
                    hands[k].RemoveAt(i - 1);
                }
            }
            ShuffleDrawDeck();
        }
        private static void PrintDrawDeck()
        {
            PrintPile(drawDeck, 1);
        }
        private static void SortDrawDeck()
        {
            drawDeck.OrderBy(card => card.Name);
        }
        private static void SortDiscardPileCharacterFirst()
        {
            discardPile.OrderBy(card => card.Building).ThenBy(card => card.Name);
        }

        private static void PickOneFromPile(List<Card> pile)
        {
            PickOneFromPile(pile, 1);
        }
        private static void PickOneFromPile(List<Card> pile, byte option)
        {
            Console.WriteLine();
            PrintPile(pile, option);
            Console.WriteLine();
            byte max = 0;
            switch (option)
            {
                case 1: // all cards
                    max = (byte)pile.Count;
                    break;
                case 2: // only characters
                    foreach (Card card in pile)
                    {
                        if (!card.Building)
                        {
                            max++;
                        }
                    }
                    break;
                case 3: // only buildings
                    foreach (Card card in pile)
                    {
                        if (card.Building)
                        {
                            max++;
                        }
                    }
                    break;
            }
            byte picked = PromptForNumber(1, max);
            hands[ActivePlayer].Add(pile[picked]);
            pile.RemoveAt(picked);
        }
        private static void MakeNewDrawDeck()
        {
            drawDeck = discardPile;
            discardPile = new List<Card>();
            ShuffleDrawDeck();
        }
        private static void PickOneFromDrawDeck()
        {
            SortDrawDeck();
            PickOneFromPile(drawDeck, 1);
            ShuffleDrawDeck();
            ActionFinished();
        }
        private static bool AreNoCharactersInDiscardPile()
        {
            foreach (Card card in discardPile)
            {
                if (!card.Building)
                    return false;
            }
            return true;
        }
        private static void PrintActionUnavailable(string reason)
        {
            Console.WriteLine("Tuto schopnost není aktuálně možné použít, protože {0}.", reason);
        }
        private static void PickOneCharacterFromDiscardPile()
        {
            if (!AreNoCharactersInDiscardPile())
            {
                Console.WriteLine();
                PrintActionUnavailable("odkládací balíček neobsahuje žádné karty osob");
                RefundActionDie();

            }
            else
            {
                SortDiscardPileCharacterFirst();
                PickOneFromPile(discardPile, 2);
                ActionFinished();
            }
        }
        private static void MakeBuildingsFreeThisTurn()
        {
            freeBuldings = true;
        }
        private static void MakeCharactersFreeThisTurn()
        {
            freeCharacters = true;
        }
        private static void RollActionDice()
        {
            byte newDieRoll;
            for (int i = 1; i <= 3; i++)
            {
                newDieRoll = (byte)random.Next(1, 7);
                actionDice.Add(newDieRoll);
            }
            actionDice.Sort();
        }
        /// <summary>
        /// Left names are aligned to right, right names aligned to left.
        /// 12 Name1 + space + #1# + space + 12 Name2
        /// 12 Name1 + space + #2# + space + 12 Name2
        /// 12 Name1 + space + #3# + space + 12 Name2
        /// 12 Name1 + space + #4# + space + 12 Name2
        /// 12 Name1 + space + #5# + space + 12 Name2
        /// 12 Name1 + space + #6# + space + 12 Name2
        /// </summary>
        private static byte ChooseActionDie()
        {
            Console.WriteLine("Zbývající hodnoty akčních kostek:");
            foreach (byte die in actionDice)
            {
                Console.WriteLine("hodnota {0}", die);
            }
            Console.WriteLine();
            string answer;
            byte chosenDieValue;
            bool first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zkus to znovu.");
                    Console.WriteLine();
                }
                else
                    first = false;
                Console.Write("Zadej hodnotu akční kostky, která se má použít: ");
                answer = Console.ReadLine() ?? string.Empty;
            }
            while (!byte.TryParse(answer, out chosenDieValue) || !actionDice.Contains(chosenDieValue));

            actionDice.Remove(chosenDieValue);

            return chosenDieValue;
        }
        /// <summary>
        /// For player, the slot numbers are 1, 2, 3, 4, 5, 6. In code, they are 0–5 => conversion must take place.
        /// </summary>
        /// <returns>Slot number 0–5.</returns>
        private static byte ChooseSlot()
        {
            Console.WriteLine();
            Console.WriteLine("Aktuální stav slotů:");
            PrintSlots();
            Console.WriteLine();
            string answer;
            byte chosenSlot;
            bool first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zkus to znovu.");
                    Console.WriteLine();
                }
                else
                    first = false;
                Console.WriteLine("Zvol číslo slotu (1, 2, 3, 4, 5, 6): ");
                answer = Console.ReadLine() ?? string.Empty;
            }
            while (!byte.TryParse(answer, out chosenSlot) || chosenSlot < 1 || chosenSlot > 6);

            return (byte)(chosenSlot - 1);
        }
        private static byte ChooseNonBlockedSlot()
        {
            Console.WriteLine();
            Console.WriteLine("Aktuální stav slotů:");
            PrintSlots();
            Console.WriteLine();
            StringBuilder sb = new StringBuilder();
            string alreadyBlockedString;
            if (blockedSlotsOfOpponentNextTurn.Count <= 0)
                alreadyBlockedString = "žádný";
            else
            {
                blockedSlotsOfOpponentNextTurn.Sort();
                sb.Append(blockedSlotsOfOpponentNextTurn[0] + 1);
                for (int i = 1; i < blockedSlotsOfOpponentNextTurn.Count; i++)
                {
                    sb.Append(", ");
                    sb.Append(blockedSlotsOfOpponentNextTurn[i] + 1);
                }
                alreadyBlockedString = sb.ToString();
            }
            Console.WriteLine("Z toho už jsou pro soupeře na příští kolo zablokované sloty: {0}", alreadyBlockedString);
            Console.WriteLine();
            string answer;
            byte chosenSlot;
            bool first = true;
            sb.Clear();
            for (byte i = 0; i < 6; i++)
            {
                if (!blockedSlotsOfOpponentNextTurn.Contains(i))
                {
                    sb.Append(i + 1);
                    sb.Append(", ");
                }
            }
            sb.Remove(sb.Length - 2, 2);    // Always has at least 4 slots. Removing the last ", " string.
            do
            {
                if (!first)
                {                    
                    Console.WriteLine("Neplatná hodnota. Zkus to znovu.");
                    Console.WriteLine();
                }
                else
                    first = false;
                Console.WriteLine("Zvol číslo zatím nezablokovaného soupeřova slotu {0}: ", sb.ToString());
                answer = Console.ReadLine() ?? string.Empty;
            }
            while (!byte.TryParse(answer, out chosenSlot) || chosenSlot < 1 || chosenSlot > 6 || blockedSlotsOfOpponentNextTurn.Contains((byte)(chosenSlot - 1)));

            return (byte)(chosenSlot - 1);
        }
        private static byte ChooseSlotOfCertainType(bool ofActivePlayer, bool buildings, Type cardTypeToDodge)
        {
            Console.WriteLine();
            Console.WriteLine("Aktuální stav slotů:");
            PrintSlots();
            Console.WriteLine();
            string answer;
            byte chosenSlot;
            bool first = true;
            List<byte> validSlotNumbers = new List<byte>();
            byte player = (byte)(ofActivePlayer ? ActivePlayer : ActivePlayer ^ 1);
            for (byte i = 0; i < 6; i++)
            {
                if (slots[player][i] != emptySlotValue && slots[player][i]!.Building == buildings && slots[player][i]!.GetType() != cardTypeToDodge)
                    validSlotNumbers.Add((byte)(i + 1));    // This will for sure happen at least once, otherwise this method wouldn't be called.
            }
            if (validSlotNumbers.Count == 1)
            {
                chosenSlot = validSlotNumbers[0];
                Console.WriteLine("Slot byl zvolen automaticky, jelikož nebylo na výběr.");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(validSlotNumbers[0]);
                for (byte j = 1; j < validSlotNumbers.Count; j++)
                {
                    sb.Append(", ");
                    sb.Append(validSlotNumbers[j]);
                }
                do
                {
                    if (!first)
                    {
                        Console.WriteLine("Neplatná hodnota. Zkus to znovu.");
                        Console.WriteLine();
                    }
                    else
                        first = false;
                    Console.WriteLine("Zvol číslo {0} slotu, který obsahuje {1}{2} ({3}): ", (ofActivePlayer ? "svého" : "soupeřova"), (cardTypeToDodge != notCardType ? "použitelnou " : ""), (buildings ? "budovu" : "osobu"), sb.ToString());
                    answer = Console.ReadLine() ?? string.Empty;
                }
                while (!byte.TryParse(answer, out chosenSlot) || !validSlotNumbers.Contains(chosenSlot));
            }

            return (byte)(chosenSlot - 1);
        }        
        private static void RefundActionDie()
        {
            actionDice.Add((byte)(lastActivatedSlot + 1));
            actionDice.Sort();
        }
        private static void DiscardCardInPlay(bool ofActivePlayer, byte slot)
        {
            byte player = (byte)(ofActivePlayer ? ActivePlayer : (ActivePlayer ^ 1));
            discardPile.Add(slots[player][slot]!);
            slots[player][slot] = emptySlotValue;
        }
        private static byte GetBonusDefense(byte attackedSlot)
        {
            byte numberOfHelpingTowers = 0;
            for (int i = 0; i < 6; i++)
            {
                if (slots[ActivePlayer ^ 1][i] != emptySlotValue && slots[ActivePlayer ^ 1][i] is Turris && i != attackedSlot) numberOfHelpingTowers++;
            }
            return numberOfHelpingTowers;
        }
        private static void LowerDefenseOfOpponent(byte byHowMuch)
        {
            opponentsDefenseLoweredBy += byHowMuch;
        }
        private static bool CanWinByAddingActionDie(byte combatDie, byte goal)
        {
            // The action die used to activate Centurion has to be removed before his ability is activated => always remove the die before using ability.s
            return combatDie + (actionDice.Count > 0 ? actionDice.Max() : 0) >= goal;
        }
        internal static void AttackSlot(byte slot, bool canAddActionDie)
        {
            Console.WriteLine();
            Console.WriteLine("ÚTOK:");
            byte goal = slots[ActivePlayer ^ 1][slot]!.Defense; // Must be handled: Player cannot choose an empty slot as a target of attack.
            goal += GetBonusDefense(slot);
            goal -= opponentsDefenseLoweredBy;
            Console.WriteLine("Potřebná hodnota pro zničení soupeřovy karty: {0}", goal);
            Console.Write("Zmáčkni libovolnou klávesu pro hod bojovou kostkou... ");
            Console.ReadKey();
            byte combatDie = (byte)random.Next(1, 7);
            Console.WriteLine("Bojová kostka: {0}", combatDie);
            if (combatDie >= goal)  // successful attack
            {
                DiscardCardInPlay(false, slot);
                Console.WriteLine("Útok byl úspěšný! Soupeřova karta je odhozena na odhazovací balíček.");
                ActionFinished();
            }
            else
            {
                if (canAddActionDie && CanWinByAddingActionDie(combatDie, goal))
                {
                    Console.WriteLine("Útočná síla zatím nestačí. Je ještě potřeba alespoň {0} další útočné síly.", goal - combatDie);
                    Console.Write("Chceš přidat svou nepoužitou akční kostku, aby byl útok úspěšný? (a = ano, n = ne)");
                    if ((Console.ReadLine() ?? string.Empty).ToLower() == "a")
                    {
                        byte chosenActionDie = actionDice[actionDice.Count - 1];
                        if (actionDice.Count == 2 && actionDice[0] + combatDie >= goal && actionDice[0] != actionDice[1]) // both dice can help win the combat and are not the same
                        {
                            chosenActionDie = ChooseActionDie();
                        }
                        else
                        {
                            actionDice.RemoveAt(actionDice.Count - 1);
                        }
                        Console.WriteLine("Akční kostka byla spotřebována.");
                        Console.WriteLine("Celková síla útoku: {0}", combatDie + chosenActionDie);
                        Console.WriteLine("Celková obrana napadené karty: {0}", goal);
                        Console.WriteLine("Útok byl úspěšný.");
                        ActionFinished();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Akční kostka nebyla přidána.");
                    }
                }
                else if (canAddActionDie)
                {
                    Console.WriteLine("Útok není možné vyhrát, a proto bylo o nepřidání akční kostky rozhodnuto automaticky.");
                }
                // Attack was unsuccessful:
                Console.WriteLine("Útok byl neúspěšný.");
                ActionFinished();
            }
        }
        private static void AttackSameSlot(bool canAddActionDie)
        {
            if (slots[ActivePlayer ^ 1][lastActivatedSlot] != emptySlotValue)
                AttackSlot(lastActivatedSlot, canAddActionDie);
            else
            {
                Console.WriteLine();
                PrintActionUnavailable("v soupeřově protějším slotu není žádná karta");
                RefundActionDie();
            }
        }
        private static void AttackSlotOfCertainType(bool buildings)
        {
            if (HasPlayerNoCardsOfCertainType(false, buildings))
            {
                Console.WriteLine();
                PrintActionUnavailable($"soupeř nemá žádné vyložené karty {(buildings ? "budov" : "osob")}");
                RefundActionDie();
            }
            else
            {
                byte chosenSlot = ChooseSlotOfCertainType(false, buildings, notCardType);
                AttackSlot(chosenSlot, false);
                ActionFinished();
            }
        }
        private static bool IsSlotNotEmpty(bool activePlayer, byte slot)
        {
            return slots[activePlayer ? ActivePlayer : ActivePlayer ^ 1][slot] != emptySlotValue;
        }
        private static void RepositionCards(bool buildings)
        {
            List<byte> repositionedSlots = new List<byte>();
            for (byte i = 0; i < 6; i++)
            {
                if (IsSlotNotEmpty(true, i) && slots[ActivePlayer][i]!.Building == buildings)
                {
                    repositionedSlots.Add(i);
                }
            }
            if (repositionedSlots.Count <= 0)
            {
                Console.WriteLine();
                PrintActionUnavailable($"ve svých slotech nemáš žádnou {(buildings ? "budovu" : "osobu")}");
                RefundActionDie();
            }
            else
            {
                List<Card> tempPile = new List<Card>();
                byte chosenSlot;
                for (int i = 0; i < repositionedSlots.Count; ++i)
                {
                    tempPile.Add(slots[ActivePlayer][i]!);
                    slots[ActivePlayer][i] = emptySlotValue;
                }
                tempPile.OrderBy(card => card.Name);

                // Place all the cards back in the slots.
                for (int i = tempPile.Count; i > 0; i--)
                {
                    Console.WriteLine();
                    Console.WriteLine("Zvol si kartu, kterou chceš nyní umístit. Poté si zvol slot, kam ji chceš umístit. Postupně tak umístíš každou nabízenou kartu. Je možné překrýt existující karty.");
                    PickOneFromPile(tempPile, 1);   // Adds the picked card to hand.
                    chosenSlot = ChooseSlot();
                    if (slots[ActivePlayer][chosenSlot] != emptySlotValue)
                        discardPile.Add(slots[ActivePlayer][chosenSlot]!);  // Existing card is replaced and therefore discarded.
                    slots[ActivePlayer][chosenSlot] = hands[ActivePlayer][hands[ActivePlayer].Count - 1];   // Adds the picked card to the chosen slot.
                    hands[ActivePlayer].RemoveAt(hands[ActivePlayer].Count - 1);    // Removes the picked card from hand again, since it never should have been there.
                }
                ActionFinished();
            }
        }
        private static void RepositionCharacters()
        {
            RepositionCards(false);
        }
        private static void RepositionBuildings()
        {
            RepositionCards(true);
        }
        private static void ChangeActionDieValueByOne()
        {
            if (actionDice.Count <= 0)
            {
                Console.WriteLine();
                PrintActionUnavailable("už nemáš žádnou nepoužitou akční kostku.");
                RefundActionDie();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Zvol si kostku, jejíž hodnotu chceš změnit o 1.");
                byte chosenActionDie = ChooseActionDie();   // This automatically removes the action die from actionDice.
                if (chosenActionDie == 1)
                {
                    actionDice.Add(2);
                    Console.WriteLine("K hodnotě vybrané akční kostky byla automaticky přičtena jednička.");
                }
                else if (chosenActionDie == 6)
                {
                    actionDice.Add(5);
                    Console.WriteLine("Z hodnoty vybrané akční kostky byla automaticky odečtena jednička.");
                }
                else
                {
                    string answer;
                    char chosenAction;
                    bool first = true;
                    char[] possibilities = { '-', '1', '+', '2' };
                    do
                    {
                        if (!first)
                        {
                            Console.WriteLine("Neplatná hodnota. Zkus to znovu.");
                            Console.WriteLine();
                        }
                        else
                            first = false;
                        Console.Write($"Urči, zda se bude jednička odčítat ({possibilities[0]} nebo {possibilities[1]}), nebo přičítat ({possibilities[2]} nebo {possibilities[3]}): ");
                        answer = Console.ReadLine() ?? string.Empty;
                    }
                    while (!char.TryParse(answer, out chosenAction) || !possibilities.Contains(chosenAction));

                    if (chosenAction == possibilities[0] || chosenAction == possibilities[1])
                    {
                        actionDice.Add((byte)(chosenActionDie - 1));
                        Console.WriteLine("Z hodnoty vybrané akční kostky byla odečtena jednička.");
                    }
                    else
                    {
                        actionDice.Add((byte)(chosenActionDie + 1));
                        Console.WriteLine("K hodnotě vybrané akční kostky byla přičtena jednička.");
                    }
                }
                ActionFinished();
            }
        }
        private static void GameEnded(byte winner)
        {
            Console.WriteLine();
            PrintStatsVictoryPoints();
            Console.WriteLine($"Zvítězil(a) {(winner == 0 ? namePlayer1 : namePlayer2)}! Gratulujeme. :-)");
            Console.WriteLine("KONEC HRY");
            gameState = GameStates.GameEnded;
        }
        internal static void CheckIfGameEnded()
        {
            if (TryGetWinnerPlayerIndex(out byte winner))
            {
                Console.WriteLine($"Hra skončila, protože {(playerStats[winner ^ 1][1] <= 0 ? "jednomu z hráčů došly vítězné body" : "v zásobě již nejsou žádné vítězné body")}.");
                GameEnded(winner);
            }
            else
            {
                Console.WriteLine("Hra stále pokračuje.");
                PrintStatsVictoryPoints();
            }
        }
        /// <summary>
        /// Shows who has more victory points and if the game ended yet.
        /// </summary>
        /// <param name="winningPlayer">Player 1 (= 0) if he has more victory points than the other player, player 2 (= 1) otherwise.</param>
        /// <returns>True if game ended, false if game still goes on.</returns>
        private static bool TryGetWinnerPlayerIndex(out byte winningPlayer)
        {
            winningPlayer = (byte)(playerStats[0][1] > playerStats[1][1] ? 0 : 1);
            return DidGameEnd();
        }
        private static bool DidGameEnd()
        {
            return DoesAnyoneHaveZeroVictoryPoints() || DidVictoryPointsRunOut();
        }
        private static bool DoesAnyoneHaveZeroVictoryPoints()
        {
            return playerStats[0][1] == 0 || playerStats[1][1] == 0;
        }
        private static bool DidVictoryPointsRunOut()
        {
            return playerStats[0][1] + playerStats[1][1] >= totalVictoryPoints;
        }
        private static byte GetNumberOfCertainCardsNextToActivatedSlot(Type cardType)
        {
            byte numberOfSearchedBuildings = 0;
            if (lastActivatedSlot == 0)
            {
                if (slots[ActivePlayer][1] != emptySlotValue && slots[ActivePlayer][1]!.GetType() == cardType)
                {
                    numberOfSearchedBuildings++;
                }
            }
            else if (lastActivatedSlot == 5)
            {
                if (slots[ActivePlayer][4] != emptySlotValue && slots[ActivePlayer][4]!.GetType() == cardType)
                {
                    numberOfSearchedBuildings++;
                }
            }
            else
            {
                if (slots[ActivePlayer][lastActivatedSlot - 1] != emptySlotValue && slots[ActivePlayer][lastActivatedSlot - 1]!.GetType() == cardType)
                {
                    numberOfSearchedBuildings++;
                }
                if (slots[ActivePlayer][lastActivatedSlot + 1] != emptySlotValue && slots[ActivePlayer][lastActivatedSlot + 1]!.GetType() == cardType)
                {
                    numberOfSearchedBuildings++;
                }
            }

            return numberOfSearchedBuildings;
        }
        private static byte GetBasilicaNumberNextToActivatedForum()
        {
            return GetNumberOfCertainCardsNextToActivatedSlot(typeof(Basilica));
        }
        private static bool IsAnyTemplumNextToActivatedForum()
        {
            return GetNumberOfCertainCardsNextToActivatedSlot(typeof(Templum)) > 0;
        }
        internal static void LoseVictoryPointsToSupply(byte penalty)
        {
            Console.WriteLine("Celková ztráta vítězných bodů: {0}", penalty);
            playerStats[ActivePlayer][1] -= penalty;
            CheckIfGameEnded();
        }
        /// <summary>
        /// Adds the victory points, calls ActionFinished() and then CheckIfGameEnded().
        /// </summary>
        /// <param name="gain"></param>
        internal static void GainVictoryPointsFromSupply(byte gain)
        {
            Console.WriteLine("Celkový zisk vítězných bodů: {0}", gain);
            playerStats[ActivePlayer][1] += gain;
            ActionFinished();
            CheckIfGameEnded();
        }
        /// <summary>
        /// Adds the victory points to one player and takes them from the opponent, calls ActionFinished() and then CheckIfGameEnded().
        /// </summary>
        /// <param name="gain"></param>
        internal static void GainVictoryPointsFromOpponent(byte gain)
        {
            Console.WriteLine("Celkový zisk vítězných bodů: {0}", gain);
            playerStats[ActivePlayer][1] += gain;
            playerStats[ActivePlayer ^ 1][1] -= gain;
            ActionFinished();
            CheckIfGameEnded();
        }
        private static void TransferMoneyFromActivePlayerToOpponent(byte amount)
        {
            Console.WriteLine("Celkem zaplaceno sestercií: {0}", amount);
            playerStats[ActivePlayer][0] -= amount;
            playerStats[ActivePlayer ^ 1][0] += amount;
        }
        private static void GainVictoryPointsFromForum()
        {
            if (actionDice.Count <= 0)
            {
                Console.WriteLine();
                PrintActionUnavailable("na aktivaci této karty jsou potřeba dvě akční kostky a ty už máš pouze jednu");
                RefundActionDie();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Zvol si akční kostku, jejíž hodnota určí, kolik vítězných bodů ze zásoby obdržíš.");
                byte gain = ChooseActionDie();
                gain += (byte)(2 * GetBasilicaNumberNextToActivatedForum());
                if (IsAnyTemplumNextToActivatedForum())
                {
                    if (actionDice.Count > 0)
                    {
                        Console.WriteLine("Vedle tvého aktivovaného fóra je chrám. Přeješ si použít svou třetí akční kostku pro zisk doplňujících vítězných bodů ze zásob? (a = ano, n = ne)");
                        if ((Console.ReadLine() ?? string.Empty).ToLower() == "a")
                        {
                            gain += actionDice[0];
                            Console.WriteLine("Třetí akční kostka byla použita pro zisk doplňujících vítězných bodů (hodnota {0}).", actionDice[0]);
                            actionDice.RemoveAt(0);
                        }
                        else
                        {
                            Console.WriteLine("Třetí akční kostka nebyla použita a je stále k dispozici.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Jelikož už žádná akční kostka nezbývá, automaticky byla schopnost chrámu přeskočena.");
                    }
                }
                Console.WriteLine();
                GainVictoryPointsFromSupply(gain);                
            }
        }
        private static bool HasPlayerNoCardsOfCertainType(bool activePlayer, bool buildings)
        {
            for (byte i = 0; i < 6; i++)
            {
                byte player = (byte)(activePlayer ? ActivePlayer : ActivePlayer ^ 1);
                if (slots[player][i] != emptySlotValue && slots[player][i]!.Building == buildings)
                    return false;
            }

            return true;
        }
        private static bool HasPlayerNoCardsOfCertainTypeExcludingACardType(bool activePlayer, bool buildings, Type cardType)
        {
            for (byte i = 0; i < 6; i++)
            {
                byte player = (byte)(activePlayer ? ActivePlayer : ActivePlayer ^ 1);
                if (slots[player][i] != emptySlotValue && slots[player][i]!.Building == buildings && slots[player][i]!.GetType() != cardType)
                    return false;
            }

            return true;
        }
        private static void ReturnOpponentCharacterBackToHand()
        {
            if (HasPlayerNoCardsOfCertainType(false, false))
            {
                Console.WriteLine();
                PrintActionUnavailable("soupeř nemá vyložené žádné karty osob");
                RefundActionDie();
            }
            else
            {
                byte chosenSlot = ChooseSlotOfCertainType(false, false, notCardType);
                hands[ActivePlayer ^ 1].Add(slots[ActivePlayer ^ 1][chosenSlot]!);  // Adding to hand.
                slots[ActivePlayer ^ 1][chosenSlot] = emptySlotValue;               // Emptying the slot.
                ActionFinished();
            }
        }
        private static void GainVictoryPointsFromEmptyOpponentSlots()
        {
            byte emptySlotsOfOpponent = GetNumberOfEmptySlots(false);
            if (emptySlotsOfOpponent <= 0)
            {
                Console.WriteLine();
                PrintActionUnavailable("soupeř nemá žádné prázdné sloty");
                RefundActionDie();
            }
            else
            {
                Console.WriteLine();
                GainVictoryPointsFromSupply(emptySlotsOfOpponent);
            }
        }
        private static void BuyVictoryPointsFromOpponent()
        {
            if (playerStats[ActivePlayer][0] < 2)
            {
                Console.WriteLine();
                PrintActionUnavailable("nemáš dostatečné množství sestercií pro nákup jediného bodu (cena 2 sestercie za 1 vítězný bod)");
                RefundActionDie();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Zvol počet vítězných bodů, které si chceš od soupeře koupit. Každý 1 vítězný bod stojí 2 sestercie.");
                byte numberOfBoughtVictoryPoints = PromptForNumber(1, (byte)(playerStats[ActivePlayer][0] / 2));
                TransferMoneyFromActivePlayerToOpponent((byte)(2 * numberOfBoughtVictoryPoints));
                GainVictoryPointsFromOpponent(numberOfBoughtVictoryPoints);
            }
        }
        private static byte GetNumberOfForums(bool activePlayer)
        {
            byte forums = 0;
            for (byte i = 0; i < 6; i++)
            {
                byte player = (byte)(activePlayer ? ActivePlayer : ActivePlayer ^ 1);
                if (slots[player][i] != emptySlotValue && slots[player][i] is Forum)
                    forums++;
            }

            return forums;
        }
        private static void GainVictoryPointsFromOpponentForums()
        {
            byte forumsOfOponent = GetNumberOfForums(false);
            if (forumsOfOponent <= 0)
            {
                Console.WriteLine();
                PrintActionUnavailable("soupeř nemá žádná vyložená fóra");
                RefundActionDie();
            }
            else
            {
                Console.WriteLine();
                GainVictoryPointsFromOpponent(forumsOfOponent);
            }
        }
        private static void DiscardOpponentCardInPlayAndSelf(bool buildings)
        {
            if (HasPlayerNoCardsOfCertainType(false, buildings))
            {
                Console.WriteLine();
                PrintActionUnavailable($"soupeř nemá žádné vyložené karty {(buildings ? "budov" : "osob")}");
                RefundActionDie();
            }
            else
            {
                byte chosenSlot = ChooseSlotOfCertainType(false, buildings, notCardType);
                discardPile.Add(slots[ActivePlayer ^ 1][chosenSlot]!);      // Discarding opponent's building.
                slots[ActivePlayer ^ 1][chosenSlot] = emptySlotValue;       // Emptying the slot.
                discardPile.Add(slots[ActivePlayer][lastActivatedSlot]!);   // Discarding self.
                slots[ActivePlayer][lastActivatedSlot] = emptySlotValue;    // Emptying the slot after self.
                ActionFinished();
            }
        }
        private static void CopyAbility(bool buildings, Type cardType)
        {
            if (HasPlayerNoCardsOfCertainTypeExcludingACardType(true, buildings, cardType))
            {
                Console.WriteLine();
                PrintActionUnavailable($"nemáš vyložené žádné další karty {(buildings ? "budov" : "osob")}, od kterých by bylo možné schopnost zkopírovat");
                RefundActionDie();
            }
            else
            {
                Console.WriteLine();
                byte chosenSlotToCopy = ChooseSlotOfCertainType(true, buildings, notCardType);
                Console.WriteLine("Schopnost karty {0} byla úspěšně zkopírována! Schopnost se aktivuje.", slots[ActivePlayer][chosenSlotToCopy]!.Name);                
                slots[ActivePlayer][chosenSlotToCopy]!.ActivateAbility();
            }
        }
        private static void BlockSlotOfOpponent()
        {
            byte chosenSlot = ChooseNonBlockedSlot();
            blockedSlotsOfOpponentNextTurn.Add(chosenSlot);
            ActionFinished();
        }

        // Abilities:
        public static void AbilityAesculapinum()
        {
            PickOneCharacterFromDiscardPile();
        }
        public static void AbilityArchitectus()
        {
            MakeBuildingsFreeThisTurn();
        }
        public static void AbilityCenturio()
        {
            AttackSameSlot(true);
        }
        public static void AbilityConsiliarius()
        {
            RepositionCharacters();
        }
        public static void AbilityConsul()
        {
            ChangeActionDieValueByOne();
        }
        public static void AbilityEssedum()
        {
            LowerDefenseOfOpponent(2);
        }
        public static void AbilityForum()
        {
            GainVictoryPointsFromForum();
        }
        public static void AbilityGladiator()
        {
            ReturnOpponentCharacterBackToHand();
        }
        public static void AbilityHaruspex()
        {
            if (drawDeck.Count == 0)
            {
                MakeNewDrawDeck();
                Console.WriteLine("Jelikož byl dobírací balíček už prázdný, zamíchal se odkládací balíček a vytvořil nový dobírací balíček.");
            }
            PickOneFromDrawDeck();
        }
        public static void AbilityLegat()
        {            
            GainVictoryPointsFromEmptyOpponentSlots();
        }
        public static void AbilityLegionarius()
        {
            AttackSameSlot(false);
        }
        public static void AbilityMachina()
        {
            RepositionBuildings();
        }
        public static void AbilityMercator()
        {
            BuyVictoryPointsFromOpponent();
        }
        public static void AbilityMercatus()
        {
            GainVictoryPointsFromOpponentForums();
        }
        public static void AbilityNero()
        {
            DiscardOpponentCardInPlayAndSelf(true);
        }
        public static void AbilityOnager()
        {
            AttackSlotOfCertainType(true);
        }
        public static void AbilityPraetorianus()
        {
            BlockSlotOfOpponent();
        }
        public static void AbilityScaenicus()
        {
            CopyAbility(false, typeof(Scaenicus));
        }
        public static void AbilitySenator()
        {
            MakeCharactersFreeThisTurn();
        }
        public static void AbilitySicarius()
        {
            DiscardOpponentCardInPlayAndSelf(false);
        }
        public static void AbilityTribunusPlebis()
        {
            Console.WriteLine();
            GainVictoryPointsFromOpponent(1);
        }
        public static void AbilityVelites()
        {
            AttackSlotOfCertainType(false);
        }
    }
}
