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

        public ProjectService(IGeralPersist geralPersist, IProjectPersist projectPersist, IMapper mapper)
        {
            _geralPersist=geralPersist;
            _projectPersist=projectPersist;
            _mapper=mapper;
        }

        public async Task<ProjectDto> AddProject(long userId, ProjectDto model)
        {
            try
            {
                var project = _mapper.Map<Project>(model);
                project.UserId = userId;

                _projectPersist.Add(project);
                if(await _geralPersist.SaveChangesAsync())
                {
                    var projectReturn = await _projectPersist.GetProjectByIdAsync(project.Id, false);
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
                var project = await _projectPersist.GetProjectByIdAsync(projectId, false);

                if (project == null) throw new Exception("Project not found.");

                _geralPersist.Delete(project);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<ProjectDto>> GetAllProjectsAsync(PageParams pageParams, bool includeHumans = false)
        {
            try
            {
                var projects = await _projectPersist.GetAllProjectsAsync(pageParams, includeHumans);
                if (projects == null) return null;

                var result = _mapper.Map<PageList<ProjectDto>>(projects);
                
                result.CurrentPage = projects.CurrentPage;
                result.TotalPages = projects.TotalPages;
                result.PageSize = projects.PageSize;
                result.TotalCount = projects.TotalCount;
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<ProjectDto> GetProjectByIdAsync(long projectId, bool includeHumans = false)
        {
            try
            {
                var project = await _projectPersist.GetProjectByIdAsync(projectId, includeHumans);
                if (project == null) return null;

                var result = _mapper.Map<ProjectDto>(project);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProjectDto> UpdateProject(long userId, long projectId, ProjectDto model)
        {
            try
            {
                var project = await _projectPersist.GetProjectByIdAsync(projectId, false);
                if (project == null) return null;

                model.Id = project.Id;
                model.UserId = userId;

                _mapper.Map(model, project);

                _projectPersist.Update(project);
                if(await _projectPersist.SaveChangesAsync())
                {
                    var result = await _projectPersist.GetProjectByIdAsync(project.Id, false);
                    return _mapper.Map<ProjectDto>(result);
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
