using _2022_02_11.API.DataAccess.Interfaces;
using _2022_02_11.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2022_02_11.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly Temp_IUserProfileRepository _repository;

        public UserProfileController(Temp_IUserProfileRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersProfile()
        {
            try
            {
                return Ok(await _repository.GetUsersProfile());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            try
            {
                var result = await _repository.GetUserProfile(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserProfile(UserProfile userProfile)
        {
            try
            {
                if (userProfile == null)
                    return BadRequest();

                //var emp = await _repository.GetEntityByName(UserProfile.FirstName);

                //if (emp != null)
                //{
                //    ModelState.AddModelError("Email", "UserProfile name already in use");
                //    return BadRequest(ModelState);
                //}

                var createdUserProfile = await _repository.AddUserProfile(userProfile);

                return CreatedAtAction(nameof(GetUserProfile),
                    new { id = createdUserProfile.Id }, createdUserProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new User Profile record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserProfile>> UpdateTournament(int id, UserProfile userProfile)
        {
            try
            {
                if (id != userProfile.Id)
                    return BadRequest("User Profile ID mismatch");

                var UpdateUserProfileToUpdate = await _repository.GetUserProfile(id);

                if (UpdateUserProfileToUpdate == null)
                {
                    return NotFound($"User Profile with Id = {id} not found");
                }

                return await _repository.UpdateUserProfile(userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating User Profile record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUserProfile(int id)
        {
            try
            {
                var UserProfileToDelete = await _repository.GetUserProfile(id);

                if (UserProfileToDelete == null)
                {
                    return NotFound($"User profile with Id = {id} not found");
                }

                await _repository.DeleteUserProfile(id);

                return Ok($"User profile with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting User profile record");
            }
        }
    }
}
