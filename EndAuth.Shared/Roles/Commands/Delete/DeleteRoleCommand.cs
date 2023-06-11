using EndAuth.Shared.Interfaces.Markers;

namespace EndAuth.Shared.Roles.Commands.Delete;
public record DeleteRoleCommand(string Id) : IRequest, IRoleManagerRequest;