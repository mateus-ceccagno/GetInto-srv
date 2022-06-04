using GetInto.Domain;
using GetInto.Persistence.Pagination;

namespace GetInto.Persistence.Contracts
{
    public interface IHumanPersist : IGeralPersist
    {
        Task<PageList<Human>> GetAllHumansAsync(PageParams pageParams, bool includeProjects = false);
        Task<Human> GetHumanByUserIdAsync(long userId, bool includeProjects = false);
    }
}
