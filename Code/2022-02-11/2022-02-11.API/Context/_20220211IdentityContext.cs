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


    //public class _20220211IdentityContext : IdentityDbContext
    //{


    //    public _20220211IdentityContext(DbContextOptions options) : base(options)
    //    {
    //    }


    //    //public _20220211IdentityContext(DbContextOptions<_20220211IdentityContext> options) : base(options)
    //    //{
    //    //}

    //    ////protected override void OnModelCreating(ModelBuilder builder)
    //    ////{
    //    ////    base.OnModelCreating(builder);
    //    ////    // Customize the ASP.NET Core Identity model and override the defaults if needed.
    //    ////    // For example, you can rename the ASP.NET Core Identity table names and more.
    //    ////    // Add your customizations after calling base.OnModelCreating(builder);
    //    ////}
    //}

}
