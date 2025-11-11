using System.Text;

namespace SelfHealingCodeGenerator
{
    class Solver
    {
        /// <summary>
        /// Returns a Manhatton distance of numbers in rows row1 and row2, both ranging from 1 to n.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="numbers"></param>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static int GetDistance(int n, int[,] numbers, int row1, int row2)
        {
            int distance = 0;
            if (row1 < 1 || row1 > n || row2 < 1 || row2 > n)
                throw new ArgumentException("Čísla řad by měla být od 1 do n.");
            for (int j = 0; j < n; j++)
            {
                if (numbers[row1 - 1, j] != numbers[row2 - 1, j])
                    distance++;
            }

            return distance;
        }

        /// <summary>
        /// Returns the number of the row with the changed number, i.e. ranging from 1 to n.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="changedNumbers"></param>
        /// <returns></returns>
        static int GetRowOfChangedNumber(int n, int[,] changedNumbers)
        {
            int rowWithChange;
            int numberOfPointInEachLine = (int)Math.Ceiling(Math.Sqrt(n - 1));
            // Proof:
            // n = # lines = # points = o^2 + o + 1 = o(o + 1) + 1, where o is the order of the FPP
            // # points in each line = o + 1
            // => o^2 + o = (o + 0.5)^2 - 0.25 = n - 1
            // => o = sqrt(n - 0.75) - 0.5
            // => # points in each line = o + 1 = sqrt(n - 0.75) + 0.5
            // numberOfPointInEachLine = (int)(Math.Sqrt(n - 0.75) + 0.5);
            // Alternative:
            // o(o+1) = n-1 => n - 1 is a natural number less than o + 1 or equal to o + 1 and more than o
            // => sqrt(n-1) is a real number between o and o + 1
            // => ceiling(sqrt(n-1)) is o + 1
            // numberOfPointInEachLine = (int)Math.Ceiling(Math.Sqrt(n - 1));
            int expectedDistance = n - numberOfPointInEachLine;
            // Why though? Definitely works for o = 2 => n = 7 => points are 3 in each line => distance is 4.
            if (GetDistance(n, changedNumbers, 1, 2) != expectedDistance)
            {
                if (GetDistance(n, changedNumbers, 1, 3) != expectedDistance)
                {
                    rowWithChange = 1;
                }
                else
                    rowWithChange = 2;
            }
            else if (GetDistance(n, changedNumbers, 3, 4) != expectedDistance)
            {
                if (GetDistance(n, changedNumbers, 3, 5) != expectedDistance)
                {
                    rowWithChange = 3;
                }
                else
                    rowWithChange = 4;
            }
            else if (GetDistance(n, changedNumbers, 5, 6) != expectedDistance)
            {
                if (GetDistance(n, changedNumbers, 5, 7) != expectedDistance)
                {
                    rowWithChange = 5;
                }
                else
                    rowWithChange = 6;
            }
            else if (n == 7)
                rowWithChange = 7;
            else
                throw new NotImplementedException("Prozatím je systém připraven pouze na n = 7, tj. Fanovu rovinu řádu 2.");
            // Expansion is easy if dumb (take first, try everything – check only if 1, 2 don't have expectedDistance), harder if smart but possible.

            return rowWithChange;
        }

        static int GetCommonDigit(int n, int[,] changedNumbers, List<int> indecesOfNumbersToCompare)
        {
            for (int i = 0; i < n; i++)
            {
                if (changedNumbers[indecesOfNumbersToCompare[0], i] == changedNumbers[indecesOfNumbersToCompare[1], i]
                    && changedNumbers[indecesOfNumbersToCompare[0], i] == changedNumbers[indecesOfNumbersToCompare[2], i])
                {
                    return i + 1;
                }
            }

            // This should never happen since we work with FPP:
            return -1;
        }

        static int GetPositionOfChangedNumber(int n, int[,] changedNumbers, int rowWithChange)
        {
            int positionWithChange;
            int indexOfChangedNumber = rowWithChange - 1;
            int numberOfPointInEachLine = (int)Math.Ceiling(Math.Sqrt(n - 1));
            int expectedDistance = n - numberOfPointInEachLine;
            List<int> indecesOfLessDistance = new List<int>();
            List<int> indecesOfMoreDistance = new List<int>();

            int i = (indexOfChangedNumber + 1) % n; // starting at the row right below the changed number in our matrix, wrapping around (after last is first)
            while (Math.Max(indecesOfLessDistance.Count, indecesOfMoreDistance.Count) < numberOfPointInEachLine)
            {
                if ((GetDistance(n, changedNumbers, rowWithChange, i + 1) < expectedDistance))
                {
                    indecesOfLessDistance.Add(i);
                }
                else
                {
                    indecesOfMoreDistance.Add(i);
                }
                i = (i + 1) % n;
            }
            // Taking only the list with the sufficient number of lines:
            List<int> indecesOfNumbersWithOneCommonDigit = indecesOfLessDistance.Count > indecesOfMoreDistance.Count ? indecesOfLessDistance : indecesOfMoreDistance;
            positionWithChange = GetCommonDigit(n, changedNumbers, indecesOfNumbersWithOneCommonDigit);

            return positionWithChange;
        }

