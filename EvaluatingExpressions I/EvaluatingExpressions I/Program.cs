using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EvaluatingExpressions_I
{
    abstract class Operator
    {
        public int OperandCount { get; protected set; }
        public string Symbol { get; set; }
        
        public virtual int GetValue(ref Errors indicator, params int[] operands)
        {
            return int.MinValue;
        }
    }

    class UnaryOperator : Operator
    {
        public UnaryOperator(string symbol)
        {
            Symbol = symbol;
            OperandCount = 1;
        }

        public override int GetValue(ref Errors indicator, params int[] operands)
        {
            if (operands.Length != OperandCount)
            {
                throw new Exception("Called the unary operation " + Symbol + " with "+ operands.Length.ToString() + "operands.");
            }
            switch (Symbol)
            {
                case "~":
                    // Math.Abs(int.MinValue) > Math.Abs(int.MaxValue).
                    if (operands[0] == int.MinValue)
                    {
                        indicator = Errors.Overflow;
                        return int.MinValue;
                    }
                    return -operands[0];
                default:
                    throw new Exception("Called an unhlandled unary operation " + Symbol + " to give away its result.");
            }
        }
    }

    class BinaryOperator : Operator
    {
        public BinaryOperator(string symbol)
        {
            Symbol = symbol;
            OperandCount = 2;
        }

        public override int GetValue(ref Errors indicator, params int[] operands)
        {
            if (operands.Length != OperandCount)
            {
                throw new Exception("Called the binary operation " + Symbol + " with " + operands.Length.ToString() + "operands.");
            }

            int result;
            long testResult;

            // Implicit casting on long is necessary, otherwise the result would be int and would be casted on long after overflowing.
            switch (Symbol)
            {                
                case "+":
                    result = operands[0] + operands[1];
                    testResult = (long)operands[0] + (long)operands[1];
                    if (result != testResult)
                    {
                        indicator = Errors.Overflow;
                        return int.MinValue;
                    }
                    return result;

                case "-":
                    result = operands[0] - operands[1];
                    testResult = (long)operands[0] - (long)operands[1];
                    if (result != testResult)
                    {
                        indicator = Errors.Overflow;
                        return int.MinValue;
                    }
                    return result;

                case "*":
                    result = operands[0] * operands[1];
                    testResult = (long)operands[0] * (long)operands[1];
                    if (result != testResult)
                    {
                        indicator = Errors.Overflow;
                        return int.MinValue;
                    }
                    return result;
                case "/":
                    if (operands[1] == 0)
                    {
                        indicator = Errors.Divide;
                        return int.MinValue;
                    }
                    result = operands[0] / operands[1];
                    testResult = (long)operands[0] / (long)operands[1];
                    if (result != testResult)   // int.MinValue / (-1) -> overflow
                    {
                        indicator = Errors.Overflow;
                        return int.MinValue;
                    }
                    else
                    {
                        return result;
                    }
                default:
                    throw new Exception("Called unhlandled binary operation " + Symbol + " to give away its result.");
            }
        }
    }

    public enum Errors
    {
        None = 0,
        Format = 1,
        Overflow = 2,
        Divide = 3
    }

    class EvaluationClass
    {
        private bool IsWhiteSpace(char ch, bool registerCR)
        {
            if (ch == ' ' || ch == '\n' || ch == '\t')
            {
                return true;
            }
            else if (registerCR && ch == '\r')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets another operand or operator from the reader. (Ignores white spaces.)
        /// If there's nothing more non-white to read, returns null.
        /// </summary>
        /// <param name="reader">Keep this reader open if you're calling this method more than once.</param>
        /// <returns>String representing an operand, an operator or null for end of the stream.</returns>
        public string GetOperand(TextReader reader)
        {
            StringBuilder sb = new StringBuilder();
            int numCh;
            char ch;
            bool lastWhite = true;
            bool chWhite;
            string symbols;

            while ((numCh = reader.Read()) != -1)
            {
                ch = (char)numCh;
                chWhite = IsWhiteSpace(ch, true);
                if (chWhite && !lastWhite)
                {
                    symbols = sb.ToString();
                    sb.Clear();
                    return symbols;
                }
                else if (!chWhite)
                {
                    lastWhite = false;
                    sb.Append(ch);
                }
                // else: lastWhite = true;
            }

            return null;
        }

        public int EvaluateOperation(TextReader reader, ref Errors indicator)
        {
            if (indicator != Errors.None)
            {
                return int.MinValue;
            }

            int result;
            string symbols = GetOperand(reader);

            if (int.TryParse(symbols, out result))
            {
                // Only positive integers are allowed in the input stream according to the task specification.
                if (result <= 0 || symbols.StartsWith("0") || symbols.StartsWith("+"))
                {
                    indicator = Errors.Format;
                    return int.MinValue;
                }
                return result;
            }
            else
            {
                Operator op;
                switch (symbols)
                {
                    case "~":
                        op = new UnaryOperator(symbols);
                        break;
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        op = new BinaryOperator(symbols);
                        break;
                    default:
                        indicator = Errors.Format;
                        return int.MinValue;
                }
                int[] operands = new int[op.OperandCount];
                for (int i = 0; i < op.OperandCount; i++)
                {
                    operands[i] = EvaluateOperation(reader, ref indicator);   // Rethink. (need to call EvaluateStream)
                    if (indicator != Errors.None)
                    {
                        return int.MinValue;
                    }
                }
                result = op.GetValue(ref indicator, operands);
                if (indicator != Errors.None)
                {
                    return int.MinValue;
                }
                else
                {
                    return result;  // Could have been together. (Spliting so we're not dependent on GetValue()'s invalid results.)
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string errorFormat = "Format Error";
            const string errorOverflow = "Overflow Error";
            const string errorDiv = "Divide Error";

            EvaluationClass eval = new EvaluationClass();
            Errors errorIndicator = Errors.None;

            TextReader reader = Console.In;
            TextWriter writer = Console.Out;

            int result = eval.EvaluateOperation(reader, ref errorIndicator);

            switch (errorIndicator)
            {
                case Errors.None:
                    string surplusTest = eval.GetOperand(reader);
                    if (surplusTest != null)
                    {
                        writer.Write(errorFormat);
                    }
                    else
                    {
                        writer.Write(result.ToString());
                    }
                    break;
                case Errors.Format:
                    writer.Write(errorFormat);
                    break;
                case Errors.Overflow:
                    writer.Write(errorOverflow);
                    break;
                case Errors.Divide:
                    writer.Write(errorDiv);
                    break;
                default:
                    throw new Exception("Unhlandled format of EvaluationClass.Errors variable.");
            }
        }
    }
}
