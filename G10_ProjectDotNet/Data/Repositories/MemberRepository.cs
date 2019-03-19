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

        public Member GetById(int memberId)
        {
            return _members.Where(u => u.Id == memberId).SingleOrDefault();
        }

        public Member GetByName(string name) 
        {
            return _members.Where(m => m.Firstname == name).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}