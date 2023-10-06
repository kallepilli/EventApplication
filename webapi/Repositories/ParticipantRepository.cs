using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace webapi.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository<Participant>
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<Participant> dbSet;

        public ParticipantRepository(ApplicationDbContext context) : base(context)
        {
            db = context;
            dbSet = context.Participants;
        }

        public async Task<Participant> GetByNameAndIdCode(Participant data)
        {
            if (!data.IsCompany)
            {
                return dbSet.FirstOrDefault(x => x.FirstName.ToLower() == data.FirstName.ToLower() && x.LastName.ToLower() == data.LastName.ToLower() && x.IdCode.ToLower() == data.IdCode.ToLower());
            }
            else
            {
                return dbSet.FirstOrDefault(x => x.CompanyName.ToLower() == data.CompanyName.ToLower() && x.IdCode.ToLower() == data.IdCode.ToLower());
            }
        }


        public override async Task<Participant?> Update(string id, Participant data)
        {
            var dbParticipant = await Get(id);
            if (dbParticipant is not null)
            {
                dbParticipant.IdCode = data.IdCode;
                dbParticipant.FirstName = data.FirstName;
                dbParticipant.LastName = data.LastName;
                dbParticipant.CompanyName = data.CompanyName;
                dbParticipant.IsCompany = data.IsCompany;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }

                return data;
            }
            return null;
        }

        public bool IsIdCodeAvailable(string idCode)
        {
            if (dbSet.FirstOrDefault(x => x.IdCode == idCode && x.IsCompany == false) == null)
                return true;
            return false;
        }
    }
}
