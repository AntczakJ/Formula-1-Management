using System.Collections.Generic;

namespace f1management.TeamManagement
{
    public class Team
    {
        public string Team_Name { get; set; }
        public Principal Principal { get; set; }
        public List<Mechanics> Mechanics { get; set; }
        public List<Driver> Drivers { get; set; }
        public float Budget { get; set; }

        public Team(string teamName, Principal principal, List<Mechanics> mechanics, List<Driver> drivers, float budget)
        {
            Team_Name = teamName;
            Principal = principal;
            Mechanics = mechanics;
            Drivers = drivers;
            Budget = budget;
        }
    }
}