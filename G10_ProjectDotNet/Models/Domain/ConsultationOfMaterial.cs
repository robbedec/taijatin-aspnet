using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class ConsultationOfMaterial
    {
        public int MemberId { get; set; }
        public DateTime Timestamp { get; set; }
        public TeachingMaterial TeachingMaterial { get; set; }
    }
}
