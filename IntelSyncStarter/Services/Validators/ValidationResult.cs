namespace IntelSyncStarter.Services.Validators;

public record ValidationResult(bool IsValid = true, string ErrorMessage = null);