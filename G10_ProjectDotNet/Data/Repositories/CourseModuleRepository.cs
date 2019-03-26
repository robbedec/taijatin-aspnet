using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            return _courseModules.Where(b => b.Course.CourseId == courseId).ToList().OrderBy(b => b.Name);
        }

        public CourseModule GetById(int? id)
        {
            return _courseModules.Where(c => c.CourseModuleId == id).Include(c => c.Comments).ThenInclude(c => c.Replies).Include(c => c.Comments).ThenInclude(c => c.Member).SingleOrDefault();
        }

        public void AddComment(Comment comment, int courseModuleId)
        {
            _courseModules.Where(c => c.CourseModuleId == courseModuleId).SingleOrDefault().AddComment(comment);
        }

        public Comment GetComment(int courseModuleId, int commentId) 
        {
            return GetById(courseModuleId).GetComment(commentId);
        }

        public void RemoveComment(int courseModuleId, Comment comment) 
        {
            GetById(courseModuleId).RemoveComment(comment);
        }

        public void AddCommentReply(CommentReply reply, int commentId, int courseModuleId) 
        {
            Comment c = GetById(courseModuleId).GetComment(commentId);
            c.AddReply(reply);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
