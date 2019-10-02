using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class TicketsController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }

        [System.Web.Http.Authorize]
        [Route("[action]/{idFromPost?}")]
        [HttpGet]
        public IActionResult Get( int? idFromPost)
        {
            ClaimsPrincipal user = HttpContext.User;

            if (user != null && user.Claims != null)
            {
                Permission role = UserData.GetRoleFromClaim(User);
                bool viewable = role.HasFlag(Permission.ViewTicket) || role.HasFlag(Permission.ViewAll);
                bool viewAll = role.HasFlag(Permission.ViewAll);

                if ( role >= 0 && viewable )
                {
                    int userId = UserData.GetUserIdFromClaim(User);
                    List<Ticket> tickets = null;
                    
                    //TODO: Add view all roles here
                    if(idFromPost == null)
                    {
                        tickets = _context.Ticket.Where(t => (t.OwnerId == userId || viewAll)).ToList();
                    }
                    else
                    {
                        //int idToFind = Int32.Parse(idFromPost);
                        tickets = new List<Ticket>() { _context.Ticket.FirstOrDefault(t => (t.OwnerId == userId || viewAll) && t.Id == idFromPost) };
                    }

                    var canDelete = role.HasFlag(Permission.DeleteAll);
                    return StatusCode(200, Json(new
                        {
                            canDelete,
                            tickets
                        }
                    ));

                }
                else
                {
                    return Json("NoTickets");
                }
            }
            else
            {
                return Json("NoTickets");
            }
        }

        [System.Web.Http.Authorize]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Ticket ticketFromPost)
        {
            int newId = _context.Ticket.Count() >0 ? _context.Ticket.Max(t => t.Id)+1 : 0;
            int userId = UserData.GetUserIdFromClaim(User);
            bool canCreate = UserData.GetRoleFromClaim(User).HasFlag(Permission.CreateTicket);

            if(canCreate)
            {
                _context.Ticket.Add(new Ticket
                {
                    Id = newId,
                    OwnerId = userId,
                    Status = ticketFromPost.Status,
                    Words = ticketFromPost.Words
                });
                _context.SaveChanges();

                return StatusCode(200);
            }
            else
            {
                return StatusCode(403);
            }
            
        }

        [System.Web.Http.Authorize]
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> Update([FromBody] Ticket ticketFromPost)
        {
            bool canUpdateAll = UserData.GetRoleFromClaim(User).HasFlag(Permission.UpdateAll);
            int userId = UserData.GetUserIdFromClaim(User);

            int oldId = ticketFromPost.Id;

            Ticket ticket = _context.Ticket.FirstOrDefault(t => t.Id == ticketFromPost.Id);
            if(canUpdateAll || ticket.OwnerId == userId)
            {
                ticket.Status = ticketFromPost.Status;
                ticket.Words = ticketFromPost.Words;

                _context.Ticket.Update(ticket);
                _context.SaveChanges();

                return StatusCode(200);

            }
            else
            {
                return StatusCode(403);
            }
        }

        [System.Web.Http.Authorize]
        [Route("[action]/{idFromPost}")]
        [HttpPost]
        public async Task<ActionResult> Delete(int idFromPost)
        {
            bool canDeleteAll = UserData.GetRoleFromClaim(User).HasFlag(Permission.DeleteAll);

            if(canDeleteAll)
            {
                Ticket ticket = _context.Ticket.FirstOrDefault(t => t.Id == idFromPost);
                if(ticket != null)
                {
                    _context.Ticket.Remove(ticket);
                    _context.SaveChanges();

                    return StatusCode(200);

                }
                else
                {
                    return StatusCode(404);
                }

            }
            else
            {
                return StatusCode(403);
            }
        }
    }
}
