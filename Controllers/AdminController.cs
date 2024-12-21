using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Страница управления отзывами
        public IActionResult ManageReviews()
        {   
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null || !user.IsAdmin)  // Проверяем, что пользователь существует и является администратором
            {
                return Unauthorized();  // Возвращаем ошибку 401, если пользователь не является администратором
            }
            // Получаем список концертов
            var concerts = _context.Concerts
                .Select(c => new { c.ConcertId, c.Name })
                .ToList();

            ViewBag.Concerts = concerts; // Передаём список концертов во View
            return View();
        }

        // Получить отзывы по концерту и статусу видимости
        [HttpGet]
        public IActionResult GetReviews(int concertId, bool isVisible)
        {
            var reviews = _context.Reviews
                .Include(r => r.User)
                .Where(r => r.ConcertId == concertId && r.IsVisible == isVisible)
                .Select(r => new
                {
                    r.ReviewId,
                    r.User.Email,
                    r.Rating,
                    r.Comment,
                    r.ReviewDate
                })
                .ToList();

            return Json(reviews); // Возвращаем JSON-ответ
        }

        // Публиковать отзыв
        [HttpPost]
        public async Task<IActionResult> PublishReview(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null) return NotFound();

            review.IsVisible = true;
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            //return Ok();
            return RedirectToAction("ManageReviews");
        }

        // Удалить (скрыть) отзыв
        [HttpPost]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null) return NotFound();

            review.IsVisible = false;
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageReviews");
        }
    }
}
