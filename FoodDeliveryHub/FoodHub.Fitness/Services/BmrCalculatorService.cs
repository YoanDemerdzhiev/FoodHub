using FoodHub.Fitness.Enums;
using FoodHub.Fitness.Validation;

namespace FoodHub.Fitness.Services;

public sealed class BmrCalculatorService
{
    // Mifflin-St Jeor formula. Height is in centimetres, weight in kilograms.
    public decimal CalculateBmr(Gender gender, int age, decimal weightKg, decimal heightCm)
    {
        InputValidator.ValidateMeasurements(age, weightKg, heightCm);
        decimal genderAdjustment = gender switch
        {
            Gender.Male => 5m,
            Gender.Female => -161m,
            _ => throw new ArgumentOutOfRangeException(nameof(gender))
        };
        return 10m * weightKg + 6.25m * heightCm - 5m * age + genderAdjustment;
    }

    public decimal CalculateTdee(Gender gender, int age, decimal weightKg,
        decimal heightCm, ActivityLevel activityLevel)
    {
        decimal activityMultiplier = activityLevel switch
        {
            ActivityLevel.Sedentary => 1.2m,
            ActivityLevel.LightlyActive => 1.375m,
            ActivityLevel.ModeratelyActive => 1.55m,
            ActivityLevel.VeryActive => 1.725m,
            ActivityLevel.ExtraActive => 1.9m,
            _ => throw new ArgumentOutOfRangeException(nameof(activityLevel))
        };
        return Math.Round(CalculateBmr(gender, age, weightKg, heightCm) * activityMultiplier, 2);
    }

    public decimal CalculateDailyCalories(Gender gender, int age, decimal weightKg,
        decimal heightCm, ActivityLevel activityLevel, FitnessGoal goal)
    {
        decimal goalMultiplier = goal switch
        {
            FitnessGoal.LoseWeight => 0.85m,
            FitnessGoal.Maintain => 1.0m,
            FitnessGoal.GainMass => 1.15m,
            _ => throw new ArgumentOutOfRangeException(nameof(goal))
        };
        // Avoid intermediate TDEE rounding when applying the goal adjustment.
        decimal bmr = CalculateBmr(gender, age, weightKg, heightCm);
        decimal activityMultiplier = activityLevel switch
        {
            ActivityLevel.Sedentary => 1.2m,
            ActivityLevel.LightlyActive => 1.375m,
            ActivityLevel.ModeratelyActive => 1.55m,
            ActivityLevel.VeryActive => 1.725m,
            ActivityLevel.ExtraActive => 1.9m,
            _ => throw new ArgumentOutOfRangeException(nameof(activityLevel))
        };
        return Math.Round(bmr * activityMultiplier * goalMultiplier, 2);
    }
}
