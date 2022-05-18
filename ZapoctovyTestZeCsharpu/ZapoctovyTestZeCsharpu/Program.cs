using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace ZapoctovyTestZeCsharpu
{
    class Pattern
    {
        public string Name { get; private set; }
        public string[] Shapes { get; private set; }

        /// <summary>
        /// Checks if we know a shape of selected pattern which is the same the word wanted.
        /// This method is CASE INSENSITIVE!
        /// </summary>
        /// <param name="wanted"></param>
        /// <returns>True if wanted is contained in pattern.Shapes, false otherwise.</returns>
        public bool ContainsShape(string wanted)
        {
            wanted = wanted.ToUpper();
            for (int i = 0; i < this.Shapes.Length; i++)
                if (wanted == this.Shapes[i].ToUpper())
                    return true;
            return false;
        }

        public Pattern(string name, List<string> foundShapes)
        {
            this.Name = name;
            this.Shapes = foundShapes.ToArray();
        }
    }

    public enum OnlineOptions { OnlineOnly, Offline, Default }

    class ParsingClass
    {
        const string fileCacheSuffix = ".txt";
        const string fileWebSuffix = ".html";
        const string cacheFileNamesPath = "fileNames" + fileCacheSuffix;
        //const string tempPath = "temp.txt";
        const string timeoutError = "Jiný dotaz z Vaší internetové adresy je ještě vyhodnocován";
        const string webPath = @"C:\Users\luk19\OneDrive\Desktop\downloaded_files\";

        // Implements the given method. (Note: I though we'd be given string, not a file...)
        static bool TrySearchOnWeb(string word, out string html)
        {
            html = null;
            /*if (!DownloadFile(word, tempPath))
            {
                html = null;
                return false;
            }*/
            
            try
            {
                using (StreamReader sr = new StreamReader(webPath + word + fileWebSuffix))
                {
                    html = sr.ReadToEnd();  // It's fine.

                    /*if (html.Contains(timeoutError))    // Server too busy.
                        throw new Exception("Timeout message recieved from the server, no cache was returned.");*/
                }
            }            
            catch
            {
                html = null;
                return false;
            }

            return true;
        }

        /*// Given by the teacher.  // RIP method due to the stupid server.
        static bool DownloadFile(string word, string filename)
        {
            if (word == null || filename == null)
            {
                return false;
            }
            string baseUrl = "http://prirucka.ujc.cas.cz/?slovo= ";
            string url = baseUrl + word;
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, filename);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }*/

        internal void ClearCache()
        {
            using (StreamReader sr = new StreamReader(cacheFileNamesPath))
            {
                string path;
                while ((path = sr.ReadLine()) != null)
                {
                    File.Delete(path);
                }
            }

            File.Delete(cacheFileNamesPath);

            using (StreamWriter sw = new StreamWriter(cacheFileNamesPath))
            {
                // Empty file.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patternName"></param>
        /// <param name="shapes"></param>
        /// <returns>True if we found it in cache, false if not.</returns>
        public bool UseOnlyCache(string patternName, List<string> shapes)
        {
            // Actual cachePath.
            string cachePath = null;

            // We don't want to catch possible exception! (possible bug)
            using (StreamReader sr = new StreamReader(cacheFileNamesPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.EndsWith(patternName + fileCacheSuffix))
                    {
                        cachePath = line;
                        break;
                    }                        
                }
            }

            if (cachePath != null)   // Cache  file has been found.
            {
                using (StreamReader sr = new StreamReader(cachePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // if (!shapes.Contains(line))  // Should work without it. (That's how we've built it.)
                        shapes.Add(line);
                    }
                }

                return true;
            }
            else
            {
                shapes.Add(patternName);    // Contains only its name since it wasn't found in the cache.
                return false;
            }
        }

        private void AddToCache(string patternName, List<string> futureCache)
        {
            string pathOut = patternName + fileCacheSuffix;
            using (StreamWriter sw = new StreamWriter(pathOut)) // Creating new file regardless.    // TODO: Saving in UTF-8
            {
                for (int i = 0; i < futureCache.Count; i++)
                    sw.WriteLine(futureCache[i]);
            }

            using (StreamWriter sw = File.AppendText(cacheFileNamesPath))
            {
                sw.WriteLine(pathOut);  // Saving the cacheFilePath.
            }
        }

        private void ParseHtmlForShapes(string htmlText, out List<string> foundShapes)
        {
            char[] htmlTagSeparators = new char[] { '<', '>' };
            const string notShape1 = "pád";
            const string notShape2 = "osoba";
            const string notShape3 = "příčestí";
            const string notShape4 = "přechodník";
            const string notShape5 = "verbální";
            const string notShape6 = "způsob";
            const string wordToAvoid = "span";
            const string bait = "x";
            const string row = "tr";
            const string cell = "td";
            const string singular = "jednotné číslo";
            const string plural = "množné číslo";
            const string refer = "a href";
            const string notValid = "/";
            const string notValid2 = "sup";


            const char shapeSeparator = ',';


            foundShapes = new List<string>();

            string[] htmlParts = htmlText.Split(htmlTagSeparators);
            for (int i = 1; i < htmlParts.Length; i++)
            {
                if (!htmlParts[i].StartsWith(wordToAvoid) && !htmlParts[i].EndsWith(notShape1) && htmlParts[i] != bait && !htmlParts[i].StartsWith(row) && !htmlParts[i].EndsWith(row) && !htmlParts[i].StartsWith(cell) && !htmlParts[i].EndsWith(cell))
                {
                    if (!htmlParts[i].StartsWith("\n") && htmlParts[i] != singular && htmlParts[i] != plural && !htmlParts[i].EndsWith(notShape2) && htmlParts[i] != "" && !htmlParts[i].StartsWith(notShape3) && !htmlParts[i].StartsWith(notShape4) && !htmlParts[i].StartsWith(notShape5))
                    {
                        if (!htmlParts[i].EndsWith(notShape6) && !htmlParts[i].StartsWith(refer) && !htmlParts[i].StartsWith(notValid) && !htmlParts[i].StartsWith(notValid2) && htmlParts[i] != "L" && htmlParts[i] != "/L" && htmlParts[i] != "nobr" && htmlParts[i] != "/nobr")
                        {
                            if (!htmlParts[i].StartsWith("1") && !htmlParts[i].StartsWith("2") && !htmlParts[i].StartsWith("3") && !htmlParts[i].StartsWith("4") && !htmlParts[i].StartsWith("5") && !htmlParts[i].StartsWith("6") && !htmlParts[i].StartsWith("7") && !htmlParts[i].StartsWith("8") && !htmlParts[i].StartsWith("9") && !htmlParts[i].StartsWith("10"))
                            {
                                // We've found a shape of our pattern!
                                if (htmlParts[i].Contains(shapeSeparator.ToString()))
                                {
                                    string[] shapes = htmlParts[i].Split(shapeSeparator);
                                    if (!foundShapes.Contains(shapes[0]))
                                        foundShapes.Add(shapes[0]);
                                    if (!foundShapes.Contains(shapes[1].TrimStart(' ')))
                                        foundShapes.Add(shapes[1].TrimStart(' '));
                                }
                                else if (!foundShapes.Contains(htmlParts[i]))
                                    foundShapes.Add(htmlParts[i]);
                            }

                        }
                    }
                }
            }

        }

        public void OnlineOnly(string patternName, List<string> shapes)
        {
            const string tableStartIdentificator = "<table";
            const string tableEndIdentificator = "</table>";
            const string acknowledgement = "množné číslo";    // We have correct table according to the task specification.
            const string notOnePattern = "Zadaný řetězec není samostatným heslem, vyskytuje se ale v následujících heslech:";

            if (TrySearchOnWeb(patternName, out string html))
            {
                // Not what we want.
                if (html.Contains(notOnePattern))
                {
                    shapes.Add(patternName);
                    return;
                }

                while (true)    // Will process all tables in the html document.
                {
                    if (html.Contains(tableStartIdentificator))
                    {

                        int indexStart = html.IndexOf(tableStartIdentificator) + tableStartIdentificator.Length;
                        int indexEnd = html.IndexOf(tableEndIdentificator) - 1;

                        string htmlPart = html.Substring(indexStart, indexEnd - indexStart + 1);
                        if (htmlPart.Contains(acknowledgement))
                        {
                            ParseHtmlForShapes(htmlPart, out List<string> shapesCache);
                            AddToCache(patternName, shapesCache);
                            foreach (string item in shapesCache)
                                shapes.Add(item);
                            if (!shapes.Contains(patternName))
                                shapes.Add(patternName);
                            break;
                        }
                        else
                        {
                            html = html.Remove(indexStart - tableStartIdentificator.Length, htmlPart.Length + tableEndIdentificator.Length + 1);
                        }
                    }
                    else
                    {
                        shapes.Add(patternName);
                        return;
                    }
                }
            }
            else
            {
                shapes.Add(patternName);
                return;
            }
        }

        public void BestEffort(string patternName, List<string> shapes)
        {
            if (!UseOnlyCache(patternName, shapes))
            {
                shapes.Clear(); // Contains only its name, we want to remove it and check online.
                OnlineOnly(patternName, shapes);    // If it's not found, it will add only its name back again.
            }                
        }
    }

    class ControlClass
    {
        static readonly char[] wordSeparators = new char[] { ' ', '\t', '.', ',', '!', '?', '"' };
        /// <summary>
        /// Will go through the patterns and print them, their shapes and their count + total count using writer.
        /// </summary>
        /// <param name="writer">Eg. Console.</param>
        /// <param name="patterns">Array of patterns with assigned valid Shapes property.</param>
        public void RunDry(TextWriter writer, Pattern[] patterns)
        {
            const string padding = "   ";
            const string endOfHeader = ": ";

            StringBuilder sb = new StringBuilder();
            int totalCount = 0;

            for (int i = 0; i < patterns.Length; i++)
            {
                sb.Clear();
                sb.Append(patterns[i].Name);
                sb.Append(endOfHeader);
                writer.WriteLine(sb.ToString());
                for (int j = 0; j < patterns[i].Shapes.Length; j++)
                {
                    sb.Clear();
                    sb.Append(padding);
                    sb.Append(j + 1);
                    sb.Append(endOfHeader);
                    sb.Append(patterns[i].Shapes[j]);
                    writer.WriteLine(sb.ToString());
                }               
                sb.Clear();
                sb.Append("Amount of found shapes: ");
                sb.Append(patterns[i].Shapes.Length);
                writer.WriteLine(sb.ToString());
                totalCount += patterns[i].Shapes.Length;
            }
            sb.Clear();
            sb.Append("Total amount of all found shapes: ");
            sb.Append(totalCount);
            writer.WriteLine(sb.ToString());
        }

        private bool IsArrayTrueOnly(bool[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i])
                    return false;
            }
            return true;
        }

        public void CheckFile(TextWriter writer, Pattern[] patterns, string pathIn)
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathIn))
                {
                    bool[] patternOnLine;
                    string line;
                    string[] words;
                    int rowNumber = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        words = line.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);
                        patternOnLine = new bool[patterns.Length];   // We know it's initialized with clear values (false).
                        for (int i = 0; i < words.Length; i++)
                        {
                            for (int j = 0; j < patterns.Length; j++)
                            {
                                if (patterns[j].ContainsShape(words[i]))
                                    patternOnLine[j] = true;    // Pattern with index j was found on this line.
                                // No break. (We might have two shapes of the same pattern as two different patterns from user.)
                            }
                        }
                        if (IsArrayTrueOnly(patternOnLine))

                            writer.WriteLine(rowNumber + " " + line);
                                                    
                        rowNumber++;
                    }
                }
            }
            catch (IOException)
            {
                writer.WriteLine("An error has occured in reading from the file.");
                writer.WriteLine("Check if the path is correct. It's possible the file is already open, locked or doesn't exist.");
            }
        }

        // According to the options look into cache / download a website + parse it
        internal void RunParse(OnlineOptions cacheOpts, string patternName, out List<string> shapes)
        {
            ParsingClass parsing = new ParsingClass();
            shapes = new List<string>();

            switch (cacheOpts)
            {
                case OnlineOptions.Default:
                    parsing.BestEffort(patternName, shapes);
                    break;
                case OnlineOptions.Offline:
                    parsing.UseOnlyCache(patternName, shapes);
                    break;
                case OnlineOptions.OnlineOnly:
                    parsing.OnlineOnly(patternName, shapes);
                    break;
                default:
                    throw new Exception("Unhandled OnlineOptions option found while trying to find all shapes of patterns.");
            }
        }

        internal void ClearCacheFirst()
        {
            ParsingClass cacheManager = new ParsingClass();
            cacheManager.ClearCache();
        }
    }

    class Program
    {
        public const string commandStart = "--";

        /// <summary>
        /// See below.
        /// </summary>
        /// <param name="args">Args from command line.</param>
        /// <param name="dry">Ignoring input file path?</param>
        /// <param name="cacheOpts">Online only, offline or the best effort way?</param>
        /// <returns>Index of first non-option value in args.</returns>
        static int SetRequestedOptions(string[] args, out bool dry, out bool verbose, out bool clearCache, out OnlineOptions cacheOpts)
        {
            // Default options.
            dry = false;
            verbose = false;
            clearCache = false;
            cacheOpts = OnlineOptions.Default;

            int i = 0;
            while (args[i].StartsWith(commandStart))
            {
                switch(args[i])
                {
                    case "--dry":
                        dry = true;
                        break;
                    case "--verbose":
                        verbose = true;
                        break;
                    case "--offline":
                        cacheOpts = OnlineOptions.Offline;
                        break;
                    case "--force-online":
                        cacheOpts = OnlineOptions.OnlineOnly;
                        break;
                    case "--clear-cache":
                        clearCache = true;
                        break;
                    default:
                        throw new Exception($"Option {args[i]} is not handled. Please, do not use it while running the program.");
                }
                i++;
            }

            return i;
        }

        static void HelpUser(TextWriter writer)
        {
            writer.WriteLine("Usage: Program.exe [options] pattern1 pattern2 ... patternN file");       // No commas in this solution!
        }

        // Main method:
        static void Main(string[] args)
        {
            // General settings.
            Console.OutputEncoding = Encoding.UTF8;
            TextReader reader = Console.In;
            TextWriter writer = Console.Out;

            if (args.Length <= 0)
            {
                HelpUser(writer);
                writer.WriteLine();
                writer.Write("Press any key to continue... ");
                Console.ReadKey();
                return;
            }

            // Instance of ControlClass.
            ControlClass control = new ControlClass();

            // Options.
            bool dry;
            bool verbose;
            bool clearCache;
            OnlineOptions cacheOptions;

            // Getting requested options and index of first pattern in args.
            int firstPatternIndex = SetRequestedOptions(args, out dry, out verbose, out clearCache, out cacheOptions);

            // Pattern list.
            Pattern[] patterns = new Pattern[args.Length - 1 - firstPatternIndex];
            if (patterns.Length <= 0)
            {
                HelpUser(writer);
                return;
            }

            // Clearing cache first if requested.
            if (clearCache)
                control.ClearCacheFirst();

            // Creating pattern.Shapes, pattern and then adding it to patterns in input order.
            List<string> shapes;
            for (int i = firstPatternIndex; i < args.Length - 1; i++)
            {
                control.RunParse(cacheOptions, args[i], out shapes);
                Pattern pattern = new Pattern(args[i], shapes);
                patterns[i - firstPatternIndex] = pattern;  // offset
            }

            // Assigning path of input file.
            string pathIn = args[args.Length - 1];

            // Running correct program.
            if (verbose)
            {
                // Running RunVerbose(...)
                control.RunDry(writer, patterns);
                writer.WriteLine(); // Separating two processes.
                control.CheckFile(writer, patterns, pathIn);
            }
            else if (dry)
                // Running RunDry(...).
                control.RunDry(writer, patterns);
            else    // Default program.
                // Running CheckFile(...).
                control.CheckFile(writer, patterns, pathIn);

            writer.WriteLine();
            writer.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
