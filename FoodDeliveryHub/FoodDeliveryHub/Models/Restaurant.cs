module Restaurant
    namespace FoodDeliveryHub.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal MinimumOrderAmount { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MenuCategory> Categories { get; set; } = new List<MenuCategory>();

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
