using AutoMapper;
using GetInto.Application.Contracts;
using GetInto.Application.Dtos;
using GetInto.Domain;
using GetInto.Persistence.Contracts;

namespace GetInto.Application
{
    public class JobService : IJobService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IJobPersist _jobPersist;
        private readonly IMapper _mapper;
        public JobService(IJobPersist jobPersist, IMapper mapper, IGeralPersist geralPersist)
        {
            _jobPersist = jobPersist;
            _mapper = mapper;
            _geralPersist = geralPersist;
        }

        public async Task AddJob(long projectId, JobDto model)
        {
            try
            {
                var job = _mapper.Map<Job>(model);
                job.ProjectId = projectId;

                _geralPersist.Add<Job>(job);

                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<JobDto[]> SaveJobs(long projectId, JobDto[] models)
        {
            try
            {
                var jobs = await _jobPersist.GetJobsByProjectIdAsync(projectId);
                if (jobs == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddJob(projectId, model);
                    }
                    else
                    {
                        var job = jobs.FirstOrDefault(job => job.Id == projectId);
                        model.ProjectId = projectId;
                        _mapper.Map(model, job);
                        _geralPersist.Update(job);
                        await _geralPersist.SaveChangesAsync();
                    }
                }
                var jobReturn = await _jobPersist.GetJobsByProjectIdAsync(projectId);
                return _mapper.Map<JobDto[]>(jobReturn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public async Task<bool> DeleteJob(long projectId, long jobId)
        {
            try
            {
                var job = await _jobPersist.GetJobByIdsAsync(projectId, jobId);
                if (job == null) throw new Exception("Job not found.");

                _geralPersist.Delete(job);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<JobDto> GetJobByIdsAsync(long projectId, long jobId)
        {
            try
            {
                var job = await _jobPersist.GetJobByIdsAsync(projectId, jobId);
                if (job == null) return null;

                var result = _mapper.Map<JobDto>(job);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<JobDto[]> GetJobsByProjectIdAsync(long projectId)
        {
            try
            {
                var jobs = await _jobPersist.GetJobsByProjectIdAsync(projectId);
                if (jobs == null) return null;

                var result = _mapper.Map<JobDto[]>(jobs);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
