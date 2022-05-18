using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    struct Empty
    {
        // Variable of this struct will take 0 B.
        public override string ToString()
        {
            return "Very empty space, 0 bytes taken.";
        }
    }



    class Program
    {
        public static void PAKtC()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void PAKtC(bool end)
        {
            Console.WriteLine();
            if (end)
            {
                Console.Write("Press any key to close the program... ");
                Console.ReadKey();
            }                
            else
            {
                Console.Write("Press any key to continue... ");
                Console.ReadKey();
                Console.WriteLine();
            }                
        }

        static void Main(string[] args)
        {
            Empty[] emptyField = new Empty[100];
            Empty[] emptyField2 = new Empty[] { };

            // Should've been a method.
            Console.WriteLine($"Array \"{nameof(emptyField)}\":");
            Console.WriteLine($"Length: { emptyField.Length}");

            PAKtC();

            for (int i = 0; i < emptyField.Length; i++)
            {
                Console.Write("{0:D3}: ", i + 1);
                Console.WriteLine(emptyField[i]);
            }

            PAKtC();

            Console.WriteLine($"Array \"{nameof(emptyField2)}\":");
            Console.WriteLine($"Length: { emptyField2.Length}");

            for (int i = 0; i < emptyField2.Length; i++)
            {
                Console.Write("{0:D3}: ", i + 1);
                Console.WriteLine(emptyField2[i]);
            }

            PAKtC();

            PAKtC(true);
        }
    }
}
