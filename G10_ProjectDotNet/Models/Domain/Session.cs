using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Session
    {
        public int SessionId { get; set; }
        public ICollection<Formula> Formulas { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        public Session()
        {
            Attendances = new HashSet<Attendance>();
        }

        public void AddAttendance(Attendance attendance)
        {
            if (PastHalftime())
            {
                throw new OperationCanceledException("Je kan je niet meer registreren na de eerste leshelft");
            }
            else if (Attendances.Contains(attendance))
            {
                throw new InvalidOperationException();
            }
            Attendances.Add(attendance);
        }

        private Boolean PastHalftime()
        {
            //return DateTime.Now.TimeOfDay > Formulas.First().GetTodaysFormulaDay((int)Weekday.Woensdag).StartTime.Add(Formulas.First().GetTodaysFormulaDay((int)Weekday.Woensdag).EndTime.Subtract(Formulas.First().GetTodaysFormulaDay((int)Weekday.Woensdag).StartTime) / 2) ? true : false;
            return false;
        }
        
    }
}
