using _2022_02_11.Entities.Context;
using _2022_02_11.Entities.DTOs;
using _2022_02_11.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _2022_02_11.Repositories
{
    public interface IUsersProfileRepository
    {
        //Task CreateAsync(UserProfile userProfile);
        Task CreateAsync(TblUserProfile userProfile);

        void Remove(UserProfile userProfile);

        Task<IEnumerable<UserProfile>> GetAll();

        Task<UserProfile> GetByUserId(string usersProfileId);

        Task<UserProfile> GetByIdAsync(int id);
    }

    public class UsersProfileRepository : IUsersProfileRepository
    {
        private readonly _20220211DatabaseContext _context;
        //private readonly IMapper _mapper;

        public UsersProfileRepository(_20220211DatabaseContext dataContext/*, IMapper mapper*/)
        {
            _context = dataContext;
            //_mapper = mapper;
        }

        public async Task CreateAsync(TblUserProfile userProfile)
        {
            await _context.TblUserProfiles.AddAsync(userProfile);

            //await _context.TblUserProfiles.AddAsync(new TblUserProfile()
            //{
            //    FirstName = userProfile.FirstName,
            //    LastName = userProfile.LastName,
            //    UserId = userProfile.UserId
            //});

            //var result = await _context.TblUserProfiles.AddAsync(_mapper.Map<TblUserProfile>(userProfile));
            //await _context.SaveChangesAsync();

            ////var newEntity = _mapper.Map<UserProfile>(result.Entity);
            ////return newEntity;
        }

        public void Remove(UserProfile userProfile)
        {
            _context.TblUserProfiles.Remove(new TblUserProfile()
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                UserId = userProfile.UserId
            });
        }

        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            var result = _context.TblUserProfiles;
            return result.Select(x => new UserProfile()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserId = x.UserId
            }).ToList();

            //return _context.TblUserProfiles;

            //var result = await _context.TblUserProfiles.ToListAsync();

            //return _mapper.Map<List<UserProfile>>(result);
        }

        public async Task<UserProfile> GetByUserId(string usersProfileId)
        {
            //return await _context.TblUserProfiles
            //                .Include(p => p.PlaylistVideos)
            //                .ThenInclude(p => p.Video)
            //                .SingleOrDefaultAsync(p => p.Id == id);

            var result = await _context.TblUserProfiles
               .FirstOrDefaultAsync(e => e.UserId == usersProfileId);

            return new UserProfile()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                UserId = result.UserId
            };

            //return _mapper.Map<UserProfile>(result);
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            //return await _context.TblUserProfiles
            //                .Include(p => p.PlaylistVideos)
            //                .ThenInclude(p => p.Video)
            //                .SingleOrDefaultAsync(p => p.Id == id);

            var result = await _context.TblUserProfiles
               .FirstOrDefaultAsync(e => e.Id == id);

            return new UserProfile()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                UserId = result.UserId
            };

            //return _mapper.Map<UserProfile>(result);
        }
    }
}