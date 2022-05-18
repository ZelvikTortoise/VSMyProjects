using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Riskuj
{
    static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>

        internal static string[,] questions1;
        internal static string[,] questions2;

        internal static FormStart formStart;
        internal static FormGame formGame;
        internal static FormEnd formEnd;

        internal static int maxTime;
        internal static string adjective;
        internal static string teamName1;
        internal static string teamName2;
        internal static string teamName3;
        internal static string teamName4;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            formStart = new FormStart();
            formGame = new FormGame();
            formEnd = new FormEnd();
            Application.Run(formStart);
        }
    }
}
