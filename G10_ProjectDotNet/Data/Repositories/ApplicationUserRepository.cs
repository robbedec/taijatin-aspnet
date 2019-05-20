using System.Diagnostics;
using System.Linq;
using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ApplicationUser> _users;

        public ApplicationUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Gebruikers;
        }

        public ApplicationUser GetByEmail(string email)
        {
            return _users.Include(u => u.Address).Where(u => u.Email == email).SingleOrDefault();
        }

        public ApplicationUser GetUser(string username)
        {
            Trace.WriteLine(username);
            return _users.Include(u => u.Address).Where(u => u.UserName == username).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}