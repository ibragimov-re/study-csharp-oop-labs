namespace Geometry.Domain
{
    public class Triangle
    {
        // Статический счетчик созданных объектов, общий для всех экземпляров класса
        private static int objectCount = 0;

        // Закрытые атрибуты (стороны треугольника) реализуются за счет автосвойств без сеттеров
        // Автосвойства создают закрытые поля, доступ к которым возможен только для чтения через геттеры
        // Таким образом, после создания объекта изменить стороны нельзя
        public double A { get; }
        public double B { get; }
        public double C { get; }

        // Статическое свойство для получения количества созданных объектов
        public static int ObjectCount => objectCount;

        // Инициализация атрибутов доступна только через конструктор с параметрами
        public Triangle(double a, double b, double c)
        {
            ValidateAllSides(a, b, c);

            A = a;
            B = b;
            C = c;

            objectCount++;
        }

        // Так как поля A, B, C неизменяемы, то конструктор без параметров не задается

        // Метод класса для вычисления площади треугольника
        public double GetArea()
        {
            if (!IsTriangleValid(A, B, C))
                return -1; // отрицательный результат для несуществующего треугольника

            // Вычисление площади треугольника по трем сторонам через формулу Герона
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        // Статическая функция. Вызывается без объекта класса, принимает аргументы
        public static double GetArea(double a, double b, double c)
        {
            if (!IsTriangleValid(a, b, c))
                return -1; // отрицательный результат для несуществующего треугольника

            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }


        // ------------- Унарные операторы --------------
        public static Triangle operator ++(Triangle t)
        {
            return new Triangle(t.A + 1, t.B + 1, t.C + 1);
        }

        public static Triangle operator --(Triangle t)
        {
            return new Triangle(t.A - 1, t.B - 1, t.C - 1);
        }

        // ------------- Бинарные операторы --------------
        public static bool operator <=(Triangle t1, Triangle t2)
        {
            return t1.GetArea() <= t2.GetArea();
        }

        public static bool operator >=(Triangle t1, Triangle t2)
        {
            return t1.GetArea() >= t2.GetArea();
        }

        // ------------- Операции приведения типов --------------
        // Явное приведение (explicit)
        public static explicit operator double(Triangle t)
        {
            if (!IsTriangleValid(t.A, t.B, t.C))
                return -1;
            return t.GetArea();
        }

        // Неявное приведение (implicit)
        public static implicit operator bool(Triangle t)
        {
            return IsTriangleValid(t.A, t.B, t.C);
        }

        // ------------- Валидация --------------
        private void ValidateAllSides(double a, double b, double c)
        {
            ValidateSide(a);
            ValidateSide(b);
            ValidateSide(c);
        }

        private static void ValidateSide(double side)
        {
            if (side <= 0)
                throw new ArgumentException("Side length must be positive and non-zero");
        }

        // Проверка существования треугольника с заданными сторонами
        // В любом треугольнике сумма любых двух сторон должна быть больше третьей
        private static bool IsTriangleValid(double a, double b, double c)
        {
            // Проверяет, существует ли треугольник
            return a + b > c && a + c > b && b + c > a;
        }
    }
}
