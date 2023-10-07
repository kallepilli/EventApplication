using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace webapi.Repositories
{
    public class EventRepository : BaseRepository<Event>, IBaseRepository<Event>
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<Event> dbSet;

        public EventRepository(ApplicationDbContext context) : base(context)
        {
            db = context;
            dbSet = context.Events;
        }

        public override async Task<Event> Update(string id, Event data)
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
    }
}
