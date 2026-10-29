using _2sem_SpejderProjekt.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace _2sem_SpejderProjekt.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Indtast din e-mail.")]
        [EmailAddress(ErrorMessage = "Ugyldig e-mailadresse.")]
        public string Email { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Indtast din adgangskode.")]
        public string Password { get; set; } = "";

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string email = Email.Trim();

            User? user = null;
            string role = "";

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == email);

            if (admin != null)
            {
                user = admin;
                role = "Admin";
            }
            else
            {
                var scout = await _context.Scouts
                    .FirstOrDefaultAsync(s => s.Email == email);

                if (scout != null)
                {
                    user = scout;
                    role = "Scout";
                }
            }

            if (user == null)
            {
                ModelState.AddModelError("",
                    "Forkert e-mail eller adgangskode.");
                return Page();
            }

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(
                user, user.PasswordHash, Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("",
                    "Forkert e-mail eller adgangskode.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,
                    role == "Admin"
                        ? ((Admin)user).AdminId.ToString()
                        : ((Scout)user).ScoutId.ToString()),

                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, role)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToPage("/Index");
        }
    }
}

