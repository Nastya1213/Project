using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
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
    }
}