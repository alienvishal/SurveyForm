
using System.ComponentModel.DataAnnotations;

namespace URLWhitelsit.Models
{
    public class ProjectInstance
    {
        [Key]
        public int Project_Id { get; set; }
        public string ProjectName { get; set; }
    }
}
