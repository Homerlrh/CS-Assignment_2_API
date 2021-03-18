using Assignment2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Repositories
{
    public class EventRepo
    {
        ApplicationDbContext db;

        public EventRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public bool Create(string eventName , string description, DateTime date, string time)
        {
            Event newEvent = new Event()
            {
                EventName = eventName,
                Description = description,
                Date = date,
                Time = time
            };

            db.Events.Add(newEvent);
            db.SaveChanges();

            return true;
        }

        public bool Delete(int eventID)
        {
            Event Event = db.Events.Where(e =>e.ID == eventID).FirstOrDefault();
            db.Remove(Event); 
            db.SaveChanges();
            return true;
        }

        public bool JoinEvent (int eventID , int userID)
        {
            JoinedEvent joinEvent = new JoinedEvent { ClientID = userID, EventID = eventID };
            db.JoinedEvents.Add(joinEvent);
            db.SaveChanges();
            return true;
        }

        public bool UnJoinEvent(int eventID , int userID)
        {
            JoinedEvent joinEvent = db.JoinedEvents.Where(je => je.ClientID == userID && je.EventID == eventID).FirstOrDefault();
            db.Remove(joinEvent);
            db.SaveChanges();
            return true;
        }
    }
}
