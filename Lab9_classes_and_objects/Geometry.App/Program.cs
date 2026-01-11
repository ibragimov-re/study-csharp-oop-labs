using Geometry.Domain;

namespace Geometry.App
{
    class Program
    {
        static void Main(string[] args)
        {
            UI.PrintLine("================= LAB9 PART1 =================");
            RunPart1();

            UI.PrintLine("\n\n================= LAB9 PART2 =================");
            RunPart2();

            UI.PrintLine("\n\n================= LAB9 PART3 =================");
            RunPart3();
        }

        static void RunPart1()
        {
            Triangle triangle = UI.CreateTriangleFromInput();

            double triangleAreaByClassMethod = triangle.GetArea();
            UI.Print("\nTriangle area by class method: ");
            UI.PrintTriangleArea(triangleAreaByClassMethod);

            double triangleAreaByStaticFunc = Triangle.GetArea(triangle.A, triangle.B, triangle.C);
            UI.Print("Triangle area by static function: ");
            UI.PrintTriangleArea(triangleAreaByStaticFunc);

            UI.PrintLine("");
            UI.PrintObjectCount(Triangle.ObjectCount);

            // Создание нескольких тестовых треугольников для проверки счетчика объектов
            UI.PrintLine("Creating sample triangles...");
            UI.CreateTriangle(3, 4, 5);
            UI.CreateTriangle(6, 7, 8);

            UI.PrintObjectCount(Triangle.ObjectCount);
        }

        static void RunPart2()
        {
            UI.PrintLine("Creating sample triangles t1 and t2...\n");
            var triangle1 = UI.CreateTriangle(3, 4, 5);
            var triangle2 = UI.CreateTriangle(6, 7, 8);

            // ---------- Унарные операторы ----------
            triangle1++;
            triangle2--;

            UI.PrintLine("\nAfter applying unary operators ++ and --");
            UI.PrintTriangleSides(triangle1);
            UI.PrintTriangleSides(triangle2);

            // ---------- Бинарные операторы ----------
            UI.PrintLine("\nComparing triangles using binary operators <= and >=");
            UI.PrintLine($"t1 <= t2: {triangle1 <= triangle2}");
            UI.PrintLine($"t1 >= t2: {triangle1 >= triangle2}");

            // ---------- Приведение типов ----------
            UI.PrintLine("\nUsing explicit and implicit type conversions:");
            double areaT1 = (double)triangle1; // Явное приведение
            double areaT2 = (double)triangle2; // Явное приведение

            UI.PrintLine($"Area of t1: {areaT1}");
            UI.PrintLine($"Area of t2: {areaT2}");

            
            UI.PrintLine("\nCreating non-existent triangle t3...");
            var triangle3 = UI.CreateTriangle(30, 4, 5);

            if (triangle1) // Неявное приведение
                UI.PrintLine("t1 exists");
            else
                UI.PrintLine("t1 does not exist");

            if (triangle3) // Неявное приведение
                UI.PrintLine("t3 exists");
            else
                UI.PrintLine("t3 does not exist");
        }

        static void RunPart3()
        {
            // ---------- Создание пустого массива и заполнение вручную ----------
            TriangleArray manualArray = new TriangleArray(2);

            manualArray[0] = new Triangle(3, 4, 5);
            manualArray[1] = new Triangle(5, 6, 7);

            UI.PrintLine("Array defined manually in code:");
            manualArray.Print();

            // ---------- Создание массива с случайными треугольниками ----------
            TriangleArray randomArray = new TriangleArray(5);

            UI.PrintLine("\nRandom triangle array:");
            randomArray.Print();

            // ---------- Создание массива из ввода пользователя ----------
            UI.PrintLine("\nCreating triangle array from user input...");

            TriangleArray userArray = new TriangleArray(2, UI.CreateTriangleFromInput);

            UI.PrintLine("User-defined triangle array:");
            userArray.Print();

            // Вывести общее количество созданных треугольников
            UI.PrintObjectCount(Triangle.ObjectCount);

            // ---------- Поиск треугольника с минимальной площадью ----------
            UI.PrintLine("\nSearching index with minimum area in random array...");
            var indexMinTriangle = FindIndexOfMinArea(randomArray);
            if (indexMinTriangle == -1)
                UI.PrintLine("Array is empty");
            else
            {
                UI.PrintLine($"Index of triangle with minimum area: {indexMinTriangle}");
                var minTriangle = randomArray[indexMinTriangle];
                UI.Print("Triangle sides: ");
                UI.PrintTriangleSides(minTriangle);
                UI.Print("Minimum area: ");
                UI.PrintTriangleArea((double)minTriangle);
            }
        }

        static int FindIndexOfMinArea(TriangleArray array)
        {
            int minIndex = -1;
            double minArea = double.MaxValue;

            for (int i = 0; i < array.Length; i++)
            {
                var triangle = array[i];
                if (triangle == null)
                    continue;

                double area = (double)triangle;
                if (area >= 0 && area < minArea)
                {
                    minArea = area;
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}