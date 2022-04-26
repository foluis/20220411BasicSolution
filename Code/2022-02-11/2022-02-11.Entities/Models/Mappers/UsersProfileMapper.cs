using _2022_02_11.Entities.DTOs;

namespace _2022_02_11.Entities.Models.Mappers
{
    public static class UsersProfileMapper
    {
        //public static UserProfile ToUsersProfile(this UserProfile userProfile, IEnumerable<TblUserProfile> tblUserProfile = null)
        //{
        //    return new UserProfile
        //    {
        //        Id = userProfile.Id,
        //        FirstName = userProfile.FirstName,
        //        LastName = userProfile.LastName,
        //        UserId = userProfile.UserId
        //    };
        //}

        public static UserProfile ToUsersProfile(this TblUserProfile userProfile/*, IEnumerable<TblUserProfile> tblUserProfile = null*/)
        {
            return new UserProfile
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                UserId = userProfile.UserId
            };
        }
    }
}