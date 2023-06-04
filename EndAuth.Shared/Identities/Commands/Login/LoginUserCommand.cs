using EndAuth.Shared.Dtos;

namespace EndAuth.Shared.Identities.Commands.Login;
public record LoginUserCommand(string Email, string Password) : IRequest<AuthSuccessResponse>;