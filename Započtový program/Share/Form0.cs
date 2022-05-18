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
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
        }

        public void Reakce(string text)
        {
            MessageBox.Show(text, "Reakce", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void GoToForm1()
        {
            this.Hide();
            InstanceForm1.Show();
            InstanceForm1.RadioButton1Checked(false);       // Je nutné, aby to bylo až za Show()!!!
        }

        public void GoToForm2Úvod()
        {
            InstanceForm1.TimerČasEnabled(false);
            Form2Úvod snyanke = new Form2Úvod();
            snyanke.Closed += (s, args) => this.Close();
            this.Hide();
            snyanke.Show();
        }

        public void GoToForm3()
        {
            InstanceForm1.TimerČasEnabled(false);
            Form3 miny = new Form3();
            miny.Closed += (s, args) => this.Close();
            this.Hide();
            miny.Show();
        }

        public void GoToForm4()
        {
            InstanceForm1.TimerČasEnabled(false);
            Form4 tlačítka = new Form4();
            tlačítka.Closed += (s, args) => this.Close();
            this.Hide();
            tlačítka.Show();
        }


        public Form1 InstanceForm1
        {
            get;
            set;
        }

        // Tlačítka:
        // Form1
        private void buttonForm1_Click(object sender, EventArgs e)
        {
            Reakce("Jé, super. :3 Rád si s tebou popovídá. ♥ :* :3");
            GoToForm1();
        }

        // Form2Úvod
        private void buttonFormÚvod2_Click(object sender, EventArgs e)
        {
            Reakce("Honem, honem! Nyan cat už hladoví, pořádně ji nakrm. :D");
            GoToForm2Úvod();
        }

        // Form3
        private void buttonForm3_Click(object sender, EventArgs e)
        {
            Reakce("Doporučuju gumáky nebo aspoň dobré soustředění, pokud nechceš skončit zališená. ':D Good luck. :D ^^");
            GoToForm3();
        }

        // Form4
        private void buttonForm4_Click(object sender, EventArgs e)
        {
            Reakce("Ale pozor, jsou horší než malé děti... Jen tak je nechytneš. :D");
            GoToForm4();
        }
    }
}
