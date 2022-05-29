using AutoMapper;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using GetInto.Persistence.Contracts;
using GetInto.Persistence.Pagination;

namespace GetInto.Application
{
    public class HumanService : IHumanService
    {
        private readonly IHumanPersist _humanPersist;
        private readonly IMapper _mapper;

        public HumanService(IHumanPersist humanPersist)
        {
            _humanPersist = humanPersist;
        }

        public Task<HumanDto> AddHuman(HumanAddDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<PageList<HumanDto>> GetAllHumansAsync(PageParams pageParams)
        {
            try
            {
                var humans = await _humanPersist.GetAllHumansAsync(pageParams);
                if (humans == null) return null;

                var result = _mapper.Map<PageList<HumanDto>>(humans);

                result.CurrentPage = humans.CurrentPage;
                result.TotalPages = humans.TotalPages;
                result.PageSize = humans.PageSize;
                result.TotalCount = humans.TotalCount;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<HumanDto> GetHumanByIdAsync(long id)
        {
            try
            {
                var human = await _humanPersist.GetHumanByUserIdAsync(id);
                if (human == null) return null;

                var result = _mapper.Map<HumanDto>(human);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<HumanDto> UpdateHuman(HumanUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}