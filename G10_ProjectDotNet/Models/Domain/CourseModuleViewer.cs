namespace G10_ProjectDotNet.Models.Domain
{
    public class CourseModuleViewer
    {
        public int CourseModuleViewerId { get; set; }
        public int CourseModuleId { get; set; }
        public virtual CourseModule CrouseModule { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}