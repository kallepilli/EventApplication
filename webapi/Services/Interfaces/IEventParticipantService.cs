using webapi.Data.Model;

namespace webapi.Services.Interfaces
{
    public interface IEventParticipantService<T, R> : IBaseService<T, R>
    where T : class
    where R : class

    {
        Task<List<EventParticipantWithParticipant>> GetParticipantListByEventId(string id);
        Task<EventParticipantWithParticipant> GetEventParticipantWithParticipant(string eventParticipantId);
    }
}
