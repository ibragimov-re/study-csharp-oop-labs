using Geometry.Domain;

namespace Geometry.Tests
{
    public class TriangleTests
    {
        // ------------------ Конструктор ------------------

        [Fact]
        public void Constructor_ValidSides_CreatesTriangle()
        {
            var triangle = new Triangle(3, 4, 5);

            Assert.Equal(3, triangle.A);
            Assert.Equal(4, triangle.B);
            Assert.Equal(5, triangle.C);
        }

        [Theory]
        [InlineData(0, 4, 5)]
        [InlineData(-3, 4, 5)]
        [InlineData(3, 0, 5)]
        public void Constructor_NonPositiveSide_Throws(double a, double b, double c)
        {
            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
        }

        // ------------------ Площадь ------------------

        [Fact]
        public void GetArea_ValidTriangle_ReturnsCorrectArea()
        {
            var triangle = new Triangle(3, 4, 5);

            double area = triangle.GetArea();

            Assert.Equal(6, area);
        }

        [Fact]
        public void GetArea_NonExistentTriangle_ReturnsMinusOne()
        {
            var triangle = new Triangle(10, 1, 1);

            double area = triangle.GetArea();

            Assert.Equal(-1, area);
        }

        [Fact]
        public void StaticGetArea_ValidTriangle_ReturnsCorrectArea()
        {
            double area = Triangle.GetArea(3, 4, 5);

            Assert.Equal(6, area);
        }

        [Fact]
        public void StaticGetArea_InvalidTriangle_ReturnsMinusOne()
        {
            double area = Triangle.GetArea(1, 2, 10);

            Assert.Equal(-1, area);
        }

        // ------------------ ObjectCount ------------------

        [Fact]
        public void ObjectCount_IncreasesWhenTriangleCreated()
        {
            int before = Triangle.ObjectCount;

            var t1 = new Triangle(3, 4, 5);
            var t2 = new Triangle(5, 6, 7);

            Assert.Equal(before + 2, Triangle.ObjectCount);
        }

        // ------------------ Унарные операторы ------------------

        [Fact]
        public void IncrementOperator_IncreasesAllSides()
        {
            var t = new Triangle(3, 4, 5);

            t++;

            Assert.Equal(4, t.A);
            Assert.Equal(5, t.B);
            Assert.Equal(6, t.C);
        }

        [Fact]
        public void DecrementOperator_DecreasesAllSides()
        {
            var t = new Triangle(5, 6, 7);

            t--;

            Assert.Equal(4, t.A);
            Assert.Equal(5, t.B);
            Assert.Equal(6, t.C);
        }

        // ------------------ Бинарные операторы ------------------

        [Fact]
        public void ComparisonOperators_WorkCorrectly()
        {
            var t1 = new Triangle(3, 4, 5);   // area = 6
            var t2 = new Triangle(6, 7, 8);   // area > 6

            Assert.True(t1 <= t2);
            Assert.False(t1 >= t2);
        }

        // ------------------ Приведение типов ------------------

        [Fact]
        public void ExplicitDoubleConversion_ReturnsArea()
        {
            Assert.Equal(6, (double)new Triangle(3, 4, 5));

            Assert.Equal(-1, (double)new Triangle(1, 2, 10));
        }

        [Fact]
        public void ImplicitBoolConversion_ValidTriangle_ReturnsTrue()
        {
            var triangle = new Triangle(3, 4, 5);

            bool exists = triangle;

            Assert.True(exists);
        }

        [Fact]
        public void ImplicitBoolConversion_InvalidTriangle_ReturnsFalse()
        {
            var triangle = new Triangle(10, 1, 1);

            bool exists = triangle;

            Assert.False(exists);
        }

        // ------------------ Random triangle ------------------

        [Fact]
        public void CreateRandomTriangle_CreatesValidTriangle()
        {
            var triangle = Triangle.CreateRandomTriangle(5, 100);

            Assert.True(triangle);
            Assert.True(triangle.A > 0);
            Assert.True(triangle.B > 0);
            Assert.True(triangle.C > 0);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(-5, 10)]
        [InlineData(10, 10)]
        [InlineData(15, 10)]
        public void CreateRandomTriangle_InvalidRange_ThrowsArgumentException(int min, int max)
        {
            Assert.Throws<ArgumentException>(() => Triangle.CreateRandomTriangle(min, max));
        }

        [Fact]
        public void CreateRandomTriangle_InvalidRange_Throws()
        {
            Assert.Throws<ArgumentException>(() => Triangle.CreateRandomTriangle(5, 5));
        }
    }
}
