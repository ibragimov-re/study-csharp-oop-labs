using Geometry.Domain;
using System.Runtime.InteropServices;

namespace Geometry.App
{
    public static class UI
    {
        public static void Print(string message)
        {
            Console.Write(message);
        }

        public static void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

        public static Triangle CreateTriangleFromInput()
        {
            while (true)
            {
                try
                {
                    double a = ReadPositiveDouble("Enter side a: ");
                    double b = ReadPositiveDouble("Enter side b: ");
                    double c = ReadPositiveDouble("Enter side c: ");

                    return CreateTriangle(a, b, c);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Try again\n");
                }
            }
        }

        public static Triangle CreateTriangle(double a, double b, double c)
        {
            var triangle = new Triangle(a, b, c);
            Console.Write($"Created triangle with sides: ");
            PrintTriangleSides(triangle);

            return triangle;
        }

        public static void PrintTriangleSides(Triangle triangle)
        {
            Console.WriteLine($"a={triangle.A}, b={triangle.B}, c={triangle.C}");
        }

        public static void PrintTriangleArea(double area)
        {
            Console.WriteLine(area);
        }

        public static void PrintObjectCount(int count)
        {
            Console.WriteLine($"Number of created objects: {count}");
        }

        // Приватный вспомогательный метод для чтения положительного числа с консоли
        private static double ReadPositiveDouble(string message)
        {
            Console.Write(message);

            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                throw new FormatException("Input is not a valid numeric value");
            }

            if (value <= 0)
            {
                throw new ArgumentException("Value must be greater than zero");
            }

            return value;
        }
    }
}
