using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Страница для написания отзыва
        public IActionResult Write(int concertId)
        {
            // Проверяем, существует ли концерт
            var concert = _context.Concerts.FirstOrDefault(c => c.ConcertId == concertId);
            if (concert == null)
            {
                return NotFound("Концерт не найден.");
            }

            // Передаем данные в представление
            ViewBag.ConcertId = concertId;
            ViewBag.ConcertName = concert.Name; // Например, передача имени концерта
            return View();
        }

        // Обработка отправки отзыва
        [HttpPost]
        public async Task<IActionResult> Write(int concertId, [Bind("Rating,Comment")] Review review)
        {
            // Проверяем, существует ли концерт
            var concert = _context.Concerts.FirstOrDefault(c => c.ConcertId == concertId);
            if (concert == null)
            {
                return NotFound("Концерт не найден.");
            }

            // Получаем текущего пользователя из сессии
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Заполняем остальные поля отзыва
            review.ConcertId = concertId;
            review.UserId = user.UserId;
            review.ReviewDate = DateTime.Now;
            review.IsVisible = false; // Отзыв скрыт до модерации

            // Добавляем отзыв в базу данных
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Ваш отзыв успешно отправлен и будет опубликован после модерации.";
            return RedirectToAction("Details", "Concert", new { id = concertId });
        }
    }
}
