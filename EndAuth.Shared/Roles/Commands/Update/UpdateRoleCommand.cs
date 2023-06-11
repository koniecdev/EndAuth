using EndAuth.Shared.Interfaces.Markers;

namespace EndAuth.Shared.Roles.Commands.Update;
public record UpdateRoleCommand(string Id, string Name) : IRequest, IRoleManagerRequest, IRoleManagerPossibleDuplicateRequest;