using EndAuth.Shared.Dtos.Identites;

namespace EndAuth.Shared.Identities.Commands.Login;
public record LoginUserCommand(string Email, string Password) : IRequest<AuthSuccessResponse>;