using System.ComponentModel.Design;

namespace RomeBoardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Rome game;
            string player1Name, player2Name;
            Console.WriteLine("ŘÍM");
            Console.WriteLine("Vítejte ve hře Řím.");
            Console.WriteLine("Jedná se o hru pro 2 hráče.");
            Console.Write("Chcete si nastavit jména pro každého z hráčů? (a = ano, n = ne)");
            if ((Console.ReadLine() ?? string.Empty).ToLower() == "a")
            {
                Console.Write("Zadejte jméno hráče 1: ");
                player1Name = Console.ReadLine()!;
                Console.Write("Zadejte jméno hráče 2: ");
                player2Name = Console.ReadLine()!;
                game = new Rome(player1Name, player2Name);
            }
            else
            {
                Console.WriteLine("Hra použije výchozí jména pro hráče.");
                game = new Rome();
            }
            
        }
    }
}
