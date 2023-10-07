namespace webapi.Services.Interfaces
{
    public interface IBaseService<TEntity, TDto>
    where TEntity : class
    where TDto : class

    {
        Task<TEntity> Get(string id);
        Task<List<TEntity>> GetList();
        Task<TEntity> Save(TDto data);
        Task<TEntity> Update(string id, TDto data);
        Task<bool> Delete(string id);
        TEntity DtoToEntity(TDto data);
    }
}
