using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Concert
    {
        [Key]
        [Column("concert_id")] // Указываем имя столбца в таблице
        public int ConcertId { get; set; }

        [Required]
        [Column("name")] // Указываем имя столбца в таблице
        public required string Name { get; set; }

        [Required]
        [Column("date")] // Указываем имя столбца в таблице
        public DateTime Date { get; set; }

        [Required]
        [Column("time")] // Указываем имя столбца в таблице
        public TimeSpan Time { get; set; }

        [Column("venue")] // Указываем имя столбца в таблице
        public string? Venue { get; set; } // Поле `venue` может быть null, если в таблице это не указано

        [Column("description")] // Указываем имя столбца в таблице
        public string? Description { get; set; } // Поле `description` может быть null

         // Поле для хранения имени файла изображения
        [Column("image_filename")]
        public string? ImageFileName { get; set; }
    }
}
