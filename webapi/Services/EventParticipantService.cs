using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventParticipantService : BaseService<EventParticipant, EventParticipantDTO>, IEventParticipantService<EventParticipant, EventParticipantDTO>
    {
        private readonly IEventParticipantRepository<EventParticipant> repo;
        private readonly IParticipantRepository<Participant> participantRepo;
        public EventParticipantService(IEventParticipantRepository<EventParticipant> repository, 
            IParticipantRepository<Participant> participantRepository) : base(repository)
        {
            repo = repository;
            participantRepo = participantRepository;
        }

        public override EventParticipant DtoToEntity(EventParticipantDTO dto)
        {
            return new EventParticipant
            {
                EventId = dto.EventId,
                ParticipantId = dto.ParticipantId,
                ParticipantCount = dto.ParticipantCount,
                AdditionalInfo = dto.AdditionalInfo,
                PaymentMethod = dto.PaymentMethod
            };
        }

        public async Task<List<EventParticipantWithParticipant>> GetParticipantListByEventId(string id)
        {
            var eventParticipants = repo.GetEventParticipantListByEventId(id);
            var returnList = new List<EventParticipantWithParticipant>();

            foreach (var eventParticipant in eventParticipants)
            {
                returnList.Add(await GetEventParticipantWithParticipant(eventParticipant));
            }
            return returnList.OrderBy(p => p.FirstName ?? p.CompanyName).ToList();
        }

        public async Task<EventParticipantWithParticipant> GetEventParticipantWithParticipant(string eventParticipantId)
        {
            var eventParticipant = await repo.Get(eventParticipantId);
            return await GetEventParticipantWithParticipant(eventParticipant);
        }


        private async Task<EventParticipantWithParticipant> GetEventParticipantWithParticipant(EventParticipant eventParticipant)
        {
            var participant = await participantRepo.Get(eventParticipant.ParticipantId);
            var item = new EventParticipantWithParticipant
            {
                Id = eventParticipant.Id,
                EventId = eventParticipant.EventId,
                ParticipantId = eventParticipant.ParticipantId,
                IdCode = participant.IdCode,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                CompanyName = participant.CompanyName,
                IsCompany = participant.IsCompany,
                ParticipantCount = eventParticipant.ParticipantCount,
                AdditionalInfo = eventParticipant.AdditionalInfo,
                PaymentMethod = eventParticipant.PaymentMethod
            };

            return item;
        }
    }
}
