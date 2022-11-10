using System;
using System.Text;
using System.Collections.Generic;
// using c = System.Console;    // alias

namespace TeachingClassesCode1
{
    class Employee
    {
        public string? Name { get; private set; }
        public int Age { get; private set; }
        public readonly int birthYear;
        public enum Positions { CEO, Accountant, Laborer, TruckDriver, MaintenanceWorker, Cleaner }
        public Positions Position { get; private set; }
        public static int employees;

        // Metody na povýšení (private void Promote()), zestárnutí (private ChangeAge(int by)), ...
        // Metody na vyhazování (employees) - pozor, nelze předpovídat GC.

        // Explicitní bezparametrický konstruktor:
        public Employee()
        {
            // Nemusí se tu nic stát.
            employees++;
        }

        // Statický konstruktor (volá se pouze jednou za celý program, a to před voláním prvního konstruktoru či metody třídy):
        // Vždy bezparametrický a vždy bez modifikátoru přístupu (public):
        static Employee()
        {
            employees = 0;
        }

        // Parametrický konstruktor:
        // Tzv. overloading = přetěžování (liší se v počtu parametrů / typech parametrů)
        public Employee(string name)
        {
            Name = name;
            employees++;
            // Lze:
            // this.Name = name; // this ... odkaz na 
        }

        public Employee(string name, int birthYear, Positions position)
        {
            Name = name;
            this.birthYear = birthYear;
            Position = position;
            employees++;

            Age = DateTime.Now.Year - birthYear;    // Vadné – správně podle data narození a dodatečného if!
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {            
            Employee employee1 = new Employee(); // Používá se bezparametrický konstruktor
            Employee employee2 = new Employee("Pavel");
            Employee employee3 = new Employee("Petr", 1998, Employee.Positions.CEO);
            Employee employee4 = new Employee("Dominik", 2004, Employee.Positions.MaintenanceWorker);

            List<Employee> employees = new List<Employee>();
            employees.Add(employee1);
            employees.Add(employee2);
            employees.Add(employee3);
            employees.Add(employee4);

            int n = 0;
            foreach (Employee e in employees)
            {
                Console.WriteLine("Employee number: {0}", ++n);
                Console.WriteLine("My name is {0}.", e.Name);
                Console.WriteLine("I was born in {0}.", e.birthYear);
                Console.WriteLine("I am {0} years old.", e.Age);
                Console.WriteLine("My position is {0}.", e.Position);
                Console.WriteLine();
            }
            if (Employee.employees != 1)
                Console.WriteLine("There are {0} employees in the company.", Employee.employees);
            else
                Console.WriteLine("There is 1 employee in the company.");


            Console.ReadKey();
        }
    }

}