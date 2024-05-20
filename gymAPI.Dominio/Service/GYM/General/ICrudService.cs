namespace gymAPI.Dominio.Service.GYM.General
{
    public interface ICrudService<T>
    {
        Task<T> Create(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(string id);
        Task<T> Update(T entity);
        Task Remove(string id);
    }
}