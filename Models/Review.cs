using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
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
        [Required]
        public int ConcertId { get; set; }
    }
}