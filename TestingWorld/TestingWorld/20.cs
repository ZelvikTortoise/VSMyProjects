using C = System.Console;
class W4
{
    public virtual void m(double t) { C.Write(1); }
}
class X4 : W4
{
    public virtual void m<T>(T t) { C.Write(2); }
}
class Y4 : X4
{
    public override void m<T>(T t) { C.Write(3); }
    public void m(float t) { C.Write(4); }
}
class Z4 : Y4
{
    public sealed override void m<T>(T t)
    {
        C.Write(5);
    }
    public override void m(double t)
    {
        C.Write(6);
    }
}
class Prg4
{
    public static void Main()
    {
        X4 x = new Z4();
        x.m(3.14);

        C.ReadKey();
    }
}