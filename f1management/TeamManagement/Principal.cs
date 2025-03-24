using f1management.Methods;

namespace f1management.TeamManagement
{
    public class Principal : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Principal(string firstName, string lastName) : base(Role.Principal)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}