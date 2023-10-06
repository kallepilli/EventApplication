using webapi.Data.Model;
using webapi.Data;
using webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using webapi.Data.Model.Base;

namespace webapi.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseModel
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }
        public virtual async Task<TEntity> Get(string id) => (await dbSet.FirstOrDefaultAsync(e => e.Id == id))!;

        public virtual async Task<List<TEntity>> GetList() => (await dbSet.ToListAsync());

        public virtual async Task<TEntity?> Save(TEntity data)
        {
            try
            {
                dbSet.Add(data);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            return await Get(data.Id);
        }

        public virtual Task<TEntity> Update(string id, TEntity data) => throw new NotImplementedException();

        public virtual async Task<bool> Delete(string id)
        {
            var dbEvent = await Get(id);
            if (dbEvent is not null)
            {
                dbSet.Remove(dbEvent);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
