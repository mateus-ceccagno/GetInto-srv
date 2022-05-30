using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GetInto.API.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService=jobService;
        }

        [HttpGet("{jobId}")]
        public async Task<IActionResult> Get(long jobId)
        {
            try
            {
                var jobs = await _jobService.GetJobsByProjectIdAsync(jobId);
                if (jobs == null) return NoContent();

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover job. Error: {ex.Message}");
            }
        }

        [HttpDelete("{projectId}/{jobId}")]
        public async Task<IActionResult> Delete(long projectId, long jobId)
        {
            try
            {
                var job = await _jobService.GetJobByIdsAsync(projectId, jobId);
                if (job == null) return NoContent();

                return (await _jobService.DeleteJob(projectId, jobId))
                    ? Ok(new { message = "Deleted Job" })
                    : throw new Exception("An unspecific problem occurred when trying to delete Job.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to delete job. Error: {ex.Message}");
            }
        }

        [HttpPut("{jobId}")]
        public async Task<IActionResult> Put(long jobId, JobDto[] models)
        {
            try
            {
                var jobs = await _jobService.SaveJobs(jobId, models);
                if (jobs == null) return NoContent();

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error when trying to recover job. Error: {ex.Message}");
            }
        }
    }
}
