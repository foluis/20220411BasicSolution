using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2022_02_11.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Temp_RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public Temp_RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateRole(string roleName)
        {
            try
            {
                if (await _roleManager.FindByNameAsync(roleName) != null)
                    return BadRequest("Role name already exists");

                var role = new IdentityRole
                {
                    Name = roleName
                };

                var roleCreationResult = await _roleManager.CreateAsync(role);

                if (roleCreationResult.Succeeded)
                    return Ok("Role created");
                else
                    return BadRequest(string.Join("*", roleCreationResult.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new role");
            }
        }
    }
}
