using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EvalExpressions_I
{    
    class EvaluationClass
    {
        public enum Errors
        {
            None = 0,
            FormatError = 1,
            OverflowError = 2,
            DivideError = 3
        }

        public bool CheckEnd(TextReader reader)
        {
            // Works?
            int numCh;
            char ch;
            while ((numCh = reader.Read()) != -1)
            {
                ch = (char)numCh;
                if (ch != ' ' && ch != '\n' && ch != '\r' && ch != '\t')
                {
                    return false;
                }
            }
            return true;
        }

        // Operation operation;
        Addition addition = new Addition();
        Subtraction subtraction = new Subtraction();
        Multiplication multiplication = new Multiplication();
        Division division = new Division();
        public int EvaluateExpression(TextReader reader, ref Errors indicator)
        {
            if (indicator != Errors.None)
            {                
                return int.MinValue;
            }
            StringBuilder sb = new StringBuilder();
            int numCh;
            char ch;
            bool lastWhite = true;

            int newNum;
            string symbols;
           
            int a;
            int b;
            int result;

            while ((numCh = reader.Read()) != -1)
            {                
                ch = (char)numCh;
                if ((ch == ' ' || ch == '\n' || ch == '\t') && !lastWhite)
                {
                    lastWhite = true;
                    symbols = sb.ToString();
                    sb.Clear();
                    if (int.TryParse(symbols, out newNum))
                    {
                        return newNum;
                    }
                    else
                    {
                        switch (symbols)
                        {
                            case "+":
                                a = EvaluateExpression(reader, ref indicator);
                                b = EvaluateExpression(reader, ref indicator);
                                if (indicator != Errors.None)
                                {
                                    return int.MinValue;
                                }
                                try
                                {
                                    result = addition.GetValue(a, b);
                                }
                                catch (ArithmeticException)
                                {
                                    result = int.MinValue;
                                    indicator = Errors.OverflowError;
                                }
                                break;
                            case "~":
                                a = EvaluateExpression(reader, ref indicator);
                                if (indicator != Errors.None)
                                {
                                    return int.MinValue;
                                }
                                try
                                {
                                    result = subtraction.GetValue(a);
                                }
                                catch (ArithmeticException)
                                {
                                    result = int.MinValue;
                                    indicator = Errors.OverflowError;
                                }
                                break;
                            case "-":
                                a = EvaluateExpression(reader, ref indicator);
                                b = EvaluateExpression(reader, ref indicator);
                                if (indicator != Errors.None)
                                {
                                    return int.MinValue;
                                }
                                try
                                {
                                    result = subtraction.GetValue(a, b);
                                }
                                catch (ArithmeticException)
                                {
                                    result = int.MinValue;
                                    indicator = Errors.OverflowError;
                                }
                                break;
                            case "*":
                                a = EvaluateExpression(reader, ref indicator);
                                b = EvaluateExpression(reader, ref indicator);
                                if (indicator != Errors.None)
                                {
                                    return int.MinValue;
                                }
                                try
                                {
                                    result = multiplication.GetValue(a, b);
                                }
                                catch (ArithmeticException)
                                {
                                    result = int.MinValue;
                                    indicator = Errors.OverflowError;
                                }
                                break;
                            case "/":
                                a = EvaluateExpression(reader, ref indicator);
                                b = EvaluateExpression(reader, ref indicator);
                                if (indicator != Errors.None)
                                {
                                    return int.MinValue;
                                }
                                try
                                {
                                    result = division.GetValue(a, b);
                                }
                                catch (DivideByZeroException)
                                {
                                    result = int.MinValue;
                                    indicator = Errors.DivideError;
                                }
                                catch (ArithmeticException)
                                {
                                    result = int.MinValue;
                                    indicator = Errors.OverflowError;
                                }
                                break;
                            default:
                                result = int.MinValue;
                                indicator = Errors.FormatError;
                                break;
                        }
                        return result;
                    }
                }
                else if (ch != ' ' && ch != '\n' && ch != '\r' && ch != '\t')
                {
                    lastWhite = false;
                    sb.Append(ch);
                }
            }

            // No more operands for us.
            indicator = Errors.FormatError;
            return int.MinValue;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string errorFormat = "Format Error";
            const string errorOverflow = "Overflow Error";
            const string errorDivByZero = "Divide Error";


            // Checking for arithmetic overflow / underflow is on.
            TextReader reader = Console.In;
            TextWriter writer = Console.Out;

            EvaluationClass eval = new EvaluationClass();
            EvaluationClass.Errors indicator = EvaluationClass.Errors.None;
            int result = eval.EvaluateExpression(reader, ref indicator);

            if (!eval.CheckEnd(reader))
            {
                writer.Write(errorFormat);
            }
            else
            {
                switch (indicator)
                {
                    case EvaluationClass.Errors.None:
                        writer.Write(result.ToString());
                        break;
                    case EvaluationClass.Errors.FormatError:
                        writer.Write(errorFormat);
                        break;
                    case EvaluationClass.Errors.OverflowError:
                        writer.Write(errorOverflow);
                        break;
                    case EvaluationClass.Errors.DivideError:
                        writer.Write(errorDivByZero);
                        break;
                    default:
                        throw new Exception("Unhandled EvaluationClass.Errors instance.");
                }
            }
            // NOTE: Will be reworked as next project.
        }
    }
}
