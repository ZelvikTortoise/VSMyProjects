namespace RandomNumberGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();            

            while (true)
            {
                Console.Write(random.Next());
            }
        }
    }
}