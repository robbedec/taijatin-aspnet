using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public string NationalInsuranceNumber { get; set; }
        public DateTime Registrationdate { get; set; }
        public string BornIn { get; set; }
        public DateTime Birthday { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailParent { get; set; }
        public string Type { get; set; }
        public bool AgreeWithBylaws { get; set; }
        public bool AgreeWithPicturesAndAudio { get; set; }
        public bool ReceiveClubinfo { get; set; }
        public bool ReceiveInfoAboutPromotionsAndFederalMatters { get; set; }

        public ApplicationUser()
        {
        }
    }
}
