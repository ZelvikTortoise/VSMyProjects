namespace ExcelXMLSearchEngine
{
    // C# konzolová aplikace pro .NET 8.0
    // Hledá zadaný řetězec v souborech XML a XLSX ve vybrané složce a podle volby buď generuje TXT výstup, nebo přesouvá soubory do složek ANO / NE
    // Nutné mít přidaný NuGet balíček ClosedXML (instaluje i DocumentFormat.OpenXml)

    using System.Text;
    using System.Xml;
    using ClosedXML.Excel;
    using System.IO;
    using System.Windows.Forms; // nutné přidat referenci System.Windows.Forms

    class Program
    {
        [STAThread]
        static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding(1250);
            Console.WriteLine("Vítejte v programu pro prohledávání XML a XLSX souborů.\nStiskněte libovolnou klávesu a vyberte složku, kde chcete hledat...");
            Console.ReadKey();

            string searchText;
            int mode;
            string folderPath = string.Empty;

            while (true)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Vyberte složku pro prohledávání";
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        Console.WriteLine("Nebyla vybrána žádná složka. Program se ukončí.");
                        return;
                    }
                    folderPath = dialog.SelectedPath;
                }

                Console.Write("Zadejte hledaný text: ");
                searchText = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(searchText)) continue;

                while (true)
                {
                    Console.WriteLine("Zvolte režim: 1 = výstupní TXT soubor, 2 = rozřazení souborů do složek ANO a NE");
                    Console.Write("Zadejte číslo režimu: ");
                    if (int.TryParse(Console.ReadLine(), out mode) && (mode == 1 || mode == 2))
                        break;
                }

                bool searchAgain = false;
                while (true)
                {
                    string[] files = Directory.GetFiles(folderPath)
                        .Where(f => f.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                        .ToArray();

                    if (files.Length == 0)
                    {
                        Console.WriteLine("Ve zvolené složce nejsou žádné XML nebo XLSX soubory.");
                        Console.Write("Chcete zvolit jinou složku? (a/n): ");
                        if (Console.ReadLine()?.Trim().ToLower() == "a") break; else return;
                    }

                    List<string> matchingFiles = new();

                    foreach (string file in files.OrderBy(f => f))
                    {
                        try
                        {
                            if (file.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                            {
                                var doc = new XmlDocument();
                                doc.Load(file);
                                if (doc.InnerText.Contains(searchText)) matchingFiles.Add(Path.GetFileName(file));
                            }
                            else if (file.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                            {
                                using var workbook = new XLWorkbook(file);
                                foreach (var sheet in workbook.Worksheets)
                                {
                                    foreach (var cell in sheet.CellsUsed())
                                    {
                                        if (cell.Value.ToString().Contains(searchText))
                                        {
                                            matchingFiles.Add(Path.GetFileName(file));
                                            goto NextFile;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Chyba při čtení souboru {file}: {ex.Message}");
                        }
                    NextFile:;
                    }

                    if (matchingFiles.Count == 0)
                    {
                        Console.WriteLine("Hledaný text nebyl nalezen v žádném souboru.");
                        Console.Write("Chcete zadat jiný text? (a/n): ");
                        if (Console.ReadLine()?.Trim().ToLower() == "a") { searchAgain = true; break; } else return;
                    }
                    else
                    {
                        if (mode == 1)
                        {
                            string safeName = string.Join("_", searchText.Split(Path.GetInvalidFileNameChars()));
                            string baseName = string.IsNullOrWhiteSpace(safeName) ? "output" : safeName;
                            string outputFile = Path.Combine(folderPath, baseName + ".txt");
                            int i = 1;
                            while (File.Exists(outputFile))
                                outputFile = Path.Combine(folderPath, $"{baseName}{i++}.txt");

                            File.WriteAllLines(outputFile, matchingFiles, Encoding.GetEncoding(1250));
                            Console.WriteLine($"Hotovo. Výsledky uloženy do souboru: {Path.GetFileName(outputFile)}");
                        }
                        else
                        {
                            string anoDir = Path.Combine(folderPath, "ANO");
                            string neDir = Path.Combine(folderPath, "NE");
                            Directory.CreateDirectory(anoDir);
                            Directory.CreateDirectory(neDir);

                            foreach (string file in files)
                            {
                                string targetDir = matchingFiles.Contains(Path.GetFileName(file)) ? anoDir : neDir;
                                string destPath = Path.Combine(targetDir, Path.GetFileName(file));
                                File.Copy(file, destPath, overwrite: true);
                            }
                            Console.WriteLine("Hotovo. Soubory byly rozřazeny do složek ANO a NE.");
                        }
                        return;
                    }
                }
                if (!searchAgain) break;
            }
        }
    }
}
