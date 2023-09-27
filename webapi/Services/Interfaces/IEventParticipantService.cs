namespace webapi.Services.Interfaces
{
    public interface IEventParticipantService<T, R> : IBaseService<T, R>
    where T : class
    where R : class

    {
        List<T> GetParticipantListByEventId(string id);
    }
}
