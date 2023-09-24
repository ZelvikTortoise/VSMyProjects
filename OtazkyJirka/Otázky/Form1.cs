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
                    if (newLine[0] == '#')
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
            for (i = 1; i <= chapters.Count; i++)
            {
                Button newBtn = new Button();
                newBtn.Name = "BtnSelectChapter" + i;
                newBtn.Text = String.Format("{0} (otázek: {1})", chapters[i - 1].ChapterName, chapters[i - 1].TotalQuestions);
                newBtn.Location = new Point(50 + ((i - 1) % 5) * 150, 150 + (i - 1) / 5 * 100);
                newBtn.Size = new Size(100, 80);
                newBtn.Click += LoadChapter;
                newBtn.Tag = i;

                this.Controls.Add(newBtn);
            }
            i--;
            this.Size = new Size(830, 300 + ((i - 1) / 5) * 100);
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

            // Loads questions into the main data field
            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                for (int i = 0; i < currentChapter.FirstQuestionIndex; i++)
                {
                    sr.ReadLine();
                }

                for (int i = 0; i < currentChapter.TotalQuestions; i++)
                {
                    currentQuestions.Add(sr.ReadLine());
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

            btnSelectFile.Visible = true;
            lblSelectedFile.Visible = true;

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
            // Removes all selectchapter buttons
            for (int i = 1; i <= chapters.Count; i++)
            {
                Controls.Remove(Controls.Find("BtnSelectChapter" + i, false)[0]);
            }
        }

        private void ResetWindowSize()
        {
            this.Height = 300;
            this.Width = 830;
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
