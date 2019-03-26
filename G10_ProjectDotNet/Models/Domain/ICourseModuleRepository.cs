using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface ICourseModuleRepository
    {
        IEnumerable<CourseModule> GetByCourse(int courseId);
        CourseModule GetById(int? courseModuleId);
        void AddComment(Comment comment, int courseModuleId);
        Comment GetComment(int courseModuleId, int commentId);
        void RemoveComment(int courseModuleId, Comment comment);
        void AddCommentReply(CommentReply reply, int commentId, int courseModuleId);
        void SaveChanges();
    }
}
