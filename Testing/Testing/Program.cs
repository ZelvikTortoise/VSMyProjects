using System;
using System.Text;

namespace Testing
{
    class Program
    {
        static void Test(StringBuilder sb)
        {
            sb.Append(2);
        }

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            int x = 3;

            sb.Append(x);
            Test(sb);

            Console.WriteLine(sb.ToString());

            Console.ReadKey();
        }
    }
}
