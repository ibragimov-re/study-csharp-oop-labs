using InputHelperLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("================= LAB2 TASK38 =================");

        int numberOfTerm = InputHelper.ReadPositiveInt("Enter the number of terms n: ");
        int result = CalculateSeriesSum(numberOfTerm);

        Console.WriteLine($"S = {result}");
    }

    static int CalculateSeriesSum(int numberOfTerm)
    {
        int term = 15; // Слагаемое, начинается с 15 по условию задачи
        int sum = 0; // Счетчик, начиная с первого слагаемого
        int currentTerm = 1;

        do
        {
            // Каждое третье слагаемое вычитается
            if (currentTerm % 3 == 0)
                sum -= term;
            else
                sum += term;

            term += 2;
            currentTerm++;

        } while (currentTerm <= numberOfTerm);

        return sum;
    }
}