namespace AlgebraLokalizaceKorenu
{
    internal class Program
    {
        static double Function1(double x)
        {            
            return x * x - Math.Pow(2, x);  // First, x^2 is greater than 2^x (until intersection).
        }

        static double Function2(double x)
        {
            return Math.Cos(x) - x; // First, cos(x) is greater than x (until intersection).            
        }   

        static void Main(string[] args)
        {
            const double maxIterations = 100_000_000;
            const double accuracy = 1e-15;
            double a, b;
            double x = 0, y;
            int numberOfIterations = 0;
            bool found = true;
            const double defaultA = -1000;
            const double defaultB = 1000;


            Console.WriteLine("Set your starting boundaries:");
            Console.Write("a = ");
            if (!double.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Your input was impossible to parse to double...");
                Console.WriteLine("Using default value...");
                a = defaultA;  // Default value if not parsed.
            }
                
            Console.Write("b = ");
            if (!double.TryParse(Console.ReadLine(), out b))
            {
                Console.WriteLine("Your input was impossible to parse to double...");
                Console.WriteLine("Using default value...");
                b = defaultB;    // Default value if not parsed.
            }
            
            if (a > b)
            {
                Console.WriteLine("Swapping the boundaries, so a > b.");
                double temp = a;
                a = b;
                b = temp;
            }
            
            if (a == b)
            {
                x = a;
                y = Function2(x);   // Insert the function here !!! (#1)
                if (Math.Abs(y) < accuracy)
                    Console.WriteLine("Found x = {0} with accuracy of {1} in 1 iteration.", x, accuracy);
                else
                    Console.WriteLine("The number x = a = b isn't a solution.");

                Console.ReadKey();
                return;
            }
            Console.Write("Press any key to continue... ");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Calculating with:");
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            Console.WriteLine();

            do
            {
                if (numberOfIterations >= maxIterations)
                {
                    found = false;
                    break;
                }
                numberOfIterations++;
                x = (a + b) / 2d;   // We're choosing the midpoint of the interval (a, b).
                y = Function2(x);   // The selection of the function !!! (#2)

                if (y < 0)  // We need to adjust the right boundary.
                {
                    b = x;
                }
                else if (y > 0)   // We need to adjust the left boundary.
                {
                    a = x;
                }                
            }
            while (Math.Abs(y) >= accuracy);

            if (found)
                Console.WriteLine("Found x = {0} with accuracy of {1} in {2} iterations.", x, accuracy, numberOfIterations);
            else
                Console.WriteLine("The x wasn't found even after {0} iterations of interval-halving. Check your starting boundaries.", maxIterations);
            Console.ReadKey();
        }
    }
}