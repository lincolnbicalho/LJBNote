using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;
public interface IBaseRepository<T> where T : BaseEntity
{
    Task<List<T>> ListAll();
    Task<T> GetById(int id);
    Task<T> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
}