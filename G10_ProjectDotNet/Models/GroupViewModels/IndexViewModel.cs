using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.GroupViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<UserGroup> UserGroups { get; set; }
       // public IEnumerable< MyProperty { get; set; }
    }
}
