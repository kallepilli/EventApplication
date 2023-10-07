using webapi.Data.Model;

namespace webapi.Services.Interfaces
{
    public interface IEventParticipantService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class
    where TDto : class

    {
        Task<List<EventParticipantWithParticipant>> GetParticipantListByEventId(string id);
        Task<EventParticipantWithParticipant> GetEventParticipantWithParticipant(string eventParticipantId);
    }
}
