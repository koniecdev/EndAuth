namespace EndAuth.Shared.Identities.Commands.Register;
public record RegisterUserCommand(string Email, string Username, string Password) : IRequest<Result<string>>;