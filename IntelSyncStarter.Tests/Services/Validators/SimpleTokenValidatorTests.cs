using IntelSyncStarter.Models;
using IntelSyncStarter.Models.SyncJob;
using IntelSyncStarter.Services.Validators;

namespace IntelSyncStarter.Tests.Services.Validators
{
    [TestFixture]
    public class SimpleTokenValidatorTests
    {
        private SimpleTokenValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new SimpleTokenValidator();
        }

        [Test]
        public void Validate_WithValidJob_ReturnsValidResult()
        {
            // Arrange
            var job = new SyncJob
            {
                User = new CrmUser
                {
                    Id = 1,
                    Email = "test@example.com",
                    Token = "token123",
                    Platform = "Test"
                }
            };

            // Act
            var result = _validator.Validate(job);

            // Assert
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
        }

        [Test]
        public void Validate_WithNullUser_ReturnsInvalidResult()
        {
            // Arrange
            var job = new SyncJob
            {
                User = null
            };

            // Act
            var result = _validator.Validate(job);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Missing CRM user."));
        }

        [Test]
        public void Validate_WithNullToken_ReturnsInvalidResult()
        {
            // Arrange
            var job = new SyncJob
            {
                User = new CrmUser
                {
                    Id = 1,
                    Email = "test@example.com",
                    Token = null,
                    Platform = "Test"
                }
            };

            // Act
            var result = _validator.Validate(job);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Missing CRM token."));
        }

        [Test]
        public void Validate_WithEmptyToken_ReturnsInvalidResult()
        {
            // Arrange
            var job = new SyncJob
            {
                User = new CrmUser
                {
                    Id = 1,
                    Email = "test@example.com",
                    Token = "",
                    Platform = "Test"
                }
            };

            // Act
            var result = _validator.Validate(job);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Missing CRM token."));
        }

        [Test]
        public void Validate_WithWhitespaceToken_ReturnsInvalidResult()
        {
            // Arrange
            var job = new SyncJob
            {
                User = new CrmUser
                {
                    Id = 1,
                    Email = "test@example.com",
                    Token = "   ",
                    Platform = "Test"
                }
            };

            // Act
            var result = _validator.Validate(job);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Missing CRM token."));
        }
    }
}
