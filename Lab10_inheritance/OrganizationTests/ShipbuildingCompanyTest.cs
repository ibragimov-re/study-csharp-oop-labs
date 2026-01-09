using OrganizationLib;
using System;
using System.IO;
using Xunit;

namespace OrganizationTests
{
    public class ShipbuildingCompanyTests
    {
        // ------------------ Конструкторы ------------------
        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var company = new ShipbuildingCompany();
            Assert.Equal("Unknown", company.Name);
            Assert.Equal(0, company.EmployeesCount);
            Assert.Equal(0, company.ShipCount);
        }

        [Fact]
        public void ParameterizedConstructor_SetsProperties()
        {
            var company = new ShipbuildingCompany("ShipWorks", 200, 150);
            Assert.Equal("ShipWorks", company.Name);
            Assert.Equal(200, company.EmployeesCount);
            Assert.Equal(150, company.ShipCount);
        }

        [Fact]
        public void CopyConstructor_CreatesEqualObject()
        {
            var original = new ShipbuildingCompany("ShipWorks", 200, 150);
            var copy = new ShipbuildingCompany(original);
            Assert.Equal(original, copy);
        }

        // ------------------ Свойство ShipCount ------------------
        [Theory]
        [InlineData(-1)]
        [InlineData(50001)]
        public void ShipCount_Invalid_ThrowsException(int invalidCount)
        {
            var company = new ShipbuildingCompany();
            Assert.Throws<ArgumentException>(() => company.ShipCount = invalidCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(500)]
        [InlineData(50000)]
        public void ShipCount_Valid_SetsCorrectly(int validCount)
        {
            var company = new ShipbuildingCompany();
            company.ShipCount = validCount;
            Assert.Equal(validCount, company.ShipCount);
        }

        // ------------------ Show ------------------
        [Fact]
        public void Show_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var company = new ShipbuildingCompany("ShipWorks", 42, 100);
                using var writer = new StringWriter();
                Console.SetOut(writer);

                company.Show();

                string expected = $"Name: ShipWorks, employees: 42, ship count: 100{Environment.NewLine}";
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
                using var input = new StringReader("ShipWorks\n50\n200\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var company = new ShipbuildingCompany();
                company.Init();

                Assert.Equal("ShipWorks", company.Name);
                Assert.Equal(50, company.EmployeesCount);
                Assert.Equal(200, company.ShipCount);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Init_InvalidShipCount_Throws()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("ShipWorks\n50\nNotANumber\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var company = new ShipbuildingCompany();
                Assert.Throws<ArgumentException>(() => company.Init());
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
            var company = new ShipbuildingCompany();
            company.RandomInit();

            Assert.NotNull(company.Name);
            Assert.StartsWith("Organization", company.Name); // базовый RandomInit
            Assert.InRange(company.ShipCount, 0, 50000);
        }

        // ------------------ Equals ------------------
        [Fact]
        public void Equals_WorksCorrectly()
        {
            var c1 = new ShipbuildingCompany("Ship1", 50, 100);
            var c2 = new ShipbuildingCompany("Ship2", 50, 100);
            var c3 = new ShipbuildingCompany("Ship1", 50, 100);
            var c4 = new ShipbuildingCompany("Ship1", 50, 200);
            var c5 = new ShipbuildingCompany("Ship1", 100, 100);

            Assert.False(c1.Equals(c2));
            Assert.True(c1.Equals(c3));
            Assert.False(c1.Equals(c4));
            Assert.False(c1.Equals(c5));

            Assert.False(c1.Equals(5));
            Assert.False(c1.Equals("some text"));
        }

        // ------------------ CompareTo (через базовый класс) ------------------
        [Fact]
        public void CompareTo_WorksCorrectly()
        {
            var c1 = new ShipbuildingCompany("AShip", 50, 100);
            var c2 = new ShipbuildingCompany("BShip", 50, 200);
            var c3 = new ShipbuildingCompany("AShip", 200, 50);

            Assert.True(c1.CompareTo(c2) < 0);
            Assert.True(c2.CompareTo(c1) > 0);
            Assert.Equal(0, c1.CompareTo(c3));
        }

        [Fact]
        public void CompareTo_InvalidObject_Throws()
        {
            var company = new ShipbuildingCompany("TestShip", 50, 100);

            Assert.Throws<ArgumentException>(() => company.CompareTo("string"));
            Assert.Throws<ArgumentException>(() => company.CompareTo(3));
        }
    }
}
