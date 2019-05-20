using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IApplicationUserRepository
    {
        ApplicationUser GetByEmail(string email);
        ApplicationUser GetUser(string username);
        void SaveChanges();
    }
}