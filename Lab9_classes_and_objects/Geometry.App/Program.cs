using Geometry.Domain;

namespace Geometry.App
{
    class Program
    {
        static void Main(string[] args)
        {
            UI.Print("================= LAB9 PART1 =================\n");

            Triangle triangle = UI.CreateTriangleFromInput();

            double triangleAreaByClassMethod = triangle.GetArea();
            UI.Print("Triangle area by class method: ");
            UI.PrintTriangleArea(triangleAreaByClassMethod);

            double triangleAreaByStaticFunc = Triangle.GetArea(triangle.A, triangle.B, triangle.C);
            UI.Print("Triangle area by static function: ");
            UI.PrintTriangleArea(triangleAreaByStaticFunc);

            UI.PrintObjectCount(Triangle.ObjectCount);

            // Создание нескольких тестовых треугольников для проверки счетчика объектов
            UI.CreateSampleTriangle();
            UI.CreateSampleTriangle();

            UI.PrintObjectCount(Triangle.ObjectCount);
        }
    }
}
