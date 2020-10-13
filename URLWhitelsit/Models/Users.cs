using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLWhitelsit.Models
{
    public class Users:IdentityUser
    {
        [ForeignKey("ProjectInstance")]
        public int project_Id { get; set; }
        public virtual ProjectInstance ProjectInstance { get; set; }
    }
}
