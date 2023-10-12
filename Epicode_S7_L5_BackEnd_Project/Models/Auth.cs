using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epicode_S7_L5_BackEnd_Project.Models
{
    public class Auth
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}