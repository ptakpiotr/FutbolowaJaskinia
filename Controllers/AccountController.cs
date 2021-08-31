using FutbolowaJaskinia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FutbolowaJaskinia.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _sender;

        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
            IEmailSender sender)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _sender = sender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel md)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = md.Email, UserName = md.Email, FavouriteClub = md.FavouriteClub };

                var creationRes = await _userManager.CreateAsync(user, md.Password);

                if (creationRes.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var redirMessage = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    await _sender.SendEmailAsync(md.Email, "FutbolowaJaskinia potwierdz konto", redirMessage);

                    return View("ConfirmEmailWait");
                }
                else
                {
                    foreach (var err in creationRes.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(md);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl, LoginModel md)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(md.Email);

                if (user == null)
                {
                    return View("Error");
                }

                if (user.EmailConfirmed == false)
                {
                    ModelState.AddModelError("", "Email not confirmed");
                }

                var res = await _signInManager.PasswordSignInAsync(user, md.Password, md.RememberMe, true);

                if (res.Succeeded)
                {
                    if (returnUrl != null && Url.IsLocalUrl(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (res.IsLockedOut)
                {
                    ModelState.AddModelError("", "Konto zablokowane");
                }
                else
                {
                    ModelState.AddModelError("", "Couldn\'t log in");
                }
            }

            return View(md);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(token))
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }

            var confirm = await _userManager.ConfirmEmailAsync(user, token);

            if (confirm.Succeeded)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel md)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(md.Email);

                if (user == null)
                {
                    return View("Error");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var redirMessage = Url.Action("ResetPassword", "Account",
                    new { email = md.Email, token = token }, Request.Scheme);

                await _sender.SendEmailAsync(md.Email, "FutbolowaJaskinia zapomniano hasla", redirMessage);

                return View("ForgotPasswordWait");
            }

            return View(md);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            ViewData["Email"] = email;
            ViewData["Token"] = token;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string email, string token, ChangePasswordModel md)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token) || !ModelState.IsValid)
            {
                return View("Error");
            }
            else
            {
                var user = await _userManager.FindByNameAsync(email);

                var res = await _userManager.ResetPasswordAsync(user, token, md.NewPassword);

                if (res.Succeeded)
                {
                    return View("PasswordChangeSuccess");
                }
                else
                {
                    return View("Error");
                }
            }
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
