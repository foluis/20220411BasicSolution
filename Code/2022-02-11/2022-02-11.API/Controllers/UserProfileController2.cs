using _2022_02_11.Entities.Models;
using _2022_02_11.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2022_02_11.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController2 : ControllerBase
    {
        private readonly IUsersProfileService _usersProfileService;

        public UserProfileController2(IUsersProfileService usersProfileService)
        {
            _usersProfileService = usersProfileService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserProfile(UserProfile model)
        {
            try
            {
                var result = await _usersProfileService.CreateAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new User Profile record");
            }
        }
    }
}
