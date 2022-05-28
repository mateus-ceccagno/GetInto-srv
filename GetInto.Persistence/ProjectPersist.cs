using GetInto.Domain;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using GetInto.Persistence.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence
{
    public class ProjectPersist : IProjectPersist
    {
        private readonly GetIntoContext _context;
        public ProjectPersist(GetIntoContext context)
        {
            _context = context;
        }
        public async Task<PageList<Project>> GetAllProjectsAsync(PageParams pageParams)
        {
            IQueryable<Project> query = _context.Projects
                .Include(e => e.Jobs);

            query = query.AsNoTracking()
                         .Where(p => p.Title.ToLower().Contains(pageParams.Term.ToLower()) ||
                                     p.Location.ToLower().Contains(pageParams.Term.ToLower()))                                    
                         .OrderBy(p => p.Id);

            return await PageList<Project>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Project> GetProjectByIdAsync(long projectId)
        {
            IQueryable<Project> query = _context.Projects
                .Include(e => e.Jobs);

            query = query.AsNoTracking().OrderBy(p => p.Id)
                         .Where(p => p.Id == projectId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
