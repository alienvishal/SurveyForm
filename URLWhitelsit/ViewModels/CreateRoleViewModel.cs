using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Please Enter the Role Name")]
        public string RoleName { get; set; }
    }
}
