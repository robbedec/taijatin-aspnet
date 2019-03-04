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
        public int Score { get; set; }
        public bool Attendancy { get; set; }
        public int FormulaId { get; set; }
        public virtual Formula Formula { get; set; }
        

        public Member()
        {
            Formula = new Formula();
        }
    }
}
