using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerátorPříkladůKonzole
{
    class ModeSelector
    {
        const string mode1Desc = "malá násobilka – násobení";
        const string mode2Desc = "malá násobilka – dělení";
        const string mode3Desc = "malá násobilka – mix";
        const string mode4Desc = "velká násobilka – násobení";
        const string mode5Desc = "velká násobilka – dělení";
        const string mode6Desc = "velká násobilka – mix";
        const string mode7Desc = "multiplikativní super mix (malá + velká násobilka, dělení + násobení)";
        const string mode8Desc = "multiplikativní rozcvička";
        const string mode9Desc = "malé sčítání";
        const string mode10Desc = "malé odčítání";
        const string mode11Desc = "sčítání a odčítání (mix) – malé";
        const string mode12Desc = "velké sčítání";
        const string mode13Desc = "velké odčítání";
        const string mode14Desc = "sčítání a odčítání (mix) – velké";
        const string mode15Desc = "aditivní super mix (malé + velké sčítání a odčítání)";

        readonly static string[] modeDescriptions = {
            mode1Desc,
            mode2Desc,
            mode3Desc,
            mode4Desc,
            mode5Desc,
            mode6Desc,
            mode7Desc,
            mode8Desc,
            mode9Desc,
            mode10Desc,
            mode11Desc,
            mode12Desc,
            mode13Desc,
            mode14Desc,
            mode15Desc
        };

        public static int SelectMode()
        {
            Console.WriteLine("Zvolte si mód:");
            for (int i = 1; i <= modeDescriptions.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i, modeDescriptions[i - 1]);
            }
            Console.WriteLine("-----------------------------------------------");
            Console.Write("Vaše volba: ");
            int chosenMode;
            while (!int.TryParse(Console.ReadLine(), out chosenMode) || chosenMode < 1 || chosenMode > modeDescriptions.Length)
            {
                Console.WriteLine("Neplatná hodnota! Hodnota musí být celé číslo od 1 do {0}, poté zmáčkněte ENTER.", modeDescriptions.Length);
                Console.WriteLine("Zkuste to znovu.");
                Console.WriteLine();
                Console.Write("Vaše volba: ");
            }

            Console.WriteLine("Mód zvolen!");
            Console.WriteLine();

            return chosenMode;
        }

        public static int ChooseNumberOfProblems()
        {
            Console.WriteLine("Kolik příkladů z vybraného módu si přejete?");
            Console.WriteLine("Poznámka: Pokud zvolíte víc než v módu je, automaticky se vygenerují všechny.");
            Console.WriteLine("Poznámka: Pokud chcete vygenerovat všechny příklady, pouze zmáčkněte ENTER.");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.Write("Počet příkladů: ");
            int numberOfProblems = int.MaxValue;
            string input;
            const string all = "";
            while ((input = Console.ReadLine()) == input)
            {
                if (input == all || input == null)
                {
                    break;
                }

                if (int.TryParse(input, out numberOfProblems) && numberOfProblems >= 1 && numberOfProblems <= int.MaxValue)
                {
                    break;
                }

                Console.WriteLine("Neplatná hodnota! Hodnota musí být přirozené číslo od 1 do {0}, poté zmáčkněte ENTER.", int.MaxValue);
                Console.WriteLine("Zkuste to znovu.");
                Console.WriteLine();
                Console.WriteLine("Poznámka: Pokud chcete vygenerovat všechny příklady, pouze zmáčkněte ENTER.");
                Console.Write("Počet příkladů: ");
            }
            /*
            while ((!int.TryParse((input = Console.ReadLine()), out numberOfProblems) && input != all && input != null) || numberOfProblems < 1 || numberOfProblems > int.MaxValue)
            {
                Console.WriteLine("Neplatná hodnota! Hodnota musí být přirozené číslo od 1 do {0}, poté zmáčkněte ENTER.", int.MaxValue);
                Console.WriteLine("Zkuste to znovu.");
                Console.WriteLine();
                Console.WriteLine("Poznámka: Pokud chcete vygenerovat všechny příklady, pouze zmáčkněte ENTER.");
                Console.Write("Počet příkladů: ");
            }*/           

            switch(numberOfProblems)
            {
                case 1:
                    Console.WriteLine("Bude vygenerován 1 příklad.");
                    break;
                case 2:
                case 3:
                case 4:
                    Console.WriteLine("Budou vygenerovány {0} příklady.", numberOfProblems);    // It's sensible to assume each mode has at least 4 different problems.
                    break;
                case int.MaxValue:
                    Console.WriteLine("Budou vygenerovány všechny příklady vybraného módu.");
                    break;
                default:
                    Console.WriteLine("Bude vygenerováno {0} (maximálně) příkladů.", numberOfProblems);
                    break;
            }            
            Console.WriteLine();

            return numberOfProblems;
        }

        public static string GetModeDescritpion(int modeNum)
        {
            return modeDescriptions[modeNum - 1];
        }
    }

    abstract class MathOperation
    {
        public abstract int Evaluate();
        public abstract override string ToString();
    }
    abstract class UnaryOperation : MathOperation
    {
        public int Operand { get; protected set; }
    }
    abstract class BinaryOperation : MathOperation
    {
        public int OperandLeft { get; protected set; }
        public int OperandRight { get; protected set; }
    }

    class Addition : BinaryOperation
    {
        public Addition(int op1, int op2)
        {
            this.OperandLeft = op1;
            this.OperandRight = op2;
        }

        public override int Evaluate()
        {
            return OperandLeft + OperandRight;
        }

        public override string ToString()
        {
            return string.Format("{0} + {1}", OperandLeft, OperandRight);
        }
    }
    class Subtraction : BinaryOperation
    {
        public Subtraction(int op1, int op2)
        {
            this.OperandLeft = op1;
            this.OperandRight = op2;
        }

        public override int Evaluate()
        {
            return OperandLeft - OperandRight;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", OperandLeft, OperandRight);
        }
    }
    class Multiplication : BinaryOperation
    {
        public Multiplication(int op1, int op2)
        {
            this.OperandLeft = op1;
            this.OperandRight = op2;
        }

        public override int Evaluate()
        {
            return OperandLeft * OperandRight;
        }

        public override string ToString()
        {
            return string.Format("{0} * {1}", OperandLeft, OperandRight);
        }
    }
    class Division : BinaryOperation
    {
        public Division(int op1, int op2)
        {
            this.OperandLeft = op1;
            this.OperandRight = op2;
        }

        public override int Evaluate()
        {
            return (int)((double)OperandLeft / OperandRight);
        }

        public override string ToString()
        {
            return string.Format("{0} / {1}", OperandLeft, OperandRight);
        }
    }
    class SquaringInt : UnaryOperation
    {
        public SquaringInt(int op)
        {
            this.Operand = op;         
        }

        public override int Evaluate()
        {
            return Operand * Operand;
        }

        public override string ToString()
        {
            return string.Format("{0}^2", Operand);
        }
    }
    class SquareRootingInt : UnaryOperation
    {
        public SquareRootingInt(int op)
        {
            this.Operand = op;
        }

        public override int Evaluate()
        {
            return (int)Math.Sqrt(Operand);
        }

        public override string ToString()
        {
            return string.Format("odmocnina({0})", Operand);
        }
    }


    class Generator
    {
        private int Mode { get; }
        private string ModeDescription { get; }
        private int MaxProblems { get; }
        private bool EnoughProblems { get; set; }
        public int Score { get; private set; }
        Stack<MathOperation> Stack = new Stack<MathOperation>();
        Random random = new Random();

        const int scoreIncrement = 100;
        const char separator = ' ';

        public Generator(int modeNum, string modeDesc, int numberOfProblems, int score)
        {
            this.Mode = modeNum;
            this.ModeDescription = modeDesc;
            this.MaxProblems = numberOfProblems;
            this.Score = score;

            FillStack();
        }

        private MathOperation GetProblem()
        {
            if (Stack.Count == 0)
            {
                return null;
            }
            else
            {
                return Stack.Pop();
            }            
        }

        private List<MathOperation> GenerateList()
        {
            List<MathOperation> list = new List<MathOperation>();

            int from = 0;
            int to = 0;
            bool optAdd = false;
            bool optSub = false;
            bool optMult = false;
            bool optDiv = false;
            bool optSqr = false;
            bool optSqrt = false;

            switch(Mode)
            {
                case 1:
                    from = 0;
                    to = 10;
                    optMult = true;
                    optSqr = true;
                    break;
                case 2:
                    from = 0;
                    to = 10;
                    optDiv = true;
                    optSqrt = true;
                    break;
                case 3:
                    from = 0;
                    to = 10;
                    optMult = true;
                    optDiv = true;
                    optSqr = true;
                    optSqrt = true;
                    break;
                case 4:
                    from = 10;
                    to = 20;
                    optMult = true;
                    optSqr = true;
                    break;
                case 5:
                    from = 10;
                    to = 20;
                    optDiv = true;
                    optSqrt = true;
                    break;
                case 6:
                    from = 10;
                    to = 20;
                    optMult = true;
                    optDiv = true;
                    optSqr = true;
                    optSqrt = true;
                    break;
                case 7:
                    from = 0;
                    to = 20;
                    optMult = true;
                    optDiv = true;
                    optSqr = true;
                    optSqrt = true;
                    break;
                case 8:
                    from = 2;
                    to = 10;
                    optMult = true;
                    optSqr = true;
                    break;
                case 9:
                    from = 0;
                    to = 10;
                    optAdd = true;
                    break;
                case 10:
                    from = 0;
                    to = 10;
                    optSub = true;
                    break;
                case 11:
                    from = 0;
                    to = 10;
                    optAdd = true;
                    optSub = true;
                    break;
                case 12:
                    from = 10;
                    to = 100;
                    optAdd = true;
                    break;
                case 13:
                    from = 10;
                    to = 100;
                    optSub = true;
                    break;
                case 14:
                    from = 10;
                    to = 100;
                    optAdd = true;
                    optSub = true;
                    break;
                case 15:
                    from = 0;
                    to = 100;
                    optAdd = true;
                    optSub = true;
                    break;
                default:
                    throw new Exception("Uknown mode -> cannot determine mode options to generate the problem list.");
            }

            if (optAdd)
            {
                for (int i = from; i <= to; i++)
                {
                    for (int j = from; j <= to; j++)
                    {
                        list.Add(new Addition(i, j));
                    }                    
                }
            }

            if (optSub)
            {
                for (int i = from; i <= to; i++)
                {
                    for (int j = from; j <= to; j++)
                    {
                        list.Add(new Subtraction(i, j));    // Change? (Leave only positive results?)
                    }
                }
            }

            if (optMult)
            {
                for (int i = from; i <= to; i++)
                {
                    for (int j = from; j <= to; j++)
                    {
                        list.Add(new Multiplication(i, j));
                    }
                }
            }

            if (optDiv)
            {
                for (int i = from + 1; i <= to; i++)
                {
                    for (int j = from; j <= to; j++)
                    {
                        list.Add(new Division(i * j, i));
                    }
                }
            }

            if (optSqr)
            {
                for (int i = from; i <= to; i++)
                {
                    list.Add(new SquaringInt(i));
                }
            }

            if (optSqrt)
            {
                for (int i = from; i <= to; i++)
                {
                    list.Add(new SquareRootingInt(i * i));
                }
            }

            return list;
        }

        private void FillStack()
        {
            List<MathOperation> list = GenerateList();
            int index;

            while (list.Count > 0 && Stack.Count < this.MaxProblems)
            {
                index = this.random.Next(0, list.Count);
                Stack.Push(list[index]);
                list.RemoveAt(index);
            }                

            if (Stack.Count == this.MaxProblems)
            {
                this.EnoughProblems = true;
            }
            else
            {
                this.EnoughProblems = false;
            }
        }

        /// <summary>
        /// Prints the current score.
        /// </summary>
        /// <param name="final">Is it the final score?</param>
        private void TellScore(bool final)
        {
            string text;
            if (final)
            {
                text = "Celkové skóre: ";
            }
            else
            {
                text = "Skóre: ";
            }

            Console.WriteLine(text + this.Score);
        }

        private int Calculate(MathOperation problem)
        { 
            return problem.Evaluate();
        }

        public void Evaluate(MathOperation problem, int answer)
        {
            int correct = Calculate(problem);
            if (correct == answer)
            {
                this.Score += scoreIncrement;
                Console.WriteLine("Správně.");                
            }
            else
            {
                Console.WriteLine("CHYBA. Správná odpověď: " + correct);
            }

            TellScore(false);
            Console.WriteLine();
        }

        /// <summary>
        /// Runs the main interactive code of the application.
        /// </summary>
        /// <returns>Do we want to continue with selecting another mode?</returns>
        public bool Run()
        {
            int problemNum = 1;
            MathOperation problem;
            string problemString;
            while ((problem = GetProblem()) != null)
            {
                problemString = problem.ToString();
                Console.WriteLine("Příklad {0}: {1} = ?", problemNum, problemString);
                Console.Write("Vaše odpověď: ");
                int answerResult;
                while (!int.TryParse(Console.ReadLine(), out answerResult))
                {
                    Console.WriteLine("Neplatná hodnota! Výsledek musí být celé číslo od {0} do {1}; poté zmáčkněte ENTER.", int.MinValue, int.MaxValue);
                    Console.WriteLine();
                    Console.WriteLine("Příklad {0}: {1} = ?", problemNum, problemString);
                    Console.Write("Vaše odpověď: ");
                }
                Evaluate(problem, answerResult);

                problemNum++;
                Console.WriteLine();
            }

            // Console.WriteLine($"Všechny {(this.EnoughProblems ? "vygenerované" : "")} příklady tohoto módu byly vyčerpány.");
            if (this.EnoughProblems)
            {
                Console.WriteLine("Všechny vygenerované příklady byly vyčerpány.");
            }
            else
            {
                Console.WriteLine("Všechny příklady tohoto módu byly vyčerpány.");
            }           
            Console.WriteLine("Chcete vygenerovat další příklady (lze si vybrat jiný mód)? (A - ano, N - ne)");
            Console.Write("Vaše odpověď: ");
            char answerContinue;
            while (!char.TryParse(Console.ReadLine(), out answerContinue) || (answerContinue != 'A' && answerContinue != 'a' && answerContinue != 'N' && answerContinue != 'n'))
            {
                Console.WriteLine("Neplatná odpověď. Zmáčkněte pouze písmenko \"A\" pro výběr nového módu, anebo písmenko \"N\", pokud pokračovat nechcete; poté zmáčkněte ENTER.");
                Console.WriteLine();
                Console.WriteLine("Chcete vygenerovat další příklady (lze si vybrat jiný mód)? (A - ano, N - ne)");
                Console.Write("Vaše odpověď: ");
            }
            if (answerContinue == 'A' || answerContinue == 'a')
            {
                Console.WriteLine();
                Console.WriteLine("Pokračujeme. :)");
                Console.WriteLine();

                return true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Děkujeme za pěkné počítání a těšíme se na příště. :)");
                TellScore(true);

                return false;
            }
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {
            int mode = ModeSelector.SelectMode();      // IO
            string modeDesc = ModeSelector.GetModeDescritpion(mode);       // no IO
            int problems = ModeSelector.ChooseNumberOfProblems();      // IO

            Generator generator = new Generator(mode, modeDesc, problems, 0);

            while (generator.Run())
            {
                mode = ModeSelector.SelectMode();   // IO
                modeDesc = ModeSelector.GetModeDescritpion(mode);   // no IO
                problems = ModeSelector.ChooseNumberOfProblems();   // IO

                generator = new Generator(mode, modeDesc, problems, generator.Score);
            }

            Console.WriteLine();
            Console.Write("Zmáčkněte libovolnou klávesu k ukončení programu... ");
            Console.ReadKey();
        }
    }
}
