using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_02_11.Repositories
{
    public interface IUnitOfWork
    {

        IUsersRepository Users { get; }
        //IPlaylistsRepository Playlists { get; }
        //IVideosRepository Videos { get; }
        //ICommentsRepository Comments { get; }
        Task CommitChangesAsync();
    }
}
