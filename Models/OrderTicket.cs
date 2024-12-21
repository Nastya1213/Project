using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class OrderTicket
    {
        [Key]
        public int OrderTicketId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int TicketId { get; set; }

        // Навигационное свойство для связи с Ticket
        public Ticket Ticket { get; set; }
    }
}