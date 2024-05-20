namespace gymAPI.Infraestructura.Repositorios.General
{
    public interface ICrudRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> GetUserByID(string id);
        Task RemoveAsync(T entity);
    }
}