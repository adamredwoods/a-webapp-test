using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        [Route("[action]")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserData userDataPost)
        {
            var userData = await GetUserData(userDataPost.Name, userDataPost.Password);

            if (userData !=null)
            {
                int role = Convert.ToInt32(userData.GroupRole);
                var claims = new List<Claim>
                 {
                    new Claim(ClaimTypes.Name, userData.Name),
                    new Claim(ClaimTypes.Sid, userData.Id.ToString()),
                    new Claim(ClaimTypes.Role, Convert.ToString(role))
                 };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
 
                return StatusCode(200, Json( new
                    {
                        userData.Name,
                        userData.GroupRole
                    }
                ));
            }
            else
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            }
            
        }

        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return StatusCode(200);
        }

        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Info()
        {
            Claim userObject = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            Permission role = UserData.GetRoleFromClaim(User);
            if(userObject != null)
            {
                return StatusCode(200, Json ( new
                    {
                        Name = userObject.Value,
                        Role = role
                    }     
                ));

            }
            else
            {
                return StatusCode(404, Json(new
                    {
                        Name = String.Empty,
                        Role = 0
                    }
                ));
            }
        }

        public async Task<UserData> GetUserData(string username, string password)
        {
            //TODO: should check in the database
            List<UserData> fakeUsers = new List<UserData>
            {
                new UserData
                {
                    Id = 1,
                    Name = "User1",
                    Password = "123456",
                    GroupRole = UserGroup.User()
                },
                new UserData
                {
                    Id = 2,
                    Name = "HelpdeskUser1",
                    Password = "123456",
                    GroupRole = UserGroup.HelpdeskUser()
                },
                new UserData
                {
                    Id = 3,
                    Name = "HelpdeskUser2",
                    Password = "123456",
                    GroupRole = UserGroup.HelpdeskUser()
                },
                new UserData
                {
                    Id = 4,
                    Name = "HelpdeskTeamMember",
                    Password = "123456",
                    GroupRole = UserGroup.HelpdeskTeamMember()
                }
            };

            UserData user = fakeUsers.FirstOrDefault(u => username == u.Name);

            return user;
        }
        
    }
}
