using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace GenerovaniElemPermutaci
{
    class Program
    {
        static readonly string pathInPrefix = "S_";
        static readonly string pathInPostfix = ".txt";
        static readonly string pathOut = "Statistics.txt";
        static readonly char splitter = ' ';
        static readonly int invIndex = 2;
        private static int GetMaxInvs(int n)
        {
            // n choose 2
            return n * (n - 1) / 2;
        }

        private static void PrintInvArray(TextWriter writer, int[] invs)
        {
            for (int i = 0; i < invs.Length; i++)
            {
                writer.WriteLine("#in = {0}{1}# = {2}", i, i / 10 < 1 ? "   " : "  ", invs[i]);
            }
        }

        public static void PrintByInvs(int maxN)
        {
            using (StreamWriter writer = new(pathOut))
            {
                string line;
                int[] invs;
                for (int i = 1; i <= maxN; i++)
                {
                    using (StreamReader reader = new(pathInPrefix + i.ToString() + pathInPostfix))
                    {
                        invs = new int[GetMaxInvs(i) + 1];  // MaxInvs + 1 ... starting at 0, ending at MaxInvs
                        while ((line = reader.ReadLine()) != null)
                        {
                            // WARNING: All lines must be strictly data. Delete titles, texts, empty lines, etc.
                           invs[int.Parse(line.Split(splitter)[invIndex])]++;
                        }
                    }
                    writer.WriteLine(pathInPrefix + i.ToString() + ":");
                    PrintInvArray(writer, invs);
                    writer.WriteLine();
                }
            }
        }

        static void Main()
        {
            const int maxNInFile = 7;
            PrintByInvs(maxNInFile);
            Console.Write("Zmáčkněte libovolnou klávesu pro ukončení programu... ");
            Console.ReadKey();
        }
    }
}