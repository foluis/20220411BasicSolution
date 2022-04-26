using _2022_02_11.Entities.DTOs;
using _2022_02_11.Entities.Models;
using _2022_02_11.Entities.Models.Mappers;
using _2022_02_11.Infrastructure;
using _2022_02_11.Repositories;

namespace _2022_02_11.Services
{
    public interface IUsersProfileService
    {
        Task<OperationResponse<UserProfile>> CreateAsync(UserProfile model);

        CollectionResponse<UserProfile> GetAll(string query, int pageNumber = 1, int pageSize = 10);

        Task<OperationResponse<UserProfile>> GetByIdAsync(int id);

        Task<OperationResponse<UserProfile>> GetByUserId(string usersProfileId);

        Task<OperationResponse<UserProfile>> RemoveAsync(int id);

        Task<OperationResponse<UserProfile>> UpdateAsync(UserProfile model);
    }

    public class UsersProfileService : IUsersProfileService
    {
        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;

        public UsersProfileService(IUnitOfWork unitOfWork, IdentityOptions identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }

        public async Task<OperationResponse<UserProfile>> CreateAsync(UserProfile model)
        {
            var currentUser = _identity.UserId;

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

        public CollectionResponse<UserProfile> GetAll(string query, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 5)
                pageSize = 5;

            if (pageSize > 50)
                pageSize = 50;

            var usersProfile = _unitOfWork.UsersProfile.GetAll();
            int usersProfileCount = usersProfile.Count();

            var usersProfilePage = usersProfile
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(p => p.ToUsersProfile());

            var pageCount = usersProfileCount / pageSize;

            if (usersProfileCount % pageSize != 0)
                pageCount++;

            return new CollectionResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "Retrive successfully",
                Records = usersProfilePage,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = pageCount
            };
        }

        public async Task<OperationResponse<UserProfile>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.UsersProfile.GetByIdAsync(id);

            return new OperationResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "User profile",
                Data = result.ToUsersProfile()
            };
        }

        public async Task<OperationResponse<UserProfile>> GetByUserId(string usersProfileId)
        {
            var result = await _unitOfWork.UsersProfile.GetByUserId(usersProfileId);           

            return new OperationResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "User profile",
                Data = result.ToUsersProfile()
            };
        }

        public async Task<OperationResponse<UserProfile>> RemoveAsync (int id)
        {
            var result = await _unitOfWork.UsersProfile.GetByIdAsync(id);

            if (result == null)
                return new OperationResponse<UserProfile>
                {
                    IsSuccess = false,
                    Message = "User profile not found",
                    Data = null
                };

            _unitOfWork.UsersProfile.Remove(result);
            await _unitOfWork.CommitChangesAsync();

            return new OperationResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "User profile has been deleted successfully!",
                Data = result.ToUsersProfile()
            };
        }

        public async Task<OperationResponse<UserProfile>> UpdateAsync(UserProfile model)
        {
            var result = await _unitOfWork.UsersProfile.GetByIdAsync(model.Id);

            if(result == null)
                return new OperationResponse<UserProfile>
                {
                    IsSuccess = false,
                    Message = "User profile not found",
                    Data = null
                };

            result.FirstName = model.FirstName;
            result.LastName = model.LastName;
            result.UserId = model.UserId;
            
            await _unitOfWork.CommitChangesAsync();

            return new OperationResponse<UserProfile>
            {
                IsSuccess = true,
                Message = "User profile has been updated successfully!",
                Data = model
            };
        }
    }
}