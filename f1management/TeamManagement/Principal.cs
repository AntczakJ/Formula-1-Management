using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Principal : User, Information
    {

        public Principal(string firstName, string lastName) : base(firstName, lastName, Role.Principal)
        {
            User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
        
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Szef zespołu: {FirstName} {LastName}");
        }

        public void AddPrincipal(User user, List<Principal> principals)
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

        public void RemovePrincipal(List<Principal> principals)
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
    }
}