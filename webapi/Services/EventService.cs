using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventService : BaseService<Event, EventDTO>, IEventService<Event, EventDTO>
    {
        private readonly IBaseRepository<Event> repo;
        private readonly IEventParticipantRepository<EventParticipant> eventParticipantRepo;
        private readonly IBaseRepository<Participant> participantRepo;

        public EventService(IBaseRepository<Event> repository, 
            IEventParticipantRepository<EventParticipant> eventParticipantRepository, 
            IParticipantRepository<Participant> participantRepository) : base(repository)
        {
            repo = repository;      
            eventParticipantRepo = eventParticipantRepository;
            participantRepo = participantRepository;
        }

        public override Event DtoToEntity(EventDTO dto)
        {
            return new Event
            {
                Name = dto.Name,
                EventTime = dto.EventTime.ToUniversalTime(),
                Location = dto.Location,
                AdditionalInfo = dto.AdditionalInfo
            };
        }

        public async Task<EventWithParticipantCount> GetEventWithParticipantCount(string eventId)
        {
            var count = await GetParticipantCount(eventId);
            var eventEntity = await repo.Get(eventId);

            return new EventWithParticipantCount
            {
                EventId = eventId,
                Name = eventEntity.Name,
                EventTime = eventEntity.EventTime,
                Location = eventEntity.Location,
                AdditionalInfo = eventEntity.AdditionalInfo ?? string.Empty,
                ParticipantCount = count
            };
        }

        public async Task<List<EventWithParticipantCount>[]> GetEventWithParticipantsList()
        {
            var events = (await repo.GetList()).OrderBy(x => x.EventTime);
            var returnArray = new List<EventWithParticipantCount>[2];
            returnArray[0] = new List<EventWithParticipantCount>();
            returnArray[1] = new List<EventWithParticipantCount>();
            foreach (var item in events) 
            {
                if (item.EventTime >= DateTime.Today.ToUniversalTime()) // index 0 - future events, index 1 - past event
                    returnArray[0].Add(await GetEventWithParticipantCount(item.Id));
                else
                    returnArray[1].Add(await GetEventWithParticipantCount(item.Id));
            }

            return returnArray;
        }

        public async Task<int> GetParticipantCount(string id)
        {
            var eventParticipants = eventParticipantRepo.GetEventParticipantListByEventId(id);
            int count = 0;

            foreach (var item in eventParticipants)
            {
                var participant = await participantRepo.Get(item.ParticipantId);
                if (participant.IsCompany)
                    count += item.ParticipantCount ?? 0;
                else
                    count++;
            }
            return count;
        }
    }
}
