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

        public List<Session> GetSessionsToday()
        {
            int today = (int)System.DateTime.Now.DayOfWeek;
            return _sessions.Where(s => s.Day == (Weekday)today).Include(b => b.Formula).Include(b => b.Formula.Members).ToList();
            //return _sessions.Where(b => b.StartDate < DateTime.Now && b.EndDate > DateTime.Now).Include(b => b.Formula).Include(b => b.Formula.Members).ToList();
        }

        public void Add(Session session)
        {
            _sessions.Add(session);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Session GetBy(int id)
        {
            return _dbContext.Sessions.Where(b => b.SessionId == id).Include(b => b.Formula).Include(b => b.Formula.Members).Include(b => b.Attendances).SingleOrDefault();
        }
    }
}
