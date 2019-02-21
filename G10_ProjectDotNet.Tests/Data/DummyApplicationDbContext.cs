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

        }
    }
}
