using Geometry.Domain;

namespace Geometry.App
{
    public static class UI
    {
        public static void Print(string message)
        {
            Console.Write(message);
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

                    var triangle = new Triangle(a, b, c);
                    Console.Write($"Created triangle: ");
                    PrintTriangleSides(triangle);

                    return triangle;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Try again\n");
                }
            }
        }

        public static Triangle CreateSampleTriangle()
        {
            var triangle = new Triangle(3, 4, 5);
            Console.Write($"Created sample triangle: ");
            PrintTriangleSides(triangle);
            return triangle;
        }

        public static void PrintTriangleSides(Triangle triangle)
        {
            Console.WriteLine($"triangle sides: a={triangle.A}, b={triangle.B}, c={triangle.C}");
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
