using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class User
    {
        [Key]
        [Column("user_id")] // Указываем имя столбца в таблице
        public int UserId { get; set; }

        [Required]
        [Column("name")] // Указываем имя столбца в таблице
        public string Name { get; set; }

        [Required]
        [Column("email")] // Указываем имя столбца в таблице
        public string Email { get; set; }

        [Required]
        [Column("password")] // Указываем имя столбца в таблице
        public string Password { get; set; }
        
        [Required]
        [Column("is_admin")] // Указываем имя столбца в таблице
        public bool IsAdmin { get; set; }

        
    }
}
