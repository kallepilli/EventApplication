using webapi.Data.Model;

namespace webapi.Services.Interfaces
{
    public interface IEventService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class
    where TDto : class

    {
        Task<EventWithParticipantCount> GetEventWithParticipantCount(string eventId);
        Task<List<EventWithParticipantCount>[]> GetEventWithParticipantsList();
        Task<int> GetParticipantCount(string id);
    }
}
