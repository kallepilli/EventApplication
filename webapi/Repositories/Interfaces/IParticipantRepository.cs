using webapi.Data.Model.DTOs;

namespace webapi.Repositories.Interfaces
{
    public interface IParticipantRepository<T> : IBaseRepository<T>
    where T : class
    {
        Task<T> GetByNameAndIdCode(T data);
        bool IsIdCodeAvailable(string idCode);
    }

}
