using G10_ProjectDotNet.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models.SessionViewModels
{
    public class CreateSessionViewModel
    {
        [Required]
        [Display(Name = "Groep")]
        public int Group { get; set; }

        [Required]
        [Range(8,22)]
        [Display(Name = "Begin uur")]
        public int StartTime { get; set; }

        [Required]
        [Display(Name = "Hoelang duurt deze sessie?")]
        public int Duration { get; set; }

        public CreateSessionViewModel()
        {

        }

        // In case you want to change session properties
        public CreateSessionViewModel(Session session)
        {
            // Unimplemented
        }
    }
}
