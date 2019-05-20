using System.Linq;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Member> _members;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _members = dbContext.Members;
        }

        public Member GetByUserName(string userName)
        {
            return _members.Where(u => u.UserName == userName).SingleOrDefault();
        }

        public Member GetById(int memberId)
        {
            return _members.Where(u => u.Id == memberId).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}