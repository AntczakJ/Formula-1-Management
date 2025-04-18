using f1management.TeamManagement;
using f1management.RaceManagement;
using f1management.Methods;
using System.IO;

namespace f1management;


class Program
{
    
    
    public static void ShowMenu()
    {
   
        Console.WriteLine("Menu");
        Console.WriteLine("1. Zaloguj się");
        Console.WriteLine("2. Dodaj");
        Console.WriteLine("3. Usun");
        Console.WriteLine("4. Wyświetl zespoły");
        Console.WriteLine("5. Wybierz zespół i wyświetl info");
        Console.WriteLine("6. Wybierz kierowce i wyświetl info");
        Console.WriteLine("7. Wybierz dyrektora i wyświtl info");
        Console.WriteLine("8. Wybierz wyscig i wyświetl info");
        Console.WriteLine("9. Wyloguj się");
        Console.WriteLine("10. Wyjdź");
        Console.Write("Wybierz opcję: ");
    
    }

    
    public static void Save(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
    {
        try
        {
            File.AppendAllText("Lista.txt", $"{x.FirstName} {x.LastName}\n");

            if (x is Driver d)
                d.AddDriver(x, drivers);
            else if (x is Principal p)
                p.AddPrincipal(x, principals);
            else if (x is Mechanics m)
                m.AddMechanic(x, mechanics);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void SaveRaces(Race race, List<Race> races)
    {
        race.AddRace(races);
    }

    public static List<Principal> Principals = new List<Principal>();
    public static List<Driver> Drivers = new List<Driver>();
    public static List<Mechanics> Mechanics = new List<Mechanics>();
    public static List<Race> Races = new List<Race>();
    public static void Main(string[] args)
    {
        // User.SaveInfo += Save;
        // Race.SaveRaceToList += SaveRaces;
        User.SaveInfo += (x, _, _, _) => Save(x, Drivers, Mechanics, Principals);
        Race.SaveRaceToList += (r, _) => SaveRaces(r, Races);
        
        
        Driver kier = new Driver("Imie", "Nazwisko");
        Console.WriteLine(kier.LastName);
        Mechanics mech = new Mechanics("mak", "lek");
        // kier.AddDriver(Drivers);
        foreach (Driver m in Drivers)
        {
            Console.WriteLine($"{m.FirstName}");
        }
        
        // Mechanics mek = new Mechanics("mek", "lek");
        Console.WriteLine("Zapisa");
        // foreach (Mechanics m in Mechanics)
        // {
        //     Console.WriteLine($"{m.FirstName}");
        // }
        // while (true)
        // {
        //     try
        //     {
        //         ShowMenu();
        //         var choice = int.Parse(Console.ReadLine());
        //
        //         switch (choice)
        //         {
        //             case 1:
        //                 
        //                 break;
        //             case 2:
        //                 
        //                 break;
        //             case 3:
        //                 
        //                 break;
        //             case 4:
        //                 
        //                 break;
        //             case 5:
        //                 
        //                 break;
        //             case 6:
        //                 
        //                 break;
        //             case 7:
        //                 
        //                 break;
        //             case 8:
        //                 
        //                 break;
        //             case 9:
        //                 return;
        //             default:
        //                 
        //                 break;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //     }
        // }
    }
}