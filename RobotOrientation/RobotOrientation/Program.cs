using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobotOrientation
{
    class Robot
    {
        public enum Direction { xNeg, xPoz, yNeg, yPoz, zNeg, zPoz };
        public enum Rotation { rightLeft, upDown, rightLeftSide }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }       
        public Direction InFront { get; private set; } = Direction.zPoz;
        public Direction AboveHead { get; private set; } = Direction.yPoz;
        public Direction ToTheRight { get; private set; } = Direction.xPoz;
        
        private Direction[] XY { get; } = { Direction.xPoz, Direction.yPoz, Direction.xNeg, Direction.yNeg };
        private Direction[] YZ { get; } = { Direction.yPoz, Direction.zPoz, Direction.yNeg, Direction.zNeg };
        private Direction[] ZX { get; } = { Direction.zPoz, Direction.xPoz, Direction.zNeg, Direction.xNeg };
        
        private bool GetCurrentOrientation(out Direction[] plain, out bool reverseMovement, Rotation rotation)
        {
            Direction dir1, dir2;
            int index = -1; // -1 for out of range exception in case of logical error
            plain = null;
            reverseMovement = false;

            // Rotating in direction of dir2 is normal, inverse rotation is reversed.
            switch (rotation)
            {
                case Rotation.rightLeft:
                    dir1 = this.InFront;
                    dir2 = this.ToTheRight;
                    break;
                case Rotation.upDown:
                    dir1 = this.InFront;
                    dir2 = this.AboveHead;
                    break;
                case Rotation.rightLeftSide:
                    dir1 = this.AboveHead;
                    dir2 = this.ToTheRight;
                    break;
                default:
                    return false;
            }

            // Getting correct plain.
            if (dir1 == Direction.xNeg || dir1 == Direction.xPoz)
            {
                if (dir2 == Direction.yNeg || dir2 == Direction.yPoz)
                    plain = this.XY;
                else
                    plain = this.ZX;
            }
            else if (dir1 == Direction.yNeg || dir1 == Direction.yPoz)
            {
                if (dir2 == Direction.xNeg || dir2 == Direction.xPoz)
                    plain = this.XY;
                else
                    plain = this.YZ;
            }
            else   // dir1 == Direction.zNeg || dir1 == Direction.zPoz)
            {
                if (dir2 == Direction.xNeg || dir2 == Direction.xPoz)
                    plain = this.ZX;
                else
                    plain = this.YZ;
            }

            // Finding dir1's index.
            for (int i = 0; i < plain.Length; i++)
                if (dir1 == plain[i])
                {
                    index = i;
                    break;
                }

            // Getting value of reverseMovement.
            // Note: Rotating in direction of dir2 is normal, that means + 1 in array is normal.
            if (index == 0)
            {
                if (dir2 == plain[index + 1])
                    reverseMovement = false;
                else if (dir2 == plain[plain.Length - 1])
                    reverseMovement = true;
                else
                    return false;
            }
            else if (index == plain.Length - 1)
            {
                if (dir2 == plain[0])
                    reverseMovement = false;
                else if (dir2 == plain[index - 1])
                    reverseMovement = true;
                else
                    return false;
            }
            else
            {
                if (dir2 == plain[index + 1])
                    reverseMovement = false;
                else if (dir2 == plain[index - 1])
                    reverseMovement = true;
                else
                    return false;
            }

            return true;
        }
        private Direction GetNewDirection(Direction[] plain, bool reversed, Direction currentState)
        {
            int currentIndex = 0, newIndex;
            for (int i = 0; i < plain.Length; i++)
                if (plain[i] == currentState)
                {
                    currentIndex = i;
                    break;
                }

            if (!reversed)
                newIndex = currentIndex + 1;
            else
                newIndex = currentIndex - 1;

            if (newIndex == plain.Length)
                newIndex = 0;
            else if (newIndex == -1)
                newIndex = plain.Length - 1;

            return plain[newIndex];
        }

        private void MoveByOne()
        {
            switch (this.InFront)
            {
                case Direction.xNeg:
                    this.X--;
                    break;
                case Direction.xPoz:
                    this.X++;
                    break;
                case Direction.yNeg:
                    this.Y--;
                    break;
                case Direction.yPoz:
                    this.Y++;
                    break;
                case Direction.zNeg:
                    this.Z--;
                    break;
                case Direction.zPoz:
                    this.Z++;
                    break;
                default:
                    throw new ArgumentException("Unhandled value of Direction enumeration type.");
            }
        }
        private void TurnLeft()
        {
            if (GetCurrentOrientation(out Direction[] plain, out bool reverse, Rotation.rightLeft))
            {
                // Left is reversed movement itself by default.
                this.InFront = GetNewDirection(plain, !reverse, this.InFront);
                this.ToTheRight = GetNewDirection(plain, !reverse, this.ToTheRight);
            }
            else
                throw new ArgumentException("Unhandled values in {0}(...) method.", nameof(GetCurrentOrientation));

        }
        private void TurnRight()
        {
            if (GetCurrentOrientation(out Direction[] plain, out bool reverse, Rotation.rightLeft))
            {
                this.InFront = GetNewDirection(plain, reverse, this.InFront);
                this.ToTheRight = GetNewDirection(plain, reverse, this.ToTheRight);
            }
            else
                throw new ArgumentException("Unhandled values in {0}(...) method.", nameof(GetCurrentOrientation));
        }
        private void TurnUp()
        {
            if (GetCurrentOrientation(out Direction[] plain, out bool reverse, Rotation.upDown))
            {
                this.InFront = GetNewDirection(plain, reverse, this.InFront);
                this.AboveHead = GetNewDirection(plain, reverse, this.AboveHead);
            }
            else
                throw new ArgumentException("Unhandled values in {0}(...) method.", nameof(GetCurrentOrientation));
        }
        private void TurnDown()
        {
            if (GetCurrentOrientation(out Direction[] plain, out bool reverse, Rotation.upDown))
            {
                // Down is reversed movement itself by default.
                this.InFront = GetNewDirection(plain, !reverse, this.InFront);
                this.AboveHead = GetNewDirection(plain, !reverse, this.AboveHead);
            }
            else
                throw new ArgumentException("Unhandled values in {0}(...) method.", nameof(GetCurrentOrientation));
        }
        private void TurnToLeftSide()
        {
            if (GetCurrentOrientation(out Direction[] plain, out bool reverse, Rotation.rightLeftSide))
            {
                // Turn on left side is reversed movement itself by default.
                this.ToTheRight= GetNewDirection(plain, !reverse, this.ToTheRight);
                this.AboveHead = GetNewDirection(plain, !reverse, this.AboveHead);
            }
            else
                throw new ArgumentException("Unhandled values in {0}(...) method.", nameof(GetCurrentOrientation));
        }
        private void TurnToRightSide()
        {
            if (GetCurrentOrientation(out Direction[] plain, out bool reverse, Rotation.rightLeftSide))
            {              
                this.ToTheRight = GetNewDirection(plain, reverse, this.ToTheRight);
                this.AboveHead = GetNewDirection(plain, reverse, this.AboveHead);
            }
            else
                throw new ArgumentException("Unhandled values in {0}(...) method.", nameof(GetCurrentOrientation));
        }

        public void ProcessCommand(char command)
        {
            switch (command)
            {
                case 'F':
                    MoveByOne();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                case 'R':
                    TurnRight();
                    break;
                case 'U':
                    TurnUp();
                    break;
                case 'D':
                    TurnDown();
                    break;
                case '<':
                    TurnToLeftSide();
                    break;
                case '>':
                    TurnToRightSide();
                    break;
                default:
                    throw new ArgumentException("Unhandled command.");
            }
        }
        public void PrintCurrentPozition(TextWriter printer)
        {
            const char separator = ' ';
            StringBuilder sb = new StringBuilder();

            sb.Append(this.X.ToString());
            sb.Append(separator);
            sb.Append(this.Y.ToString());
            sb.Append(separator);
            sb.Append(this.Z.ToString());
            
            printer.WriteLine(sb.ToString());
        }

        public Robot()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const char stopChar = '.';
            Robot robot = new Robot();            
            int commandInt;
            while ((commandInt = Console.Read()) != -1 && commandInt != stopChar)
            {
                robot.ProcessCommand((char)commandInt);
                robot.PrintCurrentPozition(Console.Out);
            }

            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}