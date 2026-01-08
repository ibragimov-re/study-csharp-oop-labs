using OrganizationLib;

class Part3
{
    private const int organizationsCount = 5;
    private const int organizationTypesCount = 4;

    static void Main()
    {
        // Работа с сортировками через IComparable и IComparer, бинарным поиском
        // =======================================================================
        Console.WriteLine("Work with sorting and searching organizations");
        Console.WriteLine("=======================================================");

        Organization[] orgs = new Organization[organizationsCount];
        InitializeOrgs(orgs);

        Console.WriteLine("Organizations before sorting:");
        ShowOrganizations(orgs);

        // Сортировка по имени (благодаря добавленому IComparable в Organization)
        // Внутри метода Sort вызывается CompareTo интерфейса IComparable
        Array.Sort(orgs);
        Console.WriteLine("\nOrganizations after sorting by name:");
        ShowOrganizations(orgs);

        // Сортировка по количеству сотрудников через Comparer в OrganizationByEmployeesComparer
        Array.Sort(orgs, new OrganizationByEmployeesComparer());
        Console.WriteLine("\nOrganizations after sorting by employees count:");
        ShowOrganizations(orgs);

        Console.Write("\n[Search organization] Enter employee count: ");
        if (!int.TryParse(Console.ReadLine(), out int employeeCount))
        {
            Console.WriteLine("Count must be a valid integer");
            return;
        }

        // Бинарный поиск организации по количеству сотрудников
        // Перед бинарным поиском массив должен быть отсортирован по тому же критерию
        // В этом случае - по количеству сотрудников
        var orgIndex = BinarySearchByEmployeesCount(orgs, employeeCount);
        if (orgIndex != -1)
            orgs[orgIndex].Show();
        else
            Console.WriteLine("Organization not found");


        // Работа с интерфейсом IInit
        // =======================================================================
        Console.WriteLine("\n\nWork with IInit interface");
        Console.WriteLine("=======================================================");

        IInit[] inits = new IInit[6];

        InitializeAllOrgsWithSchools(inits);

        Console.WriteLine("\nAll organizations including schools");
        foreach (var org in inits)
            org.Show();


        // Работа с поверхностным и глубоким клонированием
        // =======================================================================
        Console.WriteLine("\n\nWork with cloning");
        Console.WriteLine("=======================================================");
        School originalSchool = new School("School", 500);
        School shallowCopySchool = originalSchool.ShallowCopy();
        School clonedSchool = (School)originalSchool.Clone();
        PrintSchoolOriginalAndCopies(originalSchool, shallowCopySchool, clonedSchool);

        Console.WriteLine("\nChanging original school's name...");
        originalSchool.Name = "Cool School";
        
        Console.WriteLine("After changes:");
        PrintSchoolOriginalAndCopies(originalSchool, shallowCopySchool, clonedSchool);
    }

    static void InitializeOrgs(Organization[] orgs)
    {
        Random rand = new Random();

        for (int i = 0; i < orgs.Length; i++)
        {
            int typeIndex = rand.Next(organizationTypesCount);
            orgs[i] = typeIndex switch
            {
                0 => new InsuranceCompany(),
                1 => new ShipbuildingCompany(),
                2 => new Factory(),
                3 => new Library(),
                _ => throw new InvalidOperationException()
            };

            orgs[i].RandomInit();
        }
    }

    static void ShowOrganizations(Organization[] orgs)
    {
        foreach (var org in orgs)
        {
            org.Show();
        }
    }

    static int BinarySearchByEmployeesCount(Organization[] orgs, int count)
    {
        int leftBound = 0;
        int rightBound = orgs.Length - 1;

        while (leftBound <= rightBound)
        {
            int middleIndex = (leftBound + rightBound) / 2;
            int middleValue = orgs[middleIndex].EmployeesCount;

            if (middleValue == count)
                return middleIndex;

            if (middleValue < count)
                leftBound = middleIndex + 1;
            else
                rightBound = middleIndex - 1;
        }

        // Если не найдено
        return -1;
    }

    static void InitializeAllOrgsWithSchools(IInit[] inits)
    {
        Random rand = new Random();

        inits[0] = new InsuranceCompany();
        inits[1] = new ShipbuildingCompany();
        inits[2] = new Factory();
        inits[3] = new Library();
        inits[4] = new School();
        inits[5] = new School();

        // Случайная инициализация всех объектов, кроме последнего
        for (int i = 0; i < inits.Length - 1; i++)
            inits[i].RandomInit();

        // Последний объект инициализируется пользователем для демонстрации
        inits[inits.Length - 1].Init();
    }

    static void PrintSchoolOriginalAndCopies(School original, School shallowCopy, School clone)
    {
        Console.Write("[Original school] ");
        original.Show();
        Console.Write("[Shallow copy] ");
        shallowCopy.Show();
        Console.Write("[Cloned school] ");
        clone.Show();
    }
}