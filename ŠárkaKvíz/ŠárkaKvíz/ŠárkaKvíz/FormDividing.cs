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
    public partial class FormDividing : Form
    {
        public FormDividing()
        {
            InitializeComponent();

            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormDividing_FormClosed);
        }

        // Deklarace s inicializací SoundPlayera.
        System.Media.SoundPlayer ilumináti = new System.Media.SoundPlayer(ŠárkaKvíz.Properties.Resources.Ilumináti);

        public static int clicks = 0;

        static int sekundy = 0;

        // Closed.
        private void FormDividing_FormClosed(object sender, FormClosedEventArgs e)
        {
           Application.Exit();
        }

        // Včetně ošetřené výjimky, kdyby už nebyly otázky. (Zeptá se na vyresetování, ale včetně skóre!)
        private void buttonMix_Click(object sender, EventArgs e)
        {
            if (FormWelcoming.returnedOnce)
            {
                DialogResult res = MessageBox.Show("Přejete si otázky restartovat? \n \n UPOZORNĚNÍ: Zároveň vynulujete i své skóre!", "Otázky už došly", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // Přidání všech věcí zpět do seznamu.
                    foreach (Věc věc in FormWelcoming.seznamVěcPůvodní)
                    {
                        FormWelcoming.seznamVěcNový.Add(věc);
                    }

                    // Znovu budeme otevírat nerozlosovaný kvíz, tudíž je potřeba říct programu, aby kvíz rozlosoval ještě před tím, než něco zmáčkneme.
                    //FormWelcoming.quizFirstTime = true;   // bugged

                    // Už zase otázky jsou, tedy můžeme nastavit hodnotu proměnné na false.
                    FormWelcoming.returnedOnce = false;

                    // Vynulování skóre:
                    FormWelcoming.skóre = 0;
                    FormWelcoming.správných = 0;
                    FormWelcoming.špatných = 0;

                    // Vynulování clicků v případě návratu:
                    timerBarvy.Enabled = false;
                    timerOdpočet.Enabled = false;
                    sekundy = 0;
                    clicks = 0;
                    ilumináti.Stop();

                    // Přesun do FormMixQuiz:
                    this.Hide();
                    (new FormMixQuiz()).Show();
                }
            }
            else
            {
                timerBarvy.Enabled = false;
                timerOdpočet.Enabled = false;
                sekundy = 0;
                clicks = 0;
                ilumináti.Stop();
                this.Hide();
                (new FormMixQuiz()).Show();
            }
        }

        private void buttonOsobnosti_Click(object sender, EventArgs e)
        {
            timerBarvy.Enabled = false;
            timerOdpočet.Enabled = false;
            clicks = 0;
            sekundy = 0;
            ilumináti.Stop();
            this.Hide();
            (new FormOsobnosti()).Show();
        }

        private void buttonPointless_Click(object sender, EventArgs e)
        {
            // This has to be first.
            if (FormOsobnosti.ritualDoneAdd)
            {
                MessageBox.Show("Really. There's nothing here anymore. \nMaybe something will come in the future again. :)", "Pointless button", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormOsobnosti.ritualDoneAdd = false;
            }
            //  This has to be second.
            if (!FormOsobnosti.ritualDone)      // Nové využití pro proměnnou ritualDone a ritualDoneAdd.
            {
                MessageBox.Show("Warning : Pointless", "Pointless button", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                FormOsobnosti.ritualDone = true;
                FormOsobnosti.ritualDoneAdd = true;
            }
            
            /*
            clicks++;

            if (clicks == 1)
            {
                MessageBox.Show("Warning: Not so pointless anymore...", "USEFUL BUTTON?", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonPointless.Text = "Useful";
            }
            else if (clicks == 10)
            {
                MessageBox.Show("Yeah, click me.", "Please", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                buttonPointless.Text = "Click me";
            }
            else if (clicks == 25)
            {
                buttonPointless.Text = "Click me!";
            }
            else if (clicks == 50)
            {
                buttonPointless.Text = "Click me!!!";
            }
            else if (clicks == 80)
            {
                buttonPointless.Text = "Click me more!!!";
            }
            else if (clicks == 115)
            {
                buttonPointless.Text = "I need more!!!";
                buttonPointless.BackColor = Color.Orange;
            }
            else if (clicks == 150)
            {
                buttonPointless.Text = "MORE!!!";
                buttonPointless.BackColor = Color.OrangeRed;
                buttonPointless.ForeColor = Color.Gold;
            }
            else if (clicks == 200)
            {
                buttonPointless.Text = "YEEEES!!!";
                buttonPointless.BackColor = Color.Red;
                buttonPointless.ForeColor = Color.Yellow;
            }
            else if (clicks == 250)
            {
                buttonPointless.Text = "THE POWER!!!";
                timerBarvy.Interval = 250;
                timerBarvy.Enabled = true;
            }
            else if (clicks == 300)
            {
                buttonPointless.Text = "AAAAA!!!";
                timerBarvy.Interval = 100;
            }
            else if (clicks == 350)
            {
                timerBarvy.Enabled = false;
                buttonPointless.BackColor = Color.Bisque;
                buttonPointless.ForeColor = Color.SaddleBrown;
                buttonPointless.Text = "50 more";
                MessageBox.Show("Wait! Something's happening...", "Time to slow down", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (clicks > 350 && clicks < 400)
            {
                buttonPointless.Text = (400 - clicks).ToString() + " more";
            }
            else if (clicks == 400)
            {
                MessageBox.Show("Don't click anymore!", "Wait", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                buttonPointless.Text = "Don't click!";
                timerOdpočet.Interval = 1000;
                timerOdpočet.Enabled = true;
            }                      
            // Pokud nepočká, když má čekat.
            else if (clicks == 401)
            {
                timerOdpočet.Enabled = false;
                clicks = 0;
                sekundy = 0;
                buttonPointless.BackColor = Color.Bisque;
                buttonPointless.ForeColor = Color.SaddleBrown;
                buttonPointless.Text = "Pointless?";
                MessageBox.Show("Why didn't you listen...", "Shame on you", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            // Rituál:
            else if (clicks == 500)     // Dosažitelné jenom čekáním a 1 zmáčknutím po změně textu tlačítka na nápis "The button".
            {
                buttonPointless.Visible = false;
                pointlessVisible = false;       // Už splnilo svůj úkol.    // Proměnná byla smazána, potřeba znovu deklarovat s inicializací na true.
                clicks = 0;
                timerOdpočet.Enabled = false;
                FormOsobnosti.ritualDone = true;
                FormOsobnosti.ritualDoneAdd = true;

                buttonOsobnosti.BackColor = Color.Red;
                buttonOsobnosti.ForeColor = Color.Yellow;
            }
            */
        }

        private void buttonKonec_Click(object sender, EventArgs e)
        {
            timerBarvy.Enabled = false;
            timerOdpočet.Enabled = false;
            clicks = 0;
            sekundy = 0;
            ilumináti.Stop();
            timerOdpočet.Enabled = false;
            timerBarvy.Enabled = false;

            FormWelcoming.formCode = 1;
            this.Hide();
            (new FormKonec()).Show();
        }

        private void FormDividing_Shown(object sender, EventArgs e)
        {
            timerBarvy.Enabled = false;
            timerOdpočet.Enabled = false;

            clicks = 0;
            sekundy = 0;
        }

        private void timerBarvy_Tick(object sender, EventArgs e)
        {
            switch (buttonPointless.BackColor.Name)
            {
                case "Red":
                    buttonPointless.BackColor = Color.Orange;
                    buttonPointless.ForeColor = Color.SaddleBrown;
                    break;
                case "Orange":
                    buttonPointless.BackColor = Color.OrangeRed;
                    buttonPointless.ForeColor = Color.Gold;
                    break;
                case "OrangeRed":
                    buttonPointless.BackColor = Color.Red;
                    buttonPointless.ForeColor = Color.Yellow;
                    break;
            }
        }

        private void timerOdpočet_Tick(object sender, EventArgs e)
        {
            sekundy++;

            if (sekundy >= 1 && sekundy <= 5)
            {
                buttonPointless.Text = (6 - sekundy).ToString() + "...";
            }
            else if (sekundy >= 6)
            {
                switch (sekundy)
                {
                    case 6:
                        buttonPointless.Enabled = false;
                        ilumináti.Play();
                        clicks = 499;
                        break;
                    case 7:
                        buttonPointless.Text = "T";
                        break;
                    case 8:
                        buttonPointless.Text = "Th";
                        break;
                    case 9:
                        buttonPointless.Text = "The";
                        break;
                    case 10:
                        buttonPointless.Text = "The ";
                        break;
                    case 11:
                        buttonPointless.Text = "The b";
                        break;
                    case 12:
                        buttonPointless.Text = "The bu";
                        break;
                    case 13:
                        buttonPointless.Text = "The but";
                        break;
                    case 14:
                        buttonPointless.Text = "The butt";
                        break;
                    case 15:
                        buttonPointless.Text = "The butt?";
                        break;
                    case 16:
                        buttonPointless.Text = "The BUTT!";
                        break;
                    case 17:
                        buttonPointless.Text = "THE BUTT! HAHAHA!";
                        break;
                    case 18:
                        buttonPointless.Text = "The butt...";
                        break;
                    case 19:
                        buttonPointless.Text = "The butto";
                        break;
                    case 20:
                        buttonPointless.Text = "The button";
                        break;
                    case 21:
                        buttonPointless.Enabled = true;
                        break;
                    case 26:
                        timerOdpočet.Enabled = false;
                        MessageBox.Show("One last click...", "The button", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }         
        }
    }
}
