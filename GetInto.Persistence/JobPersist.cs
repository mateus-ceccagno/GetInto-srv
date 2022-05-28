using GetInto.Domain;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence
{
    public class JobPersist : IJobPersist
    {
        private readonly GetIntoContext _context;
        public JobPersist(GetIntoContext context)
        {
            _context = context;
        }
        public async Task<Job> GetJobByIdsAsync(long projectId, long id)
        {
            IQueryable<Job> query = _context.Jobs;

            query = query.AsNoTracking()
                         .Where(job => job.ProjectId == projectId && job.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Job[]> GetJobsByProjectIdAsync(long projectId)
        {
            IQueryable<Job> query = _context.Jobs;

            query = query.AsNoTracking()
                         .Where(job => job.ProjectId == projectId);

            return await query.ToArrayAsync();
        }
    }
}
