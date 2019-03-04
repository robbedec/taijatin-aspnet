using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Formula Formula { get; set; }
        public int AmountEnrolled => Formula.Members.Count;

        public Session()
        {
        }

        private Boolean PastHalftime()
        {
            return DateTime.Now > StartDate.Add(EndDate.Subtract(StartDate) / 2) ? true : false;
        }
        
    }
}
