using System;

namespace Matice1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int min = int.MaxValue;
            int kolikMin = 0;
            int max = int.MinValue;
            int kolikMax = 0;
            int soucet = 0;

            int m = random.Next(2, 10); // od 2 do 9
            int n = random.Next(2, 10);

            int[,] matice = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matice[i, j] = random.Next(1, 99);    // od 1 do 98
                    Console.Write(matice[i, j] + " ");
                    soucet += matice[i, j];
                }
                Console.WriteLine();
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matice[i, j] < min)
                    {
                        min = matice[i, j];
                    }
                    else if (matice[i , j] > max)
                    {
                        max = matice[i, j];
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Minimum: " + min);
            Console.WriteLine("Indexy výskytu minima:");

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matice[i, j] == min)
                    {
                        Console.Write("[" + i + "; " + j + "]" + " ");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Maximum: " + max);
            Console.WriteLine("Indexy výskytu maxima:");

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matice[i, j] == max)
                    {
                        Console.Write("[" + i + "; " + j + "]" + " ");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Součet všech prvků v matici: " + soucet);

            Console.ReadKey();
        }
    }
}
