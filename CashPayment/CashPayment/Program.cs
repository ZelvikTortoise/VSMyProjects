using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CashPayment
{
    class Reader  // Edited class Ctecka from RNDr. Holan PhD.
    {
        private static char? lastChar = null;

        public static int ReadNum()
        {
            int z;
            bool negative = false;

            if (lastChar.HasValue)
                z = (char)lastChar;
            else
                z = Console.Read();

            while ((z < '0') || (z > '9'))
            {
                if (z == '-')
                    negative = true;    // Another z should break the cycle.

                z = Console.Read();
            }

            int x = 0;
            while ((z >= '0') && (z <= '9'))
            {
                x = 10 * x + z - '0';
                z = Console.Read();
            }

            if (z != -1)
                lastChar = (char)z;
            else
                lastChar = null;

            if (negative)
                return -x;
            else
                return x;
        }
    }

    class Till
    {
        readonly int[] CashTypes;
        readonly TextWriter Writer;
        const char separator = ' ';
        const int invalid = -1;

        public void BeginSearch(int toBePaid)
        {
            List<int> payments = new List<int>();
            Search(toBePaid, payments);
        }

        private void ServePossiblePayments(List<int> payments)
        {
            for (int i = 0; i < payments.Count - 1; i++)
            {
                this.Writer.Write(payments[i].ToString() + separator);
            }

            this.Writer.WriteLine(payments[payments.Count - 1]);
        }

        private int GetCashTypeIndex(int amount)
        {
            for (int i = 0; i < this.CashTypes.Length; i++)
                if (CashTypes[i] == amount)
                    return i;

            // Not found.
            return invalid;
        }

        private void Search(int toBePaid, List<int> payments)
        {
            int start = 0;
            if (payments.Count > 0)
                start = GetCashTypeIndex(payments[payments.Count - 1]);

            for (int i = start; i < CashTypes.Length; i++)
            {
                if (CashTypes[i] < toBePaid)
                {
                    payments.Add(CashTypes[i]);

                    Search(toBePaid - CashTypes[i], payments);

                    payments.RemoveAt(payments.Count - 1);
                }
                else if (CashTypes[i] == toBePaid)
                {
                    payments.Add(CashTypes[i]);
                    ServePossiblePayments(payments);    // Output.
                    payments.RemoveAt(payments.Count - 1);
                }
            }
        }

        public Till(int[] types, TextWriter writer)
        {
            this.CashTypes = types;
            this.Writer = writer;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int typeCount = Reader.ReadNum();
            int[] types = new int[typeCount];
            for (int i = 0; i < typeCount; i++)
            {
                types[i] = Reader.ReadNum();
            }
            int neededMoney = Reader.ReadNum();

            Console.WriteLine();
            Console.WriteLine("Possible payments:");
            Till till = new Till(types, Console.Out);
            till.BeginSearch(neededMoney);

            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
