using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetAll();
        Group GetById(int groupId);
        void SaveChanges();
    }
}
