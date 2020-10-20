using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.ViewModels
{
    public class AddFormSubmitDateViewModel
    {
        [Required(ErrorMessage = "Please Select Date")]
        [DataType(DataType.DateTime)]
        public DateTime FormDate { get; set; }
    }
}
