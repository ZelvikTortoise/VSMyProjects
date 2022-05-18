using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace TapCodeConverter
{
    class TapCode
    {
        public List<int> Code;
        public List<int> Spaces;

        public TapCode()
        {
            this.Code = new List<int>();
            this.Spaces = new List<int>();
        }
    }
    class Program
    {
        private static char GetLetter(int value)
        {
            if (value <= 25)
            {
                if (value >= 11)
                {
                    value++;
                }

                return (char)(value + 'A' - 1);
            }
            else if (value == 36)
            {
                return 'K';
            }
            else if (value <= 30)
            {
                return (char)(value - 26 + '1');
            }
            else if (value <= 34)
            {
                return (char)(value - 26 + '1');
            }
            else if (value == 35)
            {
                return '0';
            }
            else
            {
                throw new ArgumentException("Invalid code.\nSource Tap Code table: https://ktane.timwi.de/HTML/%C3%9Cbermodule.html.");
            }

        }
        public static TapCode ParseTapCode(TextReader reader)
        {
            TapCode tapCode = new TapCode();
            int cint;
            bool prepared = false;
            int temp;

            while ((cint = reader.Read()) != -1)
            {
                char c = (char)cint;
                if (c >= '1' && c <= '6')
                {
                    tapCode.Code.Add((int)c - '1' + 1);
                    prepared = false;
                }
                else if(c == '/')
                {
                    if (prepared)
                    {
                        temp = tapCode.Code.Count - 1;
                        if (temp >= 0 && temp % 2 == 1)
                        {
                            tapCode.Spaces.Add(temp);
                        }                        
                    }
                    prepared = !prepared;
                }
                else
                {
                    prepared = false;
                }
            }


            tapCode.Spaces.Add(-1);

            return tapCode;
        }
        public static string ConvertFromTapCodeToText(TapCode tapCode)
        {
            StringBuilder sb = new StringBuilder();
            int value = 0;
            int j = 0;

            for (int i = 0; i < tapCode.Code.Count; i++)
            {            
                if (i % 2 == 0)
                {
                    if (tapCode.Code[i] == 6)
                    {
                        value += 30;
                    }
                    else
                    {
                        value += 5 * (tapCode.Code[i] - 1);
                    }
                }
                else
                {
                    if (tapCode.Code[i] == 6)
                    {
                        if (value == 30)
                        {
                            value = 36;
                        }
                        else
                        {
                            value = (value / 5 + 1) + 25;
                        }
                    }
                    else
                    {
                        value += tapCode.Code[i];
                    }

                    sb.Append(GetLetter(value));
                    value = 0;

                    if (tapCode.Spaces[j] == i)
                    {
                        sb.Append(' ');
                        j++;
                    }
                }
            }

            return sb.ToString();
        }

        static void Main(string[] args)
        {            
            Console.WriteLine("Input the file path: ");
            Console.Write("Path: ");    // C:\\Users\\luk19\\OneDrive\\Desktop\\TapCode.txt
            string path = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Your text:");
            using (StreamReader sr = new StreamReader(path))
            {
                TapCode tap = ParseTapCode(sr);
                string text = ConvertFromTapCodeToText(tap);
                Console.WriteLine(text);
            }

            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
