using InputHelperLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("================= LAB2 TASK4 =================");

        var n = InputHelper.ReadPositiveInt("Enter the number of elements n: ");
        var result = CalculateSumOfOdd(n);

        Console.WriteLine($"Sum of odd elements: {result}");
    }

    static int CalculateSumOfOdd(int n)
    {
        int sum = 0;

        for (int i = 1; i <= n; i++)
        {
            int x = InputHelper.ReadInt("Enter a number: ");

            if (x % 2 != 0)
                sum += x;
        }

        return sum;
    }
}