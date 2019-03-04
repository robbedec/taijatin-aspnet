using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.SessionViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Session> Sessions { get; set; }
    }
}
