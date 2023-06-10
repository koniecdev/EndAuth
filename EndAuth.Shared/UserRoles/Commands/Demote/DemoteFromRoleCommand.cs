namespace EndAuth.Shared.Users.Commands.Delete;
public record DemoteFromRoleCommand(string UserId, string RoleId) : IRequest;