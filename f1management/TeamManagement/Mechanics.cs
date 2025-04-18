using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Mechanics : User, Information
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Mechanics(string firstName, string lastName) : base(firstName, lastName, Role.Mechanic)
        {
            FirstName = firstName;
            LastName = lastName;

        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Mechanik: {FirstName} {LastName}");
        }

        public void AddMechanic(List<Mechanics> mechanics)
        {
            if (mechanics.Contains(this))
            {
                Console.WriteLine($"Mechanik {FirstName} {LastName} już istnieje.");
            }
            else
            {
                mechanics.Add(this);
                Console.WriteLine($"Mechanik {FirstName} {LastName} dodany.");
            }
        }

        public void RemoveMechanic(List<Mechanics> mechanics)
        {
            if (mechanics.Contains(this))
            {
                mechanics.Remove(this);
                Console.WriteLine($"Mechanik {FirstName} {LastName} usunięty.");
            }
            else
            {
                Console.WriteLine($"Mechanik {FirstName} {LastName} nie istnieje.");
            }
        }
    }
}