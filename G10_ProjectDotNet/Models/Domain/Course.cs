using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Course
    {
        public int CourseId { get; set; }
        public Grade Grade { get; set; }
        public TypeOfExcersize Type { get; set; }
        public ICollection<CourseModule> Modules { get; set; }
    }
}
