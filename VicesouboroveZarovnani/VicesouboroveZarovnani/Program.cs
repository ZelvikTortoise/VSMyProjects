using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("TestBenchClass")] // We want our TestBenchClass to see and be able to use the class Program that is in this namespace.
namespace VicesouboroveZarovnani
{
    class Program
    {
        /// <summary>
        /// Reads the text from all the input files (always printing one after other) and prints it justified in one output file.
        /// It can highlight spaces if commanded.
        /// </summary>
        /// <param name="str">MEANT FOR THE CONSOLE ARGUMENTS. (Rules: str[0] can be "--highlight-spaces" for highlighted spaces. Other strings are input file paths. The array's penultimate string is the output file's path. The last string in the array is the maximum wide of the text on each line (without highlight strings at the end of each line).)</param>
        /// <param name="lineWide">Parsed str[str.Length - 1] to int.</param>
        /// <param name="highlightSpaces">True if the str[0] == "--highlight-spaces", false otherwise.</param>
        public static void Bench(ref string[] str, int lineWide, bool highlightSpaces)
        {
            // The declaration space for the Main method:
            List<string> unusedWords = new List<string>();  // List of read words which haven't been written to the output file yet.
            string w;   // Last word read from the input file and added to the list.
            byte decider = 0;  // 0, 1 ... nothing special; 2 ... a paragraph; 3 ... the end of the last file; 4, 5 ... the end of a file but we want to continue (Note: 0 and 1 – 4 and 5 remember if the last read character was '\n' (1, 5) or not (0, 4).)
            int line = -1;   // Characters on a current line. (-1 because we don't want to count a space after the last word on a current line.)
            int d;  // The number of words on a current line -1.
            int x;  // The minimum number of additional spaces between each word on a current line. (The minimum number of spaces... x + 1.)
            int y;  // The number of gaps between two words one a current line that have 1 more ' ' than the others. (Note: For all the gaps: left gap >= right gap.)
            int wordCount;  // The number of words on a current line.
            byte pom = 0;    // A variable helping us with dividing a last line of a paragraph into two if needed.
            bool justHadPar = false;    // True if we've just printed a paragraph and haven't read any non-white characters yet. False otherwise.
            bool bonusLine = false;     // Tells the program to make a bonus line after every paragraph except the last one.
            string nextLine;    // What string do we want at the end of each line?
            char space;     // What character do we want to print between two words?

            // HIGHLIGHTED SPACES:
            if (highlightSpaces)
            {
                nextLine = "<-";
                space = '.';
            }
            // NOT HIGHLIGHTED SPACES:
            else
            {
                nextLine = "";
                space = ' ';
            }

            // The penultimate argument is the path to the output file.
            using (StreamWriter sw = new StreamWriter(str[str.Length - 2]))
            {
                // We want to start from the argument with index 1 if there's the highlight command. (Starting from the index 0 otherwise.)
                for (int round = (highlightSpaces ? 1 : 0); round <= str.Length - 3; round++)
                {
                    // If a path to a file doesn't exist or the file cannot be opened to read, then the program interprets it as an empty file.
                    try
                    {
                        using (StreamReader sr = new StreamReader(str[round]))
                        {
                            // So the while cycle below stops, we can read a next file but we still know if decider was 0 or 1.
                            if (decider >= 4)
                            {
                                decider -= 4;
                            }

                            // The main algorithm:
                            // While it's not the end of the file.
                            while (decider <= 2)
                            {
                                while (line < lineWide && decider <= 1)    // There will be maximum one extra word (over the lineWide).
                                {
                                    w = ReadWord(sr, ref decider, ref justHadPar, out bonusLine);
                                    if (bonusLine)
                                    {
                                        sw.WriteLine(nextLine);
                                    }
                                    if (w != "")
                                    {
                                        unusedWords.Add(w);
                                        line = line + w.Length + 1;
                                    }
                                    // We're reading the very last input file.
                                    if (round == str.Length - 3 && decider >= 4)
                                    {
                                        decider = 3;    // We want the program to end.
                                    }
                                }

                                if (pom > 0)
                                {
                                    decider = pom;
                                    pom = 0;
                                }
                                else if (line >= lineWide && decider >= 2)
                                {
                                    pom = decider;
                                    decider = 1;
                                }

                                switch (decider)
                                {
                                    // We have to make a paragraph.
                                    case 2:
                                    // The end of the file.
                                    case 3:
                                        // It's the end of the current paragraph.                                        
                                        wordCount = unusedWords.Count;
                                        if (wordCount == 1)
                                        {
                                            sw.WriteLine(unusedWords[0] + nextLine);
                                            unusedWords.Remove(unusedWords[0]);
                                        }
                                        else if (wordCount >= 2)
                                        {
                                            for (int i = 0; i <= wordCount - 2; i++)
                                            {
                                                sw.Write(unusedWords[0] + space);
                                                unusedWords.Remove(unusedWords[0]);
                                            }
                                            sw.WriteLine(unusedWords[0] + nextLine);
                                            unusedWords.Remove(unusedWords[0]);
                                        }

                                        // Now unused.Words.Count == 0.
                                        // The second condition is for the situation where the file ends after printing an empty paragraph. (There'd be an extra line.)
                                        if (decider == 2)
                                        {
                                            decider = 0;    // Ready to form a new paragraph.
                                            justHadPar = true;  // So we do not count any '\n' until we find a non-white character.
                                            line = -1;
                                        }
                                        break;
                                    case 4:
                                    case 5:
                                        // We just want this while cycle to end.
                                        break;
                                    // We have all the words needed for a full new line. (And one word over top.)
                                    default:    // 0 or 1
                                                // Creating a line in lineWide limit.
                                        if (line == lineWide)
                                        {
                                            wordCount = unusedWords.Count;
                                        }
                                        else
                                        {
                                            wordCount = unusedWords.Count - 1;
                                        }

                                        if (line == lineWide)
                                        {
                                            for (int i = 0; i < wordCount - 1; i++)
                                            {
                                                sw.Write(unusedWords[0] + space);
                                                unusedWords.Remove(unusedWords[0]);
                                            }
                                            sw.WriteLine(unusedWords[0] + nextLine);
                                            unusedWords.Remove(unusedWords[0]);
                                            // Reinitialization of the variable "line". (Note: There is no ' ' behind the last word on a line.)
                                            line = -1;
                                        }
                                        // wordCount == 0:
                                        // There is only one word in the list and this word's length is greater than the lineWide.
                                        // This word will be placed alone on one line, altough it break the lineWide limit.
                                        // wordCount == 1:
                                        // There are two words in the list but combined they don't fit on one line.
                                        else if (wordCount <= 1)    // line != lineWide is guaranteed
                                        {
                                            sw.WriteLine(unusedWords[0] + nextLine);
                                            unusedWords.Remove(unusedWords[0]);
                                            // Reinitialization of the variable "line". (Note: There is no ' ' behind the last word on a line.)
                                            if (wordCount == 0)
                                            {
                                                line = -1;
                                            }
                                            else
                                            {
                                                line = unusedWords[0].Length;
                                            }
                                        }
                                        else
                                        {
                                            line = line - unusedWords[wordCount].Length - 1;
                                            d = wordCount - 1;  // One less gap than words on the current line.
                                            x = (lineWide - line) / d;  // Division without the decimal part.
                                            y = (lineWide - line) % d;  // Modulo.

                                            // Writting in the output file.
                                            for (int i = 0; i <= wordCount - 2; i++)
                                            {
                                                sw.Write(unusedWords[0] + space);   // First word in the list.
                                                unusedWords.Remove(unusedWords[0]); // "The array's indexes will all be decreased by one."
                                                if (y > 0)
                                                {
                                                    sw.Write(space);
                                                    y--;
                                                }
                                                for (int j = x; j > 0; j--)
                                                {
                                                    sw.Write(space);
                                                }
                                            }
                                            sw.WriteLine(unusedWords[0] + nextLine);
                                            unusedWords.Remove(unusedWords[0]);

                                            // Reinitialization of the variable "line".
                                            line = unusedWords[0].Length;
                                        }
                                        if (pom > 0)
                                        {
                                            decider = 2;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    catch (IOException)
                    {
                        // If the last input file cannot be opened, read or the path is incorrect, we need a way to print our unusedWords.
                        // (switch (decider)) case 3:
                        if (round == str.Length - 3)
                        {
                            // It's the end of the current paragraph.                                        
                            wordCount = unusedWords.Count;
                            if (wordCount == 1)
                            {
                                sw.WriteLine(unusedWords[0] + nextLine);
                                unusedWords.Remove(unusedWords[0]);
                            }
                            else if (wordCount >= 2)
                            {
                                for (int i = 0; i <= wordCount - 2; i++)
                                {
                                    sw.Write(unusedWords[0] + space);
                                    unusedWords.Remove(unusedWords[0]);
                                }
                                sw.WriteLine(unusedWords[0] + nextLine);
                                unusedWords.Remove(unusedWords[0]);
                            }
                        }
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if c is a white character or the end of the file.
        /// Modifies the variable "p" which refers to "decider".
        ///     Those detect paragraphs and the end of the file.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool WhiteChar(int c, ref byte p, bool ignore)
        {
            switch (c)
            {
                case -1:
                    p += 4;  // The end of a file. Note: We still want to remember if the last read character was '\n'. Therefore we have two file end values – 4 and 5.
                    return true;    // Note: A cycle checking only non-white characters will end.                    
                case ' ':
                case '\t':
                    return true;
                case '\n':
                    if (!ignore)
                    {
                        p++;    // Potencially a paragraph.
                    }
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Reads a word from the opened input file and returns it as string.
        /// When a paragraph or the end of file is found, "p" is changed to matching value and the method returns "" if there is no word read.
        /// Ignores '\n' characters when ignorePar == true so the program doesn't print empty paragraphs.
        /// If a paragraph ended and another non-white character was found, sets the variable makeLine to true. Otherwise stays false.
        /// Note: Every word fits in our memory (guaranteed). Also, there are no '\r' characters in the input file.
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="p"></param>
        /// <param name="ignorePar"></param>
        /// <param name="makeLine"></param>
        /// <returns></returns>
        public static string ReadWord(StreamReader sr, ref byte p, ref bool ignorePar, out bool makeLine)  // p == 2 means a paragraph, p == 3 means the end of file.
        {
            StringBuilder word = new StringBuilder();
            int ch;
            makeLine = false;

            while (WhiteChar(ch = sr.Read(), ref p, ignorePar))
            {
                if (p >= 2) // So the cycle stops immediately after finding a paragraph or the end of the file.
                {
                    return "";
                }
            }

            // Will guarantee an empty line after every paragraph except the last one.
            if (ignorePar)
            {
                makeLine = true;
            }
            p = 0;  // After '\n' character was a non-white character. Therefore, there's no new paragraph.
            ignorePar = false;  // We found a non-white character.
            word.Append((char)ch);
            while (!WhiteChar(ch = sr.Read(), ref p, ignorePar))
            {
                word.Append((char)ch);
            }

            // Returning the read word.
            // Note: Can happen in case of the last character in the file being a non-white character.
            return word.ToString();
        }

        /// <summary>
        /// The main method. Justifies the text read from the input file in the output file.
        /// </summary>
        /// <param name="args">1st arg can be "--highlight-spaces" for highlighted spaces. Other arguments are input file paths. The penultimate argument is the output file's path. The last argument is the maximum wide of the text on each line (without highlight strings at the end of each line).</param>
        static void Main(string[] args)
        {
            // IMPORTANT CONSTANT VARIABLES.
            int lWide;   // Maximum wide of the text on every line in the output file.
            bool highlight = false;  // If it's true, the program replaces all ' ' with '.' and before every '\n' inserts "<-" in the output file. Otherwise normal.   

            // CHECKING THE FIRST ARGUMENT FOR THE HIGHLIGHT COMMAND.
            if (args.Length == 0)
            {
                Console.WriteLine("Argument Error");
            }
            else if (args[0] == "--highlight-spaces")
            {
                highlight = true;
            }

            // CHECKING THE ARGUMENT CORRECTNESS.
            // Args are at least 3, lineWide is parsable to int which has to be a pozitive number.
            if (args.Length < 3 || !(int.TryParse(args[args.Length - 1], out lWide)) || lWide <= 0)
            {
                if (args.Length != 0)
                {
                    Console.WriteLine("Argument Error");
                }                
            }
            // If there is a highlight command we have to have at least 4 arguments.
            else if (highlight && args.Length == 3)
            {
                Console.WriteLine("Argument Error");
            }
            /*else if (!args[args.Length - 2].EndsWith(".txt") && !args[args.Length - 2].EndsWith(".docx") && !args[args.Length - 2].EndsWith(".out")) // Recodex's reaction?
            {
                Console.WriteLine("Argument Error");
            }*/

            // THE MAIN ALGORITHM.
            else
            {
                Bench(ref args, lWide, highlight);
            }
        }
    }
}
