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
    public partial class FormKonec : Form
    {
        public FormKonec()
        {
            InitializeComponent();

            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormKonec_FormClosed);
        }

        // Closed.
        private void FormKonec_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void buttonNe_Click(object sender, EventArgs e)
        {
            switch (FormWelcoming.formCode)
            {
                case 0:
                    // FormWelcoming (never used):
                    this.Hide();
                    (new FormWelcoming()).Show();
                    break;

                case 1:
                    // FormDividing:
                    this.Hide();
                    (new FormDividing()).Show();
                    break;

                case 2:
                    // FormMixQuiz:
                    this.Hide();
                    (new FormMixQuiz()).Show();
                    break;

                case 3:
                    // FormoOsobnosti:
                    this.Hide();
                    (new FormOsobnosti()).Show();
                    break;

                case 4:
                    // FormUrčitáOsobnost:
                    this.Hide();
                    (new FormUrčitáOsobnost()).Show();
                    break;

                default:
                    // Chyba:
                    MessageBox.Show("Omlouváme se, ale někde nastala chyba.", "Hlášení", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    break;
            }
        }

        private void buttonAno_Click(object sender, EventArgs e)
        {
            if (checkBoxZapsatSkóre.Checked)
            {
                this.Hide();
                (new FormSubmiting()).Show();
            }
            else
            {
                Application.Exit();
            }
        }

        // Ovládání checkBoxu checkBoxZapsatSkóre pomocí klávey Enter, je-li označen.
        private void checkBoxZapsatSkóre_KeyDown(object sender, KeyEventArgs e)
        {
            if (checkBoxZapsatSkóre.Focused)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (checkBoxZapsatSkóre.Checked)
                    {
                        checkBoxZapsatSkóre.Checked = false;
                    }
                    else
                    {
                        checkBoxZapsatSkóre.Checked = true;
                    }
                }
            }
            return;
        }
    }
}
