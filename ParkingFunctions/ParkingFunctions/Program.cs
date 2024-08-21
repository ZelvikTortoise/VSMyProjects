namespace ParkingFunctions
{
    class ParkingPreference
    {
        public readonly uint[] preferences;
        public readonly uint length;

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
    }
    internal class Program
    {
        static void Main()
        {
            
        }
    }
}
