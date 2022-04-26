using _2022_02_11.Entities.Context;
using _2022_02_11.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace _2022_02_11.Repositories
{
    public interface IUsersProfileRepository
    {       
        Task CreateAsync(TblUserProfile userProfile);

        IEnumerable<TblUserProfile> GetAll();

        Task<TblUserProfile> GetByIdAsync(int id);

        Task<TblUserProfile> GetByUserId(string usersProfileId);

        void Remove(TblUserProfile userProfile);

        Task<TblUserProfile> Update(TblUserProfile model);
    }

    public class UsersProfileRepository : IUsersProfileRepository
    {
        private readonly _20220211DatabaseContext _context;       

        public UsersProfileRepository(_20220211DatabaseContext dataContext)
        {
            _context = dataContext;
        }

        public async Task CreateAsync(TblUserProfile userProfile)
        {
            await _context.TblUserProfiles.AddAsync(userProfile);
        }

        public IEnumerable<TblUserProfile> GetAll()
        {
            return _context.TblUserProfiles;
        }

        public async Task<TblUserProfile> GetByIdAsync(int id)
        {
            return await _context.TblUserProfiles.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TblUserProfile> GetByUserId(string usersProfileId)
        {
            return await _context.TblUserProfiles.FirstOrDefaultAsync(e => e.UserId == usersProfileId);
        }

        public void Remove(TblUserProfile userProfile)
        {
            _context.TblUserProfiles.Remove(userProfile);          
        }

        public async Task<TblUserProfile> Update(TblUserProfile model)
        {
            return null;
        }
    }
}