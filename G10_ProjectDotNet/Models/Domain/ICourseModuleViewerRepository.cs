using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseModuleViewerRepository
    {
        void AddViewer(CourseModuleViewer courseModuleViewer);
        void SaveChanges();
    }
}