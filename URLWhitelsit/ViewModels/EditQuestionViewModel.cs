using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.ViewModels
{
    public class EditQuestionViewModel
    {
        public int Q_Id { get; set; }
        [Required(ErrorMessage = "Please Enter URL")]
        [Display(Name = "URL Text")]
        public string Q_Text { get; set; }
    }
}
