using System.Runtime.ExceptionServices;

namespace GenshinAlchemy
{
    internal class Program
    {
        static Random rnd = new Random();
        public static int First(int res, out int rem)
        {
            int products = 0;
            while (res > 2)
            {
                res -= 3;
                products++;
                if (rnd.Next(4) == 0)   // 25 % chance to return a resource
                {
                    res++;
                }
            }

            rem = res;
            return products;
        }

        public static int Second(int res, out int rem)
        {
            int products = 0;
            
            while (res > 2)
            {
                res -= 3;
                products++;
                if (rnd.Next(10) == 0)  // 10 % chance to create an extra product
                {
                    products++;
                }
            }

            rem = res;
            return products;
        }
        static void Main(string[] args)
        {
            int products1 = 0;
            int products2 = 0;
            bool first;
            int difference;
            double percentage;
            int remaining;
            int resources = 20;
            int attempt = 1;
            while (resources > 0)   // Arithmetic overflow will end the cycle.
            {                
                Console.WriteLine("*********************************************");
                Console.WriteLine("       ATTEMPT NUMBER {0}", attempt);
                Console.WriteLine("*********************************************");
                Console.WriteLine("STARTING RESOURCES");
                Console.Write("Input starting resources: ");
                /*
                int resources = int.Parse(Console.ReadLine());
                if (resources < 0)
                {
                    Console.WriteLine("Invalid number of resources. Press any key for program to exit...");
                    Console.ReadKey();
                    return;
                }
                */
                Console.WriteLine("---------------------------");
                Console.WriteLine("OBTAINED PRODUCTS");
                products1 = First(resources, out remaining);
                Console.WriteLine("Using the first method: {0} ({1} resources remaining)", products1.ToString(), remaining.ToString());
                Console.WriteLine();
                products2 = Second(resources, out remaining);
                Console.WriteLine("Using the second method: {0} ({1} resources remaining)", products2.ToString(), remaining.ToString());
                Console.WriteLine();
                Console.WriteLine("SUMMARY:");
                if (products1 == products2)
                {
                    Console.WriteLine("Both methods yielded the same number of products.");
                }
                else
                {
                    first = products1 > products2 ? true : false;
                    difference = Math.Abs(products1 - products2);
                    percentage = Math.Round(100d * (first ? products1 : products2) / (products1 + products2), 3);
                    Console.WriteLine("The " + (first ? "first" : "second") + " method produced " + difference + " more products than the other one method.");
                    Console.WriteLine("The " + (first ? "first" : "second") + " method produced " + percentage + " % of the total products, while the other method produced " + (100 - percentage) + " % of the total products.");
                }
                Console.WriteLine();
                Console.WriteLine();

                resources *= 10;
                attempt++;
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to exit the program...");
            Console.ReadKey();
        }
    }
}