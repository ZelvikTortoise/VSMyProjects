using System;
using System.Collections.Generic;


// Using the sample solution from our teacher and modifying it.
// Uses Visitor Patern.
namespace ExpressionEvaluator
{
    class EvaluationAlgorithmInDouble
    {
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
        public abstract double Accept(EvaluationAlgorithmInDouble alg);
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

        public override double Accept(EvaluationAlgorithmInDouble alg)
        {
            return alg.Visit(this);
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
        public override double Accept(EvaluationAlgorithmInDouble alg)
        {
            return alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return checked(op0Value + op1Value);
        }
    }

    sealed class MinusExpression : BinaryExpression
    {
        public override double Accept(EvaluationAlgorithmInDouble alg)
        {
            return alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return checked(op0Value - op1Value);
        }
    }

    sealed class MultiplyExpression : BinaryExpression
    {
        public override double Accept(EvaluationAlgorithmInDouble alg)
        {
            return alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return checked(op0Value * op1Value);
        }
    }

    sealed class DivideExpression : BinaryExpression
    {
        public override double Accept(EvaluationAlgorithmInDouble alg)
        {
            return alg.Visit(this);
        }

        protected override int Evaluate(int op0Value, int op1Value)
        {
            return (op0Value / op1Value);
        }
    }

    sealed class UnaryMinusExpression : UnaryExpression
    {
        public override double Accept(EvaluationAlgorithmInDouble alg)
        {
            return alg.Visit(this);
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
            EvaluationAlgorithmInDouble alg = new EvaluationAlgorithmInDouble();
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
                            double result = expr.Accept(alg);
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