internal class Program
{
    // Improvement ideas:
    // TODO: Put in just N -> factor it and choose all pairs X, Y of same parity, where X < Y (or X = Y), then generate A^2 - B^2 for every one of them.
    // TODO: Put in just "a" (integer length of a side not being hypotenuse of a right trinagle) -> N = a^2 and repeat the previous steps, finish with generating all "a, b, c" (lengths of all sides of a right triangle).
    // TODO: Put in just "c" (integer length of a hypotenuse of a right triangle) -> find all possible integer "a, b", where c^2 = a^2 + b^2 by trying, generate all "a, b, c".
    // TODO: Put in just an odd integer N -> B = N div 2, A = B + 1, generate N = A^2 - B^2.
    // TODO: Put in just an even integer N divisible by 4 -> A = (N / 4) + 1, B = A - 2, generate N = A^2 - B^2.
    private static void Main(string[] args)
    {        
        string? s;
        int X = 0, Y = 0, N = 0, A, B;
        bool first = true, odd;
        /*/ TODO add this option
        Console.WriteLine("Put in two integer factors of same parity:");
        Console.Write("X = ");        
        if (!string.IsNullOrEmpty(s = Console.ReadLine()))
        {
            X = int.Parse(s);
        }        
        Console.Write("Y = ");
        if (!string.IsNullOrEmpty(s = Console.ReadLine()))
        {
            Y = int.Parse(s);
        }
        /*/
        Console.WriteLine("Input an integer either odd or divisible by 4:");
        do
        {
            if (!first)
            {
                Console.WriteLine();
                Console.WriteLine("{0} is not odd nor divisible by 4.", N);
                Console.WriteLine("Try again.");
                Console.WriteLine();
            }
            Console.Write("N = ");
            if (!string.IsNullOrEmpty(s = Console.ReadLine()))
            {
                N = int.Parse(s);
            }
            else
            {
                N = 0;
            }
            first = false;
        }
        while (!(N % 2 == 1 || N % 4 == 0));
        first = true;
        
        if (N % 2 == 1)
        {
            odd = true;
            Console.WriteLine("Input a factor of N:");
        }
        else
        {
            odd = false;
            Console.WriteLine("Input an even factor of N such that N divided by it is still even:");
        }
                
        do
        {
            if (!first)
            {
                Console.WriteLine();
                if (N % X != 0)
                {
                    Console.WriteLine("{0} is not a factor of N.", X);
                }
                else if (X % 2 != 0)
                {
                    Console.WriteLine("{0} is not even.", X);
                }
                else
                {
                    Console.WriteLine("{0}/{1} is not even.", N, X);
                }
                Console.WriteLine("Try again.");
                Console.WriteLine();
            }
            Console.Write("X = ");
            if (!string.IsNullOrEmpty(s = Console.ReadLine()))
            {
                X = int.Parse(s);
            }
            first = false;
        }
        while (!(N % X == 0 && (odd || (X % 2 == 0 && (N/X) % 2 == 0))));
        first = true;
        Y = N / X;
        Console.WriteLine("Y = {0}", Y);
        Console.WriteLine();

        if (X > Y)
        {
            int pom = X;
            X = Y;
            Y = pom;
        }
        
        if (int.TryParse(((X+Y)/2).ToString(), out A))
        {
            Console.WriteLine("RESULTS:");
            Console.WriteLine("A = {0}", A);
            B = A - X;
            Console.WriteLine("B = {0}", B);
            Console.WriteLine();
            N = X * Y;
            if (A < 0)  // Bracketing for negative a, b.
            {
                if (B < 0)
                {
                    Console.WriteLine("{0} = {1}*{2} = ({3})^2 - ({4})^2", N, X, Y, A, B);
                }
                else
                {
                    Console.WriteLine("{0} = {1}*{2} = ({3})^2 - {4}^2", N, X, Y, A, B);
                }
            }
            else if (B < 0)
            {
                Console.WriteLine("{0} = {1}*{2} = {3}^2 - ({4})^2", N, X, Y, A, B);
            }
            else
            {
                Console.WriteLine("{0} = {1}*{2} = {3}^2 - {4}^2", N, X, Y, A, B);
            }            
        }
        else
        {
            Console.WriteLine("{0} and {1} have different parity, therefore cannot be used.", X, Y);
        }

        Console.WriteLine();
        Console.Write("Press any key to exit the program... ");
        Console.ReadKey();
    }
}