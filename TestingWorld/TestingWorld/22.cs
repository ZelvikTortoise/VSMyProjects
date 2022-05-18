using C = System.Console;

interface IW6
{
    void m1(); void m2(); void m3();
}
abstract class X6
{
    public void m1() { C.WriteLine(11); }
    public abstract void m2();
    public virtual void m3() { C.WriteLine(13); }
}
class Y6 : X6, IW6
{
    public virtual void m1() { C.WriteLine(21); }
    public override void m2() { C.WriteLine(22); }
}
class Z6 : Y6
{
    public override void m1() { C.WriteLine(31); }
    public virtual void m2() { C.WriteLine(32); }
    public override void m3() { C.WriteLine(33); }
}
class A6 : Z6
{
    public void m3() { C.WriteLine(43); }
}
class Prg6
{
    public static void Main()
    {
        IW6 iw = new A6();
        iw.m1();
        iw.m2();
        iw.m3();

        C.ReadKey();
    }
}