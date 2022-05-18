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
    public partial class FormMainMenu : Form, IMyForm
    {
        public FormMainMenu()
        {
            InitializeComponent();
        }

        private void CenterLabel(Label l)
        {
            l.Left = this.Width / 2 - l.Width / 2;
        }
        public void LoadInCorrectLanguage(Language lang)
        {
            // Text.
            this.Text = lang.MainMenuText;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            labelDescMainMenu.Text = lang.MainMenuText;
            CenterLabel(labelDescMainMenu);
            labelDesc2D.Text = lang.LabelDesc2DText;
            labelDesc3D.Text = lang.LabelDesc3DText;
            buttonGoToAdd2D.Text = lang.ButtonGoToAddText;
            buttonGoToDisplay2D.Text = lang.ButtonGoToDisplayText;
            buttonGoToRemove2D.Text = lang.ButtonRemoveText;
            buttonGoToAdd3D.Text = lang.ButtonGoToAddText;
            buttonGoToDisplay3D.Text = lang.ButtonGoToDisplayText;
            buttonGoToRemove3D.Text = lang.ButtonRemoveText;
            buttonGoToChangeLanguage.Text = lang.ButtonGoToChangeLanguageText;
            buttonExit.Text = lang.ButtonExitText;
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

        private void buttonGoToAdd2D_Click(object sender, EventArgs e)
        {
            GoToOtherForm(Program.formAdd2D);
        }

        private void buttonGoToDisplay2D_Click(object sender, EventArgs e)
        {
            if (Program.object2DNames.Count == 0)
                MessageBox.Show(Program.language.MessageNoObejctsText, Program.language.MessageNoObejctsCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                GoToOtherForm(Program.formDisplay2D);
        }

        private void buttonGoToAdd3D_Click(object sender, EventArgs e)
        {

        }

        private void buttonGoToDisplay3D_Click(object sender, EventArgs e)
        {

        }

        private void buttonGoToChangeLanguage_Click(object sender, EventArgs e)
        {
            GoToOtherForm(Program.formLanguage);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            LoadInCorrectLanguage(Program.language);
        }

        private void buttonGoToRemove2D_Click(object sender, EventArgs e)
        {
            if (Program.object2DNames.Count == 0)
                MessageBox.Show(Program.language.MessageNoObejctsText, Program.language.MessageNoObejctsCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                Program.formRemove.interfaceType = Program.ObjectInterfaceType.I2D;
                GoToOtherForm(Program.formRemove);
            }                
        }

        private void buttonGoToRemove3D_Click(object sender, EventArgs e)
        {
            if (Program.object3DNames.Count == 0)
                MessageBox.Show(Program.language.MessageNoObejctsText, Program.language.MessageNoObejctsCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                Program.formRemove.interfaceType = Program.ObjectInterfaceType.I3D;
                GoToOtherForm(Program.formRemove);
            }
        }
    }
}
