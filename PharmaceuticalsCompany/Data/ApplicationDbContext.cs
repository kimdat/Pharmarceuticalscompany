using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalsCompany.Models.Career;

namespace PharmaceuticalsCompany.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EducationCareer> EducationCareers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EducationCareer>()
                .HasKey(c => new { c.User_Id ,c.Education_Id});
        }
    }
}
