using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class CourseAccess
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
