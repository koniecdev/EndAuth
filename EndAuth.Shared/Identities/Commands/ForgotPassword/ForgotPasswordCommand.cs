namespace EndAuth.Shared.Identities.Commands.ForgotPassword;
public record ForgotPasswordCommand(string Email) : IRequest;