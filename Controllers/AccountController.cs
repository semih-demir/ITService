using ITService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı MSSQL'den doğrula
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null && VerifyPasswordHash(model.Password, user.PasswordHash))
                {
                    bool isPasswordValid = (model.Password == user.PasswordHash);
                    // Kullanıcıyı rolüne göre yönlendir
                    if (user.RoleId == 1) // Admin rolü
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
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
