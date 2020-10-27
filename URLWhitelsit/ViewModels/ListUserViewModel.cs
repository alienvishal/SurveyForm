using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLWhitelsit.Models;

namespace URLWhitelsit.ViewModels
{
    public class ListUserViewModel
    {
        public string Email { get; set; }
        public string ProjectInstance { get; set; }
        public string Id { get; set; }
        public int Keep { get; set; }
        public int Discard { get; set; }
    }
}
