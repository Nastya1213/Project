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
        {   // Путь к папке с изображениями для карусели
            var carouselDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images", "carousel");

            // Получаем список всех файлов изображений в папке
            var imageFiles = Directory.GetFiles(carouselDirectory, "*.*")
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"))
                .Select(file => Path.GetFileName(file)) // Получаем только имя файла
                .ToArray();

            // Передаем список изображений в представление
            ViewBag.ConcertImages = imageFiles;

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
