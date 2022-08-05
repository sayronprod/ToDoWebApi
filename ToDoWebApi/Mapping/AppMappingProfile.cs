using AutoMapper;
using ToDo.Models;
using ToDo.Models.ModelsDbo;
using ToDo.Models.ModelsDto;

namespace ToDo.WebApi.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<NoteDbo, MyNote>().ReverseMap();
            CreateMap<NoteDbo, Note>().ReverseMap();
            CreateMap<UserDbo, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(so => so.UserRoles.Select(t => t.RoleName).ToList()))
                .ReverseMap();
        }
    }
}
