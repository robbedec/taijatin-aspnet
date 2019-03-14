using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class CourseModule
    {
        public int CourseModuleId { get; set; }
        public string Name { get; set; }
        public Course Course { get; set; }
        public TypeOfExcersise TypeOfExcersise { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public AudioFile AudioFile { get; set; }
    }
}
