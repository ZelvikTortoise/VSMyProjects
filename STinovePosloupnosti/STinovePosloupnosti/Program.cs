using System.IO.Pipes;
using System.Reflection.Metadata;

namespace STinovePosloupnosti
{
    class Sequence
    {
        public int S { get; }
        private int XSMinus1 { set; get; } = defaultXSminus1;
        const int defaultS = 7;
        const int defaultXSminus1 = 0;

        public static void ShowFormulaAn()
        {
            Console.WriteLine("ans = {[(n - sStriped) mod (s - sStriped) + sStriped] * 10^[(n - 1) div (s - 1) + 1]} div s,\nwhere sStriped = 1 + [(s - 1) div 10].");
        }

        public long GetAn(int n)
        {
            int sStriped = 1 + (S - 1) / 10;
            int rem = (n - sStriped) % (S - sStriped) + sStriped;
            int exp = (n - 1) / (S - 1) + 1;
            long divident = rem;

            checked
            {
                while (exp > 0)
                {
                    divident *= 10;
                    exp--;
                }
            }

            return (divident / S);
        }

        public void WriteAnFromUntil(int from, int to)
        {
            if (from < 1)
            {
                Console.WriteLine("The value of the parameter {0} has to be greater or equal to 1.", nameof(from));
                return;
            }
            else if (to < from)
            {
                Console.WriteLine("The value of the parameter {0} has to be greater or equal to the value of the parameter {1}.", nameof(to), nameof(from));
                return;
            }

            long an;
            Console.WriteLine("This is a sequence of 1/{0} for n = {1}, ..., {2}:", this.S, from, to);
            for (int i = from; i <= to; i++)
            {
                Console.Write("a{0} = ", i);

                try
                {
                    an = GetAn(i);
                    Console.WriteLine(an);
                }
                catch (ArithmeticException)
                {
                    Console.WriteLine("OVERFLOW");
                }
            }
            Console.WriteLine();
        }

        public void WriteAnUntil(int until)
        {
            if (until < 1)
            {
                Console.WriteLine("The value of the parameter {0} has to be greater or equal to 1.", nameof(until));
                return;
            }

            WriteAnFromUntil(1, until);
        }

        public static void ShowFormulaBn()
        {
            Console.WriteLine("bns = {[(n - sStriped) mod (s - sStriped) + sStriped] * 10^[(n - 1) div (s - 1) + x_(s - 1)]} div s,\nwhere sStriped = 1 + [(s - 1) div 10]\nand x_(s - 1) is the number of digits of the number s - 1.");
        }

        public long GetBn(int n)
        {
            // Initializing XSMinus1 if not done yet.
            if (this.XSMinus1 == 0)
            {
                int rest = this.S - 1;
                while (rest > 0)
                {
                    XSMinus1++;
                    rest /= 10;
                }
            }

            int sStriped = 1 + (S - 1) / 10;
            int rem = (n - sStriped) % (S - sStriped) + sStriped;
            int exp = (n - 1) / (S - 1) + this.XSMinus1;
            long divident = rem;

            checked
            {
                while (exp > 0)
                {
                    divident *= 10;
                    exp--;
                }
            }

            return (divident / S);
        }

        public void WriteBnFromUntil(int from, int to)
        {
            if (from < 1)
            {
                Console.WriteLine("The value of the parameter {0} has to be greater or equal to 1.", nameof(from));
                return;
            }
            else if (to < from)
            {
                Console.WriteLine("The value of the parameter {0} has to be greater or equal to the value of the parameter {1}.", nameof(to), nameof(from));
                return;
            }

            long bn;
            Console.WriteLine("This is a sequence of 1/{0} for n = {1}, ..., {2}:", this.S, from, to);
            for (int i = from; i <= to; i++)
            {
                Console.Write("b{0} = ", i);

                try
                {
                    bn = GetBn(i);
                    Console.WriteLine(bn);
                }
                catch (ArithmeticException)
                {
                    Console.WriteLine("OVERFLOW");
                }
            }
            Console.WriteLine();
        }

        public void WriteBnUntil(int until)
        {
            if (until < 1)
            {
                Console.WriteLine("The value of the parameter {0} has to be greater or equal to 1.", nameof(until));
                return;
            }

            WriteBnFromUntil(1, until);
        }

        public Sequence(int s)
        {
            // S-tinová posloupnost, kde s > 1.
            if (s <= 1)
            {
                Console.WriteLine("ERROR. The parameter {0} has to be integer greater than 1.", nameof(s));
                Console.WriteLine("Setting {0} = {1}.", nameof(s), defaultS);
                this.S = defaultS;
                //throw new ArgumentException("The parameter s has to be integer greater than 1.");
            }
            else
            {
                this.S = s;
            }            
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("These are the formulas:");
            Sequence.ShowFormulaAn();
            Console.WriteLine();
            Sequence.ShowFormulaBn();
            Console.WriteLine();

            bool end = false;
            string answer = null;

            do
            {
                if (answer != null)
                {
                    Console.WriteLine("Continuing...");
                    Console.WriteLine();
                }
                    
                Console.Write("Type s for sequence of 1/s: ");
                int s = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Sequence seq = new Sequence(s);
                seq.WriteAnUntil(30);
                seq.WriteBnUntil(30);
                Console.Write("Submit n/N to exit the prgoram, otherwise continue: ");
                answer = Console.ReadLine();
                if (answer.ToLower() == "n")
                    end = true;
            }
            while (!end);

            Console.ReadKey();

            // Lemma: For every natural s > 1 holds that {an} = {bn}, where n goes from 1 to infinity.
            // Theorem: For every natural s > 1 the sequence {an}, where n goes from 1 to infinity, is non-decreasing.
            // Theorem: For every natural s > 1 the sequence {bn}, where n goes from 1 to infinity, is increasing and has no 0 as a member.
            // Consequence: The function Pbs : N -> N is an injection.
            // Question: Is the range of values of the function Pbs equal to the set of natural numbers for s tends to infinity?
        }
    }
}