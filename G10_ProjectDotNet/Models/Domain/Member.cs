using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    /*
     * A Member is an ApplicationUser
     * */
    public class Member : ApplicationUser
    {
        public int Score { get; set; }
        public int FormulaId { get; set; }
        public virtual Formula Formula { get; set; }
        public Grade Grade { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

        public Member()
        {
            Formula = new Formula();
            Attendances = new HashSet<Attendance>();
        }
    }
}
