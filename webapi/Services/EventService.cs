using webapi.Data.Model;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventService : IBaseService<Event>
    {
        private readonly IBaseRepository<Event> repo;
        private readonly IConfiguration _configuration;

        public EventService(IBaseRepository<Event> repository, IConfiguration configuration)
        {
            repo = repository;
            _configuration = configuration;
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
