using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Project.Controllers
{
    public class ConcertController : Controller
    {
        private readonly ApplicationDbContext _context; // Контекст базы данных

        public ConcertController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для отображения деталей концерта
        public IActionResult Details(int id)
        {
            // Поиск концерта по ID с подгрузкой связанных отзывов
            var concert = _context.Concerts
                .Include(c => c.Reviews)  // Включаем отзывы для концерта
                .ThenInclude(r => r.User)  // Включаем пользователя, оставившего отзыв (если требуется)
                .FirstOrDefault(c => c.ConcertId == id);


            if (concert == null)
            {
                // Если концерт не найден, возвращаем сообщение об ошибке
                return NotFound("Концерт не найден.");
            }

            // Передача данных в представление
            return View(concert);
        }
        // Страница покупки билетов
        public IActionResult Buy(int concertId)
        {
        var concert = _context.Concerts
            .Include(c => c.Tickets)  // Включаем билеты, связанные с концертом
            .FirstOrDefault(c => c.ConcertId == concertId);

        if (concert == null)
        {
            return NotFound();
        }

        // Фильтруем доступные билеты
        var availableTickets = concert.Tickets.Where(t => t.Available).ToList();

        // Отправляем данные в представление
        ViewBag.Concert = concert;
        return View(availableTickets); // Страница для выбора билетов
        }

        // Покупка билета
        [HttpPost]
        public async Task<IActionResult> BuyTicket(int ticketId)
        {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == ticketId && t.Available);

        if (ticket == null)
        {
            return BadRequest("Билет недоступен для покупки.");
        }

        // Создание заказа
        var userEmail = HttpContext.Session.GetString("UserEmail");
        var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var order = new Order
        {
            UserId = user.UserId,
            OrderStatus = "Новый",
            OrderDate = DateTime.Now,
            TotalPrice = ticket.Price
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Добавление билета в заказ
        var orderTicket = new OrderTicket
        {
            OrderId = order.OrderId,
            TicketId = ticket.TicketId
        };

        _context.OrderTickets.Add(orderTicket);
        ticket.Available = false;  // Обновляем доступность билета
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Билет успешно куплен!";
        return RedirectToAction("Profile", "Account");
    }
    


    }
}
