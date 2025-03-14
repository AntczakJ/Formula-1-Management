namespace projektt.classes;

public class Principal : Team
{
    public string Principal_FirstName { get; set; }
    public string Principal_LastName { get; set; }

    public Principal(string FirstName, string LastName)
    {
        Principal_FirstName = FirstName;
        Principal_LastName = LastName;
    }

    public void PrincipalInfo(string FirstName, string LastName)
    {
        Console.WriteLine($"nazywa sie {FirstName} {LastName}");
    }
}