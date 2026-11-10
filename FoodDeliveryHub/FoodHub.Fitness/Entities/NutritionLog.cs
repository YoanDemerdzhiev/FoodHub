using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Fitness.Entities;

public sealed class NutritionLog : Entity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; private set; }
    [ForeignKey(nameof(MealPlan))]
    public int? MealPlanId { get; private set; }
    public DateTime LoggedAtUtc { get; private set; }
    public int Calories { get; private set; }
    public decimal ProteinGrams { get; private set; }
    public decimal CarbsGrams { get; private set; }
    public decimal FatGrams { get; private set; }
    public User? User { get; private set; }
    public MealPlan? MealPlan { get; private set; }

    public NutritionLog(int id, int userId, int calories, decimal proteinGrams,
        decimal carbsGrams, decimal fatGrams, int? mealPlanId = null) : base(id)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (mealPlanId is <= 0) throw new ArgumentOutOfRangeException(nameof(mealPlanId));
        if (calories < 0) throw new ArgumentOutOfRangeException(nameof(calories));
        if (proteinGrams < 0) throw new ArgumentOutOfRangeException(nameof(proteinGrams));
        if (carbsGrams < 0) throw new ArgumentOutOfRangeException(nameof(carbsGrams));
        if (fatGrams < 0) throw new ArgumentOutOfRangeException(nameof(fatGrams));
        UserId = userId;
        MealPlanId = mealPlanId;
        LoggedAtUtc = DateTime.UtcNow;
        Calories = calories;
        ProteinGrams = proteinGrams;
        CarbsGrams = carbsGrams;
        FatGrams = fatGrams;
    }
}
