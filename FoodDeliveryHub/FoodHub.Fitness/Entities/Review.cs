using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Fitness.Entities;

public sealed class Review : Entity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; private set; }
    [ForeignKey(nameof(Workout))]
    public int WorkoutId { get; private set; }
    public int Rating { get; private set; }
    public string? Comment { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public User? User { get; private set; }
    public Workout? Workout { get; private set; }

    public Review(int id, int userId, int workoutId, int rating, string? comment = null) : base(id)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        if (workoutId <= 0) throw new ArgumentOutOfRangeException(nameof(workoutId));
        if (rating is < 1 or > 5) throw new ArgumentOutOfRangeException(nameof(rating));
        UserId = userId;
        WorkoutId = workoutId;
        Rating = rating;
        Comment = comment;
        CreatedAtUtc = DateTime.UtcNow;
    }
}
