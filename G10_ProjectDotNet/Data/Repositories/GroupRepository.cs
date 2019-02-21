using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Group> _groups;

        public GroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _groups = dbContext.Groups;
        }

        public IEnumerable<Group> GetAll()
        {
            return _groups.Include(b => b.Teacher).Include(b => b.UserGroups).ToList();
        }

        public IEnumerable<UserGroup> GetLinkedUserGroups(int groupId)
        {
            var selectedGroup = _groups.Where(x => x.GroupId == groupId).Single();
            _dbContext.Entry(selectedGroup).Collection(x => x.UserGroups).Load();
            foreach (UserGroup userGroup in selectedGroup.UserGroups)
            {
                _dbContext.Entry(userGroup).Reference(x => x.Member).Load();
            }
            return selectedGroup.UserGroups;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
