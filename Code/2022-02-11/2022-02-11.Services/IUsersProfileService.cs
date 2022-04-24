using _2022_02_11.Entities.DTOs;
using _2022_02_11.Entities.Models;
using _2022_02_11.Repositories;

namespace _2022_02_11.Services
{
    public interface IUsersProfileService
    {
        Task<OperationResponse<UserProfile>> CreateAsync(UserProfile userProfile);

        //void Remove(UserProfile userProfile);

        Task<IEnumerable<UserProfile>> GetAll();
        //CollectionResponse<VideoDetail> GetAllVideos(string query, int pageNumber = 1, int pageSize = 10);

        Task<UserProfile> GetByUserId(string usersProfileId);

        //Task<UserProfile> GetByIdAsync(int id);
    }

    public class UsersProfileService : IUsersProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResponse<UserProfile>> CreateAsync(UserProfile model)
        {
            var userProfile = new TblUserProfile()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = model.UserId
            };

            await _unitOfWork.UsersProfile.CreateAsync(userProfile);
            await _unitOfWork.CommitChangesAsync();

            model.Id = userProfile.Id;

            return new OperationResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "User profile created successfully",
                Data = model
            };
        }

        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            return await _unitOfWork.UsersProfile.GetAll();
        }

        public async Task<UserProfile> GetByUserId(string usersProfileId)
        {
            return await _unitOfWork.UsersProfile.GetByUserId(usersProfileId);
        }
    }
}