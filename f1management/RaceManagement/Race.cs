using f1management.Methods;
using f1management.TeamManagement;
using System;
using System.Collections.Generic;

namespace f1management.RaceManagement
{
    public class Race
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Dictionary<string, DateTime> Race_Info { get; set; }

        public delegate void SaveToList(Race race, List<Race> races);
        public static event SaveToList SaveRaceToList;

        public delegate void RemoveFromList(Race race, List<Race> races);
        public static event RemoveFromList RemoveRaceInfo;

        public static void TriggerRemove(Race race, List<Race> races)
        {
            RemoveRaceInfo?.Invoke(race, races);
        }

        public Race(string name, DateTime startDate)
        {
            Name = name;
            StartDate = startDate;

            try
            {
                SaveRaceToList?.Invoke(this, new List<Race>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy zapisie wyścigu: {ex.Message}");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Wyścig {Name} zacznie się {StartDate:yyyy-MM-dd}");
        }

        public void AddRace(List<Race> races)
        {
            try
            {
                if (races.Contains(this))
                {
                    Console.WriteLine($"Wyścig {Name} już istnieje.");
                }
                else
                {
                    races.Add(this);
                    Console.WriteLine($"Wyścig {Name} dodany.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy dodawaniu wyścigu: {ex.Message}");
            }
        }

        public void RemoveRace(List<Race> races)
        {
            try
            {
                if (races.Contains(this))
                {
                    races.Remove(this);
                    Console.WriteLine($"Wyścig {Name} został usunięty.");
                    TriggerRemove(this, races);
                }
                else
                {
                    Console.WriteLine($"Wyścig {Name} nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy usuwaniu wyścigu: {ex.Message}");
            }
        }

        public string DateChange(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
