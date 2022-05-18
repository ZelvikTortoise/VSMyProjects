using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Riskuj
{
    public partial class FormGame : Form
    {
        #region Constructor
        public FormGame()
        {
            InitializeComponent();

            DoOnlyAtTheBeginningOfTheProgram();
        }
        #endregion
        #region Enumerables
        //
        // Enumerables: 
        //
        enum States { Start, Choosing, AnsweringNormal, AnsweringPremiumNoStake, AnsweringPremiumStake, LuckyBrick, EndOfRound, End }
        enum ActiveTeam { None, Team1, Team2, Team3, Team4 }
        enum PremiumAnswers { None, A, B, C }
        enum Bonuses { Lumos, PetrificusTotalus, Protego, Reparifors }
        //
        // End of Enumerables.
        //
        #endregion
        #region Variables
        //
        // Variables:
        //
        States state = States.Start;
        const string roundTextSuffix = ".\n\nk\no\nl\no";
        public const int startingRound = 1;
        public const int lastRound = 2;
        int round = startingRound;
        int maxTime;
        int time;
        ActiveTeam activeTeam = ActiveTeam.None;
        ActiveTeam teamThatSelectedLastQuestion = ActiveTeam.None;
        Button selectedQuestion;
        PremiumAnswers correct = PremiumAnswers.None;
        Random random = new Random();
        List<Tuple<int, int>> luckyBricks = new List<Tuple<int, int>>();
        const int luckyBricksRound1 = 3;
        const int luckyBricksRound2 = 2;
        Stack<Tuple<Bonuses, Bonuses>> bonuses = new Stack<Tuple<Bonuses, Bonuses>>();

        Bonuses? activeBonus = null;
        string activeBonusHouse = null;
        bool bonusChosen = false;
        static readonly Image lumos = Properties.Resources.Lumos;
        static readonly Image lumos_1 = Properties.Resources.Lumos_1;
        static readonly Image lumos_2 = Properties.Resources.Lumos_2;
        static readonly Image lumos_3 = Properties.Resources.Lumos_3;
        static readonly Image petrificusTotalus = Properties.Resources.PetrificusTotalus;
        static readonly Image petrificusTotalus_1 = Properties.Resources.PetrificusTotalus_1;
        static readonly Image petrificusTotalus_2 = Properties.Resources.PetrificusTotalus_2;
        static readonly Image petrificusTotalus_3 = Properties.Resources.PetrificusTotalus_3;
        static readonly Image protego = Properties.Resources.Protego;
        static readonly Image protego_1 = Properties.Resources.Protego_1;
        static readonly Image protego_2 = Properties.Resources.Protego_2;
        static readonly Image protego_3 = Properties.Resources.Protego_3;
        static readonly Image reparifors = Properties.Resources.Reparifors;
        static readonly Image reparifors_1 = Properties.Resources.Reparifors_1;
        static readonly Image reparifors_2 = Properties.Resources.Reparifors_2;
        static readonly Image reparifors_3 = Properties.Resources.Reparifors_3;
        readonly Image[,] matrixOfBonusImages = new Image[4, 4]
            {
                { lumos, lumos_1, lumos_2, lumos_3 },
                { petrificusTotalus, petrificusTotalus_1, petrificusTotalus_2, petrificusTotalus_3 },
                { protego, protego_1, protego_2, protego_3 },
                { reparifors, reparifors_1, reparifors_2, reparifors_3 }
            };  // Same order as in the Bonuses Enum.
        int[] bonuses1 = new int[4] { 0, 0, 0, 0 };
        int[] bonuses2 = new int[4] { 0, 0, 0, 0 };
        int[] bonuses3 = new int[4] { 0, 0, 0, 0 };
        int[] bonuses4 = new int[4] { 0, 0, 0, 0 };
        const string lumosToolTip = "Lumos:\n\nTýmu, který toto kouzlo použije, se odkryje znění libovolné nezodpovězené otázky (z kola, ve kterém ji použije).\nOstatní týmy ví, na kterou otázku bylo kouzlo použito, ale její znění jim zůstává utajeno.\nPoužít je možné kdykoliv ve fázi, kdy se vybírá otázka, a to i když tým není na tahu.\n\nPoznámka: Je možné odkrýt i znění prémiové otázky. Tým se však tímto kouzlem nedozví možné odpovědi.";
        const string petrificusTotalusToolTip = "Petrificus Totalus:\n\nPokud někdo na váš tým použije toto kouzlo, nemůžete se zúčastnit odpovídání na aktuální neprémiovou otázku.\nToto kouzlo se dá použít, jakmile se vybere nějaká neprémiová otázka a spustí se časomíra. K použití stačí zakřičet Petrificus Totalus a ukázat na tým, na který kouzlo používáte.\nVybrat si můžete i tým, který už se přihlásil, že bude odpovídat. Kouzlo však musíte vyčarovat dříve, než odpoví.\n\nPoznámka: Reparifors může efekt tohoto kouzla vyrušit.";
        const string protegoToolTip = "Protego:\n\nToto kouzlo vám zajistí, že pokud ve druhém kole vsadíte na prémiovou otázku a odpovíte špatně, ztratíte pouze čtvrtinu ze vsazené částky namísto poloviny.\nProtego lze použít ve chvíli, kdy váš tým ve druhém kole odpovídá na prémiovou otázku – přesneji těsně před tím, než zvolí odpověď.\n\nPoznámka: Pokud tým toto kouzlo použil a na otázku odpověděl správně, je kouzlo také vyčerpáno.";
        const string repariforsToolTip = "Reparifors:\n\nPoužití tohoto kouzla vám umožní odpovědět znovu na neprémiovou otázku, na kterou jste již zkusili odpovědět, ale bylo to špatně.\nKouzlo se aktivuje tak, že se znovu přihlásíte k odpovídání, přestože je vaše skóre zešedlé. Moderátor pak stopne časomíru, kouzlo odklikne a označí vás jako odpovídající tým.\n\nPoznámka: Tímto kouzlem lze vyrušit efekt kouzla Petrificus Totalus.";
        static string house1;          // "Nebelvír";
        static string house2;          // "Mrzimor";
        static string house3;          // "Havraspár";
        static string house4;          // "Zmijozel";
        string[] houses;
        readonly Color defaultBackColor = SystemColors.Control;
        const int percentageOfStakeLost = 50;   // 50 %
        // NOTE: Integer division of 100 and percentageOfStakeLost is calculated.
        // The stake value is then put into negative values and divided by the integer quotient.
        // For example for percentageOfStakeLost = 33, the loss is excatly stake / 3 as another integer division.

        List<Button> scores = new List<Button>();
        List<Button> area1 = new List<Button>();
        List<Button> area2 = new List<Button>();
        List<Button> area3 = new List<Button>();
        List<Button> area4 = new List<Button>();
        List<Button> area5 = new List<Button>();
        List<Button> area6 = new List<Button>();
        bool enabledPremium1 = false;
        bool enabledPremium2 = false;
        bool enabledPremium3 = false;
        bool enabledPremium4 = false;
        bool enabledPremium5 = false;
        bool enabledPremium6 = false;
        List<RadioButton> possibilities = new List<RadioButton>();
        List<Button> questionsToBecomeVisible = new List<Button>();
        const string start = "Start";
        const string nextQuestion = "Další";
        const string nextRound = "Další kolo";
        const string results = "Výsledky";

        internal string[] finalOrder;                          
        internal string[] finalScores;
        internal Color[] finalColors;
        //
        // End of Variables.
        //
        #endregion
        #region Methods
        //
        // Methods:
        //
        internal void CallMeWhenAccessingThisForm()
        {
            // Initializing of the maximum time.
            maxTime = Program.maxTime;
            time = maxTime;

            // Initializing of the team names.
            house1 = Program.teamName1;
            house2 = Program.teamName2;
            house3 = Program.teamName3;
            house4 = Program.teamName4;
            houses = new string[4] { house1, house2, house3, house4 };

            // Adding the potential adjective.
            if (Program.adjective != "" || Program.adjective != null)
                this.Text = Program.adjective + " " + "Riskuj";
            else
                this.Text = "Riskuj";

            // Starting the game.
            ChangeToStartingState();
        }
        private void DoOnlyAtTheBeginningOfTheProgram()
        {
            CreateScoreButtonList();
            CreateAreaLists();
            CreatePossibilityList();
            AssignTeamBonusToolTips();
            AssignTeamBonusImages();
            AssignScoreValues();
            DisableAllTeamBonusButtons();
        }
        private void GoToFormEnd(FormEnd form)
        {
            if (!this.Visible)
                return;

            this.Visible = false;
            form.Location = this.Location;
            // form.StartPosition = FormStartPosition.Manual;   // not useful since the first form is much bigget than the other one
            form.FormClosing += delegate { Application.Exit(); };

            // Score parsing:
            finalOrder = new string[4];
            finalScores = new string[4];
            finalColors = new Color[4];

            int final1, final2, final3, final4;
            final1 = ParseScoreOrValue(scores[0]);
            final2 = ParseScoreOrValue(scores[1]);
            final3 = ParseScoreOrValue(scores[2]);
            final4 = ParseScoreOrValue(scores[3]);
            List<int> finalScoresList = new List<int> { final1, final2, final3, final4 };
            List<int> finalScoresListSorted = new List<int> { final1, final2, final3, final4 };
            finalScoresListSorted.Sort();
            List<int> usedIndeces = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int score = finalScoresListSorted[3 - i]; // Last element has the greatest value.
                finalScores[i] = score.ToString();

                int indexOfHouse = -1;
                for (int j = 0; j < finalScoresList.Count; j++)
                {
                    if (score == finalScoresList[j])
                    {
                        indexOfHouse = j;
                        if (usedIndeces.Contains(j))
                            continue;
                        usedIndeces.Add(j);
                        break;
                    }
                }
                if (indexOfHouse == -1)
                    throw new Exception("Dark magic and bad chakra are attacking us.");
                string house = houses[indexOfHouse];

                Color finalColor;
                if (house == house1)
                    finalColor = Color.Red;
                else if (house == house2)
                    finalColor = Color.Yellow;
                else if (house == house3)
                    finalColor = Color.Blue;
                else if (house == house4)
                    finalColor = Color.Green;
                else
                    throw new Exception("Unhandled house.");           
                finalColors[i] = finalColor;
                finalOrder[i] = $"{i + 1}. {house}";
            }

            form.Visible = true;
            Program.formEnd.CallMeWhenAccessingThisForm();
        }
        private void ChangeToStartingState()
        {
            state = States.Start;

            this.labelRoundNum.Text = round + roundTextSuffix;
            AssignQuestionTexts();

            if (round == startingRound)
            {

                RandomLuckyBricks(luckyBricksRound1);
                RandomBonuses();
                ResetAllScoresToEnabled();
                RandomAndSetStartingTeam();
            }
            else if (round == 2)
            {
                RandomLuckyBricks(luckyBricksRound2);
                ResetAllScoresToEnabled();
                ShowAllQuestions();
                Button starting = DetermineStartingTeam();
                ChangeActiveTeam(starting);
            }
            else
                throw new NotImplementedException("Only two rounds per game are supported.");

            buttonGameTime.Visible = true;
            buttonNextRound.Visible = false;
            DisableAllPremium();
            ChangeToChoosingState();
            PrintActiveHouseName();
        }
        private void ChangeToEndOfRoundState()
        {
            if (state != States.Start)
            {
                HideQuestion(selectedQuestion);
            }

            state = States.EndOfRound;

            ResetTimer(false);
            buttonGameTime.Visible = false;
            ResetAllScoresToEnabled();
            textBoxQuestionContent.Visible = false;
            buttonCorrect.Visible = false;
            buttonIncorrect.Visible = false;
            groupBoxPossibilities.Visible = false;
            radioButtonPossibilityA.Visible = false;
            radioButtonPossibilityB.Visible = false;
            radioButtonPossibilityC.Visible = false;
            buttonAnswerPremium.Visible = false;
            buttonBonus1.Visible = false;
            buttonBonus2.Visible = false;
            labelStake.Visible = false;
            textBoxStake.Visible = false;
            if (round == lastRound)
            {
                state = States.End;
                buttonNextRound.Text = results;
            }
            else
            {
                buttonNextRound.Text = nextRound;
            }
            buttonNextRound.Visible = true;
        }
        private void CreateScoreButtonList()
        {
            scores.Add(buttonScore1);
            scores.Add(buttonScore2);
            scores.Add(buttonScore3);
            scores.Add(buttonScore4);
        }
        private void CreateAreaLists()
        {
            area1.Add(buttonQuestion11);
            area1.Add(buttonQuestion12);
            area1.Add(buttonQuestion13);
            area1.Add(buttonQuestion14);
            area1.Add(buttonQuestion15);

            area2.Add(buttonQuestion21);
            area2.Add(buttonQuestion22);
            area2.Add(buttonQuestion23);
            area2.Add(buttonQuestion24);
            area2.Add(buttonQuestion25);

            area3.Add(buttonQuestion31);
            area3.Add(buttonQuestion32);
            area3.Add(buttonQuestion33);
            area3.Add(buttonQuestion34);
            area3.Add(buttonQuestion35);

            area4.Add(buttonQuestion41);
            area4.Add(buttonQuestion42);
            area4.Add(buttonQuestion43);
            area4.Add(buttonQuestion44);
            area4.Add(buttonQuestion45);

            area5.Add(buttonQuestion51);
            area5.Add(buttonQuestion52);
            area5.Add(buttonQuestion53);
            area5.Add(buttonQuestion54);
            area5.Add(buttonQuestion55);

            area6.Add(buttonQuestion61);
            area6.Add(buttonQuestion62);
            area6.Add(buttonQuestion63);
            area6.Add(buttonQuestion64);
            area6.Add(buttonQuestion65);
        }
        private void CreatePossibilityList()
        {
            possibilities.Add(radioButtonPossibilityA);
            possibilities.Add(radioButtonPossibilityB);
            possibilities.Add(radioButtonPossibilityC);
        }
        private void AssignQuestionTexts()
        {
            string[,] matrix;
            int min, max;

            if (round == 1)
            {
                matrix = Program.questions1;
                min = FormStart.minValue1;
                max = FormStart.maxValue1;
            }
            else if (round == 2)
            {
                matrix = Program.questions2;
                min = FormStart.minValue2;
                max = FormStart.maxValue2;
            }
            else
            {
                throw new Exception("Only two rounds per game are supported.");
            }

            int iterate = (max - min) / 4;
            Button[] questions1 = new Button[6]
            {
                buttonQuestion11,
                buttonQuestion21,
                buttonQuestion31,
                buttonQuestion41,
                buttonQuestion51,
                buttonQuestion61
            };
            Button[] questions2 = new Button[6]
            {
                buttonQuestion12,
                buttonQuestion22,
                buttonQuestion32,
                buttonQuestion42,
                buttonQuestion52,
                buttonQuestion62
            };
            Button[] questions3 = new Button[6]
            {
                buttonQuestion13,
                buttonQuestion23,
                buttonQuestion33,
                buttonQuestion43,
                buttonQuestion53,
                buttonQuestion63
            };
            Button[] questions4 = new Button[6]
            {
                buttonQuestion14,
                buttonQuestion24,
                buttonQuestion34,
                buttonQuestion44,
                buttonQuestion54,
                buttonQuestion64
            };
            Button[] questions5 = new Button[6]
            {
                buttonQuestion15,
                buttonQuestion25,
                buttonQuestion35,
                buttonQuestion45,
                buttonQuestion55,
                buttonQuestion65
            };
            Button[] questionsPremium = new Button[6]
            {
                buttonPremiumQuestion1,
                buttonPremiumQuestion2,
                buttonPremiumQuestion3,
                buttonPremiumQuestion4,
                buttonPremiumQuestion5,
                buttonPremiumQuestion6
            };

            Button[][] normalQuestionArrays = new Button[5][]
            {
                    questions1,
                    questions2,
                    questions3,
                    questions4,
                    questions5
            };

            // Assigning normal questions' texts.
            int value = min;
            for (int i = 0; i < normalQuestionArrays.Length; i++)
            {
                AssignValueAsText(normalQuestionArrays[i], value);
                value += iterate;
            }

            // Assigning premium questions' texts.
            for (int i = 0; i < questionsPremium.Length; i++)
            {
                questionsPremium[i].Text = matrix[0, i];
            }
        }
        private void AssignValueAsText(Button[] questions, int value)
        {
            string valueS = value.ToString();
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].Text = valueS;
            }
        }
        private void AssignScoreValues()
        {
            buttonScore1.Text = "0";
            buttonScore2.Text = "0";
            buttonScore3.Text = "0";
            buttonScore4.Text = "0";
        }
        private void AssignTeamBonusToolTips()
        {
            toolTipBonuses.SetToolTip(buttonBonus11, lumosToolTip);
            toolTipBonuses.SetToolTip(buttonBonus21, lumosToolTip);
            toolTipBonuses.SetToolTip(buttonBonus31, lumosToolTip);
            toolTipBonuses.SetToolTip(buttonBonus41, lumosToolTip);

            toolTipBonuses.SetToolTip(buttonBonus12, petrificusTotalusToolTip);
            toolTipBonuses.SetToolTip(buttonBonus22, petrificusTotalusToolTip);
            toolTipBonuses.SetToolTip(buttonBonus32, petrificusTotalusToolTip);
            toolTipBonuses.SetToolTip(buttonBonus42, petrificusTotalusToolTip);

            toolTipBonuses.SetToolTip(buttonBonus13, protegoToolTip);
            toolTipBonuses.SetToolTip(buttonBonus23, protegoToolTip);
            toolTipBonuses.SetToolTip(buttonBonus33, protegoToolTip);
            toolTipBonuses.SetToolTip(buttonBonus43, protegoToolTip);

            toolTipBonuses.SetToolTip(buttonBonus14, repariforsToolTip);
            toolTipBonuses.SetToolTip(buttonBonus24, repariforsToolTip);
            toolTipBonuses.SetToolTip(buttonBonus34, repariforsToolTip);
            toolTipBonuses.SetToolTip(buttonBonus44, repariforsToolTip);
        }
        private void AssignTeamBonusImages()
        {
            buttonBonus11.Image = lumos;
            buttonBonus21.Image = lumos;
            buttonBonus31.Image = lumos;
            buttonBonus41.Image = lumos;

            buttonBonus12.Image = petrificusTotalus;
            buttonBonus22.Image = petrificusTotalus;
            buttonBonus32.Image = petrificusTotalus;
            buttonBonus42.Image = petrificusTotalus;

            buttonBonus13.Image = protego;
            buttonBonus23.Image = protego;
            buttonBonus33.Image = protego;
            buttonBonus43.Image = protego;

            buttonBonus14.Image = reparifors;
            buttonBonus24.Image = reparifors;
            buttonBonus34.Image = reparifors;
            buttonBonus44.Image = reparifors;
        }
        private void DisableAllTeamBonusButtons()
        {
            buttonBonus11.Enabled = false;
            buttonBonus21.Enabled = false;
            buttonBonus31.Enabled = false;
            buttonBonus41.Enabled = false;

            buttonBonus12.Enabled = false;
            buttonBonus22.Enabled = false;
            buttonBonus32.Enabled = false;
            buttonBonus42.Enabled = false;

            buttonBonus13.Enabled = false;
            buttonBonus23.Enabled = false;
            buttonBonus33.Enabled = false;
            buttonBonus43.Enabled = false;

            buttonBonus14.Enabled = false;
            buttonBonus24.Enabled = false;
            buttonBonus34.Enabled = false;
            buttonBonus44.Enabled = false;
        }
        private void RandomAndSetStartingTeam()
        {
            int starting = random.Next(1, 5);   // Randoming numbers from 1 to 4.
            activeTeam = (ActiveTeam)starting;
            HighlightOnlyActiveTeam();
        }
        private Button DetermineStartingTeam()
        {
            int[] scoresInt = new int[4];
            for (int i = 0; i < 4; i++)
            {
                scoresInt[i] = ParseScoreOrValue(scores[i]);
            }

            int indexOfMax = GetIndexOfMaximum(scoresInt);

            return scores[indexOfMax];
        }
        private int GetIndexOfMaximum(int[] array)
        {
            if (array.Length == 0)
                return -1;

            if (array.Length == 1)
                return 0;

            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                    maxIndex = i;
            }
            return maxIndex;
        }
        private void RandomLuckyBricks(int amount)
        {
            int toBeRandomed = amount;
            int i, j;
            bool collision;
            while (toBeRandomed != 0)
            {
                i = random.Next(0, 5);
                j = random.Next(0, 6);
                Tuple<int, int> tuple = new Tuple<int, int>(i, j);
                collision = false;
                foreach (Tuple<int, int> brick in luckyBricks)
                {
                    if (tuple.Item1 == brick.Item1 && tuple.Item2 == tuple.Item2)
                    {
                        collision = true;
                        break;
                    }
                }

                if (!collision)
                {
                    luckyBricks.Add(tuple);
                    toBeRandomed--;
                }
            }
        }
        private bool IsLuckyBrick(int row, int column)
        {
            var tuple = new Tuple<int, int>(row, column);
            foreach (Tuple<int, int> brick in luckyBricks)
            {
                if (tuple.Item1 == brick.Item1 && tuple.Item2 == brick.Item2)
                {
                    luckyBricks.Remove(brick);
                    return true;
                }
            }

            return false;
        }
        private void RandomBonuses()
        {
            List<Tuple<Bonuses, Bonuses>> toChoose = new List<Tuple<Bonuses, Bonuses>>()
            {
                new Tuple<Bonuses, Bonuses>(Bonuses.Lumos, Bonuses.Protego),
                new Tuple<Bonuses, Bonuses>(Bonuses.Lumos, Bonuses.Reparifors),
                new Tuple<Bonuses, Bonuses>(Bonuses.PetrificusTotalus, Bonuses.Lumos),
                new Tuple<Bonuses, Bonuses>(Bonuses.Protego, Bonuses.Reparifors),
                new Tuple<Bonuses, Bonuses>(Bonuses.PetrificusTotalus, Bonuses.Protego),
                new Tuple<Bonuses, Bonuses>(Bonuses.Reparifors, Bonuses.PetrificusTotalus)
            };

            int index;
            for (int i = 1; i <= 6; i++)
            {
                index = random.Next(0, toChoose.Count);
                bonuses.Push(toChoose[index]);
                toChoose.RemoveAt(index);
            }
        }
        private void DisableAllPremium()
        {
            enabledPremium1 = false;
            enabledPremium2 = false;
            enabledPremium3 = false;
            enabledPremium4 = false;
            enabledPremium5 = false;
            enabledPremium6 = false;
        }

        private bool CanPremiumBeEnabled(int areaNum)
        {
            List<Button> area;
            int remaining = 0;
            switch (areaNum)
            {
                case 1:
                    area = area1;
                    break;
                case 2:
                    area = area2;
                    break;
                case 3:
                    area = area3;
                    break;
                case 4:
                    area = area4;
                    break;
                case 5:
                    area = area5;
                    break;
                case 6:
                    area = area6;
                    break;
                default:
                    throw new ArgumentException("Only area numbers from 1 to 6 are allowed.");
            }

            foreach (Button question in area)
            {
                if (question.Visible)
                {
                    // The last non-premium question of the area is open (therefore visible) when this method is called.
                    remaining++;
                }

                if (remaining > 1)
                {
                    return false;
                }
            }

            return true;
        }
        private void ParseQuestionContent(int matrixRow, int matrixColumn)
        {
            // Rows:
            // 0 ... name
            // 1–5 ... normal questions
            // 6 ... premium question
            // 7–9 ... answers to the premium question
            // Columns:
            // 0–5 ... areas
            string[,] matrix;
            bool premium = false;

            if (round == 1)
            {
                matrix = Program.questions1;
            }
            else if (round == 2)
            {
                matrix = Program.questions2;
            }
            else
            {
                throw new NotImplementedException("Only two round per game are supported.");
            }

            if (matrixRow == 6)
            {
                premium = true;
            }

            if (!premium)
            {
                textBoxQuestionContent.Text = matrix[matrixRow, matrixColumn];
            }
            else
            {
                string textA, textB, textC;
                textA = matrix[7, matrixColumn];
                textB = matrix[8, matrixColumn];
                textC = matrix[9, matrixColumn];

                if (matrix[7, matrixColumn].StartsWith(FormStart.correctAnswerBeginning))
                {
                    correct = PremiumAnswers.A;
                    textA = textA.Remove(0, FormStart.correctAnswerBeginning.Length);
                }
                else if (matrix[8, matrixColumn].StartsWith(FormStart.correctAnswerBeginning))
                {
                    correct = PremiumAnswers.B;
                    textB = textB.Remove(0, FormStart.correctAnswerBeginning.Length);
                }
                else if (matrix[9, matrixColumn].StartsWith(FormStart.correctAnswerBeginning))
                {
                    correct = PremiumAnswers.C;
                    textC = textC.Remove(0, FormStart.correctAnswerBeginning.Length);
                }
                else
                {
                    throw new Exception(string.Format("No correct answer marked in the input file: Area {0}, round {1}.", matrixColumn, round));
                }

                textBoxQuestionContent.Text = matrix[matrixRow, matrixColumn];
                groupBoxPossibilities.Text = "Možnosti:";
                radioButtonPossibilityA.Text = string.Concat("a) ", textA);
                radioButtonPossibilityB.Text = string.Concat("b) ", textB);
                radioButtonPossibilityC.Text = string.Concat("c) ", textC);
            }
        }
        /// <summary>
        /// Does not manipulate with the active team!
        /// </summary>
        private void ChangeToChoosingState()
        {
            if (state != States.Start)
            {
                HideQuestion(selectedQuestion);
            }

            state = States.Choosing;

            ResetTimer(false);
            ResetAllScoresToEnabled();
            textBoxQuestionContent.Visible = false;
            buttonCorrect.Visible = false;
            buttonIncorrect.Visible = false;
            groupBoxPossibilities.Visible = false;
            radioButtonPossibilityA.Visible = false;
            radioButtonPossibilityB.Visible = false;
            radioButtonPossibilityC.Visible = false;
            buttonAnswerPremium.Visible = false;
            bonusChosen = false;
            buttonBonus1.Visible = false;
            buttonBonus2.Visible = false;
            labelStake.Visible = false;
            textBoxStake.Visible = false;
        }
        private void ChangeToNormalAnsweringState(int valueOrder, int areaNum)
        {
            state = States.AnsweringNormal;

            NoActiveTeam();
            ParseQuestionContent(valueOrder, areaNum);
            textBoxQuestionContent.Visible = true;
            buttonCorrect.Visible = true;
            buttonIncorrect.Visible = true;
            groupBoxPossibilities.Visible = false;
            radioButtonPossibilityA.Visible = false;
            radioButtonPossibilityB.Visible = false;
            radioButtonPossibilityC.Visible = false;
            buttonAnswerPremium.Visible = false;
            buttonBonus1.Visible = false;
            buttonBonus2.Visible = false;
            labelStake.Visible = false;
            textBoxStake.Visible = false;

            switch (areaNum)
            {
                case 0:
                    enabledPremium1 = CanPremiumBeEnabled(1);
                    break;
                case 1:
                    enabledPremium2 = CanPremiumBeEnabled(2);
                    break;
                case 2:
                    enabledPremium3 = CanPremiumBeEnabled(3);
                    break;
                case 3:
                    enabledPremium4 = CanPremiumBeEnabled(4);
                    break;
                case 4:
                    enabledPremium5 = CanPremiumBeEnabled(5);
                    break;
                case 5:
                    enabledPremium6 = CanPremiumBeEnabled(6);
                    break;
                default:
                    throw new ArgumentException("Only area numbers from 0 to 5 are allowed.");
            }

            ResetTimer(true);
        }
        private void ChangeToPremiumAnsweringNoStakeState(int areaNum)
        {
            state = States.AnsweringPremiumNoStake;

            ParseQuestionContent(6, areaNum);   // Finds correct answer
            textBoxQuestionContent.Visible = true;
            buttonCorrect.Visible = false;
            buttonIncorrect.Visible = false;
            groupBoxPossibilities.Visible = true;
            ResetAnswerHighlightingAndChecks();
            radioButtonPossibilityA.Visible = true;
            radioButtonPossibilityB.Visible = true;
            radioButtonPossibilityC.Visible = true;
            buttonAnswerPremium.Visible = true;
            buttonAnswerPremium.Text = "Odpovědět";
            AssignCorrectBonusImagesAndTooltips();
            EnablePremiumBonusButtons();
            buttonBonus1.Visible = true;
            buttonBonus2.Visible = true;
            labelStake.Visible = false;
            textBoxStake.Visible = false;

        }
        private void ChangeToPremiumAnsweringWithStakeState(int areaNum)
        {
            state = States.AnsweringPremiumStake;

            ParseQuestionContent(6, areaNum);   // Finds correct answer
            textBoxQuestionContent.Visible = true;
            buttonCorrect.Visible = false;
            buttonIncorrect.Visible = false;
            groupBoxPossibilities.Visible = true;
            ResetAnswerHighlightingAndChecks();
            radioButtonPossibilityA.Visible = true;
            radioButtonPossibilityB.Visible = true;
            radioButtonPossibilityC.Visible = true;
            buttonAnswerPremium.Visible = true;
            buttonAnswerPremium.Text = "Odpovědět";
            buttonBonus1.Visible = false;
            buttonBonus2.Visible = false;
            labelStake.Visible = true;
            textBoxStake.Enabled = true;
            textBoxStake.Visible = true;
            textBoxStake.Text = "0";
        }
        private void ChangeToLuckyBrickState(int value, int areaNum)
        {
            state = States.LuckyBrick;

            PrintLuckyBrickMessage(value);
            textBoxQuestionContent.Visible = true;
            buttonCorrect.Visible = false;
            buttonIncorrect.Visible = false;
            groupBoxPossibilities.Visible = false;
            radioButtonPossibilityA.Visible = false;
            radioButtonPossibilityB.Visible = false;
            radioButtonPossibilityC.Visible = false;
            buttonAnswerPremium.Visible = true;
            buttonAnswerPremium.Text = "Vyzvednout";
            buttonBonus1.Visible = false;
            buttonBonus2.Visible = false;
            labelStake.Visible = false;
            textBoxStake.Visible = false;

            switch (areaNum)
            {
                case 0:
                    enabledPremium1 = CanPremiumBeEnabled(1);
                    break;
                case 1:
                    enabledPremium2 = CanPremiumBeEnabled(2);
                    break;
                case 2:
                    enabledPremium3 = CanPremiumBeEnabled(3);
                    break;
                case 3:
                    enabledPremium4 = CanPremiumBeEnabled(4);
                    break;
                case 4:
                    enabledPremium5 = CanPremiumBeEnabled(5);
                    break;
                case 5:
                    enabledPremium6 = CanPremiumBeEnabled(6);
                    break;
                default:
                    throw new ArgumentException("Only area numbers from 0 to 5 are allowed.");
            }
        }
        private void PrintLuckyBrickMessage(int value)
        {
            StringBuilder sb = new StringBuilder();
            if (round == 1)
                sb.Append("Bronzová");
            else if (round == 2)
                sb.Append("Stříbrná");
            else
                throw new NotImplementedException("Only two rounds per game are supported.");

            sb.AppendFormat(" cihlička!{0}", Environment.NewLine);
            sb.AppendFormat("Gratulujeme, na tuto otázku nemusíte odpovídat.{0}{0}", Environment.NewLine);
            sb.Append("Štěstím tak získáváte ");
            sb.Append(value.ToString());
            sb.Append(" bodů!");

            textBoxQuestionContent.Text = sb.ToString();
        }
        private void AssignCorrectBonusImagesAndTooltips()
        {
            Tuple<Bonuses, Bonuses> tuple = bonuses.Pop();
            buttonBonus1.Image = GetCorrectBonusImage(tuple.Item1);
            buttonBonus2.Image = GetCorrectBonusImage(tuple.Item2);
            string[] toolTips = new string[2];
            for (int i = 0; i < toolTips.Length; i++)
            {
                Bonuses bonus;
                if (i == 0)
                    bonus = tuple.Item1;
                else if (i == 1)
                    bonus = tuple.Item2;
                else
                    throw new Exception("ToolTips are implemented for choosing from two bonuses at the time only.");

                switch (bonus)
                {
                    case Bonuses.Lumos:
                        toolTips[i] = lumosToolTip;
                        break;
                    case Bonuses.PetrificusTotalus:
                        toolTips[i] = petrificusTotalusToolTip;
                        break;
                    case Bonuses.Protego:
                        toolTips[i] = protegoToolTip;
                        break;
                    case Bonuses.Reparifors:
                        toolTips[i] = repariforsToolTip;
                        break;
                    default:
                        throw new Exception("Only four bonuses are implemented.");
                }
            }

            toolTipBonuses.SetToolTip(buttonBonus1, toolTips[0]);
            toolTipBonuses.SetToolTip(buttonBonus2, toolTips[1]);
        }
        private Image GetCorrectBonusImage(Bonuses bonus)
        {
            switch (bonus)
            {
                case Bonuses.Lumos:
                    return lumos;
                case Bonuses.PetrificusTotalus:
                    return petrificusTotalus;
                case Bonuses.Protego:
                    return protego;
                case Bonuses.Reparifors:
                    return reparifors;
                default:
                    throw new NotImplementedException("Only four bonuses are supported.");
            }
        }
        private bool AllAnsweredWrong()
        {
            foreach (Button score in scores)
                if (score.Enabled)
                    return false;

            return true;
        }
        private void ResetAllScoresToEnabled()
        {
            foreach (Button score in scores)
                if (!score.Enabled)
                    score.Enabled = true;

        }
        private void ChangeActiveTeamToTeamThatLastSelected()
        {
            activeTeam = teamThatSelectedLastQuestion;
            HighlightOnlyActiveTeam();
        }
        private void HighlightOnlyActiveTeam()
        {
            RemoveAllHighlighting();
            HighlightActiveTeam();
        }
        private void HighlightActiveTeam()
        {
            Color color;
            switch (activeTeam)
            {
                case ActiveTeam.None:
                    throw new ArgumentException("Nothing to be highlighted.");
                case ActiveTeam.Team1:
                    color = Color.Red;
                    break;
                case ActiveTeam.Team2:
                    color = Color.Yellow;
                    break;
                case ActiveTeam.Team3:
                    color = Color.Blue;
                    break;
                case ActiveTeam.Team4:
                    color = Color.Green;
                break;
                default:
                    throw new Exception("Only four teams per game are supported.");
            }

            Button activeTeamScore = FindActiveTeamScore();
            activeTeamScore.BackColor = color;
        }
        private void RemoveAllHighlighting()
        {
            foreach (Button score in scores)
            {
                RemoveHighlighting(score);
            }
        }
        private void RemoveHighlighting(Button score)
        {
            score.BackColor = defaultBackColor;
        }
        private void NoActiveTeam()
        {
            if (activeTeam != ActiveTeam.None)
                RemoveHighlighting(FindActiveTeamScore());
            activeTeam = ActiveTeam.None;
        }
        private void ChangeActiveTeam(Button teamScore)
        {
            if (teamScore == buttonScore1)
            {
                activeTeam = ActiveTeam.Team1;
            }
            else if (teamScore == buttonScore2)
            {
                activeTeam = ActiveTeam.Team2;
            }
            else if (teamScore == buttonScore3)
            {
                activeTeam = ActiveTeam.Team3;
            }
            else if (teamScore == buttonScore4)
            {
                activeTeam = ActiveTeam.Team4;
            }
            else
            {
                throw new ArgumentException("Only team buttons are allowed to be passed as an argument.");
            }

            if (activeTeam != ActiveTeam.None)
            {
                HighlightOnlyActiveTeam();
            }
        }
        private void ResetTimer(bool enable)
        {
            timerQuestion.Enabled = enable;
            if (enable)
                buttonGameTime.Text = maxTime.ToString();
            else
                buttonGameTime.Text = start;
            time = maxTime;
        }
        private void HideQuestion(Button question)
        {
            textBoxQuestionContent.Visible = false;
            buttonCorrect.Visible = false;
            buttonIncorrect.Visible = false;
            groupBoxPossibilities.Visible = false;
            radioButtonPossibilityA.Visible = false;
            radioButtonPossibilityB.Visible = false;
            radioButtonPossibilityC.Visible = false;
            buttonAnswerPremium.Visible = false;
            buttonBonus1.Visible = false;
            buttonBonus2.Visible = false;
            labelStake.Visible = false;
            textBoxStake.Visible = false;

            question.Visible = false;
            RemoveHighlightingFromQuestion(question);
            questionsToBecomeVisible.Add(question);
        }
        private void ShowAllQuestions()
        {
            for (int i = 0; i < questionsToBecomeVisible.Count; i++)
                questionsToBecomeVisible[i].Visible = true;
            questionsToBecomeVisible.Clear();
        }
        private void DisableActiveTeam()
        {
            Button teamScore = FindActiveTeamScore();
            teamScore.Enabled = false;
            activeTeam = ActiveTeam.None;
        }
        private void PrintActiveHouseName()
        {            
            if (activeTeam == ActiveTeam.None)
            {
                MessageBox.Show("Nikdo není na tahu.", "Nikdo se nehlásí", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = (int)activeTeam - 1;
                string house = houses[index];
                MessageBox.Show("Na tahu je tým " + house + ".", "S chutí do toho", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Button FindActiveTeamScore()
        {
            switch (activeTeam)
            {
                case ActiveTeam.Team1:
                    return buttonScore1;
                case ActiveTeam.Team2:
                    return buttonScore2;
                case ActiveTeam.Team3:
                    return buttonScore3;
                case ActiveTeam.Team4:
                    return buttonScore4;
                default:
                    throw new NotImplementedException("Only four teams are supported in one game.");
            }
        }
        private string FindTeamName(Button teamScore)
        {
            if (teamScore == buttonScore1)
                return house1;
            else if (teamScore == buttonScore2)
                return house2;
            else if (teamScore == buttonScore3)
                return house3;
            else if (teamScore == buttonScore4)
                return house4;
            else
                throw new ArgumentException($"The method {nameof(FindTeamName)} is called with argument not being a teamScoreButton.\n(Only four team are implemented.)");
        }
        private int ParseScoreOrValue(Button normalQuestion)
        {
            return int.Parse(normalQuestion.Text);
        }
        private void ChangeScoreOfActiveTeam(int value)
        {
            Button scoreOfActiveTeam = FindActiveTeamScore();
            int currentScore = ParseScoreOrValue(scoreOfActiveTeam);
            int newScore = currentScore + value;
            scoreOfActiveTeam.Text = newScore.ToString();
        }

        private void HighlightQuestion(Button question)
        {
            question.ForeColor = Color.Chartreuse;
            question.BackColor = Color.Tomato;
        }
        private void RemoveHighlightingFromQuestion(Button question)
        {
            question.ForeColor = SystemColors.ControlText;
            question.BackColor = SystemColors.Control;
        }

        private RadioButton GetSelectedRadioButton()
        {
            if (radioButtonPossibilityA.Checked)
                return radioButtonPossibilityA;
            else if (radioButtonPossibilityB.Checked)
                return radioButtonPossibilityB;
            else if (radioButtonPossibilityC.Checked)
                return radioButtonPossibilityC;
            else
                return null;
        }
        private PremiumAnswers GetSelectedPremiumAnswer()
        {
            if (radioButtonPossibilityA.Checked)
                return PremiumAnswers.A;
            else if (radioButtonPossibilityB.Checked)
                return PremiumAnswers.B;
            else if (radioButtonPossibilityC.Checked)
                return PremiumAnswers.C;
            else
                return PremiumAnswers.None;
        }
        private RadioButton GetCorrespondingRadioButton(PremiumAnswers answer)
        {
            if (answer == PremiumAnswers.A)
                return radioButtonPossibilityA;
            else if (answer == PremiumAnswers.B)
                return radioButtonPossibilityB;
            else if (answer == PremiumAnswers.C)
                return radioButtonPossibilityC;
            else if (answer == PremiumAnswers.None)
                return null;
            else
                throw new ArgumentException("The value " + answer.ToString() + " of PremiumAnswers is not supported.");
        }
        private void HighlightAnswerAsCorrect(RadioButton answer)
        {
            answer.ForeColor = Color.LimeGreen;
        }
        private void HighlightAnswerAsIncorrect(RadioButton answer)
        {
            answer.ForeColor = Color.Firebrick;
        }
        private void ResetAnswerHighlightingAndChecks()
        {
            for (int i = 0; i < possibilities.Count; i++)
            {
                possibilities[i].ForeColor = SystemColors.ControlText;
                possibilities[i].Checked = false;
            }
        }
        private Bonuses GetCorrespondingBonus(Image image)
        {
            if (image == lumos || image == lumos_1 || image == lumos_2 || image == lumos_3)
                return Bonuses.Lumos;
            else if (image == petrificusTotalus || image == petrificusTotalus_1 || image == petrificusTotalus_2 || image == petrificusTotalus_3)
                return Bonuses.PetrificusTotalus;
            else if (image == protego || image == protego_1 || image == protego_2 || image == protego_3)
                return Bonuses.Protego;
            else if (image == reparifors || image == reparifors_1 || image == reparifors_2 || image == reparifors_3)
                return Bonuses.Reparifors;
            else
                throw new ArgumentException("Unknown bonus image.");
        }
        private Button[] GetBonusButtonsOfActiveTeam()
        {
            Button[] buttons = new Button[4];
            switch (activeTeam)
            {
                case ActiveTeam.None:
                    return null;
                case ActiveTeam.Team1:
                    buttons[0] = buttonBonus11;
                    buttons[1] = buttonBonus12;
                    buttons[2] = buttonBonus13;
                    buttons[3] = buttonBonus14;
                    break;
                case ActiveTeam.Team2:
                    buttons[0] = buttonBonus21;
                    buttons[1] = buttonBonus22;
                    buttons[2] = buttonBonus23;
                    buttons[3] = buttonBonus24;
                    break;
                case ActiveTeam.Team3:
                    buttons[0] = buttonBonus31;
                    buttons[1] = buttonBonus32;
                    buttons[2] = buttonBonus33;
                    buttons[3] = buttonBonus34;
                    break;
                case ActiveTeam.Team4:
                    buttons[0] = buttonBonus41;
                    buttons[1] = buttonBonus42;
                    buttons[2] = buttonBonus43;
                    buttons[3] = buttonBonus44;
                    break;
                default:
                    throw new NotImplementedException("Only four team in one game are supported.");
            }

            return buttons;
        }
        private int[] GetBonusArrayOfActiveTeam()
        {
            switch (activeTeam)
            {
                case ActiveTeam.None:
                    return null;
                case ActiveTeam.Team1:
                    return bonuses1;
                case ActiveTeam.Team2:
                    return bonuses2;
                case ActiveTeam.Team3:
                    return bonuses3;
                case ActiveTeam.Team4:
                    return bonuses4;
                default:
                    throw new NotImplementedException("Only four team in one game are supported.");
            }
        }
        private int GetBonusButtonIndex(Bonuses bonus)
        {
            return (int)bonus;
        }
        private void AddBonusToActiveTeam(Bonuses bonus)
        {
            Button[] buttons = GetBonusButtonsOfActiveTeam();
            int index = GetBonusButtonIndex(bonus);
            int[] bonuses = GetBonusArrayOfActiveTeam();    // Via reference.

            if (bonuses[index] == 0)
                buttons[index].Enabled = true;
                
            bonuses[index]++;
            buttons[index].Image = matrixOfBonusImages[(int)bonus, bonuses[index]];
        }
        /// <summary>
        /// Assumes choosing only from two bonuses is possible at the time.
        /// Do not call this method if no bonus is selected.
        /// </summary>
        private void AddSelectedBonusToActiveTeam()
        {
            Button selected = buttonBonus1.Enabled ? buttonBonus1 : buttonBonus2;
            Bonuses bonusToBeAdded = GetCorrespondingBonus(selected.Image);
            AddBonusToActiveTeam(bonusToBeAdded);
        }
        private void EnablePremiumBonusButtons()
        {
            buttonBonus1.Enabled = true;
            buttonBonus2.Enabled = true;
        }
        private void DisablePremiumBonusButtons()
        {
            buttonBonus1.Enabled = false;
            buttonBonus2.Enabled = false;
        }

        private void EnableTimerWithMax()
        {
            if (time == maxTime)
                buttonGameTime.Text = maxTime.ToString();

            timerQuestion.Enabled = true;
        }
        /// <summary>
        /// Call this method before hiding the last premium question.
        /// </summary>
        /// <returns>True if only</returns>
        private bool IsLastQuestion()
        {
            int remaining = 0;
            int indexOfRemainingPremium = -1;
            Button[] premiumQuestions = new Button[6] {
                buttonPremiumQuestion1,
                buttonPremiumQuestion2,
                buttonPremiumQuestion3,
                buttonPremiumQuestion4,
                buttonPremiumQuestion5,
                buttonPremiumQuestion6 };

            for (int i = 0; i < premiumQuestions.Length; i++)
            {
                if (premiumQuestions[i].Visible)
                {
                    remaining++;
                    if (remaining > 1)
                        return false;   // More than one premium questions are still visible.
                    else
                        indexOfRemainingPremium = i;
                }
            }

            if (premiumQuestions[indexOfRemainingPremium].Enabled)
                return true;    // There is last premium question visible and it is the last question from its category.
            else
                return false;   // There is last premium question visible but there are still more questions in its category.
        }
        private Button GetHouseScore(string house)
        {
            if (house == house1)
                return buttonScore1;
            else if (house == house2)
                return buttonScore2;
            else if (house == house3)
                return buttonScore3;
            else if (house == house4)
                return buttonScore4;
            else
                throw new Exception("Unhandled house name.");
        }

        /// <summary>
        /// Handles casting Reparifors completely.
        /// </summary>
        /// <param name="bonusReparifors">Reparifors button</param>
        /// <returns>Did everything go well?</returns>
        private bool HandleReparifors(Button bonusReparifors)
        {
            Button score;
            if (bonusReparifors == buttonBonus14)
            {
                score = buttonScore1;
            }                
            else if (bonusReparifors == buttonBonus24)
            {
                score = buttonScore2;
            }                
            else if (bonusReparifors == buttonBonus34)
            {
                score = buttonScore3;
            }
            else if (bonusReparifors == buttonBonus44)
            {
                score = buttonScore4;
            }                
            else
                throw new Exception("Only four teams are supported in one game.");

            // Handling if the scoreButton is enabled when casting Reparifors.
            if (state != States.AnsweringNormal)
            {
                MessageBox.Show("Reparifors nelze vykouzlit ve fázi, kdy se neodpovídá na neprémiovou otázku.", "Neodpovídá se na neprémiovou otázku", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (score.Enabled)
            {
                MessageBox.Show("Reparifors nelze vykouzlit, protože " + activeBonusHouse + " může odpovídat na otázku i bez něho.", "Nelze se probudit z omráčení bez omráčení", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }                
            else
            {
                MessageBox.Show(activeBonusHouse + " vykouzlili Reparifors!", "Reparifors", MessageBoxButtons.OK, MessageBoxIcon.Information);
                score.Enabled = true;
                return true;
            }                
        }
        private bool AnsweringAtTheMoment(Button clickedQuestion)
        {
            if (selectedQuestion == clickedQuestion)
                return true;

            if (state == States.AnsweringNormal || state == States.AnsweringPremiumNoStake || state == States.AnsweringPremiumStake || state == States.LuckyBrick)
            {
                MessageBox.Show("Nejprve zodpovězte vybranou otázku.", "Nezodpovězená vybraná otázka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }

            return false;
        }
        private void ShowMessageBoxPremiumLocked()
        {
            MessageBox.Show("Tato prémiová otázka je stále zamčená.\nPro její odemčení vyberte nejprve všechny ostatní otázky z tohoto okruhu.", "Prémiová otázka zamčena", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void QuestionSelected(Button question, int matrixRow, int matrixColumn)
        {
            if (AnsweringAtTheMoment(question))
                return;

            // Handling casting of Lumos.
            if (activeBonus == Bonuses.Lumos)
            {
                int.TryParse(question.Text, out int points);
                string questionSpec = matrixRow == 6 ? "prémiové otázky" : $"otázky za { points.ToString() } bodů";
                string[,] matrix;
                if (round == 1)
                {
                    matrix = Program.questions1;
                }
                else if (round == 2)
                {
                    matrix = Program.questions2;
                }
                else
                {
                    throw new Exception("Only two rounds per game are supported.");
                }
                string area = matrix[0, matrixColumn];

                MessageBox.Show($"{ activeBonusHouse } vykouzlili Lumos! Tento tým nyní zná znění {questionSpec} z kategorie {area}.\nPoznámka: Znění vybrané otázky jim sdělí moderátor.\nPozor: U prémiové otázky se možnosti nesdělují.", "Lumos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                activeBonus = null;
                activeBonusHouse = null;
                return;
            }
            
            // Is premium question and enabled?
            if (matrixRow == 6)
            {
                bool[] enabled = new bool[6] { enabledPremium1, enabledPremium2, enabledPremium3, enabledPremium4, enabledPremium5, enabledPremium6 };
                if (!enabled[matrixColumn])
                {
                    ShowMessageBoxPremiumLocked();
                    return;
                }
            }

            teamThatSelectedLastQuestion = activeTeam;
            selectedQuestion = question;

            if (IsLuckyBrick(matrixRow - 1, matrixColumn))
            {
                ChangeToLuckyBrickState(ParseScoreOrValue(question), matrixColumn);
            }
            else if (matrixRow == 6)
            {
                if (round == 1)
                    ChangeToPremiumAnsweringNoStakeState(matrixColumn);
                else if (round == 2)
                {
                    int activeTeamScore = ParseScoreOrValue(FindActiveTeamScore());
                    if (activeTeamScore < 1000)
                    {
                        MessageBox.Show($"Abyste měli co vsadit, dostáváte od nás {1000 - activeTeamScore} bodů navíc. Gratulujeme!", "Dárek na vsazení", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ChangeScoreOfActiveTeam(1000 - activeTeamScore);
                    }

                    ChangeToPremiumAnsweringWithStakeState(matrixColumn);
                }                    
                else
                    throw new Exception("Only two rounds per game are implemented.");
            }
            else
            {
                ChangeToNormalAnsweringState(matrixRow, matrixColumn);
            }

            HighlightQuestion(question);
        }
        private void ScoreButtonClicked(Button score, string house)
        {
            if (activeBonus == Bonuses.PetrificusTotalus)
            {
                if (house == activeBonusHouse)
                {
                    MessageBox.Show("Nemůžete omráčit sami sebe.", "Petrificus Totalus na sebe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                (score).Enabled = false;
                NoActiveTeam();
                MessageBox.Show(activeBonusHouse + " vykouzlili Petrificus Totalus na tým " + FindTeamName(score) + "!", "Petrificus Totalus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                activeBonus = null;
                activeBonusHouse = null;
                if (AllAnsweredWrong())
                {
                    buttonGameTime.Text = nextQuestion;
                    Refresh();
                    System.Threading.Thread.Sleep(3000);
                    ChangeActiveTeamToTeamThatLastSelected();
                    ChangeToChoosingState();
                }
                else if (!timerQuestion.Enabled)
                {
                    timerQuestion.Enabled = true;
                }
            }
            else if (activeTeam == ActiveTeam.None)
            {
                timerQuestion.Enabled = false;
                ChangeActiveTeam(score);
            }
            else if (FindActiveTeamScore() != score)
            {
                string action, rest;
                bool askForConfirmation = true;
                if (state == States.AnsweringNormal || state == States.AnsweringPremiumNoStake || state == States.AnsweringPremiumStake)
                {
                    action = "odpovídá na otázku";
                    rest = " bez toho, aniž by na otázku odpověděl";
                }
                else if (state == States.LuckyBrick)
                {
                    action = "získá cihličku";
                    rest = "";
                }
                else if (state == States.Choosing)
                {
                    action = "vybírá otázku";
                    rest = " před tím, než otázku vybere";
                }
                else if (state == States.EndOfRound || state == States.End)
                {
                    askForConfirmation = false;
                    action = "";
                    rest = "";
                    string buttonText;
                    if (state == States.EndOfRound)
                    {
                        buttonText = nextRound;
                    }
                    else if (state == States.End)
                    {
                        buttonText = results;
                    }
                    else
                        throw new Exception("This branch shouldn't ever be accessable.");

                    MessageBox.Show($"Změna týmu je k ničemu. Pro pokračování zmáčkněte velké tlačítko s nápisem \"{buttonText}\".", "Zbytečná akce", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("This branch shouldn't be accessable during the state " + state.ToString() + ".");
                }

                if (askForConfirmation)
                {
                    DialogResult answer = MessageBox.Show("Opravdu chcete změnit tým, který " + action + rest + "?", "Že by překlep?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (answer == DialogResult.Yes)
                        ChangeActiveTeam(score);
                }
            }
            // else ... does nothing as intended
        }
        private void TeamBonusClicked(Button bonusButton, string house)
        {
            if (state == States.EndOfRound || state == States.End)
            {
                string buttonText;
                if (state == States.EndOfRound)
                {
                    buttonText = nextRound;
                }
                else if (state == States.End)
                {
                    buttonText = results;
                }
                else
                    throw new Exception("This branch shouldn't ever be accessable.");

                MessageBox.Show($"Bonus v této fázi využít nelze. Pro pokračování zmáčkněte velké tlačítko s nápisem \"{buttonText}\".", "Zbytečná akce", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (activeBonus != null)
            {
                string bonusName;
                // Handling the space in PT's name:
                if (activeBonus == Bonuses.PetrificusTotalus)
                {
                    bonusName = "Petrificus Totalus";
                }
                else
                {
                    bonusName = activeBonus.ToString();
                }
                MessageBox.Show($"{activeBonusHouse} právě kouzlí {bonusName}. Počkejte, až kouzlo dokončí, a teprve poté můžete začít další kouzlo.", "Ještě se čaruje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation:
            DialogResult answer = MessageBox.Show("Opravdu chcete vykouzlit toto kouzlo?", "Potvrzení", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (answer != DialogResult.Yes)
                return;

            activeBonus = GetCorrespondingBonus(bonusButton.Image);
            activeBonusHouse = house;

            // Handling casting PT when...
            if (activeBonus == Bonuses.PetrificusTotalus)
            {
                //  ... no normal question is being answered.
                if (state != States.AnsweringNormal)
                {
                    activeBonus = null;
                    activeBonusHouse = null;
                    MessageBox.Show("Petrificus Totalus nemůžete vykouzlit, když se neodpovídá na neprémiovou otázku.", "Prostě ne", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // ...  only the casting team is able to answer the question.
                Button scoreOfCaster = GetHouseScore(activeBonusHouse);
                bool allOut = true;
                for (int i = 0; i < scores.Count; i++)
                {
                    if (scores[i] != scoreOfCaster && scores[i].Enabled)
                    {
                        allOut = false;
                        break;
                    }
                }

                if (allOut)
                {
                    activeBonus = null;
                    activeBonusHouse = null;
                    MessageBox.Show("Petrificus Totalus nemůžete vykouzlit, když jste jediní, kdo může odpovídat na otázku.", "Proč omráčit sami sebe?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            // Handling casting Lumos when no question is being chosen.
            else if (activeBonus == Bonuses.Lumos && state != States.Choosing)
            {
                activeBonus = null;
                activeBonusHouse = null;
                MessageBox.Show("Lumos nelze vykouzlit ve fázi, kdy se nevybírá otázka.", "Kdo si počká, ten se dočká", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Handling casting Reparifors immediately – more inside the method.
            else if (activeBonus == Bonuses.Reparifors)
            {
                if (!HandleReparifors(bonusButton))
                {
                    activeBonus = null;
                    activeBonusHouse = null;
                    return;
                }
            }
            
            // Handling casting Protego when no premium answer with stake is being answered
            else if (activeBonus == Bonuses.Protego && state != States.AnsweringPremiumStake)
            {
                activeBonus = null;
                activeBonusHouse = null;
                MessageBox.Show("Protego nelze vykouzlit ve fázi, kdy se neodpovídá na prémiovou otázku, kde je možné sázet.\nPoznámka: Nelze ho tedy vyčarovat v prvním kole.", "K čemu pojištění bez sázek?", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Checking if more charges of the casted spell are available and removing one charge.
            int index = scores.IndexOf(GetHouseScore(house));
            int[] array;
            switch (index)
            {
                case 0:
                    array = bonuses1;
                    break;
                case 1:
                    array = bonuses2;
                    break;
                case 2:
                    array = bonuses3;
                    break;
                case 3:
                    array = bonuses4;
                    break;
                default:
                    throw new Exception("Unhandled score button.");
            }

            array[(int)activeBonus]--;
            bonusButton.Image = matrixOfBonusImages[(int)activeBonus, array[(int)activeBonus]];
            if (array[(int)activeBonus] == 0)            
                bonusButton.Enabled = false;    // No charges left.            
                

            // Reparifors has already been used (right after cliking on the bonus button).
            if (activeBonus == Bonuses.Reparifors)
            {
                activeBonus = null;
                activeBonusHouse = null;
            }
        }
        //
        // End of Methods.
        //
        #endregion
        #region Events
        //
        // Events:
        //
        private void buttonGameTime_Click(object sender, EventArgs e)
        {
            if (state == States.Choosing)
            {
                MessageBox.Show("Časovač se spustí ihned po kliknutí na otázku, kterou vybere tým, který je na tahu.", "Vyberte otázku", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (buttonGameTime.Text == nextQuestion)
            {
                ChangeActiveTeamToTeamThatLastSelected();
                ChangeToChoosingState();
            }
            else
            {
                if (timerQuestion.Enabled)
                    timerQuestion.Enabled = false;
                else
                {
                    DialogResult answer = MessageBox.Show("Opravdu chcete spustit časomíru manuálně?", "Spouštění času ručně", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        NoActiveTeam();
                        timerQuestion.Enabled = true;
                        // Only pressing Correct / Incorrect / Premium Answer buttons should enable the timer again.
                        // NOTE: This option is added for emergency missclicks. Can break the application if intended by the user.
                    }
                }
            }
        }
        private void timerQuestion_Tick(object sender, EventArgs e)
        {
            time--;
            if (time == 0)
            {
                timerQuestion.Enabled = false;
                buttonGameTime.Text = nextQuestion;
                Refresh();
                System.Threading.Thread.Sleep(3000);
                ChangeActiveTeamToTeamThatLastSelected();
                if (activeBonus == Bonuses.PetrificusTotalus)
                {
                    MessageBox.Show($"{activeBonusHouse} nestihl dokončit kouzelení Petrificus Totalus. Kouzlo zmizelo do ztracena.", "Nepovedený Petrificus Totalus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    activeBonus = null;
                    activeBonusHouse = null;
                }                
                ChangeToChoosingState();
            }
            else
                buttonGameTime.Text = time.ToString();
        }
        private void buttonCorrect_Click(object sender, EventArgs e)
        {
            if (activeTeam == ActiveTeam.None)
            {
                MessageBox.Show("Nejprve vyberte tým, který na otázku odpovídá.", "Není vybrán tým", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (activeBonus == Bonuses.PetrificusTotalus)
            {
                MessageBox.Show("Někdo kouzlí Petrificus Totalus. Vyberte tým, který bude omráčen.", "Není vybrán tým k omráčení", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            teamThatSelectedLastQuestion = activeTeam;
            int value = ParseScoreOrValue(selectedQuestion);
            ChangeScoreOfActiveTeam(value);
            ChangeActiveTeamToTeamThatLastSelected();   // We've already changed the team that last selected.
            ChangeToChoosingState();
        }
        private void buttonIncorrect_Click(object sender, EventArgs e)
        {
            if (activeTeam == ActiveTeam.None)
            {
                MessageBox.Show("Nejprve vyberte tým, který na otázku odpovídá.", "Není vybrán tým", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (activeBonus == Bonuses.PetrificusTotalus)
            {
                MessageBox.Show("Někdo kouzlí Petrificus Totalus. Vyberte tým, který bude omráčen.", "Není vybrán tým k omráčení", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            Button activeTeamScore = FindActiveTeamScore();
            RemoveHighlighting(activeTeamScore);
            activeTeamScore.Enabled = false;

            if (AllAnsweredWrong())
            {
                buttonGameTime.Text = nextQuestion;
                Refresh();
                System.Threading.Thread.Sleep(3000);
                ChangeActiveTeamToTeamThatLastSelected();
                ChangeToChoosingState();
            }
            else
            {
                NoActiveTeam();
                timerQuestion.Enabled = true;
            }
        }
        private void buttonAnswerPremium_Click(object sender, EventArgs e)
        {
            bool addBonus = false;
            bool delay = true;

            if (state == States.LuckyBrick)
            {
                delay = false;
                ChangeScoreOfActiveTeam(ParseScoreOrValue(selectedQuestion));
            }
            else if (state == States.AnsweringPremiumNoStake)
            {
                PremiumAnswers actual = GetSelectedPremiumAnswer();
                if (actual == PremiumAnswers.None)
                {
                    MessageBox.Show("Nejprve vyberte odpověď.", "Žádná odpověď", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (buttonBonus1.Enabled && buttonBonus2.Enabled)
                {
                    MessageBox.Show("Nejprve si zvolte bonus.", "Nevybraná odměna", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                
                if (!buttonBonus1.Enabled && !buttonBonus2.Enabled)
                {
                    throw new Exception("This branch should not be reachable.\nBoth bonus buttons are disabled, altough the active team needs to choose one of them.");
                }


                RadioButton actualButton = GetCorrespondingRadioButton(actual);
                if (actual == correct)
                {
                    HighlightAnswerAsCorrect(actualButton);
                    addBonus = true;
                }
                else
                {
                    RadioButton correctButton = GetCorrespondingRadioButton(correct);
                    HighlightAnswerAsIncorrect(actualButton);
                    HighlightAnswerAsCorrect(correctButton);
                    DisablePremiumBonusButtons();
                }
            }
            else if (state == States.AnsweringPremiumStake)
            {
                PremiumAnswers actual = GetSelectedPremiumAnswer();
                int stake;

                if (actual == PremiumAnswers.None)
                {
                    MessageBox.Show("Nejprve vyberte odpověď.", "Žádná odpověď", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (textBoxStake.Text == "" || textBoxStake.Text == null)
                {
                    MessageBox.Show("Vsaďte před tím, než odpovíte.", "Nevyzadaná sázka", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (!int.TryParse(textBoxStake.Text, out stake))
                {
                    MessageBox.Show("Vsazený počet bodů musí být celé číslo.", "Sázka není celé číslo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (stake <= 0)
                {
                    MessageBox.Show("Vsazený počet bodů musí být přirozené číslo.", "Sázka není přirozené číslo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (stake % 1000 != 0)
                {
                    MessageBox.Show("Vsazený počet bodů musí být násobkem čísla 1000.", "Sázka nekončí na \"000\"", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int scoreOfActiveTeam = ParseScoreOrValue(FindActiveTeamScore());                
                if (stake > scoreOfActiveTeam)
                {
                    MessageBox.Show("Můžete vsadit nejvýše tolik, kolik máte.", "Nelze sázet, co není vaše", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                RadioButton actualButton = GetCorrespondingRadioButton(actual);
                if (actual == correct)
                {
                    HighlightAnswerAsCorrect(actualButton);
                    ChangeScoreOfActiveTeam(stake);
                    if (activeBonus == Bonuses.Protego)
                    {
                        MessageBox.Show(activeBonusHouse + " vykouzlili Protego! Naštěstí zbytečně.", "Protego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        activeBonus = null;
                        activeBonusHouse = null;
                        delay = false;
                    }
                }
                else
                {
                    RadioButton correctButton = GetCorrespondingRadioButton(correct);
                    HighlightAnswerAsIncorrect(actualButton);
                    HighlightAnswerAsCorrect(correctButton);

                    int toLose = stake / (100 / percentageOfStakeLost);
                    if (activeBonus == Bonuses.Protego)
                    {
                        int saved = toLose - toLose / 2;
                        ChangeScoreOfActiveTeam((-1) * (toLose / 2));
                        MessageBox.Show(activeBonusHouse + " vykouzlili Protego! Zachránilo jim celkem " + saved.ToString() + " bodů.", "Protego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        activeBonus = null;
                        activeBonusHouse = null;
                        delay = false;
                    }
                    else
                    {
                        ChangeScoreOfActiveTeam((-1) * toLose);
                    }
                }
            }
            else
            {
                throw new Exception("This button shouldn't be clickable during the state " + state.ToString() + " .");
            }

            Refresh();
            if (delay || addBonus)
            {
                System.Threading.Thread.Sleep(3000);
                if (addBonus)
                {
                    AddSelectedBonusToActiveTeam(); // Adding bonus after the delay.
                }
            }                

            if (state != States.LuckyBrick && IsLastQuestion())
                ChangeToEndOfRoundState();
            else
                ChangeToChoosingState();
        }
        private void buttonNextRound_Click(object sender, EventArgs e)
        {
            if (state == States.EndOfRound)
            {
                round++;
                ChangeToStartingState();
            }
            else if (state == States.End)
            {
                GoToFormEnd(Program.formEnd);
            }            
            else
            {
                throw new Exception("This button shouldn't be clickable during the state " + state.ToString() + " .");
            }
        }
        private void buttonBonus1_Click(object sender, EventArgs e)
        {
            if (!bonusChosen)
            {
                DialogResult answer = MessageBox.Show("Opravdu si chcete vybrat toto kouzlo?", "Potvrzení", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    buttonBonus2.Enabled = false;
                    bonusChosen = true;
                }                    
            }            
            else
            {
                MessageBox.Show("Tento bonus je již vybrán.", "Už není co vybírat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonBonus2_Click(object sender, EventArgs e)
        {
            if (!bonusChosen)
            {
                DialogResult answer = MessageBox.Show("Opravdu si chcete vybrat toto kouzlo?", "Potvrzení", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    buttonBonus1.Enabled = false;
                    bonusChosen = true;
                }                    
            }
            else
            {
                MessageBox.Show("Tento bonus je již vybrán.", "Už není co vybírat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonScore1_Click(object sender, EventArgs e)
        {
            ScoreButtonClicked((Button)sender, house1);
        }
        private void buttonScore2_Click(object sender, EventArgs e)
        {
            ScoreButtonClicked((Button)sender, house2);
        }
        private void buttonScore3_Click(object sender, EventArgs e)
        {
            ScoreButtonClicked((Button)sender, house3);
        }
        private void buttonScore4_Click(object sender, EventArgs e)
        {
            ScoreButtonClicked((Button)sender, house4);
        }
        #region TeamBonusEvents
        private void buttonBonus11_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house1);
        }
        private void buttonBonus12_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house1);
        }
        private void buttonBonus13_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house1);
        }
        private void buttonBonus14_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house1);
        }
        private void buttonBonus21_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house2);
        }
        private void buttonBonus22_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house2);
        }
        private void buttonBonus23_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house2);
        }
        private void buttonBonus24_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house2);
        }
        private void buttonBonus31_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house3);
        }
        private void buttonBonus32_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house3);
        }
        private void buttonBonus33_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house3);
        }
        private void buttonBonus34_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house3);
        }
        private void buttonBonus41_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house4);
        }
        private void buttonBonus42_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house4);
        }
        private void buttonBonus43_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house4);
        }
        private void buttonBonus44_Click(object sender, EventArgs e)
        {
            TeamBonusClicked((Button)sender, house4);
        }
        #endregion
        #region QuestionSelectectionEvents
        private void buttonQuestion11_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 1, 0);
        }
        private void buttonQuestion12_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 2, 0);
        }
        private void buttonQuestion13_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 3, 0);
        }
        private void buttonQuestion14_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 4, 0);
        }
        private void buttonQuestion15_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 5, 0);
        }
        private void buttonPremiumQuestion1_Click(object sender, EventArgs e)
        {            
            QuestionSelected((Button)sender, 6, 0);
        }
        private void buttonQuestion21_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 1, 1);
        }
        private void buttonQuestion22_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 2, 1);
        }
        private void buttonQuestion23_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 3, 1);
        }
        private void buttonQuestion24_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 4, 1);
        }
        private void buttonQuestion25_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 5, 1);
        }
        private void buttonPremiumQuestion2_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 6, 1);
        }
        private void buttonQuestion31_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 1, 2);           
        }
        private void buttonQuestion32_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 2, 2);            
        }
        private void buttonQuestion33_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 3, 2);
        }
        private void buttonQuestion34_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 4, 2);
        }
        private void buttonQuestion35_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 5, 2);
        }    
        private void buttonPremiumQuestion3_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 6, 2);
        }
        private void buttonQuestion41_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 1, 3);
        }
        private void buttonQuestion42_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 2, 3);
        }
        private void buttonQuestion43_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 3, 3);
        }
        private void buttonQuestion44_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 4, 3);
        }
        private void buttonQuestion45_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 5, 3);
        }
        private void buttonPremiumQuestion4_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 6, 3);
        }
        private void buttonQuestion51_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 1, 4);
        }
        private void buttonQuestion52_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 2, 4);
        }
        private void buttonQuestion53_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 3, 4);
        }
        private void buttonQuestion54_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 4, 4);
        }
        private void buttonQuestion55_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 5, 4);
        }
        private void buttonPremiumQuestion5_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 6, 4);
        }
        private void buttonQuestion61_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 1, 5);
        }
        private void buttonQuestion62_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 2, 5);
        }
        private void buttonQuestion63_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 3, 5);
        }
        private void buttonQuestion64_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 4, 5);
        }
        private void buttonQuestion65_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 5, 5);
        }
        private void buttonPremiumQuestion6_Click(object sender, EventArgs e)
        {
            QuestionSelected((Button)sender, 6, 5);
        }
        #endregion
        //
        // End of Events.
        //
        #endregion
    }
}