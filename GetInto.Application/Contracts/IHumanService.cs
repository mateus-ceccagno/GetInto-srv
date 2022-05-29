using GetInto.Application.Dtos;
using GetInto.Persistence.Pagination;

namespace GetInto.Application.Contracts
{
    public interface IHumanService
    {
        Task<HumanDto> AddHuman(HumanAddDto model);
        Task<HumanDto> UpdateHuman(HumanUpdateDto model);

        Task<PageList<HumanDto>> GetAllHumansAsync(PageParams pageParams);
        Task<HumanDto> GetHumanByIdAsync(long id);
    }
}
