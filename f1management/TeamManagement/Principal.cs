namespace f1management.TeamManagement;

public class Principal : Team
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Principal(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    
}