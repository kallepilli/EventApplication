using webapi.Data.Model;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventParticipantService : IBaseService<EventParticipant>
    {
        private readonly IBaseRepository<EventParticipant> repo;
        private readonly IConfiguration _configuration;

        public EventParticipantService(IBaseRepository<EventParticipant> repository, IConfiguration configuration)
        {
            repo = repository;
            _configuration = configuration;
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EventParticipant> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<EventParticipant> Save(EventParticipant data)
        {
            throw new NotImplementedException();
        }

        public Task<EventParticipant> Update(string id, EventParticipant data)
        {
            throw new NotImplementedException();
        }
    }
}
