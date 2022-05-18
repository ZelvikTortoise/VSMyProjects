using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGCalculator
{   
    static class Program
    {
        // Current language of this program.
        public static Language language = Language.english;

        // Types of 2D objects.
        public static List<string> typeObject2D = language.GetTypesOf2DObjects();

        // Types of 3D objects.
        public static List<string> typeObject3D = language.GetTypesOf3DObjects();

        // Calculator's memory of 2D and 3D objects.
        public static List<string> object2DNames = new List<string>();
        public static Dictionary<string, IObject2D> objects2D = new Dictionary<string, IObject2D>();
        public static List<string> object3DNames = new List<string>();
        public static Dictionary<string, IObject3D> objects3D = new Dictionary<string, IObject3D>();

        // Currently selected object.
        public static IObjectAG selectedObject = null;
        // Interface type enum.
        public enum ObjectInterfaceType { I2D, I3D }

        // Declaring all our forms as STATIC.
        public static FormMainMenu formMainMenu = null;
        public static FormAdd2D formAdd2D = null;
        public static FormDisplay2D formDisplay2D = null;
        // public static FormAdd3D formAdd3D = null;
        // public static FormDisplay3D formDisplay3D = null;
        public static FormRemove formRemove = null;
        public static FormParametricCheck formCheckParametricEquation = null;
        public static FormLanguage formLanguage = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //
            // Initializing all our forms.
            //
            // Main menu:
            formMainMenu = new FormMainMenu();
            // Forms for 2D objects:
            formAdd2D = new FormAdd2D();
            formDisplay2D = new FormDisplay2D();
            // Forms for 3D objects:
            // formAdd3D = new FormAdd3D();
            // formDisplay3D = new FormDisplay3D();
            // General forms:
            formRemove = new FormRemove();
            formCheckParametricEquation = new FormParametricCheck();
            formLanguage = new FormLanguage();
            // End.

            // Running the application.
            Application.Run(formMainMenu);  // Starting at Main menu.
        }
    }
}
