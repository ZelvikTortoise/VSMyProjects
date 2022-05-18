using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        Random random = new Random();

        // Počítadlo, které dává pozor na to, kolikrát se opakovala podmínka.
        int i = 0;

        // Nutné manuálně (kvůli static):
        int btnChytniX = 121;       // buttonChytni.Location.X
        int btnChytniY = 179;       // button.Chytni.Location.Y
        int btnChytniXpre = 0;
        int btnChytniYpre = 0;

        Point startLocation = new Point(121, 179);  // Starting position of buttonChytni.
        const int náskok = 100;     // Jak nejblíže se může tlačítko buttonChytni objevit své původní pozici.
        int currentNáskok = 0;

        // Restart().
        public void Restart()
        {
            // buttonZmáčkni:
            buttonZmáčkni.Enabled = true;

            // buttonChytni:
            buttonChytni.Location = startLocation;
            buttonChytni.Enabled = true;
            buttonChytni.Visible = true;

            //textBoxKód:
            textBoxKód.Text = "";

            // buttonVýhra:
            buttonVýhra.Enabled = false;
            buttonVýhra.Visible = false;
        }

        // Po zadání výherního kódu.
        public void Výhra()
        {
            MessageBox.Show("Gratuluju, zvítězilas nad neposednými tlačítky. Pro potrvzení tvého vítězství si zapamatuj číslo, které je pátou odmocninou ze součtu třetí odmocniny z výherního kódu a pořadového čísla této třetí odmocniny z kódu mezi prvočísly. \nKdyž tuto číslici pak řekneš Lukovi, bude vědět, že jsi zvládla tlačítka porazit. Máš však pouze 1 pokus! \n\nToť vše, program se nyní vypne. :)", "Obdržení výherní číslice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        // Pokud se uživateli podaří zmáčknout některé z tlačítek, ukáže výherní kód.
        public void Výhrej()
        {
            MessageBox.Show("Wtf, jak? Vyhrálas, gratuluji. :D Vypínám se. (Zapiš si kód č.: " + Kód.ToString() + ")", "Výhra", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        // Vlastnost kód.
        public int Kód
        {
            get;
            set;
        }

        // Vlastnost CurrentMousePosition (pro přesnost pohybu tlačítka buttonChytni).
        public Point CurrentMousePosition
        {
            get;
            set;
        }

       
        // KeyDown (e.Handled = true).
        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        // Override metoda proti mačkání Enteru.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                MessageBox.Show("Podvádění je nepřípustné! Vypínám se.", "Tytyty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return true;
            }
            else
            {
                return false;
            }
        }

        // PreviewKeyDown().
        private void Form4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }



        // Událost MouseEnter() na tlačátko buttonZmáčkni.
        private void buttonZmáčkni_MouseEnter(object sender, EventArgs e)
        {
            buttonZmáčkni.Enabled = false;
        }

        // Událost MouseEnter() na tlačítko buttonChytni.
        private void buttonChytni_MouseEnter(object sender, EventArgs e)
        {
            i = 0;
            btnChytniXpre = btnChytniX;
            btnChytniYpre = btnChytniY;

            do
            {
                CurrentMousePosition = MousePosition;

                currentNáskok = (i + 1) * náskok;

                btnChytniX = random.Next(0, this.Width - buttonChytni.Width + 1);
                btnChytniY = random.Next(0, this.Height - buttonChytni.Height + 1);
                i++;
                if (i > 20)
                {
                    MessageBox.Show("Už musím, tak zase příště. Stále jsem nechycené! Hahahaha. \n~ tlačítko „Chytni mě“", "Výsměch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonChytni.Visible = false;
                    buttonChytni.Enabled = false;
                    break;
                }
            }
            while ((CurrentMousePosition.X >= btnChytniX && CurrentMousePosition.X <= (btnChytniX + buttonChytni.Width) && CurrentMousePosition.Y >= btnChytniY && CurrentMousePosition.Y <= (btnChytniY + buttonChytni.Height)) || (btnChytniX <= (btnChytniXpre + buttonChytni.Width + currentNáskok) && (btnChytniX + buttonChytni.Width + currentNáskok) >= btnChytniXpre &&  btnChytniY <= (btnChytniYpre+buttonChytni.Height+currentNáskok) && (btnChytniY + buttonChytni.Height + currentNáskok) >= btnChytniYpre));

            buttonChytni.Location = new Point(btnChytniX, btnChytniY);
        }

        // Shown().
        private void Form4_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(250, 100);
            Kód = 12167;
            MessageBox.Show("Nesnaž se podvádět pomocí klávesnice! Ve hře je pouze myš. :D", "Upozornění na pravidla", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // buttonZmáčkni_Click().
        private void buttonZmáčkni_Click(object sender, EventArgs e)
        {
            Výhrej();
        }

        // buttonChytni_Click().
        private void buttonChytni_Click(object sender, EventArgs e)
        {
            Výhrej();
        }

        // Restart().
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            Restart();
        }

        // textBoxKód_TextChanged()
        private void textBoxKód_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKód.Text == Kód.ToString())
            {
                buttonVýhra.Enabled = true;
                buttonVýhra.Visible = true;
                textBoxKód.Text = "";
            }
        }

        // Tlačítko Výhra.
        private void buttonVýhra_Click(object sender, EventArgs e)
        {
            Výhra();
        }
    }
}
