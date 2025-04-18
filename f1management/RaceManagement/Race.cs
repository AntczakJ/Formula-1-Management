using f1management.Methods;
using f1management.TeamManagement;

namespace f1management.RaceManagement;

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
        
        SaveRaceToList?.Invoke(this, new List<Race>());
    }
    
        public void DisplayInfo()
        {
            Console.WriteLine($"Wyścig {Name} zacznie sie {StartDate}");
        }

        public void AddRace(List<Race> races)
        {
            if (races.Contains(this))
            {
                Console.WriteLine($"Wyscig {Name} już istnieje");
            }
            else
            {
                races.Add(this);
                Console.WriteLine($"Wyścig {Name} dodany.");
            }
        }
        public void RemoveRace(List<Race> races)
        {
            if (races.Contains(this))
            {
                races.Remove(this);
                Console.WriteLine($"Wyścig {Name} jest usunięty");
                TriggerRemove(this, races);
            }
            else
            {
                Console.WriteLine($"Wyścig {Name} nie istnieje.");
            }
        }
        public string DateChange(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
}