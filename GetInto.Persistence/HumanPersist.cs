using GetInto.Domain;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using GetInto.Persistence.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence
{
    public class HumanPersist : GeralPersist, IHumanPersist
    {
        private readonly GetIntoContext _context;
        public HumanPersist(GetIntoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageList<Human>> GetAllHumansAsync(PageParams pageParams, bool includeProjects)
        {
            IQueryable<Human> query = _context.Humans
                .Include(h => h.User)
                .Include(h => h.SocialLinks);

            if(includeProjects)
            {
                query = query
                    .Include(h => h.HumansProjects)
                    .ThenInclude(hp => hp.Project);
            }

            query = query.AsNoTracking()
                         .Where(h => h.MiniCurriculum.ToLower().Contains(pageParams.Term.ToLower()))
                         .OrderBy(h => h.Id);

            return await PageList<Human>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Human> GetHumanByUserIdAsync(long userId, bool includeProjects)
        {
            IQueryable<Human> query = _context.Humans
                .Include(h => h.User)
                .Include(h => h.SocialLinks);

            if (includeProjects)
            {
                query = query
                    .Include(h => h.HumansProjects)
                    .ThenInclude(hp => hp.Project);
            }

            query = query.AsNoTracking()
                         .Where(h => h.UserId == userId);

            return await query.FirstOrDefaultAsync();

        }
    }
}
