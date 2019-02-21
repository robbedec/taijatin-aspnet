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

        public ICollection<Group> Groups { get; }
        public ICollection<UserGroup> UserGroups { get; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; }

        public DummyApplicationDbContext()
        {
            Groups = new List<Group>();


            var teacher = new Teacher { UserName = "Teacher", Email = "teacher@student.hogent.be" };
            var member = new Member { UserName = "User", Email = "user@student.hogent.be" };
            var member1 = new Member { UserName = "User1", Email = "use1r@student.hogent.be" };
            var member2 = new Member { UserName = "User2", Email = "user2@student.hogent.be" };
            var member3 = new Member { UserName = "User3", Email = "user3@student.hogent.be" };
            var admin = new Admin { UserName = "Robbe", Email = "robbe.decorte@student.hogent.be" };
            ApplicationUsers = new[] { teacher };

            var groep = new Group() { Day = Weekday.Maandag, Teacher = teacher };
            var groep1 = new Group() { Day = Weekday.Vrijdag, Teacher = teacher };
            Groups = new[] { groep, groep1 };

        }
    }
}
