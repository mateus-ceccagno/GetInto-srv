using GetInto.Domain;
using GetInto.Persistence.Pagination;

namespace GetInto.Persistence.Contracts
{
    public interface IHumanPersist
    {
        // TODO: Add bool param 'includeProject' in both
        Task<PageList<Human>> GetAllHumansAsync(PageParams pageParams);
        Task<Human> GetHumanByUserIdAsync(long id);
    }
}
