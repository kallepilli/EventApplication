namespace webapi.Services.Interfaces
{
    public interface IBaseService<T, R>
    where T : class
    where R : class

    {
        Task<T> Get(string id);
        Task<List<T>> GetList();
        Task<T> Save(R data);
        Task<T> Update(string id, R data);
        Task<bool> Delete(string id);
        T DtoToEntity(R data);
    }
}
