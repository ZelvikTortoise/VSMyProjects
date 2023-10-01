using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Text;

namespace GenerovaniElemPermutaci
{
    class Perm
    {
        public const string elpSymbols = "abcdefghijklmnopqrstuvwxyz";
        private readonly static List<string> perms;
        public int[] Elps { get; private set; }
        public int NumOfInversions { get; private set; }
        static Perm()
        {
            perms = new List<string>();
        }
        public Perm(int[] elps)
        {
            this.Elps = new int[elps.Length];
            for (int i = 0; i < elps.Length; i++)
            {
                this.Elps[i] = elps[i];
            }            
            NumOfInversions = -1;   // Not valid yet.
            if (this.Reducable(out int[] reduced))
            {
                this.Elps = reduced;
            }
            this.SetNumOfInversions();      // BREAK POINT HERE to see what's being added.
        }

        private static int Previous(int[] elements, int fromIndex, int stepSize)
        {
            int index = fromIndex;
            do
            {
                index -= stepSize;
            }
            while (index >= 0 && elements[index] == 0);

            if (index >= 0)
            {
                // We found the previous element.
                return index;
            }
            else
            {
                // Searching for the first non-zero element to the left of fromIndex including fromIndex:
                index = 0;
                while (index <= fromIndex && elements[index] == 0)
                {
                    index++;
                }

                if (index <= fromIndex)
                {
                    // Returning the first non-zero element to the left of fromIndex including fromIndex.
                    return index;
                }
                else
                {
                    // Everything previous to fromIndex is 0 including the fromIndex element.
                    return Perm.Next(elements, fromIndex, stepSize, true);
                }
            }
        }
        /// <summary>
        /// Finds the next non-zero element in elements, starting from fromIndex (exluded), moving by stepSize indeces.
        /// </summary>
        /// <param name="elements">Array of elements.</param>
        /// <param name="fromIndex">Starting index, from which we move (cannot return fromIndex).</param>
        /// <param name="stepSize">The number of indeces we move to left every try.</param>
        /// <param name="fromPrevious">Was this function called by the Previous function?</param>
        /// <returns>The next non-zero element if there is any.
        ///          Otherwise -1 for identity if fromPrevious is true.
        ///          Otherwise -2, which indicates we should stop asking for next element and cut the branch.
        /// </returns>
        private static int Next(int[] elements, int fromIndex, int stepSize, bool fromPrevious)
        {
            int index = fromIndex;
            do
            {
                index += stepSize;
            }
            while (index < elements.Length && elements[index] == 0);

            if (index < elements.Length)
            {
                // We found the next element.
                return index;
            }
            else
            {
                if (fromPrevious)
                {
                    // Everything is 0, we found identity (id).
                    return -1;
                }
                else
                {
                    // Everything AFTER the fromIndex element is 0 but there are some non-zero elements too.
                    return -2;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Does the "aa" rule apply?</returns>
        private static bool DoesRule1Apply(int first, int second)
        {
            return first == second;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Does the "ca" rule apply?</returns>
        private static bool DoesRule2Apply(int first, int second)
        {
            return first > second + 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <returns>Does the "bab" rule apply?</returns>
        private static bool DoesRule3Apply(int first, int second, int third)
        {
            // Problem: has to switch "ac" in "cbac" to find reduction to "bcba".
            // => Added rule 4 (called the "cbac" order rule).
            return first == second + 1 && first == third;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="fourth"></param>
        /// <returns>Does the "cbac" order rule apply?</returns>
        private static bool DoesRule4Apply(int first, int second, int third, int fourth)
        {
            return first == fourth && second == third + 1 && third == fourth - 2;
        }
        /// <summary>
        /// Tries to reduce the permutation in two ways:
        /// 1. Reducing the number of elementary permutations in the final composition using the "aa" ("aa" -> "") rule.
        /// 2. Changing the order of elementary permutation using the "ca" ("ca" -> "ac") and "bab" ("bab" -> "aba") rules.
        /// </summary>
        /// <param name="reducedPerm">Permutation after the reduction.</param>
        /// <returns>Was the permutation changed (reduced or changed order)?</returns>
        public bool Reducable(out int[] reducedPermElements)
        {           
            reducedPermElements = this.Elps;  // Not creating a copy. (I would like to, but it's handled, hopefully, in the constructor of the Perm class.)
            int i = Perm.Previous(reducedPermElements, 0, 1);   // Needed for numbers starting with more zeroes like (0, 0, 1, 1, 3) etc.
            int j = Perm.Next(reducedPermElements, i, 1, false);
            int k;
            bool wasChanged = false;
            int temp;            

            // Ways to exit the while-loop:
            // 1. i == -1 => we found or reduced to identity (id)
            // 2. j == -2 => nothing to reduce
            // 3. k == -2 => nothing to reduce
            // => I use breaks and condition #2.

            // j == -2 means it's time to exit the loop (nothing to reduce)
            // j will never be -1 because it's never called by the Previous function
            while (j > 0)
            {
                if (Perm.DoesRule1Apply(reducedPermElements[i], reducedPermElements[j]))
                {
                    reducedPermElements[i] = 0;
                    reducedPermElements[j] = 0;

                    wasChanged = true;

                    i = Perm.Previous(reducedPermElements, i, 1);
                    if (i >= 0)
                    {
                        // We need to move by 1 but sometimes by 2!
                        // Trying to move by 2 if possible.
                        // i cannot be < 0 because Previous returns either >= 0 or -1 iff we have identity, which we already know we don't have.
                        i = Perm.Previous(reducedPermElements, i, 1);
                        j = Perm.Next(reducedPermElements, i, 1, false);
                    }
                    else
                    {
                        break;  // EXITING loop, ended in identity (id) => nothing to reduce.
                    }
                }
                else if (Perm.DoesRule2Apply(reducedPermElements[i], reducedPermElements[j]))
                {
                    temp = reducedPermElements[i];
                    reducedPermElements[i] = reducedPermElements[j];
                    reducedPermElements[j] = temp;

                    wasChanged = true;

                    // i = Perm.Previous(reducedPermElements, i, 2);  // We can move left by 2 instead of just 1.
                    i = Perm.Previous(reducedPermElements, i, 1);   // First move.
                    // i >= 0 because we cannot create identity (id) from non-identity just by switching positions of two elements.
                    i = Perm.Previous(reducedPermElements, i, 1);   // Second move.
                    j = Perm.Next(reducedPermElements, i, 1, false);
                }
                else
                { 
                    k = Perm.Next(reducedPermElements, j, 1, false);
                    if (k < 0)  // Can be changed to < 2 but then would be confusing because in these cases, always k == -2.
                    {
                        break;  // EXITING loop, nothing to reduce.
                    }
                    else if (Perm.DoesRule3Apply(reducedPermElements[i], reducedPermElements[j], reducedPermElements[k]))
                    {
                        reducedPermElements[i] = reducedPermElements[j];
                        reducedPermElements[j] = reducedPermElements[k];
                        reducedPermElements[k] = reducedPermElements[i];

                        wasChanged = true;

                        // We have to move left by 2 if possible (abaCbc -> abAbcb => it wouldn't find aBabcb).
                        i = Perm.Previous(reducedPermElements, i, 1);   // First move.
                        i = Perm.Previous(reducedPermElements, i, 1);   // Second move.

                        // i = Perm.Previous(reducedPermElements, i, 2);   // Both moves at the same time.
                        // ^ DOESN'T WORK BECAUSE OF SITUATIONS LIKE (2, 1, 0, 2, 3, 2) WHERE i HAS TO MOVE FROM i = 3 TO i = 0, BUT IT ONLY MOVES TO i = 1.

                        // Now: i is either to the left of i in the previous step, or is exactly the same as before.
                        // => i >= 0.
                        j = Perm.Next(reducedPermElements, i, 1, false);
                        // => j > 0.
                    }
                    else if (Perm.DoesRule4Apply(reducedPermElements[Perm.Previous(reducedPermElements, i, 1)], reducedPermElements[i], reducedPermElements[j], reducedPermElements[k]))
                    {
                        temp = reducedPermElements[j];
                        reducedPermElements[j] = reducedPermElements[k];
                        reducedPermElements[k] = temp;

                        wasChanged = true;

                        i = Perm.Previous(reducedPermElements, i, 1);
                        j = Next(reducedPermElements, i, 1, false);
                    }
                    else   // No rules apply.
                    {
                        // Moving to right by 1 step:
                        i = j;
                        j = k;
                    }
                }
            }

            // Moving all zeroes to the right of non-zero numbers (redundant?):
            if (wasChanged)
            {
                int[] elems = new int[reducedPermElements.Length];
                int here = 0;
                for (int index = 0; index < reducedPermElements.Length; index++)
                {
                    if (reducedPermElements[index] != 0)
                    {
                        elems[here] = reducedPermElements[index];
                        here++;
                    }
                }
                reducedPermElements = elems;
            }

            return wasChanged;
        }

        private void SetNumOfInversions()
        {
            int numOfInversions = 0;
            for (int i = 0; i < this.Elps.Length; i++)
            {
                if (this.Elps[i] != 0)
                {
                    numOfInversions++;
                }
            }
            this.NumOfInversions = numOfInversions;
        }
        public static bool TryAddToPerms(Perm perm)
        {
            string permString = perm.ToString();
            bool adding = true;

            // Check to avoid duplicity:
            for (int i = 0; i < Perm.perms.Count; i++)
            {
                if (permString == Perm.perms[i])
                {
                    adding = false;
                    break;
                }
            }

            // Add.
            if (adding)
            {                
                Perm.perms.Add(permString);
            }

            return adding;
        }
        public static void ClearPerms()
        {
            Perm.perms.Clear();
        }

        private static bool IsFirstLexicographicallyGreater(string permString, string perm2String)
        {
            bool greater = false;

            if (permString != perm2String)            
            {
                for (int i = 0; i < permString.Length; i++)
                {
                    if (permString[i] > perm2String[i])
                    {
                        greater = true;
                        break;
                    }
                    else if (permString[i] < perm2String[i])
                    {
                        break;
                    }
                }
            }            

            return greater;
        }
        public static bool IsFirstGreaterThanSecond(string permString, string perm2String)
        {
            bool greater = false;

            if (permString.Length > perm2String.Length)
            {
                greater = true;
            }
            else if (permString.Length == perm2String.Length)
            {
                if (IsFirstLexicographicallyGreater(permString, perm2String))
                {
                    greater = true;
                }
            }

            return greater;
        }
        public static void SortShortlexSlow()
        {
            string tempPerm;
            for (int i = 1; i < Perm.perms.Count; i++)
            {
                for (int j = 0; j < Perm.perms.Count - i; j++)
                {                    
                    if (IsFirstGreaterThanSecond(Perm.perms[j], Perm.perms[j + 1]))
                    {
                        tempPerm = Perm.perms[j];
                        Perm.perms[j] = Perm.perms[j + 1];
                        Perm.perms[j + 1] = tempPerm;
                    }
                }
            }
        }

        /// <summary>
        /// This function converts an ordinal number of an elementary permutation to its symbol.
        /// </summary>
        /// <param name="elp">Natural number from 1 to 26. </param>
        /// <returns>Elp-th lowercase letter of English alphabet.</returns>
        public static char ElpSymbol(int elp)
        {
            if (elp == 0)
            {
                return '\0';
            }
            else if (elp < 1 || elp > 26)
            {
                return '-';
            }
            else
            {
                return Perm.elpSymbols[elp - 1];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < this.Elps.Length; i++)
            {
                if (Perm.ElpSymbol(this.Elps[i]) != 0)
                {
                    sb.Append(Perm.ElpSymbol(this.Elps[i]));
                }
            }

            return sb.ToString();
        }

        public static void ShowPerms(bool onePerLine)
        {
            if (onePerLine)
            {
                Console.WriteLine("#in = 0     id");
                for (int i = 1; i < Perm.perms.Count; i++)
                {                    
                    Console.WriteLine("#in = {0}     {1}", Perm.perms[i].Length, Perm.perms[i].ToString());
                }
            }
            else
            {
                int inversions = 0;
                Console.WriteLine("#in = 0     | id");
                for (int i = 1; i < Perm.perms.Count; i++)
                {
                    if (Perm.perms[i].Length > inversions)
                    {
                        inversions++;
                        Console.WriteLine();
                        Console.Write("#in = {0}     ", inversions);
                    }
                    Console.Write("| {0}", Perm.perms[i].ToString());
                }
            }
            Console.WriteLine();
            Console.WriteLine("Celkem permutací: {0}", Perm.perms.Count);
        }
        public static void PrintPerms(TextWriter writer, bool onePerLine)
        {
            using (writer)
            {
                if (onePerLine)
                {
                    writer.WriteLine("#in = 0     id");
                    for (int i = 1; i < Perm.perms.Count; i++)
                    {
                        writer.WriteLine("#in = {0}     {1}", Perm.perms[i].Length, Perm.perms[i].ToString());
                    }
                }
                else
                {
                    int inversions = 0;
                    writer.WriteLine("#in = 0     | id");
                    for (int i = 1; i < Perm.perms.Count; i++)
                    {
                        if (Perm.perms[i].Length > inversions)
                        {
                            inversions++;
                            writer.WriteLine();
                            writer.Write("#in = {0}     ", inversions);
                        }
                        writer.Write("| {0}", Perm.perms[i].ToString());
                    }
                }
                writer.WriteLine();
                writer.WriteLine("Celkem permutací: {0}", Perm.perms.Count);
            }
        }
    }


    class Program
    {
        public static int N { get; private set; }
        public static int MaxInvs { get; private set; }
        public static int MaxElp { get; private set; }
        private static void SetN(int num)
        {
            if (num > 0 && num < 27)
            {
                N = num;
            }
            else
            {
                N = 0;
            }

            MaxInvs = N * (N - 1) / 2;
            MaxElp = N - 1;
        }
        private static void DFS(int[] elps, int digit, int elpNum)
        {
            if (digit >= MaxInvs)
            {
                // The permutation in S_N cannot be a reduced composition of more than maxInvs elementary permutations.
                // Therefore: End.
                return;
            }
            else
            {
                // Asigning the digit:
                elps[digit] = elpNum;
                // Everything to the right should be zeroes:
                for (int i = digit + 1; i < MaxInvs; i++)
                {
                    elps[i] = 0;
                }

                Perm perm = new(elps);
                // TODO: MAIN PROBLEM = DUE TO NOT USING REDUCTION WHILE GENERATING, THE ARRAY IS TOO SMALL FOR SOME PERMUTATIONS!
                // TODO: Because of this, some branches cannot be generated, eg.: ababc (= bac) -> ababca (= bc ... exists) | ababcb (= aba ... exists) | ababcc (= ba ... exists).
                /*// Changing to reduced version (redundant?):
                for (int i = 0; i < elps.Length; i++)
                {
                    elps[i] = perm.Elps[i];
                    // PROBLEM: This would overwrite other branches.
                    // Example: (1, 1, 0, 0) -> reduction: (0, 0, 0, 0) -> next i: (0, 2, 0, 0) instead of (1, 2, 0, 0).
                }
                digit = perm.NumOfInversions - 1;*/
                if (Perm.TryAddToPerms(perm))
                {                                       
                    for (int i = 1; i <= MaxElp; i++)
                    {
                        // Recursion:
                        DFS(elps, digit + 1, i);
                    }
                }
            }
        }
        static void Generate()
        {            
            int[] elps = new int[MaxInvs];
            Perm.TryAddToPerms(new(elps));   // Adding id.
            for (int i = 1; i <= MaxElp; i++)
            {
                DFS(elps, 0, i);
            }
        }

        static void VSO(int[] elps, int digit)
        {
            if (digit == Program.MaxInvs)
            {
                Perm.TryAddToPerms(new Perm(elps));
            }
            else
            {
                for (int i = 0; i <= Program.MaxElp; i++)
                {
                    elps[digit] = i;
                    VSO(elps, digit + 1);
                }
            }
        }

        static void Generate2()
        {
            int[] elps = new int[MaxInvs];
            VSO(elps, 0);
        }

        static void Show(TextWriter writer, bool onePerLine)
        {
            if (writer == Console.Out)
            {
                Console.WriteLine("PERMUTACE GRUPY S_{0}:", N);
                Perm.ShowPerms(onePerLine);
            }
            else
            {
                writer.WriteLine("PERMUTACE GRUPY S_{0}:", N);
                Perm.PrintPerms(writer, onePerLine);
            }
        }
        static void Main()
        {
            Console.WriteLine("GENEROVÁNÍ VŠECH PERMUTACÍ GRUPY SYMETRICKÉ GRUPY S_n");
            Console.WriteLine("Každá permutace bude vypsána jako slože jejich elementárních permutací v shortlexikografickém pořadí.");
            Console.WriteLine();
            Console.Write("Zadejte přirozené číslo: n = ");
            string answer = Console.ReadLine() ?? "1";
            SetN(int.Parse(answer));
            Console.WriteLine();
            if (N <= 0)
            {
                Console.WriteLine("NELZE");
                return;
            }
            Console.WriteLine("Chcete permutace vygenerovat do konzole, nebo do souboru?");
            Console.WriteLine("1 - konzole");
            Console.WriteLine("2 - soubor");
            Console.WriteLine("? - konzole");
            Console.WriteLine();
            Console.Write("Vaše odpověď: ");
            answer = Console.ReadLine() ?? "";
            TextWriter writer;
            if (answer == "2")
            {
                Console.WriteLine("Zvoleno: SOUBOR");
                Console.WriteLine("Zadejte cestu k souboru.");
                Console.Write("Cesta: ");
                answer = Console.ReadLine() ?? "";
                writer = new StreamWriter(answer);
            }
            else
            {
                Console.WriteLine("Zvoleno: KONZOLE");
                writer = Console.Out;
            }
            Console.WriteLine();
            Generate2();    // TODO: CHANGE BACK TO Generate() after fixing it.
            Perm.SortShortlexSlow();
            Show(writer, true);
            Console.WriteLine();

            Console.WriteLine("Zmáčkněte libovolnou klávesu pro ukončení programu... ");
            Console.ReadKey();

            // MATH PROBLEM:
            // S_5, in = 10:
            // abacbadcba =?= abacbdcbad
        }
    }
}