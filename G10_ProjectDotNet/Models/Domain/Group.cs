using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Group
    {
        [Display(Name = "Groepnummer")]
        public int GroupId { get; set; }
        [Display(Name = "Dag")]
        public Weekday Day { get; set; }
        [Display(Name = "Leraar")]
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Member> Members { get; set; }



        public Group()
        {
            Members = new HashSet<Member>();
        }
    }
}
