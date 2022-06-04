using GetInto.Application.Contracts;
using GetInto.Persistence.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GetInto.API.Extensions;
using GetInto.Application.Dtos;

namespace GetInto.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService=projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync(pageParams, true);
                if (projects == null) return NoContent();

                Response.AddPagination(projects.CurrentPage, projects.PageSize, projects.TotalCount, projects.TotalPages);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover projects. Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id, true);
                if (project == null) return NoContent();

                return Ok(project);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover project. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProjectDto model)
        {
            try
            {
                var project = await _projectService.AddProject(User.GetUserId(), model);
                if (project == null) return NoContent();

                return Ok(project);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to add project. Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(long id, ProjectDto model)
        {
            try
            {
                var project = await _projectService.UpdateProject(User.GetUserId(), id, model);
                if (project == null) return NoContent();

                return Ok(project);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to update project. Error: {ex.Message}");
            }
        }
        
         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                if (project == null) return NoContent();

                return (await _projectService.DeleteProject(id))
                    ? Ok(new { message = "Deleted Project" })
                    : throw new Exception("An unspecific problem occurred when trying to delete Project.");

                return Ok(project);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover project. Error: {ex.Message}");
            }
        }
    }
}
