namespace EndAuth.Shared.Identities.Commands.ResetPassword;
public record ResetPasswordCommand(string Email, string NewPassword, string ResetToken) : IRequest;