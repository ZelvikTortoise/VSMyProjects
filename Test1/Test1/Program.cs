using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTestsOutOfContext")]
namespace Test1
{
    class A
    {
        public void Method()
        {
            // does nothing
            // also not being checked (but can be)
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string pathIn = "blabla";
            const string pathOut = "blabla2";

            // CASE 1:
            using (StreamReader sr = new StreamReader(pathIn))
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                Console.WriteLine("Nothing went wrong.");
            }
            // No catch block, otherwise almost the same.
            // The using heads aren't in a try block! -> can throw exception


            // CASE 2:
            try
            {
                using (StreamReader sr = new StreamReader(pathIn))
                using (StreamWriter sw = new StreamWriter(pathOut))
                {
                    Console.WriteLine("Nothing went wrong.");
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong.");
            }
            // The same as case 3.


            // CASE 3:
            {
                StreamReader sr = null;
                StreamWriter sw = null;
                try
                {
                    sr = new StreamReader(pathIn);
                    sw = new StreamWriter(pathOut);
                    Console.WriteLine("Nothing went wrong.");
                }
                catch
                {
                    Console.WriteLine("Something went wrong.");
                }
                finally
                {
                    if (sr != null)
                        sr.Dispose();
                    if (sw != null)
                        sw.Dispose();
                }
            }
        }
    }
}