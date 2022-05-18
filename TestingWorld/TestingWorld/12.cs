using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Try before expection.");
                throw new Exception();
                // Console.WriteLine("Try after expection.");   // Will be skipped.
            }
            catch
            {
                Console.WriteLine("Catch here!");
                throw new Exception();
            }
            finally
            {
                Console.WriteLine("Finally here!"); // Doesn't catch exception.
            }

            Console.WriteLine("After try block.");
            Console.ReadKey();
        }
    }
}
