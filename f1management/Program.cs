using f1management.TeamManagement;
using f1management.RaceManagement;
using f1management.Methods;

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

    public static void Save(User x)
    {
        try
        {
            File.AppendAllText("Lista.txt", $"{x.FirstName} {x.LastName}\n");
            
            // if (x is Driver d)
            //     d.AddDriver(drivers);
            // else if (x is Principal p)
            //     p.AddPrincipal(principals);
            // else if (x is Mechanics m)
            //     m.AddMechanic(mechanics);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }

    
    static void Main(string[] args)
    {
        User.SaveInfo += Save;
        List<Principal> principals = new List<Principal>();
        List<Driver> drivers = new List<Driver>();
        List<Mechanics> mechanics = new List<Mechanics>();
        List<Race> races = new List<Race>();
        
        
        Driver mehanik = new Driver("Imie", "Nazwisko");
        Mechanics mek = new Mechanics("mek", "lek");
        // File.AppendAllText("Listal.txt", $"Nice");
        Console.WriteLine("Zapisa");
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