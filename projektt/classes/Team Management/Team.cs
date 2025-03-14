namespace projektt.classes;


public class Team
{
    
    public delegate void DisplayInfo(string FirstName, string LastName);
    
    public string Team_Name { get; set; }
    public Principal Principal { get; set; }
    public List<Mechanics> Mechanics { get; set; }
    public List<Driver> drivers { get; set; }
    public float Budget { get; set; }
    
}