using IntelSyncStarter.Models.SyncJob;

namespace IntelSyncStarter.Services;

public interface IBatchSyncProcessor
{
    Task ProcessAllAsync(IEnumerable<SyncJob> jobs);
}