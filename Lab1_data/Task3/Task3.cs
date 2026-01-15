using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("================= LAB1 TASK3 =================");

        const double a = 1000.0;
        const double b = 0.0001;

        double numerator = Math.Pow((a - b), 3) - Math.Pow(a, 3);

        double denominator = 3 * a * Math.Pow(b, 2) - Math.Pow(b, 3) - 3 * Math.Pow(a, 2) * b;

        double resultDouble = numerator / denominator;

        // Явное приведение к типу float для демонтрации в разнице типов
        float resultFloat = (float)resultDouble;

        Console.WriteLine($"Result (double) = {resultDouble}");
        Console.WriteLine($"Result (float)  = {resultFloat}");
    }
}
