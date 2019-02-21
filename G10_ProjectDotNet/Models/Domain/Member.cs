using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    /*
     * Deze klasse moet de methoden bevatten die het lid allemaal kan uitvoeren (en niet de lesgever). 
     * In andere klassen zou waar met de ApplicationUser gewoon lid bedoelt worden, ApplicationUser naar Member 
     * veranderen.
     * Voor verdere uitleg, zie het klassediagram op VPOnline.
     * */
    public class Member : ApplicationUser
    {
        public bool IsAtSession { get; set; }
        public int Score { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        

        public Member()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public void RegisterAttendance()
        {
            IsAtSession = true;
            //TODO: Alternatieve verlopen
        }
    }
}
