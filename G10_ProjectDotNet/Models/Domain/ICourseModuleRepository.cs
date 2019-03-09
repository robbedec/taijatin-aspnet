using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseModuleRepository
    {
        IEnumerable<CourseModule> GetAll();
        CourseModule GetById(int id);
        IEnumerable<CourseModule> GetByGrade(Grade grade);
        IEnumerable<CourseModule> GetByType(TypeOfExcersise type);
        void SaveChanges();
    }
}
