using f1management.Methods;

namespace f1management.RaceManagement;

public class Race
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public Dictionary<string, DateTime> Race_Info { get; set; }
    
        public void DisplayInfo()
        {
            Console.WriteLine($"Wyścig {Name} zacznie sie {StartDate}");
        }

        public void AddRace(string name, DateTime startDate)
        {
            if (Race_Info.ContainsKey(name))
            {
                Console.WriteLine($"Podany wyscig {name} już istnieje");
            }
            else
            {
                Race_Info.Add(name, startDate);
            }
        }
        public string DateChange(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
}