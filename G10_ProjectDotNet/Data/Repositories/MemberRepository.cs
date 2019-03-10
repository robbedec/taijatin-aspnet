using System.Collections.Generic;
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

        public List<Member> GetMembersFromFormula(int formulaId)
        {
            return _members.Where(u => u.FormulaId == formulaId).ToList();
        }

        public List<Member> GetAll()
        {
            return _members.ToList();
        }

        public Member GetById(int id)
        {
            return _members.Where(u => u.Id == id).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}