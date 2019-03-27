namespace G10_ProjectDotNet.Models.Domain
{
    public class CommentReply
    {
        public int CommentReplyId { get; set; }
        public string ReplyText { get; set; }
        public Comment Comment { get; set; }
        public Member Member { get; set; }
    }
}