﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace QuestionRandomizer
{
    internal class Program
    {
        const int seed = 0;
        static Random random = seed <= 0 ? new Random() : new Random(seed);

        public static void RandomQuestions(TextReader reader, bool justNumbers)
        {
            ConsoleColor normalColor = Console.ForegroundColor;
            const ConsoleColor highlightColor = ConsoleColor.Red;   // highlight is red
            bool end = false;
            bool save = false;

            if (justNumbers)
            {
                List<int> questionNumbers = new List<int>();

                String? line;
                int currentAmount = 0;
                while ((line = reader.ReadLine()) != null)  // for Console.In, use Ctrl+Z
                {
                    line = line.Trim();
                    if (line.Length > 0)
                    {
                        currentAmount++;
                        questionNumbers.Add(currentAmount);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Questions were successfully loaded!");
                Console.Write("Press any key to start randoming... ");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("STARTING RANDOM GENERATION with {0} total questions:", currentAmount);

                int questionNumber;
                while (questionNumbers.Count > 1 && !end)
                {
                    questionNumber = PickQuestionNumber(questionNumbers);
                    Console.ForegroundColor = highlightColor;
                    Console.WriteLine("I randomly selected a question number {0}.", questionNumber);
                    Console.ForegroundColor = normalColor;
                    if (questionNumbers.Count > 1)
                        Console.WriteLine("There are still {0} other questions loaded.", questionNumbers.Count - 1);
                    else
                        Console.WriteLine("There is still 1 other question loaded.");
                    end = !AskIfNext(ref save, true);
                }
                if (questionNumbers.Count > 0 && !end)
                {
                    questionNumber = PickQuestionNumber(questionNumbers);
                    Console.ForegroundColor = highlightColor;
                    Console.WriteLine("I randomly selected a question number {0}.", questionNumber);
                    Console.ForegroundColor = normalColor;
                }
                // No saving implemented.
                if (questionNumbers.Count == 0)
                {
                    Console.WriteLine("All questions were asked...");
                }
                Console.WriteLine("Exiting...");
            }
            else
            {
                List<string> questions = new List<string>();

                String? line;
                int currentAmount = 0;
                while ((line = reader.ReadLine()) != null)  // for Console.In: use Ctrl+Z on Windows or Ctrl+D on Linux
                {
                    line = line.Trim();
                    if (line.Length > 0)
                    {
                        currentAmount++;
                        questions.Add(line);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Questions were successfully loaded!");
                Console.Write("Press any key to start randoming... ");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("STARTING RANDOM GENERATION with {0} total questions:", currentAmount);

                string question;
                while (questions.Count > 1 && !end)
                {
                    question = PickQuestion(questions);
                    Console.WriteLine("The randomly selected question is:");
                    Console.ForegroundColor = highlightColor;
                    Console.WriteLine(question);
                    Console.ForegroundColor = normalColor;
                    if (questions.Count > 1)
                        Console.WriteLine("There are still {0} other questions loaded.", questions.Count);
                    else
                        Console.WriteLine("There is still 1 other question loaded.");

                    end = !AskIfNext(ref save, false);
                }

                if (questions.Count > 0 && !end)
                {
                    question = PickQuestion(questions);
                    Console.WriteLine("The randomly selected question is:");
                    Console.ForegroundColor = highlightColor;
                    Console.WriteLine(question);
                    Console.ForegroundColor = normalColor;
                }
                if (questions.Count > 0 && save)
                {
                    Console.WriteLine();
                    SaveRemainingQuestions(questions);
                }
                if (questions.Count == 0)
                {
                    Console.WriteLine("All questions were asked...");
                }
                Console.WriteLine("Exiting...");
            }
        }

        /// <summary>
        /// Some output.
        /// </summary>
        /// <returns>Continue?</returns>
        private static bool AskIfNext(ref bool save, bool justNumbers)
        {
            if (justNumbers)
            {
                Console.WriteLine("Do you want to exit? (y / yes / a / ano = exit, otherwise = next question)");
            }
            else
            {
                Console.WriteLine("Do you want to exit? (y / yes / a / ano = exit without save, s / save / u / ulozit = exit with save, otherwise = next question)");
            }            
            Console.Write("Your answer: ");
            string? answer = Console.ReadLine();
            if (!string.IsNullOrEmpty(answer))
            {
                answer = answer.ToLower();
            }
            if (answer == "y" || answer == "yes" || answer == "a" || answer == "ano")
            {
                return false;
            }
            else if (!justNumbers && (answer == "s" || answer == "save" || answer == "u" || answer == "ulozit"))
            {
                save = true;
                return false;
            }
            else
            {
                Console.WriteLine("Continuing...");
                Console.WriteLine();
                return true;
            }
        }

        private static int PickQuestionNumber(List<int> questionNumbers)
        {
            int questionIndex = random.Next(0, questionNumbers.Count);
            int questionNumber = questionNumbers[questionIndex];

            questionNumbers.RemoveAt(questionIndex);

            return questionNumber;
        }

        private static string PickQuestion(List<string> questions)
        {
            int questionIndex = random.Next(0, questions.Count);
            string question = questions[questionIndex];

            questions.RemoveAt(questionIndex);

            return question;
        }

        private static void SaveRemainingQuestions(List<string> questions)
        {
            const string path = @"questions - save";
            const string suffix = @".txt";
            string pathOut = path + suffix;
            bool overwrite = false;
            bool answered = false;
            
            if (File.Exists(pathOut))
            {                
                while (!answered)
                {
                    Console.WriteLine("The file called \"{0}\" already exists.\nDo you want to overwrite it? (y / yes / a / ano = overwrite, n / no / ne = new file)", pathOut);
                    Console.Write("Your answer: ");
                    string? answer = Console.ReadLine();
                    if (!string.IsNullOrEmpty(answer))
                    {
                        answer = answer.ToLower();
                    }
                    if (answer == "y" || answer == "yes" || answer == "a" || answer == "ano")
                    {
                        overwrite = true;
                        Console.WriteLine("The file \"{0}\" will be overwritten.", pathOut);
                        answered = true;
                    }
                    else if (answer == "n" || answer == "no" || answer == "ne")
                    {
                        Console.WriteLine("Generating a new file...");
                        answered = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid answer. Repeating process...");
                        Console.WriteLine();
                    }
                }     
                
                if (!overwrite)
                {
                    // File name format: questions - save2.txt (etc.)
                    int k = 2;
                    do
                    {
                        pathOut = path + k.ToString() + suffix;
                        k++;
                    }
                    while (File.Exists(pathOut));
                }
                // else: overwrite == true ... same as generating a new file, i.e. pathOut stays the same.
            }

            // File doesn't exist or should be overwritten:            
            Console.WriteLine("Saving the remaining questions to a file called \"{0}\".", pathOut);
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                foreach (string question in questions)
                {
                    sw.WriteLine(question);
                }
            }
        }

        static void Main(string[] args)
        {
            ConsoleColor normalColor = Console.ForegroundColor;
            const ConsoleColor highlightColor = ConsoleColor.Red;   // highlight is red
            const string path = @"questions.txt";
            bool first = true;
            string? answer = "";

            do
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Console.WriteLine("Detected input: \"{0}\"", answer);
                    Console.WriteLine("Uknown option. Please try again.");
                    Console.WriteLine();
                }
                Console.WriteLine("Do you want to use Console.In or a file with path \"{0}\" as an input for questions to be randomed from?", path);
                Console.WriteLine("Type a number to select (1 - Console.In, 2 - file)");
                Console.Write("Your answer: ");
                answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer))
                {
                    answer = answer.Trim();
                }
            }
            while (answer != "1" && answer != "2");
            Console.WriteLine();

            switch (answer)
            {
                case "1":
                    Console.WriteLine("Write all your questions, one per line.");
                    Console.WriteLine("When you're done, press Ctrl+Z on Windows (Ctrl+D on Linux) and hit enter to finish the input.");
                    Console.WriteLine("Note: Empty lines are skipped.");
                    Console.WriteLine("Questions to be randomed from:");
                    RandomQuestions(Console.In, false);
                    break;
                case "2":
                    while (!File.Exists(path))
                    {
                        Console.WriteLine("Missing input file!");
                        Console.Write("Please create the input file ");
                        Console.ForegroundColor = highlightColor;
                        Console.Write(path);
                        Console.ForegroundColor = normalColor;
                        Console.WriteLine(" or move it to the directory with the .exe file.");
                        Console.Write("When you are ready, press any key to continue... ");
                        Console.ReadKey();
                        Console.WriteLine();
                        Console.WriteLine("Trying again...");
                        Console.WriteLine();
                    }
                    using (StreamReader sr = new StreamReader(path))
                    {
                        Console.WriteLine("Using an input file with the path {0} to pick questions to random from...", path);
                        RandomQuestions(sr, false);
                    }
                    break;
                default:
                    throw new Exception("Unknown option selected.\nFor programers: Either forbid this value of the variable " + nameof(answer) + " in the do-while above, or allow it as an case in the switch above.");
            }

            Console.WriteLine();
            Console.Write("Press any key to end the program... ");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}