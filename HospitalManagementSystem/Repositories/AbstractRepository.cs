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

    public void Add(T entity)
        => _entities.Add(entity);

    public void Update(T entity)
        => _entities.Update(entity);

    public void Remove(T entity)
        => _entities.Remove(entity);

    public void SaveChanges()
        => db.SaveChanges();
}
