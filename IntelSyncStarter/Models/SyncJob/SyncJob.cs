namespace IntelSyncStarter.Models.SyncJob;

public class SyncJob
{
    public SyncJobType ObjectJobType { get; set; }
    public string Payload { get; set; }
    public DateTime SyncTime { get; set; }
    public string ErrorMessage { get; set; }
    public SyncJobStatus SyncJobStatus { get; set; }

    public CrmUser User { get; set; }
}
