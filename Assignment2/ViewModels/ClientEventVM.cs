using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.ViewModels
{
    public class ClientEventVM
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
