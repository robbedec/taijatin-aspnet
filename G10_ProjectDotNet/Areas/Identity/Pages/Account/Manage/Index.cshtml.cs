using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace G10_ProjectDotNet.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender, 
            IApplicationUserRepository applicationUserRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _applicationUserRepository = applicationUserRepository;
        }

        public ApplicationUser applicationUser;

        [Display(Name = "Gebruikersnaam")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ProfileModel Input { get; set; }

        [BindProperty]
        public AddressModel Address { get; set; }

        public class ProfileModel
        {
            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Voornaam")]
            public string Firstname { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Naam")]
            public string Lastname { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [EnumDataType(typeof(Gender))]
            [Display(Name = "Geslacht")]
            public Gender Gender { get; set; }

            [Required(ErrorMessage = "Het {0} is verplicht in te vullen.")]
            [RegularExpression("^[0-9]{2}.[0-9]{2}.[0-9]{2}-[0-9]{3}.[0-9]{2}$", ErrorMessage = "Voer een correct rijksregisternummer in, bv. 99.04.05-233.75")]
            [Display(Name = "Rijksregisternummer")]
            public string NationalInsuranceNumber { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [DataType(DataType.Date)]
            [Display(Name = "Inschrijvinsdatum")]
            public DateTime Registrationdate { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [DataType(DataType.Date)]
            [Display(Name = "Geboortedatum")]
            public DateTime Birthday { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Geboorteplaats")]
            public string BornIn { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Phone]
            [Display(Name = "Gsmnummer")]
            public string MobilePhoneNumber { get; set; }

            [Phone]
            [Display(Name = "Telefoonnummer")]
            public string PhoneNumber { get; set; }

            [EmailAddress]
            [Display(Name = "Emailadres van ouder")]
            public string EmailParent { get; set; }

            [MustBeTrue(ErrorMessage = "Je moet dit accepteren voordat je jouw gewijzigde gegevens kunt opslaan")]
            [Display(Name = "Ik verklaar me akkoord met de bepalingen in de statuten en het huishoudelijk reglement van de VJJF vzw. en met de bepalingen in de statuten en het huishoudelijk reglement van TYR vzw.*")]
            public bool AgreeWithBylaws { get; set; }

            [Display(Name = "Ik geef hierbij de toestemming tot het nemen en verspreiden van audiovisueel materiaal voor Jiu-Jitsu gerelateerde doeleinden.")]
            public bool AgreeWithPicturesAndAudio { get; set; }

            [Display(Name = "Ik wens informatie te ontvangen over club aangelegenheden.")]
            public bool ReceiveClubinfo { get; set; }

            [Display(Name = "Ik wens informatie te ontvangen over federale aangelegenheden en promoties.")]
            public bool ReceiveInfoAboutPromotionsAndFederalMatters { get; set; }
        }

        public class AddressModel
        {
            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Nationaliteit")]
            public string Country { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Woonplaats")]
            public string City { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Postcode")]
            public int ZipCode { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Straat")]
            public string Street { get; set; }

            [Required(ErrorMessage = "{0} is verplicht in te vullen.")]
            [Display(Name = "Huisnummer")]
            public int Number { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return LocalRedirect("/Identity/Account/Login");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var name = _applicationUserRepository.GetUserName(userName);
            Trace.WriteLine(name);
            applicationUser = _applicationUserRepository.GetUser(name);
            Trace.WriteLine(applicationUser.UserName);
            var firstname = applicationUser.Firstname;
            var lastname = applicationUser.Lastname;
            var birthday = applicationUser.Birthday;

            Username = userName;

            Input = new ProfileModel
            {
                Email = email,
                Firstname = firstname,
                Lastname = lastname,
                Gender = applicationUser.Gender,
                NationalInsuranceNumber = applicationUser.NationalInsuranceNumber,
                Registrationdate = applicationUser.Registrationdate,       
                Birthday = birthday,
                BornIn = applicationUser.BornIn,
                MobilePhoneNumber = applicationUser.MobilePhoneNumber,
                PhoneNumber = applicationUser.PhoneNumber,
                EmailParent = applicationUser.EmailParent,
                AgreeWithBylaws = applicationUser.AgreeWithBylaws,
                AgreeWithPicturesAndAudio = applicationUser.AgreeWithPicturesAndAudio,
                ReceiveClubinfo = applicationUser.ReceiveClubinfo,
                ReceiveInfoAboutPromotionsAndFederalMatters = applicationUser.ReceiveInfoAboutPromotionsAndFederalMatters
            };

            Address = new AddressModel
            {
                Country = applicationUser.Address.Country,
                City = applicationUser.Address.City,
                ZipCode = applicationUser.Address.ZipCode,
                Street = applicationUser.Address.Street,
                Number = applicationUser.Address.Number
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Kan de gebruiker met ID '{_userManager.GetUserId(User)}' niet laden.");
                return LocalRedirect("/Identity/Account/Login");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Onverwachte error heeft plaatsgevonden bij aanpassen van email van gebruiker met ID '{userId}'.");
                }
            }
            applicationUser = _applicationUserRepository.GetUser(user.UserName);

            var phoneNumber = applicationUser.PhoneNumber;
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Onverwachte error heeft plaatsgevonden bij aanpassen van telefoonnummer voor gebruiker met ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            // Update custom fields
            updateUser(applicationUser.UserName);

            StatusMessage = "Jouw profiel is succesvol geüpdatet.";
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //return NotFound($"Kan de gebruiker met ID '{_userManager.GetUserId(User)}' niet laden.");
                return LocalRedirect("/Identity/Account/Login");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Bevestig jouw email",
                $"Bevestig jouw account door <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>hier te klikken</a>.");

            StatusMessage = "Verificatiemail verstuurd. Bekijk jouw email alstublieft.";
            return RedirectToPage();
        }

        // Update custom fields
        private void updateUser(string username) {
            var userToUpdate = _applicationUserRepository.GetUser(username);
            //Update the user
            //Conditions on which the Firstname and Lastname may be updated
            if (userToUpdate.Firstname == null && Input.Firstname != null && userToUpdate.Firstname != Input.Firstname ||
                userToUpdate.Firstname.ToString() == "" && Input.Firstname.ToString() != "" && userToUpdate.Firstname != Input.Firstname)
            {
                userToUpdate.Firstname = Input.Firstname;
            }
            if (userToUpdate.Lastname == null && Input.Lastname != null && userToUpdate.Lastname != Input.Lastname ||
                userToUpdate.Lastname.ToString() == "" && Input.Lastname.ToString() != "" && userToUpdate.Firstname != Input.Firstname)
            {
                userToUpdate.Lastname = Input.Lastname;
            }
            userToUpdate.Gender = Input.Gender;
            userToUpdate.NationalInsuranceNumber = Input.NationalInsuranceNumber;
            if (Input.Registrationdate != new DateTime() && userToUpdate.Registrationdate != Input.Registrationdate)
            {
                userToUpdate.Registrationdate = Input.Registrationdate;
            }
            userToUpdate.BornIn = Input.BornIn;
            userToUpdate.Birthday = Input.Birthday;            
            userToUpdate.MobilePhoneNumber = Input.MobilePhoneNumber;
            userToUpdate.EmailParent = Input.EmailParent;
            userToUpdate.PhoneNumber = Input.PhoneNumber;
            userToUpdate.AgreeWithBylaws = Input.AgreeWithBylaws;
            userToUpdate.AgreeWithPicturesAndAudio = Input.AgreeWithPicturesAndAudio;
            userToUpdate.ReceiveClubinfo = Input.ReceiveClubinfo;
            userToUpdate.ReceiveInfoAboutPromotionsAndFederalMatters = Input.ReceiveInfoAboutPromotionsAndFederalMatters;
            //Update the Address
            userToUpdate.Address.Country = Address.Country;
            userToUpdate.Address.Street = Address.Street;
            userToUpdate.Address.City = Address.City;
            userToUpdate.Address.ZipCode = Address.ZipCode;
            userToUpdate.Address.Number = Address.Number;
            _applicationUserRepository.SaveChanges();
        }
    }
}
