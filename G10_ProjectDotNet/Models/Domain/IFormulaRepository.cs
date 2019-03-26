using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public interface IFormulaRepository
    {
        IEnumerable<Formula> GetByWeekDay(int WeekdayId);
        void SaveChanges();
    }
}
