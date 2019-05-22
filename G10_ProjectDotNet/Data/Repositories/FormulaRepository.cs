using G10_ProjectDotNet.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace G10_ProjectDotNet.Data.Repositories
{
    public class FormulaRepository : IFormulaRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Formula> _formulas;

        public FormulaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _formulas = dbContext.Formulas;
        }

        public IEnumerable<Formula> GetByWeekDay(int WeekdayId)
        {
            System.Console.WriteLine(WeekdayId);
            //return _formulas.Where(x => x.FormulaId == 20).Include(b => b.Members).ToList();
            return _formulas.Where(b => b.Days.Any(c => (int)c.FormulaDay.Day == WeekdayId)).Include(b => b.Members).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
