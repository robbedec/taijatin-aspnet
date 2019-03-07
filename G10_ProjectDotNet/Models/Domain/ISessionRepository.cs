using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ISessionRepository
    {
        List<Session> GetSessionsToday();
        Session GetBy(int id);
        void Add(Session session);
        void SaveChanges();
    }
}
