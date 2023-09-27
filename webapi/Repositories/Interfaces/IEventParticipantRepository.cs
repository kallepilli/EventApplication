namespace webapi.Repositories.Interfaces
{
    public interface IEventParticipantRepository<T> : IBaseRepository<T>
    where T : class
    {
        List<T> GetParticipantListByEventId(string id);
    }

}
