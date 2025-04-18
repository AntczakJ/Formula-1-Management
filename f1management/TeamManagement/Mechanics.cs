using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Mechanics : User, Information
    {

        public Mechanics(string firstName, string lastName) : base(firstName, lastName, Role.Mechanic)
        {
            User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Mechanik: {FirstName} {LastName}");
        }

        public void AddMechanic(User user, List<Mechanics> mechanics)
        {
            
                if (!mechanics.Any(d => d.FirstName == FirstName && d.LastName == LastName))
                {
                    mechanics.Add(this);
                    Console.WriteLine($"Mechanik {FirstName} {LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Mechanik {FirstName} {LastName} już istnieje.");
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