using IntelSyncStarter.Models.SyncJob;

namespace IntelSyncStarter.Services.Validators
{
    public class SimpleTokenValidator : ISyncValidator
    {
        public ValidationResult Validate(SyncJob job)
        {
            if (job.User == null)
            {
                return new ValidationResult(false, "Missing CRM user.");
            }

            if (string.IsNullOrWhiteSpace(job.User.Token))
            {
                return new ValidationResult(false, "Missing CRM token.");
            }

            return new ValidationResult();
        }
    }
}