using System;

namespace WebApp.Models
{
    public enum Permission
    {
        None = -1,
        Login = 1,
        CreateTicket = 2,
        UpdateTicket = 4,
        ViewTicket = 8,
        ViewAll = 16,
        UpdateAll = 32,
        DeleteAll = 64
    }

    public class UserGroup
    {
        public Permission permissions { get; set; }

        public Permission GetPermissions()
        {
            return permissions;
        }

        public static Permission User()
        {
            return Permission.Login;
        }

        public static Permission HelpdeskTeamMember()
        {
            return Permission.Login | Permission.ViewAll | Permission.UpdateAll | Permission.DeleteAll;
        } 

        public static Permission HelpdeskUser()
        {
            return Permission.Login | Permission.CreateTicket | Permission.UpdateTicket | Permission.ViewTicket;
        }
    }
}
