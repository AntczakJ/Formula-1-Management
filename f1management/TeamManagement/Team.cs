namespace f1management.TeamManagement;


public class Team
{
    
    public string Team_Name { get; set; }
    public Principal Principal { get; set; }
    public List<Mechanics> Mechanics { get; set; }
    public List<Driver> Drivers { get; set; }
    public float Budget { get; set; }
    
}