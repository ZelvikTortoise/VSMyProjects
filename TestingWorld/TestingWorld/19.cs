using System;

class Prg1
{
    static void Log(
    object s, UnhandledExceptionEventArgs a)
    {
        Console.Write("-un-");
    }
    public static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException
        += Log;
        try
        {
            Console.Write("-pre-");
            throw new Exception();
            Console.Write("-post-");
        }
        finally
        {
            Console.Write("-fin-");
        }

        Console.ReadKey();
    }
}