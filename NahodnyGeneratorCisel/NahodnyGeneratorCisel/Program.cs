using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NahodnyGeneratorCisel
{
    class Program
    {
        const int repeatSame = 1;
        const int repeatDif = 2;
        const int notRepeat = 3;

        static int SelectMode()
        {
            int mode = 0;

            do
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Select your mode:");
                Console.WriteLine("{0} - repeat with same min and max", repeatSame);
                Console.WriteLine("{0} - repeat but with different min and max", repeatDif);
                Console.WriteLine("{0} - do not repeat, close app after first random number", notRepeat);
                Console.Write("Your choice: ");
                mode = int.Parse(Console.ReadLine());
            }
            while (mode != repeatSame & mode != repeatDif & mode != notRepeat);

            return mode;
        }

        static void Main(string[] args)
        {
            Random rand = new Random();
            bool end = false;
            int iteration = 1;
            const string cont = "y";

            int minValue;
            int maxValue;

            Console.WriteLine("Random number generator");
            switch (SelectMode())
            {
                case repeatSame:
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Input ({0}):", iteration);
                    Console.Write("Minimum: ");
                    minValue = int.Parse(Console.ReadLine());
                    Console.Write("Maximum: ");
                    maxValue = int.Parse(Console.ReadLine());
                    while (maxValue < minValue)
                    {
                        Console.WriteLine("WARNING: Please reassign the maximum value to be greater or equal to the minimum value.");
                        Console.Write("Maximum: ");
                        maxValue = int.Parse(Console.ReadLine());
                    }
                    while (!end)
                    {                        
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Action ({0}):", iteration);
                        Console.WriteLine("Randoming a number from " + minValue + " to " + maxValue + ":");
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Output ({0}):", iteration);
                        Console.WriteLine("Randomed: " + rand.Next(minValue, maxValue + 1));
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Do you want to continue? ({0} = continue / other = end)", cont);
                        Console.Write("Answer: ");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() != cont)
                        {
                            end = true;
                            Console.WriteLine("The program will end...");
                        }
                        else
                        {
                            iteration++;
                            Console.WriteLine("Continuing...");
                        }
                    }
                    break;
                case repeatDif:
                    while (!end)
                    {
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Input ({0}):", iteration);
                        Console.Write("Minimum: ");
                        minValue = int.Parse(Console.ReadLine());
                        Console.Write("Maximum: ");
                        maxValue = int.Parse(Console.ReadLine());
                        while (maxValue < minValue)
                        {
                            Console.WriteLine("WARNING: Please reassign the maximum value to be greater or equal to the minimum value.");
                            Console.Write("Maximum: ");
                            maxValue = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Action ({0}):", iteration);
                        Console.WriteLine("Randoming a number from " + minValue + " to " + maxValue + ":");
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Output ({0}):", iteration);
                        Console.WriteLine("Randomed: " + rand.Next(minValue, maxValue + 1));
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Do you want to continue? ({0} = continue / other = end)", cont);
                        Console.Write("Answer: ");
                        string answer = Console.ReadLine();
                        if (answer.ToLower() != cont)
                        {
                            end = true;
                            Console.WriteLine("The program will end...");
                        }
                        else
                        {
                            iteration++;
                            Console.WriteLine("Continuing...");
                        }
                    }
                    break;
                case notRepeat:
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Input ({0}):", iteration);
                        Console.Write("Minimum: ");
                        minValue = int.Parse(Console.ReadLine());
                        Console.Write("Maximum: ");
                        maxValue = int.Parse(Console.ReadLine());
                        while (maxValue < minValue)
                        {
                            Console.WriteLine("WARNING: Please reassign the maximum value to be greater or equal to the minimum value.");
                            Console.Write("Maximum: ");
                            maxValue = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Action ({0}):", iteration);
                        Console.WriteLine("Randoming a number from " + minValue + " to " + maxValue + ":");
                        Console.WriteLine("-----------------------");
                        Console.WriteLine("Output ({0}):", iteration);
                        Console.WriteLine("Randomed: " + rand.Next(minValue, maxValue + 1));
                    break;
            }           

            Console.WriteLine();
            Console.Write("Press any key to exit the application... ");
            Console.ReadKey();
        }
    }
}
