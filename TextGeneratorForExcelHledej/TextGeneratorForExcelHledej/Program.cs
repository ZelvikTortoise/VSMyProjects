using System.Text;
using System.IO;

namespace TextGeneratorForExcelHledej
{
    internal class Program
    {
        readonly static Random random = new();
        static void Main()
        {
            const int numberOfLines = 300;
            const string pathOut = "output.txt";
            const char separator = '.';
            string firstPart, secondPart;
            int len;
            StringBuilder sb = new();
            char nextChar;

            // A–Z = 65–90
            // a–z = 97– 122

            using StreamWriter sw = new(pathOut);
            for (int iteration = 1; iteration <= numberOfLines; iteration++)
            {
                len = random.Next(3, 11);   // 3–10
                for (int i = 0; i < len; i++)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        nextChar = (char)random.Next(65, 91);
                    }
                    else
                    {
                        nextChar = (char)random.Next(97, 123);
                    }
                    sb.Append(nextChar);
                }
                firstPart = sb.ToString();
                sb.Clear();
                len = random.Next(3, 11);   // 3–10
                for (int i = 0; i < len; i++)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        nextChar = (char)random.Next(65, 91);
                    }
                    else
                    {
                        nextChar = (char)random.Next(97, 123);
                    }
                    sb.Append(nextChar);
                }
                secondPart = sb.ToString();
                sb.Clear();

                if (iteration < numberOfLines)
                    sw.WriteLine(string.Concat(firstPart, separator, secondPart));
                else
                    sw.Write(string.Concat(firstPart, separator, secondPart));
            }
        }
    }
}
