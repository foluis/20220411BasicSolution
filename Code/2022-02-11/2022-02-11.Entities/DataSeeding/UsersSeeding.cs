using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_02_11.Entities.DataSeeding
{
    public class UsersSeeding
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersSeeding(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            if (await _roleManager.FindByNameAsync("Admin") != null)
                return;

            // Create role 
            var adminRole = new IdentityRole { Name = "Admin" };
            await _roleManager.CreateAsync(adminRole);

            var userRole = new IdentityRole { Name = "User" };
            await _roleManager.CreateAsync(userRole);

            // Create user 
            var admin = new IdentityUser
            {
                Email = "foluis@foluis.com",
                UserName = "foluis@foluis.com"
            };
            await _userManager.CreateAsync(admin, "Software1");

            await _userManager.AddToRoleAsync(admin, "Admin");
        }

    }
}
