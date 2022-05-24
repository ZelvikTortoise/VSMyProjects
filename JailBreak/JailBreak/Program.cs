using System;
using System.Collections.Generic;
using System.IO;

namespace JailBreak
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathIn = "JailBreak.txt";

            // Load words:
            List<string> words = new List<string>();
            Console.WriteLine("Welcome. This is the JailBreak program.");
            Console.WriteLine("Loading in the word bank...");
            using (StreamReader sr = new StreamReader(pathIn))
            {
                
                string word;                
                
                while ((word = sr.ReadLine()) != null)
                {
                    words.Add(word);
                }
            }
            Console.WriteLine("Loading finished.");
            Console.WriteLine();

            List<char> unknown = new List<char> { ' ', '_', '?' };
            bool finished = false;
            string clues, answer;
            List<string> possibleWords;
            int index;
            Random random = new Random();
            ConsoleKeyInfo key;
            int remaining;
            while (!finished)
            {
                Console.WriteLine("Mark unknown letters with '_', ' ' or '?'.");
                Console.WriteLine("What are your clues?");
                Console.Write("Clues: ");
                clues = Console.ReadLine().ToUpper();
                remaining = 4 - clues.Length;
                for (int i = 0; i < remaining; i++)
                {
                    clues += unknown[0];
                }               
                Console.WriteLine();
                Console.Write("Possible answer: ");
                // SELECTING A SUGGESTION
                // Possible words:
                possibleWords = new List<string>();
                for (int i = 0; i < words.Count; i++)
                {
                    possibleWords.Add(words[i]);
                }
                // Checking clue:
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = possibleWords.Count - 1; j >= 0; j--)
                    {
                        if (!unknown.Contains(clues[i]) && clues[i] != possibleWords[j][i])
                        {
                            possibleWords.RemoveAt(j);
                        }
                    }
                }
                // Randoming possible suggestion:
                if (possibleWords.Count == 0)
                {
                    Console.WriteLine("No such words exist in the word bank, sorry.");
                }
                else
                {
                    index = random.Next(0, possibleWords.Count);
                    Console.Write(possibleWords[index]);
                    possibleWords.RemoveAt(index);
                    // More suggestions:
                    key = Console.ReadKey();
                    while (key.Key == ConsoleKey.Spacebar)
                    {
                        Console.WriteLine();
                        if (possibleWords.Count == 0)
                        {
                            Console.Write("No more possible words with given clues, sorry.");
                            break;
                        }
                        else
                        {
                            Console.Write("Another option: ");
                            index = random.Next(0, possibleWords.Count);
                            Console.Write(possibleWords[index]);
                            possibleWords.RemoveAt(index);
                            key = Console.ReadKey();
                        }
                    }
                }              
                Console.WriteLine();
                Console.WriteLine("---------------------");
                Console.WriteLine();
                Console.WriteLine("Is it finished? (y = yes, otherwise = no)");
                Console.Write("Your answer: ");
                answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "y")
                {
                    finished = true;
                    Console.WriteLine("Finishing...");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Continuing...");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Thank you for trying our JailBreak program. ;)");
            Console.Write("Press any key to exit the program... ");
            Console.ReadKey();
        }
    }
}
