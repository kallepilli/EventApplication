namespace webapi.Services.Interfaces
{
    public interface IBaseService<T>
    where T : class
    {
        Task<T> Save(T data);
        Task<T> Get(string id);
        Task<T> Update(string id, T data);
        Task<bool> Delete(string id);
    }
}
