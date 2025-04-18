// Import przestrzeni nazw potrzebnych do działania klas
using f1management.TeamManagement;
using System;
using f1management.RaceManagement;
using System.Collections.Generic;

namespace f1management.Methods
{
    // Enum określający możliwe role użytkowników
    public enum Role
    {
        Principal,  // Szef zespołu
        Mechanic,   // Mechanik
        Driver      // Kierowca
    }

    // Enum określający poziomy uprawnień
    public enum Permission
    {
        ManageTracks,  // Zarządzanie torami
        Read,          // Odczyt danych
        ManageTeam     // Zarządzanie zespołem
    }

    // Interfejs do wyświetlania informacji
    public interface Information
    {
        void DisplayInfo();
    }

    // Klasa reprezentująca użytkownika systemu
    public class User
    {
        // Delegat do zapisu użytkownika do pliku
        public delegate void SaveToFile(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals);

        // Zdarzenie do zapisu użytkownika
        public static event SaveToFile SaveInfo;

        // Delegat do usuwania użytkownika z pliku
        public delegate void RemoveFromFile(User x, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals);

        // Zdarzenie do usuwania użytkownika
        public static event RemoveFromFile RemoveInfo;

        // Właściwości użytkownika
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public List<Permission> Permissions { get; set; }

        // Konstruktor użytkownika – przypisuje uprawnienia zgodnie z rolą
        public User(string firstname, string lastname, Role role, string username)
        {
            FirstName = firstname;
            LastName = lastname;
            Role = role;
            Username = username;
            Permissions = RBAC.GetPermissions(role);
        }

        // Sprawdza, czy użytkownik ma dane uprawnienie
        public bool HasPermission(Permission permission)
        {
            return Permissions.Contains(permission);
        }

        // Wywołanie zapisu użytkownika
        public static void TriggerSave(User user, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
        {
            SaveInfo?.Invoke(user, drivers, mechanics, principals);
        }

        // Wywołanie usunięcia użytkownika
        public static void TriggerRemove(User user, List<Driver> drivers, List<Mechanics> mechanics, List<Principal> principals)
        {
            RemoveInfo?.Invoke(user, drivers, mechanics, principals);
        }
    }

    // Klasa RBAC – przypisuje uprawnienia do ról
    public static class RBAC
    {
        // Słownik łączący role z ich uprawnieniami
        private static readonly Dictionary<Role, List<Permission>> _rolePermissions = new Dictionary<Role, List<Permission>>
        {
            { Role.Principal, new List<Permission>{ Permission.Read, Permission.ManageTeam, Permission.ManageTracks }},
            { Role.Mechanic, new List<Permission>{ Permission.Read, Permission.ManageTeam }},
            { Role.Driver, new List<Permission>{ Permission.Read }}
        };

        // Zwraca uprawnienia dla danej roli
        public static List<Permission> GetPermissions(Role role)
        {
            return _rolePermissions.ContainsKey(role) ? _rolePermissions[role] : new List<Permission>();
        }
    }

    // Klasa odpowiadająca za logowanie użytkownika
    public class AccountManagement
    {
        // Metoda logowania – weryfikuje dane i subskrybuje zdarzenie
        public bool LoginUser(string username, string password)
        {
            PasswordManager.PasswordVerified += OnPasswordVerified;

            bool isVerified = PasswordManager.VerifyPassword(username, password);

            PasswordManager.PasswordVerified -= OnPasswordVerified;

            return isVerified;
        }

        // Obsługa zdarzenia po weryfikacji hasła
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
