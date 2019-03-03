using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class ConsultationOfMaterial
    {
        public int MembershipNumber { get; set; }
        public DateTime TimeOfConsulting { get; set; }
        public TeachingMaterial TeachingMaterial { get; set; }
    }
}
