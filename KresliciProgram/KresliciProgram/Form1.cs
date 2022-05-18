using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KresliciProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(2000, 1000);   // Velikost bitmapy
            g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.Yellow, 0, 0, 2000, 1000);  // Pozadí
            pictureBox1.Image = bmp;
            pero = new Pen(Color.Red, 1);
            pero.StartCap = System.Drawing.Drawing2D.LineCap.Round; // Kruhový štětec
            pero.EndCap = System.Drawing.Drawing2D.LineCap.Round; // Kruhový štětec
        }

        Bitmap bmp;
        Graphics g;
        Pen pero;

        // Minule x a y.
        int minX = 0;
        int minY = 0;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                g.DrawLine(pero, minX, minY, e.X, e.Y);
            minX = e.X;
            minY = e.Y;
            pictureBox1.Refresh();
        }

        private void uložitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // bmp.Save(@"C:\a.bmp"); ... // FUJ!
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                bmp.Save(saveFileDialog1.FileName);
            System.Threading.Thread.Sleep(500);
        }

        private void otevřítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                g = Graphics.FromImage(bmp);
                pictureBox1.Image = bmp;
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap barvy = (Bitmap)pictureBox2.Image;
            pero.Color = barvy.GetPixel(e.X, e.Y);      // Získá barvu kliknutí

        }

        private void konecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pero.Width = trackBar1.Value;   // Změna tloušťky pomocí trackBaru
        }
    }
}
