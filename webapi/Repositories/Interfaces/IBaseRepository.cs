namespace webapi.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    where T : class
    {
        Task<T> Get(string id);
        Task<List<T>> GetList();
        Task<T> Save(T data);
        Task<T> Update(string id, T data);
        Task<bool> Delete(string id);
    }

}
