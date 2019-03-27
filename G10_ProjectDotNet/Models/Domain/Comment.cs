using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int CourseModuleId { get; set; } 
        public CourseModule CourseModule { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public ICollection<CommentReply> Replies { get; set; } = new List<CommentReply>();

        public void AddReply(CommentReply reply)
        {
            Replies.Add(reply);
        }
    }
}