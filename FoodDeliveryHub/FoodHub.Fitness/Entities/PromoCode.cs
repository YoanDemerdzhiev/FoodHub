using FoodHub.Fitness.Validation;

namespace FoodHub.Fitness.Entities;

public sealed class PromoCode : Entity
{
    public string Code { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? ExpiresAtUtc { get; private set; }
    public ICollection<Subscription> Subscriptions { get; private set; } = new List<Subscription>();

    public PromoCode(int id, string code, decimal discountPercent, bool isActive = true,
        DateTime? expiresAtUtc = null) : base(id)
    {
        InputValidator.ValidatePromoCode(code);
        if (discountPercent is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercent));
        Code = code;
        DiscountPercent = discountPercent;
        IsActive = isActive;
        ExpiresAtUtc = expiresAtUtc;
    }
}
