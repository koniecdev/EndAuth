namespace EndAuth.Shared.Identities.Commands.Login;
public record LoginUserCommand(string Email, string Password) : IRequest<Result<string>>;