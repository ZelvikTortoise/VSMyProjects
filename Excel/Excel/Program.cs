                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Excel
{  
    class Cell
    {
        /// <summary>
        /// Gets a value of a cell.
        /// IMPORTANT NOTE: Only FormulaCell can return int.MaxValue!
        /// </summary>
        /// <returns>The value of a cell (specific for each type).</returns>
        public virtual int GetValue()
        {
            throw new Exception("Unhandled Cell type called to give value.");
        }

        /// <summary>
        /// Prints a cell with a printer.
        /// </summary>
        /// <param name="writer">The printer.</param>
        public virtual void PrintCell(TextWriter writer)
        {
            throw new Exception("Unhandled Cell type called to be printed.");
        }
    }

    class EmptyCell : Cell
    {
        public const string emptyCellSymbol = "[]";
        public const int emptyCellValue = 0;

        public override int GetValue()
        {
            // Never int.MaxValue.
            return emptyCellValue;
        }

        public override void PrintCell(TextWriter writer)
        {
            writer.Write(emptyCellSymbol);
        }
    }

    class ErrorCell : Cell
    {
        public const string errorInvalidValue = "#INVVAL";
        public const int errorCellValue = int.MinValue;     // Not cyclic for sure. (It's not FormulaCell.)

        public override int GetValue()
        {
            // Never int.MaxValue.
            return errorCellValue;
        }

        public override void PrintCell(TextWriter writer)
        {
            writer.Write(errorInvalidValue);
        }
    }

    class FormulaCell : Cell
    {
        public const string formulaStart = "=";

        public const string errorDivisionByZero = "#DIV0";
        public const string errorReferenceCycle = "#CYCLE";
        public const string errorCalculation = "#ERROR";
        public const string errorMissingFormulaOperator = "#MISSOP";
        public const string errorFormulaInvalidOperands = "#FORMULA";
        // public static string[] errors = new string[] { errorDivisionByZero, errorReferenceCycle, errorCalculation, errorMissingFormulaOperator, errorFormulaInvalidOperands };

        /// <summary>
        /// Formula text notation.
        /// </summary>        
        public string Text { get; set; }

        /// <summary>
        /// Value to calculate with.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// // Cycle indicator.
        /// (Note: Type Byte should be sufficient.)
        /// </summary>
        public byte CallCount { get; set; }   

        /// <summary>
        /// 0...Not evaluated yet;
        /// else Evaluated:
        /// 1...Value;
        /// 2...errorDivisionByZero;
        /// 3...errorReferenceCycle;
        /// 4...errorCalculation;
        /// 5...errorMissingFormulaOperator;
        /// 6...errorFormulaInvalidOperands
        /// </summary>
        public byte Evaluated { get; set; }

        public FormulaCell(string text)
        {
            Text = text;
            Value = int.MinValue;  // We don't know yet.
            CallCount = 0;  
            Evaluated = 0;  // To be evaluated.
        }

        /// <summary>
        /// Gets FormulaCell.Value. Uses int.MaxValue and int.MinValue for giving away more information.
        /// </summary>
        /// <returns>If the cell hasn't been evaluated yet, returns int.MaxValue. If it's been evaluated already and the Value is valid, it returns the Value. If the Value is invalid due to some temporary error, it returns int.MinValue.</returns>
        public override int GetValue()
        {
            switch (Evaluated)
            {
                case 0:
                    return EvaluationClass.cycleFoundValue;    // Reserved.  // It means either the formula hasn't been evaluated yet or that we found a cycle.
                case 1:
                    return Value;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    return EvaluationClass.errorFoundValue;
                default:
                    throw new Exception("Unhandled type of FormulaCell called to give value.");
            }
        }

        public override void PrintCell(TextWriter writer)
        {
            switch (Evaluated)
            {
                case 0:
                    throw new Exception("Trying to print an unevaluated instance of FormulaCell class.");
                case 1:
                    writer.Write(Value.ToString());
                    break;
                case 2:
                    writer.Write(errorDivisionByZero);
                    break;
                case 3:
                    writer.Write(errorReferenceCycle);
                    break;
                case 4:
                    writer.Write(errorCalculation);
                    break;
                case 5:
                    writer.Write(errorMissingFormulaOperator);
                    break;
                case 6:
                    writer.Write(errorFormulaInvalidOperands);
                    break;
                default:
                    throw new Exception("Unhandled type of FormulaCell.Evaluated called to be printed.");                    
            }
        }
    }

    class ValueCell : Cell
    {
        public int Value { get; set; }  // Sufficient according to task specification.

        public ValueCell(int value)
        {
            Value = value;
        }

        public override int GetValue()
        {
            // Never int.MaxValue.
            return Value;
        }

        public override void PrintCell(TextWriter writer)
        {
            writer.Write(Value.ToString());
        }
    }

    class Sheet
    {
        public List<List<Cell>> Cells { get; set; }

        /// <summary>
        /// The class constructor of Sheet class.
        /// It creates a List of empty rows (which's type: List<Cell>).
        /// The List is created with 1st empty row (index 0).
        /// </summary>
        public Sheet()
        {
            Cells = new List<List<Cell>>();
            Cells.Add(new List<Cell>());
        }
    }

    class SheetReader
    {
        // Created only once.
        private EmptyCell emptyCell = new EmptyCell();
        private ErrorCell errorCell = new ErrorCell();

        /// <summary>
        /// According to a text, it creates a new instance of a type of cell or gets a reference to it (in case of EmptyCell and ErrorCell).
        /// </summary>
        /// <param name="text">The text to create the cell from.</param>
        /// <returns>Reference to an instance of a specific type of Cell.</returns>
        public Cell GetNewCell(string text)
        {
            if (text == EmptyCell.emptyCellSymbol)
            {
                return emptyCell;   // Already created.
            }
            else if (text.StartsWith(FormulaCell.formulaStart))
            {
                return new FormulaCell(text);
            }
            else if (int.TryParse(text, out int result))
            {
                return new ValueCell(result);
            }
            else
            {
                return errorCell;   // Already created.
            }
        }

        /// <summary>
        /// Gets a sheet from a textreader. Every line will be a row in the sheet. Cells on one row are separated with one or more spaces (' ').  
        /// Note: If the source is empty, it will return an instance of a Sheet class with one empty row.
        /// </summary>
        /// <param name="reader">A textreader to read with.</param>
        /// <returns>A new instance of a Sheet class.</returns>
        public Sheet GetSheet(TextReader reader)
        {
            Sheet sheet = new Sheet();  // List construction + first row construction included in Sheet class.
            StringBuilder sb = new StringBuilder(); // Constructing a cell value.
            int intSymbol;
            char symbol;

            while ((intSymbol = reader.Read()) != -1)
            {
                symbol = (char)intSymbol;

                switch (symbol)
                {
                    case ' ':
                    case '\n':
                        if (sb.Length > 0)
                        {
                            Cell cell = GetNewCell(sb.ToString());
                            sb.Clear();                        
                            sheet.Cells[sheet.Cells.Count - 1].Add(cell);
                        }
                        if (symbol == '\n')
                        {
                            // Note: Integer is sufficient (from task specification).
                            sheet.Cells.Add(new List<Cell>());  // Adding another row.
                        }
                        break;
                    default:
                        sb.Append(symbol);
                        break;
                }
            }

            if (sb.Length > 0)
            {
                Cell cell = GetNewCell(sb.ToString());
                sheet.Cells[sheet.Cells.Count - 1].Add(cell);
                // sb.Clear(); // Not needed.
            }

            return sheet;
        }
    }

    class BufferWriter : TextWriter
    {
        public override Encoding Encoding { get; }
        private string[] buffer { get; set; }
        private int firstEmptySpace { get; set; }
        public const int maxBufferSize = 512;  // 8 bytes per reference -> 4 KiB
        private TextWriter writer { get; set; }

        public BufferWriter(TextWriter tw)
        {
            buffer = new string[maxBufferSize];
            firstEmptySpace = 0;
            writer = tw;
        }

        private void EmptyBuffer()
        {
            StringBuilder sb = new StringBuilder();
            int stopCondition = firstEmptySpace < maxBufferSize ? firstEmptySpace : maxBufferSize;

            for (int i = 0; i < stopCondition; i++)
            {
                sb.Append(buffer[i]);
            }
            writer.Write(sb.ToString());
        }

        public override void Write(string text)
        {
            if (firstEmptySpace >= maxBufferSize)
            {
                EmptyBuffer();
                firstEmptySpace = 0;
            }
            else
            {
                buffer[firstEmptySpace] = text;
                firstEmptySpace++;
            }
        }

        public override void Write(char text)
        {
            if (firstEmptySpace >= maxBufferSize)
            {
                EmptyBuffer();
                firstEmptySpace = 0;
            }
            else
            {
                buffer[firstEmptySpace] = text.ToString();
                firstEmptySpace++;
            }
        }

        public void Finish()
        {
            EmptyBuffer();
        }
    }

    class SheetWriter
    {
        /// <summary>
        /// Prints all cells of a sheet with a printer. Every row is on a different line in correct order, cells in one row are separated with spaces (' ').
        /// </summary>
        /// <param name="sheet">The sheet to be printed.</param>
        /// <param name="writer">The printer.</param>
        public void PrintSheet(Sheet sheet, TextWriter writer)
        {
            BufferWriter buffwriter = new BufferWriter(writer);


            // We know that int is enough for storing. To avoid infinite cycle, we use long i, j in the cycles' heads.
            for (long i = 0; i < sheet.Cells.Count; i++)
            {
                for (long j = 0; j < sheet.Cells[(int)i].Count; j++)   // The conversion will be ok thanks to the task specification.
                {
                    sheet.Cells[(int)i][(int)j].PrintCell(buffwriter);  // Overridden method.
                    buffwriter.Write(' ');
                }
                buffwriter.Write('\n');
            }
            buffwriter.Finish();
        }
        // Note: There will be one ' ' after every element (even at the end of every line).
        // Note: There will be an empty line at the end of the printed sheet.
    }

    class EvaluationClass
    {
        private static Dictionary<string, Sheet> excelSheets = new Dictionary<string, Sheet>();        
        private const string sheetSuffix = ".sheet";
        private static List<char> operators = new List<char> { '+', '-', '*', '/' };        
        private const int undefinedCellValue = 0;
        private const char differentSheetSymbol = '!';

        public const int cycleFoundValue = int.MaxValue;
        public const int errorFoundValue = int.MinValue;

        private const int lettersInAlphabet = 26;
        private const char firstLetter = 'A';
        private const char lastLetter = 'Z';         

        public EvaluationClass(string mainSheetKey, Sheet mainSheet)
        {
            // Preparing our sheet dictionary.
            if (excelSheets.Count != 0)
            {
                excelSheets.Clear();
            }
            int startRemovingIndex = mainSheetKey.Length - sheetSuffix.Length;
            mainSheetKey = mainSheetKey.Remove(startRemovingIndex);
            excelSheets.Add(mainSheetKey, mainSheet);
        }

        /// <summary>
        /// Returns integer which is the correct index of a cell's column.
        /// If the notation is invalid and check == true, the method throws a new ArgumentException.
        /// Note: Works with capital letter from English alphabet only.
        /// </summary>
        /// <param name="letters">Column ("excel") notation. (Eg. AAD, EF, X, ...)</param>
        /// <param name="check">Do you want the method to check an input validity? Put true for Debug, false for Release.</param>
        /// <returns>Index of the cell's column or -1 for invalid notation.</returns>
        public int DecodeColumnIndex(string letters, bool check)
        {
            int exponent;
            int multiplicationValue = 1;
            int index = -1;  // Int must be sufficient according to the input specification.
            // Starting from -1 because we want to start indexing from 0 (letters == "A" -> index == 0).

            // Checking if the notation is valid.
            if (check)
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] < firstLetter || letters[i] > lastLetter)
                    {
                        throw new ArgumentException("Invalid notation. Parameter has to be a sequence of capital English letters.");
                    }
                }
            }

            for (exponent = 0; exponent < letters.Length; exponent++)
            {    
                index += multiplicationValue * ((int)letters[letters.Length - exponent - 1] - (int)firstLetter + 1);
                multiplicationValue *= lettersInAlphabet;
            }

            return index;
            // Debug note: Tested. Trustful.
        }

        /// <summary>
        /// Getting the sheetKey:
        /// If there is a sheet specified (sheetName + differentSheetSymbol), the method will try to MODIFY the excelSheets Dictionary by adding the sheet if it's not present yet.
        /// It will first attempt to READ the sheet FILE. If it's not possible, it will return null and both indeces will be int.MaxValue! (This will be interpreted as an errorCalculation.)
        /// If the file is read, then the sheet is created as an instance of Sheet class and if it's not present in the excelSheet Dictionary,
        /// it will be added with a string key that was in a cellCode before the differentSheetSymbol (without default sheetSuffix).
        /// Note: If the string before differentSheetSymbol ends with sheetSuffix, another sheetSuffix will be added as a path to the sheet file.
        /// If there's no sheet specification, sheetKey will be mainSheetKey (const for current EvaluationClass instance).
        /// 
        /// Getting the cellLocation:
        /// The method will then try to separate letters and numbers from the cellLocation (cellCode without the sheet information).
        /// If there are no capital letters at the beginning or if numbers start with '0', '+' or '-' or if the rest of the string
        /// after letters isn't parsable to int, the method will return null and both indeces will be int.MinValue. (This will be interpreted as an errorFormulaInvalidOperands.)
        /// If cellLocation is in a valid format, rowIndex will be ONE LESS than the parsed number (List indexing vs. excel logic indexing)
        /// and columnIndex will be figured out by calling DecodeColumnIndex method.
        /// </summary>
        /// <param name="cellCode">Reference to a cell. (Valid formats: A55, ZA9 ABC123, sheetName!ABC123)</param>
        /// <returns>Row index of a cell; column index of a cell; cell's sheet (for excelSheets Dictionary)</returns>
        public Tuple<Sheet, int, int> DecodeCellLocation(Sheet currentSheet, string cellCode)
        {
            Sheet sheet = currentSheet;
            string cellLocation = cellCode;

            // Expansion (getting a sheet key).
            if (cellCode.Contains(differentSheetSymbol.ToString()))
            {
                string[] tokens = cellCode.Split(differentSheetSymbol);
                // tokens.Length >= 2   (we want == 2 only)
                string sheetKey = tokens[0];
                cellLocation = tokens[1];                

                if (tokens.Length != 2)
                {
                    if (!File.Exists(tokens[0] + sheetSuffix))
                    {
                        return new Tuple<Sheet, int, int>(null, int.MaxValue, int.MaxValue);    // errorCalculation
                    }
                    else
                    {
                        return new Tuple<Sheet, int, int>(null, int.MinValue, int.MinValue);    // errorFormulaInvalidOperands
                    }
                }

                if (!excelSheets.ContainsKey(sheetKey))
                {
                    try
                    {
                        using (StreamReader streamReader = new StreamReader(sheetKey + sheetSuffix))
                        {
                            SheetReader sheetReader = new SheetReader();
                            sheet = sheetReader.GetSheet(streamReader);
                            excelSheets.Add(sheetKey, sheet);
                        }
                    }
                    catch (IOException)
                    {
                        return new Tuple<Sheet, int, int>(null, int.MaxValue, int.MaxValue);    // errorCalculation
                    }
                }
                else
                {
                    sheet = excelSheets[sheetKey];
                }
            }

            // Decoding cell's location in a sheet.
            StringBuilder sb = new StringBuilder();
            int rowIndex = 0;
            int columnIndex = 0;
            string rowText = null;
            string columnText = null;

            // The order of conditions is important (for range checking).
            int i = 0;
            while (i < cellLocation.Length && cellLocation[i] >= firstLetter && cellLocation[i] <= lastLetter)
            {
                sb.Append(cellLocation[i]);
                i++;
            }
            columnText = sb.ToString();
            sb.Clear();

            if (columnText.Length == 0)
            {                
                return new Tuple<Sheet, int, int>(null, int.MinValue, int.MinValue);    // errorFormulaInvalidOperands
            }

            rowText = cellLocation.Remove(0, i);

            if (rowText.StartsWith("0") || rowText.StartsWith("-") || rowText.StartsWith("+")) // Invalid: 023, 0, -5, +71.
            {
                return new Tuple<Sheet, int, int>(null, int.MinValue, int.MinValue);    // errorFormulaInvalidOperands
            }
            else if (!int.TryParse(rowText, out rowIndex))  // Invalid: q1, .x, etc.
            {
                return new Tuple<Sheet, int, int>(null, int.MinValue, int.MinValue);   // errorFormulaInvalidOperands
            }
            else   // Now we know that rowIndex is an pozitive integer.
            {
                rowIndex--; // Logic indexing starts at 1. However, List indexing starts at 0.
                columnIndex = DecodeColumnIndex(columnText, false);      // Change to true for DEBUGGING.
            }

            return new Tuple<Sheet, int, int>(sheet, rowIndex, columnIndex);
        }



        /// <summary>
        /// Returns a coresponding int value to the cell.Value.
        /// Do not call this method on a non-evaluated cell!
        /// For all errors and other non-parsable strings returns int.MinValue.
        /// For cyclic reference returns int.MaxValue and MODIFIES cell.Value. (Removes the newCycleFoundSymbol indicator from the start of cell.Value.)
        /// </summary>
        /// <param name="sheet">Cell's sheet. (Sheet in which the cell is.)</param>
        /// <param name="row">List index of a cell's row in its sheet. (Logic index - 1.)</param>
        /// <param name="column">List index of a cell's column in its sheet. (A = 0, AA = 26, AZ = 51, ...)</param>
        /// <returns>If the cell is undefined in the current sheet, returns undefinedCellValue. If the cell.Value == emptyCellSymbol, returns emptyCellValue. Returns parsed integer if parsable. If not, then returns int.MinValue. If the cell is a part of a new cycle, returns int.MaxValue.</returns>
        private int GetCellValue(Sheet sheet, int row, int column)
        {
            // Checking if the cell is defined in the sheet. (If not, returns undefinedCellValue.)
            // Note: The order is important.
            if (row >= sheet.Cells.Count || column >= sheet.Cells[row].Count)
            {
                return undefinedCellValue;
            }

            // The cell is defined in the sheet.
            Cell cell = sheet.Cells[row][column];
            int res = cell.GetValue();
            if (res == cycleFoundValue)    // Reserved value for FormulaCell.
            {
                FormulaCell fc = (FormulaCell)cell;
                if (fc.CallCount != 2)  // Unevaluated FormulaCell.
                {
                    return GetFormulaResult(sheet, row, column);
                }
            }
            // Normal result or a cycleFoundValue (for fc.CallCount ==2) or an errorFoundValue.
            return res;
        }

        /// <summary>
        /// Reads from sheet.Cells[row][column].Value. MODIFIES all cell.Value(s) of cells met on its path in our excelSheet Dictionary that haven't been evaluated yet.
        /// The modification happens through calling EvaluateCell(...) method.
        /// Call this method on formulas only! (Starting with a formulaStart symbol.)
        /// Finds an operator (called operation) and splits the rest to two operands. If there's no operator, returning errorMissingOperator.
        /// The method then takes each of the operands, calls EvaluateCell(...) on it, which can MODIFY its Value, and then calls GetCellValue(...) so it can calculate the output.
        /// If there's a division by 0, returns errorDivisionByZero, if one of the operands == int.MinValue, then returns errorReferenceCycle or errorCalculation.
        /// To distinguish between those two errors, it uses an indicator in a form of newFoundCycleSymbol which is at the start of the errorReferenceCycle cell.Value.
        /// If the cell.CallCount at the beginning of this method is == 2, errorReferenceCycle is returned and the method ends immediately.
        /// Otherwise returns the result of the operation.
        /// IMPORTANT NOTE: I'm done with overwriting this text. Can be out of date.
        /// </summary>
        /// <param name="sheet">Cell's sheet. (Sheet in which the cell is.)</param>
        /// <param name="row">List index of a cell's row in its sheet. (Logic index - 1.)</param>
        /// <param name="column">List index of a cell's column in its sheet. (A = 0, AA = 26, AZ = 51, ...)</param>
        /// <returns>Formula Value if valid, otherwise int.MinValue for invalid value and int.MaxValue for fresh cycle.</returns>
        private int GetFormulaResult(Sheet sheet, int row, int column)
        {
            if (!(sheet.Cells[row][column] is FormulaCell))
            {
                throw new Exception("Calling GetFormulaResult on non-formula cell.");
            }

            FormulaCell cell = (FormulaCell)sheet.Cells[row][column];   // We know the cell is FormulaCell.          
            cell.CallCount++;

            // This cell is the first cell detected in a newly found cycle.
            if (cell.CallCount == 2)
            {
                // cell.CallCount is left on 2 so we can distinguish cells refering to a cycle and the cycle itself in GetCellValue method
                // GetFormulaResult then clears cell.CallCount after getting that information from GetCellValue method.
                // (Note: We know one cell is in one cycle at most according to the task specification).                 
                // IMPORTANT NOTE: More cycles built together won't be detected! (eg. sheet: (=A1+B1 =B1-C1 = A1*B1) will be evaluated as (#CYCLE #CYCLE #ERROR)
                //                 because A1 and B1 cycle will be detected first, cycles A1–C1 and B1–C1 will be lost and therefore C1 will end up as #ERROR.
                //                 Similarly for other types of connected cycles. Think recursively.
                cell.Evaluated = 3; // errorReferenceCycle
                return cycleFoundValue;
            }
            else if (cell.CallCount > 2)
            {
                throw new Exception("Unhandled FormulaCell.CallCount value in GetFormulaResult(...) method.");
            }

            Tuple<Sheet, int, int> operandInfo;
            string[] operandsText;
            char operation = '?';   // Has to be assigned but the value is unknown for now.
            bool operatorFound = false;


            // Searching for the operator. // New version.
            for (int i = 0; i < cell.Text.Length; i++)
            {
                for (int j = 0; j < operators.Count; j++)
                {
                    if (cell.Text[i] == operators[j])
                    {
                        operatorFound = true;
                        operation = operators[j];
                        break;
                    }
                }
                if (operatorFound) break;
            }
            
            /*// Searching for the operator. // Old version.
            foreach (char op in operators)  // Operators is a list of all valid operators in a formula.
            {
                operatorFound = cell.Text.Contains(op.ToString());
                if (operatorFound)
                {
                    operation = op;
                    break;
                }
            }*/

            // No operator found.
            if (!operatorFound)
            {
                cell.Evaluated = 5;  // errorMissingFormulaOperator
                cell.CallCount = 0;  // Not a cycle.
                return errorFoundValue;
            }

            // found == true (we have a valid operator called operation):
            operandsText = cell.Text.Split(operation);

            // If there were more instances of 1 type of valid operators, the formula's operands are invalid.
            if (operandsText.Length != 2)
            {
                cell.Evaluated = 6; // errorFormulaInvalidOperands
                cell.CallCount = 0; // Not a cycle.
                return errorFoundValue;
            }


            // Integer values of operands:
            int[] operands = new int[2];
            // We know that operandsText == string[2].

            for (byte k = 0; k <= 1; k++)
            {
                if (k == 0)
                {                    
                    operandInfo = DecodeCellLocation(sheet, operandsText[k].Remove(0, 1));   // Removing the FormulaCell.formulaStart. ('=')
                }
                else   // k == 1
                {
                    operandInfo = DecodeCellLocation(sheet, operandsText[k]);
                }

                if (operandInfo.Item1 == null && operandInfo.Item2 == int.MaxValue && operandInfo.Item3 == int.MaxValue)
                {
                    cell.Evaluated = 4; // errorCalculation
                    cell.CallCount = 0; // Not a cycle.
                    return errorFoundValue;
                }
                else if (operandInfo.Item1 == null && operandInfo.Item2 == int.MinValue && operandInfo.Item3 == int.MinValue)
                {
                    cell.Evaluated = 6; // errorFormulaInvalidOperands
                    cell.CallCount = 0; // Not a cycle.
                    return errorFoundValue;
                }
                else
                {
                    Sheet usedSheet = operandInfo.Item1;
                    operands[k] = GetCellValue(usedSheet, operandInfo.Item2, operandInfo.Item3);
                    if (operands[k] == cycleFoundValue)
                    {
                        // Continuing the cycle.
                        cell.CallCount = 2; // Continue the cycle.
                        cell.Evaluated = 3; // errorReferenceCycle

                        // Setting the operand's CallCount back to 0.
                        FormulaCell fc = (FormulaCell)usedSheet.Cells[operandInfo.Item2][operandInfo.Item3];
                        fc.CallCount = 0;   // The cycle has already been registred.
                        // Note: For cycles with length 2 this order is important so the cell ends up with CallCount == 0.

                        return cycleFoundValue;
                    }
                    else if (operands[k] == errorFoundValue)
                    {
                        cell.Evaluated = 4; // errorCalculation
                        cell.CallCount = 0; // Not a cycle.
                        return errorFoundValue;
                    }                    
                }
            }

            /* Format:
            foreach (char op in operation)
            {
                ...
            }
            */
            // Valid operations.
            cell.Evaluated = 1; // Value is valid.
            switch (operation)
            {
                case '+':
                    cell.Value = operands[0] + operands[1];
                    break;
                case '-':
                    cell.Value = operands[0] - operands[1];
                    break;
                case '*':
                    cell.Value = operands[0] * operands[1];
                    break;
                case '/':
                    // Division by 0 is an invalid operation.
                    if (operands[1] == 0)
                    {
                        cell.Evaluated = 2; // errorDivisionByZero
                        return errorFoundValue;
                    }
                    cell.Value = operands[0] / operands[1];
                    break;
                default:
                    throw new Exception("Unhandled operation in formula evaluating.");
            }
            
            if (cell.Value == cycleFoundValue || cell.Value == errorFoundValue)
            {
                throw new Exception("Unhandled exception in formula evaluating. (Reserved values have been evaluated as a valid result.)");
            }
            // For all valid results.
            return cell.Value;           
        }

        /// <summary>
        /// Modifies all (or some) formula cells in a sheet.
        /// After calling this method on a sheet, all formula cells in the sheet will have property Evaluated greater than 0.
        /// Changes all formulas to their results (if calculatable), detects invalid cell values, cycles, invalid formulas, division by 0.
        /// If a formula cannot be calculated or there are any other invalid values, an error will be displayed in the cell. (Detectable by Evaluated being greater than 1.)
        /// Format of an error: "#..."
        /// If you intend to evaluate the whole sheet which is not freshly created, set the clearFirst parameter to true.
        /// Note: Only formula cells with their Evaluated property set to false will be evaluated in your sheet.
        /// (Note: For freshly created sheets it's faster to set the clearFirst parameter to false, since all formula cells' Evaluated values are implicitly equal to 0.)
        /// </summary>
        /// <param name="sheet">Sheet you want to be evaluated.</param>
        /// <param name="clearFirst">True: 0 will be assigned to the property Evaluated for all the formula cells in the sheet. False: the property won't be changed.</param>
        public void EvaluateSheet(Sheet sheet, bool clearFirst)
        {
            // Note: Type long is used only to prevent an infinite cycle. Variables i, j will be always convertable to int without losing information in the cycle's body.
            if (clearFirst)
            {
                for (long i = 0; i < sheet.Cells.Count; i++)
                {
                    for (long j = 0; j < sheet.Cells[(int)i].Count; j++)
                    {
                        if (sheet.Cells[(int)i][(int)j] is FormulaCell)
                        {
                            FormulaCell cell = (FormulaCell)sheet.Cells[(int)i][(int)j];
                            cell.Evaluated = 0;
                            cell.CallCount = 0;
                        }                        
                    }
                }
            }

            // Note: Type long is used only to prevent an infinite cycle. Variables i, j will be always convertable to int without losing information in the cycle's body.
            for (long i = 0; i < sheet.Cells.Count; i++)
            {
                for (long j = 0; j < sheet.Cells[(int)i].Count; j++)
                {
                    if (sheet.Cells[(int)i][(int)j] is FormulaCell)
                    {
                        FormulaCell fc = (FormulaCell)sheet.Cells[(int)i][(int)j];
                        if (fc.Evaluated == 0)  // Evaluate only unevaluated formulas.
                        {
                            GetFormulaResult(sheet, (int)i, (int)j);    // Int method used as a void. (Needs to be int due to recursion.)
                        }                        
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string argError = "Argument Error";
            const string fileError = "File Error";

            if (args.Length != 2)
            {
                Console.WriteLine(argError);
                return;
            }
            // Task specification.
            string pathIn = args[0];
            string pathOut = args[1];
            Sheet mainSheet;

            try
            {
                using (StreamReader streamReader = new StreamReader(pathIn))
                {
                    SheetReader sheetReader = new SheetReader();
                    mainSheet = sheetReader.GetSheet(streamReader);
                }
            }
            catch (IOException)
            {
                Console.WriteLine(fileError);
                return;
            }

            EvaluationClass evaluationClass = new EvaluationClass(pathIn, mainSheet);
            evaluationClass.EvaluateSheet(mainSheet, false);    // Clearing is not needed since the sheet has just been created.

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(pathOut))
                {
                    SheetWriter sheetWriter = new SheetWriter();
                    sheetWriter.PrintSheet(mainSheet, streamWriter);
                }
            }
            catch (IOException)
            {
                Console.WriteLine(fileError);
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         