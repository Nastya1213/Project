using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual  User User { get; set; }  // Навигационное свойство
        [Required]
        public int ConcertId { get; set; }
         public virtual Concert Concert { get; set; }  // Навигационное свойство
        [Required]
        public DateTime ReviewDate { get; set; } // Добавлено новое поле

        [Required]
        [Column("is_visible")] // Указываем имя столбца в таблице
        public bool IsVisible { get; set; } // Новое поле
    }
}