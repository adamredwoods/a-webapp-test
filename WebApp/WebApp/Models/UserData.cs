using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models
{
    

    public class UserData
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Permission GroupRole { get; set; }
        
        public static int GetUserIdFromClaim(ClaimsPrincipal claim)
        {
            return Int32.Parse(claim.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
        }

        public static Permission GetRoleFromClaim(ClaimsPrincipal claim)
        {
            if( claim.Claims != null && claim.Claims.Count() > 0)
            {
                return (Permission) Int32.Parse(claim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            }
            return Permission.None;
        }
    }

}
