using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class CourseModuleRepository : ICourseModuleRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CourseModule> _courseModules;

        public CourseModuleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _courseModules = _dbContext.CourseModules;
        }

        public IEnumerable<CourseModule> GetByCourse(int courseId)
        {
            return _courseModules.Where(b => b.Course.CourseId == courseId).ToList();
        }

        public CourseModule GetById(int id)
        {
            return _courseModules.Where(c => c.CourseModuleId == id).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
