using System;

namespace TeachingClasses
{
    // Víc na notebooku (finished).

    class MyClass
    {
        public int x = 0;
        public static int num = 0;
        public static int instances = 0;

        private int field;
        public int Field
        {
            get { return field; }
            set { field = value; }
        }

        /*
        int get_Field()
        {
            return field;
        }

        void set_Field(int value)
        {
            field = value;
        }
        */

        public MyClass()
        {
            instances++;
        }

        ~MyClass()
        {
            instances--;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            {
                MyClass.num++;
                Console.WriteLine("Instances " + MyClass.instances);
                MyClass cl = new MyClass();
                Console.WriteLine("Instances " + MyClass.instances);
                cl.x += 5;
                Console.WriteLine(cl.x);
                MyClass.num += 5;
                System.GC.Collect();
            }
            
            Console.WriteLine("Instances " + MyClass.instances);
            Console.WriteLine(MyClass.num);

            Console.ReadKey();
        }
    }
}