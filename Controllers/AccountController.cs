using ITService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ITService.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITServiceContext _context;

        public AccountController(ITServiceContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);
                if (user != null && model.Password == user.PasswordHash)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Admin") // Kullanıcı rolünü buraya ekleyin
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                    return RedirectToAction("AdminDashboard", "Admin");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Şifre doğrulama (hash işlemi yapılmamışsa basit karşılaştırma)
            return password == storedHash;
        }
    }
}
