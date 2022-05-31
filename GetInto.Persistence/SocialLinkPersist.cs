using GetInto.Domain;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence
{
    public class SocialLinkPersist : GeralPersist, ISocialLinkPersist
    {
        private readonly GetIntoContext _context;
        public SocialLinkPersist(GetIntoContext context) : base(context)
        {
            _context = context;
        }
        public async Task<SocialLink[]> GetAllByHumanIdAsync(long humanId)
        {
            IQueryable<SocialLink> query = _context.SocialLinks;
            
            query = query.AsNoTracking().Where(sma => sma.HumanId == humanId);
            return await query.ToArrayAsync();
        }

        public async Task<SocialLink[]> GetAllByProjectIdAsync(long projectId)
        {
            IQueryable<SocialLink> query = _context.SocialLinks;

            query = query.AsNoTracking().Where(sma => sma.ProjectId == projectId);
            return await query.ToArrayAsync();
        }

        public async Task<SocialLink> GetSocialLinkHumanByIdsAsync(long humanId, long id)
        {
            IQueryable<SocialLink> query = _context.SocialLinks;

            query = query.AsNoTracking().Where(sma => sma.HumanId == humanId && sma.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<SocialLink> GetSocialLinkProjectByIdsAsync(long projectId, long id)
        {
            IQueryable<SocialLink> query = _context.SocialLinks;

            query = query.AsNoTracking().Where(sma => sma.ProjectId == projectId && sma.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
