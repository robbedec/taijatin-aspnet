using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class CourseModule
    {
        public int CouseModuleId { get; set; }
        public string Name { get; set; }
        public Grade Grade { get; set; }
        public TypeOfExcersise Type { get; set; }
        public DateTime LastConsultation { get; set; }
        public int TotalConsultations { get; set; }
    }
}
