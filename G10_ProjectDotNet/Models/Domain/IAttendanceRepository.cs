using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetAll();
        IEnumerable<Attendance> GetByGroup(int groupId);
        IEnumerable<Attendance> GetByMember(int memberId);
        void Add(Attendance attendance);
        void SaveChanges();
    }
}
