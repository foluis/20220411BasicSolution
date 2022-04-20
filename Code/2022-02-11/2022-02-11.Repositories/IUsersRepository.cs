using _2022_02_11.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_02_11.Repositories
{
    public interface IUsersRepository
    {
        Task<ApplicationUser?> GetUserByIdAsync(string id);

        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        Task CreateUserAsync(ApplicationUser user, string password, string role);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<string> GetUserRoleAsync(ApplicationUser user);
    }
}
