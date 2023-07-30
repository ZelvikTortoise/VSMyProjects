using System.Security.Principal;
using System.Text;
using System.Collections.Generic;

namespace CampTreasureHunt1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int dirtyLen = 100;
            int correctLen = 90;
            int codeLen = 3;
            // bool repeatDigits = true;
            string code;
            bool[] used = new bool[correctLen];
            string correct;
            List<int> circleIndeces = new List<int>();
            string dirty;
            List<int> insertIndeces = new List<int>();
            int[] dirtyIndeces = new int[dirtyLen - correctLen];

            // 1) Correct string generation:
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= correctLen; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            correct = sb.ToString();
            /*
            for (int i = 0; i < len; i++)
            {
                used[i] = false;
            }*/

            // 2) Code generation:
            sb = new StringBuilder();
            int index, j = 1;
            while (j <= codeLen)
            {
                index = random.Next(0, correctLen);
                if (!used[index])
                {
                    used[index] = true;
                    circleIndeces.Add(index);
                    j++;
                }
            }
            circleIndeces.Sort();
            for (int i = 0; i < circleIndeces.Count; i++)
            {
                sb.Append(correct[circleIndeces[i]]);
            }
            code = sb.ToString();

            // 3) Making string dirty:
            for (int i = 1; i <= dirtyLen - correctLen; i++)
            {
                insertIndeces.Add(random.Next(0, correctLen));
            }
            insertIndeces.Sort();

            index = 0;
            j = 0;
            sb = new StringBuilder();
            while (j < correctLen)
            {
                if (index < dirtyLen - correctLen && j == insertIndeces[index])
                {
                    sb.Append(random.Next(0, 10));
                    dirtyIndeces[index] = j + index;
                    index++;
                }
                else
                {
                    sb.Append(correct[j]);
                    j++;
                }
            }
            dirty = sb.ToString();

            // 4) Generating output:
            Console.WriteLine("Dirty string:");
            Console.WriteLine(dirty);
            Console.WriteLine();
            Console.WriteLine("Correct string:");
            Console.WriteLine(correct);
            Console.WriteLine();
            Console.Write("Crossouts: ");
            for (int i = 0; i < dirtyIndeces.Length - 1; i++)
            {
                Console.Write("{0}, ", dirtyIndeces[i] + 1);
            }
            Console.WriteLine(dirtyIndeces[dirtyIndeces.Length - 1] + 1);
            Console.WriteLine();
            Console.Write("Circles: ");
            for (int i = 0; i < circleIndeces.Count - 1; i++)
            {
                Console.Write("{0}, ", circleIndeces[i] + 1);
            }
            Console.WriteLine(circleIndeces[circleIndeces.Count - 1] + 1);
            Console.WriteLine();
            Console.Write("Code: ");
            Console.WriteLine(code);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}