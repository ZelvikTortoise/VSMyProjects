using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ParkingFunctions
{
    class ParkingPreference
    {
        public readonly uint[] preferences;
        public readonly uint length;
        private static uint n = 0;  // for generating (temp)
        private static TextWriter writer = Console.Out;  // for generating (temp)
        private static bool longList = false;   // false => print only parkingPreferences, true => print also the parked vectors
        private static bool printLucky = false; // true => print L (lucky) or U (unlucky)
        private static uint[] vector = new uint[n]; // for generating (temp)
        private static string separator = " ";  // "parkingPreference separator parkedVector"

        public ParkingPreference(uint len)
        {
            this.length = len;
            this.preferences = new uint[len];
        }

        public ParkingPreference(uint[] prefs)
        {
            this.length = (uint)prefs.Length;
            this.preferences = new uint[length];
            for (int i = 0; i < length; i++)
            {
                if (prefs[i] <= 0)
                    this.preferences[i] = 1;
                else if (prefs[i] > length)
                    this.preferences[i] = length;
                else
                    this.preferences[i] = prefs[i];
            }
        }

        private static void ResetPrivateVariables()
        {
            ParkingPreference.n = 0;
            ParkingPreference.vector = new uint[n];
            ParkingPreference.writer = Console.Out;
            ParkingPreference.longList = false;
            ParkingPreference.printLucky = false;
    }

        /// <summary>
        /// Returns a parking vector (permutation of {1; ...; n}) where i-th position corresponds to the i-th car's parking spot.
        /// If the cars don't all park successfully, the first unparked car's parking spot will be marked as 0 and the other cars will not try to park anymore.
        /// </summary>
        /// <param name="fast">O(n.log(n)) instead of O(n^2)?</param>        
        /// <param name="luckyCars">Returns the number of lucky cars in a parking function or 0 in a non-parking preference.</param>
        /// <returns>Parking vector (isn't parking function if the last digit is 0 <=> luckyCars is 0).</returns>
        public uint[] Park(bool fast, out uint luckyCars)
        {
            uint[] parkedVector = new uint[length];
            uint parkSpot;
            bool[] parkingLotParked = new bool[length]; // true = occupied, false = free
            luckyCars = 0;
            bool repeated = false;
            if (fast)
            {
                // NOT IMPLEMENTED (tree of clusters, O(n.log(n)))
                parkedVector = Park(false, out luckyCars); // DELETE AFTER IMPLEMENTATION
            }
            else
            {
                for (uint car = 1; car <= length; car++)
                {
                    parkSpot = preferences[car - 1];    // if this holds, the car is considered a lucky car
                    while (parkSpot <= length && parkingLotParked[parkSpot - 1])   // until the car finds a free spot
                    {
                        parkSpot++; // the car has to continue down the street
                        repeated = true;
                    }

                    if (!repeated)
                    {
                        luckyCars++;
                    }
                    else
                    {
                        repeated = false;
                    }

                    if (parkSpot > length)  // not a parking function
                    {
                        parkedVector[car - 1] = 0;
                        luckyCars = 0;  // we only consider lucky cars in a parking function, not in a general parking preference
                        break;
                    }
                    else   // a parking function
                    {
                        parkingLotParked[parkSpot - 1] = true;  // the spot is occupied now
                        parkedVector[car - 1] = parkSpot;
                    }                    
                }
            }

            return parkedVector;
        }

        private static void GenerateInLexicographicOrder(int position)
        {
            // The vector is full – we have a parking preference:
            if (position == ParkingPreference.n)
            {
                ParkingPreference pf = new ParkingPreference(vector);

                // Creation of the string to print:
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ParkingPreference.vector.Length; i++)
                {
                    sb.Append(ParkingPreference.vector[i]); // constructing parking preferences
                }

                if (ParkingPreference.longList) // constructing parked cars
                {
                    sb.Append(ParkingPreference.separator);
                    uint luckyCars;
                    uint[] parkedVector = pf.Park(false, out luckyCars);   // parking the cars
                    for (int i = 0; i < parkedVector.Length; i++)
                    {
                        sb.Append(parkedVector[i]);
                    }

                    if (ParkingPreference.printLucky)   // constructing number of lucky cars
                    {
                        sb.Append(ParkingPreference.separator);
                        sb.Append(luckyCars);
                    }
                }                

                // Printing:
                writer.WriteLine(sb.ToString());
            }
            else   // some digits are still missing => filling them in
            {
                for (int i = 1; i <= ParkingPreference.n; i++)
                {
                    ParkingPreference.vector[position] = (uint)i;
                    GenerateInLexicographicOrder(position + 1);
                }
            }
        }

        /// <summary>
        /// Generates all the parking preference vectors of length n in lexicographic order. May as well print the parked cars vectors and the number of lucky cars.
        /// </summary>
        /// <param name="n">The number of cars and the number of spots in the parking lot.</param>
        /// <param name="writer">Specifies writer like StreamWriter's path or Console.Out.</param>
        /// <param name="longList">Prints also the parked cars vectors.</param>
        /// <param name="luckyCars">Its value is considered only if longList variable is true. Prints the number of lucky cars in a parking function, 0 otherwise.</param>
        public static void GeneratePFList(uint n, TextWriter writer, bool longList, bool luckyCars)
        {
            ParkingPreference.n = n;
            ParkingPreference.vector = new uint[n];
            ParkingPreference.writer = writer;
            ParkingPreference.longList = longList;
            ParkingPreference.printLucky = luckyCars;
            GenerateInLexicographicOrder(0);    // everything here is using private ParkingPreference variables
            ResetPrivateVariables();
        }

        private static uint Factorial(uint n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                uint fact = 1;
                for (uint i = 1; i <= n; i++)
                {
                    fact *= i;
                }

                return fact;
            }
        }


        // Not used:
        private static uint FactorialSum(uint n)
        {
            uint sum = 0;
            for (uint i  = 0; i <= n; i++)
            {
                sum += Factorial(n);
            }

            return sum;
        }

        private static uint SumOfPartialFactorialProducts(uint n)
        {
            uint sum = 0;
            uint product = 1;
            sum += product; // (0, 0, 0) ... 1
            for (uint i = 0; i < n; i++)
            {
                // for n = 3:
                // (1/2/3, 0, 0) ... n
                // (1/2/3, 1/2/3, 0) ... n(n-1) = n(n-1)...3.2
                // (1/2/3, 1/2/3, 1/2/3) ... n(n-1)(n-2) = n(n-1)...3.2.1 = Factorial(n) = n(n-1)...3.2
                product *= (n - i);
                sum += product;
            }

            return sum;
        }

        public static void GenerateEquivalenceClasses(TextReader reader, TextWriter writer, bool ignoreNonParkingFunctions)
        {
            String input;
            string[] info;
            string parkingVector;
            uint numberOfLuckyCars;
            uint equivalenceClassIndex = 0; // from 1 to n! = Factorial(n) (=> multidigit numbers!)
            // Note: If the non-PFs are not ignored, it is from 1 to (0! + 1! + 2! + ... + n!) = FactorialSum(n)
            bool firstLine = true;
            uint n;
            uint[] equivalenceDecompositionNumbers = { };
            string[] representatives = { };
            StringBuilder sb;
            bool found;
            uint firstFreeColorIndex = 0;
#pragma warning disable CS8600 // Literál s hodnotou null nebo s možnou hodnotou null se převádí na typ, který nemůže mít hodnotu null.
            while ((input = reader.ReadLine()) != null)
            {
                info = input.Split(ParkingPreference.separator);
                // Format: parkingPreference parkingVector numberOfLuckyCars
                // Note: numberOfLuckyCars == 0 <=> the parkingPreference is not a parking function
                if (info.Length < 3)
                {
                    // Probably an empty line at the end => ignore and finish.
                    break;
                }
                else if (firstLine)
                {
                    n = (uint)info[0].Length;
                    representatives = ignoreNonParkingFunctions ? new string[Factorial(n)] : new string[SumOfPartialFactorialProducts(n)];
                    firstLine = false;
                }
                parkingVector = info[1];
                numberOfLuckyCars = uint.Parse(info[2]);
                sb = new StringBuilder();
                sb.Append(input);
                sb.Append(ParkingPreference.separator);
                if (ignoreNonParkingFunctions && numberOfLuckyCars == 0)    // this happens n^n - (n+1)^(n-1) times
                {
                    equivalenceClassIndex = 0;  // ingoring non-parking functions
                }
                else   // this happens (n+1)^(n-1) times
                {
                    found = false;
                    for (uint i = 0; i < representatives.Length; i++)
                    {
                        if (parkingVector == representatives[i])
                        {
                            found = true;
                            equivalenceClassIndex = i + 1;
                            break;
                        }
                    }
                    if (!found)
                    {
                        representatives[firstFreeColorIndex] = parkingVector;
                        equivalenceClassIndex = firstFreeColorIndex + 1;    // equavalence class indeces are from 1 to n!
                        firstFreeColorIndex++;
                    }
                }
                sb.Append(equivalenceClassIndex);

                writer.WriteLine(sb.ToString());    // Printing.
            }
#pragma warning restore CS8600 // Literál s hodnotou null nebo s možnou hodnotou null se převádí na typ, který nemůže mít hodnotu null.
        }
    }
    internal class Program
    {
        static void Main()
        {
            string pathIn, pathOut1, pathOut2;
            /*
            File sizes:
            PFList (n = ?).txt
            n = 1:         1 kB
            n = 2:         1 kB
            n = 3:         1 kB
            n = 4:         4 kB
            n = 5:        46 kB
            n = 6:       775 kB
            n = 7:    15,281 MB
            n = 8:   344,064 MB
            n = 9: 8,701 828 GB     // canot be opened in Notepad anymore due to its size => ignoring n = 9

            PFList (n = ?) – equivalence decomposition.txt
            n = 1:         1 kB
            n = 2:         1 kB
            n = 3:         1 kB
            n = 4:         4 kB
            n = 5:        53 kB
            n = 6:       893 kB
            n = 7:    17,531 MB
            n = 8:   393,032 MB
            n = 9: not tested
            */
            for (uint n = 1; n <= 1; n++)
            {
                pathOut1 = String.Format("PFList (n = {0}).txt", n);
                using (StreamWriter sw = new StreamWriter(pathOut1))
                {
                    ParkingPreference.GeneratePFList(n, sw, true, true);
                }
                pathIn = pathOut1;
                pathOut2 = String.Format("PFList (n = {0}) – equivalence decomposition.txt", n);
                using (StreamReader sr = new StreamReader(pathIn))
                using (StreamWriter sw = new StreamWriter(pathOut2))
                {
                    ParkingPreference.GenerateEquivalenceClasses(sr, sw, true);
                }
            }

            Console.WriteLine();
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
