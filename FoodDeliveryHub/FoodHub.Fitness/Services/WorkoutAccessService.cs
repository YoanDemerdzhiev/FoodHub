using FoodHub.Fitness.Entities;

namespace FoodHub.Fitness.Services;

public sealed class WorkoutAccessService
{
    public bool CanUserAccessWorkout(User user, Workout workout)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(workout);

        if (!workout.IsPremium) return true;
        // Per assignment: IsActive is the access criterion. Subscription expiration
        // is not evaluated here; that would require an additional product rule.
        return user.Subscriptions.Any(subscription =>
            subscription.IsActive && subscription.UserId == user.Id);
    }
}
