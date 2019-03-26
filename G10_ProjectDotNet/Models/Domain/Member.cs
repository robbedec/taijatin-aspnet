using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
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


        public void AddPoints()
        {
            Score += Formula.Days.Count == 1 ?  10 : 5;
            
        }
    }
}
