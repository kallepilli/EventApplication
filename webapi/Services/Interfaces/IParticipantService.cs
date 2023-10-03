using webapi.Data.Model;

namespace webapi.Services.Interfaces
{
    public interface IParticipantService<T, R> : IBaseService<T, R>
    where T : class
    where R : class

    {
        bool IsIdCodeAvailable(string idCode);
    }
}
