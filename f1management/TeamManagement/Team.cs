using System.Collections.Generic;
using System;
using System.IO;

namespace f1management.TeamManagement
{
    // Klasa opisująca zespół F1: szef, mechanicy, kierowcy i budżet.
    public class Team
    {
        public string TeamName { get; set; }          // Nazwa zespołu.
        public Principal Principal { get; set; }     // Szef zespołu.
        public List<Mechanics> Mechanics { get; set; } // Lista mechaników.
        public List<Driver> Drivers { get; set; }    // Lista kierowców.
        public float Budget { get; set; }            // Budżet zespołu.

        public delegate void TeamInfo();             // Delegat do wyświetlania info.
        public delegate void LogTeamAction(Team team, string action); // Delegat do logowania.
        public static event LogTeamAction TeamLog;   // Event logujący akcje zespołu.

        // Wywołuje logowanie dla podanej akcji.
        public static void TriggerTeamLog(Team team, string action)
        {
            TeamLog?.Invoke(team, action);
        }

        // Konstruktor tworzący zespół z początkowymi danymi.
        public Team(string teamName, Principal principal, List<Mechanics> mechanics, List<Driver> drivers, float budget)
        {
            TeamName = teamName;
            Principal = principal;
            Mechanics = mechanics;
            Drivers = drivers;
            Budget = budget;
        }

        // Dodaje kierowcę, jeśli nie jest jeszcze w zespole.
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

        // Usuwa kierowcę z zespołu.
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

        // Dodaje mechanika, jeśli nie jest jeszcze w zespole.
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

        // Usuwa mechanika z zespołu.
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

        // Przypisuje szefa do zespołu, jeśli nie ma jeszcze szefa.
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

        // Usuwa szefa zespołu, jeśli pasuje do podanego obiektu.
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

        // Zwiększa budżet zespołu.
        public void IncreaseBudget(float amount, string reason)
        {
            Budget += amount;
            Console.WriteLine($"Budżet zwiększony o {amount}$ z powodu: {reason}. Aktualny budżet: {Budget}");
            TriggerTeamLog(this, $"Zwiększono budżet o {amount}$ z powodu: {reason}");
        }

        // Zmniejsza budżet zespołu.
        public void DecreaseBudget(float amount, string reason)
        {
            Budget -= amount;
            Console.WriteLine($"Budżet zmniejszony o {amount}$ z powodu: {reason}. Aktualny budżet: {Budget}");
            TriggerTeamLog(this, $"Zmniejszono budżet o {amount}$ z powodu: {reason}");
        }

        // Wyświetla dane o zespole, szefie, kierowcach i mechanikach.
        public void DisplayInfo()
        {
            List<string> missingInfo = new List<string>();
            if (Principal == null) missingInfo.Add("Brak szefa zespołu.");
            if (Drivers.Count == 0) missingInfo.Add("Brak kierowców.");
            if (Mechanics.Count == 0) missingInfo.Add("Brak mechaników.");

            if (missingInfo.Count > 0)
            {
                Console.WriteLine("Zespół nie jest w pełni uzupełniony:");
                foreach (var info in missingInfo)
                {
                    Console.WriteLine(info);
                }
            }
            else
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
}
