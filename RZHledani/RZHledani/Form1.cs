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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace RZHledani
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
        }

        List<string> fileNames = new List<string>();
        List<string> foundRZs = new List<string>();
        string path;   // Path to the new file with results.
        bool pathWarned = false;
        bool dialogFiltersAdded = false;

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            string newFile = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Textové soubory (*.txt)|*.txt|Soubory csv (*.csv)|*.csv|Všechny soubory (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    newFile = openFileDialog.FileName;
                    if (!newFile.EndsWith(".csv"))
                    {
                        MessageBox.Show("Chyba. Soubor nebyl přidán.\nPodpora souborů mimo formát csv zatím nebyla implementována. Načtěte pouze soubory formátu csv.", "Chybí podpora formátu vstupního souboru", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        UpdateCount(true, newFile);
                    }
                }
            }
        }

        private void UpdateCount(bool add, string what)
        {
            if (add)
            {
                if (fileNames.Contains(what))
                {
                    MessageBox.Show("Tento soubor je již ve vyhledávání obsažen.\nNebyl tedy přidán znovu.", "Duplicitní soubor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    fileNames.Add(what);
                    // MessageBox.Show("Soubor byl úspěšně přidán.\nPoznámka: Neotvírejte a nepřesouvejte ho v průběhu práce programu.", "Přidáno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            else
            {
                if (what == "All")
                {
                    fileNames = new List<string>();                    
                }
                else
                {
                    fileNames.Remove(what);
                }                
            }
            UpdateLabelNumberOfFiles();
            UpdateButtons();
            Refresh();
        }

        private void UpdateLabelNumberOfFiles()
        {
            labelNumberOfFiles.Text = fileNames.Count.ToString();
        }

        private void UpdateButtons()
        {
            if (fileNames.Count < 1)
                buttonRemoveAllFiles.Enabled = false;
            else
                buttonRemoveAllFiles.Enabled = true;

            if (fileNames.Count < 2)
                buttonSearch.Enabled = false;
            else
                buttonSearch.Enabled = true;
        }

        
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            foundRZs = new List<string>();
            Working();
            
            if (radioButtonGenerateFileYes.Checked)
            {
                FileMode fmode = FileMode.Open;
                path = textBoxPathOut.Text;
                if (path == "" || path == null)
                {
                    pathWarned = true;
                    textBoxPathOut.BackColor = Color.Yellow;
                    MessageBox.Show("Zadejte cestu výstupního souboru.\nPoznámka: Můžete si ho vybrat pomocí tlačítka Vybrat soubor...", "Zadejte cestu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateFound(false);
                    return;
                }
                else if (fileNames.Contains(path))
                {
                    pathWarned = true;
                    MessageBox.Show("Soubor, který jste zvolili jako výstupní, je již evidován jako soubor vstupní. Zvolte jiný soubor, případně tlačítkem Odebrat vše resetujte vstupní soubory a zadejte je znovu.", "Zadejte cestu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateFound(false);
                    return;
                }
                
                if (checkBoxGenerateFileNew.Checked)
                {
                    fmode = FileMode.Create;
                    if (!Directory.Exists(path))
                    {
                        string[] pathParts = path.Split('\\');
                        if (pathParts.Length > 1)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < pathParts.Length - 1; i++)
                            {
                                sb.Append(pathParts[i]);
                                sb.Append('\\');
                            }

                            if (!Directory.Exists(path))
                            {
                                pathWarned = true;
                                textBoxPathOut.BackColor = Color.Yellow;
                                MessageBox.Show("Cesta \"" + path + "\" není platná (neudává žádný adresář).\nOpravte ji a zkuste to znovu.", "Neplatná cesta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                UpdateFound(false);
                                return;
                            }
                        }
                        else
                        {
                            pathWarned = true;
                            textBoxPathOut.BackColor = Color.Yellow;
                            MessageBox.Show("Cesta \"" + path + "\" není platná (neudává žádný adresář).\nOpravte ji a zkuste to znovu.", "Neplatná cesta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            UpdateFound(false);
                            return;
                        }
                    }

                    if (!path.EndsWith("\\"))
                        path += "\\RZ výsledky.csv";
                    else
                        path += "RZ výsledky.csv";
                }
                else
                {
                    if (!File.Exists(path))
                    {
                        pathWarned = true;
                        textBoxPathOut.BackColor = Color.Yellow;                        
                        MessageBox.Show("Soubor neexistuje nebo cesta \"" + path + "\" není platná.\nOpravte ji a zkuste to znovu.", "Neplatná cesta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateFound(false);
                        return;
                    }
                }

                try
                {
                    using (StreamReader srMain = new StreamReader(fileNames[0], Encoding.Default))
                    using (StreamWriter sw = new StreamWriter(new FileStream(path, fmode), Encoding.Default))
                    {
                        string row;
                        string[] data;
                        string searchedRZ;
                        StringBuilder sb;
                        while ((row = srMain.ReadLine()) != null)
                        {
                            data = row.Split(';');
                            if (data.Length <= 1 || data[1].Length <= 2)    // Empty rows and headers are being skipped.
                                continue;

                            searchedRZ = data[1];
                            if (searchedRZ.ToUpper().Contains('O'))
                            {
                                sb = new StringBuilder();
                                for (int i = 0; i < searchedRZ.Length; i++)
                                {
                                    if (searchedRZ.ToUpper()[i] == 'O')
                                        sb.Append('0');
                                    else
                                        sb.Append(searchedRZ[i]);
                                }

                                searchedRZ = sb.ToString();
                            }

                            if (Search(searchedRZ) && !foundRZs.Contains(searchedRZ))
                            {
                                foundRZs.Add(searchedRZ);
                                sw.WriteLine(row);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Zavřete nejdříve všechny vstupní i výstupní soubory a poté zkuste hledat znovu.\n\nZnění chyby: " + ex.Message, "Přístup odepřen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                UpdateFound(true);
            }
            else
            {
                try
                {
                    using (StreamReader srMain = new StreamReader(fileNames[0]))
                    {
                        string row;
                        string[] data;
                        string searchedRZ;
                        while ((row = srMain.ReadLine()) != null)
                        {
                            data = row.Split(';');
                            if (data.Length <= 1 || data[1].Length <= 2)    // Empty rows and headers are being skipped.
                                continue;

                            searchedRZ = data[1];
                            if (Search(searchedRZ) && !foundRZs.Contains(searchedRZ))
                            {
                                foundRZs.Add(searchedRZ);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Zavřete nejdříve všechny vstupní i výstupní soubory a poté zkuste hledat znovu.\n\nZnění chyby: " + ex.Message, "Přístup odepřen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                UpdateFound(true);
            }            
        }

        private void Working()
        {
            labelNumberOfFoundLabel.Text = "Pracuji...";
            labelNumberOfFoundLabel.Visible = true;            
            labelNumberOfFound.Visible = false;
            buttonSearch.Text = "HLEDÁM...";
            buttonSearch.Enabled = false;
            Refresh();
        }

        private void UpdateFound(bool found)
        {
            buttonSearch.Text = "HLEDEJ";          

            labelNumberOfFound.Text = foundRZs.Count.ToString();
            if (foundRZs.Count > 0)
            {
                buttonShowFound.Enabled = true;
            }
            else
            {
                buttonShowFound.Enabled = false;
            }

            labelNumberOfFoundLabel.Text = "Počet nalezených RZ ve všech souborech:";
            if (found)
            {
                labelNumberOfFoundLabel.Visible = true;
                labelNumberOfFound.Visible = true;
            }
            else
            {
                labelNumberOfFoundLabel.Visible = false;
                labelNumberOfFound.Visible = false;
            }                

            buttonSearch.Enabled = true;

            Refresh();
        }

        private bool Search(string searchedRZ)
        {
            string row;
            string[] data;
            bool foundInThisFile = false;
            for (int i = 1; i < fileNames.Count; i++)
            {               
                using (StreamReader sr = new StreamReader(fileNames[i]))
                {                    
                    while ((row = sr.ReadLine()) != null)
                    {
                        data = row.Split(';');
                        if (data.Length <= 1 || data[1].Length <= 2)    // Empty rows and headers are being skipped.
                            continue;

                        if (searchedRZ == data[1])      // Found the searchedRZ in this file.
                        {
                            foundInThisFile = true;
                            break;
                        }                            
                    }                    
                }
                if (foundInThisFile)
                    foundInThisFile = false;    // We shall continue onto a next file.
                else
                    return false;   // The searchedRZ is missing in this file, therefore we end the seach unsuccessfully.
            }
            return true;
        }

        private void buttonShowFound_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < foundRZs.Count; i++)
            {
                sb.Append(foundRZs[i]);
                sb.Append('\n');
            }
            MessageBox.Show(sb.ToString(), "RZ nalezené ve všech souborech", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void radioButtonGenerateFileNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGenerateFileNo.Checked)
            {
                textBoxPathOut.Enabled = false;
                buttonPathOutFile.Enabled = false;
                checkBoxGenerateFileNew.Enabled = false;
            }
            else
            {
                textBoxPathOut.Enabled = true;
                buttonPathOutFile.Enabled = true;
                checkBoxGenerateFileNew.Enabled = true;
            }

        }

        private void textBoxPathOut_TextChanged(object sender, EventArgs e)
        {
            if (pathWarned)
                textBoxPathOut.BackColor = Color.White;
        }

        private void buttonPathOutFile_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {                
                if (checkBoxGenerateFileNew.Checked)
                    dialog.IsFolderPicker = true;
                else
                {
                    if (!dialogFiltersAdded)
                    {
                        dialogFiltersAdded = true;
                        CommonFileDialogFilter csvFilter = new CommonFileDialogFilter("Soubory csv", ".csv");
                        csvFilter.ShowExtensions = true;                        
                        CommonFileDialogFilter txtFilter = new CommonFileDialogFilter("Textové soubory", ".txt");
                        txtFilter.ShowExtensions = true;
                        dialog.Filters.Add(csvFilter);
                        dialog.Filters.Add(txtFilter);                        
                    }

                    dialog.IsFolderPicker = false;
                }
                    
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    textBoxPathOut.Text = dialog.FileName;
                }
            }
        }

        private void buttonRemoveAllFiles_Click(object sender, EventArgs e)
        {
            UpdateCount(false, "All");
            UpdateFound(false);
        }

        private void checkBoxGenerateFileNew_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGenerateFileNew.Checked)
            {
                buttonPathOutFile.Text = "Vybrat složku...";
            }
            else
            {
                buttonPathOutFile.Text = "Vybrat soubor...";
            }            
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Opravdu chcete aplikaci ukončit?", "Konec?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void oProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string hint = "BlablablaTODO.";
            MessageBox.Show(hint, "O programu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ukončitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Opravdu chcete aplikaci ukončit?", "Konec?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
