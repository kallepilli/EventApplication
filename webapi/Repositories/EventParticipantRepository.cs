using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace webapi.Repositories
{
    public class EventParticipantRepository : BaseRepository<EventParticipant>, IEventParticipantRepository<EventParticipant>
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<EventParticipant> dbSet;

        public EventParticipantRepository(ApplicationDbContext context) : base(context)
        {
            db = context;
            dbSet = context.EventParticipants;
        }


        public override async Task<EventParticipant?> Update(string id, EventParticipant data)
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

        public List<EventParticipant> GetEventParticipantListByEventId(string id)
        {
            return dbSet.Where(x => x.EventId == id).ToList();
        }
    }
}
