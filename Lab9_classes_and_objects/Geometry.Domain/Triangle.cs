namespace Geometry.Domain
{
    public class Triangle
    {
        // Статический счетчик созданных объектов, общий для всех экземпляров класса
        private static int objectCount = 0;

        // Закрытые атрибуты (стороны треугольника) реализуются за счет свойств без сеттеров
        // Таким образом, после создания объекта изменить стороны нельзя
        public double A { get; }
        public double B { get; }
        public double C { get; }

        // Инициализация атрибутов доступна только через конструктор с параметрами
        public Triangle(double a, double b, double c)
        {
            ValidateSide(a);
            ValidateSide(b);
            ValidateSide(c);

            A = a;
            B = b;
            C = c;

            objectCount++;
        }

        // Так как поля A, B, C неизменяемы, то конструктор без параметров не задается

        // Метод класса для вычисления площади треугольника
        public double GetArea()
        {
            // Вычисление площади треугольника по трем сторонам через формулу Герона
            double p = (A + B + C) / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        // Статическая функция. Вызывается без объекта класса, принимает аргументы
        public static double GetArea(double a, double b, double c)
        {
            ValidateSide(a);
            ValidateSide(b);
            ValidateSide(c);

            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static int ObjectCount => objectCount;

        private static void ValidateSide(double side)
        {
            if (side <= 0)
                throw new ArgumentException("Side length must be positive and non-zero");
        }
    }
}
