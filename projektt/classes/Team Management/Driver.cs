namespace projektt.classes;

public class Driver : Team
{
    public string Driver_FirstName { get; set; }
    public string Driver_LastName { get; set; }
    
    public void PrincipalInfo(string FirstName, string LastName)
    {
        Console.WriteLine($"nazywa sie {FirstName} {LastName}");
    }
}