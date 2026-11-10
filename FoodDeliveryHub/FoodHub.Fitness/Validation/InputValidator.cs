using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FoodHub.Fitness.Validation;

public static class InputValidator
{
    // Project rule: 6-12 uppercase Latin letters or digits, e.g. FIT2026.
    private static readonly Regex PromoPattern =
        new(@"^[A-Z0-9]{6,12}$", RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public static void ValidateMeasurements(int age, decimal weightKg, decimal heightCm)
    {
        if (age <= 0) throw new ArgumentOutOfRangeException(nameof(age));
        if (weightKg <= 0) throw new ArgumentOutOfRangeException(nameof(weightKg));
        if (heightCm <= 0) throw new ArgumentOutOfRangeException(nameof(heightCm));
    }

    public static bool IsValidEmail(string? email) =>
        !string.IsNullOrWhiteSpace(email)
        && new EmailAddressAttribute().IsValid(email)
        && Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");

    public static void ValidateEmail(string? email)
    {
        if (!IsValidEmail(email)) throw new ArgumentException("Invalid email address.", nameof(email));
    }

    public static bool IsValidPromoCode(string? code) =>
        code is not null && PromoPattern.IsMatch(code);

    public static void ValidatePromoCode(string? code)
    {
        if (!IsValidPromoCode(code))
            throw new ArgumentException("Promo code must contain 6-12 uppercase letters or digits.", nameof(code));
    }
}
