using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Mechanics : User, Information
    {
        public Mechanics(string firstName, string lastName, string username)
            : base(firstName, lastName, Role.Mechanic, username)
        {
            try
            {
                User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy dodawaniu mechanika: {ex.Message}");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Mechanik: {FirstName} {LastName}");
        }

        public void AddMechanic(User user, List<Mechanics> mechanics)
        {
            try
            {
                if (!mechanics.Contains(this))
                {
                    mechanics.Add(this);
                    Console.WriteLine($"Mechanik {FirstName} {LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Mechanik {FirstName} {LastName} już istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy dodawaniu mechanika: {ex.Message}");
            }
        }

        public void RemoveMechanic(List<Mechanics> mechanics)
        {
            try
            {
                if (mechanics.Contains(this))
                {
                    mechanics.Remove(this);
                    Console.WriteLine($"Mechanik {FirstName} {LastName} usunięty.");
                    User.TriggerRemove(this, Program.Drivers, Program.Mechanics, Program.Principals);
                }
                else
                {
                    Console.WriteLine($"Mechanik {FirstName} {LastName} nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy usuwaniu mechanika: {ex.Message}");
            }
        }
    }
}
