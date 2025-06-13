using IntelSyncStarter.Services.Validators;

namespace IntelSyncStarter.Tests.Services.Validators
{
    [TestFixture]
    public class ValidationResultTests
    {
        [Test]
        public void Constructor_DefaultParameters_CreatesValidResult()
        {
            // Act
            var result = new ValidationResult();
            
            // Assert
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorMessage, Is.Null);
        }
        
        [Test]
        public void Constructor_WithInvalidStatus_CreatesInvalidResult()
        {
            // Arrange
            const string errorMessage = "Test error";
            
            // Act
            var result = new ValidationResult(false, errorMessage);
            
            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(errorMessage));
        }
        
        [Test]
        public void Constructor_WithValidStatusAndErrorMessage_CreatesValidResultWithErrorMessage()
        {
            // Arrange
            const string errorMessage = "This won't be used";
            
            // Act
            var result = new ValidationResult(true, errorMessage);
            
            // Assert
            Assert.That(result.IsValid, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(errorMessage));
        }
        
        [Test]
        public void Equality_SameValues_ReturnsTrue()
        {
            // Arrange
            var result1 = new ValidationResult(false, "error");
            var result2 = new ValidationResult(false, "error");
            
            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }
        
        [Test]
        public void Equality_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var result1 = new ValidationResult(false, "error");
            var result2 = new ValidationResult(false, "different error");
            
            // Assert
            Assert.That(result1, Is.Not.EqualTo(result2));
        }
    }
}
