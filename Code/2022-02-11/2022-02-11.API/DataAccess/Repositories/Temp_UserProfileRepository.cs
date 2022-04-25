using _2022_02_11.API.DataAccess.Interfaces;
using _2022_02_11.Entities.Context;
using _2022_02_11.Entities.DTOs;
using _2022_02_11.Entities.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _2022_02_11.API.DataAccess.Repositories
{
    public class Temp_UserProfileRepository : Temp_IUserProfileRepository
    {
        private readonly _20220211DatabaseContext _context;
        private readonly IMapper _mapper;

        public Temp_UserProfileRepository(_20220211DatabaseContext dataContext, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
        }

        public async Task<UserProfile> AddUserProfile(UserProfile userProfile)
        {
            var result = await _context.TblUserProfiles.AddAsync(_mapper.Map<TblUserProfile>(userProfile));
            await _context.SaveChangesAsync();
            var newEntity = _mapper.Map<UserProfile>(result.Entity);
            return newEntity;
        }

        public async Task DeleteUserProfile(int usersProfileId)
        {
            var result = await _context.TblUserProfiles
                .FirstOrDefaultAsync(e => e.Id == usersProfileId);

            if (result != null)
            {
                _context.TblUserProfiles.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserProfile> GetUserProfile(int usersProfileId)
        {
            var result = await _context.TblUserProfiles
                .FirstOrDefaultAsync(e => e.Id == usersProfileId);

            return _mapper.Map<UserProfile>(result);
        }

        public async Task<UserProfile> GetUserProfileByUserId(string userId)
        {
            var result = await _context.TblUserProfiles
        .FirstOrDefaultAsync(e => e.UserId == userId);

            return _mapper.Map<UserProfile>(result);
        }

        public async Task<IEnumerable<UserProfile>> GetUsersProfile()
        {
            var result = await _context.TblUserProfiles.ToListAsync();

            return _mapper.Map<List<UserProfile>>(result);
        }

        public Task<IEnumerable<UserProfile>> Search(string firstName, string? lastName)
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfile> UpdateUserProfile(UserProfile userProfile)
        {
            var currentEntity = await _context.TblUserProfiles
                .FirstOrDefaultAsync(e => e.Id == userProfile.Id);

            if (currentEntity != null)
            {
                _mapper.Map<UserProfile, TblUserProfile>(userProfile, currentEntity);

                await _context.SaveChangesAsync();

                return _mapper.Map<UserProfile>(currentEntity);
            }

            return null;
        }
    }
}
