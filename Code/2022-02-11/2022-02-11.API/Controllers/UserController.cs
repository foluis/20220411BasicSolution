using _2022_02_11.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2022_02_11.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult<string>> RegisterUser(ApplicationUser appUser)
    {
        try
        {
            if (await _userManager.FindByEmailAsync(appUser.Email) != null)
                return BadRequest("Email already exists");

            var user = new IdentityUser
            {
                Email = appUser.Email,
                UserName = appUser.Email
            };

            var userCreatedResult = await _userManager.CreateAsync(user, "Software1.");

            if (userCreatedResult.Succeeded)
            {
                var userRoleAsignationResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (userRoleAsignationResult.Succeeded)
                    return Ok();
                else
                    return BadRequest(string.Join("*", userRoleAsignationResult.Errors));
            }
            else
                return BadRequest(string.Join("*", userCreatedResult.Errors));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new User Profile record");
        }
    }
}
}