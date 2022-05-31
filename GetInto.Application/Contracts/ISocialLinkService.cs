using GetInto.Application.Dtos;

namespace GetInto.Application.Contracts
{
    public interface ISocialLinkService
    {
        Task<SocialLinkDto[]> SaveByProject(long projectId, SocialLinkDto[] models);
        Task<bool> DeleteByProject(long projectId, long socialLinkId);

        Task<SocialLinkDto[]> SaveByHuman(long humanId, SocialLinkDto[] models);
        Task<bool> DeleteByHuman(long humanId, long socialLinkId);

        Task<SocialLinkDto[]> GetAllByProjectIdAsync(long projectId);
        Task<SocialLinkDto[]> GetAllByHumanIdAsync(long humanId);

        Task<SocialLinkDto> GetSocialLinkProjectByIdsAsync(long projectId, long socialLinkId);
        Task<SocialLinkDto> GetSocialLinkHumanByIdsAsync(long humanId, long socialLinkId);
    }
}
