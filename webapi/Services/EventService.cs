using System.Reflection.Metadata.Ecma335;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventService : IEventService<Event, EventDTO>
    {
        private readonly IBaseRepository<Event> repo;
        private readonly IEventParticipantRepository<EventParticipant> eventParticipantRepo;

        public EventService(IBaseRepository<Event> repository, IEventParticipantRepository<EventParticipant> eventParticipantRepository)
        {
            repo = repository;      
            eventParticipantRepo = eventParticipantRepository;
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
                EventTime = dto.EventTime.ToUniversalTime(),
                Location = dto.Location,
                AdditionalInfo = dto.AdditionalInfo
            };
        }

        public async Task<EventWithParticipants> GetEventWithParticipants(string eventId)
        {
            var count = GetParticipantCount(eventId);
            var eventEntity = await repo.Get(eventId);

            return new EventWithParticipants
            {
                EventId = eventId,
                Name = eventEntity.Name,
                EventTime = eventEntity.EventTime,
                Location = eventEntity.Location,
                ParticipantCount = count
            };
        }

        public async Task<List<EventWithParticipants>[]> GetEventWithParticipantsList()
        {
            var events = (await repo.GetList()).OrderBy(x => x.EventTime);
            var returnArray = new List<EventWithParticipants>[2];
            returnArray[0] = new List<EventWithParticipants>();
            returnArray[1] = new List<EventWithParticipants>();
            foreach (var item in events) 
            {
                if (item.EventTime > DateTime.Today) // index 0 - future events, index 1 - past event
                    returnArray[0].Add(await GetEventWithParticipants(item.Id));
                else
                    returnArray[1].Add(await GetEventWithParticipants(item.Id));
            }

            return returnArray;
        }

        private int GetParticipantCount(string id)
        {
            var eventParticipants = eventParticipantRepo.GetParticipantListByEventId(id);
            int count = 0;

            foreach (var item in eventParticipants)
            {
                if (item.IsCompany)
                    count += item.ParticipantCount ?? 0;
                else
                    count++;
            }
            return count;
        }
    }
}
