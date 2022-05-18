using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGCalculator
{
    public partial class FormLanguage : Form, IMyForm
    {
        public FormLanguage()
        {
            InitializeComponent();
        }

        public void LoadInCorrectLanguage(Language lang)
        {
            // This.
            this.Text = lang.FormLanguageText;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // RadioButtons.
            radioButtonLanguageEnglish.Checked = true;
            radioButtonLanguageCzech.Checked = false;

            // Text.            
            labelDescSelectLanguage.Text = lang.LabelDescSelectLanguageText;
            buttonSelectLanguage.Text = lang.ButtonSelectLanguageText;
        }
        public void GoToOtherForm(IMyForm nextForm)
        {
            if (!this.Visible)
                return;

            this.Visible = false;
            nextForm.LoadInCorrectLanguage(Program.language);
            ((Form)nextForm).Location = this.Location;
            ((Form)nextForm).StartPosition = FormStartPosition.Manual;
            ((Form)nextForm).FormClosing += delegate { Application.Exit(); };
            ((Form)nextForm).Visible = true;
        }

        private void buttonSelectLanguage_Click(object sender, EventArgs e)
        {
            // Changing actual language.
            if (radioButtonLanguageEnglish.Checked)
            {
                if (Program.language == Language.english)
                {
                    GoToOtherForm(Program.formMainMenu); // Nothing to change.
                    return;
                }                    
                else
                    Program.language = Language.english;
            }                               
            else if (radioButtonLanguageCzech.Checked)
            {
                if (Program.language == Language.czech)
                {
                    GoToOtherForm(Program.formMainMenu); // Nothing to change.
                    return;
                }
                else
                    Program.language = Language.czech;
            }                
            else
            {
                MessageBox.Show(Program.language.ExceptionUnhandledLanguageText, "E-e...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // We need user to choose something else.
            }                

            // Adjusting lists of types of objects. (We know that the language has been changed.)
            // 2D:
            Program.typeObject2D.Clear();
            Program.typeObject2D = Program.language.GetTypesOf2DObjects();
            // 3D:
            Program.typeObject3D.Clear();
            Program.typeObject3D = Program.language.GetTypesOf3DObjects();

            GoToOtherForm(Program.formMainMenu);
        }
    }
}
