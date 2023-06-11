using EndAuth.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace UnitTests.Application.Common;

public class FakeUserManager : UserManager<ApplicationUser>
{
    public FakeUserManager()
        : base(new Mock<IUserStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            Array.Empty<IUserValidator<ApplicationUser>>(),
            Array.Empty<IPasswordValidator<ApplicationUser>>(),
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
    {
    }
}

public class FakeSignInManager : SignInManager<ApplicationUser>
{
    public FakeSignInManager()
        : base(
            new Mock<FakeUserManager>().Object,
            new HttpContextAccessor(),
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<ApplicationUser>>().Object)
    { }
}

public class FakeRoleManager : RoleManager<IdentityRole>
{
    public FakeRoleManager()
        :base(
                new Mock<IRoleStore<IdentityRole>>().Object,
                Array.Empty<IRoleValidator<IdentityRole>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object)
    { }
}
public class FakeRoleManagerBuilder
{
    private readonly Mock<FakeRoleManager> _mock = new();

    public FakeRoleManagerBuilder With(Action<Mock<FakeRoleManager>> mock)
    {
        var list = new List<IdentityRole>()
        {
            new IdentityRole("SuperAdmin"),
            new IdentityRole("Admin"),
            new IdentityRole("Moderator")
        }
        .AsQueryable();
        _mock.Setup(r => r.Roles).Returns(list);
        mock(_mock);
        return this;
    }

    public Mock<FakeRoleManager> Build()
    {
        return _mock;
    }
}

public class FakeUserManagerBuilder
{
    private readonly Mock<FakeUserManager> _mock = new();

    public FakeUserManagerBuilder With(Action<Mock<FakeUserManager>> mock)
    {
        mock(_mock);
        return this;
    }

    public Mock<FakeUserManager> Build()
    {
        return _mock;
    }
}

public class FakeSignInManagerBuilder
{
    private readonly Mock<FakeSignInManager> _mock = new();

    public FakeSignInManagerBuilder With(Action<Mock<FakeSignInManager>> mock)
    {
        mock(_mock);
        return this;
    }

    public Mock<FakeSignInManager> Build()
    {
        return _mock;
    }
}