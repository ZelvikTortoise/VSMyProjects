using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Riskuj
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
        }

        private void GoToFormGame(FormGame form)
        {
            if (!this.Visible)
                return;

            this.Visible = false;
            form.Location = this.Location;
            // form.StartPosition = FormStartPosition.Manual; // not useful because this form is much smaller than the next one
            form.FormClosing += delegate { Application.Exit(); };
            form.Visible = true;
            form.CallMeWhenAccessingThisForm();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialogLoading.ShowDialog() == DialogResult.OK)
                textBoxFilePath.Text = openFileDialogLoading.FileName;
        }

        public const int roundNum = 2;
        public const int questionAreaNum = 6;
        public const int valueNum = 5;
        public const int minValue1 = 1000;
        public const int maxValue1 = 5000;
        public const int iterateValue1 = 1000;
        public const int minValue2 = 6000;
        public const int maxValue2 = 10000;
        public const int iterateValue2 = 1000;
        public const string correctAnswerBeginning = "!";
        public const string validFileSuffix = "txt";
        public const string modelInputFileName = "Vzorový formát souboru.txt";

        private void buttonFormatInfo_Click(object sender, EventArgs e)
        {
            bool printModelInputFile = false;       // Option to make a new model input file.

            StringBuilder sb = new StringBuilder();
            int start;
            int end;
            int iterate;

            string title;
            string message;

            for (int round = 1; round <= roundNum; round++)
            {
                int premiumQuestionCount = 0;
                for (int area = 1; area <= questionAreaNum; area++)
                {
                    sb.Append("Název okruhu otázek číslo ");
                    sb.Append(area);
                    sb.Append(" (");
                    sb.Append(round);
                    sb.Append(". kolo)\n");

                    if (round == 1)
                    {
                        start = minValue1;
                        end = maxValue1;
                        iterate = iterateValue1;
                    }
                    else if (round == 2)
                    {
                        start = minValue2;
                        end = maxValue2;
                        iterate = iterateValue2;
                    }
                    else
                        throw new NotImplementedException("Only two rounds per game are supported.");

                    for (int value = start; value <= end; value += iterate)
                    {
                        sb.Append("Otázka okruhu ");
                        sb.Append(area);
                        sb.Append(" za ");
                        sb.Append(value);
                        sb.Append(" bodů (");
                        sb.Append(round);
                        sb.Append(". kolo)\n");
                    }
                    premiumQuestionCount++;
                    sb.Append("Prémiová otázka okruhu ");
                    sb.Append(area);
                    sb.Append(" (");
                    sb.Append(round);
                    sb.Append(". kolo) + dále její možnosti:\n");
                    if (premiumQuestionCount % 3 == 1)
                    {
                        sb.Append(correctAnswerBeginning);
                    }
                    sb.Append("První možnost (ta za a)\n");
                    if (premiumQuestionCount % 3 == 2)
                    {
                        sb.Append(correctAnswerBeginning);
                    }
                    sb.Append("Druhá možnost (ta za b)\n");
                    if (premiumQuestionCount % 3 == 0)
                    {
                        sb.Append(correctAnswerBeginning);
                    }
                    sb.Append("Třetí možnost (ta za c)\n");
                }
                if (round == 1)
                    sb.Append("\n");
            }

            title = "Vzorový formát vstupního souboru (.txt)";
            message = sb.ToString();

            if (printModelInputFile)
            {
                using (StreamWriter sw = new StreamWriter(modelInputFileName))
                {
                    sw.Write(message);
                }
            }
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Vzorový formát souboru by měl být přiložen k programu v textovém souboru s názvem \"Vzorový formát souboru.txt\".", "Kde najít vzorový formát přehledně", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool IsAnythingEmpty(string[,] questions, int round)
        {
            for (int j = 0; j < questionAreaNum; j++)
            {
                for (int i = 0; i < valueNum + 5; i++)
                {
                    if (questions[i, j] == "" || questions[i, j] == null)
                    {
                        MessageBox.Show($"Textové pole číslo {i + 1} v okuruhu otázek číslo {j + 1} ({round}. kolo) je prázdné!\nSoubor není správně naformátovaný.\nDoplňte do vstupního souboru chybějící text.", "Prázdná pole ve vstupním textovém souboru", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        return true;
                    }                        
                }
            }

            return false;
        }

        private bool AnswersContainExcatlyOneCorrect(string[,] questions, int round)
        {
            // Answers for the premium question: questions[7, j], questions[8, j], questions[9, j] where j goes from 0 to questionAreaNum - 1.
            bool foundCorrect;

            for (int j = 0; j < questionAreaNum; j++)
            {
                foundCorrect = false;
                for (int i = 7; i <= 9; i++)
                {
                    if (questions[i, j].StartsWith(correctAnswerBeginning))
                    {
                        if (foundCorrect)
                        {
                            MessageBox.Show($"Ve vstupním textovém souboru je u prémiové otázky okruhu číslo {j + 1} ({round}. kolo) označena více než jedna odpověď jako správná.\nV pořadí druhá odpověď označená jako správná: za {(i == 8 ? "b)" : "c)")}\nRada: Znak '{correctAnswerBeginning}' na začátku textového pole značí správnou odpověď; je nutné, aby u každé prémiové otázky byla označena právě jedna správná odpověď.\n\nZnění vadné odpovědi:\n{questions[i, j]}", "Více správných odpovědí u prémiové otázky", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            return false;
                        }
                        else
                        {
                            foundCorrect = true;
                        }
                    }                        
                }
                if (foundCorrect == false)
                {
                    MessageBox.Show($"Ve vstupním textovém souboru není u prémiové otázky okruhu číslo {j + 1} ({round}. kolo) označena správná odpověď.\nProsím, označte správnou odpověď ve vstupním souboru před tím, než soubor znovu načtete.\nRada: Správnou odpověď značí na začátku textového pole znak '{correctAnswerBeginning}'.\n\nZnění prémiové otázky bez označené odpovědi:\n{questions[6, j]}", "Neoznačena správná odpověď u prémiové otázky", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private bool IsTextFileCorrect()
        {
            List<string[,]> questionFields = new List<string[,]> { Program.questions1, Program.questions2 };
            int round;

            for (int i = 0; i < questionFields.Count; i++)
            {
                round = i + 1;
                if (IsAnythingEmpty(questionFields[i], round) || !AnswersContainExcatlyOneCorrect(questionFields[i], round))
                {
                    // Showing MessageBoxes is handled in the functions in the if-condition.
                    return false;
                }
            }

            return true;
        }

        private bool IsAnyTeamNameEmpty()
        {
            TextBox[] teamNamesTextBoxes = new TextBox[4] { textBoxTeam1, textBoxTeam2, textBoxTeam3, textBoxTeam4 };
            for (int i = 0; i < teamNamesTextBoxes.Length; i++)
            {
                if (teamNamesTextBoxes[i].Text == "" || teamNamesTextBoxes[i].Text == null)
                {
                    MessageBox.Show($"Tým číslo {i + 1} nemá jméno. Prosím, pojmenujte tým, i kdyby měl mít 0 členů.\nTakové týmy je dobré pojmenovat jako \"Prázdný 1\", apod.", "Nepojmenovaný tým", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }

            return false;
        }

        private bool AreThereDualTeamNames()
        {
            TextBox[] teamNameTextBoxes = new TextBox[4] { textBoxTeam1, textBoxTeam2, textBoxTeam3, textBoxTeam4 };
            string[] teamNames = new string[4] { "", "", "", "" };

            for (int i = 0; i < teamNames.Length; i++)
            {
                teamNames[i] = teamNameTextBoxes[i].Text;

                for (int j = 0; j < i; j++)
                {
                    if (teamNames[i] == teamNames[j])
                    {
                        MessageBox.Show($"Tým číslo {j + 1} má stejné jméno jako tým číslo {i + 1}. Prosím, přejmenujte aspoň jeden z týmů tak, aby se každá dvě jména týmů lišila.\nPokud chcete, aby se týmy opravdu jmenovaly stejně, použijte číslování (\"Tým 1\", \"Tým 2\", apod.).", "Dva týmy mají stejné jméno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
            }

            return false;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (textBoxFilePath.Text == "" || textBoxFilePath.Text == null)
            {
                MessageBox.Show("Nejprve vyberte vstupní textový soubor v požadovaném formátu.\n\nRada: Použijte tlačítko s názvem \"" + buttonBrowse.Text + "\".", "Žádná cesta nebyla zadána", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!textBoxFilePath.Text.EndsWith(".txt"))
            {
                string[] parts = textBoxFilePath.Text.Split('.');
                MessageBox.Show($"Soubory s příponou \".{parts[parts.Length - 1]}\" nejsou našim programem podporovány.\nVyberte prosím textový soubor s příponou \".{validFileSuffix}\".", "Chybná přípona", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (IsAnyTeamNameEmpty())
            {
                // Showing of the MessageBox is handled in the function.
                return;
            }
            else if (AreThereDualTeamNames())
            {
                // Showing of the MessageBox is handled in the function.
                return;
            }
            
            try
            {
                string path = textBoxFilePath.Text;
                using (StreamReader sr = new StreamReader(path))
                {
                    // Areas in columns (as in the game interface):
                    Program.questions1 = new string[valueNum + 5, questionAreaNum];
                    Program.questions2 = new string[valueNum + 5, questionAreaNum];
                    // + 5 because of 1) the area name; 2) the premium question; 3), 4), 5) the answer possibilities for the premium question.
                    // NOTE: Values in columns will go from a premium question, over 5000 to 1000 (from top to bottom) in the game interface.
                    // IMPORTANT NOTE: Mark correct answers with "!" as a beginning character. Not implemented as a message.

                    for (int round = 1; round <= 2; round++)
                    {
                        for (int column = 0; column < questionAreaNum; column++)
                        {
                            for (int row = 0; row < valueNum + 5; row++)
                            {
                                if (round == 1)
                                    Program.questions1[row, column] = sr.ReadLine();
                                else if (round == 2)
                                    Program.questions2[row, column] = sr.ReadLine();
                                else
                                    throw new NotFiniteNumberException("Only two rounds per game are supported.");
                            }
                        }

                        if (round == 1)
                            sr.ReadLine();  // Reading the new line between two rounds in the input file.
                    }
                }

                if (IsTextFileCorrect())
                {
                    Program.maxTime = (int)Math.Round(numericUpDownMaxTime.Value);
                    Program.adjective = textBoxAdjective.Text;
                    Program.teamName1 = textBoxTeam1.Text;
                    Program.teamName2 = textBoxTeam2.Text;
                    Program.teamName3 = textBoxTeam3.Text;
                    Program.teamName4 = textBoxTeam4.Text;

                    MessageBox.Show("Soubor byl úspěšně nahrán.", "Nahrávání vstupního souboru dokončeno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GoToFormGame(Program.formGame);
                }                
            }
            catch
            {
                MessageBox.Show("Došlo k chybě. Formát vstupního souboru je vadný.\n\nZkontrolujte ho a opravte ho.", "Neplatný formát vstupního souboru", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
