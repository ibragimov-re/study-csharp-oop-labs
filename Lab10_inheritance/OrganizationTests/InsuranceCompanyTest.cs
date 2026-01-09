using OrganizationLib;
using System;
using System.IO;
using Xunit;

namespace OrganizationTests
{
    public class InsuranceCompanyTests
    {
        // ------------------ Конструкторы ------------------
        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var ic = new InsuranceCompany();
            Assert.Equal("Unknown", ic.Name);
            Assert.Equal(0, ic.EmployeesCount);
            Assert.Equal("Unknown", ic.InsuranceType);
        }

        [Fact]
        public void ParameterizedConstructor_SetsProperties()
        {
            var ic = new InsuranceCompany("InsureCorp", 100, "Health");
            Assert.Equal("InsureCorp", ic.Name);
            Assert.Equal(100, ic.EmployeesCount);
            Assert.Equal("Health", ic.InsuranceType);
        }

        [Fact]
        public void CopyConstructor_CreatesEqualObject()
        {
            var ic1 = new InsuranceCompany("InsureCorp", 100, "Health");
            var ic2 = new InsuranceCompany(ic1);
            Assert.Equal(ic1, ic2);
        }

        // ------------------ Свойство InsuranceType ------------------
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        [InlineData("ThisInsuranceTypeIsWayTooLong")]
        public void InsuranceType_Invalid_ThrowsException(string invalidType)
        {
            var ic = new InsuranceCompany();
            Assert.Throws<ArgumentException>(() => ic.InsuranceType = invalidType);
        }

        [Theory]
        [InlineData("Life")]
        [InlineData("Car")]
        [InlineData("Travel")]
        public void InsuranceType_Valid_SetsCorrectly(string validType)
        {
            var ic = new InsuranceCompany();
            ic.InsuranceType = validType;
            Assert.Equal(validType, ic.InsuranceType);
        }

        // ------------------ Show ------------------
        [Fact]
        public void Show_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var ic = new InsuranceCompany("InsureCorp", 42, "Health");
                using var writer = new StringWriter();
                Console.SetOut(writer);

                ic.Show();

                string expected = $"Name: InsureCorp, employees: 42, insurance type: Health{Environment.NewLine}";
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
                using var input = new StringReader("InsureCorp\n200\nLife\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var ic = new InsuranceCompany();
                ic.Init();

                Assert.Equal("InsureCorp", ic.Name);
                Assert.Equal(200, ic.EmployeesCount);
                Assert.Equal("Life", ic.InsuranceType);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Init_InvalidInsuranceType_Throws()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("InsureCorp\n200\nA\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var ic = new InsuranceCompany();
                Assert.Throws<ArgumentException>(() => ic.Init());
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
            var ic = new InsuranceCompany();
            ic.RandomInit();

            Assert.NotNull(ic.Name);
            Assert.StartsWith("Organization", ic.Name); // из базового RandomInit
            Assert.False(string.IsNullOrWhiteSpace(ic.InsuranceType));
        }

        // ------------------ Equals ------------------
        [Fact]
        public void Equals_WorksCorrectly()
        {
            var ic1 = new InsuranceCompany("Insure1", 10, "Health");
            var ic2 = new InsuranceCompany("Insure2", 10, "Health");
            var ic3 = new InsuranceCompany("Insure1", 10, "Health");
            var ic4 = new InsuranceCompany("Insure1", 10, "Life");
            var ic5 = new InsuranceCompany("Insure1", 50, "Health");

            Assert.False(ic1.Equals(ic2));
            Assert.True(ic1.Equals(ic3));
            Assert.False(ic1.Equals(ic4));
            Assert.False(ic1.Equals(ic5));

            Assert.False(ic1.Equals(5));
            Assert.False(ic1.Equals("some text"));
        }

        // ------------------ CompareTo (через базовый класс) ------------------
        [Fact]
        public void CompareTo_WorksCorrectly()
        {
            var ic1 = new InsuranceCompany("AInsure", 10, "Health");
            var ic2 = new InsuranceCompany("BInsure", 10, "Life");
            var ic3 = new InsuranceCompany("AInsure", 200, "Car");

            Assert.True(ic1.CompareTo(ic2) < 0);
            Assert.True(ic2.CompareTo(ic1) > 0);
            Assert.Equal(0, ic1.CompareTo(ic3));
        }

        [Fact]
        public void CompareTo_InvalidObject_Throws()
        {
            var ic = new InsuranceCompany("Test", 10, "Life");

            Assert.Throws<ArgumentException>(() => ic.CompareTo("string"));
            Assert.Throws<ArgumentException>(() => ic.CompareTo(3));
        }
    }
}
