namespace InputHelperLib
{
    public class InputHelper
    {
        // Методы для проверки ввода значений в консоль
        public static int ReadInt(string message)
        {
            int value;
            bool isValid;

            do
            {
                Console.Write(message);
                isValid = int.TryParse(Console.ReadLine(), out value);

                if (!isValid)
                    Console.WriteLine("[Error] enter a valid number");

            } while (!isValid);

            return value;
        }

        public static int ReadPositiveInt(string message)
        {
            int value;

            do
            {
                value = ReadInt(message);

                if (value <= 0)
                    Console.WriteLine("[Error] enter a positive and non-zero number");

            } while (value <= 0);

            return value;
        }
    }
}
