using _20220211.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace _2022_02_11.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly UserManager<IdentityUser?> _userManager;

        private readonly ApplicationDbContext _db;

        public EfUnitOfWork(UserManager<IdentityUser> userManager, ApplicationDbContext db)
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

        //private IPlaylistsRepository _playlists;
        //public IPlaylistsRepository Playlists
        //{
        //    get
        //    {
        //        if (_playlists == null)
        //            _playlists = new PlaylistsRepository(_db);

        //        return _playlists;
        //    }
        //}

        //private IVideosRepository _videos;
        //public IVideosRepository Videos
        //{
        //    get
        //    {
        //        if (_videos == null)
        //            _videos = new VideosRepository(_db);

        //        return _videos;
        //    }
        //}

        //private ICommentsRepository _comments;
        //public ICommentsRepository Comments
        //{
        //    get
        //    {
        //        if (_comments == null)
        //            _comments = new CommentsRepository(_db);

        //        return _comments;
        //    }
        //}

        public async Task CommitChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}