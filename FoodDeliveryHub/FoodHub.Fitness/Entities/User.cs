using System.ComponentModel.DataAnnotations.Schema;
using FoodHub.Fitness.Enums;
using FoodHub.Fitness.Validation;

namespace FoodHub.Fitness.Entities;

public sealed class User : Entity
{
    public string Email { get; private set; }
    public int Age { get; private set; }
    public decimal WeightKg { get; private set; }
    public decimal HeightCm { get; private set; }
    public Gender Gender { get; private set; }

    [ForeignKey(nameof(Role))]
    public int RoleId { get; private set; }
    public Role? Role { get; private set; }

    public ICollection<Subscription> Subscriptions { get; private set; } = new List<Subscription>();
    public ICollection<MealPlan> MealPlans { get; private set; } = new List<MealPlan>();
    public ICollection<NutritionLog> NutritionLogs { get; private set; } = new List<NutritionLog>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();

    public User(int id, string email, int age, decimal weightKg, decimal heightCm, Gender gender, int roleId)
        : base(id)
    {
        InputValidator.ValidateEmail(email);
        InputValidator.ValidateMeasurements(age, weightKg, heightCm);
        if (roleId <= 0) throw new ArgumentOutOfRangeException(nameof(roleId));
        if (!Enum.IsDefined(gender)) throw new ArgumentOutOfRangeException(nameof(gender));
        Email = email.Trim();
        Age = age;
        WeightKg = weightKg;
        HeightCm = heightCm;
        Gender = gender;
        RoleId = roleId;
    }

    public void AddSubscription(Subscription subscription)
    {
        ArgumentNullException.ThrowIfNull(subscription);
        if (subscription.UserId != Id)
            throw new ArgumentException("Subscription belongs to a different user.", nameof(subscription));
        if (Subscriptions.Any(x => x.Id == subscription.Id))
            throw new InvalidOperationException("Subscription already added.");
        Subscriptions.Add(subscription);
    }
}
