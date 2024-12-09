using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер
builder.Services.AddControllersWithViews();

// Регистрация ApplicationDbContext с использованием SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=/Users/anastasiaponomareva/Desktop/Project/Data/mydb.db")); // Укажите ваш путь к базе данных

// Добавляем сессии
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Время жизни сессии
    options.Cookie.HttpOnly = true; // Безопасность: сессии доступны только через HTTP
    options.Cookie.IsEssential = true; // Обязательно для работы приложения
});

var app = builder.Build();

// Настройка приложения
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Подключаем сессии

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
