using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GetInto.API.Controllers
{
    [ApiController]
    [Route("api/social-links")]
    public class SocialLinkController : ControllerBase
    {
        private readonly ISocialLinkService _socialLinkService;
        private readonly IProjectService _projectService;
        private readonly IHumanService _humanService;

        public SocialLinkController(ISocialLinkService socialLinkService,
                                    IProjectService projectService,
                                    IHumanService humanService)
        {
            _socialLinkService=socialLinkService;
            _projectService=projectService;
            _humanService=humanService;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(long projectId)
        {
            try
            {
                var socialLinks = await _socialLinkService.GetAllByProjectIdAsync(projectId);
                if(socialLinks == null) return NoContent();
                
                return Ok(socialLinks);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover Social Links by Project. Error: {ex.Message}");
            }
        }

        [HttpGet("human/{humanId}")]
        public async Task<IActionResult> GetByHuman(long humanId)
        {
            try
            {
                var socialLinks = await _socialLinkService.GetAllByHumanIdAsync(humanId);
                if (socialLinks == null) return NoContent();

                return Ok(socialLinks);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover Social Links by Human. Error: {ex.Message}");
            }
        }

        [HttpPut("project/{projectId}")]
        public async Task<IActionResult> SaveByProject(long projectId, SocialLinkDto[] models)
        {
            try
            {
                var socialLinks = await _socialLinkService.SaveByProject(projectId, models);
                if (socialLinks == null) return NoContent();

                return Ok(socialLinks);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to save Social Links by Project. Error: {ex.Message}");
            }
        }

        [HttpPut("human/{humanId}")]
        public async Task<IActionResult> SaveByHuman(long humanId, SocialLinkDto[] models)
        {
            try
            {
                var socialLinks = await _socialLinkService.SaveByHuman(humanId, models);
                if (socialLinks == null) return NoContent();

                return Ok(socialLinks);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to save Social Links by Human. Error: {ex.Message}");
            }
        }

        [HttpDelete("project/{projectId}/{socialLinkId}")]
        public async Task<IActionResult> DeleteByProject(long projectId, long socialLinkId)
        {
            try
            {
                var socialLinks = await _socialLinkService.GetSocialLinkProjectByIdsAsync(projectId, socialLinkId);
                if (socialLinks == null) return NoContent();

                return await _socialLinkService.DeleteByProject(projectId, socialLinkId)
                    ? Ok(new { message = "Deleted Social Link"})
                    : throw new Exception("An unspecific problem occurred when trying to delete Social Link by Project.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to delete Social Links by Project. Error: {ex.Message}");
            }
        }

        [HttpDelete("human/{humanId}/{socialLinkId}")]
        public async Task<IActionResult> DeleteByHuman(long humanId, long socialLinkId)
        {
            try
            {
                var socialLinks = await _socialLinkService.GetSocialLinkHumanByIdsAsync(humanId, socialLinkId);
                if (socialLinks == null) return NoContent();

                return await _socialLinkService.DeleteByHuman(humanId, socialLinkId)
                    ? Ok(new { message = "Deleted Social Link" })
                    : throw new Exception("An unspecific problem occurred when trying to delete Social Link by Human.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to delete Social Links by Human. Error: {ex.Message}");
            }
        }

    }
}
