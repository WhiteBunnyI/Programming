using Lab_1;

Point2d.WIDTH = 1024;
Point2d.HEIGHT = 720;

Point2d point = new(0, 0);

Console.WriteLine(point);
Console.WriteLine();

try
{
    point.X = -1;
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

Console.WriteLine();

point.X = 400;
point.Y = 200;

Point2d otherPoint = new(400, 200);
Console.WriteLine(point);
Console.WriteLine(otherPoint);
Console.WriteLine("Сравниваем:");
Console.WriteLine($"{point} == {otherPoint} ? {(point == otherPoint)}");

otherPoint.Y = 180;
Console.WriteLine($"{point} == {otherPoint} ? {(point == otherPoint)}");

Console.WriteLine();
Console.WriteLine("================================================");
Console.WriteLine();

//==================================================================

Vector2d vector = new(3, 2);
Console.WriteLine(vector);
Console.WriteLine();

Console.WriteLine($"Получение по индексу [1]: {vector[1]}");
vector[1] = 4;
Console.WriteLine($"Назначаем по индексу [1] значение 4: {vector[1]}");
Console.WriteLine();

Console.Write("Итерируем объект: {");
foreach (var comp in vector)
{
    Console.Write(comp + ", ");
}
Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
Console.WriteLine("}\n");

Console.WriteLine("Модуль: " + vector.Abs);
Console.WriteLine();

Vector2d otherVector = new(1, 4);
Console.WriteLine($"1: {vector}; 2: {otherVector}");
Console.WriteLine($"Сложение: {vector + otherVector}");
Console.WriteLine($"Вычитание: {vector - otherVector}");
Console.WriteLine($"Умножение на 2: {vector * 2}");
Console.WriteLine($"Деление на 2: {vector / 2}");
Console.WriteLine();

Console.WriteLine($"1: {vector}; 2: {otherVector}");
Console.WriteLine($"Скалярное произведение: {vector.DotProduct(otherVector)}");
Console.WriteLine($"Скалярное произведение (static): {Vector2d.DotProduct(vector, otherVector)}");
Console.WriteLine($"Векторное произведение: {vector.CrossProduct(otherVector)}");
Console.WriteLine($"Векторное произведение (static): {Vector2d.CrossProduct(vector, otherVector)}");
Console.WriteLine($"Смешанное произведение: {vector.ScalarTripleProduct(otherVector)}");
Console.WriteLine();

Console.WriteLine($"Задаем вектор по 2 точкам: {point} и {otherPoint}");
otherVector = new Vector2d(point, otherPoint);
Console.WriteLine(otherVector);
Console.WriteLine();
