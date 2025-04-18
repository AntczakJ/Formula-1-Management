using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Driver : User, Information
    {
        public Driver(string firstName, string lastName, string username)
            : base(firstName, lastName, Role.Driver, username)
        {
            try
            {
                User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy dodawaniu kierowcy: {ex.Message}");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"{FirstName} {LastName}");
        }

        public void AddDriver(List<Driver> drivers)
        {
            try
            {
                if (!drivers.Contains(this))
                {
                    drivers.Add(this);
                    Console.WriteLine($"Kierowca {FirstName} {LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Kierowca {FirstName} {LastName} już istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy dodawaniu kierowcy: {ex.Message}");
            }
        }

        public void RemoveDriver(List<Driver> drivers)
        {
            try
            {
                if (drivers.Contains(this))
                {
                    drivers.Remove(this);
                    Console.WriteLine($"Kierowca {FirstName} {LastName} usunięty.");
                    User.TriggerRemove(this, Program.Drivers, Program.Mechanics, Program.Principals);
                }
                else
                {
                    Console.WriteLine($"Kierowca {FirstName} {LastName} nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy usuwaniu kierowcy: {ex.Message}");
            }
        }
    }
}
