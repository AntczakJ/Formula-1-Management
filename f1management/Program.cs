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

            string login = $"{x.FirstName}.{x.LastName}";
            PasswordManager.RemoveLogin(login);

            if (x is Driver d && drivers.Contains(d))
                drivers.Remove(d);
            else if (x is Mechanics m && mechanics.Contains(m))
                mechanics.Remove(m);
            else if (x is Principal p && principals.Contains(p))
                principals.Remove(p);
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
    

    // Fragment rozszerzonego ShowLoggedMenu
    


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
    public static List<Team> Teams = new List<Team>();
    

    public static List<Team> GetAllTeams()
    {
        return Teams;
    }
    
    public static Team GetTeamFromUser()
    {
        Console.Write("Podaj nazwę zespołu: ");
        string name = Console.ReadLine();
        return GetAllTeams().FirstOrDefault(t => t.TeamName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
    
   // Fragment rozszerzonego ShowLoggedMenu
    public static void ShowLoggedMenu(User user)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Zalogowano jako: {user.FirstName} {user.LastName} ({user.Role})");

            Console.WriteLine("1. Wyświetl zespoły");
            Console.WriteLine("2. Dodaj zespół");
            if (user.Role == Role.Principal)
            {
                Console.WriteLine("3. Zwiększ budżet zespołu");
                Console.WriteLine("4. Zmniejsz budżet zespołu");
            }
            Console.WriteLine("5. Wyświetl kierowcę");
            Console.WriteLine("6. Wyświetl szefa zespołu");
            Console.WriteLine("7. Wyświetl zespół");
            Console.WriteLine("8. Wyświetl mechanika");

            if (user.Role == Role.Principal)
            {
                Console.WriteLine("9. Dodaj kierowcę do zespołu");
                Console.WriteLine("10. Dodaj mechanika do zespołu");
                Console.WriteLine("11. Dodaj wyścig");
                Console.WriteLine("13. Dodaj nowego kierowcę");
                Console.WriteLine("14. Dodaj nowego mechanika");
                Console.WriteLine("15. Przypisz się do zespołu jako szef");
            }

            if (user.Role != Role.Mechanic)
            {
                Console.WriteLine("12. Wyświetl informacje o wyścigu");
            }

            Console.WriteLine("0. Wyloguj");
            Console.Write("Opcja: ");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        if (user.HasPermission(Permission.Read))
                        {
                            if (Teams.Count == 0)
                                Console.WriteLine("Brak zespołów.");
                            else
                                foreach (var t in Teams) t.DisplayInfo();
                        }
                        break;

                    case "2":
                        if (user.HasPermission(Permission.ManageTeam))
                        {
                            Console.Write("Nazwa zespołu: ");
                            string name = Console.ReadLine();
                            if (Teams.Any(t => t.TeamName == name))
                            {
                                Console.WriteLine("Zespół o tej nazwie już istnieje.");
                                break;
                            }
                            var newTeam = new Team(name, null, new List<Mechanics>(), new List<Driver>(), 0);
                            Teams.Add(newTeam);
                            Team.TriggerTeamLog(newTeam, $"Utworzono zespół {name}");
                        }
                        break;

                    case "3":
                    case "4":
                        if (user.Role != Role.Principal)
                        {
                            Console.WriteLine("Brak uprawnień do modyfikowania budżetu.");
                            break;
                        }

                        var team = GetTeamFromUser();
                        if (team == null)
                        {
                            Console.WriteLine("Zespół nie istnieje.");
                            break;
                        }
                        Console.Write("Kwota: ");
                        if (!float.TryParse(Console.ReadLine(), out float kwota))
                        {
                            Console.WriteLine("Niepoprawna kwota.");
                            break;
                        }
                        Console.Write("Powód: ");
                        string reason = Console.ReadLine();
                        if (option == "3")
                            team.IncreaseBudget(kwota, reason);
                        else
                            team.DecreaseBudget(kwota, reason);
                        break;

                    case "5":
                        if (Drivers.Count == 0) { Console.WriteLine("Brak kierowców."); break; }
                        for (int i = 0; i < Drivers.Count; i++)
                            Console.WriteLine($"{i + 1}. {Drivers[i].FirstName} {Drivers[i].LastName}");
                        Console.Write("Wybierz nr kierowcy: ");
                        if (int.TryParse(Console.ReadLine(), out int dIndex) && dIndex > 0 && dIndex <= Drivers.Count)
                            Drivers[dIndex - 1].DisplayInfo();
                        else
                            Console.WriteLine("Niepoprawny wybór.");
                        break;

                    case "6":
                        if (Principals.Count == 0) { Console.WriteLine("Brak szefów zespołów."); break; }
                        for (int i = 0; i < Principals.Count; i++)
                            Console.WriteLine($"{i + 1}. {Principals[i].FirstName} {Principals[i].LastName}");
                        Console.Write("Wybierz nr: ");
                        if (int.TryParse(Console.ReadLine(), out int pIndex) && pIndex > 0 && pIndex <= Principals.Count)
                            Principals[pIndex - 1].DisplayInfo();
                        else
                            Console.WriteLine("Niepoprawny wybór.");
                        break;

                    case "7":
                        if (Teams.Count == 0) { Console.WriteLine("Brak zespołów."); break; }
                        for (int i = 0; i < Teams.Count; i++)
                            Console.WriteLine($"{i + 1}. {Teams[i].TeamName}");
                        Console.Write("Wybierz nr zespołu: ");
                        if (int.TryParse(Console.ReadLine(), out int tIndex) && tIndex > 0 && tIndex <= Teams.Count)
                            Teams[tIndex - 1].DisplayInfo();
                        else
                            Console.WriteLine("Niepoprawny wybór.");
                        break;

                    case "8":
                        if (Mechanics.Count == 0) { Console.WriteLine("Brak mechaników."); break; }
                        for (int i = 0; i < Mechanics.Count; i++)
                            Console.WriteLine($"{i + 1}. {Mechanics[i].FirstName} {Mechanics[i].LastName}");
                        Console.Write("Wybierz nr: ");
                        if (int.TryParse(Console.ReadLine(), out int mIndex) && mIndex > 0 && mIndex <= Mechanics.Count)
                            Mechanics[mIndex - 1].DisplayInfo();
                        else
                            Console.WriteLine("Niepoprawny wybór.");
                        break;

                    case "9":
                        if (user.Role == Role.Principal)
                        {
                            var teamAddDriver = GetTeamFromUser();
                            if (teamAddDriver == null) { Console.WriteLine("Zespół nie istnieje."); break; }
                            if (Drivers.Count == 0) { Console.WriteLine("Brak dostępnych kierowców."); break; }
                            for (int i = 0; i < Drivers.Count; i++)
                                Console.WriteLine($"{i + 1}. {Drivers[i].FirstName} {Drivers[i].LastName}");
                            Console.Write("Wybierz nr kierowcy do dodania: ");
                            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Drivers.Count)
                                teamAddDriver.AddDriver(Drivers[index - 1]);
                            else
                                Console.WriteLine("Nieprawidłowy wybór.");
                        }
                        break;

                    case "10":
                        if (user.Role == Role.Principal)
                        {
                            var teamAddMech = GetTeamFromUser();
                            if (teamAddMech == null) { Console.WriteLine("Zespół nie istnieje."); break; }
                            if (Mechanics.Count == 0) { Console.WriteLine("Brak dostępnych mechaników."); break; }
                            for (int i = 0; i < Mechanics.Count; i++)
                                Console.WriteLine($"{i + 1}. {Mechanics[i].FirstName} {Mechanics[i].LastName}");
                            Console.Write("Wybierz nr mechanika do dodania: ");
                            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Mechanics.Count)
                                teamAddMech.AddMechanic(Mechanics[index - 1]);
                            else
                                Console.WriteLine("Nieprawidłowy wybór.");
                        }
                        break;

                    case "11":
                        if (user.Role == Role.Principal)
                        {
                            Console.Write("Nazwa wyścigu: ");
                            string raceName = Console.ReadLine();
                            Console.Write("Data (YYYY-MM-DD): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime raceDate))
                            {
                                var race = new Race(raceName, raceDate);
                                Races.Add(race);
                                Console.WriteLine("Wyścig dodany.");
                            }
                            else
                            {
                                Console.WriteLine("Niepoprawna data.");
                            }
                        }
                        break;

                    case "12":
                        if (user.Role != Role.Mechanic)
                        {
                            if (Races.Count == 0)
                            {
                                Console.WriteLine("Brak wyścigów.");
                                break;
                            }
                            for (int i = 0; i < Races.Count; i++)
                                Console.WriteLine($"{i + 1}. {Races[i].Name}");
                            Console.Write("Wybierz nr wyścigu: ");
                            if (int.TryParse(Console.ReadLine(), out int raceIndex) && raceIndex > 0 && raceIndex <= Races.Count)
                                Races[raceIndex - 1].DisplayInfo();
                            else
                                Console.WriteLine("Nieprawidłowy wybór.");
                        }
                        else
                        {
                            Console.WriteLine("Brak uprawnień do wyświetlania wyścigów.");
                        }
                        break;
                    case "13":
                        if (user.Role == Role.Principal)
                        {
                            Console.Write("Imię kierowcy: ");
                            string df = Console.ReadLine();
                            Console.Write("Nazwisko kierowcy: ");
                            string dl = Console.ReadLine();
                            Console.Write("Login: ");
                            string du = Console.ReadLine();
                            if (PasswordManager.DoesLoginExist(du))
                            {
                                Console.WriteLine("Użytkownik z tym loginem już istnieje.");
                                break;
                            }
                            Console.Write("Hasło: ");
                            string dp = Console.ReadLine();
                            PasswordManager.SavePassword(du, dp);
                            var newDriver = new Driver(df, dl, du);
                            Program.Drivers.Add(newDriver);
                            Console.WriteLine("Kierowca dodany.");
                        }
                        break;

                    case "14":
                        if (user.Role == Role.Principal)
                        {
                            Console.Write("Imię mechanika: ");
                            string mf = Console.ReadLine();
                            Console.Write("Nazwisko mechanika: ");
                            string ml = Console.ReadLine();
                            Console.Write("Login: ");
                            string mu = Console.ReadLine();
                            if (PasswordManager.DoesLoginExist(mu))
                            {
                                Console.WriteLine("Użytkownik z tym loginem już istnieje.");
                                break;
                            }
                            Console.Write("Hasło: ");
                            string mp = Console.ReadLine();
                            PasswordManager.SavePassword(mu, mp);
                            var newMech = new Mechanics(mf, ml, mu);
                            Program.Mechanics.Add(newMech);
                            Console.WriteLine("Mechanik dodany.");
                        }
                        break;

                    case "15":
                        if (user.Role == Role.Principal)
                        {
                            var targetTeam = GetTeamFromUser();
                            if (targetTeam == null)
                            {
                                Console.WriteLine("Zespół nie istnieje.");
                                break;
                            }
                            if (targetTeam.Principal != null)
                            {
                                Console.WriteLine("Ten zespół już ma przypisanego szefa.");
                                break;
                            }
                            targetTeam.AddPrincipal((Principal)user);
                        }
                        break;


                    case "0":
                        return;

                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("Naciśnij Enter, aby kontynuować...");
            Console.ReadLine();
        }
    }


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

                    if (PasswordManager.DoesLoginExist(username))
                    {
                        Console.WriteLine("Taki login już istnieje. Rejestracja przerwana.");
                        Console.ReadLine();
                        break;
                    }

                    Console.Write("Hasło: ");
                    string password = Console.ReadLine();

                    Console.WriteLine("Wybierz rolę: 1-Principal, 2-Mechanic, 3-Driver");
                    Role role = (Role)int.Parse(Console.ReadLine()) - 1;

                    PasswordManager.SavePassword(username, password);
                    switch (role)
                    {
                        case Role.Principal:
                            new Principal(fname, lname, username);
                            break;
                        case Role.Mechanic:
                            new Mechanics(fname, lname, username);
                            break;
                        case Role.Driver:
                            new Driver(fname, lname, username);
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

                    // Sprawdzenie hasła
                    if (!PasswordManager.VerifyPassword(username, password))
                    {
                        Console.WriteLine("Błędny login lub hasło. Enter aby kontynuować.");
                        Console.ReadLine();
                        break;
                    }

                    // Szukanie obiektu użytkownika po loginie (user.Username)
                    currentUser = null;

                    foreach (var d in Drivers)
                    {
                        if (d.Username == username)
                        {
                            currentUser = d;
                            break;
                        }
                    }

                    if (currentUser == null)
                    {
                        foreach (var m in Mechanics)
                        {
                            if (m.Username == username)
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
                            if (p.Username == username)
                            {
                                currentUser = p;
                                break;
                            }
                        }
                    }

                    if (currentUser == null)
                    {
                        Console.WriteLine("Użytkownik został znaleziony w pliku, ale nie istnieje obiekt w systemie.");
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