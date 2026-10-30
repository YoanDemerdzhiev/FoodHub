using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Fitness.Entities;

public sealed class MealPlan : Entity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public int DailyCalories { get; private set; }
    public User? User { get; private set; }
    public ICollection<NutritionLog> NutritionLogs { get; private set; } = new List<NutritionLog>();

    public MealPlan(int id, int userId, string name, int dailyCalories) : base(id)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (dailyCalories <= 0) throw new ArgumentOutOfRangeException(nameof(dailyCalories));
        UserId = userId;
        Name = !string.IsNullOrWhiteSpace(name) ? name.Trim()
            : throw new ArgumentException("Meal plan name is required.", nameof(name));
        DailyCalories = dailyCalories;
    }
}
