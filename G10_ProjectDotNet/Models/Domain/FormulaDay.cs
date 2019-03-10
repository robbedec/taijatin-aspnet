using System;
using System.Collections.Generic;

namespace G10_ProjectDotNet.Models.Domain
{
    public class FormulaDay
    {
        public int FormulaDayId { get; set; }
        public Weekday Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<FormulaFormulaDay> Join { get; set; }
    }

    public class FormulaFormulaDay
    {
        public int Id { get; set; }
        public int FormulaId { get; set; }
        public virtual Formula Formula { get; set; }

        public int FormulaDayId { get; set; }
        public virtual FormulaDay FormulaDay { get; set; }
    }
}