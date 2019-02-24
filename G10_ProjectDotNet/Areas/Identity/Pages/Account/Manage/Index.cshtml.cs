﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Naam")]
            public string Lastname { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Geboortedatum")]
            public DateTime Birthday { get; set; }

            [Display(Name = "Graad")]
            public string Grade { get; set; }

            [Phone]
            [Display(Name = "Telefoonnummer")]
            public string PhoneNumber { get; set; }
        }

        public class AddressModel
        {
            [Display(Name = "Woonplaats")]
            public string City { get; set; }

            [Display(Name = "Postcode")]
            public int ZipCode { get; set; }

            [Display(Name = "Straat")]
            public string Street { get; set; }

            [Display(Name = "Huisnummer")]
            public int Number { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            applicationUser = _applicationUserRepository.GetUser(userName);

            Username = userName;

            Input = new ProfileModel
            {
                Email = email,
                PhoneNumber = applicationUser.PhoneNumber,
                Firstname = applicationUser.Firstname,
                Lastname = applicationUser.Lastname,
                Birthday = applicationUser.Birthday,
                Grade = applicationUser.Grade == null ? "0" : applicationUser.Grade
            };

            Address = new AddressModel
            {
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
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
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
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            // Update custom fields
            updateUser(user.UserName);

            StatusMessage = "Your profile has been updated";
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
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
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
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }

        // Update custom fields
        private void updateUser(string username) {
            var userToUpdate = _applicationUserRepository.GetUser(username);
            userToUpdate.Firstname = Input.Firstname;
            userToUpdate.Lastname = Input.Lastname;
            userToUpdate.Birthday = Input.Birthday;
            userToUpdate.PhoneNumber = Input.PhoneNumber;
            userToUpdate.Grade = Input.Grade;
            userToUpdate.Address.Street = Address.Street;
            userToUpdate.Address.City = Address.City;
            userToUpdate.Address.ZipCode = Address.ZipCode;
            userToUpdate.Address.Number = Address.Number;
            _applicationUserRepository.SaveChanges();
        }
    }
}