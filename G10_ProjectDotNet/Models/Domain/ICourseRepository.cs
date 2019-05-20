using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetByMinGrade(int grade);
        void SaveChanges();
    }
}
