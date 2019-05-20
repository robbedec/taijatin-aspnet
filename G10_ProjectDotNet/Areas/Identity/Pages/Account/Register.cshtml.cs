using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using G10_ProjectDotNet.Data;
using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace G10_ProjectDotNet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ApplicationDbContext _dbContext;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IApplicationUserRepository applicationUserRepository,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _applicationUserRepository = applicationUserRepository;
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Gebruikersnaam")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //[Required]
            //[Display(Name = "Voornaam")]
            //public string Firstname { get; set; }

            //[Required]
            //[Display(Name = "Achternaam")]
            //public string Lastname { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} en max {1} karakters lang zijn.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("Password", ErrorMessage = "Wachtwoord en bevestigd wachtwoord komen niet overeen.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var username = Input.UserName;
                var email = Input.Email;
                var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email };
                //var address = new Address { Country = "België", City = "bv. Gent", ZipCode = 9000, Street = "bv. Korenmarkt", Number = 1 };
                //var formula = new Formula { FormulaName = "Geen" };
                //var appUser = new Member { UserName = Input.UserName, Email = Input.Email, Firstname = Input.Firstname, Lastname = Input.Lastname, Gender = Gender.Man, Birthday = new DateTime(1920, 01, 01), Registrationdate = DateTime.Now, MobilePhoneNumber = "+32", BornIn = "Geboorteplaats", Address = address, Formula = formula, Grade = Grade.Zesde_Kyu };
                //_dbContext.Gebruikers.Add(appUser);
                try
                {
                    var checkIfUserExist = _applicationUserRepository.GetUser(username);

                    if (checkIfUserExist != null)
                    {
                        if (checkIfUserExist.Email.ToLower() == email.ToLower())
                        {
                            var result = await _userManager.CreateAsync(user, Input.Password);
                            if (checkIfUserExist.Type == "Lesgever")
                            {
                                checkIfUserExist = (Teacher)checkIfUserExist;
                                await _userManager.AddClaimAsync(await _userManager.FindByEmailAsync(user.Email), new Claim(ClaimTypes.Role, "Teacher"));
                            }
                            if (checkIfUserExist.Type == "Beheerder")
                            {
                                checkIfUserExist = (Admin)checkIfUserExist;
                                await _userManager.AddClaimAsync(await _userManager.FindByEmailAsync(user.Email), new Claim(ClaimTypes.Role, "Admin"));
                            }
                            else
                            {
                                checkIfUserExist = (Member)checkIfUserExist;
                                await _userManager.AddClaimAsync(await _userManager.FindByEmailAsync(user.Email), new Claim(ClaimTypes.Role, "User"));
                            }
                            if (result.Succeeded)
                            {
                                _logger.LogInformation("User activated account with password.");

                                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                var callbackUrl = Url.Page(
                                    "/Account/ConfirmEmail",
                                    pageHandler: null,
                                    values: new { userId = user.Id, code = code },
                                    protocol: Request.Scheme);

                                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect(returnUrl);
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Dit emailadres kan niet bij jouw gebruiker gevonden worden.");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Gebruikersnaam", "Gebruiker met deze gebruikersnaam bestaat nog niet. Gebruikersnaam is hoofdlettergevoelig, dus probeer eens met een (of meerdere) hoofdletter(s).");
                        return Page();
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Fout", "Er is een fout met de databankverbinding. Probeer later opnieuw of contacteer de beheerders...");
                    return Page();
                }
            }
               
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
