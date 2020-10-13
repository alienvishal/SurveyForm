using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.Models
{
    public class Question
    {
        [Key]
        public int Q_Id { get; set; }
        public string Q_Text { get; set; }
    }
}
