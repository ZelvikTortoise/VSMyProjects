using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestingWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            bool poZnacich = false;
            string cesta1 = "";
            Encoding cj1 = Encoding.GetEncoding(1250);
            string cesta2 = "";
            bool append = false;
            Encoding cj2 = Encoding.GetEncoding(852);


            // Lze psát takto.
            using (StreamReader sr = new StreamReader(cesta1, cj1))
            using (StreamWriter sw = new StreamWriter(cesta2, append, cj2)) // Pozor na zadávání kódování u sw. Nutné bool append!
            {
                // Kopie souboru.
                if (poZnacich)
                    while (!sr.EndOfStream)
                        sw.Write((char)sr.Read());
                else
                    sw.Write(sr.ReadToEnd());
            }
        }
    }
}
