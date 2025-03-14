namespace projektt.classes.Race_Management;

public class Race
{
    public string Race_Name { get; set; }
    public DateTime Race_StartDate { get; set; }
    public Dictionary<string, DateTime> Race_Info { get; set; }

    public string DateChange(DateTime date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}