using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLWhitelsit.Models
{
    public class SurveyDBContext:IdentityDbContext<Users>
    {
        public SurveyDBContext(DbContextOptions<SurveyDBContext> options):base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ProjectInstance> ProjectInstances { get; set; }
    }
}
