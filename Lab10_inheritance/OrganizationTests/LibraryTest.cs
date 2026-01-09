using OrganizationLib;
using System;
using System.IO;
using Xunit;

namespace OrganizationTests
{
    public class LibraryTests
    {
        // ------------------ Конструкторы ------------------
        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var lib = new Library();
            Assert.Equal("Unknown", lib.Name);
            Assert.Equal(0, lib.EmployeesCount);
            Assert.Equal(0, lib.BooksCount);
        }

        [Fact]
        public void ParameterizedConstructor_SetsProperties()
        {
            var lib = new Library("CityLibrary", 50, 10000);
            Assert.Equal("CityLibrary", lib.Name);
            Assert.Equal(50, lib.EmployeesCount);
            Assert.Equal(10000, lib.BooksCount);
        }

        [Fact]
        public void CopyConstructor_CreatesEqualObject()
        {
            var lib1 = new Library("CityLibrary", 50, 10000);
            var lib2 = new Library(lib1);
            Assert.Equal(lib1, lib2);
        }

        // ------------------ Свойство BooksCount ------------------
        [Theory]
        [InlineData(-1)]
        [InlineData(50_000_001)]
        public void BooksCount_Invalid_ThrowsException(int invalidCount)
        {
            var lib = new Library();
            Assert.Throws<ArgumentException>(() => lib.BooksCount = invalidCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5000)]
        [InlineData(50_000_000)]
        public void BooksCount_Valid_SetsCorrectly(int validCount)
        {
            var lib = new Library();
            lib.BooksCount = validCount;
            Assert.Equal(validCount, lib.BooksCount);
        }

        // ------------------ Show ------------------
        [Fact]
        public void Show_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var lib = new Library("CityLibrary", 42, 15000);
                using var writer = new StringWriter();
                Console.SetOut(writer);

                lib.Show();

                string expected = $"Name: CityLibrary, employees: 42, books count: 15000{Environment.NewLine}";
                Assert.Equal(expected, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        // ------------------ Init ------------------
        [Fact]
        public void Init_NormalInput_SetsProperties()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("CityLibrary\n20\n5000\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var lib = new Library();
                lib.Init();

                Assert.Equal("CityLibrary", lib.Name);
                Assert.Equal(20, lib.EmployeesCount);
                Assert.Equal(5000, lib.BooksCount);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Init_InvalidBooksCount_Throws()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("CityLibrary\n20\nNotANumber\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var lib = new Library();
                Assert.Throws<ArgumentException>(() => lib.Init());
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        // ------------------ RandomInit ------------------
        [Fact]
        public void RandomInit_SetsValidValues()
        {
            var lib = new Library();
            lib.RandomInit();

            Assert.NotNull(lib.Name);
            Assert.StartsWith("Organization", lib.Name); // из базового RandomInit
            Assert.InRange(lib.BooksCount, 0, 50_000_000);
        }

        // ------------------ Equals ------------------
        [Fact]
        public void Equals_WorksCorrectly()
        {
            var lib1 = new Library("Lib1", 10, 1000);
            var lib2 = new Library("Lib2", 10, 1000);
            var lib3 = new Library("Lib1", 10, 1000);
            var lib4 = new Library("Lib1", 10, 5000);
            var lib5 = new Library("Lib1", 50, 1000);

            Assert.False(lib1.Equals(lib2));
            Assert.True(lib1.Equals(lib3));
            Assert.False(lib1.Equals(lib4));
            Assert.False(lib1.Equals(lib5));

            Assert.False(lib1.Equals(5));
            Assert.False(lib1.Equals("some text"));
        }

        // ------------------ CompareTo (через базовый класс) ------------------
        [Fact]
        public void CompareTo_WorksCorrectly()
        {
            var lib1 = new Library("ALib", 10, 1000);
            var lib2 = new Library("BLib", 10, 2000);
            var lib3 = new Library("ALib", 200, 500);

            Assert.True(lib1.CompareTo(lib2) < 0);
            Assert.True(lib2.CompareTo(lib1) > 0);
            Assert.Equal(0, lib1.CompareTo(lib3));
        }

        [Fact]
        public void CompareTo_InvalidObject_Throws()
        {
            var lib = new Library("TestLib", 10, 100);

            Assert.Throws<ArgumentException>(() => lib.CompareTo("string"));
            Assert.Throws<ArgumentException>(() => lib.CompareTo(3));
        }
    }
}
