using System;

public class X
{
    public void m(int i)
    {
        Console.WriteLine("X.f(int)");
    }
}
public class Y : X
{
    public void m(float f)
    {
        Console.WriteLine("Y.f(float)");
    }
    public void Test()
    {
        m(1);
    }
}
class Prg6
{
    static void Main(string[] args)
    {
        Y y = new Y();
        y.Test();
    }
}