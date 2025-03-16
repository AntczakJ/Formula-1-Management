namespace f1management.RaceManagement;

public class Race
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public Dictionary<string, DateTime> Race_Info { get; set; }

    public string DateChange(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}