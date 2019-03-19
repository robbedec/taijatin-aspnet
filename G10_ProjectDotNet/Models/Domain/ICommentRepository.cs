namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void SaveChanges();
    }
}