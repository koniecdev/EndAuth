namespace EndAuth.Shared.Roles.Commands.Create;
public record CreateRoleCommand(string Name) : IRequest<int>;