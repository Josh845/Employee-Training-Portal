using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Employee_Training_Portal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employee_Training_Portal.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Employer> Employer { get; set; }

        public DbSet<Progress> Progress { get; set; }

        public DbSet<UploadFile> UploadFile { get; set; }
    }
}
