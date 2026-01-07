using OrganizationLib;

class Part2
{
    private const int organizationsCount = 20;

    static void Main()
    {
        Organization[] orgs = new Organization[organizationsCount];
        InitializeOrgs(orgs);
        
        while (true)
        {
            PrintMenu();

            if (!int.TryParse(Console.ReadLine(), out int operationNumber))
            {
                Console.WriteLine("Unknown option, try again");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();

            switch (operationNumber)
            {
                case 1:
                    ShowOrgsBySelectedType(orgs);
                    break;

                case 2:
                    ShowShipCompsWithMoreShips(orgs);
                    break;

                case 3:
                    ShowOrgsCountByType(orgs);
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Unknown option, try again");
                    Console.WriteLine();
                    continue;
            }
            Console.WriteLine("\n");
        }
    }

    static void InitializeOrgs(Organization[] orgs)
    {
        Random rand = new Random();

        for (int i = 0; i < orgs.Length; i++)
        {
            int t = rand.Next(4);
            switch (t)
            {
                case 0:
                    orgs[i] = new InsuranceCompany();
                    break;
                case 1:
                    orgs[i] = new ShipbuildingCompany();
                    break;
                case 2:
                    orgs[i] = new Factory();
                    break;
                case 3:
                    orgs[i] = new Library();
                    break;
            }

            orgs[i].RandomInit();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("===== LAB10 PART2 MENU =====");
        Console.WriteLine("1 - Show organizations info of selected type (is)");
        Console.WriteLine("2 - Show shipbuilding companies with more than the specified number of ships (as)");
        Console.WriteLine("3 - Count all organizations and by type (typeof)");
        Console.WriteLine("0 - Exit");
        Console.Write("Enter a number: ");
    }

    // Демонстрация идентификации через is
    static void ShowOrgsBySelectedType(Organization[] orgs)
    {
        Console.WriteLine("Choose type:");
        Console.WriteLine("1 - Insurance Company");
        Console.WriteLine("2 - Shipbuilding Company");
        Console.WriteLine("3 - Factory");
        Console.WriteLine("4 - Library");
        Console.Write("Enter a number: ");

        if (!int.TryParse(Console.ReadLine(), out int typeNumber))
        {
            Console.WriteLine("Unknown type, try again");
            Console.WriteLine();
            return;
        }

        Console.WriteLine();

        foreach (var org in orgs)
        {
            bool match =
                (typeNumber == 1 && org is InsuranceCompany)
                || (typeNumber == 2 && org is ShipbuildingCompany)
                || (typeNumber == 3 && org is Factory)
                || (typeNumber == 4 && org is Library);

            if (match)
                org.Show();
        }
    }

    // Демонстрация идентификации через as
    static void ShowShipCompsWithMoreShips(Organization[] orgs)
    {
        Console.Write("Enter minimum ship count: ");
        if (!int.TryParse(Console.ReadLine(), out int minCount))
        {
            Console.WriteLine("Count must be a valid integer");
            Console.WriteLine();
            return;
        }

        Console.WriteLine();

        foreach (var org in orgs)
        {
            // Попытка приведения к нужному типу при неудаче возвращается null без исключения
            var shipCompany = org as ShipbuildingCompany;
            if (shipCompany != null && shipCompany.ShipCount > minCount)
                Console.WriteLine($"Name: {shipCompany.Name}, ships: {shipCompany.ShipCount}");
        }
    }

    // Демонстрация идентификации через typeof.
    // проверка строгого типа, без наследников
    static void ShowOrgsCountByType(Organization[] orgs)
    {
        int insurCount = 0, shipCount = 0, factCount = 0, librCount = 0;

        foreach (var org in orgs)
        {
            if (org.GetType() == typeof(InsuranceCompany))
                insurCount++;
            else if (org.GetType() == typeof(ShipbuildingCompany))
                shipCount++;
            else if (org.GetType() == typeof(Factory))
                factCount++;
            else if (org.GetType() == typeof(Library))
                librCount++;
        }

        Console.WriteLine($"Total: {orgs.Length}");
        Console.WriteLine($"Insurance Company: {insurCount}");
        Console.WriteLine($"Shipbuilding Company: {shipCount}");
        Console.WriteLine($"Factory: {factCount}");
        Console.WriteLine($"Library: {librCount}");
    }
}
