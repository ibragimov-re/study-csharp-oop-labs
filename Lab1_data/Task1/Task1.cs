using System;
using InputHelperLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("================= LAB1 TASK1 =================");

        int m = InputHelper.ReadInt("Enter m: ");
        int n = InputHelper.ReadInt("Enter n: ");
        double x = InputHelper.ReadDouble("Enter x: ");

        Console.WriteLine($"1) m={m} n={n}");

        PrintExpr1(m, n);
        PrintExpr2(m, n);
        PrintExpr3(m, n);
        PrintExpr4(x);
    }

    // 1) n++ * m
    static void PrintExpr1(int m, int n)
    {
        int result = n++ * m;
        Console.WriteLine($"1) n++ * m = {result}");
    }

    // 2) n++ < m
    static void PrintExpr2(int m, int n)
    {
        bool result = n++ < m;
        Console.WriteLine($"2) n++ < m = {result}");
    }

    // 3) --m > n
    static void PrintExpr3(int m, int n)
    {
        bool result = --m > n;
        Console.WriteLine($"3) --m > n = {result}");
    }

    // 4) cbrt(x - x^2 + x^5)
    static void PrintExpr4(double x)
    {
        // Кубический корень можно записать как степень 1/3
        double result = Math.Pow(x - Math.Pow(x, 2) + Math.Pow(x, 5), 1.0 / 3.0);
        Console.WriteLine($"4) cbrt(x-x^2+x^5) = {result}");
    }
}
