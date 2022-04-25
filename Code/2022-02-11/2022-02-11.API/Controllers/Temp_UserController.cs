using _2022_02_11.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2022_02_11.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
public class Temp_UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public Temp_UserController(UserManager<ApplicationUser> userManager)
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

            var user = new ApplicationUser
            {
                Email = appUser.Email,
                UserName = appUser.Email
            };

            var userCreatedResult = await _userManager.CreateAsync(appUser, "Software1.");

            if (userCreatedResult.Succeeded)
            {
                var userRoleAsignationResult = await _userManager.AddToRoleAsync(appUser, "Admin");
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