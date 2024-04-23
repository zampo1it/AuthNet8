using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModel;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Login model { get; set; }

        public bool isLockedOut { get; set; }
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (identityResult.IsLockedOut)
                {
                    isLockedOut = true;
                }
                if (identityResult.Succeeded)
                {
                    if (returnUrl == null)
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return Page();
        }
    }
}
