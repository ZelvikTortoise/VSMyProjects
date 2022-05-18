using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRNG
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxDigitInOneRow = 6; // How much our digits in a row will stop the RNG.
            Random random = new Random();
            int digit;  // Last randomed digit.
            int examinedDigit = int.MaxValue;   // The digit at the end of the stream.
            // Note: Inicialized to such value that it cannot be randomed as a digit.
            int counter = 1;    // How many our digits in a row are at the end of the stream at the moment.

            do
            {
                digit = random.Next(0, 10); // Only digits 0–9 are digits.
                Console.Write(digit.ToString());
                if (examinedDigit == digit)
                {
                    counter++;
                }
                else
                {
                    counter = 1;
                    examinedDigit = digit;
                }
            }
            while (counter < maxDigitInOneRow) ;
            
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
