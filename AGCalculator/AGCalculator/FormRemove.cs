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
    public partial class FormRemove : Form, IMyForm
    {
        public FormRemove()
        {
            InitializeComponent();
        }
        
        internal Program.ObjectInterfaceType interfaceType = Program.ObjectInterfaceType.I2D;    // Inicialization doesn't matter.

        public void LoadInCorrectLanguage(Language language)
        {
            string removingInWhat;
            switch (this.interfaceType)
            {
                case Program.ObjectInterfaceType.I2D:
                    removingInWhat = language.RemovingIn2DText;
                    break;
                case Program.ObjectInterfaceType.I3D:
                    removingInWhat = language.RemovingIn3DText;
                    break;
                default:
                    throw new Exception(language.ExceptionObjectInterfaceTypeText);
            }
            this.Text = string.Concat(language.FormRemoveText, " (", removingInWhat, ")");
            labelDescRemoveHeadline.Text = language.LabelRemoveHeadlineText;
            buttonRemoveObject.Text = language.ButtonRemoveText;
            buttonMenuFromRemove.Text = language.MainMenuText;
            buttonAddFromRemove.Text = string.Concat(language.ButtonGoToAddText, " (", removingInWhat, ")");

            comboBoxRemoveObject.Text = "";
            comboBoxRemoveObject.Items.Clear();
            switch (this.interfaceType)
            {
                case Program.ObjectInterfaceType.I2D:
                    labelDimension.Text = language.Dimension2D;
                    foreach (string name in Program.object2DNames)
                        comboBoxRemoveObject.Items.Add(name);
                    break;
                case Program.ObjectInterfaceType.I3D:
                    labelDimension.Text = language.Dimension3D;
                    foreach (string name in Program.object3DNames)
                        comboBoxRemoveObject.Items.Add(name);
                    break;
                default:
                    throw new Exception(language.ExceptionObjectInterfaceTypeText);
            }
            buttonRemoveObject.Enabled = false;
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

        private void buttonRemoveObject_Click(object sender, EventArgs e)
        {
            Language language = Program.language;
            // The Remove button should be disabled if there's nothing newly selected in the comboBox.
            if (comboBoxRemoveObject.Text == "" || comboBoxRemoveObject.Text == null)
                throw new Exception("A logical error has occured in disabling the Remove button. Please contact us.");

            DialogResult result = MessageBox.Show(language.MessageSureToRemoveObjectText, language.MessageSureToRemoveObjectCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string objectName = comboBoxRemoveObject.Text;
                List<string> removingList;
                switch (this.interfaceType)
                {
                    case Program.ObjectInterfaceType.I2D:
                        Program.objects2D.Remove(objectName);
                        removingList = Program.object2DNames;
                        break;
                    case Program.ObjectInterfaceType.I3D:
                        Program.objects3D.Remove(objectName);
                        removingList = Program.object3DNames;
                        break;
                    default:
                        throw new Exception(language.ExceptionObjectInterfaceTypeText);
                }
                removingList.Remove(objectName);
                MessageBox.Show(language.MessageObjectRemovedText, language.MessageObjectRemovedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (removingList.Count == 0)
                {
                    MessageBox.Show(language.MessageEverythingWasRemovedText, language.MessageEverythingWasRemovedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GoToOtherForm(Program.formMainMenu);
                }
                else
                    LoadInCorrectLanguage(language);
            }
        }

        private void buttonAddFromRemove_Click(object sender, EventArgs e)
        {
            switch (this.interfaceType)
            {
                case Program.ObjectInterfaceType.I2D:
                    GoToOtherForm(Program.formAdd2D);
                    break;
                case Program.ObjectInterfaceType.I3D:
                    throw new NotImplementedException(Program.language.ExceptionNotImplementedText);
                // GoToOtherForm(Program.formAdd3D);    // TODO 3D
                // break;
                default:
                    throw new Exception(Program.language.ExceptionObjectInterfaceTypeText);
            }
        }
        private void buttonMenuFromRemove_Click(object sender, EventArgs e)
        {
            GoToOtherForm(Program.formMainMenu);
        }
        private void comboBoxRemoveObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemoveObject.Enabled = true;
        }
    }
}
