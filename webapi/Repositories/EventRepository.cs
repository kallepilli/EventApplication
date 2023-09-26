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


        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Save(Event data)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Update(string id, Event data)
        {
            throw new NotImplementedException();
        }
    }
}
