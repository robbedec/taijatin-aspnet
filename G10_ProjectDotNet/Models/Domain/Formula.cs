using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Formula
    {
        public int FormulaId { get; set; }
        public ICollection<FormulaFormulaDay> Days { get; set; }
        public string FormulaName { get; set; }
        public virtual Teacher Teacher { get; set; }
        public ICollection<Member> Members { get; set; }

        public Formula()
        {
            Members = new HashSet<Member>();
            Days = new HashSet<FormulaFormulaDay>();
        }
    }
}
