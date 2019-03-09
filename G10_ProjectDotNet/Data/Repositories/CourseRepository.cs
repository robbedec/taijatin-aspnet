using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Course> _courses;

        public CourseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _courses = _dbContext.Courses;
        }

        public IEnumerable<Course> GetAll()
        {
            return _courses.Include(c => c.Modules).ToList();
        }

        public IEnumerable<Course> GetByGrade(Grade grade)
        {
            return _courses.Include(c => c.Modules).Where(c => c.Grade == grade).ToList();
        }

        public IEnumerable<Course> GetByType(TypeOfExcersise type)
        {
            return _courses.Include(c => c.Modules).Where(c => c.Type == type).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
