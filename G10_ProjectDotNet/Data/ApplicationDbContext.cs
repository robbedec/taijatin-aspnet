using G10_ProjectDotNet.Data.Mappers;
using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<ApplicationUser> Gebruikers { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserGroupConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            // Change the name of the table to be Users instead of AspNetUsers
            builder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}
