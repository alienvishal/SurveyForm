using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLWhitelsit.Models
{
    public class Result
    {
        [Key]
        public int Res_Id { get; set; }
        [ForeignKey("ProjectInstance")]
        public int Project_Id { get; set; }
        public virtual ProjectInstance ProjectInstance { get; set; }
        [ForeignKey("Question")]
        public int Q_Id { get; set; }
        public virtual Question Question { get; set; }
        [ForeignKey("Users")]
        public string Id { get; set; }
        public virtual Users Users { get; set; }
    }
}
