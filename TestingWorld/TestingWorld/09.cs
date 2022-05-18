using System;
class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
struct Color
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }
    public Color(byte red, byte green, byte blue)
    {
        Red = red; Green = green; Blue = blue;
    }
}
class Prg4
{
    public static void m()
    {
        var pink = new Color(255, 192, 203);
        var jan = new Student
        {
            FirstName = "Jan",
            LastName = "Radsetoulal"
        };
        Console.WriteLine($"{jan} {pink}");
    }
    static void Main(string[] args)
    {
        Console.WriteLine("I'm here!");
        m();
        Console.WriteLine("I'm dying, so sad!");
    }
}
