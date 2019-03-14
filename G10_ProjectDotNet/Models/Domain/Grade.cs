using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public enum Grade
    {
        [Description("Zesde Kyu")]
        Zesde_Kyu = 0,
        [Description("Vijfde Kyu")]
        Vijfde_Kyu  = 1,
        [Description("Vierde Kyu")]
        Vierde_Kyu = 2,
        [Description("Derde Kyu")]
        Derde_Kyu = 3,
        [Description("Tweede Kyu")]
        Tweede_Kyu = 4,
        [Description("Eerste Kyu")]
        Eerste_Kyu = 5,
        [Description("Eerste Dan")]
        Eerste_Dan = 6,
        [Description("Tweede Dan")]
        Tweede_Dan = 7,
        [Description("Derde Dan")]
        Derde_Dan = 8,
        [Description("Vierde Dan")]
        Vierde_Dan = 9,
        [Description("Vijfde Dan")]
        Vijfde_Dan = 10,
        [Description("Zesde Dan")]
        Zesde_Dan = 11,
        [Description("Zevende Dan")]
        Zevende_Dan = 12,
        [Description("Achtste Dan")]
        Achtste_Dan = 13,
        [Description("Negende Dan")]
        Negende_Dan = 14,
        [Description("Tiende Dan")]
        Tiende_Dan = 15,
        [Description("Elfde Dan")]
        Elfde_Dan = 16,
        [Description("Twaalfde Dan")]
        Twaalfde_Dan = 17
    }
}
