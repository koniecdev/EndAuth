namespace EndAuth.Shared.Identities.Commands.ResetPassword;
public record ResetPasswordCommand(string NewPassword, string ResetToken) : IRequest;