using GetInto.Domain;
using GetInto.Persistence.Pagination;

namespace GetInto.Persistence.Contracts
{
    public interface IProjectPersist : IGeralPersist
    {
        Task<PageList<Project>> GetAllProjectsAsync(PageParams pageParams, bool includeHumans = false);
        Task<Project> GetProjectByIdAsync(long projectId, bool includeHumans = false);
    }
}
