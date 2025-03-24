using f1management.Methods;

namespace f1management.TeamManagement
{
    public class Mechanics : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Mechanics(string firstName, string lastName) : base(Role.Mechanic)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}