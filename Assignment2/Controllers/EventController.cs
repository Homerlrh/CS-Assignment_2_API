using Assignment2.Data;
using Assignment2.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync([FromBody] Event Event)
        {
            if (ModelState.IsValid)
            {
                EventRepo eRP = new EventRepo(_context);
                var result = eRP.Create(Event.EventName, Event.Description, Event.Date, Event.Time);
                if (result)
                {
                    var obj = new
                    {
                        StatusCode = "Ok",
                        EventName = Event.EventName
                    };

                    return new ObjectResult(obj);
                }
            }

            var InvalidObj = new
            {
                StatusCode = "Invalid",
                EventName = Event.EventName
            };

            return new ObjectResult(InvalidObj);

        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int eventID)
        {
            try
            {
                EventRepo eRP = new EventRepo(_context);
                eRP.Delete(eventID);
                var Obj = new
                {
                    StatusCode = "Deleted",
                };

                return new ObjectResult(Obj);
            }
            catch
            {
                var Obj = new
                {
                    errorMessage = "something went wrong, please try again",
                };

                return new ObjectResult(Obj);
            }

        }

        [HttpGet]
        [Route("JoinEvent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Manager")]

        public IActionResult JoinEvent(int eventID)
        {
            ClientRepo cRP = new ClientRepo(_context);
            EventRepo eRP = new EventRepo(_context);

            var claim = HttpContext.User.Claims.ElementAt(0);
            string email = claim.Value;
            var user = cRP.GetOneByEmail(email);

            eRP.JoinEvent(eventID, user.ID);

            var responseObject = new
            {
                StatusCode = "Ok",
            };
            return new ObjectResult(responseObject);
        }




    }
}
