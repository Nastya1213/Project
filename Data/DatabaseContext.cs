using Microsoft.EntityFrameworkCore;
using Project.Models;

public class ApplicationDbContext : DbContext
{
    public required DbSet<Concert> Concerts { get; set; }
    public required DbSet<User> Users { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Users/anastasiaponomareva/Desktop/c#/Project/Data/mydb.db"); // Укажите путь к вашей базе данных
    }
}
