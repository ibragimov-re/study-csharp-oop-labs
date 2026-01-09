using OrganizationLib;
using System;
using System.IO;
using Xunit;

// Отключение параллельного выполнения тестов для избежания конфликтов при перенаправлении консольного ввода/вывода
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace OrganizationTests
{
    public class OrganizationLibTests
    {
        // ------------------ Конструкторы ------------------
        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var org = new Organization();
            Assert.Equal("Unknown", org.Name);
            Assert.Equal(0, org.EmployeesCount);
        }

        [Fact]
        public void ParameterizedConstructor_SetsProperties()
        {
            var org = new Organization("TestOrg", 100);
            Assert.Equal("TestOrg", org.Name);
            Assert.Equal(100, org.EmployeesCount);
        }

        [Fact]
        public void CopyConstructor_CreatesEqualObject()
        {
            var org1 = new Organization("TestOrg", 100);
            var org2 = new Organization(org1);
            Assert.Equal(org1, org2);
        }

        // ------------------ Свойство Name ------------------
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        [InlineData("This name is definitely longer than thirty characters")]
        public void Name_Invalid_ThrowsException(string invalidName)
        {
            var org = new Organization();
            Assert.Throws<ArgumentException>(() => org.Name = invalidName);
        }

        [Theory]
        [InlineData("Org")]
        [InlineData("Valid Name")]
        public void Name_Valid_SetsCorrectly(string validName)
        {
            var org = new Organization();
            org.Name = validName;
            Assert.Equal(validName, org.Name);
        }

        // ------------------ Свойство EmployeesCount ------------------
        [Theory]
        [InlineData(-1)]
        [InlineData(5_000_001)]
        public void EmployeesCount_Invalid_ThrowsException(int invalidCount)
        {
            var org = new Organization();
            Assert.Throws<ArgumentException>(() => org.EmployeesCount = invalidCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5000)]
        [InlineData(5_000_000)]
        public void EmployeesCount_Valid_SetsCorrectly(int count)
        {
            var org = new Organization();
            org.EmployeesCount = count;
            Assert.Equal(count, org.EmployeesCount);
        }

        // ------------------ ShowNonVirtual и Show ------------------
        [Fact]
        public void ShowNonVirtual_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var org = new Organization("TestOrg", 42);
                using var writer = new StringWriter();
                Console.SetOut(writer);

                org.ShowNonVirtual();

                string expected = "Name: TestOrg, employees: 42" + Environment.NewLine;
                Assert.Equal(expected, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Show_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var org = new Organization("TestOrg", 42);
                using var writer = new StringWriter();
                Console.SetOut(writer);

                org.Show();

                string expected = "Name: TestOrg, employees: 42";
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
                using var input = new StringReader("TestOrg\n2000\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var org = new Organization();
                org.Init();

                Assert.Equal("TestOrg", org.Name);
                Assert.Equal(2000, org.EmployeesCount);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Init_InvalidEmployeeCount_Throws()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("TestOrg\nThisTextIsNotAnInt\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var org = new Organization();
                Assert.Throws<ArgumentException>(() => org.Init());
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
            var org = new Organization();
            org.RandomInit();

            Assert.NotNull(org.Name);
            Assert.StartsWith("Organization", org.Name);
        }

        // ------------------ Equals ------------------
        [Fact]
        public void Equals_WorksCorrectly()
        {
            var org1 = new Organization("Org1", 10);
            var org2 = new Organization("Org2", 10);
            var org3 = new Organization("Org1", 10);
            var org4 = new Organization("Org1", 400);

            Assert.False(org1.Equals(org2));
            Assert.True(org1.Equals(org3));
            Assert.False(org1.Equals(org4));

            Assert.False(org1.Equals(5));
            Assert.False(org1.Equals("some text"));
        }

        // ------------------ CompareTo ------------------
        [Fact]
        public void CompareTo_WorksCorrectly()
        {
            var org1 = new Organization("AOrg", 10);
            var org2 = new Organization("BOrg", 10);
            var org3 = new Organization("AOrg", 200);

            Assert.True(org1.CompareTo(org2) < 0);
            Assert.True(org2.CompareTo(org1) > 0);
            Assert.Equal(0, org1.CompareTo(org3));
        }

        [Fact]
        public void CompareTo_InvalidObject_Throws()
        {
            var org = new Organization("TestOrg", 10);

            Assert.Throws<ArgumentException>(() => org.CompareTo("string"));
            Assert.Throws<ArgumentException>(() => org.CompareTo(3));
        }
    }
}
