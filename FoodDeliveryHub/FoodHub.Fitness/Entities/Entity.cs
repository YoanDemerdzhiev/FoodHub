using System.ComponentModel.DataAnnotations;

namespace FoodHub.Fitness.Entities;

public abstract class Entity : IEntity
{
    [Key]
    public int Id { get; private set; }

    protected Entity(int id)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be positive.");
        Id = id;
    }
}
