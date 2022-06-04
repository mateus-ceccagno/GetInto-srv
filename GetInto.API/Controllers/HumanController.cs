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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAccountService _accountService;

        public HumanController(IHumanService humanService,
                               IWebHostEnvironment webHostEnvironment,
                               IAccountService accountService)
        {   
            _humanService=humanService;
            _webHostEnvironment=webHostEnvironment;
            _accountService=accountService;
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

        [HttpPost]
        public async Task<IActionResult> Post(HumanAddDto model)
        {
            try
            {
                //  'User' comes from ControllerBase
                //  'GetUserId()' comes from the created class: ClaimsPrincipalExtensions
                var human = await _humanService.GetHumanByUserIdAsync(User.GetUserId(), true);
                
                if (human == null)
                    human = await _humanService.AddHuman(User.GetUserId(), model);
                
                return Ok(human);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to add applicant. Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(HumanUpdateDto model)
        {
            try
            {
                var human = await _humanService.UpdateHuman(User.GetUserId(), model);
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
