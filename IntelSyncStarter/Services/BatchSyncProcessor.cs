using System.Text.Json;
using IntelSyncStarter.Extensions;
using IntelSyncStarter.Models.SyncJob;
using IntelSyncStarter.Services.Validators;
using Microsoft.Extensions.Logging;

namespace IntelSyncStarter.Services
{
    public class BatchSyncProcessor(
        ISyncValidator validator,
        ILogger<BatchSyncProcessor> logger)
        : IBatchSyncProcessor
    {
        public async Task ProcessAllAsync(IEnumerable<SyncJob> jobs)
            => await Task.WhenAll(jobs.Select(ProcessJobAsync));

        private async Task ProcessJobAsync(SyncJob job)
        {
            var validationResult = validator.Validate(job);
            if (!validationResult.IsValid)
            {
                job.Fail(validationResult.ErrorMessage);
                logger.LogError("Job {Type} failed validation. User: {User}", job.SyncJobStatus, job.ErrorMessage);
                return;
            }

            try
            {
                // Processing
                await Task.Delay(1000);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing job: {Job}", JsonSerializer.Serialize(job));
                job.Fail($"Job: {job.ObjectJobType} for User: {job.User!.Email} failed. Error: {e.Message}");
            }

            job.Success();

            logger.LogInformation(
                "Job {Type} is processed. User: {User}",
                job.ObjectJobType,
                job.User?.Email);
        }
    }
}