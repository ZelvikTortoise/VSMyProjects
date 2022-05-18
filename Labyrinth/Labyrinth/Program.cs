using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Reader  // edited class Ctecka from RNDr. Holan, PhD.
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

        /// <summary>
        /// Reads a character / a line of input, returns (and remembers) the first character read.
        /// If none character has been read due to the end of input, returns and remembers null.
        /// </summary>
        /// <param name="newLine">Shall the reader skip to new line with remembering only the first character read?</param>
        /// <returns>First read character if any. Otherwise null.</returns>
        public static char? ReadChar(bool newLine)
        {
            char? ch;

            if (newLine)
            {
                string input = Console.ReadLine();
                if (input != null)
                    ch = input[0];
                else
                    ch = null;
            }
            else
            {
                int intCh = Console.Read();

                if (intCh != -1)
                    ch = (char)intCh;
                else
                    ch = null;
            }

            lastChar = ch;
            return ch;
        }
    }

    class Labyrinth
    {
        const char wall = 'X';
        const char space = '.';
        readonly char[] monsterChars = new char[4] { '^', 'v', '>', '<' };

        // For free spaces – true; for walls – false.
        public readonly bool[,] labyrinth;

        public int Height { get; }
        public int Width { get; }

        // List of monsters in the labyrinth.
        private List<Monster> monsters = new List<Monster>();

        const string warning = "WARNING: Due to erros during constructing the labyrinth it is very possible it will not work properly.\nPlease restart the program and type in proper input.";

        internal bool IsMonster(int x, int y)
        {
            foreach (Monster m in monsters)
            {
                if (m.X == x && m.Y == y)
                    return true;            
            }

            return false;
        }
        private bool IsThisCharMonster(char element, out char? direction)
        {
            bool isMonster;
                
            switch (element)
            {
                case '^':                                       
                case 'v':
                case '>':
                case '<':
                    isMonster = true;
                    direction = element;
                    break;
                default:
                    isMonster = false;
                    direction = null;
                    break;
            }

            return isMonster;
        }

        // User interface:
        public void MoveAllMonsters()
        {
            foreach (Monster monster in this.monsters)
                monster.Action();
        }
        public void MapLabyrinthWithMonsters()
        {
            char[,] matrix = new char[this.Height, this.Width];

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (labyrinth[i, j])
                        matrix[i, j] = space;
                    else
                        matrix[i, j] = wall;
                }
            }

            char ch;
            foreach (Monster m in monsters)
            {
                switch (m.direction)
                {                    
                    case Monster.Direction.Up:
                        ch = '^';
                        break;
                    case Monster.Direction.Down:
                        ch = 'v';
                        break;
                    case Monster.Direction.Right:
                        ch = '>';
                        break;
                    case Monster.Direction.Left:
                        ch = '<';
                        break;
                    default:
                        throw new Exception("Unhandled value of enum Direction.");
                }
                matrix[m.X, m.Y] = ch;
            }

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();    // New line of the matrix.
            }
            Console.WriteLine();    // New line after the matrix.
        }

        public Labyrinth(int M, int N, char[,] maze)
        {
            this.Height = M;
            this.Width = N;

            labyrinth = new bool[M, N];

            try
            {
                for (int i = 0; i < M; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        switch (maze[i, j])
                        {
                            case wall:
                                this.labyrinth[i, j] = false;
                                break;
                            case space:
                                this.labyrinth[i, j] = true;
                                break;
                            default:
                                char? dir;
                                if (IsThisCharMonster(maze[i, j], out dir))
                                {
                                    // Monster found -> creating it and adding to our list.
                                    Monster monster = new Monster(this, i, j, (char)dir);
                                    this.monsters.Add(monster);

                                    this.labyrinth[i, j] = true;
                                }
                                else
                                    throw new ArgumentException("Unknown character in the labyrinth inicialization.");
                                break;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The arguments for the height and the width of the labyrinth differ from the actual labyrinth matrix inputed.");
                Console.WriteLine(warning);
            }
            catch (ArgumentException exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine(warning);
            }
        }
    } 
    class Monster
    {
        public readonly Labyrinth labyrinth;

        public enum Direction { Up, Down, Right, Left }
        public Direction direction { get; private set; }

        // With respect to the matrix coordinating: top-left corner is [0; 0].
        public int X { get; private set; }
        public int Y { get; private set; }

        // Procedures and atributes deciding movement:
        private bool lastMoveTurnRight = false;
        private bool hitAnotherMonsterRecently = false;
        private bool HasWallToTheRight()
        {
            bool x, plus;
            int change;

            // Seeking for the element of the matrix to the monster's right.
            switch (this.direction)
            {
                case Direction.Up:
                    x = true;
                    plus = true;
                    break;
                case Direction.Down:
                    x = true;
                    plus = false;
                    break;
                case Direction.Right:
                    x = false;
                    plus = true;
                    break;
                case Direction.Left:
                    x = false;
                    plus = false;
                    break;
                default:
                    throw new ArgumentException("Invalid direction of the monster!");
            }

            if (plus)
                change = 1;
            else
                change = -1;

            // Negating the value – REVERSED LOGIC: true – wall; false – empty
            if (x)
                return !labyrinth.labyrinth[this.X, this.Y + change];   // Changing column (moving on <-> x)
            else
                return !labyrinth.labyrinth[this.X + change, this.Y];   // Changing row (moving on ↓↑ y)
        }
        private bool CanContinueStraight(out bool hitMonster)
        {
            bool x, plus;
            int change;

            switch (this.direction)
            {
                case Direction.Up:
                    x = false;
                    plus = false;
                    break;
                case Direction.Down:
                    x = false;
                    plus = true;
                    break;
                case Direction.Right:
                    x = true;
                    plus = true;
                    break;
                case Direction.Left:
                    x = true;
                    plus = false;
                    break;
                default:
                    throw new ArgumentException("There is no monster in the labyrinth!");
            }

            if (plus)
                change = 1;
            else
                change = -1;

            // Looking for the proper element of the matrix and checking if it's possible to walk there.
            // Note: Monsters cannot walk on each other.
            if (x)
            {
                // Changing column (moving on <-> x)    
                hitMonster = labyrinth.IsMonster(this.X, this.Y + change);
                return labyrinth.labyrinth[this.X, this.Y + change] && !hitMonster;
            }
            else
            {
                // Changing row (moving on ↓↑ y)
                hitMonster = labyrinth.IsMonster(this.X + change, this.Y);
                return labyrinth.labyrinth[this.X + change, this.Y] && !hitMonster;
            }           
        }

        // Possible actions:
        private void MoveByOne()
        {
            bool x, plus;
            int change;

            switch (this.direction)
            {
                case Direction.Up:
                    x = false;
                    plus = false;
                    break;
                case Direction.Down:
                    x = false;
                    plus = true;
                    break;
                case Direction.Right:
                    x = true;
                    plus = true;
                    break;
                case Direction.Left:
                    x = true;
                    plus = false;
                    break;
                default:
                    throw new ArgumentException("There is no monster in the labyrinth!");
            }

            // The move:
            if (plus)
                change = 1;
            else
                change = -1;

            if (x)
                this.Y += change;   // Changing column (moving on <-> x)
            else
                this.X += change;   // Changing row (moving on ↓↑ y)
        }
        private void TurnLeft()
        {
            switch (this.direction)
            {
                case Direction.Up:
                    this.direction = Direction.Left;
                    break;
                case Direction.Down:
                    this.direction = Direction.Right;
                    break;
                case Direction.Right:
                    this.direction = Direction.Up;
                    break;
                case Direction.Left:
                    this.direction = Direction.Down;
                    break;
                default:
                    throw new ArgumentException("There is no monster in the labyrinth!");
            }
        }
        private void TurnRight()
        {
            switch (this.direction)
            {
                case Direction.Up:
                    this.direction = Direction.Right;
                    break;
                case Direction.Down:
                    this.direction = Direction.Left;
                    break;
                case Direction.Right:
                    this.direction = Direction.Down;
                    break;
                case Direction.Left:
                    this.direction = Direction.Up;
                    break;
                default:
                    throw new ArgumentException("There is no monster in the labyrinth!");
            }
        }

        /// <summary>
        /// The most important procedure of the monster – decides how to move.
        /// The task is to sneak near to wall in a way that we go around the boundaries of the labyrinth with a wall to monster's right.
        /// </summary>
        private void DecideAndDoIt()
        {
            if (HasWallToTheRight() || hitAnotherMonsterRecently)
            {
                if (CanContinueStraight(out hitAnotherMonsterRecently))
                    MoveByOne();
                else
                    TurnLeft();

                lastMoveTurnRight = false;
            }
            else if (!lastMoveTurnRight)
            {
                TurnRight();
                lastMoveTurnRight = true;
            }
            else if (CanContinueStraight(out hitAnotherMonsterRecently))
            {
                MoveByOne();
                lastMoveTurnRight = false;
            }
            else
                TurnRight();
        }
        
        public void Action()
        {
            DecideAndDoIt();
        }

        public Monster(Labyrinth lab, int x, int y, char dirChar)
        {
            this.labyrinth = lab;   // Or copy? (May be better but is not needed to accomplish our goal.)
            switch (dirChar)
            {
                case '^':
                    this.direction = Direction.Up;
                    break;
                case 'v':
                    this.direction = Direction.Down;
                    break;
                case '>':
                    this.direction = Direction.Right;
                    break;
                case '<':
                    this.direction = Direction.Left;
                    break;
                default:
                    throw new ArgumentException("There is no monster in the labyrinth!");
            }

            this.X = x;
            this.Y = y;
        }
    }

    class Program
    {
        private static bool IsPartOfMaze(char ch)
        {
            bool part;

            switch (ch)
            {
                case 'X':
                case '.':
                case '>':
                case 'v':
                case '<':
                case '^':
                    part = true;
                    break;
                default:
                    part = false;
                    break;
            }

            return part;
        }

        static void Main(string[] args)
        {
            // The amount of the monster's moves in the labyrinth.
            const int numberOfMoves = 20;

            // Carefull: Inputing N first, then M.
            char[,] inputLab;  // Inicialized in Input handling
            const char notInaMaze = (char)0;  // This character is not in maze

            #region Input handling
            // Labyrinth = matrix (type: M × N)
            int N = Reader.ReadNum();   // width of the matrix
            int M = Reader.ReadNum();   // height of the matrix

            // Validity check.
            if (N <= 0 || M <= 0)
            {
                Console.WriteLine("The M, N arguments have invalid values: M = {0}, N = {1}.", M, N);
                Console.WriteLine("Please restart the program and insert two positive integers.");

                Console.Write("Press any key to continue... ");
                Console.ReadKey();
                return;
            }

            // Reading the matrix from the input.
            inputLab = new char[M, N];
            char ch = notInaMaze;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    while (!IsPartOfMaze(ch))
                        ch = (char)Reader.ReadChar(false);  // If the explicit cast is invalid, it means the input is invalid.
                    inputLab[i, j] = ch;
                    ch = notInaMaze;
                }
            }
            #endregion
            // Variables "int M, N" inside.

            Labyrinth labyrinth = new Labyrinth(M, N, inputLab);

            for (int i = 1; i <= numberOfMoves; i++)
            {
                labyrinth.MoveAllMonsters();
                labyrinth.MapLabyrinthWithMonsters();
            }
        }
    }
}
