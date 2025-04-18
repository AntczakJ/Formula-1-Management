﻿using System.Collections.Generic;
using System;
using System.IO;

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

        // Event i delegat do logowania
        public delegate void LogTeamAction(Team team, string action);
        public static event LogTeamAction TeamLog;

        public static void TriggerTeamLog(Team team, string action)
        {
            TeamLog?.Invoke(team, action);
        }

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
                TriggerTeamLog(this, $"Dodano kierowcę {driver.FirstName} {driver.LastName}");
            }
        }

        public void RemoveDriver(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                Drivers.Remove(driver);
                Console.WriteLine($"Kierowca {driver.FirstName} {driver.LastName} został usunięty z zespołu.");
                TriggerTeamLog(this, $"Usunięto kierowcę {driver.FirstName} {driver.LastName}");
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
                TriggerTeamLog(this, $"Dodano mechanika {mechanic.FirstName} {mechanic.LastName}");
            }
        }

        public void RemoveMechanic(Mechanics mechanic)
        {
            if (Mechanics.Contains(mechanic))
            {
                Mechanics.Remove(mechanic);
                Console.WriteLine($"Mechanik {mechanic.FirstName} {mechanic.LastName} został usunięty z zespołu.");
                TriggerTeamLog(this, $"Usunięto mechanika {mechanic.FirstName} {mechanic.LastName}");
            }
            else
            {
                Console.WriteLine($"Mechanik {mechanic.FirstName} {mechanic.LastName} nie jest członkiem tego zespołu.");
            }
        }

        public void AddPrincipal(Principal principal)
        {
            if (Principal == null)
            {
                Principal = principal;
                Console.WriteLine($"Szef zespołu {principal.FirstName} {principal.LastName} został przypisany do zespołu.");
                TriggerTeamLog(this, $"Przypisano szefa zespołu {principal.FirstName} {principal.LastName}");
            }
            else
            {
                Console.WriteLine("Zespół już posiada szefa.");
            }
        }


        public void RemovePrincipal(Principal principal)
        {
            if (Principal == principal)
            {
                Principal = null;
                Console.WriteLine($"Szef zespołu {principal.FirstName} {principal.LastName} został usunięty z zespołu.");
                TriggerTeamLog(this, $"Usunięto szefa zespołu {principal.FirstName} {principal.LastName}");
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
            TriggerTeamLog(this, $"Zwiększono budżet o {amount}$ z powodu: {reason}");
        }

        public void DecreaseBudget(float amount, string reason)
        {
            Budget -= amount;
            Console.WriteLine($"Budżet zmniejszony o {amount}$ z powodu: {reason}. Aktualny budżet: {Budget}");
            TriggerTeamLog(this, $"Zmniejszono budżet o {amount}$ z powodu: {reason}");
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
