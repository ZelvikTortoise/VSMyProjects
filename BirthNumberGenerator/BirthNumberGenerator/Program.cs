using System.CodeDom.Compiler;
using System.Text;
using System.IO;

namespace BirthNumberGenerator
{
    internal class Program
    {
        readonly static Random random = new();
        public static bool IsLeapYear(int year)
        {
            return year % 400 == 0 || year % 4 == 0 && year % 100 != 0;
        }

        public static string Generate(int minYear, int maxYear)
        {
            string YY, MM, DD, XXXX;
            bool isFemale = random.Next(0, 2) == 1; // 0 == M, 1 == F
            int year = random.Next(minYear, maxYear + 1) % 100; // last two digits only
            if (year < 10)
            {
                YY = "0" + year.ToString();
            }
            else
            {
                YY = year.ToString();
            }
            int month = random.Next(1, 13); // 1 – 12                        
            int maxDays, day;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    maxDays = 31;
                    break;
                case 2:
                    maxDays = 28;
                    if (IsLeapYear(year))
                    {
                        maxDays++;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    maxDays = 30;
                    break;
                default:
                    maxDays = -1;    // something is wrong
                    break;
            }
            if (isFemale)
            {
                month += 50;
            }
            if (month < 10)
            {
                MM = "0" + month.ToString();
            }
            else
            {
                MM = month.ToString();
            }
            day = random.Next(1, maxDays + 1);
            if (day < 10)
            {
                DD = "0" + day.ToString();
            }
            else
            {
                DD = day.ToString();
            }
            // int firstDigit, lastDigit;

            // int oddSum = (year + month + day) / 10;
            // int evenSum = year % 10 + month % 10 + day % 10;
            int threeDigitNumber = random.Next(1, 1_000);    // TODO: add checking that the whole birth number is always divisible by 11
            int remainder = ((year + month + day) / 10 + year % 10 + month % 10 + day % 10 + threeDigitNumber % 10 + (threeDigitNumber / 10) % 10 + (threeDigitNumber / 100) % 10) % 11;
            int lastDigit = remainder < 10 ? remainder : 0;
            XXXX = (threeDigitNumber * 10 + lastDigit).ToString();
            while (XXXX.Length < 4)
            {
                XXXX = "0" + XXXX;
            }

            return string.Concat(YY, MM, DD, '/', XXXX);
        }

        static void Main()
        {
            const int amount = 300;
            const char separator = '\n';
            const int minYear = 1940;
            const int maxYear = 2023;
            const string pathOut = "birthNumbers.txt";
            StringBuilder sb = new();
            string newNumber;

            if (amount > 0)
            {
                newNumber = Generate(minYear, maxYear);
                sb.Append(newNumber);
            }
            if (amount > 1)
            {
                for (int i = 0; i < amount - 1; i++)
                {
                    newNumber = Generate(minYear, maxYear);
                    sb.Append(separator);
                    sb.Append(newNumber);
                }
            }
            using StreamWriter sw = new(pathOut);
            sw.Write(sb.ToString());
        }
    }
}
