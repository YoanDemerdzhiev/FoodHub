using FoodHub.Fitness.Entities;

namespace FoodHub.Fitness.Repositories;

// CRUD implementation for development and demos; does not persist data to a database.
public sealed class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly Dictionary<int, T> _items = new();
    private readonly object _sync = new();

    public IEnumerable<T> GetAll()
    {
        lock (_sync) return _items.Values.ToArray();
    }

    public T? GetById(int id)
    {
        lock (_sync) return _items.GetValueOrDefault(id);
    }

    public void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        lock (_sync)
        {
            if (!_items.TryAdd(entity.Id, entity))
                throw new InvalidOperationException($"Entity with Id {entity.Id} already exists.");
        }
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        lock (_sync)
        {
            if (!_items.ContainsKey(entity.Id))
                throw new KeyNotFoundException($"Entity with Id {entity.Id} was not found.");
            _items[entity.Id] = entity;
        }
    }

    public void Delete(int id)
    {
        lock (_sync)
        {
            if (!_items.Remove(id))
                throw new KeyNotFoundException($"Entity with Id {id} was not found.");
        }
    }
}
