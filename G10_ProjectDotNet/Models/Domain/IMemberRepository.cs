namespace G10_ProjectDotNet.Models.Domain
{
    public interface IMemberRepository
    {
        Member GetById(int memberId);
        void SaveChanges();
    }
}