namespace f1management.classes;

public class Mechanics : Team
{
    public string Mechanic_FirstName { get; set; }
    public string Mechanic_LastName { get; set; }
    
    public void PrincipalInfo(string FirstName, string LastName)
    {
        Console.WriteLine($"nazywa sie {FirstName} {LastName}");
    }
}