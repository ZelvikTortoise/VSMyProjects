using System;
using System.Collections.Generic;

namespace LockScreenPatterns
{
    class Program
    {
        static bool NotRepeated(List<int> pattern, int minimumNumberOfVertices)
        {            
            int a, b, c;
            if (minimumNumberOfVertices <= 2)
            {
                return true;
            }
            else if (pattern.Count <= minimumNumberOfVertices)
            {
                int smallestSkipped = int.MaxValue;

                for (int i = 0; i < pattern.Count - 1; i++)
                {
                    a = pattern[i];
                    b = pattern[i + 1];
                    if (a > b)
                    {
                        int temp = a;
                        a = b;
                        b = temp;
                    }

                    // Now a <= b:

                    switch (a)
                    {
                        case 1:
                            switch (b)
                            {
                                case 3:
                                    smallestSkipped = 2;
                                    break;
                                case 7:
                                    smallestSkipped = 4;
                                    break;
                                case 9:
                                    smallestSkipped = 5;
                                    break;
                            }
                            break;
                        case 2:
                            if (b == 8)
                            {
                                smallestSkipped = 5;
                                break;
                            }
                            break;
                        case 3:
                            switch (b)
                            {
                                case 7:
                                    smallestSkipped = 5;
                                    break;
                                case 9:
                                    smallestSkipped = 6;
                                    break;
                            }
                            break;
                        case 4:
                            if (b == 6)
                            {
                                smallestSkipped = 5;
                                break;
                            }
                            break;
                        case 7:
                            if (b == 9)
                            {
                                smallestSkipped = 8;
                                break;
                            }
                            break;
                    }
                }

                for (int i = 0; i < pattern.Count - 2; i++)
                {
                    a = pattern[i];
                    b = pattern[i + 1];
                    c = pattern[i + 2];
                    if (a > c)
                    {
                        int temp = a;
                        a = c;
                        c = temp;
                    }

                    if (b <= a || b >= c)
                    {
                        // Cannot be a line.
                        continue;
                    }

                    // Now a < b < c.                    

                    switch (a)
                    {
                        case 1:
                            switch (c)
                            {
                                case 3:
                                    if (b == 2)
                                    {
                                        if (smallestSkipped <= 2)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            continue;
                                        }                                        
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                case 7:
                                    if (b == 4)
                                    {
                                        if (smallestSkipped <= 4)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                case 9:
                                    if (b == 5)
                                    {
                                        if (smallestSkipped <= 5)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                            }
                            break;
                        case 2:
                            if (c == 8)
                            {
                                if (b == 5)
                                {
                                    if (smallestSkipped <= 5)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                        case 3:
                            switch (c)
                            {
                                case 7:
                                    if (b == 5)
                                    {
                                        if (smallestSkipped <= 5)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                case 9:
                                    if (b == 6)
                                    {
                                        if (smallestSkipped <= 6)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                            }
                            break;
                        case 4:
                            if (c == 6)
                            {
                                if (b == 5)
                                {
                                    if (smallestSkipped <= 5)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                        case 7:
                            if (c == 9)
                            {
                                if (b == 8)
                                {
                                    if (smallestSkipped <= 8)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                    }   
                }

                return true;
            }

            for (int i = 0; i < pattern.Count - 2; i++)
            {
                a = pattern[i];
                b = pattern[i + 1];
                c = pattern[i + 2];
                if (a > c)
                {
                    int temp = a;
                    a = c;
                    c = temp;
                }

                if (b <= a || b >= c)
                {
                    // Cannot be a line.
                    continue;
                }

                // Now a < b < c.

                switch (a)
                {
                    case 1:
                        switch (c)
                        {
                            case 3:
                                if (b == 2)
                                {
                                    return false;
                                }
                                else
                                {
                                    continue;
                                }                                
                            case 7:
                                if (b == 4)
                                {
                                    return false;
                                }
                                else
                                {
                                    continue;
                                }
                            case 9:
                                if (b == 5)
                                {
                                    return false;
                                }
                                else
                                {
                                    continue;
                                }
                        }
                        break;
                    case 2:
                        if (c == 8)
                        {
                            if (b == 5)
                            {
                                return false;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                    case 3:
                        switch (c)
                        {
                            case 7:
                                if (b == 5)
                                {
                                    return false;
                                }
                                else
                                {
                                    continue;
                                }
                            case 9:
                                if (b == 6)
                                {
                                    return false;
                                }
                                else
                                {
                                    continue;
                                }
                        }
                        break;
                    case 4:
                        if (c == 6)
                        {
                            if (b == 5)
                                {
                                    return false;
                                }
                                else
                                {
                                    continue;
                                }
                        }
                        break;
                    case 7:
                        if (c == 9)
                        {
                            if (b == 8)
                            {
                                return false;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                }
            }

            return true;
        }

        static bool Forbidden(bool[] allowed, int num)
        {
            return !allowed[num - 1];
        }

        static void MakeInvalid(bool[] allowed, int num)
        {
            allowed[num - 1] = false;
        }

        static bool PatternValid(List<int> pattern)
        {
            // 2 <= pattern.Count <= 9
            bool[] allowed = { true, true, true, true, true, true, true, true, true };       // Is expandable (add parameter List<int> rem) -> int[rem.Count], then all -> true.

            int a, b;
            for (int i = 0; i < pattern.Count - 1; i++)
            {
                if (Forbidden(allowed, pattern[i]))
                {
                    return false;
                }

                a = pattern[i];
                b = pattern[i + 1];
                if (a > b)
                {
                    int temp = a;
                    a = b;
                    b = temp;
                }

                switch (a)
                {
                    case 1:
                        switch (b)
                        {
                            case 3:
                                MakeInvalid(allowed, 2);
                                break;
                            case 7:
                                MakeInvalid(allowed, 4);
                                break;
                            case 9:
                                MakeInvalid(allowed, 5);
                                break;
                        }
                        break;
                    case 2:
                        if (b == 8)
                        {
                            MakeInvalid(allowed, 5);
                        }
                        break;
                    case 3:
                        switch (b)
                        {
                            case 7:
                                MakeInvalid(allowed, 5);
                                break;
                            case 9:
                                MakeInvalid(allowed, 6);
                                break;
                        }
                        break;
                    case 4:
                        if (b == 6)
                        {
                            MakeInvalid(allowed, 5);
                        }
                        break;
                    case 7:
                        if (b == 9)
                        {
                            MakeInvalid(allowed, 8);
                        }
                        break;
                }
            }

            return Forbidden(allowed, pattern[pattern.Count - 1]) ? false : true;
        }

        static void ChooseVerteces(List<int> pat, List<int> rem, ref int patNum, int verNum, int minVer, bool alwaysValid)
        {
            if (pat.Count == verNum)
            {
                if (alwaysValid ? true : PatternValid(pat) && NotRepeated(pat, minVer))
                {
                    patNum++;
                }
                return;
            }

            int vertex;
            for (int i = 0; i < rem.Count; i++)
            {
                vertex = rem[i];
                pat.Add(vertex);
                rem.RemoveAt(i);
                ChooseVerteces(pat, rem, ref patNum, verNum, minVer, alwaysValid);
                pat.Remove(vertex);
                rem.Insert(i, vertex);
            }
        }

        static void Main(string[] args)
        {
            List<int> remainingVertices = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };  // Sorted!
            List<int> pattern = new List<int>();
            int numberOfPatterns = 0;
            int tempNumber = 0;
            int numberOfPatternsWithKVertices = 0;

            // Can be toggled:
            const bool realistic = true;
            const int minimumVertices = 4;

            for (int k = minimumVertices; k <= remainingVertices.Count; k++)
            {
                int vertex;
                for (int i = 0; i < remainingVertices.Count; i++)
                {
                    vertex = remainingVertices[i];
                    pattern.Add(vertex);
                    remainingVertices.RemoveAt(i);
                    ChooseVerteces(pattern, remainingVertices, ref numberOfPatterns, k, minimumVertices, !realistic);
                    pattern.Remove(vertex);
                    remainingVertices.Insert(i, vertex);
                }

                numberOfPatternsWithKVertices = numberOfPatterns - tempNumber;
                tempNumber = numberOfPatterns;
                Console.WriteLine($"Number of possible patterns for {nameof(k)} = {k}: {numberOfPatternsWithKVertices}");
            }

            Console.WriteLine();
            Console.WriteLine("Final number of possible patterns: " + numberOfPatterns);
            Console.ReadKey();
        }
    }
}
