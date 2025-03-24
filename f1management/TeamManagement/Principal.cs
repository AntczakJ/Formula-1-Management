using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Principal : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Principal(string firstName, string lastName) : base(Role.Principal)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Szef zespołu: {FirstName} {LastName}");
        }

        public void AddPrincipal(List<Principal> principals)
        {
            if (principals.Contains(this))
            {
                Console.WriteLine($"Szef zespołu {FirstName} {LastName} już istnieje.");
            }
            else
            {
                principals.Add(this);
                Console.WriteLine($"Szef zespołu {FirstName} {LastName} dodany.");
            }
        }

        public void RemovePrincipal(List<Principal> principals)
        {
            if (principals.Contains(this))
            {
                principals.Remove(this);
                Console.WriteLine($"Szef zespołu {FirstName} {LastName} usunięty.");
            }
            else
            {
                Console.WriteLine($"Szef zespołu {FirstName} {LastName} nie istnieje.");
            }
        }
    }
}