using System.Collections.Generic;
using G10_ProjectDotNet.Models.Domain;

namespace G10_ProjectDotNet.Models.CourseViewModel
{
    public class CourseModuleViewerModel
    {
        public IEnumerable<CourseModule> Modules { get; set; }
    }
}