using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Identities.Commands.ForgotPassword;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Roles.Commands.Delete;
using FluentAssertions;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Mail;
using UnitTests.Application.Common;
using EndAuth.Shared.Roles.Commands.Create;

namespace EndAuth.Application.Roles.Commands.Delete;
public class DeleteRoleCommandHandlerTest : CommandTestBase
{
    private readonly DeleteRoleCommandHandler _handler;
    private readonly DeleteRoleCommandValidator _validator;
    public DeleteRoleCommandHandlerTest()
    {
        var fakeRoleManager = new FakeRoleManagerBuilder().Build();
        _handler = new(fakeRoleManager.Object);
        _validator = new();
    }
    [Fact]
    public async Task ValidData_ShouldDeleteRole()
    {
        DeleteRoleCommand command = new("SimpleRole");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeNull();
        Exception ex1 = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex.Should().BeNull();
    }

    private void ValidateRequest(DeleteRoleCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}