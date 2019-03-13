using System.Collections.Generic;
using System.Linq;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class CourseModuleViewerRepository : ICourseModuleViewerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CourseModuleViewer> _courseModuleViewers;

        public CourseModuleViewerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _courseModuleViewers = dbContext.CourseModuleViewers;
        }


        public IEnumerable<CourseModuleViewer> GetMembersByCourseModule(int courseModuleId)
        {
            return _courseModuleViewers.Where(b => b.CourseModuleId == courseModuleId).Include(b => b.MemberId);
        }

        public void AddViewer(CourseModuleViewer courseModuleViewer)
        {
            CourseModuleViewer c = _courseModuleViewers.Find(courseModuleViewer.CourseModuleId, courseModuleViewer.MemberId);
            if (c == null)
            {
                _courseModuleViewers.Add(courseModuleViewer);
            }
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}