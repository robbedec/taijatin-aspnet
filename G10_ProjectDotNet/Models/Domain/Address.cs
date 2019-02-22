using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Address
    {
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
