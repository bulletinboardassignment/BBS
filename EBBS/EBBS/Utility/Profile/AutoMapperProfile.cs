using AutoMapper;
using EBBS.Data;
using EBBS.Models;

namespace EBBS.Models
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, Models.RoleViewModel>().ReverseMap();

            CreateMap<SecurityQuestion, Models.SecurityQuestionViewModel>().ReverseMap();

            CreateMap<Category, Models.CategoryViewModel>().ReverseMap();

            CreateMap<User, Models.UserViewModel>().ReverseMap();
        }
    }
}