using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exists(int id);
    Task<bool> SaveChangesAsync();

    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<List<T>> ListAsync(ISpecification<T> spec);
}
