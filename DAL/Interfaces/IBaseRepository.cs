namespace DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<T> Get(int id);
        IQueryable<T> GetAll();

        Task<bool> Delete(T entity);

        Task<T> Update(T entity);
    }
}
