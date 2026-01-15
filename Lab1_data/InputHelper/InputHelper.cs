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
                    Console.WriteLine("[Error] Enter a valid number");

            } while (!isValid);

            return value;
        }

        public static double ReadDouble(string message)
        {
            double value;
            bool isValid;

            do
            {
                Console.Write(message);
                isValid = double.TryParse(Console.ReadLine(), out value);

                if (!isValid)
                    Console.WriteLine("[Error] Enter a valid number");

            } while (!isValid);

            return value;
        }
    }
}
