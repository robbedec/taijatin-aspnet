using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.CourseViewModel
{
    public class IndexViewModel
    {
        public int MemberId { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<CourseModule> Modules { get; set; } = null;
        public CourseModule CourseModule { get; set; }
        public int? CourseModuleId { get; set; }
        public string CommentTextInput { get; set; }
    }
}
