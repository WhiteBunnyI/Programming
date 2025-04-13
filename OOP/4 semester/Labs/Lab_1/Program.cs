namespace Lab_1
{
    internal class Program
    {
        static void Main()
        {
            Point2d.WIDTH = 1024;
            Point2d.HEIGHT = 720;

            Point2d point = new Point2d(0, 0);
            Console.WriteLine(point);

            try
            {
                point.X = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            point.X = 400;
            point.Y = 200;
            Console.WriteLine(point);

            Point2d otherPoint = new Point2d(400, 200);
            Console.WriteLine(otherPoint);
            Console.WriteLine("Сравниваем 2 точки: " + (point == otherPoint));
            otherPoint.Y = 180;
            Console.WriteLine("Сравниваем 2 точки: " + (point == otherPoint));

            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine();

            //==========================================

            Vector2d vector = new Vector2d(3, 2);
            Console.WriteLine(vector);
            Console.WriteLine();

            Console.WriteLine("Получение по индексу [1]: " + vector[1]);
            vector[1] = 4;
            Console.WriteLine("Назначаем по индексу [1] значение 4: " + vector[1]);
            Console.WriteLine();

            Console.Write("Итерируем объект: {");
            foreach(var comp in vector)
            {
                Console.Write(comp + ", ");
            }
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
            Console.WriteLine("}");
            Console.WriteLine();

            Console.WriteLine("Модуль: " + vector.Abs);
            Console.WriteLine();

            Vector2d otherVector = new Vector2d(1, 4);
            Console.WriteLine("1: " + vector + "\n2: " + otherVector);
            Console.WriteLine("Сложение: " + (vector + otherVector));
            Console.WriteLine("Вычитание: " + (vector - otherVector));
            Console.WriteLine("Умножение (1) на 2: " + (vector * 2));
            Console.WriteLine("Деление (1) на 2: " + (vector / 2));
            Console.WriteLine();

            Console.WriteLine("1: " + vector + "\n2: " + otherVector);
            Console.WriteLine("Скалярное произведение: " + (vector.DotProduct(otherVector)));
            Console.WriteLine("Скалярное произведение (static): " + (Vector2d.DotProduct(vector, otherVector)));
            Console.WriteLine("Векторное произведение: " + (vector.CrossProduct(otherVector)));
            Console.WriteLine("Векторное произведение (static): " + (Vector2d.CrossProduct(vector, otherVector)));
            Console.WriteLine("Смешанное произведение: " + vector.ScalarTripleProduct(otherVector));
            Console.WriteLine();

            Console.WriteLine("Задаем вектор по 2 точкам: " + point + " и " + otherPoint);
            otherVector = new Vector2d(point, otherPoint);
            Console.WriteLine(otherVector);
            Console.WriteLine();
        }
    }
}
