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
    public partial class FormParametricCheck : Form, IMyForm
    {
        public FormParametricCheck()
        {
            InitializeComponent();
        }

        private Program.ObjectInterfaceType interfaceType = Program.ObjectInterfaceType.I2D;    // Inicialization doesn't matter.

        private const string firstCoordinateText = "x";
        private const string secondCoordinateText = "y";
        private const string thirdCoordinateText = "z";
        private const string pointFirstCoordinateText = "A1";
        private const string pointSecondCoordinateText = "A2";
        private const string pointThirdCoordinateText = "A3";
        private const string vectorFirstCoordinateText = "u1";
        private const string vectorSecondCoordinateText = "u2";
        private const string vectorThirdCoordinateText = "u3";
        private const string mutliplicationSymbol = ".";
        private const string defaultNumericValueText = "0,00";

        public void LoadInCorrectLanguage(Language language)
        {
            this.Text = language.FormParametricCheckText;
            labelDescCheckParametricEquation.Text = language.LabelParametricCheckHeadlineText;

            labelDescModel.Text = language.LabelParametricModelText;
            if (Program.selectedObject == null)
                throw new Exception(language.ExceptionUnhandledSelectedNullText);

            IParametric checkedObject = (IParametric)Program.selectedObject;
            labelDescFirstCoordinate.Text = string.Concat(firstCoordinateText, " = ", pointFirstCoordinateText, " + ", vectorFirstCoordinateText, mutliplicationSymbol, checkedObject.ParameterName);
            labelDescSecondCoordinate.Text = string.Concat(secondCoordinateText, " = ", pointSecondCoordinateText, " + ", vectorSecondCoordinateText, mutliplicationSymbol, checkedObject.ParameterName);
            labelDescThirdCoordinate.Text = string.Concat(thirdCoordinateText, " = ", pointThirdCoordinateText, " + ", vectorThirdCoordinateText, mutliplicationSymbol, checkedObject.ParameterName);
            labelParameterRange.Text = checkedObject.GetParameterNameAndRange();

            labelDescGetA1.Text = string.Concat(pointFirstCoordinateText, " = ");
            labelDescGetA2.Text = string.Concat(pointSecondCoordinateText, " = ");
            labelDescGetA3.Text = string.Concat(pointThirdCoordinateText, " = ");
            labelDescGetU1.Text = string.Concat(vectorFirstCoordinateText, " = ");
            labelDescGetU2.Text = string.Concat(vectorSecondCoordinateText, " = ");
            labelDescGetU3.Text = string.Concat(vectorThirdCoordinateText, " = ");

            textBoxGetA1.Text = defaultNumericValueText;
            textBoxGetA2.Text = defaultNumericValueText;
            textBoxGetA3.Text = defaultNumericValueText;
            textBoxGetU1.Text = defaultNumericValueText;
            textBoxGetU2.Text = defaultNumericValueText;
            textBoxGetU3.Text = defaultNumericValueText;

            labelDescAreEquationsEquivalent.Text = language.LabelParametricCheckQuestionText;
            labelAnswerEquation.Visible = false;
            labelAnswerEquation.Text = "";  // No answer yet, thus no text needed.

            buttonMenuFromCheckParametricEquation.Text = language.ButtonMenuFrom2DText;
            buttonDisplayFromCheckParametricEquation.Text = language.ButtonGoToDisplayText;
            buttonCheckEquation.Text = language.ButtonCheckParametricEquationText;
            buttonCheckEquation.Enabled = true;

            if (Program.selectedObject is IObject2D)
            {
                labelDescThirdCoordinate.Visible = false;
                labelDescGetA3.Visible = false;
                textBoxGetA3.Visible = false;
                labelDescGetU3.Visible = false;
                textBoxGetU3.Visible = false;
                this.interfaceType = Program.ObjectInterfaceType.I2D;
            }
            else if (Program.selectedObject is IObject3D)
            {
                labelDescThirdCoordinate.Visible = true;
                labelDescGetA3.Visible = true;
                textBoxGetA3.Visible = true;
                labelDescGetU3.Visible = true;
                textBoxGetU3.Visible = true;
                this.interfaceType = Program.ObjectInterfaceType.I3D;
            }
            else
                throw new Exception(Program.language.ExceptionUnhandledLanguageText);
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

        private double ParseTextBoxWithoutException(TextBox textBox)
        {
            Language lang = Program.language;
            if (textBox.Text == "-" || textBox.Text == "-" + lang.DecimalPointText || textBox.Text == lang.DecimalPointText.ToString() || textBox.Text == "" || textBox.Text == null)
                return 0d;
            else
                return double.Parse(textBox.Text);
        }

        private void buttonCheckEquation_Click(object sender, EventArgs e)
        {
            const double defaultValue = 0.00;
            double x, y, z;
            double u1, u2, u3;
            bool answer;
            Vector[] vectors = new Vector[1];   // Array so we can expand the calculator to include planes here as well.

            x = ParseTextBoxWithoutException(textBoxGetA1);
            y = ParseTextBoxWithoutException(textBoxGetA2);
            u1 = ParseTextBoxWithoutException(textBoxGetU1);
            u2 = ParseTextBoxWithoutException(textBoxGetU2);

            switch (this.interfaceType)
            {
                case Program.ObjectInterfaceType.I2D:
                    z = defaultValue;
                    u3 = defaultValue;
                    break;
                case Program.ObjectInterfaceType.I3D:
                    z = ParseTextBoxWithoutException(textBoxGetA3);
                    u3 = ParseTextBoxWithoutException(textBoxGetU3);
                    break;
                default:
                    throw new Exception(Program.language.ExceptionUnhandledLanguageText);
            }

            vectors[0] = new Vector(u1, u2, u3);
            answer = ((IParametric)Program.selectedObject).CheckParametricEquationEquivalency(new MyPoint(x, y, z), vectors);

            if (answer)
                labelAnswerEquation.Text = Program.language.AnswerYes;
            else
                labelAnswerEquation.Text = Program.language.AnswerNo;

            labelAnswerEquation.Visible = true;
            buttonCheckEquation.Enabled = false;
        }

        private void buttonMenuFromCheckParametricEquation_Click(object sender, EventArgs e)
        {
            Program.selectedObject = null;
            GoToOtherForm(Program.formMainMenu);
        }

        private void buttonDisplayFromCheckParametricEquation_Click(object sender, EventArgs e)
        {
            IObjectAG selectedObj = Program.selectedObject;
            Program.selectedObject = null;

            if (selectedObj is IObject2D)
                GoToOtherForm(Program.formDisplay2D);
            else if (selectedObj is IObject3D)
                throw new NotImplementedException(Program.language.ExceptionNotImplementedText); // TODO FormDisplay3D
            else
                throw new Exception(Program.language.ExceptionUnhandledLanguageText);
        }


        // Handling visibility.
        private void textBoxGetA1_TextChanged(object sender, EventArgs e)
        {
            labelAnswerEquation.Visible = false;
            buttonCheckEquation.Enabled = true;
        }
        private void textBoxGetA2_TextChanged(object sender, EventArgs e)
        {
            labelAnswerEquation.Visible = false;
            buttonCheckEquation.Enabled = true;
        }
        private void textBoxGetA3_TextChanged(object sender, EventArgs e)
        {
            labelAnswerEquation.Visible = false;
            buttonCheckEquation.Enabled = true;
        }
        private void textBoxGetU1_TextChanged(object sender, EventArgs e)
        {
            labelAnswerEquation.Visible = false;
            buttonCheckEquation.Enabled = true;
        }
        private void textBoxGetU2_TextChanged(object sender, EventArgs e)
        {
            labelAnswerEquation.Visible = false;
            buttonCheckEquation.Enabled = true;
        }
        private void textBoxGetU3_TextChanged(object sender, EventArgs e)
        {
            labelAnswerEquation.Visible = false;
            buttonCheckEquation.Enabled = true;
        }


        // Handling input.
        private void textBoxGetA1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Note: ShortcutsEnabled == false.

            // Handling delete and backspace.
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // We don't want control keys, non-numeric characters. We're allowing a decimal point and a minus symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Program.language.DecimalPointText && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Handling a decimal point.
            if (e.KeyChar == Program.language.DecimalPointText && (sender as TextBox).Text.IndexOf(Program.language.DecimalPointText) > -1)
            {
                e.Handled = true;
            }

            // Handling a minus symbol.
            if (e.KeyChar == '-' && ((sender as TextBox).SelectionStart > 0 || (sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

            // Nothing can be before a possible minus symbol.
            if ((sender as TextBox).Text.IndexOf('-') > -1 && (sender as TextBox).SelectionStart == 0)
            {
                e.Handled = true;
            }
        }
        private void textBoxGetA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Note: ShortcutsEnabled == false.

            // Handling delete and backspace.
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // We don't want control keys, non-numeric characters. We're allowing a decimal point and a minus symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Program.language.DecimalPointText && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Handling a decimal point.
            if (e.KeyChar == Program.language.DecimalPointText && (sender as TextBox).Text.IndexOf(Program.language.DecimalPointText) > -1)
            {
                e.Handled = true;
            }

            // Handling a minus symbol.
            if (e.KeyChar == '-' && ((sender as TextBox).SelectionStart > 0 || (sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

            // Nothing can be before a possible minus symbol.
            if ((sender as TextBox).Text.IndexOf('-') > -1 && (sender as TextBox).SelectionStart == 0)
            {
                e.Handled = true;
            }
        }
        private void textBoxGetA3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Note: ShortcutsEnabled == false.

            // Handling delete and backspace.
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // We don't want control keys, non-numeric characters. We're allowing a decimal point and a minus symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Program.language.DecimalPointText && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Handling a decimal point.
            if (e.KeyChar == Program.language.DecimalPointText && (sender as TextBox).Text.IndexOf(Program.language.DecimalPointText) > -1)
            {
                e.Handled = true;
            }

            // Handling a minus symbol.
            if (e.KeyChar == '-' && ((sender as TextBox).SelectionStart > 0 || (sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

            // Nothing can be before a possible minus symbol.
            if ((sender as TextBox).Text.IndexOf('-') > -1 && (sender as TextBox).SelectionStart == 0)
            {
                e.Handled = true;
            }
        }
        private void textBoxGetU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Note: ShortcutsEnabled == false.

            // Handling delete and backspace.
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // We don't want control keys, non-numeric characters. We're allowing a decimal point and a minus symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Program.language.DecimalPointText && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Handling a decimal point.
            if (e.KeyChar == Program.language.DecimalPointText && (sender as TextBox).Text.IndexOf(Program.language.DecimalPointText) > -1)
            {
                e.Handled = true;
            }

            // Handling a minus symbol.
            if (e.KeyChar == '-' && ((sender as TextBox).SelectionStart > 0 || (sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

            // Nothing can be before a possible minus symbol.
            if ((sender as TextBox).Text.IndexOf('-') > -1 && (sender as TextBox).SelectionStart == 0)
            {
                e.Handled = true;
            }
        }
        private void textBoxGetU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Note: ShortcutsEnabled == false.

            // Handling delete and backspace.
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // We don't want control keys, non-numeric characters. We're allowing a decimal point and a minus symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Program.language.DecimalPointText && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Handling a decimal point.
            if (e.KeyChar == Program.language.DecimalPointText && (sender as TextBox).Text.IndexOf(Program.language.DecimalPointText) > -1)
            {
                e.Handled = true;
            }

            // Handling a minus symbol.
            if (e.KeyChar == '-' && ((sender as TextBox).SelectionStart > 0 || (sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

            // Nothing can be before a possible minus symbol.
            if ((sender as TextBox).Text.IndexOf('-') > -1 && (sender as TextBox).SelectionStart == 0)
            {
                e.Handled = true;
            }
        }
        private void textBoxGetU3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Note: ShortcutsEnabled == false.

            // Handling delete and backspace.
            if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // We don't want control keys, non-numeric characters. We're allowing a decimal point and a minus symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Program.language.DecimalPointText && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Handling a decimal point.
            if (e.KeyChar == Program.language.DecimalPointText && (sender as TextBox).Text.IndexOf(Program.language.DecimalPointText) > -1)
            {
                e.Handled = true;
            }

            // Handling a minus symbol.
            if (e.KeyChar == '-' && ((sender as TextBox).SelectionStart > 0 || (sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

            // Nothing can be before a possible minus symbol.
            if ((sender as TextBox).Text.IndexOf('-') > -1 && (sender as TextBox).SelectionStart == 0)
            {
                e.Handled = true;
            }
        }
    }
}
