using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Session> _sessions;

        public SessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _sessions = dbContext.Sessions;
        }

        public Session GetCurrentSession()
        {
            return _sessions.Where(b => b.StartDate < DateTime.Now && b.EndDate > DateTime.Now).Include(b => b.Group).Include(b => b.Attendances).SingleOrDefault();
        }

        public void Add(Session session)
        {
            _sessions.Add(session);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
