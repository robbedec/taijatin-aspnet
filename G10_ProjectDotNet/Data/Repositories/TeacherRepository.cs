using System.Linq;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Teacher> _teachers;

        public TeacherRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _teachers = dbContext.Teachers;
        }

        public Teacher GetByUserName(string userName)
        {
            return _teachers.Include(u => u.Address).Include(u => u.Formulas).Where(u => u.UserName == userName).SingleOrDefault();
        }
        
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}