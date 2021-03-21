using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.ViewModels
{
    public class viewAttendeesVM
    {
        public string eventName { get; set; }
        public int eventID { get; set; }
        public DateTime eventDate { get; set; }
        public string eventTime { get; set; }
        public List<ClientEventVM> guestList { get; set; }
    }
}
