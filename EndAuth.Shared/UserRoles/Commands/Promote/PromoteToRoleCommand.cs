namespace EndAuth.Shared.Users.Commands.Update;
public record PromoteToRoleCommand(string UserId, string RoleId) : IRequest;