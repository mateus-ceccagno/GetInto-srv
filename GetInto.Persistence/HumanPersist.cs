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

        public async Task<PageList<Human>> GetAllHumansAsync(PageParams pageParams)
        {
            IQueryable<Human> query = _context.Humans
                .Include(h => h.SocialLinks);

            query = query.AsNoTracking()
                         .Where(h => h.MiniCurriculum.ToLower().Contains(pageParams.Term.ToLower()))
                         .OrderBy(h => h.Id);

            return await PageList<Human>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Human> GetHumanByUserIdAsync(long id)
        {
            IQueryable<Human> query = _context.Humans;

            query = query.AsNoTracking()
                         .Where(h => h.Id == id);

            return await query.FirstOrDefaultAsync();

        }
    }
}
