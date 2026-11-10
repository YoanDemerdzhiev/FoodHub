namespace FoodHub.Fitness.Entities;

public sealed class Exercise : Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public ICollection<WorkoutExercise> WorkoutExercises { get; private set; } = new List<WorkoutExercise>();

    public Exercise(int id, string name, string? description = null) : base(id)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name.Trim()
            : throw new ArgumentException("Exercise name is required.", nameof(name));
        Description = description;
    }
}