        /// <summary>
        /// Loads numbers from a file with specified path.
        /// Matrix should have n rows, each having an n-digit number consisting of 0s and 1s.
        /// There can be ads, i.e. 1), 2) etc., in front of the nubmers, as long as they are separated by a space ' '.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="pathIn"></param>
        /// <returns></returns>
        static int[,] LoadNumbers(int n, string pathIn)
        {
            // TODO: Loading from Console.
            // Only from files for now.
            int[,] numbers = new int[n,n];
            Console.WriteLine("Zadej matici čísel do souboru s cestou .\\{0} tak, aby každý řádek obsahovat {1}-ciferná čísla.", pathIn, n);
            Console.WriteLine("Zkontroluj, že na konci není žádný prázdný řádek. Soubor poté ulož (a můžeš ho vypnout).");
            Console.Write("Až bude vše připraveno, zmáčkni v konzoli libovolnou klávesu... ");
            Console.ReadKey();

            using (StreamReader sr = new StreamReader(pathIn))
            {
                String row;
                for (int i = 0; i < n; i++)
                {
                    row = sr.ReadLine()!;
                    if (row.Split(' ').Length == 2)
                        row = row.Split(' ')[1];    // disregarding the i) in front of each number: 1) 1011011 -> 10111011
                    for (int j = 0; j < n; j++)
                    {
                        numbers[i, j] = row[j] - '0';   // for '0' yields 0, for '1' yields 1
                    }
                }
            }

            return numbers;
        }

        /// <summary>
        /// Both changedRow and changedDigit are ranging from 1 to n.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="changedNumbers"></param>
        /// <param name="changedRow"></param>
        /// <param name="changedDigit"></param>
        public static void Solve(int n, string pathIn, out int changedRow, out int changedDigit)
        {
            int[,] changedNumbers = LoadNumbers(n, pathIn);
            changedRow = GetRowOfChangedNumber(n, changedNumbers);
            changedDigit = GetPositionOfChangedNumber(n, changedNumbers, changedRow);
        }
    }

    internal class Program
    {
        public static Random random = new Random();
        public enum outputOption { Console, File };

        public static int[,] flipMatrix7 =
        {
            {0, 0, 0, 0, 0, 0, 0},
            {0, 1, 0, 1, 0, 1, 1},
            {0, 1, 1, 0, 1, 1, 0},
            {1, 1, 1, 0, 0, 0, 1},
            {1, 0, 1, 1, 0, 1, 0},
            {1, 0, 0, 0, 1, 1, 1},
            {1, 1, 0, 1, 1, 0, 0}
        };
        /// <summary>
        /// Returns a matrix of n ints in rows (consisting of 0s and 1s) of length n (columns).
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int[,] RandomBits(int n)
        {
            int[,] numbers = new int[n, n];
            int[,] numbersTemp = new int[n, n];
            // Randoming the first number:
            int digit;
            for (int i = 0; i < n; i++)
            {
                digit = random.Next(2) == 0 ? 0 : 1; // 0 or 1 ... 50 %
                numbersTemp[0, i] = flipMatrix7[0, i] == 0 ? digit : 1 - digit;
            }
            // Creating the other numbers:
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    numbersTemp[i, j] = flipMatrix7[i, j] == 0 ? numbersTemp[0, j] : 1 - numbersTemp[0, j];
                }
            }
            // Shuffling the numbers:
            List<int> indeces = new List<int>();
            for (int i = 0; i < n; i++)
                indeces.Add(i);

            int index;
            for (int i = 0; i < n; i++)
            {
                index = indeces[random.Next(indeces.Count)];
                indeces.Remove(index);
                for (int j = 0; j < n; j++)
                    numbers[i, j] = numbersTemp[index, j];                
            }

