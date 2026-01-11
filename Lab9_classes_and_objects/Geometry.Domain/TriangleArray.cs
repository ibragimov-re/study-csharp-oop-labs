namespace Geometry.Domain
{
    public class TriangleArray
    {
        // Одномерный массив треугольников
        private Triangle[] arr;

        public int Length => arr.Length;

        public TriangleArray()
        {
            arr = Array.Empty<Triangle>();
        }

        // Конструктор с размером массива (случайные значения)
        public TriangleArray(int size)
        {
            if (size == 0) {
                arr = Array.Empty<Triangle>();
                return;
            }

            if (size < 0)
                throw new ArgumentException("Array size cannot be negative");

            arr = new Triangle[size];
            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                // Генерируем стороны от 1 до 10
                arr[i] = Triangle.CreateRandomTriangle();
            }
        }

        // Конструктор с размером массива и фабричной функцией для создания треугольников (для пользовательского ввода)
        // Сама по себе идея реализовать конструктор с пользовательским вводом в доменном слое не очень хороша, и лучше это сделать в UI классе,
        // но по заданию требуется создание треугольника именно через конструктор, поэтому сюда передается фабричная функция, которая уже реализована в UI
        public TriangleArray(int size, Func<Triangle> triangleFactory)
        {
            if (size == 0)
            {
                arr = Array.Empty<Triangle>();
                return;
            }

            if (size < 0)
                throw new ArgumentException("Array size cannot be negative");

            if (triangleFactory == null)
                throw new ArgumentNullException(nameof(triangleFactory));

            arr = new Triangle[size];

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Create triangle #{i + 1}:");
                arr[i] = triangleFactory();
            }
        }

        // Индексатор для доступа к элементам массива
        public Triangle this[int index]
        {
            get
            {
                if (index < 0 || index >= arr.Length)
                    throw new ArgumentException("Index is out of range");

                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length)
                    throw new ArgumentException("Index is out of range");

                arr[index] = value;
            }
        }

        // Метод для просмотра элементов массива
        public void Print()
        {
            if (arr.Length == 0)
                Console.WriteLine("Array is empty");

            for (int i = 0; i < arr.Length; i++)
            {
                Triangle t = arr[i];
                Console.WriteLine($"[{i}] a={t.A}, b={t.B}, c={t.C}, area={(double)t}");
            }
        }
    }
}
