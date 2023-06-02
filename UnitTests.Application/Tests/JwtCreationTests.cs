using Castle.Core.Configuration;
using EndAuth.Application.Identities.Commands.Login;
using EndAuth.Domain.Entities;
using EndAuth.JwtProvider;
using EndAuth.JwtProvider.Services;
using EndAuth.Shared.Identities.Commands.Login;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Claims;
using UnitTests.Application.Common;

namespace UnitTests.Application.Tests;
public class JwtCreationTests : CommandTestBase
{
    private readonly JwtService<ApplicationUser> _service;
    public JwtCreationTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"JwtSettings:Key", "ForTheLoveOfGodStoreAndLoadThisSecurely"},
            {"JwtSettings:Issuer", "https://localhost:7207"},
            {"JwtSettings:Audience", "https://localhost:7171"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var accessorMock = new Mock<IHttpContextAccessor>();
        var claimsMock = new Mock<ClaimsPrincipal>();

        var emailClaim = new Claim(ClaimTypes.Email, "Default@example.com");
        claimsMock.Setup(c => c.Claims).Returns(new[] { emailClaim });
        accessorMock.Setup(h => h.HttpContext!.User).Returns(claimsMock.Object);

        accessorMock.Setup(m=>m.HttpContext!.User.HasClaim(It.IsAny<Predicate<Claim>>())).Returns(true);

        var appUser = new ApplicationUser
        {
            UserName = "Default",
            NormalizedUserName = "DEFAULT",
            Email = "Default@example.com",
            NormalizedEmail = "DEFAULT@EXAMPLE.COM"
        };
        
        var fakeUserManager = new FakeUserManagerBuilder()
        .With(m => m.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
        .ReturnsAsync(IdentityResult.Success))
        .With(m => m.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
        .ReturnsAsync(appUser))
        .With(m => m.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
        .ReturnsAsync(new List<string>()))
        .Build();
        var fakeSignInManager = new FakeSignInManagerBuilder()
        .With(x => x.Setup(sm => sm.PasswordSignInAsync(It.IsAny<ApplicationUser>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
        .ReturnsAsync(SignInResult.Failed))
        .With(x => x.Setup(sm => sm.PasswordSignInAsync(
                It.Is<ApplicationUser>(m=>m.Equals(appUser)),
                It.Is<string>(xd => xd == "Default123$"),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
        .ReturnsAsync(SignInResult.Success))
        .Build();
        _service = new(fakeUserManager.Object, configuration, accessorMock.Object, new TokenParametersFactory(configuration), _context);
    }

    [Fact]
    public async Task ShouldReturnJWT()
    {
        var result = await _service.CreateTokenAsync("default@koniec.dev");
        result.Length.Should().BeGreaterThan(1);
    }
}