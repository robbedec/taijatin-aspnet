using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IMemberRepository
    {
        List<Member> GetMembersFromFormula(int formulaId);
        List<Member> GetAll();
        Member UpdateAttendancy(int memberId);
        void SaveChanges();
    }
}