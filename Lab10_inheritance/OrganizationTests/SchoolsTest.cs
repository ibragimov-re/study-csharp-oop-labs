using OrganizationLib;
using System;
using System.IO;
using Xunit;

namespace OrganizationTests
{
    public class SchoolTests
    {
        // ------------------ Конструкторы ------------------
        [Fact]
        public void DefaultConstructor_SetsDefaults()
        {
            var school = new School();
            Assert.Equal("Unknown", school.Name);
            Assert.Equal(0, school.StudentsCount);
        }

        [Fact]
        public void ParameterizedConstructor_SetsProperties()
        {
            var school = new School("HighSchool", 500);
            Assert.Equal("HighSchool", school.Name);
            Assert.Equal(500, school.StudentsCount);
        }

        [Fact]
        public void CopyConstructor_CreatesEqualObject()
        {
            var original = new School("HighSchool", 500);
            var copy = new School(original);
            Assert.Equal(original, copy);
        }

        // ------------------ ShallowCopy ------------------
        [Fact]
        public void ShallowCopy_CopiesObject()
        {
            var original = new School("HighSchool", 500);
            var copy = original.ShallowCopy();

            Assert.Equal(original, copy);

            // Проверяем, что имя хранится в одном объекте MutableName (поверхностное копирование)
            Assert.Same(original.GetType().GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(original),
                        copy.GetType().GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(copy));
        }

        // ------------------ Clone (глубокое копирование) ------------------
        [Fact]
        public void Clone_CopiesObject()
        {
            var original = new School("HighSchool", 500);
            var clone = (School)original.Clone();

            Assert.NotSame(original, clone);
            Assert.Equal("Clone of HighSchool", clone.Name);
            Assert.Equal(original.StudentsCount, clone.StudentsCount);
        }

        // ------------------ Свойство Name ------------------
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("AB")]
        [InlineData("ThisNameIsWayTooLongForSchoolClass")]
        public void Name_Invalid_ThrowsException(string invalidName)
        {
            var school = new School();
            Assert.Throws<ArgumentException>(() => school.Name = invalidName);
        }

        [Theory]
        [InlineData("ABC")]
        [InlineData("High School")]
        [InlineData("MySchool")]
        public void Name_Valid_SetsCorrectly(string validName)
        {
            var school = new School();
            school.Name = validName;
            Assert.Equal(validName, school.Name);
        }

        // ------------------ Свойство StudentsCount ------------------
        [Theory]
        [InlineData(-1)]
        [InlineData(5001)]
        public void StudentsCount_Invalid_ThrowsException(int invalidCount)
        {
            var school = new School();
            Assert.Throws<ArgumentException>(() => school.StudentsCount = invalidCount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(5000)]
        public void StudentsCount_Valid_SetsCorrectly(int validCount)
        {
            var school = new School();
            school.StudentsCount = validCount;
            Assert.Equal(validCount, school.StudentsCount);
        }

        // ------------------ Show ------------------
        [Fact]
        public void Show_PrintsCorrectly()
        {
            var originalOut = Console.Out;
            try
            {
                var school = new School("HighSchool", 500);
                using var writer = new StringWriter();
                Console.SetOut(writer);

                school.Show();

                string expected = $"Name: HighSchool, students: 500{Environment.NewLine}";
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
                using var input = new StringReader("HighSchool\n400\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var school = new School();
                school.Init();

                Assert.Equal("HighSchool", school.Name);
                Assert.Equal(400, school.StudentsCount);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Init_InvalidStudentsCount_Throws()
        {
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                using var input = new StringReader("HighSchool\nNotANumber\n");
                using var output = new StringWriter();
                Console.SetIn(input);
                Console.SetOut(output);

                var school = new School();
                Assert.Throws<ArgumentException>(() => school.Init());
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
            var school = new School();
            school.RandomInit();

            Assert.NotNull(school.Name);
            Assert.StartsWith("School", school.Name);
            Assert.InRange(school.StudentsCount, 10, 5000);
        }

        // ------------------ Equals ------------------
        [Fact]
        public void Equals_WorksCorrectly()
        {
            var s1 = new School("School1", 200);
            var s2 = new School("School2", 200);
            var s3 = new School("School1", 200);
            var s4 = new School("School1", 300);

            Assert.False(s1.Equals(s2));
            Assert.True(s1.Equals(s3));
            Assert.False(s1.Equals(s4));

            Assert.False(s1.Equals(5));
            Assert.False(s1.Equals("text"));
        }
    }
}
