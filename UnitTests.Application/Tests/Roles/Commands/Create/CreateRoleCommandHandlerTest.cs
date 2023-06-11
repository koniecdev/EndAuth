using EndAuth.Application.Common.Exceptions;
using EndAuth.Application.Common.Interfaces.Factories;
using EndAuth.Application.Common.Interfaces;
using EndAuth.Application.Identities.Commands.ForgotPassword;
using EndAuth.Domain.Entities;
using EndAuth.Shared.Identities.Commands.ForgotPassword;
using EndAuth.Shared.Roles.Commands.Create;
using FluentAssertions;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace EndAuth.Application.Roles.Commands.Create;
public class CreateRoleCommandHandlerTest : CommandTestBase
{
    private readonly CreateRoleCommandHandler _handler;
    private readonly CreateRoleCommandValidator _validator;
    public CreateRoleCommandHandlerTest()
    {
        var fakeRoleManager = new FakeRoleManagerBuilder()
            .With(m=>m.Setup(x=>x.CreateAsync(It.IsAny<IdentityRole>())).ReturnsAsync(IdentityResult.Success))
            .With(m=>m.Setup(x=>x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new IdentityRole() { Id = "lolololol", Name = "CustomRole" }))
            .Build();
        _handler = new(fakeRoleManager.Object);
        _validator = new();
    }

    [Fact]
    public async Task ValidData_ShouldCreateRole()
    {
        CreateRoleCommand command = new("SimpleRole");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeNull();
        Exception ex1 = await Record.ExceptionAsync(() => _handler.Handle(command, CancellationToken.None));
        ex1.Should().BeNull();
    }

    [Fact]
    public Task InvalidData_ShouldBeValidationException()
    {
        CreateRoleCommand command = new(" ");
        Exception ex = Record.Exception(() => ValidateRequest(command));
        ex.Should().BeOfType<ValidationException>();
        return Task.CompletedTask;
    }

    private void ValidateRequest(CreateRoleCommand command)
    {
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}