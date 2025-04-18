using System.Collections.Generic;
using System;

namespace f1management.TeamManagement
{
    public class Team
    {
        public string TeamName { get; set; }
        public Principal Principal { get; set; }
        public List<Mechanics> Mechanics { get; set; }
        public List<Driver> Drivers { get; set; }
        public float Budget { get; set; }
        

        public delegate void TeamInfo();

        public Team(string teamName, Principal principal, List<Mechanics> mechanics, List<Driver> drivers, float budget)
        {
            TeamName = teamName;
            Principal = principal;
            Mechanics = mechanics;
            Drivers = drivers;
            Budget = budget;
            
        }

        public void AddDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} już jest członkiem tego zespołu.");
            }
            else
            {
                Drivers.Add(driver);
                Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} został dodany do zespołu.");
            }
        }

        public void RemoveDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                Drivers.Remove(driver);
                Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} został usunięty z zespołu.");
            }
            else
            {
                Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} nie jest członkiem tego zespołu.");
            }
        }

        public void AddMechanic(Mechanics mechanic)
        {
            if (Mechanics.Contains(mechanic))
            {
                Console.WriteLine($"Mechanik {mechanic.FirstName} {mechanic.LastName} już jest członkiem tego zespołu.");
            }
            else
            {
                Mechanics.Add(mechanic);
                Console.WriteLine($"Mechanik {mechanic.FirstName} {mechanic.LastName} został dodany do zespołu.");
            }
        }

        public void RemoveMechanic(Mechanics mechanic)
        {
            if (Mechanics.Contains(mechanic))
            {
                Mechanics.Remove(mechanic);
                Console.WriteLine($"Mechanik {mechanic.FirstName} {mechanic.LastName} został usunięty z zespołu.");
            }
            else
            {
                Console.WriteLine($"Mechanik {mechanic.FirstName} {mechanic.LastName} nie jest członkiem tego zespołu.");
            }
        }

        public void AddPrincipal(Principal principal)
        {
            if (Principal == principal)
            {
                Console.WriteLine($"Szef zespołu {principal.FirstName} {principal.LastName} już jest członkiem tego zespołu.");
            }
            else
            {
                Principal = principal;
                Console.WriteLine($"Szef zespołu {principal.FirstName} {principal.LastName} został dodany do zespołu.");
            }
        }

        public void RemovePrincipal(Principal principal)
        {
            if (Principal == principal)
            {
                Principal = null;
                Console.WriteLine($"Szef zespołu {principal.FirstName} {principal.LastName} został usunięty z zespołu.");
            }
            else
            {
                Console.WriteLine($"Szef zespołu {principal.FirstName} {principal.LastName} nie jest członkiem tego zespołu.");
            }
        }

        public void IncreaseBudget(float amount, string reason)
        {
            Budget += amount;
            Console.WriteLine($"Budżet zwiększony o {amount}$ z powodu: {reason}. Aktualny budżet: {Budget}");
        }

        public void DecreaseBudget(float amount, string reason)
        {
            Budget -= amount;
            Console.WriteLine($"Budżet zmniejszony o {amount}$ z powodu: {reason}. Aktualny budżet: {Budget}");
        }

        public void DisplayInfo()
        {
            TeamInfo info = Principal.DisplayInfo;
            info += () =>
            {
                Console.WriteLine("Kierowcy:");
                foreach (var driver in Drivers)
                {
                    driver.DisplayInfo();
                }
            };
            info += () =>
            {
                Console.WriteLine("Mechanicy:");
                foreach (var mechanic in Mechanics)
                {
                    mechanic.DisplayInfo();
                }
            };

            Console.WriteLine($"Zespół: {TeamName}");
            Console.WriteLine($"Budżet: {Budget}$");
            info.Invoke();
        }
    }
}