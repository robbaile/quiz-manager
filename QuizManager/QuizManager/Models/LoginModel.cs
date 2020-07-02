using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizManager.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username and password")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username between 3 and 30 characters" )]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your username and password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Password between 3 and 20 characters")]
        public string Password { get; set; }
    }
}
