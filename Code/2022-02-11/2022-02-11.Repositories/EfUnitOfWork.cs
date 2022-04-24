using _2022_02_11.Entities.Context;
using _2022_02_11.Entities.Models;
using _20220211.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace _2022_02_11.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _identityDb;
        private readonly _20220211DatabaseContext _db;

        public EfUnitOfWork(UserManager<ApplicationUser> userManager, ApplicationDbContext identityDb, _20220211DatabaseContext db)
        {
            _userManager = userManager;
            _identityDb = identityDb;
            _db = db;

        }

        private IUsersRepository? _users;

        public IUsersRepository Users
        {
            get
            {
                if (_users == null)
                    _users = new UsersRepository(_userManager);

                return _users;
            }
        }

        private IUsersProfileRepository? _usersProfile;

        public IUsersProfileRepository UsersProfile
        {
            get
            {
                if (_usersProfile == null)
                    _usersProfile = new UsersProfileRepository(_db);

                return _usersProfile;
            }
        }

        public async Task CommitIdentityChangesAsync()
        {
            await _identityDb.SaveChangesAsync();            
        }

        public async Task CommitChangesAsync()
        {            
            await _db.SaveChangesAsync();
        }
    }
}