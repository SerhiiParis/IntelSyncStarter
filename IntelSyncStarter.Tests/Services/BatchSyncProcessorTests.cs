using IntelSyncStarter.Models;
using IntelSyncStarter.Models.SyncJob;
using IntelSyncStarter.Services;
using IntelSyncStarter.Services.Validators;
using Microsoft.Extensions.Logging;
using Moq;

namespace IntelSyncStarter.Tests.Services
{
    [TestFixture]
    public class BatchSyncProcessorTests
    {
        private Mock<ISyncValidator> _validatorMock;
        private Mock<ILogger<BatchSyncProcessor>> _loggerMock;
        private BatchSyncProcessor _processor;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<ISyncValidator>();
            _loggerMock = new Mock<ILogger<BatchSyncProcessor>>();
            _processor = new BatchSyncProcessor(_validatorMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task ProcessAllAsync_WithValidJob_SetsStatusToSuccess()
        {
            // Arrange
            var job = GetJob();
            _validatorMock
                .Setup(v => v.Validate(It.IsAny<SyncJob>()))
                .Returns(new ValidationResult());

            // Act
            await _processor.ProcessAllAsync([job]);

            // Assert
            Assert.That(job.SyncJobStatus, Is.EqualTo(SyncJobStatus.Success));
            Assert.That(job.ErrorMessage, Is.Null);
        }

        [Test]
        public async Task ProcessAllAsync_WithInvalidJob_SetsStatusToFailed()
        {
            // Arrange
            const string errorMessage = "Invalid job";
            var job = GetJob();

            _validatorMock
                .Setup(v => v.Validate(It.IsAny<SyncJob>()))
                .Returns(new ValidationResult(false, errorMessage));

            // Act
            await _processor.ProcessAllAsync([job]);

            // Assert
            Assert.That(job.SyncJobStatus, Is.EqualTo(SyncJobStatus.Failed));
            Assert.That(job.ErrorMessage, Is.EqualTo(errorMessage));
        }

        [Test]
        public async Task ProcessAllAsync_WithMultipleJobs_ProcessesAllJobs()
        {
            // Arrange
            var job1 = GetJob();
            var job2 = GetJob();

            _validatorMock
                .Setup(v => v.Validate(It.IsAny<SyncJob>()))
                .Returns(new ValidationResult());

            // Act
            await _processor.ProcessAllAsync([job1, job2]);

            // Assert
            Assert.That(job1.SyncJobStatus, Is.EqualTo(SyncJobStatus.Success));
            Assert.That(job2.SyncJobStatus, Is.EqualTo(SyncJobStatus.Success));
        }

        [Test]
        public async Task ProcessAllAsync_WithMixedValidationResults_ProcessesAccordingly()
        {
            // Arrange
            const string errorMessage = "Invalid job";
            var validJob = GetJob();
            var invalidJob = GetJob();

            _validatorMock.Setup(v => v.Validate(validJob))
                .Returns(new ValidationResult());
            _validatorMock.Setup(v => v.Validate(invalidJob))
                .Returns(new ValidationResult(false, errorMessage));

            // Act
            await _processor.ProcessAllAsync([validJob, invalidJob]);

            // Assert
            Assert.That(validJob.SyncJobStatus, Is.EqualTo(SyncJobStatus.Success));
            Assert.That(invalidJob.SyncJobStatus, Is.EqualTo(SyncJobStatus.Failed));
            Assert.That(invalidJob.ErrorMessage, Is.EqualTo(errorMessage));
        }

        private static SyncJob GetJob()
        {
            return new SyncJob
            {
                User = new CrmUser
                {
                    Id = 1,
                    Email = "test@example.com",
                    Token = "token123",
                    Platform = "Test"
                },
                ObjectJobType = SyncJobType.Contact,
                Payload = "{ \"test\": \"data\" }",
                SyncTime = DateTime.UtcNow,
                SyncJobStatus = SyncJobStatus.Pending
            };
        }
    }
}