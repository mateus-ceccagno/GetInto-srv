using GetInto.Application.Dtos;
using GetInto.Persistence.Pagination;

namespace GetInto.Application.Contracts
{
    public interface IHumanService
    {
        Task<HumanDto> AddHuman(long userId, HumanAddDto model);
        Task<HumanDto> UpdateHuman(long userId, HumanUpdateDto model);

        Task<PageList<HumanDto>> GetAllHumansAsync(PageParams pageParams, bool includeProjects = false);
        Task<HumanDto> GetHumanByUserIdAsync(long userId, bool includeProjects = false);
    }
}
