// Import przestrzeni nazw (TeamManagement zawiera np. klasy związane z zespołami F1)
using f1management.TeamManagement;
using System;

namespace f1management.Methods
{
    // Enum określający role użytkowników w systemie
    public enum Role
    {
        Principal,  // Szef zespołu
        Mechanic,   // Mechanik
        Driver      // Kierowca
    }

    // Enum reprezentujący poziomy uprawnień
    public enum Permission
    {
        ManageTracks,  // Zarządzanie torami
        Read,          // Odczyt danych
        ManageTeam     // Zarządzanie zespołem
    }

    public interface Information
    {
        void DisplayInfo();
    }

    // Klasa reprezentująca użytkownika systemu
    public class User
    {
        // Definicja delegata (podpis metody) do zapisu użytkownika do pliku
        public delegate void SaveToFile(User x);

        // Event, który można wywołać by zapisać użytkownika
        public static event SaveToFile SaveInfo;

        // Właściwości użytkownika: imię, nazwisko, rola, uprawnienia
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public List<Permission> Permissions { get; set; }

        // Konstruktor – tworzy użytkownika i przypisuje mu odpowiednie uprawnienia na podstawie roli
        public User(string firstname, string lastname, Role role)
        {
            FirstName = firstname;
            LastName = lastname;
            Role = role;

            // Pobranie uprawnień z klasy RBAC
            Permissions = RBAC.GetPermissions(role);

            // Wywołanie zdarzenia – np. zapis do pliku
            SaveInfo?.Invoke(this);
        }

        // Sprawdzenie, czy użytkownik ma konkretne uprawnienie
        public bool HasPermission(Permission permission)
        {
            return Permissions.Contains(permission);
        }
    }

    // Statyczna klasa odpowiedzialna za Role-Based Access Control (RBAC)
    public static class RBAC
    {
        // Słownik przypisujący role do listy uprawnień
        private static readonly Dictionary<Role, List<Permission>> _rolePermissions = new Dictionary<Role, List<Permission>>
        {
            { Role.Principal, new List<Permission>{ Permission.Read, Permission.ManageTeam, Permission.ManageTracks }},
            { Role.Mechanic, new List<Permission>{ Permission.Read, Permission.ManageTeam }},
            { Role.Driver, new List<Permission>{ Permission.Read }}
        };

        // Metoda zwracająca listę uprawnień przypisanych do danej roli
        public static List<Permission> GetPermissions(Role role)
        {
            return _rolePermissions.ContainsKey(role) ? _rolePermissions[role] : new List<Permission>();
        }
    }

    // Klasa zarządzająca logowaniem użytkownika
    public class AccountManagement
    {
        // Logowanie użytkownika na podstawie loginu i hasła
        public bool LoginUser(string username, string password)
        {
            // Podpinanie się pod zdarzenie z PasswordManagera
            PasswordManager.PasswordVerified += OnPasswordVerified;

            // Weryfikacja hasła
            bool isVerified = PasswordManager.VerifyPassword(username, password);

            // Odpinanie zdarzenia po zakończeniu
            PasswordManager.PasswordVerified -= OnPasswordVerified;

            return isVerified;
        }

        // Obsługa zdarzenia PasswordVerified – wypisuje komunikat w zależności od wyniku
        private void OnPasswordVerified(string user, bool isVerified)
        {
            if (isVerified)
            {
                Console.WriteLine("Pomyślnie zweryfikowano hasło");
            }
            else
            {
                Console.WriteLine("Nie udało się zweryfikować hasła");
            }
        }
    }
}
