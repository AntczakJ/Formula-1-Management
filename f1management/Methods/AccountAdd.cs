namespace f1management.Methods;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Linq;

public class AccountAdd
{
    public enum Role
    {
        Principal,
        Mechanic,
        Driver
    }

    public enum Permission
    {
        Read,
        ManageTeam,
        ManageTrack
    }

    public class User
    {
        public static string UserName { get; set; }
        
        public static string Password { get; set; }
        
        public static Role role { get; set; }
        
        public static Dictionary<string, string> LoginInfo { get; set; }

        public User()
        {
            LoginInfo = new Dictionary<string, string>();
        }
        
        public void Login()
        {
            Console.WriteLine("----------LOGIN----------");
            Console.WriteLine("Enter login -> ");
             UserName = Console.ReadLine();

            if (! LoginInfo.ContainsKey(UserName))
            {
                
                 Password = Console.ReadLine();
                 LoginInfo.Add(UserName, Password);
            }
            else
            {
                
                Console.WriteLine("Enter password -> ");
                Password = Console.ReadLine();
                Console.WriteLine("Choose your role: Principal | Mechanic | Driver");
                string roleInput = Console.ReadLine();
                while (!Enum.TryParse<AccountAdd.Role>(roleInput, out var parsedRole))
                {
                    Console.WriteLine("Enter valid role. ");
                }
                 role = Enum.Parse<AccountAdd.Role>(roleInput);
                
                
                if (LoginInfo[UserName] ==  Password)
                {
                    Console.WriteLine($"Successfully logged as {role}");
                }
            }
        }
    }

    public class RBAC
    {
        private readonly Dictionary<Role, List<Permission>> _rolePermissions;
        
        public RBAC()
        {
            _rolePermissions = new Dictionary<Role, List<Permission>>
            {
                { Role.Principal, new List<Permission>{ Permission.Read, Permission.ManageTeam, Permission.ManageTrack }},
                { Role.Mechanic, new List<Permission>{ Permission.Read, Permission.ManageTeam }},
                { Role.Driver, new List<Permission>{ Permission.Read }}
            };
        }



        
    }
}