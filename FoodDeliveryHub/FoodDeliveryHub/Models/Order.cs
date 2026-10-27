module Order
namespace FoodDeliveryHub.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public int AddressId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Accepted";

        public decimal TotalAmount { get; set; }

        public decimal DeliveryFee { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public User? User { get; set; }

        public Restaurant? Restaurant { get; set; }

        public Address? Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
