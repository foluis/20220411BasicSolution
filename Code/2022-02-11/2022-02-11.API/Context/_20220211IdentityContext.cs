using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _2022_02_11.API.Context
{
    public class _20220211IdentityContext : IdentityDbContext
    {
        public _20220211IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }

}
