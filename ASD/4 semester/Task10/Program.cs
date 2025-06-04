namespace Task10;

internal class Program
{
    private static void Main()
    {
        int CRITICAL_FLOOR = 60;
        int FLOORS = 100;

        //k(k+1)/2=FLOORS
        int step = (int)Math.Ceiling((-1 + Math.Sqrt(1 + 8 * FLOORS)) / 2);

        int result = 0;
        for (int i = 0; i <= step; i++)
        {
            if (result >= CRITICAL_FLOOR)
            {
                result = i + (CRITICAL_FLOOR - result + step + 1);
                break;
            }

            result += step;
            step--;
        }

        Console.WriteLine($"Искомый этаж будет найден за {result} бросков");
    }
}