using f1management.Methods;
using System;
using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Driver : User, Information
    {
        public Driver(string firstName, string lastName) : base(firstName, lastName, Role.Driver)
        {
            User.TriggerSave(this, Program.Drivers, Program.Mechanics, Program.Principals);
        }

        

        public void DisplayInfo()
        {
            Console.WriteLine($"Kierowca: {FirstName} {LastName}");
        }

        public void AddDriver(User user, List<Driver> drivers)
        {
            if (user is Driver driver)
            {
                if (!drivers.Any(d => d.FirstName == driver.FirstName && d.LastName == driver.LastName))
                {
                    drivers.Add(driver);
                    Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} już istnieje.");
                }
            }
            else
            {
                Console.WriteLine("[ERROR] Obiekt user NIE JEST typu Driver");
            }
        }

        public void RemoveDriver(List<Driver> drivers)
        {
            if (drivers.Contains(this))
            {
                drivers.Remove(this);
                Console.WriteLine($"Kierowca {FirstName} {LastName} usunięty.");
            }
            else
            {
                Console.WriteLine($"Kierowca {FirstName} {LastName} nie istnieje.");
            }
        }
    }
}