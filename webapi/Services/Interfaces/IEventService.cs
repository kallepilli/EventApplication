using webapi.Data.Model;

namespace webapi.Services.Interfaces
{
    public interface IEventService<T, R> : IBaseService<T, R>
    where T : class
    where R : class

    {
        Task<EventWithParticipants> GetEventWithParticipants(string eventId);
        Task<List<EventWithParticipants>> GetEventWithParticipantsList();
    }
}
