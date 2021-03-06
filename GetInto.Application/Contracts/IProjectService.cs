using GetInto.Application.Dtos;
using GetInto.Persistence.Pagination;

namespace GetInto.Application.Contracts
{
    public interface IProjectService
    {
        Task<ProjectDto> AddProject(long userId, ProjectDto model);
        Task<ProjectDto> UpdateProject(long userId, long projectId, ProjectDto model);
        Task<bool> DeleteProject(long projectId);

        Task<PageList<ProjectDto>> GetAllProjectsAsync(PageParams pageParams, bool includeHumans = false);
        Task<ProjectDto> GetProjectByIdAsync(long projectId, bool includeHumans = false);
    }
}
