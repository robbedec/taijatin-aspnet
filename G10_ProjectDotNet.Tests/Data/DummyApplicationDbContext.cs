using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace G10_ProjectDotNet.Tests.Data
{
    public class DummyApplicationDbContext
    {
        // Contains all test cases

        public IEnumerable<ApplicationUser> ApplicationUsers { get; }
        public IEnumerable<Session> Sessions { get; }
        public IEnumerable<Formula> Formulas { get; }
        public Session Session { get; }
        public Session SessionPastHalftime { get; }
        public Attendance Attendance { get; }

        public DummyApplicationDbContext()
        {

            var adress1 = new Address() { City = "Brugge", ZipCode = 8000, Street = "Noordzandstraat", Number = 1 };
            var adress2 = new Address() { City = "Zedelgem", ZipCode = 8210, Street = "Leliestraat", Number = 76 };
            var adress3 = new Address() { City = "Gent", ZipCode = 9000, Street = "Overpoortstraat", Number = 65 };

            var teacher = new Teacher { UserName = "Teacher", Email = "teacher@student.hogent.be", Firstname = "Teacher", Lastname = "Decorte", Address = adress3, Birthday = new DateTime(1950, 11, 29), PhoneNumber = "0498696969" };
            var member = new Member { UserName = "User", Email = "user@student.hogent.be", Firstname = "User", Lastname = "User", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
            var member1 = new Member { UserName = "User1", Email = "use1r@student.hogent.be", Firstname = "User", Lastname = "1", Address = adress2, Birthday = new DateTime(1999, 1, 9), PhoneNumber = "0498696969" };
            var member2 = new Member { UserName = "User2", Email = "user2@student.hogent.be", Firstname = "User", Lastname = "2", Address = adress3, Birthday = new DateTime(1999, 5, 6), PhoneNumber = "0498696969" };
            ApplicationUsers = new ApplicationUser[] { teacher, member, member1, member2 };


            var formulaDay1 = new FormulaDay() { Day = Weekday.Woensdag };
            var formulaDay2 = new FormulaDay() { Day = Weekday.Maandag };
            var formulaDay3 = new FormulaDay() { Day = Weekday.Zaterdag };

            var groep = new Formula() { Days = new List<FormulaDay> { formulaDay1, formulaDay3 }, Teacher = teacher, Members = new List<Member> { member } };
            var groep1 = new Formula() { Days = new List<FormulaDay> { formulaDay2, formulaDay3 }, Teacher = teacher, Members = new List<Member> { member1, member2 } };
            Formulas = new[] { groep, groep1 };

            var attendance = new Attendance { Member = member };
            var attendance1 = new Attendance { Member = member1 };
            Attendance = attendance;

            Session = new Session { StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.Now.AddHours(2), Formula = groep1, Attendances = new List<Attendance> { attendance, attendance1 } };
            SessionPastHalftime = new Session { StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now.AddHours(1), Formula = groep1, Attendances = new List<Attendance> { attendance } };

            Sessions = new[] { Session, SessionPastHalftime };
        }
    }
}
