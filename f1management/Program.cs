using f1management.TeamManagement;
using f1management.RaceManagement;
using f1management.Methods;
using System.IO;

namespace f1management;

class Program
{
    // Globalne listy przechowujące obiekty różnych ról i elementów systemu
    public static List<Principal> Principals = new List<Principal>();
    public static List<Driver> Drivers = new List<Driver>();
    public static List<Mechanics> Mechanics = new List<Mechanics>();
    public static List<Race> Races = new List<Race>();
    public static List<Team> Teams = new List<Team>();

    // Zapisuje użytkownika i dodaje go do odpowiedniej listy, jeśli jeszcze tam nie istnieje
    public static void Save(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
    {
        try
        {
            File.AppendAllText("Lista.txt", $"[DODANO] {x.Role}: {x.FirstName} {x.LastName} | {DateTime.Now}\n");
            if (x is Driver d && !drivers.Contains(d)) drivers.Add(d);
            else if (x is Mechanics m && !mechanics.Contains(m)) mechanics.Add(m);
            else if (x is Principal p && !principals.Contains(p)) principals.Add(p);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Usuwa użytkownika z listy oraz loguje zdarzenie i usuwa jego login
    public static void Remove(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
    {
        try
        {
            File.AppendAllText("Lista.txt", $"[USUNIĘTO] {x.Role}: {x.FirstName} {x.LastName} | {DateTime.Now}\n");
            PasswordManager.RemoveLogin($"{x.FirstName}.{x.LastName}");
            if (x is Driver d && drivers.Contains(d)) drivers.Remove(d);
            else if (x is Mechanics m && mechanics.Contains(m)) mechanics.Remove(m);
            else if (x is Principal p && principals.Contains(p)) principals.Remove(p);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Dodaje wyścig do listy
    public static void SaveRaces(Race race, List<Race> races) => race.AddRace(races);

    // Usuwa wyścig z listy i zapisuje to do pliku
    public static void RemoveRace(Race race, List<Race> races)
    {
        try
        {
            File.AppendAllText("Lista.txt", $"[USUNIĘTO] Race: {race.Name} | {DateTime.Now}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd przy zapisie do pliku: {ex.Message}");
        }
    }

    // Loguje akcję zespołu do pliku i konsoli
    public static void LogTeamAction(Team team, string action)
    {
        string logEntry = $"[{DateTime.Now}] [{team.TeamName}] {action}";
        Console.WriteLine(logEntry);
        File.AppendAllText("Lista.txt", logEntry + Environment.NewLine);
    }

    // Pobiera zespół po nazwie od użytkownika
    public static Team GetTeamFromUser()
    {
        Console.Write("Podaj nazwę zespołu: ");
        string name = Console.ReadLine();
        return Teams.FirstOrDefault(t => t.TeamName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // Główne menu po zalogowaniu użytkownika
    public static void ShowLoggedMenu(User user)
    {
        bool continueMenu = true;
        while (continueMenu)
        {
            Console.Clear();
            Console.WriteLine($"Zalogowano jako: {user.FirstName} {user.LastName} ({user.Role})");
            Console.WriteLine("1. Wyświetl zespół");
            Console.WriteLine("2. Dodaj zespół");
            Console.WriteLine("3. Zwiększ budżet zespołu");
            Console.WriteLine("4. Zmniejsz budżet zespołu");
            Console.WriteLine("5. Wyświetl kierowcę");
            Console.WriteLine("6. Wyświetl szefa zespołu");
            Console.WriteLine("7. Wyświetl mechanika");
            Console.WriteLine("8. Dodaj kierowcę do zespołu");
            Console.WriteLine("9. Dodaj mechanika do zespołu");
            Console.WriteLine("10. Dodaj wyścig");
            Console.WriteLine("11. Dodaj nowego kierowcę");
            Console.WriteLine("12. Dodaj nowego mechanika");
            Console.WriteLine("13. Przypisz się do zespołu jako szef");
            Console.WriteLine("14. Wyświetl informacje o wyścigu");
            Console.WriteLine("0. Wyloguj");
            Console.Write("Opcja: ");
            string option = Console.ReadLine();

            try
            {
                Console.Clear();
                switch (option)
                {
                    // Wyświetlanie zespołu
                    case "1":
                        var team = GetTeamFromUser();
                        if (team == null) Console.WriteLine("Nie znaleziono zespołu.");
                        else team.DisplayInfo();
                        break;

                    // Dodawanie nowego zespołu (tylko dla szefa)
                    case "2":
                        if (user.Role != Role.Principal)
                        {
                            Console.WriteLine("Brak uprawnień.");
                            break;
                        }

                        Console.Write("Nazwa zespołu: ");
                        string name = Console.ReadLine();
                        if (Teams.Any(t => t.TeamName == name))
                        {
                            Console.WriteLine("Zespół już istnieje.");
                            break;
                        }

                        Teams.Add(new Team(name, null, new List<Mechanics>(), new List<Driver>(), 0));
                        Console.WriteLine("Zespół dodany.");
                        break;

                    // Zwiększanie lub zmniejszanie budżetu zespołu
                    case "3":
                    case "4":
                        if (user.Role != Role.Principal)
                        {
                            Console.WriteLine("Brak uprawnień.");
                            break;
                        }

                        var t = GetTeamFromUser();
                        if (t == null)
                        {
                            Console.WriteLine("Nie znaleziono zespołu.");
                            break;
                        }

                        Console.Write("Kwota: ");
                        if (!float.TryParse(Console.ReadLine(), out float amount))
                        {
                            Console.WriteLine("Niepoprawna kwota.");
                            break;
                        }

                        Console.Write("Powód: ");
                        string reason = Console.ReadLine();
                        if (option == "3") t.IncreaseBudget(amount, reason);
                        else t.DecreaseBudget(amount, reason);
                        break;

                    // Wyświetlanie kierowców
                    case "5":
                        if (Drivers.Count == 0)
                        {
                            Console.WriteLine("Brak kierowców.");
                            break;
                        }

                        for (int i = 0; i < Drivers.Count; i++)
                            Console.WriteLine($"{i + 1}. {Drivers[i].FirstName} {Drivers[i].LastName}");
                        Console.Write("Wybierz nr: ");
                        if (int.TryParse(Console.ReadLine(), out int dIndex) && dIndex > 0 && dIndex <= Drivers.Count)
                            Drivers[dIndex - 1].DisplayInfo();
                        break;

                    // Wyświetlanie szefów
                    case "6":
                        if (Principals.Count == 0)
                        {
                            Console.WriteLine("Brak szefów.");
                            break;
                        }

                        for (int i = 0; i < Principals.Count; i++)
                            Console.WriteLine($"{i + 1}. {Principals[i].FirstName} {Principals[i].LastName}");
                        Console.Write("Wybierz nr: ");
                        if (int.TryParse(Console.ReadLine(), out int pIndex) && pIndex > 0 &&
                            pIndex <= Principals.Count)
                            Principals[pIndex - 1].DisplayInfo();
                        break;

                    // Wyświetlanie mechaników
                    case "7":
                        if (Mechanics.Count == 0)
                        {
                            Console.WriteLine("Brak mechaników.");
                            break;
                        }

                        for (int i = 0; i < Mechanics.Count; i++)
                            Console.WriteLine($"{i + 1}. {Mechanics[i].FirstName} {Mechanics[i].LastName}");
                        Console.Write("Wybierz nr: ");
                        if (int.TryParse(Console.ReadLine(), out int mIndex) && mIndex > 0 && mIndex <= Mechanics.Count)
                            Mechanics[mIndex - 1].DisplayInfo();
                        break;

                    // Dodawanie kierowcy do zespołu
                    case "8":
                        if (user.Role != Role.Principal) break;
                        var t8 = GetTeamFromUser();
                        if (t8 == null || Drivers.Count == 0) break;
                        for (int i = 0; i < Drivers.Count; i++)
                            Console.WriteLine($"{i + 1}. {Drivers[i].FirstName} {Drivers[i].LastName}");
                        Console.Write("Nr kierowcy: ");
                        if (int.TryParse(Console.ReadLine(), out int dAdd) && dAdd > 0 && dAdd <= Drivers.Count)
                            t8.AddDriver(Drivers[dAdd - 1]);
                        break;

                    // Dodawanie mechanika do zespołu
                    case "9":
                        if (user.Role != Role.Principal) break;
                        var t9 = GetTeamFromUser();
                        if (t9 == null || Mechanics.Count == 0) break;
                        for (int i = 0; i < Mechanics.Count; i++)
                            Console.WriteLine($"{i + 1}. {Mechanics[i].FirstName} {Mechanics[i].LastName}");
                        Console.Write("Nr mechanika: ");
                        if (int.TryParse(Console.ReadLine(), out int mAdd) && mAdd > 0 && mAdd <= Mechanics.Count)
                            t9.AddMechanic(Mechanics[mAdd - 1]);
                        break;

                    // Dodanie nowego wyścigu
                    case "10":
                        if (user.Role != Role.Principal) break;
                        Console.Write("Nazwa wyścigu: ");
                        string raceName = Console.ReadLine();
                        Console.Write("Data (YYYY-MM-DD): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime raceDate))
                        {
                            var race = new Race(raceName, raceDate);
                        }

                        break;

                    // Dodawanie nowego kierowcy z rejestracją loginu i hasła
                    case "11":
                        if (user.Role != Role.Principal) break;
                        Console.Write("Imię kierowcy: ");
                        string df = Console.ReadLine();
                        Console.Write("Nazwisko: ");
                        string dl = Console.ReadLine();
                        Console.Write("Login: ");
                        string du = Console.ReadLine();
                        if (PasswordManager.DoesLoginExist(du))
                        {
                            Console.WriteLine("Login zajęty.");
                            break;
                        }

                        Console.Write("Hasło: ");
                        string dp = Console.ReadLine();
                        PasswordManager.SavePassword(du, dp);
                        new Driver(df, dl, du);
                        Console.WriteLine("Kierowca dodany.");
                        break;

                    // Dodawanie mechanika
                    case "12":
                        if (user.Role != Role.Principal) break;
                        Console.Write("Imię mechanika: ");
                        string mf = Console.ReadLine();
                        Console.Write("Nazwisko: ");
                        string ml = Console.ReadLine();
                        Console.Write("Login: ");
                        string mu = Console.ReadLine();
                        if (PasswordManager.DoesLoginExist(mu))
                        {
                            Console.WriteLine("Login zajęty.");
                            break;
                        }

                        Console.Write("Hasło: ");
                        string mp = Console.ReadLine();
                        PasswordManager.SavePassword(mu, mp);
                        new Mechanics(mf, ml, mu);
                        Console.WriteLine("Mechanik dodany.");
                        break;

                    // Przypisanie siebie jako szefa zespołu
                    case "13":
                        if (user.Role != Role.Principal) break;
                        var team13 = GetTeamFromUser();
                        if (team13 == null)
                        {
                            Console.WriteLine("Zespół nie istnieje.");
                            break;
                        }

                        if (team13.Principal != null)
                        {
                            Console.WriteLine("Zespół ma już przypisanego szefa.");
                            break;
                        }

                        team13.AddPrincipal((Principal)user);
                        break;

                    // Wyświetlenie informacji o wyścigu
                    case "14":
                        if (user.Role == Role.Mechanic) break;
                        if (Races.Count == 0)
                        {
                            Console.WriteLine("Brak wyścigów.");
                            break;
                        }

                        for (int i = 0; i < Races.Count; i++)
                            Console.WriteLine($"{i + 1}. {Races[i].Name}");
                        Console.Write("Wybierz nr: ");
                        if (int.TryParse(Console.ReadLine(), out int rIndex) && rIndex > 0 && rIndex <= Races.Count)
                            Races[rIndex - 1].DisplayInfo();
                        break;

                    // Wylogowanie użytkownika
                    case "0":
                        continueMenu = false;
                        break;

                    // Obsługa nieznanej opcji
                    default:
                        Console.WriteLine("Nieznana opcja.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            if (continueMenu)
            {
                Console.WriteLine("Naciśnij Enter, aby kontynuować...");
                Console.ReadLine();
            }
        }
    }







    public static void Main(string[] args)
    {
        // Subskrypcja zdarzeń. Kiedy zachodzą zdarzenia w systemie, wywoływane są odpowiednie metody
        User.SaveInfo += (x, _, _, _) => Save(x, Drivers, Mechanics, Principals); // Zapisuje użytkownika
        User.RemoveInfo += (x, d, m, p) => Remove(x, d, m, p); // Usuwa użytkownika
        Team.TeamLog += LogTeamAction; // Logowanie działań związanych z zespołem
        Race.SaveRaceToList += (r, _) => SaveRaces(r, Races); // Zapisuje wyścig do listy

        // Inicjalizacja zmiennych
        User currentUser = null; // Zmienna przechowująca aktualnie zalogowanego użytkownika
        string username = ""; // Zmienna przechowująca login użytkownika

        // Pętla główna programu (menu)
        while (true)
        {
            Console.Clear(); // Czyści ekran
            // Wyświetla główne menu
            Console.WriteLine("1. Zarejestruj użytkownika");
            Console.WriteLine("2. Zaloguj się");
            Console.WriteLine("0. Wyjdź");
            Console.Write("Opcja: ");
            var mainChoice = Console.ReadLine(); // Pobiera wybór użytkownika

            switch (mainChoice)
            {
                // Rejestracja nowego użytkownika
                case "1":
                    // Pobieranie danych użytkownika
                    Console.Write("Imię: ");
                    string fname = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string lname = Console.ReadLine();
                    Console.Write("Login: ");
                    username = Console.ReadLine();

                    // Sprawdzanie, czy login już istnieje
                    if (PasswordManager.DoesLoginExist(username))
                    {
                        Console.WriteLine("Taki login już istnieje. Rejestracja przerwana.");
                        Console.ReadLine();
                        break;
                    }

                    Console.Write("Hasło: ");
                    string password = Console.ReadLine();

                    // Wybór roli użytkownika
                    Console.WriteLine("Wybierz rolę: 1-Principal, 2-Mechanic, 3-Driver");
                    string roleInput = Console.ReadLine();

                    // Sprawdzanie poprawności roli
                    if (!int.TryParse(roleInput, out int roleNumber) || roleNumber < 1 || roleNumber > 3)
                    {
                        Console.WriteLine("Niepoprawna rola. Rejestracja przerwana.");
                        Console.ReadLine();
                        break;
                    }

                    Role role = (Role)(roleNumber - 1); // Ustawienie roli na podstawie wyboru

                    // Zapisanie hasła
                    PasswordManager.SavePassword(username, password);

                    // Tworzenie użytkownika na podstawie roli
                    switch (role)
                    {
                        case Role.Principal:
                            new Principal(fname, lname, username); // Tworzenie szefa zespołu
                            break;
                        case Role.Mechanic:
                            new Mechanics(fname, lname, username); // Tworzenie mechanika
                            break;
                        case Role.Driver:
                            new Driver(fname, lname, username); // Tworzenie kierowcy
                            break;
                    }

                    // Potwierdzenie rejestracji
                    Console.WriteLine("Użytkownik zarejestrowany. Enter aby kontynuować.");
                    Console.ReadLine();
                    break;

                // Logowanie istniejącego użytkownika
                case "2":
                    // Pobieranie danych logowania
                    Console.Write("Login: ");
                    username = Console.ReadLine();
                    Console.Write("Hasło: ");
                    password = Console.ReadLine();

                    // Sprawdzenie poprawności hasła
                    if (!PasswordManager.VerifyPassword(username, password))
                    {
                        Console.WriteLine("Błędny login lub hasło. Enter aby kontynuować.");
                        Console.ReadLine();
                        break;
                    }

                    // Szukanie obiektu użytkownika po loginie (sprawdzanie wszystkich ról)
                    currentUser = null; // Inicjalizacja zmiennej
                    foreach (var d in Drivers)
                    {
                        if (d.Username == username)
                        {
                            currentUser = d; // Jeśli znaleziono kierowcę, ustawiamy go jako aktualnego użytkownika
                            break;
                        }
                    }

                    if (currentUser == null)
                    {
                        foreach (var m in Mechanics)
                        {
                            if (m.Username == username)
                            {
                                currentUser = m; // Jeśli znaleziono mechanika, ustawiamy go jako aktualnego użytkownika
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
                                currentUser = p; // Jeśli znaleziono szefa, ustawiamy go jako aktualnego użytkownika
                                break;
                            }
                        }
                    }

                    // Jeżeli użytkownik nie istnieje w systemie
                    if (currentUser == null)
                    {
                        Console.WriteLine("Użytkownik został znaleziony w pliku, ale nie istnieje obiekt w systemie.");
                        Console.ReadLine();
                        break;
                    }

                    // Po pomyślnym zalogowaniu, wyświetla menu użytkownika
                    Console.WriteLine(
                        $"Zalogowano jako {currentUser.FirstName} {currentUser.LastName} ({currentUser.Role})");
                    ShowLoggedMenu(currentUser); // Wywołanie menu użytkownika
                    break;

                // Opcja wyjścia z programu
                case "0":
                    return; // Kończy działanie programu

                // Obsługa niepoprawnego wyboru
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }
    }
}
