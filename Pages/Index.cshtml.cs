using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IdentityUser[] Users { get; set; } = Array.Empty<IdentityUser>();
        public string CurrentUserEmail { get; set; }

        public bool IsCurrentUserBlocked { get; set; }
        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> user, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = user;
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                CurrentUserEmail = currentUser.Email;
                IsCurrentUserBlocked = !(currentUser.LockoutEnd == null);
            }

            Users = _userManager.Users.ToArray();
        }

        public async Task<IActionResult> OnPostBlockingAsync(string[] selectedUsers)
        {

            foreach (var userEmail in selectedUsers)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user != null)
                {
                    user.LockoutEnd = new DateTime(2070, 1, 1);
                    await _userManager.UpdateAsync(user);
                }
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.LockoutEnd != null) {
                await _signInManager.SignOutAsync();
                Response.Redirect("/Login");
                Response.Headers.Add("Refresh", "0;url=/Login");
            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUnBlockingAsync(string[] selectedUsers)
        {

            foreach (var userEmail in selectedUsers)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user != null)
                {
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                }
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.LockoutEnd != null)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(string selectedUsers)
        {
            if (selectedUsers != null)
            {
                var usersToDelete = selectedUsers.Split(',');

                foreach (var user in usersToDelete)
                {
                    await DeleteUserFromDatabase(user);
                }
            

            var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null && usersToDelete.Contains(currentUser.Email))
                {
                    await _signInManager.SignOutAsync();
                }
            }

            return RedirectToPage();
        }

        private async Task DeleteUserFromDatabase(string userId)
        {

            var user = await _userManager.FindByEmailAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    Console.WriteLine("User deleted successfully");
                }
                else
                {
                    Console.WriteLine("User NOT deleted");
                }
            }
        }
    }
}
