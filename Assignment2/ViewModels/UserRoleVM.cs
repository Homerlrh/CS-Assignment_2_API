using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.ViewModels
{
    public class UserRoleVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
