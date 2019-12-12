using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LessCardAPI.Views.Authentication
{
    public class OnGetModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Token { get; set; }
        }
        public OnGetModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult OnGet()
        {
            return Page();

        }
        public async Task<IActionResult> OnPostAsync([FromForm]InputModel resetPasswordViewModel)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            IdentityResult result = await _userManager.ResetPasswordAsync
                      (user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return Page();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }

       
    }
}