using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Fitness.Entities;

public sealed class Subscription : Entity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; private set; }
    [ForeignKey(nameof(PromoCode))]
    public int? PromoCodeId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime StartsAtUtc { get; private set; }
    public DateTime EndsAtUtc { get; private set; }
    public User? User { get; private set; }
    public PromoCode? PromoCode { get; private set; }

    public Subscription(int id, int userId, DateTime startsAtUtc, DateTime endsAtUtc,
        bool isActive, int? promoCodeId = null) : base(id)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (promoCodeId is <= 0) throw new ArgumentOutOfRangeException(nameof(promoCodeId));
        if (endsAtUtc <= startsAtUtc)
            throw new ArgumentException("Subscription end date must be after start date.", nameof(endsAtUtc));
        UserId = userId;
        PromoCodeId = promoCodeId;
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;
        IsActive = isActive;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}
