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

        public void AddDriver(List<Driver> drivers)
        {
            
                if (drivers.Contains(this))
                {
                    drivers.Add(this);
                    Console.WriteLine($"Kierowca {FirstName} {LastName} dodany.");
                }
                else
                {
                    Console.WriteLine($"Kierowca {FirstName} {LastName} już istnieje.");
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