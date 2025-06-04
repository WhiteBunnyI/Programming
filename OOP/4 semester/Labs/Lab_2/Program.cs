using Lab_2;
using Color = Lab_2.ConsolePrint.Color;
using Size = Lab_2.ConsolePrint.Size;

string chr = "привет";

using (var printer1 = new ConsolePrint(Color.Blue, Size.Five))
{
    printer1.Print(chr, 8, 2);
    using(var printer2 = new ConsolePrint(Color.Red, Size.Seven, '█'))
    {
        using (var printer3 = new ConsolePrint(Color.Green, Size.One))
            printer3.Print(chr);
        printer2.Print(chr);
    }
}
