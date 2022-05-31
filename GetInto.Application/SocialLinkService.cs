using AutoMapper;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using GetInto.Domain;
using GetInto.Persistence.Contracts;

namespace GetInto.Application
{
    public class SocialLinkService : ISocialLinkService
    {
        private readonly ISocialLinkPersist _socialLinkPersist;
        private readonly IMapper _mapper;
        public SocialLinkService(ISocialLinkPersist socialLinkPersist,
                                     IMapper mapper)
        {
            _socialLinkPersist = socialLinkPersist;
            _mapper = mapper;
        }

        public async Task<bool> DeleteByHuman(long humanId, long socialLinkId)
        {
            try
            {
                var socialLink = await _socialLinkPersist.GetSocialLinkHumanByIdsAsync(humanId, socialLinkId);
                if (socialLink == null) throw new Exception("Social Link not found.");

                _socialLinkPersist.Delete(socialLink);
                return await _socialLinkPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> DeleteByProject(long projectId, long socialLinkId)
        {
            try
            {
                var socialLink = await _socialLinkPersist.GetSocialLinkProjectByIdsAsync(projectId, socialLinkId);
                if (socialLink == null) throw new Exception("Social Link not found.");

                _socialLinkPersist.Delete(socialLink);
                return await _socialLinkPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SocialLinkDto[]> GetAllByHumanIdAsync(long humanId)
        {
            try
            {
                var socialLinks = await _socialLinkPersist.GetAllByHumanIdAsync(humanId);
                if (socialLinks == null) return null;
                
                var result =  _mapper.Map<SocialLinkDto[]>(socialLinks);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SocialLinkDto[]> GetAllByProjectIdAsync(long projectId)
        {
            try
            {
                var socialLinks = await _socialLinkPersist.GetAllByProjectIdAsync(projectId);
                if (socialLinks == null) return null;

                var result = _mapper.Map<SocialLinkDto[]>(socialLinks);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SocialLinkDto> GetSocialLinkHumanByIdsAsync(long humanId, long socialLinkId)
        {
            try
            {
                var socialLink = await _socialLinkPersist.GetSocialLinkHumanByIdsAsync(humanId, socialLinkId);
                if (socialLink == null) return null;

                var result = _mapper.Map<SocialLinkDto>(socialLink);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SocialLinkDto> GetSocialLinkProjectByIdsAsync(long projectId, long socialLinkId)
        {
            try
            {
                var socialLink = await _socialLinkPersist.GetSocialLinkProjectByIdsAsync(projectId, socialLinkId);
                if (socialLink == null) return null;

                var result = _mapper.Map<SocialLinkDto>(socialLink);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SocialLinkDto[]> SaveByHuman(long humanId, SocialLinkDto[] models)
        {
            try
            { 
                var socialLinks = await _socialLinkPersist.GetAllByHumanIdAsync(humanId);
                if (socialLinks == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddSocialLink(humanId, model, false);
                    }
                    else
                    {
                        var socialLink = socialLinks.FirstOrDefault(sl => sl.Id == model.Id);
                        model.HumanId = humanId;

                        _mapper.Map(model, socialLink);
                        _socialLinkPersist.Update<SocialLink>(socialLink);

                        await _socialLinkPersist.SaveChangesAsync();
                    }
                }

                var result = await _socialLinkPersist.GetAllByHumanIdAsync(humanId);
                return _mapper.Map<SocialLinkDto[]>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SocialLinkDto[]> SaveByProject(long projectId, SocialLinkDto[] models)
        {
            try
            {
                var socialLinks = await _socialLinkPersist.GetAllByProjectIdAsync(projectId);
                if (socialLinks == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddSocialLink(projectId, model, true);
                    }
                    else
                    {
                        var socialLink = socialLinks.FirstOrDefault(sl => sl.Id == model.Id);
                        model.HumanId = projectId;

                        _mapper.Map(model, socialLink);
                        _socialLinkPersist.Update<SocialLink>(socialLink);

                        await _socialLinkPersist.SaveChangesAsync();
                    }
                }

                var result = await _socialLinkPersist.GetAllByHumanIdAsync(projectId);
                return _mapper.Map<SocialLinkDto[]>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Auxiliary method to save first project and human's social link.
        /// </summary>
        /// <param name="id">projectId or humanId</param>
        /// <param name="model"></param>
        /// <param name="isProject"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddSocialLink(long id, SocialLinkDto model, bool isProject)
        {
            try
            {
                var socialLink = _mapper.Map<SocialLink>(model);
                if(isProject)
                {
                    socialLink.ProjectId = id;
                    socialLink.HumanId = null;
                }
                else
                {
                    socialLink.HumanId = id;
                    socialLink.ProjectId = null;
                }
                _socialLinkPersist.Add(socialLink);
                await _socialLinkPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
