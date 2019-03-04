using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Formula
    {
        [Display(Name = "Formule Nummer")]
        public int FormulaId { get; set; }
        [Display(Name = "Deg(en)")]
        public List<FormulaDay> Days { get; set; }
        [Display(Name = "Leraar")]
        public virtual Teacher Teacher { get; set; }
        public ICollection<Member> Members { get; set; }



        public Formula()
        {
            Members = new HashSet<Member>();
        }
    }
}
