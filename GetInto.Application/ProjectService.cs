using AutoMapper;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using GetInto.Domain;
using GetInto.Persistence.Contracts;
using GetInto.Persistence.Pagination;

namespace GetInto.Application
{
    public class ProjectService : IProjectService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IProjectPersist _projectPersist;
        private readonly IMapper _mapper;
        public async Task<ProjectDto> AddProject(ProjectDto model)
        {
            try
            {
                var project = _mapper.Map<Project>(model);
                
                _geralPersist.Add(project);
                if(await _geralPersist.SaveChangesAsync())
                {
                    var projectReturn = await _projectPersist.GetProjectByIdAsync(project.Id);
                    return _mapper.Map<ProjectDto>(projectReturn);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProject(long projectId)
        {
            try
            {
                var project = await _projectPersist.GetProjectByIdAsync(projectId);

                if (project == null) throw new Exception("Project not found.");

                _geralPersist.Delete(project);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProjectDto>> GetAllProjectsAsync(PageParams pageParams)
        {
            try
            {
                var projects = _projectPersist.GetAllProjectsAsync(pageParams);
                if (projects == null) return null;

                var result = _mapper.Map<PageList<ProjectDto>>(projects);

                result.CurrentPage = projects.Result.CurrentPage;
                result.TotalPages = projects.Result.TotalPages;
                result.PageSize = projects.Result.PageSize;
                result.TotalCount = projects.Result.TotalCount;
                
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public async Task<ProjectDto> GetProjectByIdAsync(long projectId)
        {
            try
            {
                var project = await _projectPersist.GetProjectByIdAsync(projectId);
                if (project == null) return null;

                var result = _mapper.Map<ProjectDto>(project);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ProjectDto> UpdateProject(long projectId, ProjectDto model)
        {
            throw new NotImplementedException();
        }
    }
}
