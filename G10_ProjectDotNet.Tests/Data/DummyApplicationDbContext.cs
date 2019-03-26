using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Newtonsoft.Json;
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
        public IEnumerable<Course> Courses { get; }
        public IEnumerable<CourseModule> CourseModules { get; }
        public Session Session { get; }
        public Session SessionFinished { get; }
        public Session SessionLastWeek { get; }
        public Attendance Attendance { get; }
        public CourseModule CourseModule { get; }

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

            Session = new Session { Day = Weekday.Maandag, Attendances = new List<Attendance> { attendance, attendance1 }, Date = DateTime.Now.Date };
            Session.StateSerialized = JsonConvert.SerializeObject(new RegistrationState(Session).GetType());
            Session.State = new RegistrationState(Session);
            SessionFinished = new Session { Day = Weekday.Maandag, Attendances = new List<Attendance> { attendance }, Date = DateTime.Now.Date };
            SessionFinished.StateSerialized = JsonConvert.SerializeObject(new SessionEndedState(SessionFinished).GetType());
            SessionLastWeek = new Session { Day = Weekday.Maandag, Attendances = new List<Attendance> { attendance, attendance1 }, Date = DateTime.Now.Date.AddDays(-7) };

            Sessions = new[] { Session, SessionFinished };

            CourseModule = new CourseModule
            {
                CourseModuleId = 1,
                Name = "Groeten bij Jiu-jitsu",
                Text = "",
                Url = "https://www.youtube.com/embed/VsNh_8KQ_II",
                ImageUrl = "http://jiu-jitsu-gent.be/wp-content/uploads/2016/09/groeten.png",
                ImageAlt = "Verschillende groeten"
            };

            var courseModule1 = new CourseModule
            {
                CourseModuleId = 2,
                Name = "Jiu-jitsu: Een kennismaking",
                Text = "De Taijitan methode is een zeer oude Jiu-Jitsu methode, mogelijks de oudste en is equivlent aan de Yawara stijl. Deze zijn niet alleen origineel maar tevens enorm hard vermits zij zich focussen op zelfverdediging. De Taijitan stijl wordt gekenmerkt door onder andere accenten van Karate, Taekwondo, Judo en Aikido. ...",
                ImageUrl = "http://jiu-jitsu-gent.be/wp-content/uploads/2016/09/groeten.png",
                ImageAlt = "Verschillende groeten"
            };

            var course1 = new Course { CourseId = 1, MinGrade = (Grade)4, Modules = new List<CourseModule> { CourseModule, courseModule1 } };
            var course2 = new Course { CourseId = 2, MinGrade = (Grade)3, Modules = new List<CourseModule> { courseModule1 } };

            Courses = new[] { course1, course2 };

        }

        public ApplicationUser GetApplicationUser()
        {
            var adress1 = new Address() { City = "Brugge", ZipCode = 8000, Street = "Noordzandstraat", Number = 1 };
            return new Member { UserName = "User", Email = "user@student.hogent.be", Firstname = "User", Lastname = "User", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
        }
    }
}
