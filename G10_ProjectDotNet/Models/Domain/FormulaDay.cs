using System;

namespace G10_ProjectDotNet.Models.Domain
{
    public class FormulaDay
    {
        public int FormulaDayId { get; set; }
        public Weekday Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}