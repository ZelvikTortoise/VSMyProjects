using System;
using System.Collections.Generic;
using System.IO;


// Using the sample solution from our teacher and modifying it.
// Uses Visitor Patern with interfaces.
namespace ExpressionEvaluator
{
    interface IAlgorithms
    {

    }

    interface IPrintAlgorithms : IAlgorithms
    {
        // For new operations not yet implemented to this interface.
        void Visit(Expression e);

        void Visit(ConstantExpression e);
        void Visit(PlusExpression e);
        void Visit(MinusExpression e);
        void Visit(MultiplyExpression e);
        void Visit(DivideExpression e);
        void Visit(UnaryMinusExpression e);
    }


    interface IEvaluatingInDouble : IAlgorithms
    {
        // For new operations not yet implemented to this interface.
        double Visit(Expression e);

        double Visit(ConstantExpression e);
        double Visit(PlusExpression e);
        double Visit(MinusExpression e);
        double Visit(MultiplyExpression e);
        double Visit(DivideExpression e);
        double Visit(UnaryMinusExpression e);
    }

    // Only expressions with 
    class PrintSensiblyBracketInfixAlgorithm : IPrintAlgorithms
    {
        TextWriter writer = Console.Out;
        const int bufferSize = 2048;
        char[] buffer = new char[bufferSize];
        int firstInvalidIndex = 0;
        const string noPriorityKey = "";
        const string leftBracket = "(";
        const string rightBracket = ")";
        const string notImplementedOperationSymbol = "?";
        int previousPriority = priorities[noPriorityKey];
        bool finishRightBracket = false;
        enum Situations { FirstOperand, SecondOperand };
        Situations situation = Situations.FirstOperand;

        // Note: Priorities have spaces among each other which can be used by newly implemented operations.
        static Dictionary<string, int> priorities = new Dictionary<string, int>
        {
            {noPriorityKey, int.MinValue},

            {"+", 0},
            {"-1", 0},

            {"-2", 25},

            {"*", 50},
            {"/1", 50},

            {"/2", 75},

            {"~", 100}
        };
        // Unary minus has the highest priority (= - ~ 1 ~ 2 will be displayed as -1--2).
        // Binary minus, binary division need two different priorities. (For left and right operand.)
        // Default values: +, -1, *, /1, ~.

