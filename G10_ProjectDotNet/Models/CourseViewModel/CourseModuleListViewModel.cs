using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.CourseViewModel
{
    public class CourseModuleListViewModel
    {
        public IEnumerable<CourseModule> CourseModules { get; set; }
    }
}
