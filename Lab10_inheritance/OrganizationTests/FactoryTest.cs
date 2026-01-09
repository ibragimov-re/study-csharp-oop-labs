using OrganizationLib;
using System;
using System.IO;
using Xunit;

namespace OrganizationTests
{
    public class FactoryTests
    {
        // ------------------ Конструкторы ------------------
        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var factory = new Factory();
            Assert.Equal("Unknown", factory.Name);
            Assert.Equal(0, factory.EmployeesCount);
            Assert.Equal("Unknown", factory.ProductionType);
        }

        [Fact]
        public void ParameterizedConstructor_SetsProperties()
        {
            var factory = new Factory("MegaFactory", 200, "Electronics");
            Assert.Equal("MegaFactory", factory.Name);
            Assert.Equal(200, factory.EmployeesCount);
            Assert.Equal("Electronics", factory.ProductionType);
        }

        [Fact]
        public void CopyConstructor_CreatesEqualObject()
        {
            var f1 = new Factory("MegaFactory", 200, "Electronics");
            var f2 = new Factory(f1);
            Assert.Equal(f1, f2);
        }

        // ------------------ Свойство ProductionType ------------------
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        [InlineData("ThisProductionTypeNameIsWayTooLong")]
        public void ProductionType_Invalid_ThrowsException(string invalidType)
        {
            var factory = new Factory();
            Assert.Throws<ArgumentException>(() => factory.ProductionType = invalidType);
        }

        [Theory]
        [InlineData("Food")]
        [InlineData("Machinery")]
        [InlineData("Chemicals")]
        public void ProductionType_Valid_SetsCorrectly(string validType)
        {
            var factory = new Factory();
            factory.ProductionType = validType;
            Assert.Equal(validType, factory.ProductionType);
        }

        // ------------------ Show ------------------
        [Fact]
        public void Show_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var factory = new Factory("MegaFactory", 150, "Furniture");
                using var writer = new StringWriter();
                Console.SetOut(writer);

                factory.Show();

                string expected = $"Name: MegaFactory, employees: 150, production type: Furniture{Environment.NewLine}";
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
                using var input = new StringReader("MegaFactory\n100\nFood\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var factory = new Factory();
                factory.Init();

                Assert.Equal("MegaFactory", factory.Name);
                Assert.Equal(100, factory.EmployeesCount);
                Assert.Equal("Food", factory.ProductionType);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Init_InvalidProductionType_Throws()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("MegaFactory\n100\nA\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var factory = new Factory();
                Assert.Throws<ArgumentException>(() => factory.Init());
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
            var factory = new Factory();
            factory.RandomInit();

            Assert.NotNull(factory.Name);
            Assert.StartsWith("Organization", factory.Name); // базовый RandomInit
            Assert.False(string.IsNullOrWhiteSpace(factory.ProductionType));
        }

        // ------------------ Equals ------------------
        [Fact]
        public void Equals_WorksCorrectly()
        {
            var f1 = new Factory("Factory1", 50, "Electronics");
            var f2 = new Factory("Factory2", 50, "Electronics");
            var f3 = new Factory("Factory1", 50, "Electronics");
            var f4 = new Factory("Factory1", 50, "Food");
            var f5 = new Factory("Factory1", 100, "Electronics");

            Assert.False(f1.Equals(f2));
            Assert.True(f1.Equals(f3));
            Assert.False(f1.Equals(f4));
            Assert.False(f1.Equals(f5));

            Assert.False(f1.Equals(5));
            Assert.False(f1.Equals("some text"));
        }

        // ------------------ CompareTo (через базовый класс) ------------------
        [Fact]
        public void CompareTo_WorksCorrectly()
        {
            var f1 = new Factory("AFactory", 50, "Electronics");
            var f2 = new Factory("BFactory", 50, "Food");
            var f3 = new Factory("AFactory", 200, "Machinery");

            Assert.True(f1.CompareTo(f2) < 0);
            Assert.True(f2.CompareTo(f1) > 0);
            Assert.Equal(0, f1.CompareTo(f3));
        }

        [Fact]
        public void CompareTo_InvalidObject_Throws()
        {
            var factory = new Factory("TestFactory", 50, "Food");

            Assert.Throws<ArgumentException>(() => factory.CompareTo("string"));
            Assert.Throws<ArgumentException>(() => factory.CompareTo(3));
        }
    }
}
