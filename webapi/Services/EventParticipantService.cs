using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class EventParticipantService : IEventParticipantService<EventParticipant, EventParticipantDTO>
    {
        private readonly IEventParticipantRepository<EventParticipant> repo;

        public EventParticipantService(IEventParticipantRepository<EventParticipant> repository)
        {
            repo = repository;
        }

        public async Task<EventParticipant> Get(string id) => await repo.Get(id);
        public async Task<List<EventParticipant>> GetList() => await repo.GetList();
        public async Task<EventParticipant> Save(EventParticipantDTO data) => await repo.Save(DtoToEntity(data));
        public async Task<EventParticipant> Update(string id, EventParticipantDTO data) => await repo.Update(id, DtoToEntity(data));
        public async Task<bool> Delete(string id) => await repo.Delete(id);
        public EventParticipant DtoToEntity(EventParticipantDTO dto)
        {
            return new EventParticipant
            {
                EventId = dto.EventId,
                IdCode = dto.IdCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CompanyName = dto.CompanyName,
                ParticipantCount = dto.ParticipantCount,
                AdditionalInfo = dto.AdditionalInfo,
                PaymentMethod = dto.PaymentMethod,
                IsCompany = dto.IsCompany
            };
        }

        public List<EventParticipant> GetParticipantListByEventId(string id)
        {
            return repo.GetParticipantListByEventId(id);
        }
    }
}
