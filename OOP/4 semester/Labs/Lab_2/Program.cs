using Lab_2;
using Color = Lab_2.ConsolePrint.Color;
using Size = Lab_2.ConsolePrint.Size;

string chr = "привет";

using (new ConsolePrint(Color.Blue, Size.Five))
{
    ConsolePrint.Print(chr, 8, 2);
    using(new ConsolePrint(Color.Red, Size.Seven, '█'))
    {
        using (new ConsolePrint(Color.Green, Size.One))
            ConsolePrint.Print(chr);
        ConsolePrint.Print(chr);
    }
}
