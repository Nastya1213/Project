using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Project.Models;
using Microsoft.EntityFrameworkCore;

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
        HttpContext.Session.SetString("UserEmail", user.Email); //при успешном входе пользователя его имя сохраняется в сессии
        HttpContext.Session.SetString("UserName", user.Name);
        HttpContext.Session.SetInt32("UserId", user.UserId);
        // Устанавливаем сообщение
        TempData["SuccessMessage"] = $"Добро пожаловать, {user.Name}!";
        return RedirectToAction("Profile", "Account");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["InfoMessage"] = "Вы успешно вышли из системы.";
        return RedirectToAction("Login", "Account");
    }

    //---Профиль---
    public IActionResult Profile()
{
    var user = HttpContext.Session.GetString("UserName");
     // Получаем UserId из сессии
    var userId = HttpContext.Session.GetInt32("UserId");
    if (string.IsNullOrEmpty(user))
    {
        return RedirectToAction("Login", "Account");
    }
    
   // Получаем заказы этого пользователя
    var orders = _context.Orders
                         .Where(o => o.UserId == userId)
                         .ToList();

    // Передаем данные в представление
    ViewBag.UserName = HttpContext.Session.GetString("UserName");
    return View(orders);
}

    public IActionResult OrderDetails(int orderId)
{
    // Получаем все билеты для данного заказа с включением данных о билете и концерте
    var orderTickets = _context.OrderTickets
                               .Where(ot => ot.OrderId == orderId)
                               .Include(ot => ot.Ticket)             // Включаем данные о билете
                               .ThenInclude(t => t.Concert)          // Включаем данные о концерте
                               .ToList();

    if (orderTickets == null || !orderTickets.Any())
    {
        return NotFound(); // Если билеты не найдены
    }

    // Передаем данные в представление
    return View(orderTickets);
}




}