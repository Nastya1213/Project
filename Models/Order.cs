using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}