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
        public string FormulaName { get; set; }
        [Display(Name = "Leraar")]
        public virtual Teacher Teacher { get; set; }
        public ICollection<Member> Members { get; set; }
        public ICollection<Session> Sessions { get; set; }

        public Formula()
        {
            Members = new HashSet<Member>();
        }

        public FormulaDay GetTodaysFormulaDay()
        {
            int day = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;
            return Days.Where(b => b.Day == (Weekday)day).SingleOrDefault();
        }
    }
}
