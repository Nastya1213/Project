using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
{
    public class OrderTicket
    {
        [Key]
        public int OrderTicketId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int TicketId { get; set; }
    }
}