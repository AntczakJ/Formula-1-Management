using f1management.Methods;

namespace f1management.TeamManagement
{
    public class Driver : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Driver(string firstName, string lastName) : base(Role.Driver)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}