module MenuItem
namespace FoodDeliveryHub.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Restaurant? Restaurant { get; set; }

        public MenuCategory? Category { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
