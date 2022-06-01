using GetInto.API.Extensions;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using GetInto.Persistence.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace GetInto.API.Controllers
{
    [ApiController]
    [Route("api/applicants")]
    public class HumanController : ControllerBase
    {
        private readonly IHumanService _humanService;

        public HumanController(IHumanService humanService)
        {
            _humanService=humanService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams)
        {
            try
            {
                var projects = await _humanService.GetAllHumansAsync(pageParams, true);
                if (projects == null) return NoContent();

                Response.AddPagination(projects.CurrentPage, projects.PageSize, projects.TotalCount, projects.TotalPages);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover applicants. Error: {ex.Message}");
            }
        }

        // TODO: Get User Applicant, when User ok

        [HttpPost]
        public async Task<IActionResult> Post(HumanAddDto model)
        {
            try
            {
                var human = await _humanService.AddHuman(model);
                if (human == null) return NoContent();

                return Ok(human);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to add applicant. Error: {ex.Message}");
            }
        }

        [HttpPut("{applicantId}")]
        public async Task<IActionResult> Put(long applicantdId, HumanUpdateDto model)
        {
            try
            {
                var human = await _humanService.UpdateHuman(applicantdId, model);
                if (human == null) return NoContent();

                return Ok(human);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to update applicant. Error: {ex.Message}");
            }
        }

    }
}
