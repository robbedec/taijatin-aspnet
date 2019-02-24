namespace G10_ProjectDotNet.Models.Domain
{
    public interface IApplicationUserRepository
    {
        ApplicationUser GetUser(string username);
    }
}