        private void AddToBuffer(string s)
        {
            if (firstInvalidIndex + s.Length <= bufferSize)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    buffer[firstInvalidIndex] = s[i];
                    firstInvalidIndex++;
                }
            }
            else
            {
                int printNow = bufferSize - firstInvalidIndex;
                for (int i = 0; i < printNow; i++)
                {
                    buffer[firstInvalidIndex] = s[i];

                    firstInvalidIndex++;
                }
                writer.Write(buffer);
                firstInvalidIndex = 0;
                AddToBuffer(s.Remove(0, printNow));
            }
        }

        public void EmptyBuffer()
        {
            if (firstInvalidIndex > 0)
            {
                writer.Write(buffer, 0, firstInvalidIndex);
                firstInvalidIndex = 0;
            }
            // else: the buffer is already empty
        }

        private void Print(string text)
        {
            AddToBuffer(text);
        }
        private void PrintLeftBracket()
        {
            AddToBuffer(leftBracket);
        }
        private void PrintRightBracket()
        {
            AddToBuffer(rightBracket);
        }

        // For new operations not yet implemented to the interface of this class.
        public void Visit(Expression expr)
        {
            Print(notImplementedOperationSymbol);
        }

        public void Visit(ConstantExpression expr)
        {
            Print(expr.Value.ToString());
        }

        public void Visit(PlusExpression expr)
        {
            int memorizedPriority;
            bool memorizedFinishRightBracket;

            memorizedPriority = previousPriority;
            if (priorities["+"] < previousPriority)
            {
                PrintLeftBracket();
                finishRightBracket = true;
            }
            else
            {
                finishRightBracket = false;
            }
            memorizedFinishRightBracket = finishRightBracket;

            previousPriority = priorities["+"];
            situation = Situations.FirstOperand;
            expr.Op0.Accept(this);
            Print("+");
            previousPriority = priorities["+"];
            situation = Situations.SecondOperand;
            expr.Op1.Accept(this);

            finishRightBracket = memorizedFinishRightBracket;
            if (finishRightBracket)
            {
                PrintRightBracket();
            }
            previousPriority = memorizedPriority;
        }

        public void Visit(MinusExpression expr)
        {
            int memorizedPriority;
            bool memorizedFinishRightBracket;

            memorizedPriority = previousPriority;
            if (priorities["-1"] < previousPriority)
            {
                PrintLeftBracket();
                finishRightBracket = true;
            }
            else
            {
                finishRightBracket = false;
            }
            memorizedFinishRightBracket = finishRightBracket;

            previousPriority = priorities["-1"];
            situation = Situations.FirstOperand;
            expr.Op0.Accept(this);
            Print("-");
            previousPriority = priorities["-2"];
            situation = Situations.SecondOperand;
            expr.Op1.Accept(this);

            finishRightBracket = memorizedFinishRightBracket;
            if (finishRightBracket)
            {
                PrintRightBracket();
            }
            previousPriority = memorizedPriority;
        }

        public void Visit(MultiplyExpression expr)
        {
            int memorizedPriority;
            bool memorizedFinishRightBracket;

            memorizedPriority = previousPriority;
            if (priorities["*"] < previousPriority)
            {
                PrintLeftBracket();
                finishRightBracket = true;
            }
            else
            {
                finishRightBracket = false;
            }
            memorizedFinishRightBracket = finishRightBracket;

            previousPriority = priorities["*"];
            situation = Situations.FirstOperand;
            expr.Op0.Accept(this);
            Print("*");
            previousPriority = priorities["*"];
            situation = Situations.SecondOperand;
            expr.Op1.Accept(this);

            finishRightBracket = memorizedFinishRightBracket;
            if (finishRightBracket)
            {
                PrintRightBracket();
            }
            previousPriority = memorizedPriority;
        }

        public void Visit(DivideExpression expr)
        {
            int memorizedPriority;
            bool memorizedFinishRightBracket;

            memorizedPriority = previousPriority;
            if (priorities["/1"] < previousPriority)
            {
                PrintLeftBracket();
                finishRightBracket = true;
            }
            else
            {
                finishRightBracket = false;
            }
            memorizedFinishRightBracket = finishRightBracket;

            previousPriority = priorities["/1"];
            situation = Situations.FirstOperand;
            expr.Op0.Accept(this);
            Print("/");
            previousPriority = priorities["/2"];
            situation = Situations.SecondOperand;
            expr.Op1.Accept(this);

            finishRightBracket = memorizedFinishRightBracket;
            if (finishRightBracket)
            {
                PrintRightBracket();
            }
            previousPriority = memorizedPriority;
        }

        public void Visit(UnaryMinusExpression expr)
        {
            int memorizedPriority;
            bool memorizedFinishRightBracket;

            memorizedPriority = previousPriority;
            string currentPriorityKey;
            switch (situation)
            {
                case Situations.FirstOperand:
                    currentPriorityKey = "~";
                    break;
                case Situations.SecondOperand:
                    currentPriorityKey = "~";
                    break;
                default:
                    throw new Exception("Unary minus operator called by PrintSensiblyBracketInfixAlgorithm class with unhandled Situation value.");
            }
            if (priorities[currentPriorityKey] < previousPriority)
            {
                PrintLeftBracket();
                finishRightBracket = true;
            }
            else
            {
                finishRightBracket = false;
            }
            memorizedFinishRightBracket = finishRightBracket;

            Print("-");     //  We shall use the same symbol as for binary minus according to the task specification.
            previousPriority = priorities["~"];
            expr.Op.Accept(this);

            finishRightBracket = memorizedFinishRightBracket;
            if (finishRightBracket)
            {
                PrintRightBracket();
            }
            previousPriority = memorizedPriority;
        }
    }

    class PrintFullyBracketInfixAlgorithm : IPrintAlgorithms
    {
        TextWriter writer = Console.Out;
        const int bufferSize = 2048;
        char[] buffer = new char[bufferSize];
        int firstInvalidIndex = 0;
        const string leftBracket = "(";
        const string rightBracket = ")";
        const string notImplementedOperationSymbol = "?";

        private void AddToBuffer(string s)
        {
            if (firstInvalidIndex + s.Length <= bufferSize)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    buffer[firstInvalidIndex] = s[i];
                    firstInvalidIndex++;
                }
            }
            else
            {
                int printNow = bufferSize - firstInvalidIndex;
                for (int i = 0; i < printNow; i++)
                {
                    buffer[firstInvalidIndex] = s[i];

                    firstInvalidIndex++;
                }
                writer.Write(buffer);
                firstInvalidIndex = 0;
                AddToBuffer(s.Remove(0, printNow));
            }
        }

        public void EmptyBuffer()
        {
            if (firstInvalidIndex > 0)
            {
                writer.Write(buffer, 0, firstInvalidIndex);
                firstInvalidIndex = 0;
            }
            // else: the buffer is already empty
        }

        private void Print(string text)
        {
            AddToBuffer(text);
        }
        private void PrintLeftBracket()
        {
            AddToBuffer(leftBracket);
        }
        private void PrintRightBracket()
        {
            AddToBuffer(rightBracket);
        }

        // For new operations not yet implemented to the interface of this class.
        public void Visit(Expression expr)
        {
            Print(notImplementedOperationSymbol);
        }

        public void Visit(ConstantExpression expr)
        {
            Print(expr.Value.ToString());
        }

        public void Visit(UnaryMinusExpression expr)
        {
            PrintLeftBracket();
            Print("-");     //  We shall use the same symbol as for binary minus according to the task specification.
            expr.Op.Accept(this);
            PrintRightBracket();
        }

        public void Visit(PlusExpression expr)
        {
            PrintLeftBracket();
            expr.Op0.Accept(this);
            Print("+");
            expr.Op1.Accept(this);
            PrintRightBracket();
        }

        public void Visit(MinusExpression expr)
        {
            PrintLeftBracket();
            expr.Op0.Accept(this);
            Print("-");
            expr.Op1.Accept(this);
            PrintRightBracket();
        }

        public void Visit(MultiplyExpression expr)
        {
            PrintLeftBracket();
            expr.Op0.Accept(this);
            Print("*");
            expr.Op1.Accept(this);
            PrintRightBracket();
        }

        public void Visit(DivideExpression expr)
        {
            PrintLeftBracket();
            expr.Op0.Accept(this);
            Print("/");
            expr.Op1.Accept(this);
            PrintRightBracket();
        }
    }

    class EvaluationAlgorithmInDouble : IEvaluatingInDouble
    {
        // For new operations not yet implemented to the interface of this class.
        public double Visit(Expression expr)
        {
            return Double.NaN;   // Returning NaN for not fully implemented operations.
        }

        public double Visit(ConstantExpression expr)
        {
            return (double)expr.Value;
        }

        public double Visit(UnaryMinusExpression expr)
        {
            return -expr.Op.Accept(this);
        }

        public double Visit(PlusExpression expr)
        {
            return expr.Op0.Accept(this) + expr.Op1.Accept(this);
        }

        public double Visit(MinusExpression expr)
        {
            return expr.Op0.Accept(this) - expr.Op1.Accept(this);
        }

        public double Visit(MultiplyExpression expr)
        {
            return expr.Op0.Accept(this) * expr.Op1.Accept(this);
        }

        public double Visit(DivideExpression expr)
        {
            return expr.Op0.Accept(this) / expr.Op1.Accept(this);
        }
    }

    abstract class Expression
    {
        public static Expression ParsePrefixExpression(string exprString)
        {
            string[] tokens = exprString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Expression result = null;
            Stack<OperatorExpression> unresolved = new Stack<OperatorExpression>();
            foreach (string token in tokens)
            {
                if (result != null)
                {
                    // We correctly parsed the whole tree, but there was at least one more unprocessed token left.
                    // This implies incorrect input, thus return null.

                    return null;
                }

                switch (token)
                {
                    case "+":
                        unresolved.Push(new PlusExpression());
                        break;

                    case "-":
                        unresolved.Push(new MinusExpression());
                        break;

                    case "*":
                        unresolved.Push(new MultiplyExpression());
                        break;

                    case "/":
                        unresolved.Push(new DivideExpression());
                        break;

                    case "~":
                        unresolved.Push(new UnaryMinusExpression());
                        break;

                    default:
                        int value;
                        if (!int.TryParse(token, out value))
                        {
                            return null;    // Invalid token format
                        }

                        Expression expr = new ConstantExpression(value);
                        while (unresolved.Count > 0)
                        {
                            OperatorExpression oper = unresolved.Peek();
                            if (oper.AddOperand(expr))
                            {
                                unresolved.Pop();
                                expr = oper;
                            }
                            else
                            {
                                expr = null;
                                break;
                            }
                        }

                        if (expr != null)
                        {
                            result = expr;
                        }

                        break;
                }
            }

            return result;
        }

        public abstract int Evaluate();
        public abstract double Accept(IEvaluatingInDouble alg);
        public abstract void Accept(IPrintAlgorithms alg);
    }

    abstract class ValueExpression : Expression
    {
        public abstract int Value
        {
            get;
        }

        public sealed override int Evaluate()
        {
            return Value;
        }
    }

    sealed class ConstantExpression : ValueExpression
    {
        private int value;

        public ConstantExpression(int value)
        {
            this.value = value;
        }

        public override int Value
        {
            get { return this.value; }
        }

        public override double Accept(IEvaluatingInDouble alg)
        {
            return alg.Visit(this);
        }

        public override void Accept(IPrintAlgorithms alg)
        {
            alg.Visit(this);
        }
    }

    abstract class OperatorExpression : Expression
    {
        public abstract bool AddOperand(Expression op);
    }

    abstract class UnaryExpression : OperatorExpression
    {
        protected Expression op;

        public Expression Op
        {
            get { return op; }
            set { op = value; }
        }

        public override bool AddOperand(Expression op)
        {
            if (this.op == null)
            {
                this.op = op;
            }
            return true;
        }

        public sealed override int Evaluate()
        {
            return Evaluate(op.Evaluate());
        }

        protected abstract int Evaluate(int opValue);
    }

    abstract class BinaryExpression : OperatorExpression
    {
        protected Expression op0, op1;

        public Expression Op0
        {
            get { return op0; }
            set { op0 = value; }
        }

        public Expression Op1
        {
            get { return op1; }
            set { op1 = value; }
        }

        public override bool AddOperand(Expression op)
        {
            if (op0 == null)
            {
                op0 = op;
                return false;
            }
            else if (op1 == null)
            {
                op1 = op;
            }
            return true;
        }

        public sealed override int Evaluate()
        {
            return Evaluate(op0.Evaluate(), op1.Evaluate());
        }

        protected abstract int Evaluate(int op0Value, int op1Value);
    }

    sealed class PlusExpression : BinaryExpression
    {
        public override double Accept(IEvaluatingInDouble alg)
        {
            return alg.Visit(this);
        }

        public override void Accept(IPrintAlgorithms alg)
        {
            alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return checked(op0Value + op1Value);
        }
    }

    sealed class MinusExpression : BinaryExpression
    {
        public override double Accept(IEvaluatingInDouble alg)
        {
            return alg.Visit(this);
        }

        public override void Accept(IPrintAlgorithms alg)
        {
            alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return checked(op0Value - op1Value);
        }
    }

    sealed class MultiplyExpression : BinaryExpression
    {
        public override double Accept(IEvaluatingInDouble alg)
        {
            return alg.Visit(this);
        }

        public override void Accept(IPrintAlgorithms alg)
        {
            alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return checked(op0Value * op1Value);
        }
    }

    sealed class DivideExpression : BinaryExpression
    {
        public override double Accept(IEvaluatingInDouble alg)
        {
            return alg.Visit(this);
        }

        public override void Accept(IPrintAlgorithms alg)
        {
            alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return (op0Value / op1Value);
        }
    }

    sealed class UnaryMinusExpression : UnaryExpression
    {
        public override double Accept(IEvaluatingInDouble alg)
        {
            return alg.Visit(this);
        }

        public override void Accept(IPrintAlgorithms alg)
        {
            alg.Visit(this);
        }

        protected override int Evaluate(int opValue)
        {
            return checked(-opValue);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EvaluationAlgorithmInDouble doubleEvaluationAlg = new EvaluationAlgorithmInDouble();
            PrintFullyBracketInfixAlgorithm printFullyBracketInfixAlg = new PrintFullyBracketInfixAlgorithm();
            PrintSensiblyBracketInfixAlgorithm printSensiblyBracketInfixAlg = new PrintSensiblyBracketInfixAlgorithm();
            Expression expr = null;
            string line;

            while (true)
            {
                line = Console.ReadLine();
                switch (line)
                {
                    case "end":
                    case null:
                        // Program ends.
                        return;
                    case "i":
                        if (expr == null)
                        {
                            Console.WriteLine("Expression Missing");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine(expr.Evaluate().ToString());
                            }
                            catch (DivideByZeroException)
                            {
                                Console.WriteLine("Divide Error");
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Overflow Error");
                            }
                        }
                        break;
                    case "d":
                        if (expr == null)
                        {
                            Console.WriteLine("Expression Missing");
                        }
                        else
                        {
                            double result = expr.Accept(doubleEvaluationAlg);
                            if (result == Double.PositiveInfinity)
                            {
                                Console.WriteLine("Infinity");
                            }
                            else if (result == Double.NegativeInfinity)
                            {
                                Console.WriteLine("-Infinity");
                            }
                            else
                            {
                                Console.WriteLine(result.ToString("f05"));
                            }
                        }
                        break;
                    case "p":
                        if (expr == null)
                        {
                            Console.WriteLine("Expression Missing");
                        }
                        else
                        {
                            expr.Accept(printFullyBracketInfixAlg);
                            printFullyBracketInfixAlg.EmptyBuffer();
                            Console.WriteLine();
                        }
                        break;
                    case "P":
                        if (expr == null)
                        {
                            Console.WriteLine("Expression Missing");
                        }
                        else
                        {
                            expr.Accept(printSensiblyBracketInfixAlg);
                            printSensiblyBracketInfixAlg.EmptyBuffer();
                            Console.WriteLine();
                        }
                        break;
                    case "":
                        // Nothing happens.
                        break;
                    default:
                        if (line.StartsWith("= "))
                        {
                            line = line.Remove(0, 2);

                            // Only one ' ' can be after '='.
                            if (line.StartsWith(" "))
                            {
                                expr = null;
                            }
                            else
                            {
                                expr = Expression.ParsePrefixExpression(line);
                            }
                        }
                        else
                        {
                            // Some kind of invalid text input.
                            expr = null;
                        }

                        if (expr == null)
                        {
                            Console.WriteLine("Format Error");
                        }
                        break;
                }
            }
        }
    }
}