            return numbers;
        }
        public static void PrintNumbers(int n, int[,] numbers, TextWriter writer)
        {
            for (int i = 0; i < n; i++)
            {
                writer.Write("{0}) ", i + 1);
                for (int j = 0; j < n; j++)
                {
                    writer.Write(numbers[i, j]);
                }
                writer.WriteLine();
            }
        }

        public static void PromptBitChange(int n, out int numberIndex, out int bitIndex)
        {
            // Both numberIndex and bitIndex range from 0 to n - 1.
            // User is asked for a number one higher, i.e. from 1 to n.
            // Prompting is always done by Console, never by StreamWriter.
            bool valid;
            bool first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zadej celé číslo od 1 do {0}. Zkus to znovu.", n);
                    Console.WriteLine();
                }
                else
                {
                    first = false;
                }
                Console.Write("Zadej číslo řádku, ve kterém se má číslo změnit (od 1 do {0}): ", n);
                valid = int.TryParse(Console.ReadLine(), out numberIndex) && numberIndex >= 1 && numberIndex <= n;
            }
            while (!valid);
            numberIndex -= 1;
            Console.WriteLine("Odpověď se zaznamenala.");
            Console.WriteLine();

            first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zadej celé číslo od 1 do {0}. Zkus to znovu.", n);
                    Console.WriteLine();
                }
                else
                {
                    first = false;
                }
                Console.Write("Zadej pozici číslice zleva, která se má ve vybraném čísle změnit z 0 na 1, nebo z 1 na 0 (od 1 do {0}): ", n);
                valid = int.TryParse(Console.ReadLine(), out bitIndex) && bitIndex >= 1 && bitIndex <= n;
            }
            while (!valid);
            bitIndex -= 1;
            Console.WriteLine("Odpověď se zaznamenala.");
        }

        public static void FlipChosenBit(int[,] numbers, int numberIndex, int bitIndex)
        {
            numbers[numberIndex, bitIndex] = 1 - numbers[numberIndex, bitIndex];
        }

        public static bool PromptForAnswer(int n, int correctNumberIndex, int correctBitIndex)
        {
            // Again, user is asked to give a number ranging from 1 to n.
            // However, the actual indeces are 1 less, i.e. from 0 to n - 1.
            int guessedNumberIndex, guessedBitIndex;
            bool valid;
            bool first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zadej celé číslo od 1 do {0}. Zkus to znovu.", n);
                    Console.WriteLine();
                }
                else
                {
                    first = false;
                }
                Console.Write("Zadej číslo řádku, ve kterém se nachází číslo, jež bylo změněno (od 1 do {0}): ", n);
                valid = int.TryParse(Console.ReadLine(), out guessedNumberIndex) && guessedNumberIndex >= 1 && guessedNumberIndex <= n;
            }
            while (!valid);
            guessedNumberIndex -= 1;
            Console.WriteLine("Odpověď se zaznamenala.");
            Console.WriteLine();

            first = true;
            do
            {
                if (!first)
                {
                    Console.WriteLine("Neplatná hodnota. Zadej celé číslo od 1 do {0}. Zkus to znovu.", n);
                    Console.WriteLine();
                }
                else
                {
                    first = false;
                }
                Console.Write("Zadej pozici číslice zleva, která byla v daném čísle změněna (od 1 do {0}): ", n);
                valid = int.TryParse(Console.ReadLine(), out guessedBitIndex) && guessedBitIndex >= 1 && guessedBitIndex <= n;
            }
            while (!valid);
            guessedBitIndex -= 1;
            Console.WriteLine("Odpověď se zaznamenala.");

            return correctNumberIndex == guessedNumberIndex && correctBitIndex == guessedBitIndex;
        }
        static void Main(string[] args)
        {
            int order = 2;  // Order of the final projective plane.
            outputOption option = outputOption.Console;
            string pathOut = "selfHealingCode – output.txt";
            string pathIn = "selfHealingCodeSolver – input.txt";

            int n = order ^ 2 + order + 1;  // Number of lines and points in the FPP of given order.
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                TextWriter writer;
                switch (option)
                {
                    case outputOption.Console:
                        writer = Console.Out;
                        break;
                    case outputOption.File:
                        writer = sw;
                        break;
                    default:
                        throw new NotImplementedException("Some writer was possible to be chosen, although it is not implemented.");
                }
                int[,] numbers = RandomBits(n);
                writer.WriteLine("Vylosovaná čísla:");
                PrintNumbers(n, numbers, writer);
                writer.WriteLine();
                // Always in Console:
                PromptBitChange(n, out int numberIndex, out int bitIndex);
                FlipChosenBit(numbers, numberIndex, bitIndex);
                Console.WriteLine("Číslo bylo úspěšně změněno!");
                writer.WriteLine();
                writer.WriteLine("Čísla po změně:");
                PrintNumbers(n, numbers, writer);
                // Always in Console:
                Console.WriteLine();
                Console.WriteLine("Nyní všechno zmizí, objeví se pouze čísla po změně a program se bude ptát, která pozice ve kterém číslu byla změněna.");
                Console.Write("Pokračuj zmáčknutím libovolné klávesy... ");
                Console.ReadKey();
                Console.Clear();

                // Guessing:
                Console.WriteLine("Čísla po právě jedné změně:");
                PrintNumbers(n, numbers, Console.Out);
                Console.WriteLine();
                bool correct = PromptForAnswer(n, numberIndex, bitIndex);
                Console.WriteLine("");
                Console.WriteLine("Vyhodnocení:");
                if (correct)
                {
                    Console.WriteLine("Správně! Jsi úplný detektiv! :-)");
                }
                else
                {
                    Console.WriteLine("Chyba. Správná odpověď byla, že se změnilo číslo v řádku {0}, a to na {1}. pozici zleva.", numberIndex + 1, bitIndex + 1);
                }
                // Testing the solver:
                Console.WriteLine();
                Console.WriteLine("Solver říká:");
                Solver.Solve(n, pathIn, out int changedRow, out int changedDigitPositionFromLeft);
                Console.WriteLine();
                Console.WriteLine("Změnilo se číslo v řádku {0}, a to na {1}. pozici zleva.", changedRow, changedDigitPositionFromLeft);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Zmáčknutím libovolné klávesy ukončíš program... ");
                Console.ReadKey();
            }
        }
    }
}
