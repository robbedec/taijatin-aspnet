using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Comment> _comments;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _comments = _dbContext.Comments;
        }
        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}