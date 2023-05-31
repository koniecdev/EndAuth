namespace EndAuth.Shared.Identities.Commands.Login;
public record LoginUserCommand(string Username, string Password) : IRequest<Result<string>>;