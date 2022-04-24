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
        IUsersProfileRepository UsersProfile { get; }
        //IVideosRepository Videos { get; }
        //ICommentsRepository Comments { get; }
        Task CommitIdentityChangesAsync();
        Task CommitChangesAsync();
    }
}
