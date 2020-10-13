using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.ViewModels
{
    public class AddQuestionViewModel
    {
        [Required(ErrorMessage = "Please Enter URL")]
        [Display(Name = "URL Text")]
        public string UrlText { get; set; }
    }
}
