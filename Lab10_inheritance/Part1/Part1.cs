using OrganizationLib;
using System;

class Part1
{
    static void Main()
    {
        var rand = new Random();

        Organization[] orgs = new Organization[4];

        orgs[0] = new InsuranceCompany();
        orgs[1] = new ShipbuildingCompany();
        orgs[2] = new Factory();
        orgs[3] = new Library();

        for (int i = 0; i < orgs.Length; i++)
            orgs[i].RandomInit();

        Console.WriteLine("=== Viewing with non-virtual function (ShowNonVirtual) ===");
        foreach (var org in orgs)
            org.ShowNonVirtual();

        Console.WriteLine("\n=== Viewing with virtual function (Show) ===");
        foreach (var org in orgs)
            org.Show();
    }
}
