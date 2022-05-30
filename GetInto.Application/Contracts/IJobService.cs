using GetInto.Application.Dtos;

namespace GetInto.Application.Contracts
{
    public interface IJobService
    {
        Task<JobDto[]> SaveJobs(long projectId, JobDto[] models);
        Task<bool> DeleteJob(long projectId, long jobId);

        Task<JobDto[]> GetJobsByProjectIdAsync(long projectId);
        Task<JobDto> GetJobByIdsAsync(long projectId, long jobId);
    }
}
