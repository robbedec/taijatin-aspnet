using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseModuleRepository
    {
        IEnumerable<CourseModule> GetByCourse(int courseId);
        CourseModule GetById(int id);
        void SaveChanges();
    }
}
