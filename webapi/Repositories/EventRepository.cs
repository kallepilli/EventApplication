using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace webapi.Repositories
{
    public class EventRepository : IBaseRepository<Event>
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<Event> dbSet;

        public EventRepository(ApplicationDbContext context)
        {
            db = context;
            dbSet = context.Events;
        }

        public async Task<Event> Get(string id) => (await dbSet.FirstOrDefaultAsync(e => e.Id == id))!;

        public async Task<List<Event>> GetList() => (await dbSet.ToListAsync());

        public async Task<Event?> Save(Event data)
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

        public async Task<Event> Update(string id, Event data)
        {
            var dbEvent = await Get(id);
            if (dbEvent is not null)
            {
                dbEvent.Name = data.Name;
                dbEvent.Location = data.Location;
                dbEvent.AdditionalInfo = data.AdditionalInfo;
                dbEvent.EventTime = data.EventTime;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }

                return data;
            }
            return null;
        }
        public async Task<bool> Delete(string id)
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
