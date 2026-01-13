using Geometry.Domain;

namespace Geometry.Tests
{
    public class TriangleArrayTests
    {
        // ------------------ Конструктор без параметров ------------------
        [Fact]
        public void DefaultConstructor_CreatesEmptyArray()
        {
            var array = new TriangleArray();
            Assert.Equal(0, array.Length);
        }

        // ------------------ Конструктор с размером ------------------
        [Fact]
        public void SizeConstructor_ZeroSize_CreatesEmptyArray()
        {
            var array = new TriangleArray(0);
            Assert.Equal(0, array.Length);
        }

        [Fact]
        public void SizeConstructor_NegativeSize_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TriangleArray(-5));
        }

        [Fact]
        public void SizeConstructor_PositiveSize_CreatesArray()
        {
            int size = 3;
            var array = new TriangleArray(size);

            Assert.Equal(size, array.Length);
            for (int i = 0; i < size; i++)
            {
                Assert.True(array[i]); // implicit operator bool
                Assert.InRange(array[i].A, 1, 10);
                Assert.InRange(array[i].B, 1, 10);
                Assert.InRange(array[i].C, 1, 10);
            }
        }

        // ------------------ Конструктор с фабрикой ------------------
        [Fact]
        public void FactoryConstructor_ZeroSize_CreatesEmptyArray()
        {
            var array = new TriangleArray(0, () => new Triangle(3, 4, 5));
            Assert.Equal(0, array.Length);
        }

        [Fact]
        public void FactoryConstructor_NegativeSize_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TriangleArray(-2, () => new Triangle(3, 4, 5)));
        }

        [Fact]
        public void FactoryConstructor_NullFactory_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TriangleArray(3, null));
        }

        [Fact]
        public void FactoryConstructor_PositiveSize_CreatesArray()
        {
            int size = 2;
            var array = new TriangleArray(size, () => new Triangle(3, 4, 5));

            Assert.Equal(size, array.Length);
            for (int i = 0; i < size; i++)
            {
                var t = array[i];
                Assert.Equal(3, t.A);
                Assert.Equal(4, t.B);
                Assert.Equal(5, t.C);
            }
        }

        // ------------------ Индексатор ------------------
        [Fact]
        public void Indexer_GetSet_WorksCorrectly()
        {
            var array = new TriangleArray(2, () => new Triangle(1, 1, 1));

            array[0] = new Triangle(3, 4, 5);
            var t = array[0];
            Assert.Equal(3, t.A);
            Assert.Equal(4, t.B);
            Assert.Equal(5, t.C);
        }

        [Fact]
        public void Indexer_Get_InvalidIndex_ThrowsArgumentException()
        {
            var array = new TriangleArray(2, () => new Triangle(1, 1, 1));
            Assert.Throws<ArgumentException>(() => _ = array[-1]);
            Assert.Throws<ArgumentException>(() => _ = array[2]);
        }

        [Fact]
        public void Indexer_Set_InvalidIndex_ThrowsArgumentException()
        {
            var array = new TriangleArray(2, () => new Triangle(1, 1, 1));
            Assert.Throws<ArgumentException>(() => array[-1] = new Triangle(2, 2, 2));
            Assert.Throws<ArgumentException>(() => array[2] = new Triangle(2, 2, 2));
        }

        // ------------------ Print ------------------
        [Fact]
        public void Print_EmptyArray_PrintsMessage()
        {
            var array = new TriangleArray();
            var originalOut = Console.Out;
            try
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                array.Print();

                string output = writer.ToString();
                Assert.Contains("Array is empty", output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Print_NonEmptyArray_PrintsTriangles()
        {
            var array = new TriangleArray(2, () => new Triangle(3, 4, 5));
            var originalOut = Console.Out;
            try
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                array.Print();

                string output = writer.ToString();
                Assert.Contains("a=3, b=4, c=5", output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}
