using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        public string Seats { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ConcertId { get; set; }
        [Required]
        public bool Available { get; set; }

        // Навигационное свойство для связи с Concert
        public Concert Concert { get; set; }
    }
}