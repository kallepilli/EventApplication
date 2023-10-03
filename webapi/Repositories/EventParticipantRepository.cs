using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace webapi.Repositories
{
    public class EventParticipantRepository : IEventParticipantRepository<EventParticipant>
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<EventParticipant> dbSet;

        public EventParticipantRepository(ApplicationDbContext context)
        {
            db = context;
            dbSet = context.EventParticipants;
        }

        public async Task<EventParticipant> Get(string id) => (await dbSet.FirstOrDefaultAsync(e => e.Id == id))!;

        public async Task<List<EventParticipant>> GetList() => (await dbSet.ToListAsync());

        public async Task<EventParticipant?> Save(EventParticipant data)
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

        public async Task<EventParticipant?> Update(string id, EventParticipant data)
        {
            var dbEventParticipant = await Get(id);
            if (dbEventParticipant is not null)
            {
                dbEventParticipant.ParticipantCount = data.ParticipantCount;
                dbEventParticipant.AdditionalInfo = data.AdditionalInfo;
                dbEventParticipant.PaymentMethod = data.PaymentMethod;

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
            var dbEventParticipant = await Get(id);
            if (dbEventParticipant is not null)
            {
                dbSet.Remove(dbEventParticipant);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public List<EventParticipant> GetEventParticipantListByEventId(string id)
        {
            return dbSet.Where(x => x.EventId == id).ToList();
        }
    }
}
