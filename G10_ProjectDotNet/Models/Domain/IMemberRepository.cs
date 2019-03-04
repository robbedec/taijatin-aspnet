using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IMemberRepository
    {
        List<Member> GetMembersFromFormula(int formulaId);
        Member UpdateAttendancy(int memberId);
        void SaveChanges();
    }
}