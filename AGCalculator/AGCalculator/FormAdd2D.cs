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
    public partial class FormAdd2D : Form, IMyForm
    {
        public FormAdd2D()
        {
            InitializeComponent();
        }

        const int spacesForInfo = 4;
        const string comboBoxDefaultValue = "";
        const string defaultTextBoxText = "0,00";
        const string addToLabelName = " = ";
        const int nameMaxLength = 3;

        private string selectedType;
        private string selectedInfo;

        public void LoadInCorrectLanguage(Language lang)
        {
            this.Text = lang.FormAdd2DText;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Changing Visible.
            HideAllInfoSpaces();

            // Changing Enabled.
            textBoxAddName2D.Enabled = false;
            buttonAdd2DObject.Enabled = false;

            textBoxAddName2D.Text = "";
            textBoxAddName2D.MaxLength = nameMaxLength;
            labelDescAdd2D.Text = lang.LabelDescAdd2DText;
            labelDescChooseTypeOf2D.Text = lang.LabelDescChooseTypeOf2DText;
            labelDescSelectInfo2D.Text = lang.LabelDescSelectInfo2DText;
            labelDescAddName2D.Text = lang.LabelDescAddName2DText;
            buttonAdd2DObject.Text = lang.ButtonAdd2DObjectText;
            buttonMenuFromAdd2D.Text = lang.ButtonMenuFrom2DText;
            buttonDisplayFromAdd2D.Text = lang.ButtonGoToDisplayText;            
            comboBoxTypeOf2D.DropDownStyle = ComboBoxStyle.DropDownList;            
            comboBoxInfo2D.Enabled = false;
            comboBoxInfo2D.DropDownStyle = ComboBoxStyle.DropDownList;
            // Adjusting types of items saved in comboBox according to the selected language.
            if (!comboBoxTypeOf2D.Items.Contains(Program.typeObject2D[0]))  // Note: Change if adding more languages with the same expression for "line".
            {
                comboBoxTypeOf2D.Items.Clear();
                foreach (string type in Program.typeObject2D)
                    comboBoxTypeOf2D.Items.Add(type);
            }

            // Not working.
            comboBoxInfo2D.Text = comboBoxDefaultValue;
            comboBoxTypeOf2D.Text = comboBoxDefaultValue;
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
        /// Hides some information labels and input textBoxes.
        /// </summary>
        private void HideAllInfoSpaces()
        {
            // Changing Visible.
            labelSpecificationInfo2D.Visible = false;
            labelDesc2DInfo1.Visible = false;
            labelDesc2DInfo2.Visible = false;
            labelDesc2DInfo3.Visible = false;
            labelDesc2DInfo4.Visible = false;
            textBox2DInfo1.Visible = false;
            textBox2DInfo2.Visible = false;
            textBox2DInfo3.Visible = false;
            textBox2DInfo4.Visible = false;
        }

        /// <summary>
        /// Displays the correct amount of labels and textBoxes with correct values.
        /// </summary>
        /// <param name="lNames">Text properties of all labels that should be displayed.</param>
        private void ShowInfoSpaces(string[] lNames)
        {
            int count = lNames.Length;
            Label[] labels = new Label[spacesForInfo] { labelDesc2DInfo1, labelDesc2DInfo2, labelDesc2DInfo3, labelDesc2DInfo4 };
            TextBox[] textBoxes = new TextBox[spacesForInfo] { textBox2DInfo1, textBox2DInfo2, textBox2DInfo3, textBox2DInfo4 };

            for (int i = 0; i < count; i++)
            {
                labels[i].Text = lNames[i] + addToLabelName;
                labels[i].Visible = true;
                textBoxes[i].Text = defaultTextBoxText;
                textBoxes[i].Visible = true;
            }
        }

        /// <summary>
        /// Displays excatly the needed number of labels and textBoxes according to the user's choice.
        /// Changes the Text property of some labels according to the user's choice.
        /// </summary>
        /// <param name="info">Tells us how many labels and textBoxes to display with what Text.</param>
        private void ShowObjectInfo(ObjectInfo info)
        {
            HideAllInfoSpaces();    // For reset.

            labelSpecificationInfo2D.Text = info.Specification;
            labelSpecificationInfo2D.Visible = true;
            ShowInfoSpaces(info.LabelNames);

            textBoxAddName2D.Enabled = true;
            buttonAdd2DObject.Enabled = true;
        }

        private double ParseTextBoxWithoutException(TextBox textBox)
        {
            Language lang = Program.language;
            if (textBox.Text == "-" || textBox.Text == "-" + lang.DecimalPointText || textBox.Text == lang.DecimalPointText.ToString() || textBox.Text == "" || textBox.Text == null)
                return 0d;
            else
                return double.Parse(textBox.Text);
        }

        /// <summary>
        /// Creates a new instance of IObject2D according to the fields selectedType and selectedInfo in current language.
        /// </summary>
        /// <param name="language">The language all information is in.</param>
        /// <returns>A new instance of IObject2D.</returns>
        private IObject2D GetNew2DObject(Language language)
        {
            // We know that selectedInfo is one of the following values (comboBoxs' SelectedIndeces have been changed).
            // The same with selectedType.
            if (this.selectedInfo == language.InfoTwoPoints)
            {
                if (this.selectedType == language.TypeOf2DObjectLine)
                {
                    double x = ParseTextBoxWithoutException(textBox2DInfo1);
                    double y = ParseTextBoxWithoutException(textBox2DInfo2);
                    MyPoint A = new MyPoint(x, y);
                    x = ParseTextBoxWithoutException(textBox2DInfo3);
                    y = ParseTextBoxWithoutException(textBox2DInfo4);
                    MyPoint B = new MyPoint(x, y);

                    return new LineIn2D(A, B);
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoPointAndDirectionalVector)
            {
                if (this.selectedType == language.TypeOf2DObjectLine)
                {
                    double x = ParseTextBoxWithoutException(textBox2DInfo1);
                    double y = ParseTextBoxWithoutException(textBox2DInfo2);
                    MyPoint A = new MyPoint(x, y);
                    x = ParseTextBoxWithoutException(textBox2DInfo3);
                    y = ParseTextBoxWithoutException(textBox2DInfo4);
                    Vector u = new Vector(x, y);

                    return new LineIn2D(A, u);  // This order neccesary for point + directional vector.
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoPointAndNormalVector)
            {
                if (this.selectedType == language.TypeOf2DObjectLine)
                {
                    double x = ParseTextBoxWithoutException(textBox2DInfo1);
                    double y = ParseTextBoxWithoutException(textBox2DInfo2);
                    MyPoint A = new MyPoint(x, y);
                    x = ParseTextBoxWithoutException(textBox2DInfo3);
                    y = ParseTextBoxWithoutException(textBox2DInfo4);
                    Vector n = new Vector(x, y);

                    return new LineIn2D(n, A);  // This order neccesary for point + normal vector.
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoGeneralEquation)
            {
                if (this.selectedType == language.TypeOf2DObjectLine)
                {
                    double a = ParseTextBoxWithoutException(textBox2DInfo1);
                    double b = ParseTextBoxWithoutException(textBox2DInfo2);
                    double c = ParseTextBoxWithoutException(textBox2DInfo3);

                    return new LineIn2D(a, b, c);
                }
                else if (this.selectedType == language.TypeOf2DObjectCircle)
                {
                    double a = ParseTextBoxWithoutException(textBox2DInfo1);
                    double b = ParseTextBoxWithoutException(textBox2DInfo2);
                    double c = ParseTextBoxWithoutException(textBox2DInfo3);

                    return new Circle(a, b, c);
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoSlopeEquation)
            {
                if (this.selectedType == language.TypeOf2DObjectLine)
                {
                    double k = ParseTextBoxWithoutException(textBox2DInfo1);
                    double q = ParseTextBoxWithoutException(textBox2DInfo2);

                    return new LineIn2D(k, q);
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoMidpointAndRadius)
            {
                if (this.selectedType == language.TypeOf2DObjectCircle)
                {
                    double x = ParseTextBoxWithoutException(textBox2DInfo1);
                    double y = ParseTextBoxWithoutException(textBox2DInfo2);
                    double r = ParseTextBoxWithoutException(textBox2DInfo3);

                    return new Circle(new MyPoint(x, y), r);
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoEllipseParameters)
            {
                if (this.selectedType == language.TypeOf2DObjectEllipse)
                {
                    double m = ParseTextBoxWithoutException(textBox2DInfo1);
                    double n = ParseTextBoxWithoutException(textBox2DInfo2);
                    double a = ParseTextBoxWithoutException(textBox2DInfo3);
                    double b = ParseTextBoxWithoutException(textBox2DInfo4);

                    return new Ellipse(new MyPoint(m, n), a, b);
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoHyperbolaParametersAB)
            {
                if (this.selectedType == language.TypeOf2DObjectHyperbola)
                {
                    double m = ParseTextBoxWithoutException(textBox2DInfo1);
                    double n = ParseTextBoxWithoutException(textBox2DInfo2);
                    double a = ParseTextBoxWithoutException(textBox2DInfo3);
                    double b = ParseTextBoxWithoutException(textBox2DInfo4);

                    return new Hyperbola(new MyPoint(m, n), a, b, true);    // OpenLeftAndRight isn't supported here.
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoHyperbolaParametersEA)
            {
                if (this.selectedType == language.TypeOf2DObjectHyperbola)
                {
                    double m = ParseTextBoxWithoutException(textBox2DInfo1);
                    double n = ParseTextBoxWithoutException(textBox2DInfo2);
                    double e = ParseTextBoxWithoutException(textBox2DInfo3);
                    double a = ParseTextBoxWithoutException(textBox2DInfo4);

                    return new Hyperbola(e, a, new MyPoint(m, n), true);    // OpenLeftAndRight isn't supported here.
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoHyperbolaParametersEB)
            {
                if (this.selectedType == language.TypeOf2DObjectHyperbola)
                {
                    double m = ParseTextBoxWithoutException(textBox2DInfo1);
                    double n = ParseTextBoxWithoutException(textBox2DInfo2);
                    double e = ParseTextBoxWithoutException(textBox2DInfo3);
                    double b = ParseTextBoxWithoutException(textBox2DInfo4);

                    return new Hyperbola(e, new MyPoint(m, n), b, true);    // OpenLeftAndRight isn't supported here.
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            else if (this.selectedInfo == language.InfoParabolaVertexAndFocus)
            {
                if (this.selectedType == language.TypeOf2DObjectParabola)
                {
                    double m = ParseTextBoxWithoutException(textBox2DInfo1);
                    double n = ParseTextBoxWithoutException(textBox2DInfo2);
                    double fx = ParseTextBoxWithoutException(textBox2DInfo3);
                    double fy = ParseTextBoxWithoutException(textBox2DInfo4);

                    return new Parabola(new MyPoint(m, n), new MyPoint(fx, fy));
                }
                else
                    throw new Exception(language.ExceptionNotImplementedText);
            }
            // Continue by using following pattern:
            // else if(info == ...) { if(type == ...) {  } else { throw new Exception(language.ExceptionNotImplementedText); } }
            // more types can be used in one info
            else
                throw new Exception(language.ExceptionNotImplementedText);
        }

        private void buttonAdd2DObject_Click(object sender, EventArgs e)
        {
            Language language = Program.language;
            string objectName = textBoxAddName2D.Text;

            if (objectName == "" || objectName == null)
            {
                MessageBox.Show(language.MessageEmptyNameText, language.MessageEmptyNameCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.ActiveControl = textBoxAddName2D;
                return;
            }

            if (Program.objects2D.ContainsKey(objectName))
            {
                MessageBox.Show(language.MessageNameExistsText, language.MessageNameExistsCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.ActiveControl = textBoxAddName2D;
                return;
            }

            DialogResult result = MessageBox.Show(language.MessageSureToAddObjectText, language.MessageSureToAddObjectCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                IObject2D object2D = null;
                try
                {
                    object2D = GetNew2DObject(language);
                }
                catch (ArgumentException argException)
                {
                    string wholeMessage = string.Concat(language.MessageObjectCreationFailedText, '\n', language.MessageOriginalExceptionMessageText, " ", argException.Message);
                    MessageBox.Show(wholeMessage, language.MessageObjectCreationFailedCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Program.objects2D.Add(objectName, object2D);
                Program.object2DNames.Add(objectName);
                MessageBox.Show(language.MessageObjectAddedText, language.MessageObjectAddedCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInCorrectLanguage(language);
            }
        }

        private void buttonMenuFromAdd2D_Click(object sender, EventArgs e)
        {
            GoToOtherForm(Program.formMainMenu);
        }

        private void comboBoxTypeOf2D_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language language = Program.language;

            HideAllInfoSpaces();
            textBoxAddName2D.Enabled = false;
            buttonAdd2DObject.Enabled = false;

            if (!comboBoxInfo2D.Enabled)
                comboBoxInfo2D.Enabled = true;
            this.selectedType = comboBoxTypeOf2D.Text;
            List<string> newInfoList;
            comboBoxInfo2D.Items.Clear();

            // Line.
            if (this.selectedType == language.TypeOf2DObjectLine)
            {
                newInfoList = language.GetListInfoNeededLine2D();
                foreach (string info in newInfoList)
                    comboBoxInfo2D.Items.Add(info);
            }
            // Circle.
            else if (this.selectedType == language.TypeOf2DObjectCircle)
            {
                newInfoList = language.GetListInfoNeededCircle2D();
                foreach (string info in newInfoList)
                    comboBoxInfo2D.Items.Add(info);
            }
            // Ellipse.
            else if (this.selectedType == language.TypeOf2DObjectEllipse)
            {
                newInfoList = language.GetListInfoNeededEllipse2D();
                foreach (string info in newInfoList)
                    comboBoxInfo2D.Items.Add(info);
            }
            // Hyperbola.
            else if (this.selectedType == language.TypeOf2DObjectHyperbola)
            {
                newInfoList = language.GetListInfoNeededHyperbola2D();
                foreach (string info in newInfoList)
                    comboBoxInfo2D.Items.Add(info);
            }
            // Parabola.
            else if (this.selectedType == language.TypeOf2DObjectParabola)
            {
                newInfoList = language.GetListInfoNeededParabola2D();
                foreach (string info in newInfoList)
                    comboBoxInfo2D.Items.Add(info);
            }
            // Unhandled type.
            else
            {
                throw new NotImplementedException(language.ExceptionNotImplementedText);
            }
        }

        private void comboBoxInfo2D_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ObjectInfo> infoPossibilities = new List<ObjectInfo>();
            this.selectedInfo = comboBoxInfo2D.Text;
            Language language = Program.language;
            bool nameFound = false;

            // Line.
            if (this.selectedType == language.TypeOf2DObjectLine)
            {
                LineIn2D abstractLine = new LineIn2D();
                infoPossibilities = abstractLine.GetAllInfoPossibilities(language);

                foreach (ObjectInfo info in infoPossibilities)
                {
                    if (selectedInfo == info.Name)
                    {
                        ShowObjectInfo(info);
                        nameFound = true;
                        break;
                    }
                }
            }
            // Circle.
            else if (this.selectedType == language.TypeOf2DObjectCircle)
            {
                Circle abstractCircle = new Circle();
                infoPossibilities = abstractCircle.GetAllInfoPossibilities(language);

                foreach (ObjectInfo info in infoPossibilities)
                {
                    if (selectedInfo == info.Name)
                    {
                        ShowObjectInfo(info);
                        nameFound = true;
                        break;
                    }
                }
            }
            // Ellipse.
            else if (this.selectedType == language.TypeOf2DObjectEllipse)
            {
                Ellipse abstractEllipse = new Ellipse();
                infoPossibilities = abstractEllipse.GetAllInfoPossibilities(language);

                foreach (ObjectInfo info in infoPossibilities)
                {
                    if (selectedInfo == info.Name)
                    {
                        ShowObjectInfo(info);
                        nameFound = true;
                        break;
                    }
                }
            }
            // Hyperbola.
            else if (this.selectedType == language.TypeOf2DObjectHyperbola)
            {
                Hyperbola abstractHyperbola = new Hyperbola();
                infoPossibilities = abstractHyperbola.GetAllInfoPossibilities(language);

                foreach (ObjectInfo info in infoPossibilities)
                {
                    if (selectedInfo == info.Name)
                    {
                        ShowObjectInfo(info);
                        nameFound = true;
                        break;
                    }
                }
            }
            // Parabola.
            else if (this.selectedType == language.TypeOf2DObjectParabola)
            {
                Parabola abstractParabola = new Parabola();
                infoPossibilities = abstractParabola.GetAllInfoPossibilities(language);

                foreach (ObjectInfo info in infoPossibilities)
                {
                    if (selectedInfo == info.Name)
                    {
                        ShowObjectInfo(info);
                        nameFound = true;
                        break;
                    }
                }
            }
            // Unhandled type.
            else
            {
                throw new Exception(language.ExceptionNotImplementedText);
            }
            // Change info textbox, ...
            // Create a method: HideAllInfo() -> run by previous event

            // Nothing was chosen.
            if (!nameFound)
                throw new Exception(language.ExceptionWrongObjectNameText);
        }

        private void buttonDisplayFromAdd2D_Click(object sender, EventArgs e)
        {
            if (Program.objects2D.Count == 0)
                MessageBox.Show(Program.language.MessageNoObejctsText, Program.language.MessageNoObejctsCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                GoToOtherForm(Program.formDisplay2D);
        }

        // Handling input.
        private void textBox2DInfo1_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBox2DInfo2_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBox2DInfo3_KeyPress(object sender, KeyPressEventArgs e)
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
        private void textBox2DInfo4_KeyPress(object sender, KeyPressEventArgs e)
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
