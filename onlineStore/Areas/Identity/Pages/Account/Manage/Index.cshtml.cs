﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineStore.Areas.Identity.Data;

namespace OnlineStore.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "First name must be at least 1 and at max 100 characters long.", MinimumLength = 1)]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use only letters.")]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Last name must be at least 1 and at max 100 characters long.", MinimumLength = 1)]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use only letters.")]
            [Display(Name = "LastName")]
            public string LastName { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Last name must be at least 1 and at max 100 characters long.", MinimumLength = 1)]
            [Display(Name = "Username")]
            public string Username { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "City must be at least 1 and at max 100 characters long.", MinimumLength = 1)]
            [Display(Name = "City")]
            public string City { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Street must be at least 1 and at max 100 characters long.", MinimumLength = 1)]
            [Display(Name = "Street")]
            public string Street { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "House number must be at least 1 and at max 10 characters long.", MinimumLength = 1)]
            [RegularExpression(@"^[1-9][0-9]*[a-zA-Z]*$", ErrorMessage = "Enter the valid house number.")]
            [Display(Name = "HouseNumber")]
            public string Housenumber { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Zip code must be at least 1 and at max 10 characters long.", MinimumLength = 1)]
            [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Enter the zip-code in the form 00-000.")]
            [Display(Name = "Zipcode")]
            public string ZipCode { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            [RegularExpression(@"^(\+[0-9]{11})|([0-9]{9})|(\+[0-9]{2}\s[0-9]{3}\s[0-9]{3}\s[0-9]{3})|([0-9]{3}\s[0-9]{3}\s[0-9]{3})|(^$)$", ErrorMessage = "Enter the phone in the form +48123456789, +48 123 123 123, 123456789, 123 123 123")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var city = user.City;
            var street = user.Street;
            var housenumber = user.HouseNumber;
            var zipcode = user.Zipcode;
            var firstName = user.FirstName;
            var lastName = user.LastName;


            Input = new InputModel
            {
                City = city,
                Street = street,
                Housenumber = housenumber,
                ZipCode=zipcode,
                Username=userName,
                FirstName=firstName,
                LastName=lastName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var city = user.City;
            var street = user.Street;
            var housenumber = user.HouseNumber;
            var zipcode = user.Zipcode;
            var username = user.UserName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.City != city)
            {
                user.City = Input.City;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Street != street)
            {
                user.Street = Input.Street;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Housenumber != housenumber)
            {
                user.HouseNumber = Input.Housenumber;
                await _userManager.UpdateAsync(user);
            }
            if (Input.ZipCode != zipcode)
            {
                user.Zipcode = Input.ZipCode;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Username != username)
            {
                user.UserName = Input.Username;
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
