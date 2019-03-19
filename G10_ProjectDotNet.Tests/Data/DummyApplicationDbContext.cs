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
        public Session SessionFinished { get; }
        public Session SessionLastWeek { get; }
        public Attendance Attendance { get; }

        public DummyApplicationDbContext()
        {

            var adress1 = new Address() { City = "Brugge", ZipCode = 8000, Street = "Noordzandstraat", Number = 1 };
            var adress2 = new Address() { City = "Zedelgem", ZipCode = 8210, Street = "Leliestraat", Number = 76 };
            var adress3 = new Address() { City = "Gent", ZipCode = 9000, Street = "Overpoortstraat", Number = 65 };

            var teacher = new Teacher { UserName = "Teacher", Email = "teacher@student.hogent.be", Firstname = "Teacher", Lastname = "Decorte", Address = adress3, Birthday = new DateTime(1950, 11, 29), PhoneNumber = "0498696969" };
            var member = new Member { UserName = "User", Email = "user@student.hogent.be", Firstname = "User", Lastname = "User", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
            var member1 = new Member { Id = 2, UserName = "User1", Email = "use1r@student.hogent.be", Firstname = "User", Lastname = "1", Address = adress2, Birthday = new DateTime(1999, 1, 9), PhoneNumber = "0498696969" };
            var member2 = new Member { Id = 3, UserName = "User2", Email = "user2@student.hogent.be", Firstname = "User", Lastname = "2", Address = adress3, Birthday = new DateTime(1999, 5, 6), PhoneNumber = "0498696969" };
            ApplicationUsers = new ApplicationUser[] { teacher, member, member1, member2 };


            var formulaDay1 = new FormulaDay() { Day = Weekday.Woensdag, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(18, 0, 0) };
            var formulaDay2 = new FormulaDay() { Day = Weekday.Maandag, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(18, 0, 0) };
            var formulaDay3 = new FormulaDay() { Day = Weekday.Zaterdag, StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(18, 0, 0) };

            var formula = new Formula()
            {
                Teacher = teacher,
                Members = new List<Member> { member },
                Days = new List<FormulaFormulaDay>()
                {
                    new FormulaFormulaDay{ FormulaDay = formulaDay1 }
                }
            };

            var formula1 = new Formula()
            {
                Teacher = teacher,
                Members = new List<Member> { member1, member2 },
                Days = new List<FormulaFormulaDay>()
                {
                    new FormulaFormulaDay{ FormulaDay = formulaDay2 },
                    new FormulaFormulaDay{ FormulaDay = formulaDay3 }
                }
            };

            Formulas = new[] { formula1 };


            var attendance = new Attendance { Member = member };
            var attendance1 = new Attendance { Member = member1 };
            Attendance = attendance;

            Session = new Session { Day = Weekday.Maandag, Attendances = new List<Attendance> { attendance, attendance1 }, SessionEnded = false, Date = DateTime.Now.Date };
            SessionFinished = new Session { Day = Weekday.Maandag, Attendances = new List<Attendance> { attendance }, SessionEnded = true, Date = DateTime.Now.Date };
            SessionLastWeek = new Session { Day = Weekday.Maandag, Attendances = new List<Attendance> { attendance, attendance1 }, SessionEnded = true, Date = DateTime.Now.Date.AddDays(-7) };

            Sessions = new[] { Session, SessionFinished };
        }

        public ApplicationUser GetApplicationUser()
        {
            var adress1 = new Address() { City = "Brugge", ZipCode = 8000, Street = "Noordzandstraat", Number = 1 };
            return new Member { UserName = "User", Email = "user@student.hogent.be", Firstname = "User", Lastname = "User", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
        }
    }
}
