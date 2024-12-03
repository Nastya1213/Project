using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Project.Models;
using System.IO;
using System.Linq;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;  // Для работы с файловой системой

        public HomeController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            // Путь к папке с изображениями
            var imagesDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            // Получаем список всех файлов изображений в папке
            var imageFiles = Directory.GetFiles(imagesDirectory, "*.*")
                .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                .Select(file => Path.GetFileName(file)) // Берём только имена файлов
                .ToList();

            // Получение топ-10 концертов из базы данных с привязкой к картинкам
            var topConcerts = _context.Concerts
                .OrderBy(c => c.Name)
                .Take(10)
                .ToList();

            // Передаем данные в представление
            ViewBag.TopConcerts = topConcerts;

            return View();
        }

    }
}
