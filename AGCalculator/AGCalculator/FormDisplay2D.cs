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
    public partial class FormDisplay2D : Form, IMyForm
    {
        public FormDisplay2D()
        {
            InitializeComponent();
        }

        private const int myDefaultWidth = 425; // 555
        private const int thisWidthForEllipse = 750;
        private const int thisWidthForHyperbola = 750;

        private void Adjust2DObjectList()
        {
            this.comboBoxDisplay2DObject.Items.Clear();
            foreach (string object2D in Program.object2DNames)
                this.comboBoxDisplay2DObject.Items.Add(object2D);
        }
        public void LoadInCorrectLanguage(Language lang)
        {
            this.Text = lang.FormDisplay2DText;
            this.Width = myDefaultWidth;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            labelDescDisplay2DObject.Text = lang.LabelDescDisplay2DObjectText;
            comboBoxDisplay2DObject.Text = "";
            comboBoxDisplay2DObject.DropDownStyle = ComboBoxStyle.DropDownList;
            Adjust2DObjectList();   // Adjusting comboBox items.
            buttonDisplay2DObject.Text = lang.ButtonDisplay2DObjectText;
            buttonAdd2DFromDisplay2D.Text = lang.ButtonAdd2DFromDisplay2DText;
            buttonMenuFrom2D.Text = lang.ButtonMenuFrom2DText;
            labelDescTypeOf2DObject.Text = lang.LabelDescTypeOf2DObjectText;
            labelDescTypeOf2DObject.Visible = false;
            labelTypeOf2DObject.Visible = false;
            buttonCheckParametricEquation2D.Text = lang.ButtonGoToCheckParametricEquationText;
            buttonCheckParametricEquation2D.Visible = false;

            labelName1Of2DObject.Visible = false;
            labelName2Of2DObject.Visible = false;
            labelName3Of2DObject.Visible = false;
            labelName4Of2DObject.Visible = false;
            labelName5Of2DObject.Visible = false;
            labelEquation1Of2DObject.Visible = false;
            labelEquation2Of2DObject.Visible = false;
            labelEquation3Of2DObject.Visible = false;
            labelEquation4Of2DObject.Visible = false;
            labelEquation5Of2DObject.Visible = false;
            labelParameterRange2D.Visible = false;

            labelDescPointOf2DObject.Text = lang.LabelDescPointOf2DObjectText;
            labelAnswerPoint2D.Visible = false;
            labelDescXCoordinate2D.Text = "X: ";
            labelDescYCoordinate2D.Text = "Y: ";
            textBoxXCoordinate2D.Text = "0" + lang.DecimalPointText + "00";
            textBoxYCoordinate2D.Text = "0" + lang.DecimalPointText + "00";
            textBoxXCoordinate2D.Enabled = false;
            textBoxYCoordinate2D.Enabled = false;
            buttonCheckPoint2D.Text = lang.ButtonCheckPoint2DText;
            buttonCheckPoint2D.Visible = false;
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

        /// <summary>
        /// Checks comboBoxDisplay2DObject.Text. In case it's empty shows a message box.
        /// </summary>
        /// <returns>True for NOT EMPTY comboBox.Text, false otherwise.</returns>
        private bool EnsureComboBoxIsNotEmpty()
        {
            if (comboBoxDisplay2DObject.Text == "")
            {
                MessageBox.Show(Program.language.MessageEmptyComboBoxText, Program.language.MessageEmptyComboBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
                return true;
        }

        private double ParseTextBoxWithoutException(TextBox textBox)
        {
            Language lang = Program.language;
            if (textBox.Text == "-" || textBox.Text == "-" + lang.DecimalPointText || textBox.Text == lang.DecimalPointText.ToString() || textBox.Text == "" || textBox.Text == null)
                return 0d;
            else
                return double.Parse(textBox.Text);
        }

        private void ShowOrHideControls(bool parametric, bool[] showName)
        {
            const uint maxEquationsCount = 5;
            if (showName == null)
                throw new ArgumentException("The array " + nameof(showName) + " cannot be null.");
            if (showName.Length == 0)
                throw new ArgumentException("You cannot call this method with empty " + nameof(showName) + " array.");
            if (showName.Length > maxEquationsCount)
                throw new ArgumentException("You cannot command to show or hide more names than equations. Maximum number of equations is " + maxEquationsCount + ".");

            labelDescTypeOf2DObject.Visible = true;
            labelTypeOf2DObject.Visible = true;

            labelName1Of2DObject.Visible = false;
            // Equation1 is always visible.
            labelName2Of2DObject.Visible = false;
            labelEquation2Of2DObject.Visible = false;
            labelName3Of2DObject.Visible = false;
            labelEquation3Of2DObject.Visible = false;
            labelName4Of2DObject.Visible = false;
            labelEquation4Of2DObject.Visible = false;
            labelName5Of2DObject.Visible = false;
            labelEquation5Of2DObject.Visible = false;

            int equationsToShow = showName.Length;

            labelEquation1Of2DObject.Visible = true;    // Always visible.
            if (showName[0])    // Always: equationsToShow >= 1
                labelName1Of2DObject.Visible = true;
            if (equationsToShow >= 2)
            {
                if (showName[1])
                    labelName2Of2DObject.Visible = true;
                labelEquation2Of2DObject.Visible = true;
            }
            if (equationsToShow >= 3)
            {

                if (showName[2])
                    labelName3Of2DObject.Visible = true;
                labelEquation3Of2DObject.Visible = true;
            }                               
            if (equationsToShow >= 4)
            {
                if (showName[3])
                    labelName4Of2DObject.Visible = true;
                labelEquation4Of2DObject.Visible = true;
            }                              
            if (equationsToShow >= 5)
            {
                if (showName[4])
                    labelName5Of2DObject.Visible = true;
                labelEquation5Of2DObject.Visible = true;
            }                
                
            if (parametric)
            {
                buttonCheckParametricEquation2D.Visible = true;
                buttonCheckParametricEquation2D.Enabled = true;
                labelParameterRange2D.Visible = true;
            }
            else
            {
                buttonCheckParametricEquation2D.Visible = false;
                buttonCheckParametricEquation2D.Enabled = false;
                labelParameterRange2D.Visible = false;
            }
            
        }

        /// <summary>
        /// Displays all information about the selected 2D object.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDisplay2DObject_Click(object sender, EventArgs e)
        {
            if (!EnsureComboBoxIsNotEmpty())
                return;

            IObject2D object2D = Program.objects2D[comboBoxDisplay2DObject.Text];
            Program.selectedObject = object2D;
            const string charsAfterName = ": ";
            if (object2D is LineIn2D)
            {
                LineIn2D line = (LineIn2D)object2D;
                string nameDone = string.Concat(comboBoxDisplay2DObject.Text, charsAfterName);

                // Type.
                labelTypeOf2DObject.Text = Program.language.TypeOf2DObjectLine;

                // Labels with line's name.
                labelName1Of2DObject.Text = nameDone;
                labelName2Of2DObject.Text = nameDone;   // Not neccessary.
                labelName3Of2DObject.Text = nameDone;
                labelName4Of2DObject.Text = nameDone;
                labelName5Of2DObject.Text = nameDone;

                // Equations.
                labelEquation1Of2DObject.Text = line.GetParametricEquationForX();
                labelEquation2Of2DObject.Text = line.GetParametricEquationForY();
                labelEquation3Of2DObject.Text = line.GetGeneralEquation();
                labelEquation4Of2DObject.Text = line.GetLineSlopeEquation() ?? Program.language.SlopeEquationsDoesNotExist;
                labelEquation5Of2DObject.Text = line.GetLineSegmentalEquation() ?? Program.language.SegmentalEquationsDoesNotExist;

                // Parameter.
                labelParameterRange2D.Text = line.GetParameterNameAndRange();
                
                ShowOrHideControls(true, new bool[] { true, false, true, true, true });
                this.Width = myDefaultWidth;
            }
            else if (object2D is Circle)
            {
                Circle circle = (Circle)object2D;
                string nameDone = string.Concat(comboBoxDisplay2DObject.Text, charsAfterName);

                // Type.
                labelTypeOf2DObject.Text = Program.language.TypeOf2DObjectCircle;

                // Labels with line's name.
                labelName1Of2DObject.Text = nameDone;   // Not neccessary.
                labelName2Of2DObject.Text = nameDone;   // Not neccessary.
                labelName3Of2DObject.Text = nameDone;   
                labelName4Of2DObject.Text = nameDone;   
                labelName5Of2DObject.Text = nameDone;   // Not neccessary.

                // Equations.
                labelEquation1Of2DObject.Text = circle.GetMidpointInfo();
                labelEquation2Of2DObject.Text = circle.GetRadiusInfo();
                labelEquation3Of2DObject.Text = circle.GetCircleEquation();
                labelEquation4Of2DObject.Text = circle.GetGeneralEquation();

                ShowOrHideControls(false, new bool[] { false, false, true, true });
                this.Width = myDefaultWidth;
            }
            else if (object2D is Ellipse)
            {
                Ellipse ellipse = (Ellipse)object2D;
                string nameDone = string.Concat(comboBoxDisplay2DObject.Text, charsAfterName);

                // Type.
                labelTypeOf2DObject.Text = Program.language.TypeOf2DObjectEllipse;

                // Labels with line's name.
                labelName1Of2DObject.Text = nameDone;   // Not neccessary.
                labelName2Of2DObject.Text = nameDone;   // Not neccessary.
                labelName3Of2DObject.Text = nameDone;   // Not neccessary.
                labelName4Of2DObject.Text = nameDone;   
                labelName5Of2DObject.Text = nameDone;   

                // Equations.
                labelEquation1Of2DObject.Text = ellipse.GetMidpointAndFociInfo();
                labelEquation2Of2DObject.Text = ellipse.GetAllVerticesInfo();
                labelEquation3Of2DObject.Text = ellipse.GetEllipseParametersInfo();
                labelEquation4Of2DObject.Text = ellipse.GetEllipseEquation();
                labelEquation5Of2DObject.Text = ellipse.GetGeneralEquation();

                ShowOrHideControls(false, new bool[] { false, false, false, true, true });
                this.Width = thisWidthForEllipse;   // So the information can fit in.
            }
            else if (object2D is Hyperbola)
            {
                Hyperbola hyperbola = (Hyperbola)object2D;
                string nameDone = string.Concat(comboBoxDisplay2DObject.Text, charsAfterName);

                // Type.
                labelTypeOf2DObject.Text = Program.language.TypeOf2DObjectHyperbola;

                // Labels with line's name.
                labelName1Of2DObject.Text = nameDone;   // Not neccessary.
                labelName2Of2DObject.Text = nameDone;   // Not neccessary.
                labelName3Of2DObject.Text = nameDone;
                labelName4Of2DObject.Text = nameDone;
                labelName5Of2DObject.Text = nameDone;   // Not neccessary.

                // Equations.
                labelEquation1Of2DObject.Text = hyperbola.GetMidpointAndParametersInfo();
                labelEquation2Of2DObject.Text = hyperbola.GetMainVertecesAndFociInfo();
                labelEquation3Of2DObject.Text = hyperbola.GetHyperbolaEquation();
                labelEquation4Of2DObject.Text = hyperbola.GetGeneralEquation();
                labelEquation5Of2DObject.Text = hyperbola.GetAsymptotesGeneralEquation();

                ShowOrHideControls(false, new bool[] { false, false, true, true, false });
                this.Width = thisWidthForHyperbola;   // So the information can fit in.
            }
            else if (object2D is Parabola)
            {
                Parabola parabola = (Parabola)object2D;
                string nameDone = string.Concat(comboBoxDisplay2DObject.Text, charsAfterName);

                // Type.
                labelTypeOf2DObject.Text = Program.language.TypeOf2DObjectParabola;

                // Labels with line's name.
                labelName1Of2DObject.Text = nameDone;   // Not neccessary.
                labelName2Of2DObject.Text = nameDone;   // Not neccessary.
                labelName3Of2DObject.Text = nameDone;   // Not neccessary.
                labelName4Of2DObject.Text = nameDone;
                labelName5Of2DObject.Text = nameDone;

                // Equations.
                labelEquation1Of2DObject.Text = parabola.GetVertexAndFocusInfo();
                labelEquation2Of2DObject.Text = parabola.GetDirectrixEquation();
                labelEquation3Of2DObject.Text = parabola.GetOpeningDirection();
                labelEquation4Of2DObject.Text = parabola.GetParabolaEquation();
                labelEquation5Of2DObject.Text = parabola.GetGeneralEquation();

                ShowOrHideControls(false, new bool[] { false, false, false, true, true });
                this.Width = myDefaultWidth;
            }
            // To continue, use the pattern you can find above (the best is if (object2D is Line) { ... }).
            // else if (...)
            else
                throw new NotImplementedException(Program.language.ExceptionNotImplementedText);

            textBoxXCoordinate2D.Enabled = true;
            textBoxYCoordinate2D.Enabled = true;
            buttonCheckPoint2D.Visible = true;
            labelAnswerPoint2D.Visible = false;
        }

        /// <summary>
        /// Checks if a specified point is a part of the selected object.
        /// Changes labelAnswerPoint2D.Text to Language.AnswerYes or Language.AnswerNo.
        /// Sets labelAnswerPoint2D.Visibility to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckPoint2D_Click(object sender, EventArgs e)
        {
            if (!EnsureComboBoxIsNotEmpty())
                return;

            if (Program.selectedObject != null)
            {
                MyPoint point = new MyPoint(ParseTextBoxWithoutException(textBoxXCoordinate2D), ParseTextBoxWithoutException(textBoxYCoordinate2D));
                if (Program.selectedObject.IsPointOfThisObject(point))
                    labelAnswerPoint2D.Text = Program.language.AnswerYes;
                else
                    labelAnswerPoint2D.Text = Program.language.AnswerNo;
                labelAnswerPoint2D.Visible = true;
                buttonCheckPoint2D.Visible = false;
            }           
        }

        private void buttonAdd2DFromDisplay2D_Click(object sender, EventArgs e)
        {
            Program.selectedObject = null;
            GoToOtherForm(Program.formAdd2D);
        }

        private void buttonMenuFrom2D_Click(object sender, EventArgs e)
        {
            Program.selectedObject = null;
            GoToOtherForm(Program.formMainMenu);
        }

        private void buttonCheckParametricEquation2D_Click(object sender, EventArgs e)
        {
            GoToOtherForm(Program.formCheckParametricEquation);
        }

        // Handling visibility.
        private void textBoxXCoordinate2D_TextChanged(object sender, EventArgs e)
        {
            if (!buttonCheckPoint2D.Visible)
            {
                buttonCheckPoint2D.Visible = true;
                labelAnswerPoint2D.Visible = false;
            }                
        }

        private void textBoxYCoordinate2D_TextChanged(object sender, EventArgs e)
        {
            if (!buttonCheckPoint2D.Visible)
            {
                buttonCheckPoint2D.Visible = true;
                labelAnswerPoint2D.Visible = false;
            }                
        }

        // Handling input.
        private void textBoxXCoordinate2D_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBoxYCoordinate2D_KeyPress(object sender, KeyPressEventArgs e)
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
