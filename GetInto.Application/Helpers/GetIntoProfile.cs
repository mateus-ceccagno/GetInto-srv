using AutoMapper;
using GetInto.Application.Dtos;
using GetInto.Domain;

namespace GetInto.Application.Helpers
{
    public class GetIntoProfile : Profile
    {
        public GetIntoProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Job, JobDto>().ReverseMap();
            CreateMap<Human, HumanDto>().ReverseMap();
            CreateMap<Human, HumanAddDto>().ReverseMap();
            CreateMap<Human, HumanUpdateDto>().ReverseMap();
        }
    }
}
