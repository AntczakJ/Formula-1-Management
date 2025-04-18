using f1management.TeamManagement;
using f1management.RaceManagement;
using f1management.Methods;
using System.IO;

namespace f1management;

class Program
{
    // Lista przechowująca obiekty typu Principal
    public static List<Principal> Principals = new List<Principal>();
    // Lista przechowująca obiekty typu Driver
    public static List<Driver> Drivers = new List<Driver>();
    // Lista przechowująca obiekty typu Mechanics
    public static List<Mechanics> Mechanics = new List<Mechanics>();
    // Lista przechowująca obiekty typu Race
    public static List<Race> Races = new List<Race>();
    // Lista przechowująca obiekty typu Team
    public static List<Team> Teams = new List<Team>();

    // Metoda zapisująca użytkownika (kierowcę, mechanika, menedżera) do odpowiedniej listy oraz do pliku
    public static void Save(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
    {
        try
        {
            // Dodanie informacji o dodaniu użytkownika do pliku "Lista.txt"
            File.AppendAllText("Lista.txt", $"[DODANO] {x.Role}: {x.FirstName} {x.LastName} | {DateTime.Now}\n");
            
            // Sprawdzenie, do jakiej listy dodać użytkownika i dodanie go, jeśli nie ma go w tej liście
            if (x is Driver d && !drivers.Contains(d)) drivers.Add(d);
            else if (x is Mechanics m && !mechanics.Contains(m)) mechanics.Add(m);
            else if (x is Principal p && !principals.Contains(p)) principals.Add(p);
        }
        catch (Exception ex)
        {
            // Obsługa błędów i wyświetlanie komunikatu
            Console.WriteLine(ex.Message);
        }
    }

    // Metoda usuwająca użytkownika z odpowiedniej listy i zapisująca tę operację do pliku
    public static void Remove(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
    {
        try
        {
            // Dodanie informacji o usunięciu użytkownika do pliku "Lista.txt"
            File.AppendAllText("Lista.txt", $"[USUNIĘTO] {x.Role}: {x.FirstName} {x.LastName} | {DateTime.Now}\n");
            
            // Usunięcie loginu użytkownika z systemu
            PasswordManager.RemoveLogin($"{x.FirstName}.{x.LastName}");
            
            // Sprawdzenie, z jakiej listy usunąć użytkownika
            if (x is Driver d && drivers.Contains(d)) drivers.Remove(d);
            else if (x is Mechanics m && mechanics.Contains(m)) mechanics.Remove(m);
            else if (x is Principal p && principals.Contains(p)) principals.Remove(p);
        }
        catch (Exception ex)
        {
            // Obsługa błędów i wyświetlanie komunikatu
            Console.WriteLine(ex.Message);
        }
    }

    // Metoda zapisująca wyścig do listy wyścigów
    public static void SaveRaces(Race race, List<Race> races) => race.AddRace(races);

    // Metoda usuwająca wyścig z listy wyścigów i zapisująca tę operację do pliku
    public static void RemoveRace(Race race, List<Race> races)
    {
        try
        {
            // Dodanie informacji o usunięciu wyścigu do pliku "Lista.txt"
            File.AppendAllText("Lista.txt", $"[USUNIĘTO] Race: {race.Name} | {DateTime.Now}\n");
        }
        catch (Exception ex)
        {
            // Obsługa błędów i wyświetlanie komunikatu
            Console.WriteLine($"Błąd przy zapisie do pliku: {ex.Message}");
        }
    }

    // Metoda logująca akcję zespołu i zapisująca ją do pliku
    public static void LogTeamAction(Team team, string action)
    {
        // Tworzenie wpisu logu z bieżącą datą i godziną
        string logEntry = $"[{DateTime.Now}] [{team.TeamName}] {action}";
        Console.WriteLine(logEntry);
        // Zapisanie wpisu logu do pliku "Lista.txt"
        File.AppendAllText("Lista.txt", logEntry + Environment.NewLine);
    }

    // Metoda pozwalająca użytkownikowi podać nazwę zespołu i zwrócić odpowiedni obiekt Team
    public static Team GetTeamFromUser()
    {
        // Prośba o podanie nazwy zespołu
        Console.Write("Podaj nazwę zespołu: ");
        string name = Console.ReadLine();
        // Znalezienie zespołu na podstawie nazwy (ignorując wielkość liter)
        return Teams.FirstOrDefault(t => t.TeamName.Equals(name, StringComparison.OrdinalIgnoreCase));
    }



   public static void ShowLoggedMenu(User user)
{
    // Zmienna kontrolująca, czy menu powinno być wyświetlane dalej
    bool continueMenu = true;

    // Pętla do wyświetlania menu, dopóki użytkownik nie wybierze opcji wylogowania
    while (continueMenu)
    {
        // Czyszczenie ekranu i wyświetlanie informacji o zalogowanym użytkowniku
        Console.Clear();
        Console.WriteLine($"Zalogowano jako: {user.FirstName} {user.LastName} ({user.Role})");
        
        // Wyświetlanie opcji menu
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

        // Prośba o wybór opcji
        Console.Write("Opcja: ");
        string option = Console.ReadLine();

        try
        {
            // Czyszczenie ekranu po wybraniu opcji
            Console.Clear();
            
            // Obsługa różnych opcji menu
            switch (option)
            {
                case "1":
                    // Wyświetlenie informacji o zespole
                    var team = GetTeamFromUser();
                    if (team == null) Console.WriteLine("Nie znaleziono zespołu.");
                    else team.DisplayInfo();
                    break;

                case "2":
                    // Dodanie nowego zespołu, tylko dla szefa
                    if (user.Role != Role.Principal) { Console.WriteLine("Brak uprawnień."); break; }
                    Console.Write("Nazwa zespołu: ");
                    string name = Console.ReadLine();
                    if (Teams.Any(t => t.TeamName == name)) { Console.WriteLine("Zespół już istnieje."); break; }
                    Teams.Add(new Team(name, null, new List<Mechanics>(), new List<Driver>(), 0));
                    Console.WriteLine("Zespół dodany.");
                    break;

                case "3":
                case "4":
                    // Zwiększanie lub zmniejszanie budżetu zespołu, tylko dla szefa
                    if (user.Role != Role.Principal) { Console.WriteLine("Brak uprawnień."); break; }
                    var t = GetTeamFromUser();
                    if (t == null) { Console.WriteLine("Nie znaleziono zespołu."); break; }
                    Console.Write("Kwota: ");
                    if (!float.TryParse(Console.ReadLine(), out float amount)) { Console.WriteLine("Niepoprawna kwota."); break; }
                    Console.Write("Powód: ");
                    string reason = Console.ReadLine();
                    if (option == "3") t.IncreaseBudget(amount, reason);
                    else t.DecreaseBudget(amount, reason);
                    break;

                case "5":
                    // Wyświetlanie listy kierowców
                    if (Drivers.Count == 0) { Console.WriteLine("Brak kierowców."); break; }
                    for (int i = 0; i < Drivers.Count; i++)
                        Console.WriteLine($"{i + 1}. {Drivers[i].FirstName} {Drivers[i].LastName}");
                    Console.Write("Wybierz nr: ");
                    if (int.TryParse(Console.ReadLine(), out int dIndex) && dIndex > 0 && dIndex <= Drivers.Count)
                        Drivers[dIndex - 1].DisplayInfo();
                    break;

                case "6":
                    // Wyświetlanie listy szefów zespołów
                    if (Principals.Count == 0) { Console.WriteLine("Brak szefów."); break; }
                    for (int i = 0; i < Principals.Count; i++)
                        Console.WriteLine($"{i + 1}. {Principals[i].FirstName} {Principals[i].LastName}");
                    Console.Write("Wybierz nr: ");
                    if (int.TryParse(Console.ReadLine(), out int pIndex) && pIndex > 0 && pIndex <= Principals.Count)
                        Principals[pIndex - 1].DisplayInfo();
                    break;

                case "7":
                    // Wyświetlanie listy mechaników
                    if (Mechanics.Count == 0) { Console.WriteLine("Brak mechaników."); break; }
                    for (int i = 0; i < Mechanics.Count; i++)
                        Console.WriteLine($"{i + 1}. {Mechanics[i].FirstName} {Mechanics[i].LastName}");
                    Console.Write("Wybierz nr: ");
                    if (int.TryParse(Console.ReadLine(), out int mIndex) && mIndex > 0 && mIndex <= Mechanics.Count)
                        Mechanics[mIndex - 1].DisplayInfo();
                    break;

                case "8":
                    // Dodanie kierowcy do zespołu, tylko dla szefa
                    if (user.Role != Role.Principal) break;
                    var t8 = GetTeamFromUser();
                    if (t8 == null || Drivers.Count == 0) break;
                    for (int i = 0; i < Drivers.Count; i++)
                        Console.WriteLine($"{i + 1}. {Drivers[i].FirstName} {Drivers[i].LastName}");
                    Console.Write("Nr kierowcy: ");
                    if (int.TryParse(Console.ReadLine(), out int dAdd) && dAdd > 0 && dAdd <= Drivers.Count)
                        t8.AddDriver(Drivers[dAdd - 1]);
                    break;

                case "9":
                    // Dodanie mechanika do zespołu, tylko dla szefa
                    if (user.Role != Role.Principal) break;
                    var t9 = GetTeamFromUser();
                    if (t9 == null || Mechanics.Count == 0) break;
                    for (int i = 0; i < Mechanics.Count; i++)
                        Console.WriteLine($"{i + 1}. {Mechanics[i].FirstName} {Mechanics[i].LastName}");
                    Console.Write("Nr mechanika: ");
                    if (int.TryParse(Console.ReadLine(), out int mAdd) && mAdd > 0 && mAdd <= Mechanics.Count)
                        t9.AddMechanic(Mechanics[mAdd - 1]);
                    break;

                case "10":
                    // Dodanie wyścigu, tylko dla szefa
                    if (user.Role != Role.Principal) break;
                    Console.Write("Nazwa wyścigu: ");
                    string raceName = Console.ReadLine();
                    Console.Write("Data (YYYY-MM-DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime raceDate))
                    {
                        var race = new Race(raceName, raceDate);
                    }
                    break;

                case "11":
                    // Dodanie nowego kierowcy, tylko dla szefa
                    if (user.Role != Role.Principal) break;
                    Console.Write("Imię kierowcy: ");
                    string df = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string dl = Console.ReadLine();
                    Console.Write("Login: ");
                    string du = Console.ReadLine();
                    if (PasswordManager.DoesLoginExist(du)) { Console.WriteLine("Login zajęty."); break; }
                    Console.Write("Hasło: ");
                    string dp = Console.ReadLine();
                    PasswordManager.SavePassword(du, dp);
                    new Driver(df, dl, du);
                    Console.WriteLine("Kierowca dodany.");
                    break;

                case "12":
                    // Dodanie nowego mechanika, tylko dla szefa
                    if (user.Role != Role.Principal) break;
                    Console.Write("Imię mechanika: ");
                    string mf = Console.ReadLine();
                    Console.Write("Nazwisko: ");
                    string ml = Console.ReadLine();
                    Console.Write("Login: ");
                    string mu = Console.ReadLine();
                    if (PasswordManager.DoesLoginExist(mu)) { Console.WriteLine("Login zajęty."); break; }
                    Console.Write("Hasło: ");
                    string mp = Console.ReadLine();
                    PasswordManager.SavePassword(mu, mp);
                    new Mechanics(mf, ml, mu);
                    Console.WriteLine("Mechanik dodany.");
                    break;

                case "13":
                    // Przypisanie użytkownika do zespołu jako szef, tylko dla szefa
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

                case "14":
                    // Wyświetlenie informacji o wyścigu, użytkownik musi być nie mechanikiem
                    if (user.Role == Role.Mechanic) break;
                    if (Races.Count == 0) { Console.WriteLine("Brak wyścigów."); break; }
                    for (int i = 0; i < Races.Count; i++)
                        Console.WriteLine($"{i + 1}. {Races[i].Name}");
                    Console.Write("Wybierz nr: ");
                    if (int.TryParse(Console.ReadLine(), out int rIndex) && rIndex > 0 && rIndex <= Races.Count)
                        Races[rIndex - 1].DisplayInfo();
                    break;

                case "0":
                    // Wylogowanie użytkownika
                    continueMenu = false; // Zmieniamy zmienną kontrolującą pętlę, aby wyjść z menu
                    break;

                default:
                    // Obsługa nieznanej opcji
                    Console.WriteLine("Nieznana opcja.");
                    break;
            }
        }
        catch (Exception ex)
        {
            // Obsługa błędów
            Console.WriteLine($"Błąd: {ex.Message}");
        }

        // Jeśli menu ma być kontynuowane, wyświetlamy komunikat
        if (continueMenu)
        {
            Console.WriteLine("Naciśnij Enter, aby kontynuować...");
            Console.ReadLine();
        }
    }
}





    public static void Main(string[] args)
    {
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
                    string roleInput = Console.ReadLine();
                    if (!int.TryParse(roleInput, out int roleNumber) || roleNumber < 1 || roleNumber > 3)
                    {
                        Console.WriteLine("Niepoprawna rola. Rejestracja przerwana.");
                        Console.ReadLine();
                        break;
                    }
                    Role role = (Role)(roleNumber - 1);


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