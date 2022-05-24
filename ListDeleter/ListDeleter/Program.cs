using System;
using System.IO;

namespace ListDeleter
{    
    class Program
    {
        static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        static void SortList(string pathIn, string pathOut)
        {
            using (StreamReader sr = new StreamReader(pathIn))
            using (StreamWriter sw = new StreamWriter(pathOut))
            {                
                string[] rows = sr.ReadToEnd().Split("\r\n");
                int[,] matrix = new int[rows.Length, 3];
                string[] temp;
                const int printFlag = 1;

                for (int i = 0; i < rows.Length; i++)
                {
                    temp = rows[i].Split(' ');
                    matrix[i, 0] = int.Parse(temp[0]);
                    matrix[i, 1] = i;
                    matrix[i, 2] = printFlag;   // Print.
                }

                bool changed;
                for (int i = 0; i < rows.Length - 1; i++)
                {
                    changed = false;
                    for (int j = 0; j < rows.Length - 1 - i; j++)
                    {
                        if (matrix[j, 0] > matrix[j + 1, 0])
                        {                            
                            Swap(ref matrix[j, 0], ref matrix[j + 1, 0]);
                            Swap(ref matrix[j, 1], ref matrix[j + 1, 1]);
                            Swap(ref matrix[j, 2], ref matrix[j + 1, 2]);
                            changed = true;
                        }
                        else if (matrix[j, 0] == matrix[j + 1, 0])
                        {
                            matrix[j + 1, 2] = printFlag - 1;   // Do not print.                            
                        }
                    }
                    if (!changed)
                    {
                        break;
                    }
                }

                for (int i = 0; i < rows.Length - 1; i++)
                {
                    if (matrix[i, 2] == printFlag)
                    {
                        sw.WriteLine(rows[matrix[i, 1]]);
                    }                    
                }
                if (matrix[rows.Length - 1, 2] == printFlag)
                {
                    sw.Write(rows[matrix[rows.Length - 1, 1]]);
                }
            }
        }

        static void Main(string[] args)
        {
            string mainPath;
            Console.WriteLine("Welcome to SYSTEM LIST FILTER made by Luk");
            Console.WriteLine("Do you want to set your own path? (yes = y / yes / a / ano; no = otherwise)");
            Console.Write("Your answer: ");
            string answer = Console.ReadLine();
            Console.WriteLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes" || answer.ToLower() == "a" || answer.ToLower() == "ano")
            {
                bool first = true;
                bool slashNote;
                do
                {
                    if (!first)
                    {
                        Console.WriteLine("Returning to setting your own path...");
                        Console.WriteLine();                        
                    }
                    first = false;
                    slashNote = false;

                    Console.WriteLine("Paste in or type in your path.");
                    Console.WriteLine("Example path: C:\\Users\\lolhe\\Desktop\\");
                    Console.Write("Main path: ");
                    mainPath = Console.ReadLine();
                    Console.WriteLine();
                    if (!mainPath.EndsWith("\\"))
                    {
                        mainPath = string.Concat(mainPath, "\\");
                        slashNote = true;
                    }                    
                    Console.WriteLine("Loaded path: {0}", mainPath);
                    if (slashNote)
                    {
                        Console.WriteLine("Note: The path always ends with a \\.");
                    }                    
                    Console.WriteLine("Do you want to use this path? (yes = y / yes / a / ano; no = otherwise)");
                    Console.Write("Your answer: ");
                    answer = Console.ReadLine();
                }
                while (answer.ToLower() != "y" && answer.ToLower() != "yes" && answer.ToLower() != "a" && answer.ToLower() != "ano");
                Console.WriteLine("Path loaded.");
            }
            else
            {
                Console.WriteLine("Using the default path.");
                Console.WriteLine("Note: Default path is set to \"C:\\Users\\lolhe\\Desktop\\\".");
                mainPath = @"C:\Users\lolhe\Desktop\";
            }
            Console.WriteLine();
            Console.WriteLine("Make sure the input files are in the directory with the loaded path.");
            Console.WriteLine("Also, name them excatly as follows:");
            Console.WriteLine(" 1) source.txt = the source file with the whole list of names");
            Console.WriteLine(" 2) delete.txt = the file with lines to delete from the source file");
            Console.WriteLine();
            Console.WriteLine("When the program is finished, it will create three more files in the same directory:");
            Console.WriteLine(" 1) sourceSorted.txt = sorted source.txt in ascending order");
            Console.WriteLine(" 2) deleteSorted.txt = sorted delete.txt in ascending order");
            Console.WriteLine(" 3) result.txt = filtered list of names from source.txt not containing names from delete.txt");
            Console.WriteLine();
            Console.WriteLine("When everything is prepared, the program will begin.");
            Console.WriteLine("Check the path, the names of the input files and if everything is fine, there should be no error.");
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("The program is working...");

            string pathInSource = mainPath + "source.txt";
            string pathInSourceSorted = mainPath + "sourceSorted.txt";
            string pathInDelete = mainPath + "delete.txt";
            string pathInDeleteSorted = mainPath + "deleteSorted.txt";
            string pathOut = mainPath + "result.txt";

            SortList(pathInSource, pathInSourceSorted);
            SortList(pathInDelete, pathInDeleteSorted);

            using (StreamReader srSource = new StreamReader(pathInSourceSorted))
            using (StreamReader srDelete = new StreamReader(pathInDeleteSorted))
            using (StreamWriter sw = new StreamWriter(pathOut))
            {


                String rowSource, rowDelete = srDelete.ReadLine();
                if (rowDelete == null)
                {
                    if ((rowSource = srSource.ReadLine()) != null)
                    {
                        sw.Write(rowSource);
                        while ((rowSource = srSource.ReadLine()) != null)
                        {
                            sw.WriteLine();
                            sw.Write(rowSource);
                        }
                    }                    
                }
                else
                {
                    bool firstDone = false;
                    while ((rowSource = srSource.ReadLine()) != null)
                    {
                        if (rowSource != rowDelete)
                        {
                            if (firstDone)
                            {
                                sw.WriteLine();
                                sw.Write(rowSource);
                            }
                            else
                            {
                                firstDone = true;
                                sw.Write(rowSource);
                            }                            
                        }
                        else
                        {
                            rowDelete = srDelete.ReadLine();
                            if (rowDelete == null)
                            {
                                while ((rowSource = srSource.ReadLine()) != null)
                                {
                                    if (firstDone)
                                    {
                                        sw.WriteLine();
                                        sw.Write(rowSource);
                                    }
                                    else
                                    {
                                        firstDone = true;
                                        sw.Write(rowSource);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("The program finished successfully.");
            Console.WriteLine();
            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}
