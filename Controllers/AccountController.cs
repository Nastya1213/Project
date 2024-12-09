using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Project.Models;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Account/Register
    public IActionResult Register()
    {
        return View(new User());
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        // Если данные некорректны, возвращаем форму с ошибками
        return View(user);
    }

    // --- Логин ---

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Email и пароль обязательны.");
            return View();
        }

        var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

        if (user == null)
        {
            ModelState.AddModelError("", "Неверный Email или пароль.");
            return View();
        }

        // Логика аутентификации
        HttpContext.Session.SetString("UserEmail", user.Email);

        // Устанавливаем сообщение
        TempData["SuccessMessage"] = $"Добро пожаловать, {user.Name}!";
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["InfoMessage"] = "Вы успешно вышли из системы.";
        return RedirectToAction("Login", "Account");
    }
}