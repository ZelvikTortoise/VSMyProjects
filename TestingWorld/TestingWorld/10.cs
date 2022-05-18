using Co = System.Console;

abstract class A
{
    public virtual void m() => Co.Write("A");
}
class B : A
{
    public virtual void m(int i = 42)
    => Co.Write($"B-{i}");
}
class C : B
{
    public override void m() => Co.Write("C1");
    public virtual void m(int j = 333)
    => Co.Write($"C2-{j}");
}
class D : C
{
    public void m() => Co.Write("D");
}
class Prg7
{
    static void Main()
    {
        var x = new D();
        x.m();
        (x as B).m();
    }
}