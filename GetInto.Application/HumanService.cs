using AutoMapper;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using GetInto.Domain;
using GetInto.Persistence.Contracts;
using GetInto.Persistence.Pagination;

namespace GetInto.Application
{
    public class HumanService : IHumanService
    {
        private readonly IHumanPersist _humanPersist;
        private readonly IMapper _mapper;

        public HumanService(IHumanPersist humanPersist, IMapper mapper)
        {
            _humanPersist = humanPersist;
            _mapper = mapper;
        }

        public async Task<HumanDto> AddHuman(long userId, HumanAddDto model)
        {
            try
            {
                var human = _mapper.Map<Human>(model);
                human.UserId = userId;

                _humanPersist.Add(human);
                if (await _humanPersist.SaveChangesAsync())
                {
                    var result = await _humanPersist.GetHumanByUserIdAsync(userId, false);
                    return _mapper.Map<HumanDto>(result);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<HumanDto>> GetAllHumansAsync(PageParams pageParams, bool includeProjects = false)
        {
            try
            {
                var humans = await _humanPersist.GetAllHumansAsync(pageParams, includeProjects);
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

        public async Task<HumanDto> GetHumanByUserIdAsync(long userId, bool includeProjects = false)
        {
            try
            {
                var human = await _humanPersist.GetHumanByUserIdAsync(userId, includeProjects);
                if (human == null) return null;

                var result = _mapper.Map<HumanDto>(human);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<HumanDto> UpdateHuman(long userId, HumanUpdateDto model)
        {
            try
            {
                var human = await _humanPersist.GetHumanByUserIdAsync(userId);
                if (human == null) return null;

                model.Id = human.Id;
                model.UserId = userId;

                _mapper.Map<Human>(model);
                if (await _humanPersist.SaveChangesAsync())
                {
                    var result = await _humanPersist.GetHumanByUserIdAsync(userId);
                    return _mapper.Map<HumanDto>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}