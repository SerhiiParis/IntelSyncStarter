using IntelSyncStarter.Models.SyncJob;

namespace IntelSyncStarter.Services.Validators;

public interface ISyncValidator
{
    ValidationResult Validate(SyncJob job);
}