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
    public class UserProfileController : ControllerBase
    {
        private readonly IUsersProfileService _usersProfileService;

        public UserProfileController(IUsersProfileService usersProfileService)
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

        [ProducesResponseType(200, Type = typeof(CollectionResponse<UserProfile>))]
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            var result = _usersProfileService.GetAll("",pageNumber, pageSize);
            return Ok(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfile>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfile>))]
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UserProfile model)
        {
            var result = await _usersProfileService.UpdateAsync(model);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfile>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfile>))]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usersProfileService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
