using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;

namespace GenerovaniElemPermutaci
{
    class Perm
    {
        public const string elpSymbols = "abcdefghijklmnopqrstuvwxyz";
        private readonly static List<string> perms;
		private readonly static List<string> permBirths;
        public int[] Elps { get; private set; }
        public int NumOfInversions { get; private set; }
        public string BirthText { get; }
        static Perm()
        {
            perms = new List<string>();
            permBirths = new List<string>();
        }
        public Perm(int[] elps)
        {
            StringBuilder sb = new();
            this.Elps = new int[elps.Length];
            for (int i = 0; i < elps.Length; i++)
            {
                this.Elps[i] = elps[i];
                sb.Append(elps[i]);
            }
            BirthText = sb.ToString();
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
        /// AA -> none
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Does the "aa" rule apply?</returns>
        private static bool DoesRule1Apply(int first, int second)
        {
            return first == second;
        }
        /// <summary>
        /// CA -> AC
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Does the "ca" rule apply?</returns>
        private static bool DoesRule2Apply(int first, int second)
        {
            return first > second + 1;
        }
        /// <summary>
        /// BAB -> ABA
        /// CBAC -> BCBA
        /// ...
        /// Decreasing by 1 at least n-times, then increases (jumps up) by n -> 2nd largest, 1st largest, 2nd largest and then decrease until the length is the same
        /// Don't forget about CBC -> BCB, DCBD -> CDCB, ... (doesn't have to contain A, etc.)
        /// Inequality is necessary because of CBAB (jumpPotential == 2 >= B - A == 1)
        /// The jump has to be up => second > first, otherwise BA would be considered as a jump (because jumpPotential == 0 >= -1)
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="jumpPotential"></param>
        /// <returns>Does the generalized "bab"/"cbac" rule apply?</returns>
        private static bool DoesRule3Apply(int first, int second, int jumpPotential)
        {
            return second > first && second - first <= jumpPotential;
        }
        /// <summary>
        /// Helps to handle sitatuations like DCEEBAD (rule 1) -> DCBAD (rule 3) -> CDCBA.
        /// If there was a reduction inside the decreasing sequence, we shall recheck the sequence from its start.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="lastReductionRelativePosition">0 ... between i and j, -1 ... right after j, 1 ... right before i, 2 ... right before predecessor of i, etc.</param>
        /// <returns>Was there a reduction inside a decreasing sequence with such jump that rule 3 could apply?</returns>
        private static bool CanRule3Apply(int first, int second, int lastReductionRelativePosition)
        {
            // EEDCBAD -> DCBAD -> when i: A, j: D, the lastReductionRelativePosition == 4 > 3 == D - A => no recheck needed
            // DEECBAD -> DCBAD -> when i: A, j: D, the lastReductionRelativePosition == 3 <= 3 == D - A => we will backtrack
            return lastReductionRelativePosition <= second - first;
        }
        /// <summary>
        /// Decreasing by 1 (for generalized BAB/CBAC rule)?
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>True iff BA, CB, DC, etc.</returns>
        private static bool DoesSequenceDecreaseByOne(int first, int second)
        {
            // BA => B - A == 1
            return first - second == 1;
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
            int decreasingByOne = 0; // 2 ... cba, 3 ... dcba, etc.; used for DCBAD -> CDCBA rule
            bool wasChanged = false;
            int temp, max, min;
            const int smallMaxInt = 1_000_000;   // if we use int.Max and add 1, the value would suddenly be less than anything => condition would be true :( => new practical value for maxInt
            int lastReductionRelativePosition = smallMaxInt; // 0 ... between i and j, -1 ... right after j, 1 ... right before i, 2 ... right before predecessor of i, etc.; helps us track how many times we gotta go back after AA reduction to satisfy the DCBAD -> CDCBA rule
            // if lastReductionRelativePosition's value is around smallMaxInt value, it means, it's invalid
            // this smallMaxInt value should be much more than the greatest value of letter in elem. permutations but much less then an actual maxInt (much meaning at least the length of the longest permutation notated in elem. permutations)

            // Ways to exit the while-loop:
            // 1. i == -1 => we found or reduced to identity (id)
            // 2. j == -2 => nothing to reduce            
            // => I use breaks and condition #2.

            // j == -2 means it's time to exit the loop (nothing to reduce)
            // j will never be -1 because it's never called by the Previous function

            // DEBUG - BREAK POINT:
            /*/
            if (reducedPermElements[0] == 1 &&
                reducedPermElements[1] == 2 &&
                reducedPermElements[2] == 3 &&
                reducedPermElements[3] == 3 &&
                reducedPermElements[4] == 1 &&
                reducedPermElements[5] == 2)
            {
                Console.WriteLine("TADY");
            }
            /*/
            while (j > 0)
            {
                if (Perm.DoesRule1Apply(reducedPermElements[i], reducedPermElements[j]))
                {
                    reducedPermElements[i] = 0;
                    reducedPermElements[j] = 0;

                    wasChanged = true;

                    temp = i;
                    i = Perm.Previous(reducedPermElements, i, 1);
                    if (i >= 0)
                    {
                        // We move in such way that the change is either between i and j or right before i (in case everything before i is empty)
                        j = Perm.Next(reducedPermElements, i, 1, false);
                        if (temp != i)
                        {
                            lastReductionRelativePosition = 0;    // i moved left, change is between i and j -> value 0
                        }
                        else
                        {
                            lastReductionRelativePosition = 1;    // i didn't move (is the most left element), change is right before i -> value 1
                        }
                        decreasingByOne = 0;    // reset;
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

                    // We have to move number of times according to the value of element indicated by i after the swap!
                    // BADB (D ... i, B ... j) -> BABD (B ... i, D ... j) -> move twice left to find BAB -> ABA change
                    // CBAFC -> CBACF -> moving three times (value of C) -> BCBAF
                    // This only applies on the final letter in CBAC rule (otherwise, there would be a jump of 2 in the middle -> can't be).

                    // Moving (maximum number of) reducedPermElements[i]-times to left:
                    temp = i;
                    j = i;  // behaves as temp2 (we'll assign the real value to j after the loop)
                    for (int step = 1; step <= reducedPermElements[temp]; step++)
                    {
                        i = Perm.Previous(reducedPermElements, i, 1);
                        if (j == i)
                        {
                            break;  // there's nowhere to move (we're at the most left element)
                        }
                        else
                        {
                            j = i;  // reassigning the new value (j behaves at temp2)
                            lastReductionRelativePosition--;    // moving left
                        }
                    }
                    j = Perm.Next(reducedPermElements, i, 1, false);    // j is actually j again, not temp2
                    decreasingByOne = 0;    // reset;
                }
                else if (Perm.DoesRule3Apply(reducedPermElements[i], reducedPermElements[j], decreasingByOne))
                {
                    // Problem:
                    // ABCCBC

                    // CBAC -> BCBA
                    // A ... i, C ... j
                    // max == reducedPermElements[j] == 3
                    min = reducedPermElements[i];
                    max = reducedPermElements[j];
                    for (int addedValueFromMin = 0; addedValueFromMin < max - min + 1; addedValueFromMin++)
                    {
                        reducedPermElements[j] = min + addedValueFromMin;   // for CBC -> BCB (otherwise it would change to ABA)
                        j = Perm.Previous(reducedPermElements, j, 1);       // this rule applies only if there are enough elements (always >= 0)
                    }
                    reducedPermElements[j] = max - 1;   // now j points toward the first letter (first B in BCBA)

                    // i must point on a letter left from the first letter by B (second largest)
                    // i.e. from start of BCBA (because of BA CBAC -> BA BCBA -> ABACBA or B CBAC -> BB CBA -> CBA)
                    // i already points 1 to the first letter
                    // max - 1 is the value of the second largest letter (B = C - 1)
                    // => move left by max - 1 but always at least once because of the AA-rule
                    // => max(1, max - 2)

                    // Adjusting values so i is first (could be done beforehands -> OPTIMIZE):
                    i = j;
                    j = Perm.Next(reducedPermElements, i, 1, false);    // this is always valid (> 0)
                    for (int step = 1; step <= Math.Max(1, max - 1); step++)
                    {
                        temp = i;  // later we can check if there's anything to the left of i
                        i = Perm.Previous(reducedPermElements, i, 1);
                        if (i == temp)
                        {
                            break;  // there's nowhere to move anymore, we're at the start
                        }
                        else
                        {
                            j = temp; // moving j
                            lastReductionRelativePosition--;
                        }
                    }
                    decreasingByOne = 0;    // reset;
                }
                else if (Perm.CanRule3Apply(reducedPermElements[i], reducedPermElements[j], lastReductionRelativePosition))
                {
                    // It is possible that Rule 3 should apply but we didn't catch it because of a reduction (use of Rule 1).
                    // We have to backtrack by the jump value == reducedPermElements[j] - reducedPermElements[i].
                    max = reducedPermElements[j] - reducedPermElements[i];
                    for (int step = 1; step <= max; step++)
                    {
                        temp = i;
                        i = Perm.Previous(reducedPermElements, i, 1);
                        if (i == temp)
                        {
                            break;  // nowhere to move (nothing left from i)
                        }
                        else
                        {
                            j = temp;   // moving j
                        }
                    }
                    lastReductionRelativePosition = smallMaxInt;    // this value is invalid until another reduction (otherwise we would keep backtracking the same part infinite number of times)
                    decreasingByOne = 0;    // reset;
                }
                else   // No rules apply.
                {
                    if (Perm.DoesSequenceDecreaseByOne(reducedPermElements[i], reducedPermElements[j]))
                    {
                        decreasingByOne++;
                    }
                    else
                    {
                        decreasingByOne = 0;
                    }

                    // Moving right by 1:
                    i = j;
                    j = Perm.Next(reducedPermElements, i, 1, false);
                    lastReductionRelativePosition++;
                    // Note: if j < 0 (j == -2), the loop will end because of its condition
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

            // DEBUG – BREAK POINT:
            /*/
            if (permString == "abababc")
            {
                Console.WriteLine("AHA?");
            }
            /*/

            // Add.
            if (adding)
            {
                Perm.perms.Add(permString);
                Perm.permBirths.Add(perm.BirthText);
            }

            return adding;
        }
        public static bool NewBirthTextFound(Perm perm)
        {
            string permBirthText = perm.BirthText;
            bool newFound = true;

            for (int i = 0; i < Perm.permBirths.Count; i++)
            {
                if (permBirthText == Perm.permBirths[i])
                {
                    newFound = false;
                    break;
                }
            }

            if (newFound)
            {
                Perm.permBirths.Add(perm.BirthText);
            }

            return newFound;
        }
        public static void ClearAll()
        {
            ClearPerms();
            ClearPermBirths();
        }
        public static void ClearPerms()
        {
            Perm.perms.Clear();
        }
        public static void ClearPermBirths()
        {
            Perm.permBirths.Clear();
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
            string spaces = "     ";
            if (onePerLine)
            {
                Console.WriteLine("#in = 0{0}id", spaces);
                for (int i = 1; i < Perm.perms.Count; i++)
                {
                    if (Perm.perms[i].Length / 10 != (Perm.perms[i].Length - 1) / 10)
                    {
                        spaces = spaces.Remove(spaces.Length - 1);   // one less space, because the numbers of inversions use one more digit from now
                    }
                    Console.WriteLine("#in = {0}{1}{2}", Perm.perms[i].Length, spaces, Perm.perms[i].ToString());
                }
            }
            else
            {
                int inversions = 0;
                Console.WriteLine("#in = 0{0}| id", spaces);
                for (int i = 1; i < Perm.perms.Count; i++)
                {
                    if (Perm.perms[i].Length > inversions)
                    {
                        inversions++;
                        if (inversions / 10 != (inversions - 1) / 10)
                        {
                            spaces = spaces.Remove(spaces.Length - 1);   // one less space, because the numbers of inversions use one more digit from now
                        }
                        Console.WriteLine();
                        Console.Write("#in = {0}{1}", inversions, spaces);
                    }
                    Console.Write("| {0}", Perm.perms[i].ToString());
                }
            }
            Console.WriteLine();
            Console.WriteLine("Celkem permutací: {0}", Perm.perms.Count);
        }
        public static void PrintPerms(TextWriter writer, bool onePerLine)
        {
            string spaces = "     ";
            using (writer)
            {
                if (onePerLine)
                {
                    writer.WriteLine("#in = 0{0}id", spaces);
                    for (int i = 1; i < Perm.perms.Count; i++)
                    {
                        if (Perm.perms[i].Length / 10 != (Perm.perms[i].Length - 1) / 10)
                        {
                            spaces = spaces.Remove(spaces.Length - 1);   // one less space, because the numbers of inversions use one more digit from now
                        }
                        writer.WriteLine("#in = {0}{1}{2}", Perm.perms[i].Length, spaces, Perm.perms[i].ToString());
                    }
                }
                else
                {
                    int inversions = 0;
                    writer.WriteLine("#in = 0{0}| id", spaces);
                    for (int i = 1; i < Perm.perms.Count; i++)
                    {
                        if (Perm.perms[i].Length > inversions)
                        {
                            inversions++;
                            if (inversions / 10 != (inversions - 1) / 10)
                            {
                                spaces = spaces.Remove(spaces.Length - 1);   // one less space, because the numbers of inversions use one more digit from now
                            }
                            writer.WriteLine();
                            writer.Write("#in = {0}{1}", inversions, spaces);
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

                // We're going to generate a new branch from this permutation if and only if
                // the permutation is completely new (added to our list)
                // OR
                // the BirthText of this permutation is new => we haven't been here yet.

                if (Perm.TryAddToPerms(perm) || Perm.NewBirthTextFound(perm))
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
            Generate();
            Perm.SortShortlexSlow();
            Show(writer, true);
            Console.WriteLine();

            Console.WriteLine("Zmáčkněte libovolnou klávesu pro ukončení programu... ");
            Console.ReadKey();
        }
    }
}