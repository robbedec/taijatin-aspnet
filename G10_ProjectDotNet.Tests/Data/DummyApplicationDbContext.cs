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

        public IEnumerable<Group> Groups { get; }
        public IEnumerable<UserGroup> UserGroups { get; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; }

        public DummyApplicationDbContext()
        {
            var groep = new Group() { Day = Weekday.Maandag, Teacher = new Teacher { UserName = "Teacher" } };
            var groep1 = new Group() { Day = Weekday.Vrijdag, Teacher = new Teacher { UserName = "Teacher" } };

            Groups = new[] { groep, groep1 };
        }
    }
}
