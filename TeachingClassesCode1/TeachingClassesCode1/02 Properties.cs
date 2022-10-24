using System;
using System.Text;
using System.Collections.Generic;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    public class Employee
    {
        #region 1
        /*/
        public string? name;
        /*/
        #endregion
        #region 2
        /*/
        private string? name;      // changes
        public string? Name;       // stays null
        public void set_Name(string? value)
        {
            name = value;
        }
        public string? get_Name()
        {
            return name;
        }
        /*/
        #endregion
        #region 3
        /*/
        private string? name;
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        /*/
        #endregion
        #region 4
        /*/
        public string? Name { get; set; }   // wrapper
        /*/
        #endregion
        #region 5
        /*/
        public string? Name
        {
            get { return Name; }
            set
            {
                if (value.StartsWith('P'))  // Nekonečný cyklus!!!
                {
                    Name = value;
                }
            }
        }
        /*/
        #endregion
        #region 6
        /*/
        private string? name;
        public string? Name
        {
            get { return name; }
            set
            {
                if (value.StartsWith('P'))
                {
                    name = value;
                }
            }
        }
        /*/
        #endregion
        #region 7
        /**/
        public string? Name { get; private set; }   // Mimo třídu nikdo nemůže přiřazovat hodnoty!
        public int BirthYear { get; } = 2004;

        public void SetName(string name)
        {
            if (name.StartsWith('P'))
            {
                Name = name;
            }
        }
        /**/
        #endregion
    }

    internal class Properties
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee();            
            // employee.Name = "Petr";
            // employee.Name = "Emil";
            employee.SetName("Petr");
            employee.SetName("Emil");
            // employee.BirthYear = 0;
            Console.WriteLine("Zaměstnanec jménem {0} vás zdraví.", employee.Name);

            Console.ReadKey();
        }
    }
}