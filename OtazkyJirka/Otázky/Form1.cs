using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Security;
using System.Reflection;

namespace Otázky
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random random = new Random();

        private List<Chapter> chapters;
        private List<string> currentQuestions;
        private const bool seePath = false;
        private const char chapterNameSymbol = '#';
        private const string studentChapterName = "Studenti";
        private const char usedStudent = '!';
        private const char priorityStudent = '+';
        private bool priority;  // is the current student set using priority for selection?
        private bool studentsFound; // is there a chapter of only students?
        private const int btnWidth = 120;
        private const int btnHeight = 80;
        private const int horizontalSpace = 50;
        private const int verticalSpace = 20;
        private const int buttonsInRow = 5; // doesn't work properly, don't change!

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length < 5 || openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 4) != ".txt")
            {
                // If not .txt, resets the window layout
                MessageBox.Show("Neplatný soubor", "Chyba");
                lblSelectedFile.Text = "";
                ResetChapterSelectButtons();
                chapters.Clear();
                ResetWindowSize();
            } else
            {
                lblSelectedFile.Text = openFileDialog1.FileName;
                try
                {
                    LoadFile();
                }
                catch
                {
                    // If not compatible, resets the window layout
                    ResetWindowSize();
                    MessageBox.Show("Nekompatibilní zdrojový soubor", "Chyba");
                }
            }
        }

        private void LoadFile()
        {
            // Resets selection buttons and clears the main list
            if (chapters != null)
            {
                ResetChapterSelectButtons();
            }
            
            chapters = new List<Chapter>();


            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                // Sets the first chapter
                int i = 1;
                int currentStartIndex = 1;
                string currentChapterName = sr.ReadLine().Substring(1);

                while (!sr.EndOfStream)
                {
                    string newLine = sr.ReadLine();
                    if (newLine[0] == chapterNameSymbol)
                    {
                        // Records finished chapter, starts the next one
                        chapters.Add(new Chapter(currentChapterName, currentStartIndex, i - currentStartIndex));
                        currentChapterName = newLine.Substring(1);
                        currentStartIndex = i + 1;
                    }
                    i++;
                }
                chapters.Add(new Chapter(currentChapterName, currentStartIndex, i - currentStartIndex));
            }
            
            ResetQuestionControls();
            LoadButtons();
        }
        
        private void LoadButtons()
        {
            int i;
            studentsFound = false;
            for (i = 1; i <= chapters.Count; i++)
            {
                Button newBtn = new Button();
                if (chapters[i-1].ChapterName == studentChapterName)
                {
                    studentsFound = true;
                    newBtn.Name = "BtnSelectStudents";
                    newBtn.Text = String.Format("{0} (celkem: {1})", chapters[i - 1].ChapterName, chapters[i - 1].TotalQuestions);
                    newBtn.Font = new Font(newBtn.Font.FontFamily, 10, FontStyle.Bold);
                    newBtn.Location = new Point(horizontalSpace + ((i - 1) % buttonsInRow) * (btnWidth + horizontalSpace), 130 + (i - 1) / buttonsInRow * (btnHeight + verticalSpace));
                    newBtn.Size = new Size(btnWidth, btnHeight);
                    newBtn.Click += LoadChapter;
                    newBtn.Tag = i;
                }
                else
                {
                    newBtn.Name = "BtnSelectChapter" + (studentsFound ? i - 1 : i);
                    newBtn.Text = String.Format("{0} (otázek: {1})", chapters[i - 1].ChapterName, chapters[i - 1].TotalQuestions);
                    newBtn.Font = new Font(newBtn.Font.FontFamily, 10);
                    newBtn.Location = new Point(horizontalSpace + ((i - 1) % buttonsInRow) * (btnWidth + horizontalSpace), 130 + (i - 1) / buttonsInRow * (btnHeight + verticalSpace));
                    newBtn.Size = new Size(btnWidth, btnHeight);
                    newBtn.Click += LoadChapter;
                    newBtn.Tag = i;
                }

                this.Controls.Add(newBtn);
            }
            this.Size = new Size(buttonsInRow * (btnWidth + horizontalSpace) + horizontalSpace + 30, 200 + (1 + (chapters.Count - 1) / buttonsInRow) * (btnHeight + verticalSpace));
        }

        private void LoadChapter(object sender, EventArgs e)
        {
            int chapterIndex = Convert.ToInt32((sender as Button).Tag); 

            ResetChapterSelectButtons();

            LoadQuestions(chapterIndex);
        }

        private void LoadQuestions(int index)
        {
            Chapter currentChapter = chapters[index - 1];
            currentQuestions = new List<string>();
            priority = false;

            // Loads questions into the main data field
            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                // Getting to the starting index of the first question
                for (int i = 0; i < currentChapter.FirstQuestionIndex; i++)
                {
                    sr.ReadLine();
                }

                // Loading the student chapter
                if (currentChapter.ChapterName == studentChapterName)
                {
                    String line;                    
                    for (int i = 0; i < currentChapter.TotalQuestions; i++)
                    {
                        line = sr.ReadLine().Trim();
                        if (priority)   // priority mode – discarding all non-priority students
                        {
                            if (line.StartsWith(priorityStudent.ToString()))
                            {
                                currentQuestions.Add(line.Substring(1));
                            }
                        }
                        else if (line.StartsWith(priorityStudent.ToString()))   // we found our first priority student
                        {
                            priority = true;
                            currentQuestions.Clear();
                            currentQuestions.Add(line.Substring(1));
                        }
                        else if (!line.StartsWith(usedStudent.ToString()))  // student was already used
                        {
                            currentQuestions.Add(line);
                        }                     
                        // else: do not add the student
                    }
                }
                // Loading a chapter with question
                else
                {
                    for (int i = 0; i < currentChapter.TotalQuestions; i++)
                    {
                        currentQuestions.Add(sr.ReadLine());
                    }
                }
            }

            // Loads necessary controls

            this.Size = new Size(830, 500);

            // Turns off selectFile controls
            btnSelectFile.Visible = false;
            lblSelectedFile.Visible = false;

            lblQuestionsRemaining.Visible = true;
            lblQuestionsRemaining.Text = "Otázek: " + currentQuestions.Count;

            lblChapterTitle.Visible = true;
            lblChapterTitle.Text = currentChapter.ChapterName;

            lblQuestion.Location = new Point(30, this.Size.Height - 120);
            lblQuestion.Visible = true;
            
            btnGenerateQuestion.Visible = true;
            btnGenerateQuestion.Location = new Point(250, this.Size.Height - 250);
            btnGenerateQuestion.Tag = 'G';

            btnChangeChapter.Visible = true;
        }

        private void btnGenerateQuestion_Click(object sender, EventArgs e)
        {
            int i = random.Next(0, currentQuestions.Count);
            lblQuestion.Text = currentQuestions[i];
            currentQuestions.RemoveAt(i);

            lblQuestionsRemaining.Text = "Otázek: " + currentQuestions.Count;
            // Changes the generateQuestion button to changeChapter button
            if (currentQuestions.Count == 0)
            {
                btnGenerateQuestion.Text = "Změnit kapitolu";
                btnGenerateQuestion.Click -= btnGenerateQuestion_Click;
                btnGenerateQuestion.Click += btnChangeChapter_Click;
                btnGenerateQuestion.Tag = 'C';
            }

            btnSelectStudent.Enabled = true;
            if (!btnSelectStudent.Visible && lblChapterTitle.Text == studentChapterName)
            {
                btnSelectStudent.Location = new Point(
                    btnGenerateQuestion.Location.X + (btnGenerateQuestion.Width - btnSelectStudent.Width) / 2,
                    btnGenerateQuestion.Location.Y + btnGenerateQuestion.Height + 30
                    );
                btnSelectStudent.Visible = true;
            }
        }

        private void btnChangeChapter_Click(object sender, EventArgs e)
        {
            ResetQuestionControls();

            LoadButtons();
        }

        // Turns off unnecessary controls
        private void ResetQuestionControls ()
        {
            lblQuestionsRemaining.Visible = false;
            lblChapterTitle.Visible = false;
            lblQuestion.Visible = false;
            lblQuestion.Text = "";
            btnGenerateQuestion.Visible = false;
            btnChangeChapter.Visible = false;
            btnSelectStudent.Visible = false;

            btnSelectFile.Visible = true;
            lblSelectedFile.Visible = seePath;

            btnGenerateQuestion.Text = "Generovat otázku";
            if (Convert.ToChar(btnGenerateQuestion.Tag) == 'C')
            {
                btnGenerateQuestion.Click -= btnChangeChapter_Click;
                btnGenerateQuestion.Click += btnGenerateQuestion_Click;
                btnGenerateQuestion.Tag = 'G';
            }
        }

        private void ResetChapterSelectButtons()
        {
            if (studentsFound)
            {
                // Removes the student selectchapter button
                Controls.Remove(Controls.Find("BtnSelectStudents", false)[0]);
                // Removes all selectchapter buttons
                for (int i = 1; i < chapters.Count; i++)
                {
                    Controls.Remove(Controls.Find("BtnSelectChapter" + i, false)[0]);
                }
            }
            else
            {
                // Removes all selectchapter buttons
                for (int i = 1; i <= chapters.Count; i++)
                {
                    Controls.Remove(Controls.Find("BtnSelectChapter" + i, false)[0]);
                }
            }            
        }

        private void ResetWindowSize()
        {
            this.Width = buttonsInRow * (btnWidth + horizontalSpace) + horizontalSpace + 30;
            this.Height = 200 + btnHeight + verticalSpace;
        }

        private void btnSelectStudent_Click(object sender, EventArgs e)
        {
            btnSelectStudent.Enabled = false;
            string previousText, newText;
            string toChange = lblQuestion.Text;
            string before, after;
            int indexOfChange;
            using (StreamReader sr = new StreamReader(lblSelectedFile.Text))
            {
                previousText = sr.ReadToEnd();
            }

            indexOfChange = previousText.IndexOf(toChange);
            before = priority ? previousText.Substring(0, indexOfChange - 1) : previousText.Substring(0, indexOfChange);
            after = previousText.Substring(indexOfChange);
            newText = priority ? before + after : before + usedStudent + after;
            // priority => removing the priorityStudent symbol; else => adding the usedStudent symbol

            using (StreamWriter sw = new StreamWriter(lblSelectedFile.Text))
            {
                sw.Write(newText);
            }
        }
    }

    public class Chapter
    {
        public string ChapterName { get; set; }
        public int FirstQuestionIndex { get; set; }
        public int TotalQuestions { get; set; }

        public Chapter(string _chapterName, int _firstQuestionIndex, int _totalQuestions)
        {
            this.ChapterName = _chapterName;
            this.FirstQuestionIndex = _firstQuestionIndex;
            this.TotalQuestions = _totalQuestions;
        }
    }

}
