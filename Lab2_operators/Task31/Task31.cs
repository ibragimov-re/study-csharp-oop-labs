using InputHelperLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("================= LAB9 TASK31 =================");

        int k;

        do
        {
            k = InputHelper.ReadInt("Enter the number K: ");

            if (k == 0)
                Console.WriteLine("[Error] K must not be zero");

        } while (k == 0);

        int result = CalculateCountDivisibleByK(k);

        Console.WriteLine($"Number of elements divisible by {k}: {result}");
    }

    static int CalculateCountDivisibleByK(int k)
    {
        int count = 0;

        Console.WriteLine("Enter the sequence of numbers (0 to finish):");
        while (true)
        {
            int x = InputHelper.ReadInt("Enter a number: ");

            if (x == 0)
                break;

            if (x % k == 0)
                count++;
        }

        return count;
    }
}