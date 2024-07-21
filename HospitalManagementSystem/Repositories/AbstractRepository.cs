using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories;

internal class AbstractRepository<T>(HospitalDbContext db) : IRepository<T> where T : class
{
    protected readonly DbSet<T> _entities = db.Set<T>();

    public T? GetById(int id)
        => _entities.Find(id);

    public IEnumerable<T> GetAll()
        => _entities.ToList();

    public IEnumerable<T> Filter(Func<T, bool> predicate)
        => _entities.Where(predicate).ToList();

    public T? FilterSingle(Func<T, bool> predicate)
        => _entities.Where(predicate).FirstOrDefault();

    // CUD operations return this so they can be chained
    public IRepository<T> Add(T entity)
    {
        _entities.Add(entity);
        return this;
    }

    public IRepository<T> Update(T entity)
    {
        _entities.Update(entity);
        return this;
    }

    public IRepository<T> Remove(T entity)
    {
        _entities.Remove(entity);
        return this;
    }

    public void SaveChanges()
        => db.SaveChanges();
}
