using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        public Grade Grade { get; set; }
        public TypeOfExcersize Type { get; set; }
        public ICollection<TeachingMaterial> TeachingMaterials { get; set; }
    }
}
