using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Session
    {
        public int SessionId { get; set; }
        public Weekday Day { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public bool SessionEnded { get; set; }
      
        public Session()
        {
            // Attendances = new HashSet<Attendance>();
        }

        public void AddAttendance(Attendance attendance)
        {
            if (AlreadyRegistered(attendance.MemberId))
            {
                throw new InvalidOperationException();
            }
            Attendances.Add(attendance);
        }

        public bool AlreadyRegistered(int memberId)
        {
            return !(Attendances.Where(b => b.MemberId == memberId).SingleOrDefault() == null);
        }
    }
}
