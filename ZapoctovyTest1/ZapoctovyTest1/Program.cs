using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZapoctovyTest1
{
    abstract class Cell
    {
        
    }

    class ValueCell : Cell
    {
        public int Value { get; set; }  // Int should be sufficient.

        public ValueCell(int val)
        {
            this.Value = val;
        }
    }

    class TextCell : Cell
    {
        public string Text { get; set; }

        public TextCell(string s)
        {
            this.Text = s;
        }
    }


    class ContigentTable    // Spelling?
    {
        public void LoadRow(out string[] row, StreamReader sr)
        {
            string wholeRow;
            if ((wholeRow = sr.ReadLine()) == null)
            {
                row = null;                
            }
            else
            {
                row = wholeRow.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public void LoadTable(out Dictionary<string, List<Cell>> table, StreamReader sr, string[] header)
        {
            table = new Dictionary<string, List<Cell>>();
            LoadRow(out string[] row, sr);
            if (row == null)
                throw new Exception("Empty table!");

            for (int i = 0; i < header.Length; i++)
                table[header[i]] = new List<Cell>();  // Creating table columns since the table isn't empty.

            while (row != null) // While not end of the input file.
            {
                // Adding one cell to each column.
                for (int i = 0; i < row.Length; i++)
                {
                    if (int.TryParse(row[i], out int value))
                        table[header[i]].Add(new ValueCell(value));    // Saving int as int.
                    else
                        table[header[i]].Add(new TextCell(row[i]));   // Saving everything else as string.
                }                    

                LoadRow(out row, sr);   // Loading another row.
            }
        }

        public List<Cell> GetUniqueCells(List<Cell> column)
        {
            List<Cell> uniqueCells = new List<Cell>();
            bool add = true;
            foreach (Cell cell in column)
            {
                if (cell is TextCell)
                {
                    TextCell tCell = (TextCell)cell;
                    foreach (Cell uCell in uniqueCells)
                    {
                        TextCell tuCell = (TextCell)uCell;
                        if (tuCell.Text == tCell.Text)
                        {
                            add = false;
                            break;
                        }
                        else
                            add = true;
                    }
                    if (add)
                    {
                        uniqueCells.Add(tCell);
                        add = false;
                    }
                }
                else if (cell is ValueCell)
                {
                    ValueCell vCell = (ValueCell)cell;

                    foreach (Cell uCell in uniqueCells)
                    {
                        ValueCell vuCell = (ValueCell)uCell;
                        if (vuCell.Value == vCell.Value)
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add)
                    {
                        uniqueCells.Add(vCell);
                        add = false;
                    }
                    else
                        add = true;
                }
                else
                {
                    throw new Exception("Undefined cell type.");
                }
            }

            return uniqueCells;
        }
    }

    class Recursion
    {
        public void GoThroughTable(Dictionary<string, List<Cell>> table, string colName, List<string> columnNames, string[] rows, Cell[] conditions, string agregName, ref int[] values)  // Ref for clarity.
        {
            bool countIn = true;
            // Vynulování hodnot.
            for (int i = 0; i < values.Length; i++)
                values[i] = 0;

            for (int i = 0; i < table[rows[0]].Count; i++)   // Table is rectangular. (0 or 1 doesn't matter...)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    if (table[rows[j]][i] is TextCell)
                    {
                        TextCell tCell = (TextCell)table[rows[j]][i];
                        if (tCell.Text != ((TextCell)conditions[j]).Text)
                        {
                            countIn = false;
                            break;
                        }
                    }
                    else if (table[rows[j]][i] is ValueCell)
                    {
                        ValueCell vCell = (ValueCell)table[rows[j]][i];
                        if (vCell.Value != ((ValueCell)conditions[j]).Value)
                        {
                            countIn = false;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unhlandled Cell type.");
                        return;
                    }
                }
                if (countIn)
                {                    
                    string name = ((TextCell)table[colName][i]).Text;   // Whose.
                    int val = ((ValueCell)table[agregName][i]).Value;   // What.

                    for (int k = 0; k < columnNames.Count; k++)
                    {
                        if (columnNames[k] == name)
                        {
                            values[k] += val;
                            break;
                        }
                    }
                }
                countIn = true;
            }
        }

        public void Search(int col, ref Cell[] conditions, List<List<Cell>> rowValList, string colName, List<string> columnNames, string[] rows, string agregName, ref int[] values, Dictionary<string, List<Cell>> table, StreamWriter sw, int padding, bool first)
        {
            string output = "";
            for (int i = 0; i < rowValList[col].Count; i++)
            {
                // First output.
                if (!first)
                {
                    for (int k = 0; k < padding; k++)   // padding
                        sw.Write(Program.columnSeparator);
                }
                else
                    first = false;
                
                if (rowValList[col][i] is TextCell)
                    output = ((TextCell)rowValList[col][i]).Text;
                else if (rowValList[col][i] is ValueCell)
                    output = ((ValueCell)rowValList[col][i]).Value.ToString();
                else
                    throw new Exception("Unhlandled cell type.");
                sw.Write(output + Program.columnSeparator);   // Won't work for padding though.
                padding += output.Length + 1;

                // Recursion.
                if (col < rowValList.Count - 1)
                {
                    first = true;
                    Search(col + 1, ref conditions, rowValList, colName, columnNames, rows, agregName, ref values, table, sw, padding, first);            
                }
                padding = padding - output.Length - 1;

                // Conditions.
                for (int k = col; k >= 0; k--)
                {
                    if (rowValList[k].Count <= i)
                        conditions[k] = rowValList[k][rowValList[k].Count - 1];
                    else
                        conditions[k] = rowValList[k][i];                  
                }

                // Action.
                GoThroughTable(table, colName, columnNames, rows, conditions, agregName, ref values);
                
                // Important output.
                for (int k = 0; k < values.Length; k++)
                {
                    if (values[k] != 0)
                    {
                        sw.Write(columnNames[k] + Program.valueSeparator + values[k].ToString() + Program.columnSeparator);
                    }
                }
                sw.WriteLine();
             }
        }
    }


    class Program
    {
        public static readonly char valueSeparator = ':';
        public static readonly char columnSeparator = ' ';

        static void Main(string[] args)
        {
            if (args.Length <= 3)
                throw new Exception("Not enought arguments.");

            if (args.Length == 4)
            {
                Console.WriteLine("Nothing to show.");
                return;
            }

            ContigentTable contTableInst = new ContigentTable();
            string pathIn = args[0];
            string pathOut = args[1];
            string[] headRow;
            Dictionary<string, List<Cell>> table;

            // Loading information.
            using (StreamReader sr = new StreamReader(pathIn))
            {
                // Loading header.
                contTableInst.LoadRow(out headRow, sr);

                // Argument check.
                bool valid = false;
                for (int i = 2; i < args.Length; i++)
                {
                    for (int j = 0; j < headRow.Length; j++)
                    {
                        if (args[i] == headRow[j])
                        {
                            valid = true;   // We've found this argument in the header.
                            break;  // Breaks only the inner cycle.
                        }
                    }
                    if (!valid)
                    {
                        Console.WriteLine("Argument \"{0}\" isn't contained in the table header!", args[i]);
                        return;
                    }
                    else
                        valid = false;  // So we can check another argument.
                }

                // Loading table.
                contTableInst.LoadTable(out table, sr, headRow);
            }

            string agregName = args[2];
            List<Cell> tempAgreg = table[agregName]; // Agregační column.
            List<ValueCell> agreg = new List<ValueCell>();
            foreach (Cell cell in agreg)    // Test of ValueCells.
            {
                if (!(cell is ValueCell))
                {
                    Console.WriteLine("One of the cells in agregační column doesn't contain value parsable to int.");
                    return;
                }
                else
                {
                    agreg.Add((ValueCell)cell);
                    tempAgreg.Remove(cell);
                }
            }

            // Declaration with inicialization.
            string colName = args[3];
            List<Cell> temp = contTableInst.GetUniqueCells(table[colName]);    // Barták Celestýn Adamec
            List<string> printTableColumns = new List<string>();
            foreach (Cell cell in temp)
            {
                TextCell tCell = (TextCell)cell;
                printTableColumns.Add(tCell.Text);
            }
            // temp destroy
            int[] values = new int[printTableColumns.Count]; // For every column 1 value. (For each row use separatly.)
            string[] rows = new string[args.Length - 4];    // Name of the rows (in header).
            for (int i = 0; i < rows.Length; i++)
                rows[i] = args[i + 4];
            Cell[] conditions = new Cell[rows.Length];  // Current row of the table.
            List<List<Cell>> uniqueRowValues = new List<List<Cell>>();
            for (int i = 0; i < rows.Length; i++)
            {
                List<Cell> rowNames = contTableInst.GetUniqueCells(table[rows[i]]);
                uniqueRowValues.Add(rowNames);  // (leden, únor, březen), (brambory, jablka, ...), (tuzemské, vlastní, dovoz), ...
            }



            // Printing the table.
            using (StreamWriter sw = new StreamWriter(pathOut))
            {
                // The algorithm.
                Recursion rec = new Recursion();
                int padding = 0;
                rec.Search(0, ref conditions, uniqueRowValues, colName, printTableColumns, rows, agregName, ref values, table, sw, padding, true);             
            }

            // Hodnocení:
            // A) Správnost (stačí samotné řešení úlohy)
            // B) Jednorázový, ale objektový návrh

            // Zadání:
            // Tabulka obdélnáková
            // Tabulka má záhlaví
            // Na každém řádku je libovolný počet položek
            // Odděleny libovolným množstvím bílých znaků (/t, mezera)
            // Můžou být i před začátkem

            // Data: text / integer

            // Vygeneruje se kontigenční tabulka (pivot table)
        }
    }
}
