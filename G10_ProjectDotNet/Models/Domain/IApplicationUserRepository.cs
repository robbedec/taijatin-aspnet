using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IApplicationUserRepository
    {
        ApplicationUser GetByEmail(string email);
        ApplicationUser GetUser(string username);
        string GetEmail(string email);
        string GetUserName(string username);
        string GetType(string username);
        void SaveChanges();
    }
}