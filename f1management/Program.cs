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
            File.AppendAllText("Lista.txt",
                $"[DODANO] {x.Role}: {x.FirstName} {x.LastName} | {DateTime.Now}\n");

            if (x is Driver d && !drivers.Contains(d))
                drivers.Add(d);
            else if (x is Mechanics m && !mechanics.Contains(m))
                mechanics.Add(m);
            else if (x is Principal p && !principals.Contains(p))
                principals.Add(p);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    
    public static void Remove(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
    {
        try
        {
            File.AppendAllText("Lista.txt",
                $"[USUNIĘTO] {x.Role}: {x.FirstName} {x.LastName} | {DateTime.Now}\n");
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
    
    public static void RemoveRace(Race race, List<Race> races)
    {
        try
        {
            File.AppendAllText("Lista.txt",
                $"[USUNIĘTO] Race: {race.Name} | {DateTime.Now}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd przy zapisie do pliku: {ex.Message}");
        }
    }
    public static Team GetTeamFromUser()
    {
        Console.Write("Podaj nazwę zespołu: ");
        string name = Console.ReadLine();
        return GetAllTeams().FirstOrDefault(t => t.TeamName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static List<Team> GetAllTeams()
    {
        // Przykład tymczasowy – zaimplementuj prawdziwe przechowywanie zespołów jeśli chcesz
        return new List<Team>(); // Zamień na np. `public static List<Team> Teams = new();` i dodawaj do tej listy
    }
    public static void ShowLoggedMenu(User user)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Zalogowano jako: {user.FirstName} {user.LastName} ({user.Role})");

            Console.WriteLine("1. Wyświetl zespoły");
            Console.WriteLine("2. Dodaj zespół");
            Console.WriteLine("3. Zwiększ budżet zespołu");
            Console.WriteLine("4. Zmniejsz budżet zespołu");
            Console.WriteLine("5. Wyświetl dane kierowcy");
            Console.WriteLine("6. Wyświetl dane szefa zespołu");
            Console.WriteLine("0. Wyloguj");

            Console.Write("Opcja: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    if (user.HasPermission(Permission.Read))
                    {
                        Console.WriteLine("Zespoły:");
                        foreach (var team in GetAllTeams())
                            team.DisplayInfo();
                    }
                    break;

                case "2":
                    if (user.HasPermission(Permission.ManageTeam))
                    {
                        Console.Write("Nazwa zespołu: ");
                        string name = Console.ReadLine();
                        var team = new Team(name, null, new List<Mechanics>(), new List<Driver>(), 0);
                        Team.TriggerTeamLog(team, $"Utworzono zespół {name}");

                    }
                    break;

                case "3":
                    if (user.HasPermission(Permission.ManageTeam))
                    {
                        var team = GetTeamFromUser();
                        Console.Write("Kwota: ");
                        float kwota = float.Parse(Console.ReadLine());
                        Console.Write("Powód: ");
                        string reason = Console.ReadLine();
                        team?.IncreaseBudget(kwota, reason);
                    }
                    break;

                case "4":
                    if (user.HasPermission(Permission.ManageTeam))
                    {
                        var team = GetTeamFromUser();
                        Console.Write("Kwota: ");
                        float kwota = float.Parse(Console.ReadLine());
                        Console.Write("Powód: ");
                        string reason = Console.ReadLine();
                        team?.DecreaseBudget(kwota, reason);
                    }
                    break;

                case "5":
                    if (user.HasPermission(Permission.Read))
                    {
                        foreach (var driver in Drivers)
                            driver.DisplayInfo();
                    }
                    break;

                case "6":
                    if (user.HasPermission(Permission.Read))
                    {
                        foreach (var p in Principals)
                            p.DisplayInfo();
                    }
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }

            Console.WriteLine("Naciśnij Enter, aby kontynuować...");
            Console.ReadLine();
        }
    }

    public static void LogTeamAction(Team team, string action)
    {
        string logEntry = $"[{DateTime.Now}] [{team.TeamName}] {action}";
        Console.WriteLine(logEntry); // dla widoczności w konsoli
        File.AppendAllText("Lista.txt", logEntry + Environment.NewLine);
    }


    public static List<Principal> Principals = new List<Principal>();
    public static List<Driver> Drivers = new List<Driver>();
    public static List<Mechanics> Mechanics = new List<Mechanics>();
    public static List<Race> Races = new List<Race>();
    public static void Main(string[] args)
    {
        // 📌 Przenieś eventy TU
        User.SaveInfo += (x, _, _, _) => Save(x, Drivers, Mechanics, Principals);
        User.RemoveInfo += (x, d, m, p) => Remove(x, d, m, p);
        Team.TeamLog += LogTeamAction;
        Race.SaveRaceToList += (r, _) => SaveRaces(r, Races);

// Dopiero potem twórz użytkowników itp.
        User currentUser = null;
        string username = "";

        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Zarejestruj użytkownika");
            Console.WriteLine("2. Zaloguj się");
            Console.WriteLine("0. Wyjdź");
            Console.Write("Opcja: ");
            var mainChoice = Console.ReadLine();

            switch (mainChoice)
            {
                case "1":
                    Console.Write("Imię: ");
                    string fname = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lname = Console.ReadLine();
                    Console.Write("Login: ");
                    username = Console.ReadLine();
                    Console.Write("Hasło: ");
                    string password = Console.ReadLine();

                    Console.WriteLine("Wybierz rolę: 1-Principal, 2-Mechanic, 3-Driver");
                    Role role = (Role)int.Parse(Console.ReadLine()) - 1;

                    PasswordManager.SavePassword(username, password);
                    switch (role)
                    {
                        case Role.Principal:
                            Principals.Add(new Principal(fname, lname));
                            break;
                        case Role.Mechanic:
                            Mechanics.Add(new Mechanics(fname, lname));
                            break;
                        case Role.Driver:
                            Drivers.Add(new Driver(fname, lname));
                            break;
                    }

                    Console.WriteLine("Użytkownik zarejestrowany. Enter aby kontynuować.");
                    Console.ReadLine();
                    
                    Console.WriteLine("\n📋 Obecni użytkownicy w systemie:");

                    Console.WriteLine("\n▶ Szefowie zespołów:");
                    foreach (var p in Principals)
                    {
                        Console.WriteLine($"- {p.FirstName} {p.LastName}");
                    }

                    Console.WriteLine("\n▶ Mechanicy:");
                    foreach (var m in Mechanics)
                    {
                        Console.WriteLine($"- {m.FirstName} {m.LastName}");
                    }

                    Console.WriteLine("\n▶ Kierowcy:");
                    foreach (var d in Drivers)
                    {
                        Console.WriteLine($"- {d.FirstName} {d.LastName}");
                    }

                    Console.WriteLine("\nNaciśnij Enter, aby kontynuować...");
                    Console.ReadLine();

                    
                    break;

                case "2":
                    Console.Write("Login: ");
                    username = Console.ReadLine();
                    Console.Write("Hasło: ");
                    password = Console.ReadLine();

                    if (!PasswordManager.VerifyPassword(username, password))
                    {
                        Console.WriteLine("Błąd logowania. Enter aby kontynuować.");
                        Console.ReadLine();
                        break;
                    }

                    // Manualne szukanie użytkownika po imieniu i nazwisku (login = imie.nazwisko)
                    currentUser = null;

                    foreach (var d in Drivers)
                    {
                        if ($"{d.FirstName}.{d.LastName}".ToLower() == username.Trim().ToLower())
                        {
                            currentUser = d;
                            break;
                        }
                    }

                    if (currentUser == null)
                    {
                        foreach (var m in Mechanics)
                        {
                            if ($"{m.FirstName}.{m.LastName}".ToLower() == username.Trim().ToLower())
                            {
                                currentUser = m;
                                break;
                            }
                        }
                    }

                    if (currentUser == null)
                    {
                        foreach (var p in Principals)
                        {
                            if ($"{p.FirstName}.{p.LastName}".ToLower() == username.Trim().ToLower())
                            {
                                currentUser = p;
                                break;
                            }
                        }
                    }

                    if (currentUser == null)
                    {
                        Console.WriteLine("Nie znaleziono użytkownika.");
                        Console.ReadLine();
                        break;
                    }

                    Console.WriteLine($"Zalogowano jako {currentUser.FirstName} {currentUser.LastName} ({currentUser.Role})");
                    ShowLoggedMenu(currentUser);
                    break;


                case "0":
                    return;

                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }
    }
}