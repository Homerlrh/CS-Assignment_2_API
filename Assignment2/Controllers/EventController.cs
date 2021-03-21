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

        [HttpGet]
        public async Task<IActionResult> OnGetAsync ()
        {
            EventRepo eRP = new EventRepo(_context);
            var query = eRP.GetAll();
            var responseObject = new
            {
                EventArray = query,
            };
            return new ObjectResult(responseObject);
        }

        [HttpGet]
        [Route("getOne")]
        public IActionResult GetOne(int eventID)
        {
            EventRepo eRP = new EventRepo(_context);
            var query = eRP.GetOne(eventID);
            var responseObject = new
            {
                EventArray = query,
            };
            return new ObjectResult(responseObject);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Manager")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Manager")]
        public IActionResult Delete(int eventID)
        {
            try
            {
                EventRepo eRP = new EventRepo(_context);
                eRP.Delete(eventID);
                var Obj = new
                {
                    StatusCode = "ok",
                    message= "Deleted"
                };

                return new ObjectResult(Obj);
            }
            catch
            {
                var Obj = new
                {
                    StatusCode = "error",
                    message = "You can't delete event due to there's still people want to attend."
                };

                return new ObjectResult(Obj);
            }

        }

        [HttpGet]
        [Route("JoinEvent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public IActionResult JoinEvent(int eventID)
        {
            ClientRepo cRP = new ClientRepo(_context);
            EventRepo eRP = new EventRepo(_context);

            var claim = HttpContext.User.Claims.ElementAt(0);
            string email = claim.Value;
            var user = cRP.GetOneByEmail(email);

            try
            {
                eRP.JoinEvent(eventID, user.ID);

                var responseObject = new
                {
                    StatusCode = "You being added to the guest list",
                };
                return new ObjectResult(responseObject);

            }
            catch
            {
                var responseObject = new
                {
                    error = "You already added to the guest list",
                };
                return new ObjectResult(responseObject);
            }

           
        }

        [HttpGet]
        [Route("UnJoinEvent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UnJoinEvent (int eventID)
        {
            ClientRepo cRP = new ClientRepo(_context);
            EventRepo eRP = new EventRepo(_context);
            var claim = HttpContext.User.Claims.ElementAt(0);
            string email = claim.Value;
            var user = cRP.GetOneByEmail(email);
            try
            {
                eRP.UnJoinEvent(eventID, user.ID);

                var responseObject = new
                {
                    StatusCode = "You are no longer attending this event",
                };
                return new ObjectResult(responseObject);

            }
            catch
            {
                var responseObject = new
                {
                    error = "You are not in the guest list",
                };
                return new ObjectResult(responseObject);
            }
           

        }

        [HttpGet]
        [Route("MyEvent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult MyEvent()
        {
            ClientEventRepo ceRP = new ClientEventRepo(_context);
            ClientRepo cRP = new ClientRepo(_context);
            var claim = HttpContext.User.Claims.ElementAt(0);
            string email = claim.Value;
            var user = cRP.GetOneByEmail(email);
            var query = ceRP.GetCurrentClientEvents(user.ID);
            var responseObject = new
            {
                EventArray = query,
            };
            return new ObjectResult(responseObject);
        }

        [HttpGet]
        [Route("GetAttendees")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAttendees(int eventID)
        {
            ClientEventRepo ceRP = new ClientEventRepo(_context);
            var query = ceRP.ViewAttendees(eventID);
            var responseObject = new
            {
                EventArray = query,
            };
            return new ObjectResult(responseObject);
        }

    }
}
