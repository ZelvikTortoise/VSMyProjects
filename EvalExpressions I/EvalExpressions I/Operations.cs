using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EvalExpressions_I
{
    abstract class Operations
    {
        //public int OperandCount { get; set; }

        public virtual int GetValue(params int[] operands)
        {
            throw new Exception("Calling virtual method GetValue().");
        }
    }

    class Addition : Operations
    {
        /*public Addition(int operandCount)
        {
            OperandCount = operandCount;
        }*/

        public override int GetValue(params int[] operands)
        {
            int result = operands[0];
            long testResult = operands[0];
            for (int i = 1; i < operands.Length; i++)
            {
                result += operands[i];
                testResult += operands[i];
                if (result != testResult)
                {
                    throw new ArithmeticException("Addition overflow.");
                }
            }

            return result;
        }
    }

    class Subtraction : Operations
    {
        /*public Subtraction(int operandCount)
        {
            OperandCount = operandCount;
        }*/

        public override int GetValue(params int[] operands)
        {
            switch (operands.Length)
            {
                case 1:
                    if (operands[0] == int.MinValue)
                    {
                        throw new ArithmeticException("Unary subtraction overflow.");
                    }
                    return -operands[0];
                case 2:
                    int result = operands[0] - operands[1];
                    long testResult = operands[0] - operands[1];
                    if (result != testResult)
                    {
                        throw new ArithmeticException("Binary subtraction overflow.");
                    }
                    return result;
                default:
                    throw new Exception("Unhandled number of operands in subtraction.");
            }
        }
    }

    class Multiplication : Operations
    {
        /*public Multiplication(int operandCount)
        {
            OperandCount = operandCount;
        }*/

        public override int GetValue(params int[] operands)
        {
            int result = operands[0];
            long testResult = operands[0];
            for (int i = 1; i < operands.Length; i++)
            {
                result *= operands[i];
                testResult *= operands[i];
                if (result != testResult)
                {
                    throw new ArithmeticException("Multiplication overflow.");
                }
            }
            return result;
        }
    }

    class Division : Operations
    {
        /*public Division(int operandCount)
        {
            OperandCount = operandCount;
        }*/


        public override int GetValue(params int[] operands)
        {
            if (operands.Length != 2)
            {
                throw new Exception("Unhandled number of operands for division.");
            }

            if (operands[0] == int.MinValue && operands[1] == -1)
            {
                throw new ArithmeticException("Binary division overflow.");
            }

            if (operands[1] == 0)
            {
                throw new DivideByZeroException("Dividing by zero.");
            }
            /*if (operands[1] == 0)
            {
                return int.MinValue;
            }*/
            return operands[0] / operands[1];
        }
    }
}
