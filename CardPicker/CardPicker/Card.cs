using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CardPicker
{
    class Card
    {
        public Image Image { private set; get; }
        public string Text { private set; get; }

        public void ChangeText(Card card, string text)
        {
            card.Text = text;
        }

        public void ChangeImage(Card card, Image image)
        {
            card.Image = image;
        }

        public Card(string text)
        {
            this.Image = null;
            this.Text = text;
        }

        public Card(Image image, string text)
        {
            this.Image = image;
            this.Text = text;
        }
    }
}
