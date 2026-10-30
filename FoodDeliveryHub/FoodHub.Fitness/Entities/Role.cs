namespace FoodHub.Fitness.Entities;

public sealed class Role : Entity
{
    public string Name { get; private set; }
    public ICollection<User> Users { get; private set; } = new List<User>();

    public Role(int id, string name) : base(id)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name.Trim()
            : throw new ArgumentException("Role name is required.", nameof(name));
    }
}
