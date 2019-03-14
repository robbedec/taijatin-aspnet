using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseModuleViewerRepository
    {
        IEnumerable<CourseModuleViewer> GetMembersByCourseModule(int courseModuleId);
        void AddViewer(CourseModuleViewer courseModuleViewer);
        void SaveChanges();
    }
}