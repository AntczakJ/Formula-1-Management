using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Principal : User, Information
    {
        public Principal(string firstName, string lastName, string username)
            : base(firstName, lastName, Role.Principal, username)
        {
            try
            {
                User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy tworzeniu szefa zespołu: {ex.Message}");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Szef zespołu: {FirstName} {LastName}");
        }

        public void AddPrincipal(User user, List<Principal> principals)
        {
            try
            {
                if (user is Principal principal)
                {
                    if (!principals.Contains(this))
                    {
                        principals.Add(principal);
                        Console.WriteLine($"Szef {principal.FirstName} {principal.LastName} dodany.");
                    }
                    else
                    {
                        Console.WriteLine($"Szef {principal.FirstName} {principal.LastName} już istnieje.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy dodawaniu szefa: {ex.Message}");
            }
        }

        public void RemovePrincipal(List<Principal> principals)
        {
            try
            {
                if (principals.Contains(this))
                {
                    principals.Remove(this);
                    Console.WriteLine($"Szef zespołu {FirstName} {LastName} usunięty.");
                    User.TriggerRemove(this, Program.Drivers, Program.Mechanics, Program.Principals);
                }
                else
                {
                    Console.WriteLine($"Szef zespołu {FirstName} {LastName} nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy usuwaniu szefa: {ex.Message}");
            }
        }
    }
}
