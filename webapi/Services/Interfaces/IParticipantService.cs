using webapi.Data.Model;

namespace webapi.Services.Interfaces
{
    public interface IParticipantService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class
    where TDto : class

    {
        bool IsIdCodeAvailable(string idCode, string currentIdCode);
    }
}
