namespace G10_ProjectDotNet.Models.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public CourseModule CourseModule { get; set; }
        public Member Member { get; set; }
    }
}