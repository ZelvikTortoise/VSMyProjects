using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ŠárkaKvíz
{
    public partial class FormUrčitáOsobnost : Form
    {
        public FormUrčitáOsobnost()
        {
            InitializeComponent();
            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormUrčitáOsobnost_FormClosed);
        }

        // Pomocné proměnné:
        static int indexVěcCorrect;
        static int indexVěcWrong;
        static int indexButton;
        static int indexButtonCorrect;

        // Closed.
        private void FormUrčitáOsobnost_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Vytvoří seznamy při prvním zobrazením dané instance třídy FormUrčitáOsobnost.
        public void VytvořSeznamy()
        {
            FormWelcoming.seznamCorrectVěc.Clear();
            FormWelcoming.seznamWrongVěc.Clear();

            foreach (Věc věc in FormWelcoming.seznamVěcPůvodní)
            {
                if (věc.Result)
                {
                    FormWelcoming.seznamCorrectVěc.Add(věc);
                }
                else
                {
                    FormWelcoming.seznamWrongVěc.Add(věc);
                }
            }
        }

        public void Rozlosuj()
        {
            if (FormWelcoming.seznamCorrectVěc.Count >= 1 && FormWelcoming.seznamWrongVěc.Count >= 2)
            {
                indexVěcCorrect = FormWelcoming.random.Next(0, FormWelcoming.seznamCorrectVěc.Count);       // Correct věc.
                indexVěcWrong = FormWelcoming.random.Next(0, FormWelcoming.seznamWrongVěc.Count);       // Wrong věc 1.
                indexButton = FormWelcoming.random.Next(1, 4);      // Correct button.

                switch (indexButton)
                {
                    case 1:
                        indexButtonCorrect = 1;
                        buttonVěc1.BackgroundImage = FormWelcoming.seznamCorrectVěc[indexVěcCorrect].Image;     // Correct.
                        buttonVěc2.BackgroundImage = FormWelcoming.seznamWrongVěc[indexVěcWrong].Image;
                        indexButton = 3;
                        break;
                    case 2:
                        indexButtonCorrect = 2;
                        buttonVěc1.BackgroundImage = FormWelcoming.seznamWrongVěc[indexVěcWrong].Image;
                        buttonVěc2.BackgroundImage = FormWelcoming.seznamCorrectVěc[indexVěcCorrect].Image;     // Correct.
                        indexButton = 3;
                        break;
                    case 3:
                        indexButtonCorrect = 3;
                        buttonVěc1.BackgroundImage = FormWelcoming.seznamWrongVěc[indexVěcWrong].Image;
                        indexButton = 2;
                        buttonVěc3.BackgroundImage = FormWelcoming.seznamCorrectVěc[indexVěcCorrect].Image;     // Correct.
                        break;
                }

                FormWelcoming.seznamCorrectVěc.Remove(FormWelcoming.seznamCorrectVěc[indexVěcCorrect]);
                FormWelcoming.seznamWrongVěc.Remove(FormWelcoming.seznamWrongVěc[indexVěcWrong]);

                indexVěcWrong = FormWelcoming.random.Next(0, FormWelcoming.seznamWrongVěc.Count);       // Wrong věc 2.
                if (indexButton == 3)
                {
                    buttonVěc3.BackgroundImage = FormWelcoming.seznamWrongVěc[indexVěcWrong].Image;
                }
                else
                {
                    buttonVěc2.BackgroundImage = FormWelcoming.seznamWrongVěc[indexVěcWrong].Image;
                }
                buttonVěc1.Enabled = true;
                buttonVěc2.Enabled = true;
                buttonVěc3.Enabled = true;
                buttonZpět.Enabled = true;
                buttonKonec.Enabled = true;
            }
            else
            {
                MessageBox.Show("Jejda... Už nám došly otázky na tuto osobnost. \n Vyberte si jinou, anebo absolvujte stejné otázky ještě jednou!", "Hlášení", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Hide();
                (new FormOsobnosti()).Show();
            }
        }

        // Zakazuje kliknutí na obrázek s osobností.
        private void buttonOsobnost_Click(object sender, EventArgs e)
        {
            if (buttonOsobnost.Enabled) return;
        }


        public void Správně()
        {
            FormWelcoming.skóre += FormWelcoming.přidat;
            FormWelcoming.správných++;
            buttonVěc1.Enabled = false;
            buttonVěc2.Enabled = false;
            buttonVěc3.Enabled = false;
            buttonZpět.Enabled = false;
            buttonKonec.Enabled = false;
            timer.Enabled = true;
            labelResult.Text = "Správně!";
            labelResult.ForeColor = Color.Green;
            labelResult.Visible = true;
            labelSkóre.Text = FormWelcoming.skóre.ToString();
        }

        public void Špatně()
        {
            FormWelcoming.skóre -= FormWelcoming.odebrat;
            FormWelcoming.špatných++;
            buttonVěc1.Enabled = false;
            buttonVěc2.Enabled = false;
            buttonVěc3.Enabled = false;
            buttonZpět.Enabled = false;
            buttonKonec.Enabled = false;
            timer.Enabled = true;
            labelResult.Text = "Špatně...";
            labelResult.ForeColor = Color.DarkRed;
            labelResult.Visible = true;
            labelSkóre.Text = FormWelcoming.skóre.ToString();
        }

        // Timer. (Interval je 1000 ms.)
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            labelResult.Visible = false;
            Rozlosuj();
        }

        // Tlačítka věcí:
        // Věc 1:
        private void buttonVěc1_Click(object sender, EventArgs e)
        {
            if (indexButtonCorrect == 1)
            {
                Správně();
            }
            else
            {
                Špatně();
            }
        }
        
        // Věc 2:
        private void buttonVěc2_Click(object sender, EventArgs e)
        {
            if (indexButtonCorrect == 2)
            {
                Správně();
            }
            else
            {
                Špatně();
            }
        }

        // Věc 3:
        private void buttonVěc3_Click(object sender, EventArgs e)
        {
            if (indexButtonCorrect == 3)
            {
                Správně();
            }
            else
            {
                Špatně();
            }
        }

        // Konec tlačítek věcí.




        // Shown().
        private void FormUrčitáOsobnost_Shown(object sender, EventArgs e)
        {            
            VytvořSeznamy();

            this.Text = "Šárka kvíz (" + FormWelcoming.seznamOsobnost[FormOsobnosti.code].Name + ")";
            labelNadpis.Text = "Kvíz (" + FormWelcoming.seznamOsobnost[FormOsobnosti.code].Name + ")";
            buttonOsobnost.BackgroundImage = FormWelcoming.seznamOsobnost[FormOsobnosti.code].Image;
            buttonOsobnost.Text = FormWelcoming.seznamOsobnost[FormOsobnosti.code].Name;
            buttonOsobnost.ForeColor = FormWelcoming.seznamOsobnost[FormOsobnosti.code].Color;

            labelSkóre.Text = FormWelcoming.skóre.ToString();

            Rozlosuj();

            // Settings osobností jsou ve FormOsobnosti v událostech button1-8_Click().
        }

        private void buttonZpět_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormOsobnosti()).Show();
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            FormWelcoming.formCode = 4;
            this.Hide();
            (new FormKonec()).Show();
        }
      
        private void buttonOsobnost_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Tvoje vybraná osobnost se jmenuje " + FormWelcoming.seznamOsobnost[FormOsobnosti.code].Name + ". :)", "Vybraná osobnost", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
