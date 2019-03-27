using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.SessionViewModels
{
    public class IndexViewModel
    {
        public Session Session { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public bool RegistrationEnded { get; set; } = false;

        public Boolean AlreadyRegistered(int memberId)
        {
            return Session.Attendances.Where(b => b.MemberId == memberId).SingleOrDefault() == null;
        }
    }
}
