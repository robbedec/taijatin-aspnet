using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.SessionViewModels
{
    public class IndexViewModel
    {
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public Weekday Day => Session.Group.Day;
        public string Date => Session.StartDate.Date.ToShortDateString();
        public string Start => Session.StartDate.TimeOfDay.ToString("hh\\:mm");
        public string End => Session.EndDate.TimeOfDay.ToString("hh\\:mm");

        public Boolean AlreadyRegistered(int memberId)
        {
            return Session.Attendances.Where(b => b.MemberId == memberId).SingleOrDefault() == null;
        }
    }
}
