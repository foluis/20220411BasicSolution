using _2022_02_11.Entities.Models;
using _20220211.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace _2022_02_11.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _db;

        public EfUnitOfWork(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
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

        public async Task CommitChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}