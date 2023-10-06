using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services.Interfaces;

namespace webapi.Services
{
    public class ParticipantService : BaseService<Participant, ParticipantDTO>, IParticipantService<Participant, ParticipantDTO>
    {
        private readonly IParticipantRepository<Participant> repo;

        public ParticipantService(IParticipantRepository<Participant> repository) : base(repository)
        {
            repo = repository;
        }


        public async override Task<Participant> Save(ParticipantDTO data)
        {
            var participant = await repo.GetByNameAndIdCode(DtoToEntity(data));
            if (participant != null) 
                return participant;
            return await repo.Save(DtoToEntity(data));
        } 

        public override Participant DtoToEntity(ParticipantDTO dto)
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
