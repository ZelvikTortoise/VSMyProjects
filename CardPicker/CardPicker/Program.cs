using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace CardPicker
{
    static class Program
    {
        // Testing: @"C:\Users\luk19\Desktop\Destiny Cards"
        public const string path = @"C:\Users\Klimecký\Desktop\Destiny Cards";
        private static List<Card> AllCards = new List<Card>();
        private static Stack<Card> Cards = new Stack<Card>();
        public static Random random = new Random();
        public static bool cardsChanged = true;

        public static FormCardPicker formCardPicker;
        public static FormCardOverview formCardOverview;

        public static int GetAllCardCount()
        {
            return AllCards.Count;
        }

        public static Card GetCardOverview(int index)
        {
            return AllCards[index];
        }

        public static Card GetCard()
        {
            return Cards.Pop();
        }

        private static void LoadAllCards()
        {
            string[] png;
            string[] jpg;
            AllCards = new List<Card>();

            png = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
            jpg = Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < png.Length; i++)
            {
                LoadCard(png[i]);
            }

            for (int i = 0; i < jpg.Length; i++)
            {
                LoadCard(jpg[i]);
            }

            cardsChanged = false;
        }

        private static void LoadCard(string imagePath)
        {
            Image image = Image.FromFile(imagePath);
            string name = (imagePath.Remove(0, path.Length + 1));
            name = name.Remove(name.Length - 4);
            Card card = new Card(image, name);

            AllCards.Add(card);
        }

        private static void FillStack()
        {
            Cards = new Stack<Card>();
            List<int> indeces = new List<int>();
            int notIndex, realIndex;

            for (int i = 0; i < AllCards.Count; i++)
            {
                indeces.Add(i);
            }

            for (int i = 0; i < AllCards.Count; i++)
            {
                notIndex = random.Next(0, indeces.Count);
                realIndex = indeces[notIndex];
                indeces.RemoveAt(notIndex);
                Cards.Push(AllCards[realIndex]);
            }
        }

        public static void Reset()
        {
            if (cardsChanged)
            {
                LoadAllCards();
            }
            
            FillStack();
        }

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Reset();
            formCardPicker = new FormCardPicker();
            formCardOverview = new FormCardOverview();

            Application.Run(formCardPicker);
        }
    }
}
