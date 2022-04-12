using _2022_02_11.Entities.DTOs;
using _2022_02_11.Entities.Models;
using AutoMapper;

namespace _2022_02_11.API.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserProfile, TblUserProfile>().ReverseMap();
        }
    }
}
