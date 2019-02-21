using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class UserGroup
    {
        public string MemberId { get; set; }
        public virtual Member Member { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
