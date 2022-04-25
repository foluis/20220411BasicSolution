using _2022_02_11.Entities.Models;

namespace _2022_02_11.API.DataAccess.Interfaces
{

    public interface Temp_IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> Search(string firstName, string? lastName);
        Task<IEnumerable<UserProfile>> GetUsersProfile();
        Task<UserProfile> GetUserProfile(int usersProfileId);
        Task<UserProfile> GetUserProfileByUserId(string userId);
        Task<UserProfile> AddUserProfile(UserProfile userProfile);
        Task<UserProfile> UpdateUserProfile(UserProfile userProfile);
        Task DeleteUserProfile(int usersProfileId);
    }
}
