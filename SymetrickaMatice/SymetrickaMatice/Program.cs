using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SymetrickaMatice
{
    class NumberReader  // upravená třída Ctecka od pana doktora Holana.
    {
        public static int ReadNum()
        {
            bool negative = false;
            int z = Console.Read();
            while ((z < '0') || (z > '9'))
            {
                if (z == '-')
                    negative = true;    // Another z should break the cycle.

                z = Console.Read();
            }

            int x = 0;
            while ((z >= '0') && (z <= '9'))
            {
                x = 10 * x + z - '0';
                z = Console.Read();
            }

            if (negative)
                return -x;
            else
                return x;
        }
    }
    class Program
    {
        public static bool SymetricVerticalAxis(int[,] mat, int n)
        {
            bool symetric = true;
            int k;  // Last column index of the matrix's first half.
            /*if (n % 2 == 0)
            {
                k = n / 2 - 1;
            }
            else
            {
                k = n / 2 - 1;
            }*/
            if (n == 1)
                return symetric;
            else
                k = n / 2 - 1;

            for (int i = 0; i < n; i++)
                for (int j = 0; j <= k; j++)
                    if (mat[i, j] != mat[i, n - 1 - j])
                        return !symetric;

            return symetric;
        }
        public static bool SymetricMinorAxis(int[,] mat, int n)
        {
            bool symetric = true;

            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (mat[i, j] != mat[n - 1 - j, n - 1 - i])
                        return !symetric;

            return symetric;
        }
        public static bool SymetricMajorAxis(int[,] mat, int n)
        {
            bool symetric = true;

            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    if (mat[i, j] != mat[j, i])
                        return !symetric;

            return symetric;
        }

        static void Main(string[] args)
        {
            int N = NumberReader.ReadNum();
            if (N <= 0 || N >= 50)
                throw new ArgumentException("Expected value is out of range. Expected value range is 1–49.");
            int[,] matrix = new int[N, N];
            const byte yes = 1;
            const byte no = 0;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    matrix[i, j] = NumberReader.ReadNum();
            }

            if (SymetricMajorAxis(matrix, N))
                Console.Write(yes + " ");
            else
                Console.Write(no + " ");

            if (SymetricMinorAxis(matrix, N))
                Console.Write(yes + " ");
            else
                Console.Write(no + " ");

            if (SymetricVerticalAxis(matrix, N))
                Console.Write(yes);
            else
                Console.Write(no);
        }
    }
}
