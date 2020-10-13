using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.ViewModels
{
    public class AddUserInRoleViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool IsSelected { get; set; }
    }
}
