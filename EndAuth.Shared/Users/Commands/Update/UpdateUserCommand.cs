namespace EndAuth.Shared.Users.Commands.Update;
public record UpdateUserCommand(string Id, string Username) : IRequest;