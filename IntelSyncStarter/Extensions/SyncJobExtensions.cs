using IntelSyncStarter.Models.SyncJob;

namespace IntelSyncStarter.Extensions;

public static class SyncJobExtensions
{
    public static void Success(this SyncJob job) => job.SyncJobStatus = SyncJobStatus.Success;

    public static void Fail(this SyncJob job, string errorMessage)
    {
        job.SyncJobStatus = SyncJobStatus.Failed;
        job.ErrorMessage = errorMessage;
    }
}