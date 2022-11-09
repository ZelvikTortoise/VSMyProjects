namespace AlgebraLokalizaceKorenu
{
    internal class Program
    {
        static double Function(double x)
        {
            return x * x - Math.Pow(2, x);  // First, x^2 is greater than 2^x (until intersection).
        }

        static void Main(string[] args)
        {
            const double accuracy = 1e-15;
            double a = -1, b = 0;
            double x, y;
            int numberOfIterations = 0;

            do
            {
                numberOfIterations++;
                x = (a + b) / 2d;   // We're choosing the midpoint of the interval (a, b).
                y = Function(x);

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

            Console.WriteLine("Found x = {0} with accuracy of {1} in {2} iterations.", x, accuracy, numberOfIterations);
            Console.ReadKey();
        }
    }
}