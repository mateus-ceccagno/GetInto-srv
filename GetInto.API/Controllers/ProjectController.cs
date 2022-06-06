using GetInto.API.Helpers;
using GetInto.Application.Contracts;
using GetInto.Persistence.Pagination;
using Microsoft.AspNetCore.Mvc;
using GetInto.API.Extensions;
using GetInto.Application.Dtos;

namespace GetInto.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUtilImage _utilImage;
        private readonly string _address = "Assets";
        public ProjectController(IProjectService projectService, IUtilImage utilImage)
        {
            _projectService=projectService;
            _utilImage = utilImage;
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

                if (await _projectService.DeleteProject(id))
                {
                    _utilImage.DeleteImage(project.ImageURL, _address);
                    return Ok(new { message = "Deleted Project" });
                }
                else
                {
                    throw new Exception("An unspecific problem occurred when trying to delete Project.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover project. Error: {ex.Message}");
            }
        }


        [HttpPost("upload-image/{id}")]
        public async Task<IActionResult> UploadImage(int id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id, true);
                if (project == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _utilImage.DeleteImage(project.ImageURL, _address);
                    project.ImageURL = await _utilImage.SaveImage(file, _address);
                }
                var result = await _projectService.UpdateProject(User.GetUserId(), id, project);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to upload Project Image. Error: {ex.Message}");
            }
        }
    }
}
