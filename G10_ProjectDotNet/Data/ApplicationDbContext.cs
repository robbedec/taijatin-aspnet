using G10_ProjectDotNet.Data.Mappers;
using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<FormulaFormulaDay> Formula_FormulaDays { get; set; }
        public DbSet<ApplicationUser> Gebruikers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> CourseModules { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
        public DbSet<CourseModuleViewer> CourseModuleViewers { get; set; }

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new AttendanceConfiguration());
            builder.ApplyConfiguration(new Formula_FormulaDayConfiguration());
            builder.ApplyConfiguration(new CourseModuleConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());

            builder.Ignore<SessionState>();

            builder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}
