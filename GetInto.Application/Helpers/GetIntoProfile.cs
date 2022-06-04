using AutoMapper;
using GetInto.Application.Dtos;
using GetInto.Domain;
using GetInto.Domain.Identity;

namespace GetInto.Application.Helpers
{
    public class GetIntoProfile : Profile
    {
        public GetIntoProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<SocialLink, SocialLinkDto>().ReverseMap();
            CreateMap<Human, HumanDto>().ReverseMap();
            CreateMap<Human, HumanAddDto>().ReverseMap();
            CreateMap<Human, HumanUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}
