using EndAuth.Shared.Interfaces.Markers;

namespace EndAuth.Shared.Roles.Commands.Create;
public record CreateRoleCommand(string Name) : IRequest<string>, IRoleManagerPossibleDuplicateRequest;