using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Session
    {
        public int SessionId { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual Group Group { get; set; }
        
    }
}
