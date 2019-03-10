using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.CourseViewModel
{
    public class CategoryViewModel
    {
        public Grade Grade { get; set; }
        public IEnumerable<TypeOfExcersise> Types { get; set; }
    }
}
