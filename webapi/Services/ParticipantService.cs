using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class ParticipantService : IParticipantService<Participant, ParticipantDTO>
    {
        private readonly IParticipantRepository<Participant> repo;

        public ParticipantService(IParticipantRepository<Participant> repository)
        {
            repo = repository;
        }

        public async Task<Participant> Get(string id) => await repo.Get(id);
        public async Task<List<Participant>> GetList() => await repo.GetList();
        public async Task<Participant> Save(ParticipantDTO data)
        {
            var participant = await repo.GetByNameAndIdCode(DtoToEntity(data));
            if (participant != null) 
                return participant;
            return await repo.Save(DtoToEntity(data));
        } 
        public async Task<Participant> Update(string id, ParticipantDTO data) => await repo.Update(id, DtoToEntity(data));
        public async Task<bool> Delete(string id) => await repo.Delete(id);
        public Participant DtoToEntity(ParticipantDTO dto)
        {
            return new Participant
            {
                IdCode = dto.IdCode,
                FirstName = dto.IsCompany == true ? null : dto.FirstName,
                LastName = dto.IsCompany == true ? null : dto.LastName,
                CompanyName = dto.IsCompany == true ? dto.CompanyName : null,
                IsCompany = dto.IsCompany
            };
        }

        public bool IsIdCodeAvailable(string idCode) => repo.IsIdCodeAvailable(idCode);
        
    }
}
