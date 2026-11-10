using System.ComponentModel.DataAnnotations.Schema;

namespace FoodHub.Fitness.Entities;

// Explicit join entity for the many-to-many Workout <-> Exercise relationship.
// A database implementation should also enforce uniqueness of (WorkoutId, ExerciseId).
public sealed class WorkoutExercise : Entity
{
    [ForeignKey(nameof(Workout))]
    public int WorkoutId { get; private set; }
    [ForeignKey(nameof(Exercise))]
    public int ExerciseId { get; private set; }
    public int Sets { get; private set; }
    public int Repetitions { get; private set; }
    public Workout? Workout { get; private set; }
    public Exercise? Exercise { get; private set; }

    public WorkoutExercise(int id, int workoutId, int exerciseId, int sets, int repetitions) : base(id)
    {
        if (workoutId <= 0) throw new ArgumentOutOfRangeException(nameof(workoutId));
        if (exerciseId <= 0) throw new ArgumentOutOfRangeException(nameof(exerciseId));
        if (sets <= 0) throw new ArgumentOutOfRangeException(nameof(sets));
        if (repetitions <= 0) throw new ArgumentOutOfRangeException(nameof(repetitions));
        WorkoutId = workoutId;
        ExerciseId = exerciseId;
        Sets = sets;
        Repetitions = repetitions;
    }
}
