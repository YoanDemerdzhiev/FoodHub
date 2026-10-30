namespace FoodHub.Fitness.Entities;

public sealed class Workout : Entity
{
    public string Name { get; private set; }
    public bool IsPremium { get; private set; }
    public ICollection<WorkoutExercise> WorkoutExercises { get; private set; } = new List<WorkoutExercise>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();

    public Workout(int id, string name, bool isPremium) : base(id)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name.Trim()
            : throw new ArgumentException("Workout name is required.", nameof(name));
        IsPremium = isPremium;
    }
}
