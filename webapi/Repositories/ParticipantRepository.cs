using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;

namespace webapi.Repositories
{
    public class ParticipantRepository : IParticipantRepository<Participant>
    {

        private readonly ApplicationDbContext db;
        private readonly DbSet<Participant> dbSet;

        public ParticipantRepository(ApplicationDbContext context)
        {
            db = context;
            dbSet = context.Participants;
        }

        public async Task<Participant> Get(string id) => (await dbSet.FirstOrDefaultAsync(e => e.Id == id))!;

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

        public async Task<List<Participant>> GetList() => (await dbSet.ToListAsync());

        public async Task<Participant?> Save(Participant data)
        {
            try
            {
                dbSet.Add(data);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            return await Get(data.Id);
        }

        public async Task<Participant?> Update(string id, Participant data)
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
        public async Task<bool> Delete(string id)
        {
            var dbParticipant = await Get(id);
            if (dbParticipant is not null)
            {
                dbSet.Remove(dbParticipant);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
