using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StdinStdoutStderrCopying
{
    class Program
    {
        static void Main(string[] args)
        {
            string stdin = Console.ReadLine();
            string stdout = stdin, stderr = stdin;

            Console.WriteLine(stdout);
            Console.Error.WriteLine(stderr);
        }
    }
}
