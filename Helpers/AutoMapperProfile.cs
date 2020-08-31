using AutoMapper;
using DwitterApp.Entities;
using DwitterApp.Models;

namespace DwitterApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
        
    }
}