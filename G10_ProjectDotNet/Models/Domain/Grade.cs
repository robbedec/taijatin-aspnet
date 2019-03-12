using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.Domain
{
    public enum Grade
    {
        [Display(Name = "Zesde Kyu")]
        Zesde_Kyu = 0,
        [Display(Name = "Vijfde Kyu")]
        Vijfde_Kyu  = 1,
        [Display(Name = "Vierde Kyu")]
        Vierde_Kyu = 2,
        [Display(Name = "Derde Kyu")]
        Derde_Kyu = 3,
        [Display(Name = "Tweede Kyu")]
        Tweede_Kyu = 4,
        [Display(Name = "Eerste Kyu")]
        Eerste_Kyu = 5,
        [Display(Name = "Eerste Dan")]
        Eerste_Dan = 6,
        [Display(Name = "Tweede Dan")]
        Tweede_Dan = 7,
        [Display(Name = "Derde Dan")]
        Derde_Dan = 8,
        [Display(Name = "Vierde Dan")]
        Vierde_Dan = 9,
        [Display(Name = "Vijfde Dan")]
        Vijfde_Dan = 10,
        [Display(Name = "Zesde Dan")]
        Zesde_Dan = 11,
        [Display(Name = "Zevende Dan")]
        Zevende_Dan = 12,
        [Display(Name = "Achtste Dan")]
        Achtste_Dan = 13,
        [Display(Name = "Negende Dan")]
        Negende_Dan = 14,
        [Display(Name = "Tiende Dan")]
        Tiende_Dan = 15,
        [Display(Name = "Elfde Dan")]
        Elfde_Dan = 16,
        [Display(Name = "Twaalfde Dan")]
        Twaalfde_Dan = 17
    }
}
