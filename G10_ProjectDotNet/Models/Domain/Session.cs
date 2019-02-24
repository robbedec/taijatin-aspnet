using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public int AmountPresent => Attendances.Count;

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual Group Group { get; set; }

        public Session()
        {
            Attendances = new HashSet<Attendance>();
        }

        public void Add(Attendance attendance)
        {
            Attendances.Add(attendance);
        }
        
    }
}
