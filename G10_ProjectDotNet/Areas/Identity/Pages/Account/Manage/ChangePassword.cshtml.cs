using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using G10_ProjectDotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace G10_ProjectDotNet.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Voer jouw huidig wachtwoord in.")]
            [DataType(DataType.Password)]
            [Display(Name = "Huidig wachtwoord")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Voer jouw nieuw wachtwoord in.")]
            [StringLength(100, ErrorMessage = "{0} moest minstens {2} en maximaal {1} karakters lang zijn.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Nieuw wachtwoord")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Bevestig jouw nieuw wachtwoord.")]
            [DataType(DataType.Password)]
            [Display(Name = "Bevestig nieuw wachtwoord")]
            [Compare("NewPassword", ErrorMessage = "Het nieuwe wachtwoord en het bevestigde wachtwoord zijn niet gelijk.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kan de gebruiker met ID '{_userManager.GetUserId(User)}' niet laden.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

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
                return NotFound($"Kan de gebruiker met ID '{_userManager.GetUserId(User)}' niet laden.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("Gebruiker heeft succesvol zijn wachtwoord gewijzigd.");
            StatusMessage = "Jouw wachtwoord is gewijzigd.";

            return RedirectToPage();
        }
    }
}
