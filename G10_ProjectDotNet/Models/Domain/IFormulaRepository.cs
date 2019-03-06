using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IFormulaRepository
    {
        IEnumerable<Formula> GetAll();
        Formula GetById(int formulaId);
        Formula GetByName(string formulaName);
        Formula GetLinkedMembers(int formulaId);
        void SaveChanges();
    }
}
