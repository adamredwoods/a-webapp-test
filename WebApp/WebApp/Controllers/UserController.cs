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
                        userData.Name
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

        public async Task<UserData> GetUserData(string username, string password)
        {
            // should check in the database
            UserData user = new UserData
            {
                Name = "myself",
                Password = "password",
                GroupRole = UserGroup.User(),
                Id = 1
            };
            return user;
        }
        
    }
}
