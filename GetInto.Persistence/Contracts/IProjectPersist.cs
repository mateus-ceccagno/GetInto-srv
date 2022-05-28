using GetInto.Domain;
using GetInto.Persistence.Pagination;

namespace GetInto.Persistence.Contracts
{
    public interface IProjectPersist
    {
        // TODO: Add bool param 'includeHuman' in both
        Task<PageList<Project>> GetAllProjectsAsync(PageParams pageParams);
        Task<Project> GetProjectByIdAsync(long projectId);
    }
}
