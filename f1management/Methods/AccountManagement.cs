using f1management.TeamManagement;
using System;

namespace f1management.Methods
{
    public enum Role
    {
        Principal,
        Mechanic,
        Driver
    }

    public enum Permission
    {
        ManageTracks,
        Read,
        ManageTeam
    }

    public class User
    {
        public Role Role { get; set; }
        public List<Permission> Permissions { get; set; }

        public User(Role role)
        {
            Role = role;
            Permissions = RBAC.GetPermissions(role);
        }

        public bool HasPermission(Permission permission)
        {
            return Permissions.Contains(permission);
        }
    }

    public static class RBAC
    {
        private static readonly Dictionary<Role, List<Permission>> _rolePermissions = new Dictionary<Role, List<Permission>>
        {
            { Role.Principal, new List<Permission>{ Permission.Read, Permission.ManageTeam, Permission.ManageTracks }},
            { Role.Mechanic, new List<Permission>{ Permission.Read, Permission.ManageTeam }},
            { Role.Driver, new List<Permission>{ Permission.Read }}
        };

        public static List<Permission> GetPermissions(Role role)
        {
            return _rolePermissions.ContainsKey(role) ? _rolePermissions[role] : new List<Permission>();
        }
    }

    public class AccountManagement
    {
        public bool LoginUser(string username, string password)
        {
            PasswordManager.PasswordVerified += OnPasswordVerified;

            bool isVerified = PasswordManager.VerifyPassword(username, password);

            PasswordManager.PasswordVerified -= OnPasswordVerified;

            return isVerified;
        }

        private void OnPasswordVerified(string user, bool isVerified)
        {
            if (isVerified)
            {
                Console.WriteLine("Pomyślnie zweryfikowano hasło");
            }
            else
            {
                Console.WriteLine("Nie udało się zweryfikować hasła");
            }
        }
    }
}