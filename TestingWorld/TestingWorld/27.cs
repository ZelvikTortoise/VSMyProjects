using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWorld
{
    class Table
    {
        public Table()
        {
            Console.WriteLine("Hi! I am a new instance of Table class.");
        }

        public override string ToString()
        {
            return "Table named: ";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Table> dict = new Dictionary<string, Table>();
            dict.Add("t1", new Table());
            dict.Add("t2", new Table());
            dict.Add("t3", new Table());
            Console.WriteLine();

            List<KeyValuePair<string, Table>> list = new List<KeyValuePair<string, Table>>(dict);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].Value.ToString() + list[i].Key);                
            }
            Console.ReadKey();
        }
    }
}
