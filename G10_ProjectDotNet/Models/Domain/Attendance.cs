using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        public int SessionId { get; set; }
        public virtual Session Session { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

    }
}
