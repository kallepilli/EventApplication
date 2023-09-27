using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventService : IBaseService<Event, EventDTO>
    {
        private readonly IBaseRepository<Event> repo;

        public EventService(IBaseRepository<Event> repository)
        {
            repo = repository;           
        }

        public async Task<Event> Get(string id) => await repo.Get(id);
        public async Task<List<Event>> GetList() => await repo.GetList();
        public async Task<Event> Save(EventDTO data) => await repo.Save(DtoToEntity(data));
        public Task<Event> Update(string id, EventDTO data) => throw new NotImplementedException();
        public async Task<bool> Delete(string id) => await repo.Delete(id);
        
        public Event DtoToEntity(EventDTO dto)
        {
            return new Event
            {
                Name = dto.Name,
                EventTime = dto.EventTime,
                Location = dto.Location,
                AdditionalInfo = dto.AdditionalInfo
            };
        }
    }
}
