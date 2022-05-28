using GetInto.Domain;

namespace GetInto.Persistence.Contracts
{
    public interface IJobPersist
    {
        Task<Job[]> GetJobsByProjectIdAsync(long projectId);
        Task<Job> GetJobByIdsAsync(long projectId, long id);
    }
}
