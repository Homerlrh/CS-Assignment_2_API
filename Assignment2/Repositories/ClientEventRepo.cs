using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.ViewModels;

namespace Assignment2.Repositories
{

    public class ClientEventRepo
    {
        ApplicationDbContext db;

        public ClientEventRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public IQueryable<ClientEventVM> GetAll()
        {
            var query = from JE in db.JoinedEvents
                        from C in db.Clients
                        where JE.ClientID == C.ID
                        select new
                        {
                            clientID = C.ID,
                            userName = C.firstName + " " + C.lastName,
                            eventID = JE.EventID
                        };

            var query2 = from E in db.Events
                         from Q in query
                         where E.ID == Q.eventID
                         select new ClientEventVM()
                         {
                             userName = Q.userName,
                             UserID = Q.clientID,
                             EventID = E.ID,
                             Date = E.Date,
                             Time = E.Time,
                             Description = E.Description,
                             EventName = E.EventName
                         };

            return query2;

        }

        public IQueryable<ClientEventVM> GetCurrentClientEvents(int clientID)
        {
            var query = GetAll().Where(q => q.UserID == clientID);
            return query;
        }

        public viewAttendeesVM ViewAttendees(int eventID)
        {
            var all = GetAll();
            var query = all.AsEnumerable()
                .GroupBy(x => new { x.EventName, x.EventID, x.Date, x.Time })
                .Select(x => new viewAttendeesVM()
                    {
                        eventName = x.Key.EventName,
                        eventID = x.Key.EventID,
                        eventDate = x.Key.Date,
                        eventTime = x.Key.Time,
                        guestList = x.ToList()
                    }
                );
            var filter = query.Where(x => x.eventID == eventID).FirstOrDefault();
            return filter;
        }

    }
}
