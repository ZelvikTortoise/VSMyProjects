using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeriodCalculator
{
    class Program
    {
        static int FindNumberOfDigits(ulong number)
        {
            int digits = 1;
            ulong powerOfTen = 10;
            while (number / powerOfTen != 0)
            {
                digits++;
                powerOfTen *= 10;
            }

            return digits;
        }

        static void FillWithZeroes(ref StringBuilder sb, int howMany)
        {
            for (int i = 1; i <= howMany; i++)
            {
                sb.Append("0");
            }
        }

        static void FillWithZeroes(ref StringBuilder sb, ulong howMany)
        {
            for (ulong i = 1; i <= howMany; i++)
            {
                sb.Append("0");
            }
        }

        static void AddToBuffer(ref StringBuilder buffer, int num, TextWriter writer)
        {
            const int maxBufferSize = 1024;
            if (buffer.Length >= maxBufferSize)
            {
                writer.Write(buffer.ToString());
                buffer.Clear();
            }

            buffer.Append(num);
        }

        static void AddToBuffer(ref StringBuilder buffer, char character, TextWriter writer)
        {
            const int maxBufferSize = 1024;
            if (buffer.Length >= maxBufferSize)
            {
                writer.Write(buffer.ToString());
                buffer.Clear();
            }

            buffer.Append(character);
        }

        static void AddToBuffer(ref StringBuilder buffer, string text, TextWriter writer)
        {
            const int maxBufferSize = 1024;
            if (buffer.Length >= maxBufferSize)
            {
                writer.Write(buffer.ToString());
                buffer.Clear();
            }

            buffer.Append(text);
        }

        static void PrintBuffer(ref StringBuilder buffer, TextWriter writer)
        {
            if (buffer.Length > 0)
            {
                writer.Write(buffer.ToString());
            }
        }

        static void DivideOneOverDivisorAndPrint(ulong divisor, TextWriter writer)
        {
            writer.WriteLine("--------------------");
            writer.Write("Result: ");
            bool periodic, onePeriod;
            int? periodLength;

            if (divisor == 1)
            {
                periodic = false;
                onePeriod = false;
                periodLength = null;

                writer.Write("1.0");
                return;
            }
            else if (divisor == 10 || divisor == 100 || divisor == 1_000 || divisor == 10_000 || divisor == 100_000 || divisor == 1_000_000 || divisor == 10_000_000 || divisor == 100_000_000 || divisor == 1_000_000_000)
            {
                periodic = false;
                onePeriod = false;
                periodLength = null;

                ulong rest = divisor;
                StringBuilder sb2 = new StringBuilder();
                AddToBuffer(ref sb2, "0.", writer);
                while (rest / 10 > 1)
                {
                    rest = rest / 10;
                    AddToBuffer(ref sb2, '0', writer);
                }

                AddToBuffer(ref sb2, '1', writer);

                PrintBuffer(ref sb2, writer);
                return;            
            }

            int digits = FindNumberOfDigits(divisor);
            bool[] nonZeroremainders = new bool[divisor - 1];   // All set to false by default.
            {
                int index = 1;
                for (int i = 1; i < digits; i++)
                {
                    index *= 10;
                }
                nonZeroremainders[index - 1] = true;
            }
            ulong currentDividor = 10;
            StringBuilder sb = new StringBuilder();
            ulong remaining = divisor - 1;

            periodic = true;
            onePeriod = true;
            periodLength = 0;
            AddToBuffer(ref sb, "0.", writer);


            bool counting = false, done = false;
            while (currentDividor / divisor == 0)
            {
                currentDividor *= 10;
                remaining--;
                AddToBuffer(ref sb, '0', writer);
            }

            {
                int resultDigit;
                ulong remainder;
                while (remaining > 0)
                {
                    resultDigit = (int)(currentDividor / divisor);
                    remainder = currentDividor % divisor;

                    AddToBuffer(ref sb, resultDigit, writer);
                    remaining--;

                    if (remainder != 0)
                    {
                        // We see this non-zero remainder for the first time.
                        if (!nonZeroremainders[remainder - 1])
                        {
                            nonZeroremainders[remainder - 1] = true;
                            if (counting && !done)
                            {
                                done = true;
                                counting = false;
                            }
                        }
                        // We've seen this non-zero remainder already.
                        else
                        {
                            if (remaining != 0)
                            {
                                onePeriod = false;
                            }

                            if (!counting && !done)
                            {
                                counting = true;
                                periodLength++;
                                nonZeroremainders[remainder - 1] = false;
                            }
                            else if (counting && !done)
                            {
                                periodLength++;
                                nonZeroremainders[remainder - 1] = false;
                            }
                        }
                        currentDividor = remainder * 10;
                    }
                    else
                    {
                        FillWithZeroes(ref sb, remaining);
                        periodic = false;
                        onePeriod = false;
                        periodLength = null;
                        break;
                    }
                }
            }

            PrintBuffer(ref sb, writer);
            writer.WriteLine();
            writer.WriteLine("--------------------");
            writer.WriteLine("Periodic: " + (periodic ? "True." : "False."));
            if (divisor != 1)
            {
                writer.WriteLine("Has {0}-digit period: {1}", divisor - 1, (onePeriod ? "True." : "False."));
                if (periodic && !onePeriod)
                {
                    writer.WriteLine("Length of the period: " + periodLength);
                }
            }
        }
        
        static string DivideOneOverDivisor(int divisor, out bool periodic, out bool onePeriod, out int? periodLength)
        {
            if (divisor == 1)
            {
                periodic = false;
                onePeriod = false;
                periodLength = null;

                return "1.0";
            }
            else if (divisor == 10 || divisor == 100 || divisor == 1_000 || divisor == 10_000 || divisor == 100_000 || divisor == 1_000_000 || divisor == 10_000_000 || divisor == 100_000_000 || divisor == 1_000_000_000)
            {
                periodic = false;
                onePeriod = false;
                periodLength = null;

                int rest = divisor;
                StringBuilder sb2 = new StringBuilder();
                sb2.Append("0.");
                while (rest / 10 > 1)
                {
                    rest = rest / 10;
                    sb2.Append("0");
                }

                sb2.Append("1");

                return sb2.ToString();
            }

            int digits = FindNumberOfDigits((ulong)divisor);
            bool[] nonZeroremainders = new bool[divisor - 1];   // All set to false by default.
            {
                int index = 1;
                for (int i = 1; i < digits; i++)
                {
                    index *= 10;
                }
                nonZeroremainders[index - 1] = true;
            }
            int currentDividor = 10;
            StringBuilder sb = new StringBuilder();
            int remaining = divisor - 1;


            periodic = true;
            onePeriod = true;
            periodLength = 0;
            sb.Append("0.");


            bool counting = false, done = false;
            while (currentDividor / divisor == 0)
            {
                currentDividor *= 10;
                remaining--;
                sb.Append("0");
            }

            {
                int resultDigit, remainder;
                while (remaining > 0)
                {
                    resultDigit = currentDividor / divisor;
                    remainder = currentDividor % divisor;

                    sb.Append(resultDigit.ToString());
                    remaining--;

                    if (remainder != 0)
                    {
                        // We see this non-zero remainder for the first time.
                        if (!nonZeroremainders[remainder - 1])
                        {
                            nonZeroremainders[remainder - 1] = true;
                            if (counting && !done)
                            {
                                done = true;
                                counting = false;
                            }                            
                        }
                        // We've seen this non-zero remainder already.
                        else
                        {
                            if (remaining != 0)
                            {
                                onePeriod = false;
                            }
                                
                            if (!counting && !done)
                            {
                                counting = true;
                                periodLength++;
                                nonZeroremainders[remainder - 1] = false;
                            }
                            else if (counting && !done)
                            {
                                periodLength++;
                                nonZeroremainders[remainder - 1] = false;
                            }
                        }
                        currentDividor = remainder * 10;
                    }
                    else
                    {
                        FillWithZeroes(ref sb, remaining);
                        periodic = false;
                        onePeriod = false;
                        periodLength = null;
                        break;
                    }
                }
            }

            return sb.ToString();
        }

        static void AlternativeMain(string[] args)
        {
            const int startValue = 1_00_000_000;
            const int maxNum = 1_00_000_030;
            int count = 1;            
            bool periodic, onePeriod;

            for (int divisor = startValue; divisor <= maxNum; divisor++)
            {
                DivideOneOverDivisor(divisor, out periodic, out onePeriod, out int? periodLenth);
                if (periodic && onePeriod)
                {
                    Console.WriteLine($"{count}. {divisor}");
                    count++;
                }
            }

            Console.WriteLine();
            Console.Write("Press any key to exit the program... ");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            /*/
            if (true)
            {
                Console.WriteLine("00100704934541792547834843907351460221550855991943605236656596173212487411883182275931520644511581067472306143".Length);
                return;
            }
            /*/
            /*/
            if (true)
            {
                AlternativeMain(args);
                return;
            }
            /*/
            bool exit = false;
            while (!exit)
            {
                ulong divisor = 0;    // For the compiler.
                while (true)
                {
                    Console.Write("Insert a divisor: ");
                    if (!ulong.TryParse(Console.ReadLine(), out divisor))
                    {
                        Console.WriteLine("Unexpected input. Please insert a positive integer.");
                        Console.WriteLine();
                        continue;
                    }

                    if (divisor <= 0)
                    {
                        Console.WriteLine("Invalid number. Please insert a positive integer.");
                        Console.WriteLine();
                        continue;
                    }
                    else
                    {
                        // Inserted divisor is valid.
                        break;
                    }
                }

                Console.WriteLine("Calculating 1/{0}...", divisor);
                bool valid = false;
                // bool periodic, onePeriod;
                // int? periodLength;
                DivideOneOverDivisorAndPrint(divisor, Console.Out);
                Console.WriteLine();
                Console.WriteLine("Do you want to continue with another divisor? (y/n)");
                Console.Write("Your answer: ");
                while (!valid)
                {
                    if (!char.TryParse(Console.ReadLine(), out char answer))
                    {
                        Console.WriteLine("Invalid answer. Please type only 'y' to continue or 'n' to exit.");
                        Console.Write("Your answer: ");
                        continue;
                    }
                
                    switch(answer)
                    {
                        case 'y':
                        case 'Y':
                            valid = true;
                            exit = false;
                            break;
                        case 'n':
                        case 'N':
                            valid = true;
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Uknown answer. Please type only 'y' to continue or 'n' to exit.");
                            Console.Write("Your answer: ");
                            break;
                    }
                }

                if (!exit)
                {
                    Console.WriteLine("Continuing...");
                    Console.WriteLine("-------------");
                }
                Console.WriteLine();
            }

            Console.Write("Press any key to exit the program... ");
            Console.ReadKey();
        }
    }
}

// TODO:
// Zkusit přepracovat StringBuilder na lepší bufferování, ať můžeme pracovat i s většími čísly.
// Otestovat, jak velké může být pomocné booleovské pole, aby nenastala OutOfMemoryException.
// Poslední pád: 2,9 GB -> 3,0 GB.
