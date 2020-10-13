using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using URLWhitelsit.Models;

namespace URLWhitelsit.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Instance-ID")]
        [Required]
        public int SelectedInstance { get; set; }
        [Display(Name = "Daimler Email Address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
