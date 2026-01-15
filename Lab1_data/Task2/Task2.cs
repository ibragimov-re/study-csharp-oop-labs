using InputHelperLib;

class Program
{
    // Параметры круга
    const double CENTER_X = 0.0;
    const double CENTER_Y = 0.0;
    const double RADIUS = 1.0;

    static void Main()
    {
        Console.WriteLine("================= LAB1 TASK2 =================");

        double x1 = InputHelper.ReadDouble("Enter X1: ");
        double y1 = InputHelper.ReadDouble("Enter Y1: ");

        // Расчет дистанции между точками с помощью Теоремы Пифагора
        double distance = Math.Sqrt(Math.Pow(x1 - CENTER_X, 2) + Math.Pow(y1 - CENTER_Y, 2));

        bool isPointInsideCircle = distance <= RADIUS;

        Console.WriteLine($"Is the point inside the circle: {isPointInsideCircle}");
    }
}
