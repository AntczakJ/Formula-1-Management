using f1management.Methods;
using f1management.TeamManagement;
using System;
using System.Collections.Generic;

namespace f1management.RaceManagement
{
    // Klasa reprezentująca wyścig w systemie F1
    public class Race
    {
        // Nazwa wyścigu (np. "Grand Prix Monaco")
        public string Name { get; set; }

        // Data rozpoczęcia wyścigu
        public DateTime StartDate { get; set; }

        // Dodatkowe informacje o wyścigu w formie słownika
        public Dictionary<string, DateTime> Race_Info { get; set; }

        // Delegat definiujący sposób zapisu wyścigu do listy
        public delegate void SaveToList(Race race, List<Race> races);

        // Zdarzenie wywoływane przy zapisie wyścigu
        public static event SaveToList SaveRaceToList;

        // Delegat definiujący sposób usunięcia wyścigu z listy
        public delegate void RemoveFromList(Race race, List<Race> races);

        // Zdarzenie wywoływane przy usunięciu wyścigu
        public static event RemoveFromList RemoveRaceInfo;

        // Wywołuje zdarzenie usuwania wyścigu
        public static void TriggerRemove(Race race, List<Race> races)
        {
            RemoveRaceInfo?.Invoke(race, races);
        }

        // Konstruktor wyścigu – przypisuje nazwę i datę oraz wywołuje zdarzenie zapisu
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

        // Wyświetla informacje o wyścigu
        public void DisplayInfo()
        {
            Console.WriteLine($"Wyścig {Name} zacznie się {StartDate:yyyy-MM-dd}");
        }

        // Dodaje wyścig do listy, jeśli jeszcze go tam nie ma
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

        // Usuwa wyścig z listy, jeśli się w niej znajduje
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

        // Pomocnicza metoda zmieniająca format daty na string
        public string DateChange(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
