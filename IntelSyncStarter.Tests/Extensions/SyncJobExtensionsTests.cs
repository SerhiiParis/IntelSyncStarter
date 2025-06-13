using IntelSyncStarter.Extensions;
using IntelSyncStarter.Models.SyncJob;

namespace IntelSyncStarter.Tests.Extensions
{
    [TestFixture]
    public class SyncJobExtensionsTests
    {
        [Test]
        public void Success_SetsStatusToSuccess()
        {
            // Arrange
            var job = new SyncJob { SyncJobStatus = SyncJobStatus.Pending };

            // Act
            job.Success();

            // Assert
            Assert.That(job.SyncJobStatus, Is.EqualTo(SyncJobStatus.Success));
        }

        [Test]
        public void Fail_SetsStatusToFailedAndErrorMessage()
        {
            // Arrange
            const string errorMessage = "Test error";
            var job = new SyncJob { SyncJobStatus = SyncJobStatus.Pending };

            // Act
            job.Fail(errorMessage);

            // Assert
            Assert.That(job.SyncJobStatus, Is.EqualTo(SyncJobStatus.Failed));
            Assert.That(job.ErrorMessage, Is.EqualTo(errorMessage));
        }
    }
}
