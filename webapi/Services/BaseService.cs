using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IBaseRepository<TEntity> repo;
        public BaseService(IBaseRepository<TEntity> repository) 
        {
            repo = repository;
        }

        public virtual async Task<bool> Delete(string id) => await repo.Delete(id);

        public virtual TEntity DtoToEntity(TDto data) => throw new NotImplementedException();

        public virtual async Task<TEntity> Get(string id) => await repo.Get(id);

        public virtual async Task<List<TEntity>> GetList() => await repo.GetList();

        public virtual async Task<TEntity> Save(TDto data) => await repo.Save(DtoToEntity(data));

        public virtual async Task<TEntity> Update(string id, TDto data) => await repo.Update(id, DtoToEntity(data));
    }
}
