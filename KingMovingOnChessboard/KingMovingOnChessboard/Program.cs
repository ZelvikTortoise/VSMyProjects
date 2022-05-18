using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KingMovingOnChessboard
{
    class NumberReader  // upravená třída Ctecka od pana doktora Holana.
    {
        private static char? lastChar = null;

        public static int ReadNum()
        {
            int z;
            bool negative = false;

            if (lastChar.HasValue)
                z = (char)lastChar;
            else
                z = Console.Read();

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

            if (z != -1)
                lastChar = (char)z;
            else
                lastChar = null;

            if (negative)
                return -x;
            else
                return x;
        }
    }

    class Square
    {
        public bool Available { get; set; }
        public int Value { get; set; }
        public bool OnPath { get; set; }

        public Square(int val, bool avail, bool onPath)
        {
            this.Value = val;
            this.Available = avail;
            this.OnPath = onPath;
        }
    }

    class Chessboard
    {
        public int RowCount { get; }
        public int ColumnCount { get; }
        public Square[,] Board { get; }
        public const int startValue = int.MaxValue;

        public Chessboard(int rows, int columns, int[] obstacles)
        {
            this.RowCount = rows;
            this.ColumnCount = columns;
            this.Board = new Square[rows, columns];
            InicializeBoard();
            MakeObstacles(obstacles);
        }

        private void InicializeBoard()
        {
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                    Board[i, j] = new Square(startValue, true, false);                
        }

        private void MakeObstacles(int[] obstacles)
        {
            for (int i = 0; i < obstacles.Length / 2; i++)
                this.Board[obstacles[2 * i], obstacles[2 * i + 1]].Available = false;
        }
    }

    class King
    {
        public int X { get; set; }
        public int Y { get; set; }
        public const int failnum = -1;

        private Queue<Tuple<int, int>> Queue = new Queue<Tuple<int, int>>();

        public int TryReachEnd(Chessboard board, int endX, int endY)
        {
            int leastAmountOfSteps = failnum;
            Queue.Enqueue(new Tuple<int, int>(this.X, this.Y));
            board.Board[this.X, this.Y].Value = 0;

            while (Queue.Count > 0)
            {
                if ((leastAmountOfSteps = GeneratePossiblePositions(board, endX, endY)) != failnum)
                    break;
            }
                

            return leastAmountOfSteps;
        }        
        public void PrintPath(TextWriter printer, Chessboard board, int endX, int endY)
        {
            int[] path = GetPath(board, endX, endY);
            const char coordinateSeparator = ' ';

            if (path.Length == 1)
                printer.Write(path[0]);
            else
                for (int i = 0; i < path.Length - 1; i += 2)    // x, y, x, y, x, y, ...
                {
                    printer.Write(path[i] + Program.deviation);
                    printer.Write(coordinateSeparator);
                    printer.WriteLine(path[i + 1] + Program.deviation);
                }
        }

        private void AddSquareToArray(int[] squares, ref int currentX, int squareX, int squareY)
        {
            squares[currentX] = squareX;
            squares[currentX + 1] = squareY;
            currentX -= 2;
        }
        private int[] GetPath(Chessboard board, int endX, int endY)
        {
            this.X = endX;
            this.Y = endY;
            if (board.Board[this.X, this.Y].Value == Chessboard.startValue)
                return new int[] { failnum };
            else
            {
                int[] path = new int[2 * (board.Board[this.X, this.Y].Value + 1)];    // Path has a length of layer + 1 squares and each square has two coordinates.
                int currentSquareXIndex = 2 * board.Board[this.X, this.Y].Value;    // Index of x coordinate of our nearest known square from the start on the path.
                bool found = false;

                AddSquareToArray(path, ref currentSquareXIndex, this.X, this.Y);

                int layerEnd = board.Board[this.X, this.Y].Value;
                for (int layer = layerEnd - 1; layer >= 0; layer--)  // Starting 1 square away from end.
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if ((this.X + i >= 0 && this.X + i < board.ColumnCount && this.Y + j >= 0 && this.Y + j < board.RowCount) && board.Board[this.X + i, this.Y + j].Value == layer && board.Board[this.X + i, this.Y + j].Available)    // The last condition is not necessary.
                            {
                                // Moving the king.
                                this.X += i;
                                this.Y += j;
                                // Adding path square to our array.
                                AddSquareToArray(path, ref currentSquareXIndex, this.X, this.Y);
                                found = true;
                                break;
                            }
                        }
                        if (found)  // Escaping cycle after finding.
                        {
                            found = false;
                            break;
                        }
                    }                       
                }

                return path;
            }
        }
        private int GeneratePossiblePositions(Chessboard board, int endX, int endY)
        {
            Tuple<int, int> currentSqaure = Queue.Dequeue();
            int x = currentSqaure.Item1;
            int y = currentSqaure.Item2;

            int layer = board.Board[x, y].Value + 1;

            if (x == endX && y == endY)
                return board.Board[x, y].Value;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (TryMoveKing(x, y, i, j, out int NewX, out int NewY, board))
                    {
                        if (board.Board[NewX, NewY].Value > layer)  // .Value not assigned yet
                        {
                            board.Board[NewX, NewY].Value = layer;  // .Value assigning to the correct layer
                            Queue.Enqueue(new Tuple<int, int>(NewX, NewY));
                        }
                    }
                }
            }

            return failnum;
        }
        private bool TryMoveKing(int xNow, int yNow, int xMove, int yMove, out int xNew, out int yNew, Chessboard board)
        {
            xNew = xNow + xMove;
            yNew = yNow + yMove;
            if (xNew < 0 || xNew >= board.ColumnCount || yNew < 0 || yNew >= board.RowCount)
            {
                xNew = -1;
                yNew = -1;
                return false;
            }
            else if (!board.Board[xNew, yNew].Available)
                return false;
            else
                return true;
        }


        public King(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    class Program
    {
        public const int deviation = 1;
        static void Main(string[] args)
        {
            // Input:            
            int obstacleCount = NumberReader.ReadNum();
            int[] obstacles = new int[2 * obstacleCount];   // x, y; x, y; x, y; ...
            for (int i = 0; i < obstacles.Length; i++)
                obstacles[i] = NumberReader.ReadNum() - deviation;
            int kingX = NumberReader.ReadNum() - deviation;
            int kingY = NumberReader.ReadNum() - deviation;
            int endX = NumberReader.ReadNum() - deviation;
            int endY = NumberReader.ReadNum() - deviation;

            // Objects:
            King king = new King(kingX, kingY);
            Chessboard board = new Chessboard(8, 8, obstacles);

            // Output:
            if (king.TryReachEnd(board, endX, endY) == King.failnum)
                Console.Write(King.failnum);
            else
                king.PrintPath(Console.Out, board, endX, endY);
        }
    }
}
