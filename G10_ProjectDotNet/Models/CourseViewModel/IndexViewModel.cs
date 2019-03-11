using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.CourseViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
    }
}
