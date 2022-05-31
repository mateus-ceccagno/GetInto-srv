using GetInto.Domain;

namespace GetInto.Persistence.Contracts
{
    public interface ISocialLinkPersist : IGeralPersist
    {
        Task<SocialLink> GetSocialLinkProjectByIdsAsync(long projectId, long id);
        Task<SocialLink> GetSocialLinkHumanByIdsAsync(long humanId, long id);
        Task<SocialLink[]> GetAllByProjectIdAsync(long projectId);
        Task<SocialLink[]> GetAllByHumanIdAsync(long humanId);
    }
}
