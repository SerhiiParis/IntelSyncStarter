using IntelSyncStarter.Models;
using IntelSyncStarter.Models.SyncJob;

namespace IntelSyncStarter;

public static class Consts
{
    public static List<SyncJob> Jobs =>
    [
        new()
        {
            ObjectJobType = SyncJobType.Contact,
            Payload = "{\"FirstName\":\"John\",\"LastName\":\"Doe\"}",
            SyncTime = DateTime.UtcNow,
            SyncJobStatus = SyncJobStatus.Pending,
            User = new CrmUser
            {
                Id = 1,
                Email = "johndoe@domain.com",
                Platform = "Salesforce",
                Token = "token123"
            }
        },
        new()
        {
            ObjectJobType = SyncJobType.Meeting,
            Payload = "{\"Subject\":\"Weekly Sync\",\"Location\":\"Zoom\"}",
            SyncTime = DateTime.UtcNow,
            SyncJobStatus = SyncJobStatus.Pending,
            User = new CrmUser
            {
                Id = 2,
                Email = "janedoe@domain.com",
                Platform = "Dynamics365",
                Token = "token456"
            }
        },
        new()
        {
            ObjectJobType = SyncJobType.Contact,
            Payload = "{\"FirstName\":\"Alice\",\"LastName\":\"Smith\"}",
            SyncTime = DateTime.UtcNow,
            SyncJobStatus = SyncJobStatus.Pending,
            User = new CrmUser
            {
                Id = 3,
                Email = "alicesmith@domain.com",
                Platform = "HubSpot",
                Token = "token789"
            }
        }
    ];
}