using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetByMinGrade(Grade grade);
        IEnumerable<Course> GetByType(TypeOfExcersise type);
        IEnumerable<Course> GetAll();
        void SaveChanges();
    }
}
