using GetInto.Application.Dtos;
using GetInto.Persistence.Pagination;

namespace GetInto.Application.Contracts
{
    public interface IHumanService
    {
        Task<HumanDto> AddHuman(HumanAddDto model);
        Task<HumanDto> UpdateHuman(long id, HumanUpdateDto model);

        Task<PageList<HumanDto>> GetAllHumansAsync(PageParams pageParams, bool includeProjects = false);
        Task<HumanDto> GetHumanByIdAsync(long id, bool includeProjects = false);
    }
}
