using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetByMinGrade(Grade grade);
        void SaveChanges();
    }
}
