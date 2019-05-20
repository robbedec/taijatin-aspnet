namespace G10_ProjectDotNet.Models.Domain
{
    public interface ITeacherRepository
    {
        Teacher GetByUserName(string userName);
        void SaveChanges();
    }
}